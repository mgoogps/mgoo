 
function userliclick(_userid, _source) {

    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/GetDevicesListByUserID", 
        dataType: "json",
        contentType: mgoo.contentType,
        type: "POST",
        data: "{ UserID :'" + _userid + "'}", 
        success: function (data) {
            var panel = $("#bjui-collapse1 div.panel-body #mg-monitor-device-list");
            var d = JSON.parse(data.d);
            $("#msgCount").text(d.msgCount);
            d = d.data;
            var color = ""; var speedStatus = ""; var status;
      
            if (_source == "onload" || _source == "ztreeclick") {

                var select2html = [];
                select2html.push('<option data-tokens=""></option>');
                var html = [];
                var aId;
                var mTop = 0;
                var explorer = navigator.userAgent;

                if (explorer.indexOf("Firefox") >= 0) {
                    mTop = -15;
                } 
                for (var i = 0; i < d.length; i++) {
                    if (d[i].OnLine) { color = "color:#2FB64D;"; }
                    else { color = " color: gray"; }
                    status = loadDeviceStatus(d[i].DeviceStatus); aId = "mg-monitor-device-list_" + d[i].Id + "_a";
                    //  <li data-id="422" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">运行统计</li>
                    html.push('<li data-id="100' + i + '" data-pid="99" data-url="" data-tabid="doc-file" data-faicon="caret-right">');
                    html.push('<a id="' + aId + '" class="leve1 faicon" onclick=\'deviceliclick("' + d[i].Id + '")\' dname="' + d[i].Name + '" imei="' + d[i].Id + '" target="_blank" title="' + d[i].Name + " - " + d[i].Id + '" style="width:100%">');
                    html.push('<span id="mg-monitor-device-list_' + i + '_ico" title="" treenode_ico class="button ico_docu" style="><i class="fa fa-taxi"></i></span><span id="mg-monitor-device-name_' + i + '_span" style="' + color + '">' + d[i].Name + '</span>');
                    html.push('<span id="mg-monitor-device-speed_' + d[i].Id + '_span" style="float:right;width:70px;margin-top:' + mTop + 'px">' + status + '</span>');   //margin-top:-18px
                    html.push('</a> </li>');
                    select2html.push('<option data-tokens="' + d[i].Id + '" value="' + d[i].Id + '">' + d[i].Name + '</option>');
                 
                    if (status == "未激活")
                        continue;
                    var mkr = new Marker({
                        map: mgoo.map, course: d[i].Direction, lat: d[i].Lat, lng: d[i].Lon, titleText: d[i].Name, DeviceID: d[i].Id, line: d[i].OnLine ? "Online" : "Offline"
                    });
                  mkr.show();
                    mkr.addEventListener("click", function (id) {
                        $("#mg-monitor-device-list_" + id + "_a").trigger("click");
                    });
                }
                $(panel).empty().append(html.join(''));
                $("#select2Device").empty().append(select2html.join('')).selectpicker('refresh');
                //setTimeout(function () {
                //    $("li[data-original-index=2] a").attr("data-tokens", '4210032550');
                //},2000);
                $("#dockContainer").css({ visibility: "hidden" });
            } else {
                for (var i = 0; i < d.length; i++) {
                    if (d[i].OnLine) { color = { color: "#2FB64D" }; }
                    else { color = { color: "gray" }; }
                    status = loadDeviceStatus(d[i].DeviceStatus);
                    $('#mg-monitor-device-speed_' + d[i].Id + '_span').text(status);
                    $('#mg-monitor-device-name_' + d[i].Id + '_span').text(d[i].Name).css(color); 
                    tabClick($("#mgTabs li.active a"));
                    if (status == "未激活")
                        continue;
 
                    var mkr = new Marker({
                        map: mgoo.map, course: d[i].Direction, lat: d[i].Lat, lng: d[i].Lon, titleText: d[i].Name, DeviceID: d[i].Id, line: d[i].OnLine ? "Online" : "Offline"
                    });
                    mkr.show();
                    mkr.addEventListener("click", function (id) {
                        $("#mg-monitor-device-list_" + id + "_a").trigger("click");
                    });
                }
                mgoo.isAutoSetZoom = false;
                $("#mg-monitor-device-list_" + mgoo.DeviceID + "_a").trigger("click");
            }
            //显示全部设备数量，在线设备数量，离线设备数量，未激活设备数量 
            var online = $("#mg-monitor-device-list li").not(":contains(未激活)").not(":contains(离线)").length;        
            var alldevice = $("#mg-monitor-device-list li").length;
            var offline = $("#mg-monitor-device-list li:contains('离线')").length;
            var notactivate = $("#mg-monitor-device-list li:contains('未激活')").length;
           
            $("#mgTabs li a[href=#alldevice]").html("全部<span style=font-size:8px;width:16px;-webkit-transform:scale(0.7);-o-transform:scale(1);display:inline-block;color:red>(" + alldevice + ")</span>")
            $("#mgTabs li a[href=#online]").html("在线<span style=font-size:8px;width:16px;-webkit-transform:scale(0.7);-o-transform:scale(1);display:inline-block;color:red >(" + online + ")</span>")
            $("#mgTabs li a[href=#offline]").html("离线<span style=font-size:8px;width:16px;-webkit-transform:scale(0.7);-o-transform:scale(1);display:inline-block;color:red>(" + offline + ")</span>")
            $("#mgTabs li a[href=#activation]").html("未激活<span style=font-size:8px;width:16px;-webkit-transform:scale(0.7);-o-transform:scale(1);display:inline-block;color:red>(" + notactivate + ")</span>")          
        },
        error: function (error) {
            console.log("错误！！！"); 
        }
    });
}

