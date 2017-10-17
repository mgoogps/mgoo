$(function  () {
    //$('#message-prompt').modal({
    //    relatedTarget: this,
    //    onConfirm: function (e) {
    //        var cked = $("input[name=ck-message-type]:checked");
    //        var cks = [];
    //        for (var i = 0; i < cked.length; i++) {
    //            cks.push($(cked[i]).val());
    //        }
    //        var info = JSON.parse(localStorage.getItem("LoginInfo")); 
    //        $.ajax({
    //            url: "MessageAjax.asmx",
    //            way: "SetPushMsg",
    //            pars: { userid: info.UserID, msgtype: cks.join(',') },
    //            success: function (data) {
    //                var modal = new amModal({ msg: data.Message });
    //                modal.open();
    //                if (data.StatusCode === 200) {
    //                    modal.onConfirm(function () {
    //                        $('#command-prompt').modal('close');
    //                    });
    //                }
    //            },
    //            error: function (err) {
    //                console.log(err);
    //            }
    //        });
    //    },
    //    onCancel: function (e) {

    //    }
    //});
   
   // $('#message-prompt').modal('close');
  
});

function GetMessageTypeList() { 
    $.ajax({
        url: "MessageAjax.asmx",
        way: "GetMessageTypeList",
        pars: {},
        success: function (res) {
            var msg = JSON.parse(res);
            $("#msglist").empty();
            for (var i = 0; i < msg.length; i++) {
                var check = "";
                var $switch;
                if (msg[i].IsPush == 0) {
                    $switch = $('<input type="checkbox" checked value="' + msg[i].ID + '" id="msgType' + msg[i].ID + '" class="am-badge am-margin-top-sm am-switch am-round am-switch-default am-active" /> ');
                } else {
                    $switch = $('<input type="checkbox" value="' + msg[i].ID + '" id="msgType' + msg[i].ID + '" class="am-badge am-margin-top-sm am-switch am-round am-switch-default" /> ');
                }  
                $("#msglist").append(' <li id="li' + msg[i].ID + '">' + msg[i].Message + ' </li>');
                $("#li" + msg[i].ID).append($switch);
                $switch.switch();
            }
            SelectMessageType();
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function GetMessageType() {
    $.ajax({
        url: "MessageAjax.asmx",
        way: "GetMessageType",
        pars: {  },
        success: function (data) {
            var msg = JSON.parse(data); 
            $("#msglist").empty();
            for (var i = 0; i < msg.length; i++) {
                $switch = $('<input type="checkbox" value="' + msg[i].ID + '" id="msgType' + msg[i].ID + '"  class="am-badge am-margin-top-sm am-switch am-round am-switch-default" /> ');
                
                $("#msglist").append(' <li id="li' + msg[i].ID + '">'+msg[i].Message+' </li>');
                $("#li" + msg[i].ID).append($switch);
                $switch.switch(); 
            }
            SelectMessageType();
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function SelectMessageType()
{ 
    var info = mg.GetUserInfo();
    $.ajax({
        url: "MessageAjax.asmx", 
        way: "GetPushMsgByUserID",
        pars: { userid: info .UserID},
        success: function (data) { 
            data = JSON.parse(data); 
            var t = data.MsgType.split(',');
            for (var i = 0; i < t.length; i++) { 
                $("#msgType" + t[i]).attr("checked", "checked").parent().parent().addClass('am-active');
            }

            $('#msglist .am-switch').on('click', function () {
                setTimeout(function () {
                    var checks = $("input:checked");
                    var cks = [];                          
                    for (var i = 0; i < checks.length; i++) { 
                        cks.push($(checks[i]).val());
                    }
                    var info = mg.GetUserInfo();
                    $.ajax({
                        url: "MessageAjax.asmx",
                        way: "SetPushMsg",
                        pars: { userid: info.UserID, msgtype: cks.join(',') },
                        success: function (data) {
                            if (data.StatusCode != 200) {
                                var modal = new amModal({ msg: data.Message });
                                modal.open();
                            } 
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                }, 300); 
            });
        },
        error: function (err) {
            console.log(err);
        }
    });
}
 

function UsersConfig()
{
    $.ajax({
        url: "MessageAjax.asmx",
        way: "GetUsersConfig",
        pars: {},
        success: function (res) {
            res = JSON.parse(res); 
            $("#radioPeriod" + res.Period).uCheck("check");
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function SetUserConfig()
{
    var data = {};
    data.period =  $("input[type=radio]:checked").val();
    $.ajax({
        url: "MessageAjax.asmx",
        way: "SetUsersConfig",
        pars: {config:JSON.stringify(data)},
        success: function (res) { 
            if (res.StatusCode != 200) {
                var modal = new amModal({ msg: res.Message });
                modal.open();
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}