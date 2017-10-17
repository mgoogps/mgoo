<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MG_WeChat.Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <title>登录</title>

    <!-- Bootstrap -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="images/main.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="http://cdn.amazeui.org/amazeui/2.7.2/css/amazeui.min.css">     
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="//cdn.bootcss.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="//cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
  
</head>
<body> 
    <div class="container">
        <img src="images/logo.png" class="center-block img-responsive" style="width:250px;height:250px;"/>
    </div>
    
    <div class="container">
        <form class="form-signin" >
            <h3 class="form-signin-heading">欢迎您！请先登录</h3> 
               <input type="hidden" value="<%= wxCode %>" id="txtWxCode" />
            <div class="input-group input-group-lg">
                <span class="input-group-addon"  ><i class="fa fa-user fa-fw"></i></span>
                <input type="text" id="inputAccount" class="form-control" placeholder="请输入帐号" value="" required autofocus>
            </div>
            <div class="input-group input-group-lg">
                <span class="input-group-addon" ><i class="fa fa-lock fa-fw"></i></span>
                <input type="password" id="password" class="form-control" placeholder="请输入登录密码" required value="">
            </div>
            <div class="checkbox">
                <div class="row">
                   <%-- <div class="col-xs-4"> 
                        <label>
                            <!--<input type="checkbox" value="remember-me"> 记住密码-->
                        </label>
                    </div>--%>
                    <div class="col-xs-6">
                        <label>
                            <a href="./Register.html">注册帐号</a>                            
                        </label>
                    </div>
                    <div class="col-xs-6">
                        <label>
                            <a href="RetrievePassword.html">忘记密码?</a>
                        </label>
                    </div>
                </div>                
            </div>            
            <button class="btn btn-primary btn-block btn-lg" type="button" id="btnLogin" onclick="mgoo_login()">登录</button> <%-- data-am-modal="{target: '#modalLoading'}"--%>
        </form>
    </div>
   <%-- <div class="am-modal am-modal-loading am-modal-no-btn" tabindex="-1" id="modalLoading">
        <div class="am-modal-dialog">
            <div class="am-modal-hd">正在登录...</div>
            <div class="am-modal-bd">
                <span class="am-icon-spinner am-icon-spin"></span>
            </div>
        </div>
    </div>--%>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) --> 
    <script src="bootstrap/js/jquery-2.1.3.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->    
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="bootstrap/js/utils.js"></script>
    <script src="bootstrap/js/jQuery.md5.js"></script>
    <script src="bootstrap/js/amazeui-2.7.2.min.js"></script>
    <script src="bootstrap/js/public.js"></script>
    <script src="bootstrap/js/Ajax.js"></script>
    <script type="text/javascript">
        var ismd5 = true;
        function mgoo_login() {
            if (valid()) {
                var accout = $("#inputAccount").val()
                var password;
                if (ismd5) {
                    password = $.md5($("#password").val());
                } else {
                    password = $("#password").val()
                }
                var data = { loginname: accout, password: password , code: $("#txtWxCode").val() };
                //$("#modalLoading").modal('toggle');
                mg.Login(data, successCallback, errorCallback); 
            } 
        }
        function valid() {
            if ($("#inputAccount").val() == '' || $("#password").val() == '') {
                new amModal({ msg: "请输入账号和密码." }).open();
               // $("#modalLoading").modal('toggle');//.modal("close");
                return false;
            }
            return true;
        };
        function successCallback(reulst) {
            // $("#modalLoading").modal('toggle'); //.modal("close");
            if (reulst.StatusCode === 200) { 
                //reulst.date = getNowFormatDate();
               // reulst.password = password;
               // reulst.loginname = accout;
                //reulst.code = $("#txtWxCode").val();
            
               // localStorage.setItem("LoginInfo", JSON.stringify(reulst));
                // sessionStorage.setItem("isAjax", true);
               
                if ('<%= backUrl%>' == "") {
                    //window.location.href = reulst.Url;
                    window.location.replace(reulst.Url);
                } else {
                    history.back();
                }
            } else {
                //$("#modalLoading").modal('toggle');
                new amModal({ msg: "账号或密码错误." }).open();
                ismd5 = true;
                $("#btnLogin").text("登录"); 
            }
        }
        function errorCallback(a, b, c) {
           // $("#modalLoading").modal('toggle'); 
            new amModal({ msg: "网络错误,请稍后再试." }).open();
        }
        window.onload = function () {
            
            var logininfo = localStorage.getItem("LoginInfo");
            logininfo = JSON.parse(logininfo);
            if (logininfo && logininfo.loginname && logininfo.password) {
                $("#btnLogin").text("正在登录...");
                $("#inputAccount").val(logininfo.loginname)
                $("#password").val(logininfo.password)
                 ismd5 = false;
                $("#btnLogin").trigger("click");
            }
        }
    </script>

</body>
</html>