function refreshTime() {
    if (!mgoo.islogin) {
        return;
    }
    mgoo.RefreshTime--;
    if (mgoo.RefreshTime < 0) {
        mgoo.clearOverlays({ clearAll: false });
        userliclick(mgoo.CurrentzTreeUserID,"refreshTime");
        mgoo.RefreshTime = 10;
    } 
    var chked = " "; 
    if ($("#chkRefresh").attr("checked") || $("#chkRefresh").attr("checked") == "checked") {
        chked = "checked='true'"; 
        setTimeout("refreshTime()", 1000);
    } 
    $("#div_refreshTime").html(mgoo.RefreshTime + trackingPage.secondMsg + " <input type='checkbox' onclick='startTime()' id='chkRefresh' " + chked + " />");
}
//var signal=0;
function deviceliclick(_id)
{
    if ($("#mg-monitor-device-speed_" + _id + "_span").text() == "未激活")
    {
        $($("body")).alertmsg('info', '该设备未激活！') 
        return;
    } 
    if ($("#jqDock").css("visibility") == "hidden") { 
        $("#jqDock").css({ visibility: "visible" }).find(".jqDock").css({ "background-color": "#A0BDD4" }); //, left: ($("#allmap").width()/2-150) + "px"
    }
    $("#aDeviceInfo").attr("href", "/Monitor/deviceinfo.html?id="+_id);
    mgoo.Update_DeviceID = _id;
    $("#aTracking").attr("href", "/Monitor/Tracking.aspx?id=" + mgoo.CurrentzTreeUserID + "&deviceid=" + _id + "&t=" + new Date().getTime());
    $("#aHistory").attr("href", "/Monitor/History.aspx?id=" + mgoo.CurrentzTreeUserID + "&deviceid=" + _id + "&name=" + $("#mg-monitor-device-list_" + _id + "_a").attr("dname") + "&t=" + new Date().getTime());
    $("#aGeofences").attr("href", "/Monitor/Tracking.aspx?id=" + mgoo.CurrentzTreeUserID + "&deviceid=" + _id + "&t=" + new Date().getTime());
    $("#aCommandRecord").attr("href", "/Monitor/Tracking.aspx?id=" + mgoo.CurrentzTreeUserID + "&deviceid=" + _id + "&t=" + new Date().getTime());
    //$("#aSendCommand").attr("href", "/Monitor/Tracking.aspx?id=" + mgoo.CurrentzTreeUserID + "&deviceid=" + _id + "&t=" + new Date().getTime());
  
    if (_id.indexOf(mgoo.DeviceID) < 0) { 
        $("#mg-monitor-device-list_" + mgoo.DeviceID + "_a").css({ "margin-top": "0px" }).parent().css({ "background-color": "", "height": "18px" });
    }
    $("#mg-monitor-device-list_" + _id + "_a").css({ "margin-top": "10px" }).parent().css({ "background-color": "#C6C6C6", "height": "35px" });
    mgoo.DeviceID = _id;
     
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/GetDeviceData", 
        dataType: "json",
        contentType: mgoo.contentType,
        type: "POST",
        data: "{ Imei :'" + $("#mg-monitor-device-list_" + _id + "_a").attr("imei") + "'}", 
        success: function (r) { 
            var d = JSON.parse(r.d);
          
            if (d.Lon < 1 || d.Lat < 1) {
                d.Lon = d.Lbs.lon; d.Lat = d.Lbs.lat;
                $($("body")).alertmsg('info', '经纬度出现错误！');
               // return;
            } 
            showInfoWindow(d);
        },
        error: function (error) {
            $($("body")).alertmsg('info', '查询出错！')
            console.log("错误！！！"); 
        }
    });

}
function showInfoWindow(d) {
    if (d.Lbs) {
       // d.DeviceTime = d.Lbs.time;
      //  d.Lat = d.Lbs.lat == "" ? 0.006 : d.Lbs.lat;
       // d.Lon = d.Lbs.lon == "" ? 0.0065 : d.Lbs.lon;
    }
    var point = new BMap.Point(d.Lon, d.Lat); 
    if (mgoo.isAutoSetZoom) {
        mgoo.centerAndZoom(point, 18);
    }  
    var html = [];
    var height = 205;
    var signal = 0;
    html.push('<fieldset style="height:' + height + 'px;"> <legend><a style="text-decoration:none;">' + d.Name + '</a></legend>');
    html.push("<p><a style='text-decoration:none;'>IMEI号:" + d.Id + "</a></p>");   
    html.push("<p><a style='text-decoration:none;'>状态:" + deviceStatus(d.Status, d.Model) + "</a></p>"); 
    html.push("<p><a style='text-decoration:none;'>定位时间:" + d.DeviceTime.replace('T', ' ').replace('Z', '') + "</a></p>");
   
    if (d.Lbs && d.Lbs.time) {
        html.push("<p><a style='text-decoration:none;'>定位方式:LBS" + "</a> ");
    } else {
        if (d.Gps <= 4) {
            d.Gps = d.Gps ; 
            html.push("<p><a style='text-decoration:none;' title='卫星数量：" + d.Gps + "'>方式:<img src='/Scripts/icons/image/gps_1.png' alt=''width=14 height=14/><font color='red' style='font-weight:bold'>" + d.Gps + "</font></a>&nbsp");
        } else {
            d.Gps = d.Gps;
            html.push("<p><a style='text-decoration:none;' title='卫星数量：" + d.Gps + "'>方式:<img src='/Scripts/icons/image/gps_2.png' alt=''width=14 height=14/><font style='font-weight:bold'>" + d.Gps + "</font></a>&nbsp ");
        } 
    } 
    var img;
    var type;
    var signal = d.Gsm;
    if (signal==1) {
        img = "no";
        type="无信号"
    }
    else if (signal == 2) {
        img = "one";
        type = "信号弱"
    } else if (signal == 3) {
        img = "two";
        type = "信号微弱"
    } else if (signal == 4) {
        img = "three"
        type = "信号强"
    } else if (signal==5) { 
        img = "for"
        type = "信号满格"
    }
    html.push("<a style='text-decoration:none;' title=" + type + ">信号:<img src='/Scripts/icons/image/" + img + ".png' alt=''width=14 height=14/></a>");
   
    var bat = d.Battery;
    var icon;//图标
    var prompt;//提示
    //显示电量
    if (bat <= 10) {
        icon = "low";
        prompt = "电量不足";
    } else if (bat > 10 && bat <= 30) {
        icon = "mid";
        prompt = "剩余电量:" + bat;
    } else if (bat > 30 && bat <= 60) {
        icon = "high";
        prompt = "剩余电量:" + bat;
    } else if (bat > 60 && bat <= 80) {
        icon = "full";
        prompt = "剩余电量:" + bat;
    } else if (bat > 80 && bat <= 100) {
        icon = "charge";
        prompt = "剩余电量:" + bat;
    }   
    html.push("<a style='text-decoration:none; href='javascript:;' class='lock' data-original-title='' title='" + prompt + "'>&nbsp电量:<span class='typcn typcn-battery-" + icon + "' style='font-size:18px;'></span></a>")
    //  html.push("<span class='fa fa-battery-" + icon + "' aria-hidden='true'>电量</span>");
    html.push("</p>");
    var status = loadDeviceStatus(d.DeviceStatus, "showHtml"); 
    if (d.DeviceStatus.split(',')[0] == 3) {
        html.push("<p><a style='text-decoration:none;'>停止 </a></p>"); //:" + status.split('-')[1] + "
    } else if (d.DeviceStatus.split(',')[0] == 2) {
        html.push("<p><a style='text-decoration:none;'>速度:" + d.Speed + "公里/小时</a></p>");
    } else if (d.DeviceStatus.split(',')[0] == 1) {
        html.push("<p><a style='text-decoration:none;'>离线:" + status.split('-')[1] + "</a></p>");
    }
    html.push("<p><a style='text-decoration:none;' title='点击复制地址' id='Copy_A'>地址:" + d.Address + " </a></p>");
    html.push("<div style=\"position:absolute;bottom:10px;font-size:12px;font-weight:bold;color:red;\">");

    html.push('<a  target="_blank" style="text-decoration:none;font-weight:bold; hover{color:red;}"; href="/Monitor/deviceinfo.html?id='+d.Id+'" data-toggle="dialog" data-id="DeviceInfo" title="设备信息" data-width="600" data-height="440" data-on-load="editDeviceInfo"  data-reload-warn="本页已有打开的内容，确定将刷新本页内容，是否继续？" data-before-close="deviceinfo_navtab_beforeClose" id="DeviceInfoday">设备信息&nbsp</a>') 
    html.push('<a  target="_blank" href="/Monitor/Tracking.aspx?id=' + mgoo.CurrentzTreeUserID + '&deviceid='+d.Id+'&t='+new Date().getTime()+'" id="Trackingday" style="text-decoration:none;font-weight:bold;" title="实时跟踪" >实时跟踪 </a>');
    html.push("<a  target='_blank' href='/Monitor/History.aspx?id=" + mgoo.CurrentzTreeUserID + "&deviceid="+d.Id+"&name="+d.Name+"&t="+new Date().getTime()+"'id='Historyday' style='text-decoration:none;font-weight:bold;'title='历史轨迹' >历史轨迹 </a>");
    html.push("<a  target=_blank' href='/Monitor/sendcommand.html' data-toggle='dialog' data-id='SendCommand' data-title='发送指令' data-width='600' data-height='440' data-on-load='sendCommand_on_load' id='SendCommandday'style='text-decoration:none;font-weight:bold;'title='发送指令' >发送指令 </a>");
    html.push("</div>");
    html.push(' </fieldset> ');
   
    var infoBox = new InfoWindow();
    infoBox.map = mgoo.map;
    infoBox.addInfoWindow({
        style: { //filter:alpha(Opacity=80),color: "red", , overflow: "hidden" map: mgoo.map, 
            fontSize: "18px",  height: (height+4)+"px", width: "245px", "-moz-border-radius": "15px", "border-radius": "15px"
        }, html: html.join(''), point: point, "class": "panel-body"
    }); 
    mgoo.isAutoSetZoom = true;
   // mgoo.InfoWindow = infoBox;
   // getAddress("Copy_A"  , d.Lat, d.Lon);
  //  copya();
}
function startTime() {
    if ($("#chkRefresh").attr("checked") || $("#chkRefresh").attr("checked") == "checked") {
        refreshTime();
    } 
}
function tabClick(_this)
{ 
    $("#mg-monitor-device-list li").show(); 
    var tabTxt = $(_this).text();
     if ( tabTxt.indexOf("在线")>-1) {
        $("#mg-monitor-device-list li").not(":contains(停止)").not(":contains(行驶)").css({ "display": "none" });
    } else if (tabTxt.indexOf("全部设备")>-1) {
        $("#mg-monitor-device-list li").css({ "display": "block" });//.show(500);
    } else if (tabTxt.indexOf("离线")>-1) {
        $("#mg-monitor-device-list li").not(":contains(离线)").css({ "display": "none" });
    } else if (tabTxt.indexOf("未激活") > -1){
        $("#mg-monitor-device-list li").not(":contains(未激活)").css({ "display": "none" });
    }   
}

