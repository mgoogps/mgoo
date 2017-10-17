var imageName;
var s = false;
var change = false;
var userinfochange = false;

function editDeviceInfo($dialog) {
    if (!$dialog) {
        return
    } 
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/GetDeviceInfo", 
        data: "{ Imei :'" + mgoo.Update_DeviceID + "'}",
        success: function (r) { 
            var d = JSON.parse(r.d); 
            $($dialog).find("legend").text(d.Id); 
            $("#txtDeviceInfoName").val(d.Name);
            $("#txtModel").val(d.Model);
            $("#txtCreatTime").val(d.CreatTime);
            $("#txtEndTime").val(d.EndTime);  
            $("#txtContact").val(d.Contact);
            $("#txtCellPhone").val(d.CellPhone);    
            $("#txtCarNumber").val(d.CarNumber);
            $("#txtSimNumber").val(d.SimNumber);
            $('#txtLbs').on('ifChanged', function (event) {
                change = true;
            });
            if (d.Islbs)
                $('#txtLbs').iCheck('check'); 
            if (d.Image && d.Image != "undefined") {
                imageName = d.Image;
                $("#imgCarImage").attr("src", "/Upload/" + d.Image);
                $("#aRemoveImg").show();
            } else {
                $("#imgCarImage").attr("src", "/Scripts/icons/noimage.jpg");
            }
        },
        error: function (error) {
            console.log("错误！！！");
        }
    });
}
//上传图片成功回调函数
function doc_upload_success(file, data, $element) {
    var d = JSON.parse(data);
    if (!d.success || d.success=="false") {
        $("body").alertmsg('error', '上传失败！')
        return;
    }
    imageName = d.success;
    $("#imgCarImage").attr("src", "/Upload/" + d.success);
    $("#aRemoveImg").show();
    change = true;
    if (s) {
        sendDeviceInfo(); s = !s;
    } 
}

function saveDeviceInfo() {
  
    if (!$("#doc_pic_up div.queue").is(":hidden")) {
        $("#doc_pic_up div.queue").alertmsg('confirm', '保存时是否上传图片？', {
            okCall: function () {
                s = true;
                $("span.up_confirm").trigger("click");
            }, cancelCall: function  () {
                
            }
        });
    } else {
        sendDeviceInfo();
    } 
}

function sendDeviceInfo() { 
    var name = $("#txtDeviceInfoName").val();
    var contact = $("#txtContact").val();
    var cellphone = $("#txtCellPhone").val();
    var carnumber = $("#txtCarNumber").val();
    var simnumber = $("#txtSimNumber").val();
    var lbs = $("#txtLbs").attr("checked") ? true : false;
    var carimage = $("#imgCarImage").val(); 
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/UpdateDeviceInfo", 
        data: "{ ID :'" + mgoo.Update_DeviceID + "',Name:'" + name + "',CarNumber:'" + carnumber + "',Contact:'" + contact + "',CellPhone:'" + cellphone + "',Image:'" + imageName + "',IsLbs:'" + lbs + "'}",
        success: function (r) {
            change = false;
            if (r.d == 1) {
                $("body").alertmsg('ok', '保存成功！')
                $("#btnDeviceInfoClose").trigger("click");
            } else {
                $("body").alertmsg('error', '保存失败！')
            }
        }
    });
}

function removeImg() {
    imageName = "";
    change = true;
    $("#imgCarImage").attr("src", "/Scripts/icons/noimage.jpg");
    $("#aRemoveImg").hide(); 
}
  
function userinfo_dialog_onload($dialog) {
   
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/GetUserInfo",
        data: "{}",
        success: function (r) {
            var d = JSON.parse(r.d);
            $($dialog).find("legend").text(d._id);
            $("#txtUserName").val(d.username);
            $("#txtEmail").val(d.email);
            $("#txtAddress").val(d.address);
        }
    });
}

function saveUserInfo(btn){
    var name = $("#txtUserName").val();
    var email = $("#txtEmail").val();
    var address = $("#txtAddress").val();
    var user = $("#txtSessionID").val()
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/UpdateUserInfo",
        data: "{User:'" + user + "',UserName:'" + name + "',eMail:'" + email + "',Address:'" + address + "'}",
        success: function (r) {
            userinfochange = false;
            if (r.d == 1) {
                $("body").alertmsg('ok', '保存成功！') 
                $("#btnUserinfoClose").trigger("click");
            } else {
                $("body").alertmsg('error', '保存失败！')
            } 
        }
    });
}

