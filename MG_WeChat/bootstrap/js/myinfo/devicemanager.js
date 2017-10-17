
var isPostback = true;
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
            $("ul[name=devicesList]").empty();
            for (var i = 0; i < deviceList.length; i++) {
                var device = deviceList[i];
                var html = [];
                var s = getStatus(device.Status, device.Speed);
                html.push('<li name="GroupDeviceList' + device.GroupID + '" > ');
                html.push('<a href="/device/info.html?deviceid=' + device.DeviceID + '" status=' + (device.Status == 2 || device.Status == 4 ? 2 : device.Status) + '><i class="am-icon-car am-margin-left-sm"></i>' + device.DeviceName + ' </a>');
               
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
            var g = $("ul[name=devicesList]");
            for (var i = 0; i < g.length; i++) {
                var none = $("li[name=" + $(g[i]).attr("id") + "][style='display: none;']");
                var show = $("li[name=" + $(g[i]).attr("id") + "]");
                $(g[i]).prev().find("span").text((show.length - none.length));
            }
        },
        error: function () {

        }
    });
}

function getStatus(status, speed) {
    switch (status) {
        case "1":
            if (speed == 0) {
                return "停止";
            }
            return speed + "km/h";
        case "2":
            return "离线";
        case "3":
            return "未激活";
        case "4":
            return "已过期";

    }
}




//////////// 添加设备 ，调用微信接口扫码
 
function scanning() {
    sessionStorage.setItem("serialnumber", $("#txtSerialNumber").val());
    sessionStorage.setItem("groupid", $("#group-selected").val());
    window.location.href = "http://m.mgoogps.com/scanCode.aspx";
}
 
function addfrmsumbit() {
    mg.loading(); 
    var groupid = sessionStorage.getItem("groupid");
    var imei = sessionStorage.getItem("serialnumber");
    if (groupid == null || groupid == "null") {
        groupid = $("#group-selected").val();
    } 
    if (imei == null || imei=="null") {
        imei = $("#txtSerialNumber").val(); 
    }
    console.log(imei);
    var vccode = $("#txtVCCode").val();
    var info = mg.GetUserInfo();
 
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "AddDevice",
        pars: { imei: imei, vccode: vccode , userid: info.UserID, groupid: groupid },
        success: function (data) { 
            var modal = new amModal({ msg: data.Message });
            modal.open();
            console.log(modal);
            if (data.StatusCode === 200) {
                modal.onConfirm(function () {
                    window.location.href = "devicemanager.html";
                });
            }
        },
        error: function (err) {
            console.log(err);
        },
        complete: function (a, b) { 
            mg.loading();
            sessionStorage.setItem("serialnumber", null);
            sessionStorage.setItem("groupid", null);
        }
    });

}


function existVCCode() {　
    addfrmsumbit();
    return;
    var serialnumber = $("#txtSerialNumber").val()
    if ($("#txtVCCode").val() != "") {　
        addfrmsumbit();
        return;
    }
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "VCCodeExist",
        pars: { serialnumber: serialnumber },
        success: function (data) { 
            if (data.StatusCode === 200) {
                scanning();
            } else if (data.StatusCode === 300){
                addfrmsumbit();
            } else {
                var m = new amModal({ msg: data.Message });
                m.open();
            }
        },
        error: function (err) {
            console.log(err);
        }
    });

}

function scanSuccess() {
    var b = true;
    if (res !=  null ) { 
        var sn = sessionStorage.getItem("serialnumber");
        var gid = sessionStorage.getItem("groupid");
        if (sn != null) {
            $("#txtSerialNumber").val(sn);
            sessionStorage.setItem("serialnumber",null);
        }
        if (gid != null) {
            GetGroupsSelects(function () {
                var $curr = $("#group-selected").find('option[value=' + d.GroupID + ']');
                $curr.attr("selected", !$curr.get(0).selected);
            });
            b = false;
            sessionStorage.setItem("groupid",null);
        } 
        $("#txtVCCode").val(res).parent().parent().removeClass("am-hide");

        if ($("#txtSerialNumber").val() == "") {
            return;
        }
        addfrmsumbit();
    }
    if (b) {
        GetGroupsSelects();
    }
}

function adddeviceLoading() {
    if ($("#adddeviceLoading").length < 1) {
        var html = [];
        html.push(' <div class="am-modal am-modal-loading am-modal-no-btn" tabindex="-2" id="adddeviceLoading">');
        html.push('  <div class="am-modal-dialog">');
        html.push('   <div class="am-modal-hd">正在添加...</div> ');
        html.push(' <div class="am-modal-bd">');
        html.push('    <span class="am-icon-spinner am-icon-spin"></span> ');
        html.push('   </div> </div> </div> ');
        $("body").append(html.join(''));
    }
    $("#adddeviceLoading").modal('toggle');
}