function loadDeviceStatus(obj, type) {
    if (obj.split(',').length == 1) {
        if (obj == 1) {
            return "离线";
        } else if (obj == 0) {
            return "未激活";
        } else if (obj == 2) {
            return "行驶";
        } else if (obj == 3) {
            return "停止";
        }
    }
    if (obj.split(',')[0] == 1) {
        if (type) {
            return "离线"// "离线-" + MinuteToHour(obj.split(',')[1]);
        }
        return "离线"//"离线-" + MinuteToHour(obj.split(',')[1], "day");
    } else if (obj.split(',')[0] == 0) {
        return "未激活";
    } else if (obj.split(',')[0] == 2) {
        return "行驶-" + obj.split(',')[1];
    } else if (obj.split(',')[0] == 3) {
        if (type) {
            return "停止"; //-" + MinuteToHour(obj.split(',')[1])
        }
        return "停止";//+ MinuteToHour(obj.split(',')[1], "day")
        
    }
}
 
function msgHaveRead(objectid) { 
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/DeleteMsg",
        dataType: "json",
        contentType: mgoo.contentType,
        type: "POST",
        data: "{ ObjectID :'" + objectid + "'}",
        success: function (r) {
            if (r.d > 0) { 
                $($("body")).alertmsg('correct', '删除成功！')
            } else {
                $($("body")).alertmsg('warn', '删除失败！')
            } 
        }
    });
}

