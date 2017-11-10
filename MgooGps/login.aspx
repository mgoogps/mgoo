<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs"   EnableSessionState="True" Inherits="MgooGps.login" %>

<!DOCTYPE/>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />   
    <meta name="viewport" content="width=device-width, initial-scale=1.0">  
    <meta name="Keywords" content="GPS定位系统,GPS全球定位系统,GPS定位平台,GPS车辆管理系统,GPS防盗器,车载GPS定位系统,汽车定位器,汽车导航仪" />
	<meta name="description" content="GPS定位系统,GPS车辆监控系统,GPS定位平台,汽车GPS定位导航系统,GPS车辆管理系统是车辆管理的好帮手,并广泛用于,客运,货运,租车,物流,多个行业." />
	<meta name="author" content="" />
	 
	<title>GPS物联在线 - 登录</title>
    
    <script type="text/javascript" src="js/Language/language02-<%=MgooGps.com.Utils.language %>.js"></script>
    <script type="text/javascript" src="js/Language/language01-<%=MgooGps.com.Utils.language %>.js"></script> 
	<link rel="stylesheet" href="assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="assets/css/fonts/fontawesome/css/font-awesome.min.css"/>
	<link rel="stylesheet" href="assets/css/bootstrap.css"/>
	<link rel="stylesheet" href="assets/css/xenon-core.css"/>
	<link rel="stylesheet" href="assets/css/xenon-forms.css"/>
	<link rel="stylesheet" href="assets/css/xenon-components.css"/>
	<link rel="stylesheet" href="assets/css/xenon-skins.css"/>
	<link rel="stylesheet" href="assets/css/custom.css"/>

	<script src="assets/js/jquery-1.11.1.min.js"></script>

	<link rel="stylesheet" href="assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="assets/js/select2/select2-bootstrap.css"/> 
    <script src="assets/js/select2/select2.min.js"></script>
    <script src="assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>
    <script src="js/cookie.js"></script>
    <script src="js/jQuery.md5.js"></script>
 
	<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
	<!--[if lt IE 9]>
		<script src="assets/js/html5shiv.min.js"></script>
		<script src="assets/js/respond.min.js"></script>
	<![endif]-->
    <script src="js/runtime.js"></script>
    <style type="text/css">
        body, html  {
           min-width:960px;
           overflow:hidden
        }
        .main-content {
        
    min-width: 960px;

        }
       .login-page .login-form .login-header {
            margin-bottom:20px;
        }
        .login-page .login-form .login-header p {
          margin-top:0px;
        }
        .row {
          margin-top:-100px;
        }
        .login-page .login-form.fade-in-effect.in {
            width:410px;
          
        }
        .login-page .errors-container {
            max-width:410px;
        }
        .login-page .login-form .login-footer {
            margin-top:39px;
        }
        .form-control {
            border:1px solid #FF8000;
        }
        /*.login-page .login-form.fade-in-effect.in + .external-login {
           -webkit-opacity:0.8;
           -moz-opacity:0.8;
           opacity:0.8;
        }*/
        
    </style>
	<script> 
	    var _hmt = _hmt || [];
	    (function () {
	        var hm = document.createElement("script");
	        hm.src = "//hm.baidu.com/hm.js?63805c8d49dd83eac3a21c31f34f9352";
	        var s = document.getElementsByTagName("script")[0];
	        s.parentNode.insertBefore(hm, s);
	    })();
    </script>
  
    <script type="text/javascript" src="js/config/<%= MG_BLL.Common.lib.Config.CurrentHost %>.js"></script>
