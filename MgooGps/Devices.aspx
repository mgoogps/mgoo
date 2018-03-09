<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Devices.aspx.cs" Inherits="MgooGps.Devices" %>

<%@ Import Namespace="MgooGps.com" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html> 

<html lang="ch">
<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
	 
    <meta name="viewport" content="width=960,height=device-height, user-scalable=no,initial-scale=0.33, minimum-scale=1, maximum-scale=1,target-densitydpi=device-dpi ">  
	<meta name="description" content="美谷 GPS 定位 导航 防盗" /> 
	<meta name="author" content="" /> 
	<title>GPS物联监控中心 - 设备管理</title>
    <script src="js/Language/language02-<%= Utils.language %>.js"></script>
       <style type="text/css">
    	body, html {width: 100%;height: 100%; overflow-x:hidden ;overflow-y:auto;margin:0;font-family:"微软雅黑";}
	</style> 
	<link rel="stylesheet" href="assets/css/fonts/linecons/css/linecons.css">
	<link rel="stylesheet" href="assets/css/fonts/fontawesome/css/font-awesome.min.css">
	<link rel="stylesheet" href="assets/css/bootstrap.css">
	<link rel="stylesheet" href="assets/css/xenon-core.css">
	<link rel="stylesheet" href="assets/css/xenon-forms.css">
	<link rel="stylesheet" href="assets/css/xenon-components.css">
	<link rel="stylesheet" href="assets/css/xenon-skins.css">
	<link rel="stylesheet" href="assets/css/custom.css"> 
      <link href="js/css/mgoo-style.css" rel="stylesheet" />

     <link rel="stylesheet" href="assets/js/daterangepicker/daterangepicker-bs3.css">
	<link rel="stylesheet" href="assets/js/select2/select2.css">
	<link rel="stylesheet" href="assets/js/select2/select2-bootstrap.css">
	<link rel="stylesheet" href="assets/js/multiselect/css/multi-select.css">
     <script src="js/jquery-1.8.3.js"></script>
      <script src="assets/js/select2/select2.min.js"></script>
   
    <!-- Imported styles on this page -->
	<link rel="stylesheet" href="assets/js/datatables/dataTables.bootstrap.css"/>

    <!-- Imported scripts on this page -->
	<script src="assets/js/toastr/toastr.min.js"></script> 
    <script src="js/mg_public.js"></script> 
   <%--  <style type="text/css"> 
       .l-tree .l-tree-icon-none img{width:16px; height:16px; margin:3px;}
   </style>--%>
  
      
   
   
	<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
	<!--[if lt IE 9]>
		<script src="assets/js/html5shiv.min.js"></script>
		<script src="assets/js/respond.min.js"></script>
	<![endif]-->
	 
        
       <script src="js/DeviceOper.js"></script>
       <script src="js/fun.js"></script>
    <link href="js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" /> 
    <script src="js/lib/ligerUI/js/core/base.js"></script> 
    <script src="js/lib/ligerUI/js/plugins/ligerComboBox.js"></script>
    <script src="js/lib/ligerUI/js/plugins/ligerResizable.js"></script>
    <script src="js/lib/ligerUI/js/plugins/ligerTree.js"></script>
    <script>
        var _hmt = _hmt || [];
        (function () {
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?63805c8d49dd83eac3a21c31f34f9352";
            var s = document.getElementsByTagName("script")[0];
            s.parentNode.insertBefore(hm, s);
        })();
</script>