function deviceStatus(str,m) { 
    var power = str.substring(0, 1);
    var acc = str.substring(1, 2);
    var protect = str.substring(2, 3);
    var oil = str.substring(3, 4); 
    var s = [];
    if (power == 1) {
        if (m == "X28") {
            s.push("正在充电");
        } else {
            s.push("主电连接");
        } 
    } else {
        if (m != "X28") {  
            s.push("<font color='red' style='font-weight:bold'>主电断开</font>");
        } 
    }
    if (m != "X28") {
        if (acc == 1) {
            s.push("ACC开");
        } else {
            s.push("ACC关");//<font color='red' style='font-weight:bold'></font>
        }
    }
    if (protect == 1) {
        s.push("设防");
    } else {
        s.push("撤防");//<font color='red' style='font-weight:bold'>撤防</font>
    }
    if (m != "X28") {
        if (oil == 1) {
            s.push("油电正常");
        } else {
            s.push("<font color='red' style='font-weight:bold'>油电断开</font>");
        }
    }
    return s.join(' ');
}

function Battery(b) { 
    var h = [];
    h.push("<a style='text-decoration:none;' title='电池电量: "+b+"%'>电量:");
    if (b <= 10) {
        h.push('<i class="fa fa-battery-empty" aria-hidden="true"></i>');
        return "<i class='fa fa-battery-empty'aria-hidden='true' ></i>";
    } if (b>10 && b <= 30) {
        h.push('<i class="fa fa-battery-quarter aria-hidden="true""></i>');
        return "<i class='fa fa-battery-quarter'aria-hidden='true' ></i>";
    } if (b>30 && b<=60) {
        h.push('<i class="fa fa-battery-half" aria-hidden="true"></i>');
        return "<i class='fa fa-battery-half'aria-hidden='true' ></i>";
    } if (b > 60 && b <= 80) {
        h.push('<i class="fa fa-battery-quarters" aria-hidden="true"></i>');
        return "<i class='fa fa-battery-quarters'aria-hidden='true' ></i>";
    } if (b > 80 && b <= 100) {
        h.push('<i class="fa fa-battery-full" aria-hidden="true"></i>');
        return "<i class='fa fa-battery-full'aria-hidden='true' ></i>";
    }
    h.push("</a>"); 
    //return h.join('');
}
//function getAddress(id,lat ,lon)
//{ 
//    $.ajax({
//        url: mgoo.ajaxUrl + "DeviceMonitor.asmx/GetAddress",
//        dataType: "json",
//        contentType:  mgoo.contentType ,
//        type: "POST",
//        data: "{ lat :'" + lat + "',lon:'" + lon + "',mapType:'BAIDU',language:'ZH-CN'}",
//        success: function (r) {
//            console.log($("#" + id).length,r.d);
//            $("#" + id).text("地址:"+r.d);
//        },
//        error: function (error) {
//            console.log("获取地址错误！！！");
//            console.log(error);
//        }
//    }); 
//}

function AddGroups(id,parent) {
    $.ajax({
        url: mgoo.ajaxUrl + "DeviceMonitor.asmx/AddGroups",
        data: "{ id :'" + id + "',parent:'" + parent + "',username:'" + id + "',password:'',cellphone:'',address:'',path:'',email:'',weixin:''}",
        success: function (r) {
            console.log(r);
        }
    });
}