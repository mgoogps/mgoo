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
   
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "UpdateDeviceInfoByID",
        pars: { deviceid: deviceid, devicename: name, carusername: username, cellphone: phone, carnum: carnum, groupid: group, description:'' },
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