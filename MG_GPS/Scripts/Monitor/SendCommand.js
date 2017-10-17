$(function () {
    $("button[name=btnSendCommand]").on("click", function () {

       
    });
});

function SendCommand() {
    var arg = arguments;
    var phone = $("#txtBindPhone").val();
    var mode = $("#selectMode").val();
    var sens = $("#selectSens").val();
   
    if (arg[1] =="1" && !/^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\d{8}$/i.test(phone)) {
        $(arg[0]).alertmsg('warn', '手机号码格式错误！')
        return;
    }
   
     
    $.ajax({
        url: "/AjaxService/DeviceManager.asmx/SendCommand",
        data: "{command:'" + arg[1] + "',userid:'" + mgoo.CurrentzTreeUserID + "',id:'" + mgoo.DeviceID + "',phone:'" + phone + "',mode:'" + mode + "',sens:'" + sens + "'}",
        success: function (r) { 
            if (r.d=="success") {
                $(arg[0]).alertmsg('correct', '发送成功！')
            } else {
                $(arg[0]).alertmsg('warn', '发送失败！')
            }
        } 
    });
}