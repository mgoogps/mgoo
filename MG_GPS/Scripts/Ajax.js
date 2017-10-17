jQuery(function ($) {
    // 备份jquery的ajax方法    
    var _ajax = $.ajax;
    // 重写ajax方法，先判断登录在执行success函数   
    $.ajax = function (opt) {
        var _success = opt && opt.success || function (a, b) { };
        var _error = opt && opt.error || function (a,b,c) { };
        var _opt = $.extend(opt, {
             success: function (data, textStatus) {
                 // 如果后台将请求重定向到了登录页，则data里面存放的就是登录页的源码，这里需要找到data是登录页的证据(标记)  
                 // if (data.indexOf('weinianjie') != -1) {
                 //   window.location.href = Globals.ctx + "/login.action";
                  //  return;
                 //  }
              //   console.log("_success.....");
                _success(data, textStatus);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status === 500)
                { 
                    if (XMLHttpRequest.responseText.indexOf("ToKen已失效") != -1) { 
                        mgoo.errormsg = '登录ToKen已失效,请重新登录！'; 
                        mgoo.islogin = false; 
                    }
                    if (XMLHttpRequest.responseText.indexOf("登录信息已失效") != -1) { 
                        mgoo.errormsg = '登录信息已失效,请重新登录！';
                        mgoo.islogin = false; 
                    }
                    console.log(mgoo.errormsg);
                    if (!mgoo.islogin) {
                        $("body").dialog({
                            id: 'mydialog', url: '/login_timeout.html', title: '登录', mask: "true", width: "430", height: "230", onLoad: "login_timeout_onload"
                        });
                        return;
                    }
                    _error(XMLHttpRequest,textStatus,errorThrown);
                }
            }
        }); 
        _opt.dataType = _opt.dataType ||  "json";
        _opt.contentType = _opt.contentType || "application/json; charset=utf-8";
        _opt.type = _opt.type || "POST"; 
        _ajax(_opt);
    };
});
function login_timeout() {
    var name = $("#mg_username").val();
    var password = $.md5($("#mg_userpwd").val());
    $("#mg_userpwd").val(password);
    $.ajax({
        url: "/AjaxService/LoginAjax.asmx/Login",
        data: "{loginname:'"+name+"', loginpassword:'"+password+"'}",
        success: function (r) {
            if (r.d == "success") {
                location.reload();
            } else {
                $("#mg_userpwd").val("").focus();
                $("#labelLoginError").text("登录失败！");
            }
        }
    });
}
function login_timeout_onload($dialog) { 
    $("#lableErrorMsg").text(mgoo.errormsg);
    $("#mg_username").val($("#txtSessionID").val());
    $("#mg_userpwd").focus();
}