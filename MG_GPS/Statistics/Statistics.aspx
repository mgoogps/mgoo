<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="MG_GPS.Statistics.Statistics" %> 
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta   http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Expires" content="0"/> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>设备统计</title> 
	<link rel="stylesheet" href="../Scripts/assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="../Scripts/assets/css/fonts/fontawesome/css/font-awesome.min.css"/>
	<link rel="stylesheet" href="../Scripts/assets/css/bootstrap.css"/>
	<link rel="stylesheet" href="../Scripts/assets/css/xenon-core.css"/>  
     <script src="../Scripts/jquery-1.8.3.js"></script>  
    <!-- Imported styles on this page -->
	<link rel="stylesheet" href="../Scripts/assets/js/datatables/dataTables.bootstrap.css"/>

        <!--时间控件-->
    <link href="../Scripts/daterangepicker/bootstrap.min.css" rel="stylesheet" />
    <link href="../Scripts/daterangepicker/font-awesome.min.css" rel="stylesheet" />
    <link href="../Scripts/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/daterangepicker/moment.js"></script>
    <script type="text/javascript" src="../Scripts/daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="../Scripts/daterangepicker/moment.min.js"></script>

    <!-- Imported scripts on this page -->
	<script src="../Scripts/assets/js/toastr/toastr.min.js"></script> 
    <script src="../Scripts/mg_public.js"></script> 
    <script src="../Scripts/CoureName.js"></script>
   <script src="../Scripts/fun.js"></script>
      <script src="../Scripts/Statistic.js"></script>
     <style>
html { overflow-x:hidden; }
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

