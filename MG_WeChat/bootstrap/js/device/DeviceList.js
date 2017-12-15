var isPostback = true;
$(function () {
  
    GetGroupsList();
    //setInterval("GetDevicesList()", 20000);
  
});

function GetGroupsList() {
    var info = mg.GetUserInfo(); 
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetGroupList",
        pars: { userid: info.UserID },
        success: function (data) {
            var groups = JSON.parse(data);

            var html = [];
            for (var i = 0; i < groups.length; i++) { 
                html.push(' <li class="am-panel"> <a data-am-collapse="{parent: \'#DivDeviceList\', target: \'#GroupDeviceList' + groups[i].GroupID + '\'}">   ');
                html.push(' <i class="am-icon-plus-square am-margin-left-sm"></i> ' + groups[i].GroupName + ' <span class="am-badge am-badge-success am-margin-right">0</span> </a>');
                html.push(' <ul class="am-list am-collapse admin-sidebar-sub" id="GroupDeviceList' + groups[i].GroupID + '" name="devicesList"> </ul> </li>');
            }
            $("#DivDeviceList").empty().append(html.join(''));
     
            GetDevicesList();
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function GetDevicesList() {
    var info = mg.GetUserInfo();
  
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetDevicesList",
        pars: { userid: info.UserID },
        success: function (data) {
            var deviceList = JSON.parse(data);
            if (deviceList.length == 0) {
                window.location.href = "/myinfo/adddevice.html";
            }
            $("ul[name=devicesList]").empty();
            for (var i = 0; i < deviceList.length; i++) {
                var device = deviceList[i];
                var html = []; 
                var state = getStatus(device.Status, device.Speed, device.StatusMinute, device.DataContext,device);
                //<li class="am-g">
                //               <a href="../device/Tracking.html"> 粤SK8888 </a>
                //               <div class="am-list-item-text">主电连接 撤防</div>
                //               <span class="am-list-date">静止</span>
                //               <span class="am-list-date" style="top:40px;">10天10时10分</span>
                //           </li>
                var statusStr = state.zhudian + '-' + state.shefang + state.dianliang;
                if (device.Model == "MG-X11BDY") {
                    statusStr = state.zhudian + "-" + state.acc + '-' + state.shefang+"-"+state.shache;
                }
                html.push('<li class="am-g" name="GroupDeviceList' + device.GroupID + '" style="display: none;">');
                html.push('<a href="/device/Tracking.html?deviceid=' + device.DeviceID + '" status=' + (device.Status == 2 || device.Status == 4 ? 2 : device.Status) + '>' + device.DeviceName + ' </a>');
                html.push('<div class="am-list-item-text">' + statusStr + '</div>');
                // html.push('<span class="am-list-date" style="width:70px;text-align:left">' + state + '</span>');
                html.push(' <span class="am-list-date">' + state.status + '</span>');
                html.push(' <span class="am-list-date" style="top:37px;">' + state.continue + '</span>');
                html.push('</li>');
                $("#GroupDeviceList" + device.GroupID).append(html.join('')); 
            } 
             
            $("ul[name=devicesList]").on('open.collapse.amui', function () {
                $(this).prev().find("i").eq(0).removeClass("am-icon-plus-square");
                $(this).prev().find("i").eq(0).addClass("am-icon-minus-square"); 
            }).on('close.collapse.amui', function () {
                $(this).prev().find("i").eq(0).removeClass("am-icon-minus-square");
                $(this).prev().find("i").eq(0).addClass("am-icon-plus-square");
            });
            if (isPostback) {
                //打开默认分组
                $('#GroupDeviceList-1').collapse('open');
                isPostback = false;
            }
            tabClick();
        },
        error: function () {

        }
    });
}


