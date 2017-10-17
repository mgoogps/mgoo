<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeoFencesView.aspx.cs" Inherits="MgooGps.GeoFencesView" %>
 <%@ Import Namespace="MgooGps.com" %>
<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="美谷 美谷科技 GPS 定位 防盗 电子狗" />
	<meta name="author" content="" />
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta   http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Expires" content="0"/>  
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>电子围栏统计</title>
    <script src="js/Language/language02-<%= Utils.language %>.js"></script> 
	<link rel="stylesheet" href="assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="assets/css/fonts/fontawesome/css/font-awesome.min.css"/>
	<link rel="stylesheet" href="assets/css/bootstrap.css"/>
	<link rel="stylesheet" href="assets/css/xenon-core.css"/>  
     <script src="js/jquery-1.8.3.js"></script>  
    <!-- Imported styles on this page -->
	<link rel="stylesheet" href="assets/js/datatables/dataTables.bootstrap.css"/>

        <!--时间控件-->
    <link href="js/daterangepicker/bootstrap.min.css" rel="stylesheet" />
    <link href="js/daterangepicker/font-awesome.min.css" rel="stylesheet" />
    <link href="js/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
    <script type="text/javascript" src="js/daterangepicker/moment.js"></script>
    <script type="text/javascript" src="js/daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="js/daterangepicker/moment.min.js"></script>
          <%--layer 弹出 --%>
    <script src="js/lib/layer/layer.js"></script>
    <!-- Imported scripts on this page -->
	<script src="assets/js/toastr/toastr.min.js"></script> 
    <script src="js/mg_public.js"></script> 
    <script src="js/CoureName.js"></script>
   <script src="js/fun.js"></script>  
     <style>
html { overflow-x:hidden; }
</style>
     <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script>
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
     <div id="allmap" style="width:0;height:0px; display:none"></div>
      <input type="hidden" id="userid" value="<%= Utils.GetSession("UserInfo").UserID %>" />
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
									        }).on("change", function () {;
									            //var vals = $(this).val();
									            //if (vals == null) {
									            //    $("#GFDTable tbody tr").show();
									            //    return;
									            //} 
									            //var trs = $("#GFDTable tbody tr");
									            //$("#GFDTable tbody tr").hide();
									            //var hides = [];
									            //for (var i = 0; i < trs.length; i++) {
									            //    arr();
									            //    if (vals.indexOf($(trs[i]).attr("name")) >= 0) {
									            //        hides.push(trs[i]);
									            //    }
									            //}
									            //for (var i = 0; i < hides.length; i++) {
									            //    $(hides[i]).show();
									            //}
									        });
									    });
									</script>
										<h3 class="panel-title" style="margin-top:10px;font-size:12px;">设备名称：</h3>
									<select class="form-control" id="s2example-2" multiple style="float:left;width:20%; font-size:12px;">
										<option></option>
										<optgroup label="设备名称">
											   
										</optgroup>
									</select>
                            <h3 class="panel-title" style="margin-top:10px;font-size:12px;">&nbsp&nbsp&nbsp 时间段：</h3>
										 <input type="text" style="width: 180px; float:left;height:31px;font-size:12px;" name="reservation" id="reservationtime" class="form-control span4" value="<%=date %>"  />
                              
                            &nbsp&nbsp&nbsp&nbsp  <button class="btn btn-gray" id="btnSearch" style="height:31px;background-color:#5CB85C;font-weight:bold;color:white;font-size:12px;">查询</button>
							        <script type="text/javascript">
							            $(document).ready(function () {
							                $('#reservationtime').daterangepicker({
							                    timePicker: false,
							                    timePickerIncrement: 10,
							                    format: 'YYYY-MM-DD'
							                }, function (start, end, label) {
							                    $("#startDate").val($("#reservationtime").val().split("到")[0]);
							                    $("#endDate").val($("#reservationtime").val().split("到")[1]);
							                });
							             
							            });
                           </script> 
						</div>
						<div class="panel-body"> 
							<div class="table-responsive" style="color:black;" data-pattern="priority-columns" data-focus-btn-icon="fa-asterisk" data-sticky-table-header="false" data-add-display-all-btn="false" data-add-focus-btn="false">
							
								<table cellspacing="0" class="table table-small-font table-bordered table-striped" id="GFDTable">
									<thead>
										<tr> 
                                            <th data-priority="1">序号</th>
                                            <th data-priority="1">设备名称</th>
                                            <th data-priority="1">电子围栏</th> 
											<th data-priority="3">进入时间</th>
											<th data-priority="1">离开时间</th>
                                            <th data-priority="1">持续时间</th>
											<th data-priority="3">纬度,经度</th>
											<th data-priority="3">地址</th>  
										</tr>
									</thead>
									<tbody>
									 
									</tbody>
                                        <tfoot>
                                        <tr style="height:30px; border :0px;">
                                            <th>
                                                   <button class="btn btn-success btn-icon" id="btnImportExcel"style="height:31px;font-size:12px;font-weight:bold;">
									                <i class="fa-check"></i>
									                <span>导出Excel</span>
								                   </button>  
                                                <iframe src="/404.html" id="downloadIframe" style="display:none"></iframe>
                                            </th>
                                        </tr>
                                    </tfoot>
								</table>
							
							</div>
							
							<script type="text/javascript">
							    // This JavaScript Will Replace Checkboxes in dropdown toggles
							    jQuery(document).ready(function ($) {
							        var len = $(".chat-group a", parent.document).length;
							        for (var i = 0; i < len; i++) {
							            if ($.trim($(".chat-group a:eq(" + i + ")", parent.document).text()) == "电子围栏") {
							                $(".chat-group a:eq(" + i + ")", parent.document).attr("class", "menuaselect");
							            }
							        }
							        setTimeout(function () {
							            $(".checkbox-row input").addClass('cbr');
							            // cbr_replace();
							            $(".dropdown-toggle").hide(); 
							        }, 0);
							        parent.loading(100);
							       // GetFencesView($("#reservationtime").val().split("到")[0], $("#reservationtime").val().split("到")[1], $("#s2example-2").val());
							        $("#btnSearch").on("click", function () { 
							            GetFencesView($("#reservationtime").val().split("到")[0], $("#reservationtime").val().split("到")[1], $("#s2example-2").val());
							        });
							        GetCurrentUserDevicesName($("#s2example-2 optgroup"));
							        $("#btnImportExcel").on("click", function () {
							            ImportExcel("GeoFencesView", $("#reservationtime").val().split("到")[0], $("#reservationtime").val().split("到")[1], $("#s2example-2").val(),"", "", "");
							            
							        });
							        
							    });
							</script>
								
							
						</div>
					
					</div>
				</div>
			</div>
  
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

    <!-- Imported scripts on this page -->  
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
<%--	<script src="assets/js/xenon-custom.js"></script>--%>

	<!-- Imported scripts on this page -->
	<script src="assets/js/datatables/dataTables.bootstrap.js"></script>
	<script src="assets/js/datatables/yadcf/jquery.dataTables.yadcf.js"></script>
	<script src="assets/js/datatables/tabletools/dataTables.tableTools.min.js"></script>
</body>
</html> 