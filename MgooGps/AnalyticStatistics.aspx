<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnalyticStatistics.aspx.cs" Inherits="MgooGps.AnalyticStatistics" %>

 <%@ Import Namespace="MgooGps.com" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html> 

<html lang="ch">
<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge"> 
	<meta name="viewport" content="width=960,height=device-height, user-scalable=no,initial-scale=0.33, minimum-scale=1, maximum-scale=1,target-densitydpi=device-dpi ">  
	<meta name="description" content="Xenon Boostrap Admin Panel" />
	<meta name="author" content="" />
	
	<title>GPS物联监控中心 - 分析统计</title>
    <script src="js/Language/language02-<%= Utils.language %>.js"></script>
       <style type="text/css">
    	body, html {width: 100%;height: 100%;margin:0;font-family:"微软雅黑";}
	</style> 
	<link rel="stylesheet" href="assets/css/fonts/linecons/css/linecons.css">
	<link rel="stylesheet" href="assets/css/fonts/fontawesome/css/font-awesome.min.css">
	<link rel="stylesheet" href="assets/css/bootstrap.css">
	<link rel="stylesheet" href="assets/css/xenon-core.css">
	<link rel="stylesheet" href="assets/css/xenon-forms.css">
	<link rel="stylesheet" href="assets/css/xenon-components.css">
	<link rel="stylesheet" href="assets/css/xenon-skins.css">
	<link rel="stylesheet" href="assets/css/custom.css"> 

     <link rel="stylesheet" href="assets/js/daterangepicker/daterangepicker-bs3.css">
	<link rel="stylesheet" href="assets/js/select2/select2.css">
	<link rel="stylesheet" href="assets/js/select2/select2-bootstrap.css">
	<link rel="stylesheet" href="assets/js/multiselect/css/multi-select.css">
     <script src="js/jquery-1.8.3.js"></script>
      <script src="assets/js/select2/select2.min.js"></script> 
    <!-- Imported styles on this page -->
	<link rel="stylesheet" href="assets/js/datatables/dataTables.bootstrap.css"/>

    <link href="js/css/mgoo-style.css" rel="stylesheet" />

    <!-- Imported scripts on this page -->
	<script src="assets/js/toastr/toastr.min.js"></script> 
    <script src="js/mg_public.js"></script>  
       <script src="js/DeviceOper.js"></script>
    <link href="js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" /> 
    <script src="js/lib/ligerUI/js/core/base.js"></script> 
    <script src="js/lib/ligerUI/js/plugins/ligerComboBox.js"></script>
    <script src="js/lib/ligerUI/js/plugins/ligerResizable.js"></script>
    <script src="js/lib/ligerUI/js/plugins/ligerTree.js"></script>
	<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
	<!--[if lt IE 9]>
		<script src="assets/js/html5shiv.min.js"></script>
		<script src="assets/js/respond.min.js"></script>
	<![endif]-->
	   <script src="js/ajaxfileupload.js"></script>
 <script>
     var _hmt = _hmt || [];
     (function() {
         var hm = document.createElement("script");
         hm.src = "//hm.baidu.com/hm.js?63805c8d49dd83eac3a21c31f34f9352";
         var s = document.getElementsByTagName("script")[0]; 
         s.parentNode.insertBefore(hm, s);
     })();
</script>