</head>
<body class="page-body login-page login-light"> 
 <div style="position:absolute;top:0px;left:0px;bottom:0px;right:0px;z-index:10000;" id="allmap"></div>
     
	<div class="main-content">
	
		<div class="row">
		    <div class="col-sm-8"></div>

			<div class="col-sm-4">
			
				<script type="text/javascript">
				  
				    jQuery(document).ready(function ($) {
				        if (window != window.top) { 
				            window.parent.location.href = "login.aspx";
				        }
				        if (window.screen) {
				            var myw = screen.availWidth;
				            var myh = screen.availHeight;
				            window.moveTo(0, 0);
				            window.resizeTo(myw, myh);
				        }
				        var loginCode = getCookieByCookieName("username");
				        var pwd =  getCookieByCookieName("passwd"); 
				        if (pwd) { 
				            $("[name='jzpwd']").attr("checked", "true");
				        }
				        if (loginCode) { 
				            $("#username").val(loginCode);
				        }
				        if (pwd) {  
				            $("#passwd").val(pwd);
				        } 
				        // Reveal Login form
				        setTimeout(function () { $(".fade-in-effect").addClass('in'); }, 1);
                          
				        // Validation and Ajax action
				        $("form#login").validate({
				            rules: {
				                username: {
				                    required: true
				                },

				                passwd: {
				                    required: true
				                }
				            },

				            messages: {
				                username: {
				                    required: loginPage.loginMsg
				                },

				                passwd: {
				                    required: loginPage.loginMsg2
				                }
				            },
				         
				            // Form Processing via AJAX
				            submitHandler: function (form) {
				                show_loading_bar(70); // Fill progress bar to 70% (just a given value)
				                
				                var opts = {
				                    "closeButton": true,
				                    "debug": false,
				                    "positionClass": "toast-top-full-width",
				                    "onclick": null,
				                    "showDuration": "300",
				                    "hideDuration": "1000",
				                    "timeOut": "5000",
				                    "extendedTimeOut": "1000",
				                    "showEasing": "swing",
				                    "hideEasing": "linear",
				                    "showMethod": "fadeIn",
				                    "hideMethod": "fadeOut"
				                };
				                var pwd = $.md5($(form).find('#passwd').val());
				                
				                var loginData = {
				                    do_login: true,
				                    loginname: $(form).find('#username').val(),
				                    pwd: pwd,
				                    language: $("#s2Language").val()
				                };

				                $.ajax({
				                    url: "AjaxService/AjaxService.ashx?action=login",
				                    method: 'POST',
				                    dataType: 'text',
				                    data: loginData,
				                    error: function (error) {
				                        resp = loginPage.loginFailure; //"登录失败";
				                        $(".errors-container").html('<div class="alert alert-danger" style="height:100%;background-color:blue;">\
												<button type="button" class="close" data-dismiss="alert">\
													<span aria-hidden="true">&times;</span>\
													<span class="sr-only">Close</span>\
												</button>\
												' + resp + '\</div>'); 
				                        $(".errors-container .alert").hide().slideDown();
				                        $(form).find('#passwd').select();
				                        show_loading_bar(100);
				                    },
				                    success: function (resp) {
				                        
				                        if (resp != "no") {
				                            setCookie();
				                            window.location.href = resp; 
				                        }
				                        else { 
				                            resp = loginPage.loginErrorMsg //"账号或密码错误";
				                            $(".errors-container").html('<div class="alert alert-danger">\
												<button type="button" class="close" data-dismiss="alert">\
													<span aria-hidden="true">&times;</span>\
													<span class="sr-only">Close</span>\
												</button>\
												' + resp + '\</div>') .parent().css("z-index", "100003");
				                          
				                            $(".errors-container .alert").hide().slideDown();
				                            $(form).find('#passwd').select();
				                            show_loading_bar(100);
				                        }
				                      //  show_loading_bar({
				                        //    delay: .5,
				                       //     pct: 100,
				                       //     finish: function () {
				                                
				                                // Redirect after successful login page (when progress bar reaches 100%)
				                        //        if (resp.accessGranted) {
				                         //           window.location.href = 'index.aspx';
				                        //        }
				                        //    }
				                       // });


				                        // Remove any alert
				                       // $(".errors-container .alert").slideUp('fast').slideUp(5000);


				                        // Show errors
				                       // if (resp.accessGranted == false) {
				                         //   $(".errors-container").html('<div class="alert alert-danger">\
										//		<button type="button" class="close" data-dismiss="alert">\
											//		<span aria-hidden="true">&times;</span>\
										//			<span class="sr-only">Close</span>\
										//		</button>\
										//		' + resp.errors + '\
										//	</div>');


				                          //  $(".errors-container .alert").hide().slideDown();
				                          //  $(form).find('#passwd').select();
				                      //  }
				                    }
				                });

				            }
				        });

				        // Set Form focus
				        $("form#login .form-group:has(.form-control):first .form-control").focus();
				        
				    });
                   
				
				</script>
				
				<!-- Errors container -->
				<div class="errors-container">
				
									
				</div>
				
				<!-- Add class "fade-in-effect" for login form effect -->
				<form method="post" role="form" id="login" class="login-form fade-in-effect" style="z-index:10000;border:1px solid #FF8000;" >
					
					<div class="login-header">
                       <%-- <%= MG_BLL.Common.lib.Config.CurrentHostWelcomeMessage %>--%>
                        <p id="pWelcomeMessage" style="font-size:28px;color:black"></p>
                        <script>document.getElementById("pWelcomeMessage").innerHTML = WelcomeMessage;</script>			
						<p>
                             <script type="text/javascript">
                                 writePage(loginPage.welcome)
                            </script>  
						</p>
					</div>
					
					<div class="form-group">
                        <div style="margin-bottom:-40px;border:1px solid #FF8000;height: 40px;background-color:#F5F5F5"> <label style="margin-top:8px;margin-left:9px; font-size:initial;">账号</label> </div> 
						<label class="control-label" for="username" style="margin-left:15%">
                            <script type="text/javascript">
						        writePage(loginPage.accountTab)
                            </script> 
						</label>
                  
						<input type="text" class="form-control" name="username" id="username" autocomplete="off" style="width:85%;margin-left:15%" />
					</div>
					
					<div class="form-group">
                        <div style="margin-bottom:-40px;border:1px solid #FF8000;height: 40px;background-color:#F5F5F5"> <label style="margin-top:8px;margin-left:9px; font-size:initial;">密码</label> </div> 
						<label class="control-label" for="passwd" style="margin-left:15%">
                            <script type="text/javascript">
						      writePage(loginPage.password)
                            </script>
						</label>
						<input type="password" class="form-control" name="passwd" id="passwd" autocomplete="off" style="width:85%;margin-left:15%" />
					</div>
					
					<div class="form-group">
						<button type="submit" class="btn btn-primary  btn-block text-left" style="background-color:#FF8000" >
							<i class="fa-lock"></i>  <script type="text/javascript">
							                             writePage(loginPage.loginSubmit)
                                          </script> 
						</button>
                      <%--  <button type="button" class="btn btn-primary btn-block text-left" style="background-color:#FF8000">
							<i class="fa-key"></i> <script type="text/javascript">
							                           writePage(loginPage.Register)
                                          </script> 
						</button>  --%>           
					</div>
                    <div class="form-group"  >
                        
				               <label>
                                   <input type="checkbox" name="jzpwd" class="cbr"/>
										  <script type="text/javascript">
										      writePage(loginPage.RememberPassword)
                                          </script>  
							  </label> 
                     
                              <label style="margin-left:100px;cursor:pointer;"> 
										  <script type="text/javascript">
										      writePage(loginPage.ForgotPassword)
                                          </script>  
							  </label> 
                        	 
                    </div>
                   
                 
					<div class="form-group" style="text-align:right;">
					        <label style="margin-left:17px;">Language:</label> 
							<script type="text/javascript">
								jQuery(document).ready(function ($) { 
									$("#s2Language").select2({
									    placeholder: '选择语言',
									    allowClear: false,
									    minimumResultsForSearch: -1, // Hide the search bar
									    formatResult: function (state) {
									              
									        return '<div style="background:url(/icons/Language/' + state.id + '.jpg) no-repeat center center;background-size:100%;display:inline-block;position:relative;width:20px;height:15px;margin-right: 10px;top:2px;z-index:100011"></div>'
													+ state.text;
									    }
									}).on('select2-open', function () {
									    $(".select2-drop.select2-drop-active").css("z-index","110000");
									    // Adding Custom Scrollbar
									    //  $(this).data('select2').results.addClass('overflow-hidden').perfectScrollbar();
									}).on("change", function () {
									    setCookieValue("language", $(this).val());
									    window.location.href = "login.aspx?language=" + $(this).val();
									});
									      
									var language = getCookieByCookieName("language"); 
									$("#s2Language").select2("val", language);
									if (language == undefined || language == "") {
									    $("#s2Language").select2("val", "zh-cn");
									}
								});
							</script> 

                            <select class="form-control" id="s2Language" style="width:165px; ">
								<option></option>
                                <option value="zh-cn" selected="selected">中文简体</option>
                                <option value="en-us">English</option> 
								<option value="pl">Polska</option>
								<option value="vi">Việt Nam</option>
								<option value="tr">Turkey</option>
								<option value="pt">Portuguese</option>
								<option value="hk">中文繁體</option>
								<option value="ru">русский</option>
								<option value="de">Deutsch</option>
								<option value="fr">Français</option>
								<option value="es">Spanish</option>
								<option value="ar">مملكة عربية سعودية</option>
								<option value="it-IT">Netherlands</option>  
					  	 </select>
						<%--<div class="info-links">
							<a href="#">ToS</a> -
							<a href="#">Privacy Policy</a>
						</div>--%>
						
					</div> 
                    <hr />
                    <div class="form-group" style="height:100px;" > 
                     <div style="border:0px solid red;width:60%;float:left;text-align:center;">
                              <img src="assets/images/<%= MG_BLL.Common.lib.Config.CurrentHost %>_QR.png" alt="扫描二维码下载APP" style="width:110px;height:110px; " />
                            <br /> 扫码下载APP
                         </div>
                          <div style="border:0px solid blue;width:40%;float:left">
                                <br />
								<button class="btn btn-blue btn-icon" onclick="" id="btnDownloadApp" type="button" style="margin-top:5px;background-color:#FF8000" >
									<i class="fa-download"></i>
									<span>客户端下载</span>
								</button>
                               <%-- <br />
                                <button class="btn btn-blue btn-icon" type="button" style="background-color:#FF8000">
									<i class="fa-download"></i>
									<span>苹果下载</span>
								</button>--%>
                         </div> 
                    </div> 
				</form>
				
				<!-- External login -->
              <%--  <div class="external-login" style="z-index: 10000; border: 0px solid red; width: 408px;height:130px;background-color:#eeeeee">
                    <div style="border: 0px solid red; width: 60%; float: left; text-align: center;color:black">
                        <img src="assets/images/<%= MG_BLL.Common.lib.Config.CurrentHost %>_QR.png" alt="扫描二维码下载APP" style="width: 110px; height: 110px;" />
                        <br />
                        扫码下载APP
                    </div>
                    <div style="border: 0px solid blue; width: 40%; float: left">

                        <button class="btn btn-blue btn-icon" type="button" style="margin-top: 10px;">
                            <i class="fa-download"></i>
                            <span>安卓下载</span>
                        </button>
                        <br />
                        <button class="btn btn-blue btn-icon" type="button">
                            <i class="fa-download"></i>
                            <span>苹果下载</span>
                        </button>
                    </div>--%>
                    <%-- <a href="#" class="facebook">
						<!--<i class="fa-facebook"></i>-->
                        <img src="assets/images/QQ.png" alt="" width="15" />
						QQ登录
					</a> 
					 
					<a href="#" class="twitter">
						<i class="fa-twitter"></i>
						Login with Twitter
					</a>
					
					<a href="#" class="gplus">
						<i class="fa-google-plus"></i>
						Login with Google Plus
					</a>--%>
                </div> 
				
			</div>
			
		</div>
		
	</div>



	<!-- Bottom Scripts -->
	<script src="assets/js/bootstrap.min.js"></script>
	<script src="assets/js/TweenMax.min.js"></script>
	<script src="assets/js/resizeable.js"></script>
	<script src="assets/js/joinable.js"></script>
	<script src="assets/js/xenon-api.js"></script>
	<script src="assets/js/xenon-toggles.js"></script>
	<script src="assets/js/jquery-validate/jquery.validate.min.js"></script>
	<script src="assets/js/toastr/toastr.min.js"></script>
    
    <script src="js/jquery.cookie.js"></script>

	<!-- JavaScripts initializations and stuff -->
	<script src="assets/js/xenon-custom.js"></script>
      <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script>
      <script type="text/javascript"> 
           var map = new BMap.Map("allmap", { enableMapClick: false });
           map.setMapStyle({ style: 'dark' });
           var point = new BMap.Point(116.331398, 39.897445);
           map.centerAndZoom(point, 12); 
           function myFun(result) {
               var cityName = result.name;
               map.setCenter(cityName);
           }
           var myCity = new BMap.LocalCity();
           myCity.get(myFun);
</script>
</body>
</html>