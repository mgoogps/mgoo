

function GetUserDeviceInfo()
{
     
    if (window.top.UserDeviceInfo) {
      
        // setDocValue(window.top.UserDeviceInfo);
        // return;
    } 
    var model = $("#selModelList").val();
    if(model){
        model = model.join(',');
    }
    window.top.UserDeviceInfo = window.top.UserDeviceInfo || {};
    
    var starttime  = $("#reservationtime").val().split("到")[0];
    var endtime = $("#reservationtime").val().split("到")[1];

    parent.loading(25);
    var postData = { "model": model, "st": starttime, "et": endtime };

    var key = "key_" + postData.model + postData.st + postData.et;

    if (window.top.UserDeviceInfo[key]) {
        setDocValue(window.top.UserDeviceInfo[key]);
        return;
    }
    $.ajax({
        url: "../AjaxService/AjaxService.ashx?action=GetUserDeviceInfo",
        type: "POST",
        dataType: "json",
        data: postData,
        error: function (err) {   
                //alert("出错1");
        },
        success: function (res) {
            window.top.UserDeviceInfo[key] = res;
            setDocValue(res);
            
        }
    });
    function setDocValue(res) {
        parent.loading(60);
        $("label[for=sp-chk1]").next().text(res.count + "台");
        $("label[for=sp-chk2]").next().text(res.deviceOnline + "台");
        $("label[for=sp-chk3]").next().text(res.Online7Day + "台");
        $("label[for=sp-chk4]").next().text(res.deviceOffline + "台");
        $("label[for=sp-chk5]").next().text(res.useDeviceCount + "台");
        $("label[for=sp-chk6]").next().text(res.Arrears + "台");
        $("label[for=sp-chk7]").next().text(res.NoEnable + "台");
        parent.loading(100);
    }
}


function GetDeviceHireExpire() {
    parent.loading(40);
    $.ajax({
        url: "../AjaxService/AjaxService.ashx?action=GetDeviceHireExpire",
        type: "POST",
        dataType: "json",
        data: { "uid": $("#userid").val(),"day":$("#day").val() },
        error: function (err) {
           
        },
        success: function (res) {
            $("#DHETable tbody").empty();
            var html = [];
            parent.loading(60);
            $.each(res[""], function (k, v) {
                if (v.DeviceName.length == 0)
                    v.DeviceName = v.SerialNumber;
                html.push(" <tr name=\"" + v.SerialNumber + "\">");
                html.push(" <td style=\"width:50px;\">" + v.num + "</td>");
                html.push(" <td>" + v.DeviceName + "</td>");
                html.push("	<td>" + v.CarNum + "</td>");
                html.push(" <td>" + v.SerialNumber + "</td>");
                html.push(" <td>" + v.UserName + "</td>");
                html.push(" <td class=\"center\">" + v.ActiveDate + "</td>");
                html.push(" <td class=\"center\">" + v.HireExpireDate + "</td>");
                html.push(" <td class=\"center\"> <a href=\"javascript:void(0)\" onclick=\"window.parent.UpdateDevice("+v.DeviceID+")\" class=\"btn btn-blue btn-sm btn-icon icon-left\">修改</a> ");
                html.push(" <a href=\"javascript:void(0)\" onclick=\" window.parent.DeviceShiftOrExpire('expire',[{DeviceID:'" + v.DeviceID + "',UserName:'" + v.UserName + "',IMEI:'" + v.SerialNumber + "',DeviceName:'" + v.DeviceName + "'}])\" class=\"btn btn-info btn-sm btn-icon icon-left\">到期时间</a>  </td>");
                html.push("	</tr>");
            }); 
            $("#DHETable tbody").append(html.join(''));
            parent.loading(100);
        }
    });
}

function GetDeviceList(type)
{
    $.ajax({
        url: "../AjaxService/AjaxService.ashx?action=GetDeviceHireExpire",
        type: "POST",
        dataType: "json",
        data: { "uid": $("#userid").val(), "day": $("#day").val(),"type":type },
        error: function (err) {
           
        },
        success: function (res) {

        }
    });

}