</head>
<body class="page-body right-sidebar chat-open" >
     <input type="hidden" id="userid" style="bottom:2px;  " value="<%= Utils.GetSession("UserInfo").UserID %>" />
	<div class="page-container"  style="overflow:hidden"><!-- add class "sidebar-collapsed" to close sidebar by default, "chat-visible" to make chat appear always -->
	 
	 		<div class="main-content">
               
			<!-- User Info, Notifications and Menu Bar -->
			<nav class="navbar user-info-navbar horizontal-menu navbar-fixed-top " role="navigation"> 
             
				<!-- Right links for user info navbar -->
				<ul class="user-info-menu left-links list-inline list-unstyled">
                        <!-- Navbar Brand -->
			        <div class="abc-header">
				        <a href="javascript:void(0)" class="abc-header-img">
					        <img src="assets/images/<%= MG_BLL.Common.lib.Config.CurrentHost %>.png" width="240" alt="" class="hidden-xs" />					        
				        </a>				       
			        </div>
					<li onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)"  >
						<a href="#" data-toggle="chat" id="aMenu" title="">
                            <i class="fa-angle-double-left" id="iMenu" style="color:white;"></i>
						</a>  
					</li> 
                    <script type="text/javascript"> 
                        document.getElementById("aMenu").title=allPage.showhideSidebarTitle;
                        function _onmousemove(_this) {
                            $(_this).css("background-color", "#EEBEA8");
                            $(_this).children("a").children("span").css({"color":"black","font-weight":"bolder"});
                            $(_this).children("a").children("i").css({"color":"black"}); 
                        }
                        function _onmouseout(_this) {
                            $(_this).css("background-color", "#D5592E");
                            $(_this).children("a").children("span").css({"color":"white"});
                            $(_this).children("a").children("i").css({"color":"white"}); 
                        }   
                    </script>
                 <li class="opened active" onmousemove="_onmousemove(this)" onmouseout="_onmouseout(this)" style="font-size:18px; font-weight:bold;" title="定位监控">
					    <a href="main.aspx" style="border:0px solid blue;">
						    <i class="fa-binoculars" style="color:white;"></i>
						    <span class="title" style="color:white;" id="spanMonitor"><script type="text/javascript">
						                                                                  writePage(allPage.monitor);
                                            </script></span>
					    </a>
                    </li>
                    <li class="opened active" onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)" style="font-size:18px; font-weight:bold;" title="统计报表">
					    <a href="numerical.aspx" id="tongji">
						    <i class="fa-line-chart" style="color:white;"></i>
						    <span class="title" style="color:white;" id="spanReport"><script type="text/javascript">
						                                                                 writePage(allPage.report);
                                            </script> </span>
					    </a>
                    </li>
                      <li class="opened active" onmousemove="_onmousemove(this)" onmouseout="_onmouseout(this)" style="font-size:18px;font-weight:bold;" title="用户管理">
					    <a href="Devices.aspx">
						    <i class="fa-car" style="color:white;"></i>
						    <span class="title" style="color:white;" id="spanManDevice"><script type="text/javascript">
						                                                                    writePage(allPage.manUser);
                                            </script> </span>
					    </a>
                    </li>
                     <li class="opened active" onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)" style="font-size:18px;font-weight:bold;" title="分析统计">
					    <a href="AnalyticStatistics.aspx">
						    <i class="fa-bar-chart" style="color:white;"></i>
						    <span class="title" style="color:white;" id="spanStatisticalAnalysis"><script type="text/javascript">
						                                                                              writePage(allPage.StatisticalAnalysis);
                                            </script></span>
					    </a> 
                    </li>
                     
                      <!--  用户信息 -->
					<li class="dropdown user-profile hover-line"  onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)" style="float:right; margin-right:0px;">
						<a href="#" data-toggle="dropdown">
							<img src="assets/images/user-4.png" alt="user-image" class="img-circle img-inline userpic-32" width="28" />
							<span  style="color:white;">
								 <%=  Utils.GetSession("UserInfo").UserName  %> 
								<i class="fa-angle-down" style="color:white;"></i>
							</span>
						</a>
						
						<ul class="dropdown-menu user-profile-menu list-unstyled">
							<li>
								<a href="#edit-profile" id="edit-profile" userid="<%= Utils.GetSession("UserInfo").UserID %>">
									<i class="fa-edit"></i>
                                   <script type="text/javascript">
                                       writePage(allPage.edit2);
                                            </script>  
								</a>
							</li>
							<li>
								<a href="#settings" onclick="UpdatePwd()">
									<i class="fa-wrench"></i>
									   <script type="text/javascript">
									       writePage(userInfoPage.changePassword);
                                            </script> 
								</a>
							</li>
						<%--	<li>
								<a href="#" id="aOffLineDevice">
									<i class="fa-user"></i>
								   <script type="text/javascript">
								       writePage(allPage.edit2);
                                            </script>	离线设备管理
								</a>
							</li>--%>
                                   <%
                               if (Utils.GetSession().SuperAdmin == "1") {%>
                            <li>
								<a href="#importexcelsim">
									<i class="fa fa-sign-in"></i>
									 批量修改SIM卡
								</a>
							</li>
				
                             <%}
                                %>
                    
							<li>
								<a href="#service">
									<i class="fa-info"></i>
									   <script type="text/javascript">
									       writePage(userInfoPage.service);
                                        </script> 
								</a>
							</li>
							<li class="last">
								<a href="default.aspx?action=logout">
									<i class="fa-lock"></i>
									   <script type="text/javascript">
									       writePage(allPage.exit);
                                            </script> 
								</a>
							</li>
						</ul>
					</li>
                     
                      <!-- 报警信息 -->
                     <li class="dropdown hover-line" style="float:right;"  onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)">
						<a href="#" data-toggle="dropdown">
							<i class="fa-bell-o" style="color:white;"></i>
							<span class="badge badge-purple" id="spanAlarmCount">0</span>
						</a>
	  
						<ul class="dropdown-menu messages user-profile-menu" style="width:300px;">
							<li class="top"> 
								<p class="small"> 
									<a href="#" class="pull-right" id="allReadonly"><script type="text/javascript">
									                                                    writePage(allPage.clearAll);
                                            </script> </a>
                                    	<%--前<strong id="AgoAlarm">20</strong>条报警信息.--%>
                                     <label class="checkbox-inline">
											<input type="checkbox" <%= Utils.isPlayAudio?"checked='checked'":"" %> id="chkAudio" /> <script type="text/javascript">writePage(dealerPage.warnSound);</script>
									 </label>
                                    <% if (Utils.GetSession("UserInfo").UserType == "2") { 
                                       %>
                                        <label class="checkbox-inline">
											<input type="checkbox"  <%= Utils.LowerMsg?"checked='checked'":"" %> id="chkLowerMsg" /><script type="text/javascript">writePage(allPage.lowerAlarm)
											                                                                                        </script>
									    </label> 
                                    <%
                                       } %> 
								</p>
							</li> 
							<li style="margin-left:-20px; margin-right:-5px;">
								<ul class="dropdown-menu-list list-unstyled" id="ulAlarm">  
							 
								</ul>
							</li>
							
							<li class="last">
								<a href="numerical.aspx?type=1" style="width:115px;">
									<span><script type="text/javascript">writePage(allPage.viewAllAlarm)
									    </script></span>
									<i class="fa-link-ext"></i>
								</a>
                                  <i class="linecons-cog" style="cursor:pointer;float:right; margin-top:-27px;font-size:18px;" onclick="showSettingMsgType()"></i> 
							</li>
						</ul>
					</li>

                    <!-- 设置 -->
				   <%-- <li class="hover-line" style="float:right;"> 
						<a href="#" data-toggle="settings-pane" data-animate="true">
							<i class="linecons-cog"></i>
						</a>
					 </li>  --%> 
                   <%if (Utils.GetSession("UserInfo").UserType == "2")
                      {
                          %>
                       <!-- 搜索 -->
                      <li class="opened active" style="float:right; margin-top:23px;" > 
										<div class="input-group search-input" style="width:250px;">
                                            <input type="text" id="txtSearchText" class="form-control no-left-border form-focus-red" placeholder="设备号(IMEI)/客户名/账号"  />
											<span class="input-group-btn"> 
												<button type="button" class="btn btn-red dropdown-toggle" data-toggle="dropdown" onclick="javascript:SearchBox('txtSearchText',0);">
													  <script type="text/javascript">
													      writePage(homePage.quickSearch);
                                                        </script>  <%--<span class="caret"></span>--%>
												</button> 
												<%--<ul class="dropdown-menu dropdown-red no-spacing"> 
													<li><a href="#" id="btnSearchDevice" onclick="javascript:SearchBox('txtSearchText',0);">搜设备</a></li> 
													<li class="divider"></li>
													<li><a href="#" id="btnSearchUser" onclick="javascript:SearchBox('txtSearchText',1);">搜客户</a></li>
												</ul>--%>
											</span> 
										</div>  
					  </li>
                    <%
                      } %>
                             
				    
				  <!-- 信息 -->
                  <%--  <li style="float:right;">
                        <li class="dropdown hover-line">
						    <a href="#" data-toggle="dropdown">
							    <i class="fa-envelope-o"></i>
							    <span class="badge badge-green">15</span>
						    </a> 
                            
						    <ul class="dropdown-menu messages">
							<li> 
								<ul class="dropdown-menu-list list-unstyled ps-scrollbar">
								
									<li class="active"><!-- "active" class means message is unread -->
										<a href="#">
											<span class="line">
												<strong>Luc Chartier</strong>
												<span class="light small">- yesterday</span>
											</span>
											
											<span class="line desc small">
												This ain’t our first item, it is the best of the rest.
											</span>
										</a>
									</li>
									
									<li class="active">
										<a href="#">
											<span class="line">
												<strong>Salma Nyberg</strong>
												<span class="light small">- 2 days ago</span>
											</span>
											
											<span class="line desc small">
												Oh he decisively impression attachment friendship so if everything. 
											</span>
										</a>
									</li>
									
									<li>
										<a href="#">
											<span class="line">
												Hayden Cartwright
												<span class="light small">- a week ago</span>
											</span>
											
											<span class="line desc small">
												Whose her enjoy chief new young. Felicity if ye required likewise so doubtful.
											</span>
										</a>
									</li>
									
									<li>
										<a href="#">
											<span class="line">
												Sandra Eberhardt
												<span class="light small">- 16 days ago</span>
											</span>
											
											<span class="line desc small">
												On so attention necessary at by provision otherwise existence direction.
											</span>
										</a>
									</li>
									
									<!-- Repeated -->
									
									<li class="active"><!-- "active" class means message is unread -->
										<a href="#">
											<span class="line">
												<strong>Luc Chartier</strong>
												<span class="light small">- yesterday</span>
											</span>
											
											<span class="line desc small">
												This ain’t our first item, it is the best of the rest.
											</span>
										</a>
									</li>
									
									<li class="active">
										<a href="#">
											<span class="line">
												<strong>Salma Nyberg</strong>
												<span class="light small">- 2 days ago</span>
											</span>
											
											<span class="line desc small">
												Oh he decisively impression attachment friendship so if everything. 
											</span>
										</a>
									</li>
									
									<li>
										<a href="#">
											<span class="line">
												Hayden Cartwright
												<span class="light small">- a week ago</span>
											</span>
											
											<span class="line desc small">
												Whose her enjoy chief new young. Felicity if ye required likewise so doubtful.
											</span>
										</a>
									</li>
									
									<li>
										<a href="#">
											<span class="line">
												Sandra Eberhardt
												<span class="light small">- 16 days ago</span>
											</span>
											
											<span class="line desc small">
												On so attention necessary at by provision otherwise existence direction.
											</span>
										</a>
									</li>
									
								</ul>
								
							</li>
							
							<li class="external">
								<a href="blank-sidebar.html">
									<span>All Messages</span>
									<i class="fa-link-ext"></i>
								</a>
							</li>
						</ul>
					    </li> 
                    </li> --%>
				 
				</ul> 
			</nav> 
     <div style="left:0px; margin-bottom:0px; top:0px; bottom:0px;  position:absolute;width:100%; overflow:hidden; " id="ifrParent"> 
              <iframe width="100%" height="100%" name="_right" id="iframepage" src="Statistics/Statistics.aspx" frameborder="no"  border="0" marginwidth="0" marginheight="0"  scrolling="auto" allowtransparency="true"  > </iframe>
                <!-- Responsive Table --> 
           </div>  
	 </div>
		
	 
		<!-- start: Chat Section -->
		<div id="chat" > 
			<div class="chat-inner" style="width:278px;top:45px";"> 
				<h2 class="chat-header">
					<a href="#" class="chat-close" data-toggle="chat" id="aChatClose">
						<i class="fa-plus-circle rotate-45deg"></i>
					</a> 
						<script type="text/javascript">writePage(allPage.StatisticalAnalysis)
					</script>
					<span class="badge badge-success is-hidden"></span>
				</h2> 
				<div class="chat-group"> 
					<strong style="font-weight:bold; font-size:12px;color:black;"><i class="fa-angle-right"></i> 运行统计</strong> 
					<a href="#" class="menua"><i class="fa-caret-right"></i>  <em>设备统计</em></a>
                    <a href="#" class="menua"><i class="fa-caret-right"></i>  <em>设备过期提示</em></a> 
                   
					<%--<a href="#" class="menua"><i class="fa-caret-right"></i>  <em>7天内过期设备</em></a> 
					<a href="#" class="menua"><i class="fa-caret-right"></i>  <em>60天内过期设备</em></a>--%>
                <%--    <a href="#" class="menua"><i class="fa-caret-right"></i>  <em>已过期设备</em></a>--%>
                    <% if (Utils.GetSession("UserInfo").SuperAdmin == "1")
                       {  %>
                       <a href="#" class="menua"><i class="fa-caret-right"></i>  <em>已删除设备</em></a>
                     <a href="#" class="menua"><i class="fa-caret-right"></i>  <em>续费设备导出</em></a> 
                    <%   } %>
                 
				</div> 
			</div>
            <script type="text/javascript">
                var ModelType = 0;
                $(function () { 
                    $("#txtSearchText").attr("placeholder",allPage.imeiNo + "/" + dealerPage.username+"/"+allPage.devicePhone);
                    $("#txtSearchText").attr("title", allPage.deviceNo + "/" + dealerPage.username+"/"+allPage.devicePhone);

                    $(window).resize(function () {
                        $("#diifrParentv1").height($(window).height() - $("[role=navigation]").height() - 20);
                        $("#ifrParent").width($(window).width() - $("#chat").width() - 20); 
                    });
                    $("#ifrParent").css("min-width", "900px");
                    $("#ifrParent").height($(window).height() - $("[role=navigation]").height() - 20);
                    $("#ifrParent").width($(window).width() - $("#chat").width());

                    alarmTime($("#userid").val());
                    $("#chkLowerMsg").on("click", function () { 
                        alarmTime($("#userid").val());
                     });
                    $(".chat-group a").on("click", function () {
                        $(".chat-group a").attr("class", "menua");
                        $(this).attr("class", "menuaselect");
                        show_loading_bar(25);
                        if ($.trim($(this).text()) == "设备统计") {
                            $("#iframepage").attr("src", "../Statistics/Statistics.aspx");
                        }else if ($.trim($(this).text()) == "设备过期提示") {
                            $("#iframepage").attr("src", "../Statistics/DevicesHireExpire.aspx?day=10");
                        }else if ($.trim($(this).text()) == "7天内过期设备") {
                            $("#iframepage").attr("src", "../Statistics/DevicesHireExpire.aspx?day=7");
                        }else if ($.trim($(this).text()) == "60天内过期设备") {
                            $("#iframepage").attr("src", "../Statistics/DevicesHireExpire.aspx?day=60");
                        }else if ($.trim($(this).text()) == "已过期设备") {
                            $("#iframepage").attr("src", "../Statistics/DevicesHireExpire.aspx?day=-1");
                        } else if($.trim($(this).text()) == "已删除设备") {
                            $("#iframepage").attr("src", "../Statistics/DeletedDevice.aspx");
                        }else if($.trim($(this).text()) == "续费设备导出") {
                            $("#iframepage").attr("src", "../Statistics/RenewalsExport.aspx");
                        }
                    });
                });
                function showModal(type) { 
                    $("#ModalShowDeviceDetail").load("ModalShowOfflineDevice.aspx #modalDialog", function (response, status, xhr) { 
                        ModelType=type;                        
                        modelParameters(); 
                    });
                    //$("#ModalShowDeviceDetail").modal('show', { backdrop: 'static' });
                }
            </script>
            <style type="text/css">
                .menua {
                    font-size: 12px;
                    margin-left: 20px;
                }

                .menuaselect {
                    background-color: #D6D6D6;
                    font-weight: bold;
                    margin-left: 20px;
                    line-height: 30px;
                }
            </style>
        </div>
		<!-- end: Chat Section -->  
	</div>



    <!-- Modal 4 (Confirm)-->
	<div class="modal fade" id="modal-4" data-backdrop="static">
		<div class="modal-dialog">
			<div class="modal-content">
				
				<div class="modal-header">
					<h4 class="modal-title">提示</h4>
				</div> 
				<div class="modal-body"> 
					登录信息失效，请重新登录！ 
				</div> 
				<div class="modal-footer">
					<button type="button" class="btn btn-info" data-dismiss="modal" onclick="window.location.href='login.aspx'">确定</button>
				</div>
			</div>
		</div>
	</div>

	 	 	   <!--用户信息 -->
    <div class="modal fade" id="modal-6"  >
		<div class="modal-dialog">
			<div class="modal-content">
			  <form role="form" id="form1" method="post" class="validate" onsubmit="return updateUserInfo('<%=Utils.GetSession("UserInfo").UserID %>');">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title"><script type="text/javascript">
					                            writePage(allPage.userInfo)
					                        </script> </h4>
				</div>
				
				<div class="modal-body">
			
					    <div class="row">
						    <div class="col-md-6">
							
							    <div class="form-group">
								    <label for="field-1" class="control-label"><script type="text/javascript">
								                                                   writePage(userInfoPage.customerName)
					                        </script></label> 
								    <input type="text" class="form-control" id="txtUserName" placeholder="name" disabled/>
							    </div>	
							
						    </div>
						
						    <div class="col-md-6">
							
							    <div class="form-group">
								    <label for="field-2" class="control-label"><script type="text/javascript">
								                                                   writePage(userInfoPage.account);
								                                               </script></label> 
								    <input type="text" class="form-control" id="txtLoginName" placeholder="LoginName" disabled/>
							    </div>	
						
						    </div>
					    </div>
				      <div class="row">
						    <div class="col-md-4">
							
							    <div class="form-group">
								    <label for="field-4" class="control-label"><script type="text/javascript">
								                                                   writePage(allPage.cellName);
								                                               </script></label> 
								    <input type="text" class="form-control" id="txtContacts" name="name" placeholder="Contacts" data-validate="required" data-message-required="联系人不能为空." />
							    </div>	
							
						    </div>
						
						    <div class="col-md-4">
							
							    <div class="form-group">
								    <label for="field-5" class="control-label"><script type="text/javascript">
								                                                   writePage(allPage.phone);
								                                               </script></label> 
								    <input type="text" class="form-control" id="txtPhone" name="min_field" data-validate="required,number,minlength[6]" placeholder="Phone"/>
							    </div>	
						
						    </div>
						
						    <div class="col-md-4"> 
							    <div class="form-group">
								    <label for="field-6" class="control-label"><script type="text/javascript">
								                                                   writePage(allPage.primaryEmail);
								                                               </script></label> 
								    <input type="text" class="form-control"  id="txtEmail" placeholder="E-Mail" data-mask="email" name="email" data-message-email="请输入正确的邮箱" data-validate="email"/>
							    </div>	 
						    </div>
					    </div>
					    <div class="row">
						    <div class="col-md-12">
							
							    <div class="form-group">
								    <label for="field-3" class="control-label"><script type="text/javascript">
								                                                   writePage(allPage.address);
								                                               </script></label>
								    <input type="text" class="form-control" id="txtAddress" placeholder="Address"/>
							    </div>	
							
						    </div>
					    </div> 
                   
				</div>
				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnUserInfoClose"><script type="text/javascript">
					                                                                                           writePage(allPage.cancel);
								                                               </script></button>
					<button type="submit" class="btn btn-success" id="btnSaveUserInfo"><script type="text/javascript">
					                                                                       writePage(allPage.confirm);
								                                               </script></button>
				</div>
             </form>
			</div>
		</div>
	</div> 
     
   <div class="modal fade" id="ModalShowDeviceDetail"> 
  
   </div> 
	<script type="text/javascript"> 
	    function modelParameters(){
	        show_loading_bar(25); 
	        $("#ModalShowDeviceDetail").modal('show', { backdrop: 'static' });
	        $("#deviceModel").select2({
	            placeholder: '请输入设备型号查询...',
	            allowClear: true
	        }).on('select2-open', function () { 
	            $(this).data('select2').results.addClass('overflow-hidden').perfectScrollbar();
	        }).change(function(){
	            var vals = $(this).val();
	            if (vals == null) {
	                $("#OffLineDeviceTable tbody tr").show();
	                return;
	            } 
	            var trs = $("#OffLineDeviceTable tbody tr");
	            $("#OffLineDeviceTable tbody tr").hide();
	            var hides = [];
	            for (var i = 0; i < trs.length; i++) {
	                arr();
	                if (vals.indexOf($(trs[i]).attr("name")) >= 0) {
	                    hides.push(trs[i]);
	                }
	            }
	            for (var i = 0; i < hides.length; i++) {
	                $(hides[i]).show();
	            }
	        });
	        show_loading_bar(70);
	        GetDevicesModel($("#deviceModel optgroup"));
	        GetOfflineDevice(<%=  Utils.GetSession("UserInfo").UserID  %> ,$("#deviceModel").val(),"2000-01-01","2020-12-12");
	        $("#btnSearchOffLine").on("click",function(){
	            GetOfflineDevice( <%=  Utils.GetSession("UserInfo").UserID  %> ,$("#deviceModel").val(),"2000-01-01","2020-12-12");
	        });
	      
	        //var date = new Date(); 
	        //var d = date.getFullYear()+"-"+ (((date.getMonth()+1)+"").length==1? "0"+(date.getMonth()+1):(date.getMonth()+1)) +"-"+ ((date.getDay()+"").length == 1?("0"+date.getDay()) :date.getDay())  +" 到 "+date.getFullYear()+"-"+(((date.getMonth()+1)+"").length==1? "0"+(date.getMonth()+1):(date.getMonth()+1))+"-"+ ((date.getDay()+"").length == 1?("0"+date.getDay()) :date.getDay());
	        //$('#reservationtime').val(d);
	        //$('#reservationtime').daterangepicker({
	        //    timePicker: false,
	        //    timePickerIncrement: 10,
	        //    format: 'YYYY-MM-DD'
	        //}, function (start, end, label) { 
	        //});
	    }
	    $(function () {
	        $("#aOffLineDevice").on("click",function(){
	            $("#ModalShowDeviceDetail").load("ModalShowOfflineDevice.aspx #modalDialog", function (response, status, xhr) {
	                modelParameters();
	            });
	        });


	        var chatWidth = $("#chat").width();
	        $("#aMenu").on("click", function () { 
	            ResizeMain();
	        });   
	        $("#aChatClose").on("click", function () {
	            ResizeMain();
	        });
 
	        //$("#btnAllClear").on("click", function () {
	        //    allReadonly($("#userid").val());
	        //});

	    });

	    function toastrMessage(type, message, title) {
	        switch (type) {
	            case "opts_success":
	                toastr.success(message, title, opts_success);
	                break;
	            case "opts_danger":
	                toastr.success(message, title, opts_danger);
	                break;
	            case "opts_waming":
	                toastr.success(message, title, opts_waming);
	                break;
	        }
	    }
    </script>

     
 	<!-- Imported styles on this page -->
	<link rel="stylesheet" href="assets/js/daterangepicker/daterangepicker-bs3.css">
	<link rel="stylesheet" href="assets/js/select2/select2.css">
	<link rel="stylesheet" href="assets/js/select2/select2-bootstrap.css">
	<link rel="stylesheet" href="assets/js/multiselect/css/multi-select.css">

	<!-- Bottom Scripts -->
	<script src="assets/js/bootstrap.min.js"></script>
	<script src="assets/js/TweenMax.min.js"></script>
	<script src="assets/js/resizeable.js"></script>
	<script src="assets/js/joinable.js"></script>
	<script src="assets/js/xenon-api.js"></script>
	<script src="assets/js/xenon-toggles.js"></script>
	<script src="assets/js/moment.min.js"></script>


	<!-- Imported scripts on this page -->
	<script src="assets/js/daterangepicker/daterangepicker.js"></script>
	<script src="assets/js/datepicker/bootstrap-datepicker.js"></script>
	<script src="assets/js/timepicker/bootstrap-timepicker.min.js"></script>
	<script src="assets/js/colorpicker/bootstrap-colorpicker.min.js"></script>
	<script src="assets/js/select2/select2.min.js"></script>
	<script src="assets/js/jquery-ui/jquery-ui.min.js"></script>
	<script src="assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>
	<script src="assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>
	<script src="assets/js/typeahead.bundle.js"></script>
	<script src="assets/js/handlebars.min.js"></script>
	<script src="assets/js/multiselect/js/jquery.multi-select.js"></script>

    <script src="assets/js/datatables/js/jquery.dataTables.min.js"></script>
    <!-- Imported scripts on this page -->
	<script src="assets/js/rwd-table/js/rwd-table.min.js"></script>

	<!-- JavaScripts initializations and stuff -->
	<script src="assets/js/xenon-custom.js"></script>

	<!-- Imported scripts on this page -->
	<script src="assets/js/datatables/dataTables.bootstrap.js"></script>
	<script src="assets/js/datatables/yadcf/jquery.dataTables.yadcf.js"></script>
	<script src="assets/js/datatables/tabletools/dataTables.tableTools.min.js"></script>

    <!-- Imported scripts on this page -->
	<script src="assets/js/jquery-validate/jquery.validate.min.js"></script>


     <script src="js/fun.js"></script>
     <script src="js/CoureName.js"></script>

    <style type="text/css">
        a:link {
            color: blue;
            text-decoration: none;  
        }  
            a:visited {color:purple;text-decoration:none;} 
            a:hover {color: red; text-decoration:none;}
    </style>
  
</body>
    
</html>
   