$(function () {
  
    GetDeviceInfo();
});

function GetDeviceInfo() {
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetDeviceInfoByID",
        pars: { deviceid: deviceid },
        success: function (data) {

            var d = JSON.parse(data); 
            $("#txtSerialNumber").val(d.SerialNumber);
            $("#txtDeviceName").val(d.DeviceName);
            $("#txtDeviceModel").val(d.DataText);
            $("#txtActiveDate").val(d.ActiveDate == '1900-1-1 0:00:00' ? "未激活" : d.ActiveDate);
            $("#txtHireExpireDate").val(d.HireExpireDate == '1900-1-1 0:00:00' ? "未激活" : d.HireExpireDate);
            $("#txtCarNum").val(d.CarNum);
            $("#txtUserName").val(d.CarUserName);
            $("#txtCallPhone").val(d.CellPhone);
            $("#txtPhoneNum").val(d.PhoneNum);
          
            if (d.DataText == "MG-X11BDY") {
                $(".X11BDY").show();
                $(".X11BDY button").on("click", function () {
                    $(this).parent().find("button").attr("class", "am-btn am-btn-default am-active");
                    $(this).attr("class", "am-btn am-btn-primary am-active");
                });
                $("#divSens button[data-value=" + d.Sens + "]").trigger("click");
                $("#divLaba button[data-value=" + d.Horn + "]").trigger("click");
                $("#divShefang button[data-value=" + d.AutoDefense + "]").trigger("click");
            }
            GetGroupsSelects(function () {
                var $curr = $("#group-selected").find('option[value=' + d.GroupID + ']');
                $curr.attr("selected", !$curr.get(0).selected);
            }); 
        },
        error: function (err) {
            console.log(err);
        }
    });

}

function frmsubmit() {
    var name = $("#txtDeviceName").val();
    var carnum = $("#txtCarNum").val();
    var username = $("#txtUserName").val();
    var phone = $("#txtCallPhone").val();
    var group = $('#group-selected').val();
    var sens = $("#divSens button.am-btn-primary").attr("data-value");
    var horn = $("#divLaba button.am-btn-primary").attr("data-value");
    var AutoDefense = $("#divShefang button.am-btn-primary").attr("data-value");
    
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "UpdateDeviceInfoByID",
        pars: {
            deviceid: deviceid,
            devicename: name,
            carusername: username,
            cellphone: phone,
            carnum: carnum,
            groupid: group,
            description: '',
            sens: sens,
            horn: horn,
            autodefense: AutoDefense
        },
        success: function (data) {
            var modal = new amModal({ msg: data.Message });
            modal.open();
            modal.onConfirm(function () {
                if (data.StatusCode === 200) {
                    history.back();
                }
            });
        },
        error: function (err) {
            console.log(err);
        }
    });
}


function delsubmit() {
    var name = $("#txtDeviceName").val();
    var modal = new amModal({ msg: "确定删除该设备:" + name, cancel: true });
    modal.open();
    modal.onConfirm(function () {
        $.ajax({
            url: "DevicesAjax.asmx",
            way: "DeleteDevice",
            pars: { deviceid: deviceid },
            success: function (data) {
        
                // modal.close(); 
                var m = new amModal({ msg: data.Message, title: "提示" });
                m.open();
                m.onConfirm(function () {
                    if (data.StatusCode === 200) {
                        history.back();
                    } else {
                        m.close();
                    }
                });
            },
            error: function (err) {
                console.log(err);
            }
        });
    });
}