</head>
<body class="page-body right-sidebar chat-open" >
     <input type="hidden" id="userid" value="<%= Utils.GetSession("UserInfo").UserID %>" /> 
     <input type="hidden" value ="<%=imei %>" id="txtImei" />
    <input type="hidden" value="<%=userid %>" id="txtAppointUserID" />
      <input type="hidden" value="<%= Utils.GetSession().SuperAdmin %>" id="isSuperAdmin" />

	<div class="page-container"  style="overflow:hidden"><!-- add class "sidebar-collapsed" to close sidebar by default, "chat-visible" to make chat appear always -->
	  
	 		<div class="main-content">
               
			<!-- User Info, Notifications and Menu Bar -->
			<nav class="navbar user-info-navbar horizontal-menu navbar-fixed-top " role="navigation"> 
             
				<!-- Right links for user info navbar -->
				<ul class="user-info-menu left-links list-inline list-unstyled" style="">
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
                        document.getElementById("aMenu").title = allPage.showhideSidebarTitle;
                        function _onmousemove(_this) {
                            $(_this).css("background-color", "#EEBEA8");
                            $(_this).children("a").children("span").css({ "color": "black", "font-weight": "bolder" });
                            $(_this).children("a").children("i").css({ "color": "black" });
                        }
                        function _onmouseout(_this) {
                            $(_this).css("background-color", "#D5592E");
                            $(_this).children("a").children("span").css({ "color": "white" });
                            $(_this).children("a").children("i").css({ "color": "white" });
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
					    <a target="_blank" href="Geofences.aspx">
						    <i class="fa-bar-chart" style="color:white;"></i>
						    <span class="title" style="color:white;" id="spanStatisticalAnalysis"><script type="text/javascript">
						                                                                              writePage(mapPage.geofence);
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
                                            <input type="text" id="txtSearchText" class="form-control no-left-border form-focus-red" placeholder="设备号(IMEI)/客户名/账号" value="<%= searchText %>" />
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
           <div style="left:0px;margin-bottom:0px; top:0px; bottom:0px;  position:absolute;width:100%; overflow:hidden;min-width:900px; " id="ifrParent"> 
              <iframe width="100%" height="100%" name="_right" id="iframepage" src="" frameborder="no" border="0" marginwidth="0" marginheight="0"  scrolling="yes" allowtransparency="true"> </iframe>
                <!-- Responsive Table --> 
           </div>  
	 </div>
		
	 	<!-- start: Chat Section -->
		<div id="chat" > 
			<div class="chat-inner" style="width:278px; height: calc(100% - 45px);overflow:hidden;"> 
				<h2 class="chat-header">
					<a href="#" class="chat-close" data-toggle="chat" id="aChatClose">
						<i class="fa-plus-circle rotate-45deg"></i>
					</a> 
						<script type="text/javascript">writePage(homePage.customerList)
					</script>
					<span class="badge badge-success is-hidden"></span>
              <%-- <button type="button" class="btn btn-purple btn-sm" onclick="addUsers()">添加用户</button>--%>
				</h2> 
				<div class="chat-group" style="right:0px; position:absolute;top:20px;left:0px; bottom: 0px;overflow:scroll; height:calc(100% - 75px)" id="zTreeContainer">
		               <ul id="treeDemo" class="ztree" style="height:100%;width:270px;"></ul> 
				</div> 
			</div>
			 
				<script type="text/javascript">
				    // Here is just a sample how to open chat conversation box
				    jQuery(document).ready(function ($) {
				        var $chat_conversation = $(".chat-conversation");
				        $(".conversation-close").on('click', function (ev) {
				            ev.preventDefault();
				            $chat_conversation.removeClass('is-open');
				        });
				        alarmTime($("#userid").val());
				        $("#chkLowerMsg").on("click", function () {
				            alarmTime($("#userid").val());
				        });
				    }); 
				</script>
             
		</div>
		<!-- end: Chat Section -->
			 <script type="text/javascript"> 
			     $(window).resize(function () {
			         var wh = $(window).height();
			        // $("#difrParentiv1").height($(window).height() - $("[role=navigation]").height() - 20);
			        // $("#ifrParent").width($(window).width() - $("#chat").width() - 20);
			         //$(".chat-inner").css({ "max-height": wh - 45});
			        // $("#zTreeContainer").css({"bottom": 0 }); 
			     });
			     $(function () {
			         $("#txtSearchText").attr("placeholder", allPage.imeiNo + "/" + dealerPage.username + "/" + allPage.devicePhone);
			         $("#txtSearchText").attr("title", allPage.deviceNo + "/" + dealerPage.username + "/" + allPage.devicePhone);
                      
			        // $("#ifrParent").height($(window).height() - $("[role=navigation]").height() - 20);
			        // $("#ifrParent").width($(window).width() - $("#chat").width());
			       
			         $(".chat-group a").on("click", function () {
			             $(".chat-group a").attr("class", "menua");
			             $(this).attr("class", "menuaselect");
			             show_loading_bar(25);
			             if ($.trim($(this).text()) == "全部设备") {
			                 $("#iframepage").attr("src", "AllDevices.aspx");
			             }
			             if ($.trim($(this).text()) == "报警消息") {
			                 $("#iframepage").attr("src", "AlarmMessage.aspx");
			             } 
			         });
			       
			     });
			 </script>
            <style type="text/css">
                .menua {
                    font-size:10px; margin-left:10px;color:black;
                }
                .menuaselect {
                    background-color:#EEEEEE;font-weight:bold;margin-left:12px; line-height:30px;
                }
            </style> 
	</div>
   <script type="text/javascript">
       function fileUpload() {
           $("#fileUpload").modal('show', { backdrop: 'static' });
       }
       function ExcelUpload() { 
           $.ajaxFileUpload({
               url: '/AjaxService/AjaxService.ashx?userid=' + zTree_Menu.getSelectedNodes()[0].id + '&uploadType=importDevices&action=upload',//处理图片脚本
               secureuri: false,
               fileElementId: "txtDeviceFileExcel",//file控件id、name
               dataType: 'json', 
               success: function (data, status) {
                   if (data.success) { 
                       $("#txtDeviceFileExcel").val("");
                       toastr.success(data.msg, "提示", opts_success);
                   } else {
                       toastr.warning(data.msg, "提示", opts_waming);
                   }
               },
               error: function (data, status, e) {
                   toastr.warning(data.msg, "提示", opts_waming);
               }
           });
       }
       function upLoadTemplate() { 
           $("#frmTemplate").attr("src", "/Upload/template.xlsx");
       }
       function reloadDevice(id) {
           //$("#iframepage").attr("src", "AllDevices.aspx?u=" + id);
       }
       </script>
    <!-- Modal 4 (Confirm)data-backdrop="static" data-dismiss="modal"-->
	<div class="modal fade" id="fileUpload" >
		<div class="modal-dialog">
			<div class="modal-content"> 
				<div class="modal-header">
					<h4 class="modal-title">提示</h4>
				</div> 
				<div class="modal-body"> 
					<input type="file" class="form-control" id="txtDeviceFileExcel" name="txtDeviceFileExcel" />
                 
				</div> 
				<div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal">取消</button>
                    <button type="button" class="btn btn-purple" onclick="upLoadTemplate()">下载模板</button>
					<button type="button" class="btn btn-info" onclick="ExcelUpload()">上传</button> 
                    <iframe src="" id="frmTemplate" style="display:none;"></iframe>
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
								    <input type="text" class="form-control" id="txtUserName" placeholder="name"/>
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

   <div class="modal fade" id="ModalShowDeviceDetail"> </div>

     <div class="modal fade" id="newImeiBatchSearch"> 
         	<div class="modal-dialog">
			<div class="modal-content">
                	<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">批量查询 </h4>
				</div>
				 <div class="modal-body" style="height:300px;"> 
                     <ul class="nav nav-tabs" id="searchtab">
                         <li class="active" value="1">
							<a href="#divImeis" data-toggle="tab" onclick="tabQiehuan(1)">
								<span class="visible-xs"><i class="fa-home"></i></span>
								<span class="hidden-xs"> IMEI号 </span>
							</a>
						</li>
                           <li value="2">
							<a href="#divImeis" data-toggle="tab" onclick="tabQiehuan(2)">
								<span class="visible-xs"><i class="fa-home"></i></span>
								<span class="hidden-xs">流量卡号</span>
							</a>
						</li>
                           <li value="3">
							<a href="#divImeis" data-toggle="tab" onclick="tabQiehuan(3)">
								<span class="visible-xs"><i class="fa-home"></i></span>
								<span class="hidden-xs"> 设备名称 </span>
							</a>
						</li>
                     </ul>
                     <script type="text/javascript">
                         function tabQiehuan(_this) {
                             if (_this == 1) {
                                 $('#txtImeis').attr('placeholder', '请输入IMEI，如有多个一行一个。');
                             } else if (_this == 2) {
                                 $('#txtImeis').attr('placeholder', '请输入流量卡号，如有多个一行一个。');
                             } else {
                                 $('#txtImeis').attr('placeholder', '请输入设备名称，如有多个一行一个。');
                             }
                         }
                     </script>
                  	<div class="tab-pane active" id="divImeis"> 
                      <textarea class="form-control" id="txtImeis" rows="20" style="height:260px;" placeholder="请输入IMEI号，如有多个一行一个。"><%= Utils.strImeiBatch  %></textarea> 
                    </div>
				</div>
				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnModalSearchClose"><script type="text/javascript">
					                                                                                           writePage(allPage.cancel);
								                                               </script></button>
					<button type="submit" class="btn btn-success" id="btnModalSearchConfirm" onclick="ModalSearchConfirm()"><script type="text/javascript">
					                                                                       writePage(allPage.confirm);
								                                               </script></button>
				</div>
            </div>
                </div></div>
           

    <link href="zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="zTree_v3/js/jquery.ztree.core-3.5.js"></script>
    <script src="js/ajaxfileupload.js"></script> 
    <script src="js/DeviceOper.js"></script>
	<SCRIPT type="text/javascript">  
	  
	    var zTree_Menu;

	    var setting = {
	        view: {
	            showLine: false,
	            showIcon: true,
	            selectedMulti: false,
	            dblClickExpand: false,
	            addDiyDom: addDiyDom
	        },
	        data: {
	            simpleData: {
	                enable: true
	            }
	        },
	        async: {
	           // url: "AjaxService/AjaxService.ashx?action=getTree",
	           // type: "POST",
	           //// dataType: 'json',
	           // enable: true,
	           // dataFilter: filter,
	           // autoParam:["id"]
	           // otherParam:{"otherParam":"zTreeAsyncTest"}
	        },
	        callback: {
	            beforeClick: beforeClick
	        }
	    };

	    var zNodes = new Array();
	    $.ajax({
	        url: "AjaxService/AjaxService.ashx?action=getTree",
	        type: 'POST',
	        dataType: 'json',
	        data: {},
	        success: function (dataList) {
	            var i = 0;
	            $(dataList[""]).each(function (k, v) {
	                zNodes[i] = { id: v["UserID"], pId: v["ParentID"], name: v["UserName"], icon: (v.UserType == 1 ? "js/lib/ligerUI/skins/icons/memeber.gif" : "js/lib/ligerUI/skins/icons/customers.gif") }; i++;
	            }); 
	            var treeObj = $("#treeDemo");
	            $.fn.zTree.init(treeObj, setting, zNodes); 
	            zTree_Menu = $.fn.zTree.getZTreeObj("treeDemo");
	 
	            curMenu = zTree_Menu.getNodes()[0];
                 
	            if ($("#txtAppointUserID").val() != "") { 
	                curMenu = zTree_Menu.getNodeByParam("id", $("#txtAppointUserID").val(), null);//treeObj是tree对象 
	                zTree_Menu.selectNode(curMenu);
	                $("#txtAppointUserID").val("");
	            } else {
	                zTree_Menu.selectNode(curMenu);
	                curMenu = zTree_Menu.getNodes()[0];
	                zTree_Menu.selectNode(curMenu);
	                zTree_Menu.expandNode(curMenu);
	            } 
	            beforeClick(zTree_Menu.getSelectedNodes()[0].id, zTree_Menu.getSelectedNodes()[0]);
	          
	          
	            treeObj.addClass("showIcon");
	        }
	    });

	    function filter(treeId, parentNode, childNodes) { 
	        if (!childNodes) return null;
	        for (var i = 0, l = childNodes.length; i < l; i++) {
	            childNodes[i].name = childNodes[i].name.replace(/\.n/g, '.');
	        }
	        return childNodes;
	    }
	    function addDiyDom(treeId, treeNode) {
	        var spaceWidth = 5;
	        var switchObj = $("#" + treeNode.tId + "_switch"),
            icoObj = $("#" + treeNode.tId + "_ico");
	        switchObj.remove();
	        icoObj.before(switchObj); 
	        if (treeNode.level > 1) {
	            var spaceStr = "<span style='display: inline-block;width:" + (spaceWidth * treeNode.level) + "px'></span>";
	            switchObj.before(spaceStr);
	        }
	    }
	    var isLoadDevicesList = true;
	    function beforeClick(treeId, treeNode) {
	        // show_loading_bar(25); 
	        var url = "AllDevices.aspx?u=" + treeNode.id;

	        if ($("#txtImei").val()!="") {
	            url += "&imei=" + $("#txtImei").val();
	            $("#txtImei").val("");
	        }
	        $("#iframepage").attr("src", url);
	        return true;
	    }

	    $(function () { 
	        var chatWidth = $("#chat").width();
	        $("#aMenu").on("click", function () {
	            ResizeMain();
	        });
	        $("#aChatClose").on("click", function () {
	            ResizeMain();
	        });  
	    });

    </SCRIPT>

     
    <link href="zTree_v3/css/zTree.css" rel="stylesheet" />
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
   