</head>
<body>
      <input type="hidden" id="userid" value="<%= //Utils.GetSession("UserInfo").UserID %>" />
  	<div class="row"  >
				<div class="col-md-12"  >
				
					<div class="panel panel-default">
						<div class="panel-heading"> 
						 
									<script type="text/javascript">
									    jQuery(document).ready(function ($) {
									        $("#s2example-2").select2({
									            placeholder: '请输入设备名称查询...',
									            allowClear: true
									        }).on('select2-open', function () {
									            // Adding Custom Scrollbar
									            $(this).data('select2').results.addClass('overflow-hidden').perfectScrollbar();
									        });
									    });
									</script>
										<h3 class="panel-title" style="margin-top:10px;font-size:12px;">设备名称：</h3>
									<select class="form-control" id="s2example-2" multiple style="float:left;width:20%; font-size:12px;">
										<option></option>
										<optgroup label="设备类型"> 
										</optgroup>
									</select>
                            <h3 class="panel-title" style="margin-top:10px;font-size:12px;">&nbsp&nbsp&nbsp 时间段：</h3>
										 <input type="text" style="width: 180px; float:left;  font-size:12px;height:31px;  " name="reservation" id="reservationtime" class="form-control span4" value="<%=date %>"  />
                            &nbsp&nbsp&nbsp&nbsp  <button class="btn btn-gray" id="btnSearch" style="background-color:#5CB85C;height:31px;font-size:12px;color:white;font-weight:bold;">查询</button>
							        <script type="text/javascript">
							            $(document).ready(function () {
							                $('#reservationtime').daterangepicker({
							                    timePicker: false,
							                    timePickerIncrement: 10,
							                    format: 'YYYY-MM-DD'
							                }, function (start, end, label) {
							                    // $("#startDate").val($("#reservationtime").val().split("到")[0]);
							                    // $("#endDate").val($("#reservationtime").val().split("到")[1]);
							                });
							            });

                           </script> 
                            <style type="text/css">
                                .cursor
                                {
                                    cursor:pointer;
                                }
                            </style>
						</div>
						<div class="panel-body"> 
							<div class="table-responsive" data-pattern="priority-columns" data-focus-btn-icon="fa-asterisk" data-sticky-table-header="false" data-add-display-all-btn="false" data-add-focus-btn="false">
							                              <div class="links-block left-sep">
						                        <h4>
							                        <span style="font-size:12px;color:black;">分析统计包含自己用户和所有下级用户下的所有设备(点击设备统计数字,能显示该统计设备列表) </span>
						                        </h4> 
						                        <ul class="list-unstyled" style="width:300px;font-size:12px;color:black;">
							                        <li >
								                        <label for="sp-chk1">所有设备</label><span class="label label-secondary pull-right cursor" onclick="showmodel(1)">0台</span> 
							                        </li>
							                        <li> 
								                        <label for="sp-chk2">当前在线设备数</label><span class="label label-secondary pull-right cursor" onclick="showmodel(2)">0台</span> 
							                        </li>
							                        <li> 
								                        <label for="sp-chk3">超过七天设备数</label><span class="label label-secondary pull-right cursor" onclick="showmodel(3)">0台</span> 
							                        </li>
							                        <li> 
								                        <label for="sp-chk4">离线设备数</label><span class="label label-secondary pull-right cursor" onclick="showmodel(4)">0台</span> 
							                        </li>
                                                    <li> 
								                        <label for="sp-chk5">使用设备数</label><span class="label label-secondary pull-right cursor" onclick="showmodel(5)">0台</span> 
							                        </li>
                                                    <li> 
								                        <label for="sp-chk6">已过期设备数</label><span class="label label-secondary pull-right cursor" onclick="showmodel(6)">0台</span> 
							                        </li>
                                                    <li> 
								                        <label for="sp-chk7">未启用设备数</label><span class="label label-secondary pull-right cursor" onclick="showmodel(7)">0台</span> 
							                        </li>
						                        </ul>
					                        </div>
                                <style type="text/css">                                    
                                    /*li {  font-size:20px;   }*/
                                </style>
							</div>
							
						</div>

							</div>
							
							<script type="text/javascript">
							    // This JavaScript Will Replace Checkboxes in dropdown toggles
							    jQuery(document).ready(function ($) {
							        var len = $(".chat-group a", parent.document).length;
							        for (var i = 0; i < len; i++) {
							            if ($.trim($(".chat-group a:eq(" + i + ")", parent.document).text()) == "设备统计") {
							                $(".chat-group a:eq(" + i + ")", parent.document).attr("class", "menuaselect");
							            }
							        }
							        GetDevicesModel($("#s2example-2 optgroup"));
							        GetUserDeviceInfo();
							        $("#btnSearch").on("click", function () {
							            GetUserDeviceInfo();
							        });
							    });

							    function showmodel(t) {
							        //   $("#modal-5", window.parent.document).modal('show', { backdrop: 'static' });
							        // if (t == 1) {
							        $("#iframepage", parent.document).attr("src", "Statistics/DevicesHireExpire.aspx?day=0&t=" + t);
							        //  } else {
							        //      parent.showModal(t);
							        //  }
							        // $("#ModalShowDeviceDetail", window.parent.document).modal('show', { backdrop: 'static' });
							    }
							</script>  
						</div>
					
					</div>
       <!-- Modal 6 (Confirm)-->
    <div class="modal fade" id="model" data-backdrop="static">
		<div class="modal-dialog">
			<div class="modal-content">
			  <form role="form" id="form1" method="post" class="validate" onsubmit="return updateUserInfo(1);">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">用户信息</h4>
				</div>
				
				<div class="modal-body">
			
					    <div class="row">
						    <div class="col-md-6">
							
							    <div class="form-group">
								    <label for="field-1" class="control-label">客户名称</label> 
								    <input type="text" class="form-control" id="txtUserName" placeholder="name" disabled>
							    </div>	
							
						    </div>
						
						    <div class="col-md-6">
							
							    <div class="form-group">
								    <label for="field-2" class="control-label">登录账号</label> 
								    <input type="text" class="form-control" id="txtLoginName" placeholder="LoginName" disabled>
							    </div>	
						
						    </div>
					    </div>
				      <div class="row">
						    <div class="col-md-4">
							
							    <div class="form-group">
								    <label for="field-4" class="control-label">联系人</label> 
								    <input type="text" class="form-control" id="txtContacts" name="name" placeholder="Contacts" data-validate="required" data-message-required="联系人不能为空." />
							    </div>	
							
						    </div>
						
						    <div class="col-md-4">
							
							    <div class="form-group">
								    <label for="field-5" class="control-label">电话</label> 
								    <input type="text" class="form-control" id="txtPhone" name="min_field" data-validate="required,number,minlength[6]"   placeholder="Phone">
							    </div>	
						
						    </div>
						
						    <div class="col-md-4"> 
							    <div class="form-group">
								    <label for="field-6" class="control-label">邮箱</label> 
								    <input type="text" class="form-control"  id="txtEmail" placeholder="E-Mail" data-mask="email" name="email" data-message-email="请输入正确的邮箱" data-validate="email">
							    </div>	 
						    </div>
					    </div>
					    <div class="row">
						    <div class="col-md-12">
							
							    <div class="form-group">
								    <label for="field-3" class="control-label">地址</label>
								    <input type="text" class="form-control" id="txtAddress" placeholder="Address">
							    </div>	
							
						    </div>
					    </div> 
                   
				</div>
				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnUserInfoClose">关闭</button>
					<button type="submit" class="btn btn-success" id="btnSaveUserInfo">保存并关闭</button>
				</div>
             </form>
			</div>
		</div>
	</div>

				 
        <!-- Imported styles on this page --> 
	<link rel="stylesheet" href="../Scripts/assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="../Scripts/assets/js/select2/select2-bootstrap.css"/>
	<link rel="stylesheet" href="../Scripts/assets/js/multiselect/css/multi-select.css"/>
    <!-- Bottom Scripts -->
	<script src="../Scripts/assets/js/bootstrap.min.js"></script>
	<script src="../Scripts/assets/js/TweenMax.min.js"></script>
	<script src="../Scripts/assets/js/resizeable.js"></script>
	<script src="../Scripts/assets/js/joinable.js"></script>
	<script src="../Scripts/assets/js/xenon-api.js"></script>
	<script src="../Scripts/assets/js/xenon-toggles.js"></script>
	<script src="../Scripts/assets/js/moment.min.js"></script>

    <!-- Imported scripts on this page -->  
	<script src="../Scripts/assets/js/colorpicker/bootstrap-colorpicker.min.js"></script>
	<script src="../Scripts/assets/js/select2/select2.min.js"></script>
	<script src="../Scripts/assets/js/jquery-ui/jquery-ui.min.js"></script>
	<script src="../Scripts/assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>
	<script src="../Scripts/assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>
	<script src="../Scripts/assets/js/typeahead.bundle.js"></script>
	<script src="../Scripts/assets/js/handlebars.min.js"></script>
	<script src="../vassets/js/multiselect/js/jquery.multi-select.js"></script>
      
</body>
</html>
