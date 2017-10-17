
 

(function (win, owner) {
    owner.ToLoginPage = function () {
        win.location.href = "/Login.aspx";
    }
    owner.StorageKey = "LoginInfo";

    owner.GetUserInfo = function () {
        var storage = win.localStorage.getItem(owner.StorageKey);
        if (!storage) {
            owner.ToLoginPage();
        }
        return JSON.parse(storage) || {};
    } 
  
    owner.identifies = "MgooWeixin@AMAP";

    owner.LoadingText = "加载中...";

    owner.loadtime = false;

    owner.ajaxAsync = true; //异步

    owner.Login = function (LoginInfo, SuccessCallBack, ErrorCallBack) {
        owner.loading("正在登录...");
        setTimeout(function () {
            owner.loadtime = true;
        },500);
        
        LoginInfo = LoginInfo || {};
        LoginInfo.identifies = LoginInfo.identifies || owner.identifies;
  
        SuccessCallBack = SuccessCallBack || function () { };
        ErrorCallBack = ErrorCallBack || function () { };
       
        $.ajax({
            url: "/ajax/Login.asmx/MgLogin",
            data: JSON.stringify(LoginInfo),
            type: "post",
            async: owner.ajaxAsync,
            dataType: "json",
            contentType: "application/json", 
            success: function (r) {
                var reulst = JSON.parse(r.d); 
                reulst.password = LoginInfo.password;
                reulst.loginname = LoginInfo.loginname;
                reulst.code = LoginInfo.code;
                if (reulst.StatusCode == 200) {
                    console.log("login success...");
                    owner.SetUserInfo(reulst);
                } else {
                    console.log("login failure...");
                }
                if (reulst.Message) {
                    var modal = new amModal({ msg: reulst.Message });
                    modal.open();
                    modal.onConfirm(function () {
                        SuccessCallBack(reulst);
                    }); 
                }else{
                    SuccessCallBack(reulst);
                }
            },
            error: function (a, b, c) { 
                ErrorCallBack(a,b,c);
            },
            complete: function () {
                if (owner.loadtime) {
                    owner.loading();
                } else {
                    setTimeout(function () {
                        owner.loadtime = false;
                        owner.loading();
                    },300);
                }
            }
        }); 
    }
     
    owner.SetUserInfo = function (val) {
        val = val || {};
        win.localStorage.setItem(owner.StorageKey, JSON.stringify(val));
    }
     
    owner.AppendLoading = function ( )
    {
        if ($("#modalLoading").length < 1) {
            var html = []; 
            html.push(' <div class="am-modal am-modal-loading am-modal-no-btn" tabindex="-1" id="modalLoading">');
            html.push('   <div class="am-modal-dialog">');
            html.push('     <div class="am-modal-hd">  </div> ');
            html.push('     <div class="am-modal-bd">');
            html.push('       <span class="am-icon-spinner am-icon-spin"></span> ');
            html.push('  </div> </div> </div> ');
            $("body").prepend(html.join('')); 
        } else {
            //;
        } 
    }

    owner.loading = function (txt) { 
        owner.LoadingText = txt || owner.LoadingText;
        $("#modalLoading div.am-modal-hd").text(txt || owner.LoadingText) 
         $("#modalLoading").modal('toggle');  
    }

    owner.AppendLoading();
}(window, window.mg = {}))


