﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="MgooGps.main" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
 <%@ Import Namespace="MgooGps.com" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE/>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta charset="utf-8"/> 
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/> 
<%--	<meta name="viewport" content="width=device-width, initial-scale=1.0" />--%>
	<meta name="description" content="美谷 美谷科技 GPS 定位 防盗 电子狗" />
	<meta name="author" content="" />
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Expires" content="0"/>  
    <meta http-equiv="Cache-Control" content="no-transform" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <meta name="viewport" content="width=960,height=device-height, user-scalable=no,initial-scale=0.33, minimum-scale=1, maximum-scale=1,target-densitydpi=device-dpi ">  

	<title>GPS物联监控中心 - 定位监控</title> 
    <script   src="js/Language/language02-<%= Utils.language %>.js"></script>
 
    <link href="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.js"></script>
    <style type="text/css">
        #allmap {
            width: 100%;
            height: 100%;
            overflow: hidden;
            margin: 0;
            font-family: "微软雅黑";
        }

        body, html {
            overflow-x: auto;
            overflow-y: hidden;
        }
    </style>  
	<link rel="stylesheet" href="assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="assets/css/fonts/fontawesome/css/font-awesome.min.css"/>
	<link rel="stylesheet" href="assets/css/bootstrap.css"/> 
	<link rel="stylesheet" href="assets/css/xenon-core.css"/>
	<link rel="stylesheet" href="assets/css/xenon-forms.css"/>
	<link rel="stylesheet" href="assets/css/xenon-components.css"/>
	<link rel="stylesheet" href="assets/css/xenon-skins.css"/>
	<link rel="stylesheet" href="assets/css/custom.css"/> 
    <link href="js/css/mgoo-style.css" rel="stylesheet" />
     <script src="js/jquery-1.8.3.js"></script>
    <script src="js/ajaxfileupload.js"></script>
            <!--时间控件-->
    <link rel="stylesheet" href="assets/js/daterangepicker/daterangepicker-bs3.css"/>  
	<script src="assets/js/datepicker/bootstrap-datepicker.js"></script>
 
     
	<link rel="stylesheet" href="assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="assets/js/select2/select2-bootstrap.css"/>
	<link rel="stylesheet" href="assets/js/multiselect/css/multi-select.css"/>
     
      <script src="assets/js/select2/select2.min.js"></script>

       
    <!-- Imported styles on this page -->
	<link rel="stylesheet" href="assets/js/datatables/dataTables.bootstrap.css"/>

    <!-- Imported scripts on this page -->
     <script src="js/mg_public.js"></script> 
	<script src="assets/js/toastr/toastr.min.js"></script> 
    
	<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
	<!--[if lt IE 9]>
		<script src="assets/js/html5shiv.min.js"></script>
		<script src="assets/js/respond.min.js"></script>
	<![endif]-->
	<!--复制到粘贴板-->
    <script src="js/ZeroClipboard/ZeroClipboard.js"></script>
    <script src="js/DeviceOper.js"></script>
    <link href="js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" /> 
    <script src="js/lib/ligerUI/js/core/base.js"></script> 
    <script src="js/lib/ligerUI/js/plugins/ligerComboBox.js"></script> 
    <script src="js/lib/ligerUI/js/plugins/ligerTree.js"></script>

    <script src="js/main.js"></script>
    <script src="js/mainzTree.js"></script>
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
     <input type="hidden" value="<%=Utils.GetSession("UserInfo").UserID %>" id="userid" /> 
    <input type="hidden" value ="<%= Utils.GetSession("UserInfo").UserName  %>" id="userName" />
     <input type="hidden" value ="<%=imei %>" id="txtImei" />
    <input type="hidden" value="<%=userid %>" id="txtAppointUserID"/>
    <input type="hidden" value="<%= Utils.GetSession("UserInfo").LoginType %>" id="loginType" />
    <input type="hidden" value="<%= Utils.GetSession("UserInfo").DeviceID %>" id="LoginImeiDeviceID" />
    <input type="hidden" value="<%= Utils.GetSession().SuperAdmin %>" id="isSuperAdmin" />

	<div class="page-container" style="background-color:#FFFFFF;min-width:810px;"><!-- add class "sidebar-collapsed" to close sidebar by default, "chat-visible" to make chat appear always -->
	 
	 		<div class="main-content" id="divMainContent">
             
			<!-- User Info, Notifications and Menu Bar -->
			<nav class="navbar user-info-navbar horizontal-menu navbar-fixed-top " role="navigation" > 
             
				<!-- Right links for user info navbar -->
				<ul class="user-info-menu left-links list-inline list-unstyled" style="">
                        <!-- Navbar Brand -->
			         <div class="abc-header">
				        <a href="javascript:void(0)" class="abc-header-img">
					        <img src="assets/images/<%= MG_BLL.Common.lib.Config.CurrentHost %>.png" width="240" alt="" class="hidden-xs" />					        
				        </a>				       
			         </div> 
                     
                     
					<li onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)"  >
						<a href="javascript:;" data-toggle="chat" id="aMenu" title="">
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
						    <span class="title" style="color:white;" id="spanMonitor">
                                <script type="text/javascript">
                                    writePage(allPage.monitor);
                                </script>
						    </span>
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
                    <%if (Utils.GetSession("UserInfo").LoginType == "0")
                        {
                          %> 
                     <li class="opened active" onmousemove="_onmousemove(this)" onmouseout="_onmouseout(this)" style="font-size:18px;font-weight:bold;" title="用户管理">
					    <a href="Devices.aspx">
						    <i class="fa-car" style="color:white;"></i>
						    <span class="title" style="color:white;" id="spanManDevice"><script type="text/javascript">
						                                                                    writePage(allPage.manUser);
                                            </script> </span>
					    </a>
                    </li>
                     <li class="opened active" onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)" style="font-size:18px;font-weight:bold;" title="电子围栏">
					    <a target="_blank" href="Geofences.aspx">
						    <i class="fa-bar-chart" style="color:white;"></i>
						    <span class="title" style="color:white;" id="spanStatisticalAnalysis"><script type="text/javascript">
						                                                                              writePage(mapPage.geofence);
                                            </script></span>
					    </a> 
                    </li>  <% } %>
                     <!--  用户信息 -->
					<li class="dropdown user-profile hover-line"  onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)" style="float:right;margin-right:0px;   ">
						<a javascript:;" data-toggle="dropdown">
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
					 
                           <%
                               if (Utils.GetSession().SuperAdmin == "1")
                               {%>
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
                    
                 <%if (Utils.GetSession("UserInfo").LoginType == "0")
                     {
                          %> 
                    <!-- 报警信息 -->
                     <li class="dropdown hover-line" style="float:right;"  onmousemove="_onmousemove(this)"  onmouseout="_onmouseout(this)">
						<a href="#" data-toggle="dropdown">
							<i class="fa-bell-o" style="color:white;"></i>
							<span class="badge badge-purple" id="spanAlarmCount" style="font-size:10px; ">0</span> 
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
                                    <% if (Utils.GetSession("UserInfo").UserType == "2")
                                        {
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
                 <%} %>

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
                    <li class="opened active" style="float: right; margin-top: 23px;">
                        <div class="input-group search-input" style="width: 250px;">
                            <input type="text" id="txtSearchText" class="form-control no-left-border form-focus-red" placeholder=" " value="<%=searchText %>" />
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-red dropdown-toggle" data-toggle="dropdown" onclick="javascript:SearchBox('txtSearchText',0);">
                                    <script type="text/javascript">
                                        writePage(homePage.quickSearch);
                                    </script>
                                    <%--<span class="caret"></span>--%>
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
                    
				 
				 
				</ul> 
			</nav>   
                 
           <div id="allmap" class="baidu-map"></div>  
                 
	 </div>
		
	 <% if (Utils.GetSession("UserInfo").LoginType == "0")
         { %>
		<!-- start: Chat Section  class="scrollable" -->
        <div id="chat" >
             <div id="dragChat" style="background-color:#A8A8A8;width:2px;height:100%;float:right;margin-right:-2px;"></div> 
            <div class="chat-inner ps-container" style="width: 100%; overflow: no-display;">
                <h2 class="chat-header">
                    <a href="#" class="chat-close" data-toggle="chat" id="aChatClose">
                        <i class="fa-plus-circle rotate-45deg"></i>
                    </a>
                    <script type="text/javascript">writePage(homePage.customerList)
                    </script>
                    <span class="badge badge-success is-hidden"></span>
                </h2>
                <div class="chat-group" style="max-height: 250px;width:100%">
                    <div style="max-height: 250px; overflow-x: hidden; overflow-y: scroll;">
                        <ul id="treeDemo" class="ztree" style=""></ul>
                    </div>
                </div>
                <div class="form-group"style="width: 100%;">
                    <script type="text/javascript">
                        jQuery(document).ready(function ($) {
                            dragChat("dragChat");
                            $("#s2example-1").select2({
                                placeholder: allPage.search1,
                                allowClear: true
                            }).on('select2-open', function () {
                                // Adding Custom Scrollbar
                                $(this).data('select2').results.addClass('overflow-hidden').perfectScrollbar();
                            }).change(function () {
                                $("#a_device_" + $(this).val()).trigger("click");
                            });
                        });
                    </script>
                    <select class="form-control" id="s2example-1">
                        <option></option>
                    </select>
                </div>
                <div class="form-group" style="width: 100%; text-align: center;">
                    <div class="btn-group" style="width: 100%; text-align: center;">
                        <button type="button" class="btn btn-white btn-xs" name="btnDeviceStatus" id="btnDeviceAll" style="height: 30px; width: 25%;" onclick="menuClick(1,this)">
                            <script type="text/javascript">writePage(allPage.all)
                            </script>
                            (0)</button>
                        <button type="button" class="btn btn-white btn-xs" name="btnDeviceStatus" id="btnDeviceOnline" style="height: 30px; width: 25%;" onclick="menuClick(2,this)">
                            <script type="text/javascript">writePage(allPage.online)
                            </script>
                            (0)</button>
                        <button type="button" class="btn btn-white btn-xs" name="btnDeviceStatus" id="btnDeviceOffline" style="height: 30px; width: 25%;" onclick="menuClick(3,this)">
                            <script type="text/javascript">writePage(allPage.offline)
                            </script>
                            (0)</button>
                        <button type="button" class="btn btn-white btn-xs" name="btnDeviceStatus" id="btnDeviceNotActive" style="height: 30px; width: 25%;" onclick="menuClick(4,this)">
                            <script type="text/javascript">writePage(allPage.status1)
                            </script>
                            (0)</button>
                    </div>
                </div>

                <div class="chat-group" style="margin-top: -10px; text-align: right; border-bottom: 1px solid #0C56AF">
                    <button type="button" class="btn btn-orange btn-xs" style="margin-left: 0px;" onclick="AddDeviceGps()">新装车辆</button>
                    <!--  添加分组  -->
                    <input type="text" style="width: 120px; margin-top: -15px; margin-left: 2px; display: none;" id="txtGroupName" />
                    <button class="btn btn-success btn-xs" style="display: none;" onclick="btnGroup(2)" id="btnGroupSubmit">
                        <script type="text/javascript">
                            writePage(allPage.confirm);
                        </script>
                    </button>
                    <button class="btn btn-primary btn-xs" style="display: none;" id="btnGroupClose" onclick="btnGroup(3)">
                        <script type="text/javascript">
                            writePage(allPage.cancel);
                        </script>
                    </button>
                    <button class="btn btn-orange btn-xs" id="btnAddGroup" onclick="btnGroup(1)">
                        <script type="text/javascript">
                            writePage(mapPage.addGroup);
                        </script>
                    </button>
                    <!--  添加分组结束  -->
                    <label style="display: none;">
                        <input type="checkbox" id="chkLowerDevice" value="" />下属设备
                    </label>
                </div>

                <div class="chat-group " style="margin-top: 0px; margin-bottom: 0px; padding-top: 0px; padding-left: 0px; padding-bottom: 0px; padding-right: 0px; bottom: 0px;">
                    <div class="chat-group" style="height: 100%; margin-top: 0px; overflow: auto;" id="devicesDIV"></div>
                </div>
            </div>


        </div>
		<!-- end: Chat Section  scrollable-->
      <%   }%>
    
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
					<button type="button" class="btn btn-info" data-dismiss="modal" onclick="javascript:window.location.href='login.aspx'">确定</button>
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
								    <input type="text" class="form-control" id="txtUserName" placeholder="name" />
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
								    <label for="txtContacts" class="control-label"><script type="text/javascript">
								                                                       writePage(allPage.cellName);
								                                               </script></label> 
								    <input type="text" class="form-control" id="txtContacts" name="name" placeholder="Contacts" data-validate="required" data-message-required="联系人不能为空." />
							    </div>	
							
						    </div>
						
						    <div class="col-md-4"> 
							    <div class="form-group">
								    <label for="txtPhone" class="control-label">
                                        <script type="text/javascript">
                                            writePage(allPage.phone);
								        </script></label> 
								    <input type="text" class="form-control" maxlength= "12" id="txtPhone" name="min_field" data-validate="required" placeholder="Phone" style="width:calc(100% + 20px)"/><%--,number,minlength[6]--%>
							    </div>	 

						    </div>
                           <%--<div class="col-md-1">
							    <br /><br />
                                <a style=" white-space:nowrap;" href="#">绑定</a> 
						    </div>--%>
						
						    <div class="col-md-4"> 
							    <div class="form-group">
								    <label for="txtEmail" class="control-label"><script type="text/javascript">
								                                                    writePage(allPage.primaryEmail);
								                                               </script></label> 
								    <input type="text" class="form-control"  id="txtEmail" placeholder="E-Mail" data-mask="email" name="email" data-message-email="请输入正确的邮箱" data-validate="email"/>
							    </div>	 
						    </div>
					    </div>
					    <div class="row">
						    <div class="col-md-12">
							
							    <div class="form-group">
								    <label for="txtAddress" class="control-label"><script type="text/javascript">
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
    <div id="divMore" style="display:none;width:120px;height:auto;left:0px;top:0px;position:absolute;background-color:white;z-index:50002;" onmouseenter="ulMouseenter(this)" onmouseleave="ulMouseleave(this)" ></div>
    <ul id="ulGroup" onmouseenter="liGroupMouseleave(this)" style="display:none;left:0px;top:0px;position:absolute;background-color:#DBEAF9;z-index:50013;" ></ul>
    
    <link href="zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" /> 
    <script src="zTree_v3/js/jquery.ztree.core-3.5.js"></script>  
	<script type="text/javascript">

	    // 百度地图API功能
	    var map = new BMap.Map("allmap", { enableMapClick: false });    // 创建Map实例
	    map.centerAndZoom(new BMap.Point(116.404, 39.915), 8);  // 初始化地图,设置中心点坐标和地图级别
	    // map.addControl(new BMap.MapTypeControl());   //添加地图类型控件

	    map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
	    map.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP], offset: new BMap.Size(100, 10) }));  //添加 右上角混合 地图
	    map.addControl(new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT }));// 左下角，添加比例尺
	    map.enableKeyboard();//启用键盘上下左右键移动地图
	    var ctrl = new BMapLib.TrafficControl({
	        showPanel: true  //是否显示路况提示面板 
	    });
	    map.addControl(ctrl);
	    ctrl.setAnchor(BMAP_ANCHOR_TOP_RIGHT);
	    var navigationControl = new BMap.NavigationControl({
	        // 靠左上角位置
	        anchor: BMAP_ANCHOR_TOP_LEFT,
	        // LARGE类型
	        type: BMAP_NAVIGATION_CONTROL_LARGE,
	        // 启用显示定位
	        enableGeolocation: true,
	        showZoomInfo: true
	    });
	    map.addControl(navigationControl);
	    //ctrl.show();
	    var geoc = new BMap.Geocoder();
        <% if (Utils.GetSession("UserInfo").LoginType == "0")
        { %>
	    function myFun(result) {
	        var cityName = result.name;
	        map.setCenter(cityName);
	    }
	    var myCity = new BMap.LocalCity();  //根据ip获取所在城市
	    myCity.get(myFun);
	    <% } %>

	    var curMenu = null, zTree_Menu = null, zTree = null;
	    var offLineMi = 20; //多长时间算离线
	    $(function () {

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
	            callback: {
	                beforeClick: beforeClick
	            }
	        };


            <% if (Utils.GetSession("UserInfo").LoginType == "0")
        { %>
	        var zNodes = new Array();
	        $.ajax({
	            url: "AjaxService/AjaxService.ashx?action=getTree",
	            type: 'POST',
	            dataType: 'json',
	            data: {},
	            async: false,
	            success: function (dataList) {
	                var i = 0;
	                $(dataList[""]).each(function (k, v) {
	                    zNodes[i] = { id: v["UserID"], pId: v["ParentID"], name: v["UserName"], icon: (v.UserType == 1 ? "js/lib/ligerUI/skins/icons/memeber.gif" : "js/lib/ligerUI/skins/icons/customers.gif") };
	                    i++;
	                });
	                var treeObj = $("#treeDemo");
	                zTree = $.fn.zTree.init(treeObj, setting, zNodes);
	                zTree_Menu = $.fn.zTree.getZTreeObj("treeDemo");

	                //if (curMenu != undefined && zTree_Menu.getNodes().length > 0) {
	                //    if (zTree_Menu.getNodes()[0].children != undefined && zTree_Menu.getNodes()[0].children[0] != null)
	                //        curMenu = zTree_Menu.getNodes()[0]//.children[0];   //默认展开到第几级  在后面加.children[0] 
	                //} 
	                treeObj.addClass("showIcon");
	                if ($("#txtAppointUserID").val() != "") {
	                    curMenu = zTree_Menu.getNodeByParam("id", $("#txtAppointUserID").val(), null);
	                    zTree_Menu.selectNode(curMenu);
	                    //setting.callback.beforeClick = beforeClick; 
	                    beforeClick($("#txtAppointUserID").val(), curMenu);
	                    $("#txtAppointUserID").val("");
	                } else {
	                    curMenu = zTree_Menu.getNodes()[0];
	                    zTree_Menu.selectNode(curMenu);
	                    zTree_Menu.expandNode(curMenu);
	                }
	            }
	        });
	        <%} %>

	        show_loading_bar(100);
	        map.enableDragging();   //开启拖拽
	        map.enableInertialDragging();   //开启惯性拖拽

	        <% if (Utils.GetSession("UserInfo").LoginType == "0")
        { %>
	        RefreshTime();      //开启自动刷新
	        <%}
        else
        {%>
	        RefreshTime('<%= Utils.GetSession("UserInfo").LoginType %>', '<%= Utils.GetSession("UserInfo").SerialNumber %>');
	        <%}%>

	        <%if (Utils.GetSession("UserInfo").LoginType == "0")
        {%>
	        // getAlarmList($("#userid").val(), false);
	        alarmTime($("#userid").val());
	        <%}%>

	        DeviceListDidHeight(0);

	        $("#txtSearchText").attr("placeholder", allPage.imeiNo + "/" + dealerPage.username + "/" + allPage.devicePhone);
	        $("#txtSearchText").attr("title", allPage.deviceNo + "/" + dealerPage.username + "/" + allPage.devicePhone);
	        var chatWidth = $("#chat").width();
	        $("#aMenu").on("click", function () {
	            if ($("#chat").is(":hidden")) {
	                $("#allmap").width($("#allmap").width() - chatWidth);
	                $("#iMenu").attr("class", "fa-angle-double-left");
	            }
	            else {
	                $("#allmap").width($("#allmap").width() + chatWidth);
	                $("#iMenu").attr("class", "fa-angle-double-right");
	            }
	        });
	        $("#chkLowerMsg").on("click", function () {
	            alarmTime($("#userid").val());
	        });
	        $("#aChatClose").on("click", function () {
	            if ($("#chat").is(":hidden")) {
	                $("#allmap").width($("#allmap").width() - chatWidth);
	                $("#iMenu").attr("class", "fa-angle-double-left");
	            }
	            else {
	                $("#allmap").width($("#allmap").width() + chatWidth);
	                $("#iMenu").attr("class", "fa-angle-double-right");
	            }
	        });

	    });

	    var currentLineStatus = 2;
	    var isFlag = false;
	    function addDeviceList(data, isPostBack) {
	        if (isPostBack != "0") {
	            addDeviceLi(data, isPostBack);
	                <% if (Utils.GetSession("UserInfo").LoginType == "0")
        { %>
	                GetGroup();
	                <%} %>
	            } else if (isPostBack == "0") { //自动刷新 
	                var deviceLenth = $("a[line]").length; //当前页面有多少台
	                var dataLength = data[""].length; // 返回多少台
	                if (deviceLenth != dataLength) {
	                    $("#devicesDIV").html("");
	                    addDeviceLi(data, isPostBack);
	                } else {
	                    refreshDeviceLi(data);
	                }
	            }
                if (isFlag) {
                    menuClick(currentLineStatus, 2);
                }
                $("#btnDeviceAll").text(allPage.all + "(" + $("a[line]").length + ")");
                $("#btnDeviceOnline").text(allPage.online + "(" + $("a[line=online]").length + ")");
                $("#btnDeviceOffline").text(allPage.offline + "(" + $("a[line=offline]").length + ")");
                $("#btnDeviceNotActive").text(allPage.status1 + "(" + $("a[line=notactive]").length + ")");

                addMarker(data[""], isPostBack);
            }
            var CurrentSelectDevice = null;
            var isAutoSetZoom = true;
            var z_index = 0;
            // 编写自定义函数,创建地标注
            function addMarker(data, isPostBack) {
                //创建标注前，先把地图上的标注移除 
                //map.clearOverlays();

                //var i = 0;

                var markerArray = [];

                if (isPostBack != 0) {
                    map.clearOverlays();
                }
                var addMarkerToMap = function (v) {

                    if (isPostBack == 0) {
                        var currentDeviceImei = 0;
                        if (CurrentSelectDevice && CurrentSelectDevice != 0) {
                            currentDeviceImei = CurrentSelectDevice.split("_")[2];
                        }
                        if (v.Speed > 1 || v.SerialNumber == currentDeviceImei) {
                            removeMarker(v.DeviceID);
                        }
                        else {
                            return;
                        }
                    }
                    if (v["BaiduLng"] != "" && v["BaiduLat"] != "" && v["BaiduLng"] > 0 && v["BaiduLat"] > 0) {

                        var pt = new BMap.Point(v["BaiduLng"], v["BaiduLat"]);
                        var line = "Online";
                        if (v["status"] != "" && parseInt(v["status"]) > v.offLineMi) {
                            line = "Offline";
                        }
                        var myIcon = GetBaiduIcon(v["Icon"], line, v["Course"]); //new BMap.Icon("icons/carIcon/27.gif", new BMap.Size(20, 24));

                        var marker = new BMap.Marker(pt, { icon: myIcon });  // 创建标注 
                        var opts = {
                            position: pt,    // 指定文本标注所在的地理位置
                            offset: new BMap.Size(20, -10)    //设置文本偏移量
                        }
                        var deviceName = v["DeviceName"];
                        if (deviceName == "") {
                            v["DeviceName"] = v["SerialNumber"];
                            deviceName = v["SerialNumber"];
                        }
                        // 显示设备上方设备名称的覆盖物
                        function ComplexCustomOverlay(point, text, mouseoverText) {
                            this._point = point;
                            this._text = text;
                            this._overText = mouseoverText;
                        }
                        ComplexCustomOverlay.prototype = new BMap.Overlay();
                        ComplexCustomOverlay.prototype.initialize = function (z_map) {
                            this._map = z_map;
                            var div = this._div = document.createElement("div");
                            div.style.position = "absolute";
                            div.style.zIndex = BMap.Overlay.getZIndex(this._point.lat);
                            div.style.backgroundColor = "#EE5D5B";
                            div.style.border = "1px solid #BC3B3A";
                            div.style.color = "white";
                            div.style.height = "25px";
                            div.style.padding = "2px";
                            div.style.lineHeight = "18px";
                            div.style.whiteSpace = "nowrap";
                            div.style.MozUserSelect = "none";
                            div.style.fontSize = "12px"
                            var span = this._span = document.createElement("span");
                            div.appendChild(span);
                            span.appendChild(document.createTextNode(this._text));
                            var that = this;

                            var arrow = this._arrow = document.createElement("div");
                            arrow.style.background = "url(icons/label.png) no-repeat";
                            arrow.style.position = "absolute";
                            arrow.style.width = "11px";
                            arrow.style.height = "10px";
                            arrow.style.top = "24px";
                            arrow.style.left = "10px";
                            arrow.style.overflow = "hidden";
                            div.appendChild(arrow);

                            div.onmouseover = function () {
                                this.style.backgroundColor = "#6BADCA";
                                this.style.borderColor = "#0000ff";
                                z_index = this.style.zIndex;
                                this.style.zIndex = 5000;
                                this.getElementsByTagName("span")[0].innerHTML = that._overText;
                                arrow.style.backgroundPosition = "0px -10px";
                            }

                            div.onmouseout = function () {
                                this.style.backgroundColor = "#EE5D5B";
                                this.style.borderColor = "#BC3B3A";
                                this.style.zIndex = z_index;
                                this.getElementsByTagName("span")[0].innerHTML = that._text;
                                arrow.style.backgroundPosition = "0px 0px";
                            }

                            map.getPanes().labelPane.appendChild(div);

                            return div;
                        }
                        ComplexCustomOverlay.prototype.draw = function () {
                            var z_map = this._map;
                            var pixel = z_map.pointToOverlayPixel(this._point);
                            this._div.style.left = pixel.x - parseInt(this._arrow.style.left) + "px";
                            this._div.style.top = pixel.y - 40 + "px";
                        }
                        var myCompOverlay = new ComplexCustomOverlay(pt, deviceName, deviceName);
                        myCompOverlay.DeviceID = v.DeviceID;
                        map.addOverlay(myCompOverlay);
                        marker.DeviceID = v.DeviceID;
                        map.addOverlay(marker);
                        marker.addEventListener("click", function (e) {
                            $("#a_device_" + v["SerialNumber"]).trigger("click");
                        });
                    }

                    $("#a_device_" + v["SerialNumber"]).unbind("click").click(function () {
                        deviceListOnClick(this, v);
                    });
                    if (CurrentSelectDevice != null && CurrentSelectDevice == "a_device_" + v["SerialNumber"]) {
                        isAutoSetZoom = false;
                        $("#a_device_" + v["SerialNumber"]).triggerHandler("click");
                    }
	                <%if (Utils.GetSession("UserInfo").LoginType == "1")
                    {%>
	                     deviceListOnClick(null, v);
	                <%}%> 
	            }
	            function removeMarker(DeviceID) {
	                var allOverlay = map.getOverlays();
	                for (var i = 0; i < allOverlay.length; i++) {
	                    if (allOverlay[i].DeviceID == DeviceID) {
	                        map.removeOverlay(allOverlay[i]);
	                    }
	                }
	            }
	            var timeChunk = function (arr, fn, count) {
	                var obj, t;
	                // var len = arr.length; 
	                var start = function () {
	                    for (var i = 0; i < Math.min(count || 1, arr.length) ; i++) {
	                        var obj = arr.shift();
	                        fn(obj)
	                    }
	                };
	                return function () {
	                    t = setInterval(function () {
	                        if (arr.length === 0) {
	                            if ($("#txtImei").val() != "") {
	                                $("#a_device_" + $("#txtImei").val()).trigger("click");
	                                $("#txtImei").val("");
	                            }
	                            return clearInterval(t);
	                        }
	                        start()
	                    }, 200)
	                }
	            }
	            var renderFriendList = timeChunk(data, addMarkerToMap, 100)
	            renderFriendList();

	        }
	        var temp = "";
	        var showMarker = null;
	        var zhudian = "";
	        function deviceListOnClick(obj, v) {
	            var id, groupid = 0;
	            if (zTree_Menu == null) {
	                id = $("#userid").val();
	            } else {
	                id = zTree_Menu.getSelectedNodes()[0].id;
	                groupid = obj.name.split('_')[1];
	            }
	            if ($("[name=snumber_" + v["SerialNumber"] + "]") != null && $("[name=snumber_" + v["SerialNumber"] + "]").length <= 0) {
	                if (temp != "") {
	                    $("em[name=snumber_" + temp + "]").parent().css("background-color", "#FFFFFF");
	                    $("em[name=snumber_" + temp + "]").prev().remove();
	                    $("em[name=snumber_" + temp + "]").remove();
	                }
	                temp = v["SerialNumber"];
	                if (obj != null) {
	                    $(obj).css("background-color", "#DBEAF9").show(100).append("</br><em id='more_" + obj.name + "' name='snumber_" + v["SerialNumber"] + "' style = 'white-space:nowrap;'></br>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" +
                             "<a href=\"Tracking.aspx?id=" + id + "&deviceid=" + v["DeviceID"] + "&t=" + new Date().getTime() + "\" target=\"_blank\">" + allPage.tracking + "</a>" +
                             "<a href=\"PlayBack.aspx?id=" + id + "&deviceid=" + v["DeviceID"] + "&t=" + new Date().getTime() + "\" target=\"_blank\">&nbsp&nbsp" + allPage.playback + "</a>" +
                             "<a name=\"aMore\" onclick=\"aMoreClick(this)\" DeviceID=\"" + v["DeviceID"] + "\" model='" + v["Model"] + "' modelName=\"" + v["DataText"] + "\" groupid=\"" + groupid + "\"  href=\"javascript:;\">&nbsp&nbsp" + allPage.more + "▼</a> </em> ");
	                    CurrentSelectDevice = $(obj).attr("id");
	                }
	            }

	            if (v["BaiduLng"] == "" || v["BaiduLat"] == "") {
	                toastr.warning(v["SerialNumber"] + "," + allPage.status1 + "！", allPage.toastrTitle2, opts_waming);
	                return;
	            }

	            var point = new BMap.Point(v["BaiduLng"], v["BaiduLat"]);
	            if (isAutoSetZoom) {
	                map.centerAndZoom(point, 18);
	            }
	            isAutoSetZoom = true;
	            var datacontext = v["DataContext"].split('-');
	            var Context = "";
	            switch (v["DataText"]) {
	                case "MG-X80'":
	                case "MG-X83":
	                case "MG-X81":
	                case "MG-X82":
	                case "MG-X83BF":
	                case "MG-X83W":
	                case "MG-X83BA":
	                case "MG-X83B":
	                    if (datacontext[4] != undefined && datacontext[4] != null && datacontext[4] != "")
	                        Context += yiwen201405.battery + "：" + datacontext[4] + "%";
	                    break;
	                case "MG-X83A":
	                    if (datacontext[4]) {
	                        var blue = "正常";
	                        var son = "连接";
	                        if (datacontext[2] != "1") {
	                            blue = "<font color='red'>关闭</font>";
	                        }
	                        if (datacontext[3] != "1") {
	                            son = "<font color='red'>断开</font>";
	                        }
	                        Context = "电池电量:" + datacontext[4] + "%" + ",蓝牙:" + blue + ",子机:" + son;
	                    }
	                    break;
	                case "MG-X11D":
	                    if (datacontext.length >= 4) {
	                        if (datacontext[1] == 0) {
	                            Context += mapPage.dismiss + "&nbsp&nbsp";
	                        } else if (datacontext[1] == 1) {
	                            Context += mapPage.fortify + "&nbsp&nbsp";
	                        }
	                        if (datacontext[2] == 0) {
	                            Context += "未刹车&nbsp&nbsp";
	                        } else if (datacontext[2] == 1) {
	                            Context += "已刹车&nbsp&nbsp";
	                        }
	                        if (datacontext[3] == "1") {
	                            zhudian = mapPage.zdlj + "&nbsp&nbsp";
	                        } else if (datacontext[3] == "0") {
	                            var date = DateDiffMi(v["StopStartUtcDate"], GetCurrentDate());
	                            var mi = MinuteToHour(date);
	                            zhudian = "<font style=\"color:red;\">" + mapPage.zddk + "</font>" + mi + "&nbsp&nbsp";
	                        }
	                        var DeviceID = v["DeviceID"];
	                        var SpanID = 'spanFence' + DeviceID;

	                        Context += "<span id='" + SpanID + "'></span>&nbsp&nbsp";

	                        Context += zhudian;
	                    }
	                    break; 
	                default:
	                    if (datacontext.length >= 4) {
	                        if (datacontext[0] == 0)
	                            Context += "ACC" + allPage.acc0 + "&nbsp&nbsp";
	                        else if (datacontext[0] == 1)
	                            Context += "ACC" + allPage.acc1 + "&nbsp&nbsp";
	                        if (datacontext[1] == 0) {
	                            Context += mapPage.dismiss + "&nbsp&nbsp";
	                        } else if (datacontext[1] == 1) {
	                            Context += mapPage.fortify + "&nbsp&nbsp";
	                        }
	                        if (datacontext[3] == "1") {
	                            zhudian = mapPage.zdlj + "&nbsp&nbsp";
	                        } else if (datacontext[3] == "0") {
	                            var date = DateDiffMi(v["StopStartUtcDate"], GetCurrentDate());
	                            var mi = MinuteToHour(date);
	                            zhudian = "<font style=\"color:red;\">" + mapPage.zddk + "</font>" + mi + "&nbsp&nbsp";
	                        }
	                        if (v.DataText == "MG-X21BZ" && datacontext[9]) {
	                            zhudian += "温度" + datacontext[9]+"°";
	                        }
	                        Context += zhudian;
	                    }
	                    break;
	            }
	            var pt = new BMap.Point(v["BaiduLng"], v["BaiduLat"]);
	            var online = null;
	            var showText = [];
	            if (v["status"] != "" && parseInt(v["status"]) > v.offLineMi) {
	                online = allPage.offline;
	                showText.push("<div style=\" padding-top:5px;\"><font name=\"CurrentDeviceName\" style=\"font-weight:bold;font-size:13px;color:#4F4F4F;\">&nbsp&nbsp" + v["DeviceName"] + " (" + online + ")" + "</font></div>");
	            }
	            if (v["status"] != "" && parseInt(v["status"]) < v.offLineMi) {
	                online = allPage.online;
	                showText.push("<div style=\" padding-top:5px;\"><font name=\"CurrentDeviceName\" style=\"font-weight:bold;font-size:13px;color:#16722A;\">&nbsp&nbsp" + v["DeviceName"] + " (" + online + ")" + "</font></div>");
	            }

	            showText.push("<p style=\"background-color:red;height:1px;width:100%;\"></p>");
	            var hire = DateDiff(GetCurrentDate(), v.HireExpireDate);
	            var imei = v["SerialNumber"];
	            if (hire <= 30 && hire > 0) {  //过期设备显示
	                imei += "(<font style=\"color:red;\">" + parseInt(hire) + "天后过期</font>)";
	            } else if (hire <= 0) {
	                imei += "(<font style=\"color:red;\">已过期" + +parseInt(Math.abs(hire)) + "天</font>)";
	            }
	            showText.push("&nbsp&nbsp" + allPage.imeiNo + "：" + imei + "  &nbsp&nbsp");

	            var stoptime = "";
	            //"方向:" + GetCoureName(v["Course"])  +
	            showText.push(stoptime + "</br>&nbsp&nbsp")
	            if (Context != "") {
	                showText.push(Context + "</br>&nbsp&nbsp");
	            }
	            if (online == allPage.offline) {
	                showText.push(mapPage.LastOnlineTime + "：" + v["LastCommunication"] + "</br>&nbsp&nbsp");
	                showText.push(allPage.positionTime + "：" + v["DeviceUtcDate"] + "</br>&nbsp&nbsp");
	            }
	            else {
	                showText.push(allPage.positionTime + "：" + "<font style=\"color:blue;\">" + v["DeviceUtcDate"] + "</br>&nbsp&nbsp" + "</font>");
	            }
	            if (v["Speed"] > 7) {
	                showText.push(allPage.state + ":" + allPage.moving + "" + "，" + allPage.speed + ":" + v["Speed"] + allPage.speedKM);
	            } else {
	                if (online == allPage.offline) {
	                    showText.push(allPage.state + ":" + allPage.offline + "-" + MinuteToHour(v["OfflineTime"]));
	                } else {
	                    showText.push(allPage.state + ":" + allPage.stopCar);
	                    if (v["StopTime"] > 2) {
	                        showText.push("," + allPage.stopTime + "：" + MinuteToHour(v["StopTime"]));
	                    }
	                }
	            }

	            if (v["DataType"] != "") {
	                showText.push("</br>&nbsp&nbsp" + allPage.positionType + "：" + (v["DataType"] == 2 ? "LBS" : "BDS/GPS"));
	            }

	            showText.push("<br>&nbsp&nbsp" + allPage.LatLng + "：" + parseFloat(v["BaiduLat"]).toFixed(5) + "," + parseFloat(v["BaiduLng"]).toFixed(5));
	            showText.push("</br>&nbsp&nbsp" + allPage.address + "：<a href=\"javascript:void(0)\" title=\"点击复制地址\" id=\"Copy_A\" data-clipboard-target=\"Copy_A\"></a> ");
	            showText.push("<div style=\"position:absolute;bottom:0px;font-size:12px;font-weight:bold;color:red;\">");
	            showText.push("<a href=\"Tracking.aspx?id=" + id + "&deviceid=" + v["DeviceID"] + "&t=" + new Date().getTime() + "\" target=\"_blank\">" + allPage.tracking + "-" + mapPage.streetView + "&nbsp</a> ");
	            showText.push("<a href=\"PlayBack.aspx?id=" + id + "&deviceid=" + v["DeviceID"] + "&t=" + new Date().getTime() + "\" target=\"_blank\">" + allPage.playback + "&nbsp</a> ");
	            <%if (Utils.GetSession("UserInfo").LoginType == "0")
        {%>
	            showText.push("<a href=\"Geofences.aspx?id=" + id + "&deviceid=" + v["DeviceID"] + "&t=" + new Date().getTime() + "\" target=\"_blank\">" + mapPage.geofence + "&nbsp</a>");
	            <%}%>
	            showText.push("<a href=\"#\" name=\"aMore\" onclick=\"aMoreClick(this)\" DeviceID=\"" + v["DeviceID"] + "\"  model='" + v["Model"] + "' modelName=\"" + v["DataText"] + "\" groupid=\"" + groupid + "\" href=\"javascript:\">" + allPage.more + "▼</a> ");
	            showText.push("</div>");


	            var myCompOverlay = new ComplexCustomOverlay(point, showText.join(''), showText.join(''));
	            if (showMarker != null) {
	                map.removeOverlay(showMarker);
	            }
	            showMarker = myCompOverlay;
	            map.addOverlay(myCompOverlay);
	            var clip = new ZeroClipboard(document.getElementById("Copy_A"), {
	                moviePath: "js/ZeroClipboard/ZeroClipboard.swf"
	            });

	            // 复制内容到剪贴板成功后的操作
	            clip.on('complete', function (client, args) {
	                alert("复制成功，复制内容为：" + args.text);
	            });
	            IsExistFence(DeviceID);
	            GetAddressByLatlng(point.lat, point.lng, "Copy_A");
	            $("#aTracking").attr("href", "Tracking.aspx?id=" + id + "&deviceid=" + v["DeviceID"] + "&t=" + new Date().getTime());
	            $("#aGeofences").attr("href", "Geofences.aspx?id=" + id + "&deviceid=" + v["DeviceID"] + "&t=" + new Date().getTime());
	            $("#aPalyBack").attr("href", "PlayBack.aspx?id=" + id + "&deviceid=" + v["DeviceID"] + "&t=" + new Date().getTime());
	        }

	        function IsExistFence(deviceid) {

	            $.ajax({
	                url: "/AjaxService/AjaxService.ashx?action=IsExistFence",
	                data: { deviceid: deviceid },
	                type: "POST",
	                dataType: "json",
	                success: function (res) {
	                    if (res.success) {
	                        $("#spanFence" + deviceid).text("围栏" + res.msg + "米");
	                    } else {
	                        $("#spanFence" + deviceid).text("围栏关");
	                    }
	                }
	            });
	        }
	        // 定义一个控件类,即function
	        function ZoomControl() {
	            // 默认停靠位置和偏移量
	            this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
	            this.defaultOffset = new BMap.Size(200, 20); //(x,y)
	        }

	        // 通过JavaScript的prototype属性继承于BMap.Control
	        ZoomControl.prototype = new BMap.Control();

	        // 自定义控件必须实现自己的initialize方法,并且将控件的DOM元素返回
	        // 在本方法中创建个div元素作为控件的容器,并将其添加到地图容器中
	        ZoomControl.prototype.initialize = function (map) {
	            // 创建一个DOM元素
	            var div = document.createElement("div");
	            div.id = "div_refreshTime";
	            // 添加文字说明
	            div.appendChild(document.createTextNode("2" + trackingPage.secondMsg));
	            //div.innerText = "";
	            // 设置样式
	            div.style.width = "200px";
	            div.style.cursor = "pointer";
	            div.style.border = "1px solid gray";
	            div.style.backgroundColor = "white";
	            // 添加DOM元素到地图中
	            // var div = "<div id='div_refreshTime' style='width:aotu;cursor:pointer;background-color:white;border:1px solid gray'> </div>"; 
	            map.getContainer().appendChild(div);
	            // 将DOM元素返回 
	            return div;  //$(div).get(0);
	        }
	        // 创建控件
	        var myZoomCtrl = new ZoomControl();
	        // 添加到地图当中
	        map.addControl(myZoomCtrl);

    </script>
    <style type="text/css">
        .spanSize {
            width: 70px;
            font-size: 9px;
            margin-right: -30px;
            font-family: "Microsoft Yahei",Verdana,Simsun,"Segoe UI Web Light","Segoe UI Light","Segoe UI Web Regular","Segoe UI","Segoe UI Symbol","Helvetica Neue",Arial;
        }
    </style>
    <link href="zTree_v3/css/zTree.css" rel="stylesheet" />

    <script src="js/fun.js"></script>
     <script src="js/CoureName.js"></script>
	 
    <!-- 引用cookie-->
    <script src="js/jquery.cookie.js"></script>

 	<!-- Imported styles on this page -->
	
	<link rel="stylesheet" href="assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="assets/js/select2/select2-bootstrap.css"/>
	<link rel="stylesheet" href="assets/js/multiselect/css/multi-select.css"/>

	<!-- Bottom Scripts -->
	<script src="assets/js/bootstrap.min.js"></script>
	<script src="assets/js/TweenMax.min.js"></script>
	<script src="assets/js/resizeable.js"></script>
	<script src="assets/js/joinable.js"></script>
	<script src="assets/js/xenon-api.js"></script>
	<script src="assets/js/xenon-toggles.js"></script>
	<script src="assets/js/moment.min.js"></script>

  
	<%--<!-- Imported scripts on this page -->
	<script src="assets/js/daterangepicker/daterangepicker.js"></script>
	<script src="assets/js/datepicker/bootstrap-datepicker.js"></script>
	<script src="assets/js/timepicker/bootstrap-timepicker.min.js"></script>
	<script src="assets/js/colorpicker/bootstrap-colorpicker.min.js"></script> --%>

	<script src="assets/js/select2/select2.min.js"></script>
	<script src="assets/js/jquery-ui/jquery-ui.min.js"></script>
	<script src="assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>
	<script src="assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>
	<script src="assets/js/typeahead.bundle.js"></script>
	<script src="assets/js/handlebars.min.js"></script>
	<script src="assets/js/multiselect/js/jquery.multi-select.js"></script>


	<!-- JavaScripts initializations and stuff -->
	<script src="assets/js/xenon-custom.js"></script>

    <script src="assets/js/datatables/js/jquery.dataTables.min.js"></script> 
	<script src="assets/js/datatables/tabletools/dataTables.tableTools.min.js"></script>
        <!-- Imported scripts on this page -->
	<script src="assets/js/jquery-validate/jquery.validate.min.js"></script>


 

    <style type="text/css">
        a:link {
            color: blue;
            text-decoration: none;
        }

        a:visited {
            color: blue;
            text-decoration: none;
        }

        a:hover {
            color: red;
            text-decoration: none;
        }
    </style>
</body>
</html>
 