function deviceinfo_navtab_beforeClose($dialog) { 
    if (change || !$("#doc_pic_up div.queue").is(":hidden")) {
        $("#doc_pic_up div.queue").alertmsg('confirm', '数据已经改动是否退出？', {
            okCall: function () {
                change = false;
                $("#doc_pic_up div.queue").hide(); 
                $("#btnDeviceInfoClose").trigger("click"); 
            }, cancelCall: function () {

            }
        });
        return false;
    }
    return true 
}

function userinfo_navtab_beforeClose($dialog) { 
    if (userinfochange) {
        $(".bjui-pageContent").alertmsg('confirm', '数据已经改动是否退出？', {
            okCall: function () {
                userinfochange = !userinfochange;
                $("#btnUserinfoClose").trigger("click");
            }, cancelCall: function () {

            }
        });
        return false;
    }
    return true
}

function msgClick() {
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/GetAlarms",
        dataType: "json",
        contentType: mgoo.contentType,
        type: "POST",
        data: "{ UserID :'" + mgoo.CurrentzTreeUserID + "'}",
        success: function (r) {
            var d = JSON.parse(r.d);
            var html = [];
            var index = 0;
            for (var i = d.length-1; i >= 0; i--) {
                html.push(' <tr><td>' + (++index) + '</td>  <td>' + d[i].Imei + '</td><td>' + d[i].UserID + '</td><td>' +msgName( d[i].AlarmType) + '</td><td>' + d[i].Alarmtime.replace('T', ' ').replace('Z', '').substring(0, 19) + '</td><td>X-21</td><td><a href="#" onclick="msgHaveRead(\'' + d[i].Id + '\')">处理</a></td> </tr>');
            }
            $("#msgTable").empty().append(html.join(''));
        }
    });
}
function msgName(msgType) {
    switch (msgType) {
        case "1":
            return "震动报警";
        case "2":
            return "断电报警";
        case "3":
            return "接通报警";
        case "4":
            return "SOS";
        case "5":
            return "出电子围栏报警";
        case "6":
            return "入电子围栏报警";
        case "7":
            return "超速报警";
        default:
            return "其他报警";
    }
}

function getAddress() { 
    var arg = arguments;
    $.ajax({
        url: "/AjaxService/MgooPublic.asmx/GetAddress",
        data: "{lat:'" + arg[0] + "',lon:'" + arg[1] + "'}",
        success: function (r) { 
            $(arg[2]).parent().text(r.d);
        } 
    });
}