function login(callback) {
   
    var l = mg.GetUserInfo(); 
    callback = callback || function () { };
    var data = { loginname: l.loginname, password: l.password, identifies: mg.identifies, code: "" };
  
    mg.Login(data, _success, _error);
    
    function _success(reulst) { 
        if (reulst.StatusCode === 200) {
            //reulst.date = getNowFormatDate();
            reulst.password = l.password;
            reulst.loginname = l.loginname; 
            reulst.code = l.code;

            //localStorage.setItem("LoginInfo", JSON.stringify(reulst)); 
            //sessionStorage.setItem("isAjax", true); 
            callback();
            // history.go(0); //刷新
        } else {
            if (reulst.Message) {
                new amModal({ msg: reulst.Message }).open();
            }
            var m = new amModal({ msg: "账号或密码错误,请重新登录." });
            m.open();
            m.onConfirm(function () {
                mg.ToLoginPage();
            }); 
        }
    }

    function _error(a,b,c)
    { 
        console.log("login error..."); 
        var modal = new amModal({ msg: "登录出错." });
        modal.open();
        modal.onConfirm(function () {
            mg.ToLoginPage();
        });
    }
    //$.ajax({
    //    url: "/ajax/Login.asmx/MgLogin",
    //    data: JSON.stringify(data),
    //    type: "post",
    //    async: false,
    //    dataType: "json",
    //    contentType: "application/json",
    //    success: function (r) {
    //        var reulst = JSON.parse(r.d);
    //        loading("正在获取令牌...");
    //        if (reulst.StatusCode === 200) {
    //            reulst.date = getNowFormatDate();
    //            reulst.password = l.password;
    //            reulst.loginname = l.loginname; 
    //            reulst.code = l.code;
    //            localStorage.setItem("LoginInfo", JSON.stringify(reulst)); 
    //            sessionStorage.setItem("isAjax", true);
    //            console.log("login success...");
    //             history.go(0); //刷新
    //        } else {
    //            if (reulst.Message) {
    //                new amModal({ msg: reulst.Message }).open();
    //            }
    //            var m = new amModal({ msg: "账号或密码错误,请重新登录." });
    //            m.open();
    //            m.onConfirm(function () {
    //                window.location.href = "/login.aspx";
    //            });
    //            window.location.href = "/login.aspx";
    //        }
    //    },
    //    error: function (e) {
    //        console.log(e);
    //    }
    //});
}
jQuery(function ($) { 
    // 备份jquery的ajax方法    
    var _ajax = $.ajax; 
    // 重写ajax方法，先判断登录在执行success函数   
    $.ajax = function (opt) { 
        if (opt.way) { 
            var _success = opt && opt.success || function (a, b) { };
            var _error = opt && opt.error || function (a, b, c) { };
            var _beforeSend = opt && opt.beforeSend || function (a) { };
            var _complete = opt && opt.complete || function (a, b) { }; 
            var _opt = $.extend(opt, {
                success: function (data, textStatus) {
                    var result = $(data).find(_opt.way + "Result").text();
                    if (result == "") return;
                    var json = JSON.parse(result);
                    if (json.StatusCode) {
                        if (json.StatusCode === 200) {

                        } else if (json.StatusCode === 501 || json.StatusCode === 502) {
                            console.log("登录信息失效...");
                            //$("#modalLoading").modal('open');
                            //mg.loading();
                            login(function () {
                                opt.url = opt.url.replace("/ajax/", "").replace("?op=" + opt.way, "");
                                opt.data = soapXml(opt.way, opt.pars);
                                opt.success = _success;
                                opt.error = _error;
                                opt.complete = _complete;
                                $.ajax(opt);
                            });
                           // mg.loading();
                            return;
                        } else {
                            //var modal = new amModal({ msg: json.Message, title: "警告" });
                            //modal.open();
                            //modal.onConfirm(function  () {
                            //    history.go(0)
                            //});
                        }
                        _success(json, textStatus);
                    } else {
                        _success(result, textStatus);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var modal = new amModal({ msg: "网络错误,点击确定刷新页面.", title: "警告" });
                    modal.open();
                    modal.onConfirm(function () {
                        history.go(0)
                    });
                    _error(XMLHttpRequest, textStatus, errorThrown);
                },
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('SOAPAction', 'http://tempuri.org/' + opt.way);
                },
                complete: function (xmlHttpRequest, status) { 
                    //var xml = xmlHttpRequest.responseXML;
                    // console.log(JSON.parse($(xml).find('HelloWorldResult').text()));  
                    _complete(xmlHttpRequest, status);
                }
            });
            _opt.dataType = _opt.dataType || "xml";
            _opt.contentType = _opt.contentType || "text/xml; charset=utf-8";
            _opt.type = _opt.type || "POST";
            _opt.url = "/ajax/" + _opt.url + "?op=" + _opt.way;
            _opt.data = _opt.data || soapXml(_opt.way, _opt.pars);
            _opt.timeout = _opt.timeout || 6000; //设置请求超时时间为6秒
            _ajax(_opt); 
        } else { 
            _ajax(opt);
        }
    };
   
});


//function alert() {
    
//}

//console.log = (function (oriLogFunc) {
//    if (console) {
//      return  function (str) {
//            oriLogFunc.call(console, str);
//        }
//    }
    
//})(console.log);
function soapXml(name,pars)
{
   // var info = mg.GetUserInfo();
    var info = localStorage.getItem("LoginInfo");
    info = JSON.parse(info);
   //var curdate = getNowFormatDate(); 
   //var mi = comptime(info.date, curdate); 
   //if (mi > 10080) { 
   //    //大于一个星期 
    //}  
    var parstr="";
    $.each(pars, function (k,v) {
        parstr += "<"+k+">"+v+"</"+k+">";
    }); 
    var soapMessage2 =
          "<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>" +
          "  <soap:Header>" +
              "<AuthHeader xmlns='http://tempuri.org/'>" +
                  "<UserID>" + info.UserID + "</UserID>" +
                  "<Token>" + info.Token + "</Token>" +
                  "<Identifies>" + mg.identifies + "</Identifies>" +
              "</AuthHeader>" +
          "</soap:Header>" +
          "<soap:Body>" + 
                "<" + name + " xmlns='http://tempuri.org/'>" +
                parstr +
                "</" + name + ">" +
          "</soap:Body>" +
          "</soap:Envelope>";
    return soapMessage2; 
}