function tabClick(_this) { 
    if (_this) {
        $(_this).parent().find("li").removeClass("am-active");
        $(_this).addClass("am-active");  
    } 
    var tab = $("#tab-status li");
 
    for (var i = 0; i < tab.length; i++) {
        var a= $(tab[i]).text();
        if (a.indexOf('在线') >= 0) {
            $(tab[i]).find("a").text("在线(" + $("#DivDeviceList ul a[status=1]").length + ")"); 
        }
        if (a.indexOf('离线') >= 0) {
            $(tab[i]).find("a").text("离线(" + $("#DivDeviceList ul a[status=2]").length+ ")");
        }
        if (a.indexOf('未激活') >= 0) {
            var sum = parseInt($("#DivDeviceList ul a[status=3]").length) ;
            $(tab[i]).find("a").text("未激活(" + sum + ")");
        }
        if (a.indexOf('全部') >= 0) {
            $(tab[i]).find("a").text("全部(" + $("#DivDeviceList ul a").length + ")");
        }
    }
    search();
}

function getStatus(status, speed, time, DataContext,d) {
    var res = {};
  
    var context = DataContext.split('-'); 
    if (context.length >= 4) {
        if (context[0] == 0)
        {
            res.acc = "电源关";
        } else {
            res.acc = "电源开";
        }
        if (context[1] == 0) {
            res.shefang = "撤防";
        } else {
            res.shefang = "设防";
        }
        if (context[3] == 0) {
            res.zhudian = "主电断开";
        } else {
            res.zhudian = "主电连接";
        }
        res.dianliang = "";
        if (context[4]) {
            res.dianliang = "-电量:"+ context[4]+"%";
        }
        if (context[10]) {
            res.zhudian += context[10]+"V";
        }
        if (context[11]) {
            if (context[11] == 0) {
                res.shache = "已刹车";
            } else if (context[11] == 1) {
                res.shache = "未刹车"; 
            }
        }  
    } 
    res.continue = MinuteToHour(time);
    switch (status) {
        case "1":
            if (speed == 0) {
                res.status = "停止";
            } else {
                res.status = "行驶";
                res.continue = speed + "km/h";
            }
            break;
        case "2":
            res.status = "离线";
            break;
        case "3":
            res.status = "未激活";
            res.continue = "";
            break;
        case "4":
            var info = mg.GetUserInfo();
            var o = info.Token.split('@')[0];
            var url = "../pay/Recharge.aspx?deviceid=" + d.DeviceID + "&openid=" + o + "&imei=" + d.SerialNumber + "&t=" + new Date().getTime();
            res.status = "<span style='color:red' onclick='window.location.href=\""+url+"\"'>欠费</span>"; 
            break;
    } 
    return res;
}

var timer;
function search(interval) {
    if (timer) {
        clearTimeout(timer);
    }
    timer = setTimeout(function () {
        var val = $("#txtSearch").val(); 
        var v = $("#tab-status li.am-active").attr("value"); 
        var li;
        if (v == 4) {
            li = $("#DivDeviceList li ul li a"); 
        } else { 
            li = $("#DivDeviceList li ul li a[status=" + v + "]"); 
        }
       
        var dlist = $("#DivDeviceList li ul li a");
        var len = dlist.length;  
        for (var i = 0; i < len; i++) { 
            var av = $(dlist[i]).attr("status");
            if (v == 4) {
                if ($(dlist[i]).text().indexOf(val) >= 0) { 
                    $(dlist[i]).parent().show();
                } else {
                    $(dlist[i]).parent().hide(); 
                }
            } else {
                if ($(dlist[i]).text().indexOf(val) >= 0 && av == v) { 
                    $(dlist[i]).parent().show();
                } else {
                    $(dlist[i]).parent().hide(); 
                }
            }
        }
        var g = $("ul[name=devicesList]"); 
        for (var i = 0; i < g.length; i++) { 
            var none = $("li[name=" + $(g[i]).attr("id") + "][style='display: none;']");
            var show = $("li[name=" + $(g[i]).attr("id") + "]"); 
            $(g[i]).prev().find("span").text((show.length - none.length));
        } 
       
  
    }, interval || 0);
}