function sendCommand_on_load($dialog) {
   
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/GetCommandConfig",
        data: "{deviceid:'" + mgoo.DeviceID + "',userid:'"+mgoo.CurrentzTreeUserID+"'}",
        success: function  (r) { 
            var d = JSON.parse(r.d);
            if (d.Phone != "") {
                $("#txtUnbundPhone").text("当前绑定号码:"+d.Phone);
            } if (d.Mode != "") {
                $("#selectMode option[value=" + d.Mode + "]").attr("selected", true);
                if (d.Sens != "") {
                    $("#selectSens option[value=" + d.Sens + "]").attr("selected", true);
                    $("#selectSens").show().prev().show().next().next().show();
                }
            }
           
        }
    });
   // console.log($dialog);
}
function loading() {
    var html = [];
    html.push('<div class="bjui-maskProgress bjui-ajax-mask" style="z-index: 2;"><i class="fa fa-cog fa-spin"></i>&nbsp;&nbsp;正在努力加载数据，请稍等...<div class="progressBg"><div class="progress"></div></div></div>');
    html.push(' <div class="bjui-maskBackground bjui-ajax-mask" style="z-index: 1;"></div>');
    $("#bjui-navtab").find('div.bjui-pageHeader').before(html.join(''));
}
function removeloading() {
    $("#bjui-navtab").find('div.bjui-maskProgress').remove();
    $("#bjui-navtab").find('div.bjui-maskBackground').remove();
}

    function getNowFormatDate(day) {
        var date = new Date();
        var seperator1 = "-";
        var seperator2 = ":";
        var month = date.getMonth() + 1;

        var strDate = date.getDate();
        if (day) { 
            strDate = strDate - day;
        }
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
                + " " + date.getHours() + seperator2 + date.getMinutes()
                + seperator2 + date.getSeconds();
        return currentdate;
    }

    function MeterToKilometer(Meter) {
        Meter = parseFloat(Meter);
        if (Meter < 1000) {
            return Meter.toFixed(2) + allPage.m;
        }
        return (Meter / 1000).toFixed(2) + allPage.km;
    }

    function MinuteToHour(mi, split) {
        try {
            if (split != undefined && split != "") {
                if (mi <= 0)
                    return "1" + allPage.minute;
                if (mi <= 60) {
                    return parseInt(mi) + allPage.minute;
                }
                var day = parseInt(mi / 60 / 24);
                var h = parseInt(mi / 60 % 24);
                var m = parseInt(mi % 60);
                mi = "";
                if (day > 0) {
                    return day + allPage.day;
                }
                if (h > 0) {
                    return h + allPage.hour;
                }
                if (m > 0) {
                    return parseFloat(m) + allPage.minute;
                }
            }
            if (mi <= 0)
                return "1" + allPage.minute;
            if (mi <= 60) {
                return parseInt(mi) + allPage.minute;
            }
            var day = parseInt(mi / 60 / 24);
            var h = parseInt(mi / 60 % 24);
            var m = parseInt(mi % 60);
            mi = "";
            if (day > 0) {
                mi = day + allPage.day;
            } if (h > 0) {
                mi += h + allPage.hour;
            } if (m > 0) {
                mi += parseFloat(m) + allPage.minute;
            }
            return mi;
        } catch (e) {
            console.log(e);
        }
    }
    //返回分钟数
    function DateDiffMi(start, end) {
        try {
            start = start.replace(/-/g, '/');
            end = end.replace(/-/g, '/');
            var a = new Date(start);
            a = a.getTime();
            var b = new Date(end);
            b = b.getTime();
            var ticksspan = b - a;
            return ticksspan / 60 / 1000;  //返回分钟数 
        } catch (e) {

        }
    }
    function DateDiff(start, end) {
        try {
            start = start.replace(/-/g, '/');
            end = end.replace(/-/g, '/');
            var a = new Date(start);
            a = a.getTime();
            var b = new Date(end);
            b = b.getTime();
            var ticksspan = b - a;
            //return ticksspan / 60 / 1000          //返回分钟数
            return ticksspan / 60 / 1000 / 24 / 60; //返回天
        } catch (e) {

        }
    }
    //获取当前时间
    function GetCurrentDate() {
        var date = new Date(+new Date() + 8 * 3600 * 1000).toISOString().replace(/T/g, ' ').replace(/\.[\d]{3}Z/, '')
        return date;
    }

    /**
     ** 加法函数，用来得到精确的加法结果
     ** 说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。
     ** 调用：accAdd(arg1,arg2)
     ** 返回值：arg1加上arg2的精确结果
     **/
    function accAdd(arg1, arg2) {
        var r1, r2, m, c;
        try {
            r1 = arg1.toString().split(".")[1].length;
        }
        catch (e) {
            r1 = 0;
        }
        try {
            r2 = arg2.toString().split(".")[1].length;
        }
        catch (e) {
            r2 = 0;
        }
        c = Math.abs(r1 - r2);
        m = Math.pow(10, Math.max(r1, r2));
        if (c > 0) {
            var cm = Math.pow(10, c);
            if (r1 > r2) {
                arg1 = Number(arg1.toString().replace(".", ""));
                arg2 = Number(arg2.toString().replace(".", "")) * cm;
            } else {
                arg1 = Number(arg1.toString().replace(".", "")) * cm;
                arg2 = Number(arg2.toString().replace(".", ""));
            }
        } else {
            arg1 = Number(arg1.toString().replace(".", ""));
            arg2 = Number(arg2.toString().replace(".", ""));
        }
        return (arg1 + arg2) / m;
    }

    //给Number类型增加一个add方法，调用起来更加方便。
    Number.prototype.add = function (arg) {
        return accAdd(arg, this);
    };

    /**
     ** 减法函数，用来得到精确的减法结果
     ** 说明：javascript的减法结果会有误差，在两个浮点数相减的时候会比较明显。这个函数返回较为精确的减法结果。
     ** 调用：accSub(arg1,arg2)
     ** 返回值：arg1加上arg2的精确结果
     **/
    function accSub(arg1, arg2) {
        var r1, r2, m, n;
        try {
            r1 = arg1.toString().split(".")[1].length;
        }
        catch (e) {
            r1 = 0;
        }
        try {
            r2 = arg2.toString().split(".")[1].length;
        }
        catch (e) {
            r2 = 0;
        }
        m = Math.pow(10, Math.max(r1, r2)); //last modify by deeka //动态控制精度长度
        n = (r1 >= r2) ? r1 : r2;
        return ((arg1 * m - arg2 * m) / m).toFixed(n);
    }

    // 给Number类型增加一个mul方法，调用起来更加方便。
    Number.prototype.sub = function (arg) {
        return accMul(arg, this);
    };
 