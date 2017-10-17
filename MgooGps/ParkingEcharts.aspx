<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParkingEcharts.aspx.cs" Inherits="MgooGps.ParkingEcharts" %>
 <%@ Import Namespace="MgooGps.com" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="Pragma" content="no-cache"/>
    <meta   http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Expires" content="0"/> 
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>停留折线图</title>
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
</head>
<body>
      <input type="hidden" id="userid" value="<%= Utils.GetSession("UserInfo").UserID %>" />
  	<div class="row"  >
				<div class="col-md-12"  >
				
					<div class="panel panel-default">
						<div class="panel-heading"> 
									<script type="text/javascript">
									    jQuery(document).ready(function ($) {
									        $("#s2example-2").select2({
									            placeholder: '请输入设备名称查询...',
									            allowClear: false
									        }).on('select2-open', function () {
									            // Adding Custom Scrollbar
									            //  $(this).data('select2').results.addClass('overflow-hidden').perfectScrollbar(); //多选去掉注释
									        }).on("change", function () {

									            //var vals = $(this).val();
									            //if (vals == null) {
									            //    $("#SDTable tbody tr").show();
									            //    return;
									            //}

									            //var trs = $("#SDTable tbody tr");
									            //$("#SDTable tbody tr").hide();
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
									<select class="form-control" id="s2example-2"  style="float:left;width:20%; font-size:12px;"> <%--加上multiple就是多选，去掉就是单选--%>
										<option></option>
										<optgroup label="设备名称">
											   
										</optgroup>
									</select>
                            <h3 class="panel-title" style="margin-top:10px;font-size:12px;"> &nbsp&nbsp&nbsp 时间：</h3>
										 <input type="text" style="width: 110px; float:left;  font-size:12px;height:31px;  " name="reservation" id="reservationtime" class="form-control span4" value="<%=date %>"  />
                            &nbsp&nbsp&nbsp&nbsp  <button class="btn btn-gray" id="btnSearch" style="background-color:#5CB85C;height:31px;font-size:12px;font-weight:bold;color:white;">查询</button>
							        <script type="text/javascript">
							            $(document).ready(function () {
							                $('#reservationtime').daterangepicker({
							                    timePicker: false,
							                    timePickerIncrement: 10,
							                    format: 'YYYY-MM-DD',
							                    singleDatePicker:true
							                }, function (start, end, label) {
							                    $("#startDate").val($("#reservationtime").val().split("到")[0]);
							                    $("#endDate").val($("#reservationtime").val().split("到")[1]);
							                });

							                //$("#SDTable").dataTable({
							                //    dom: "<'row'<'col-sm-5'l><'col-sm-7'Tf>r>" +
							                //           "t-" +
							                //           "<'row'<'col-xs-6'i><'col-xs-6'p>>",

							                //    tableTools: {
							                //        sSwfPath: "assets/js/datatables/tabletools/copy_csv_xls_pdf.swf"
							                //    }
							                //});
							            });
                           </script> 
						</div>
						<div class="panel-body"> 
                            	 
                           
	                           
								        
							            <script type="text/javascript">
							                // This JavaScript Will Replace Checkboxes in dropdown toggles
							                jQuery(document).ready(function ($) {
							                    var len = $(".chat-group a", parent.document).length;
							                    for (var i = 0; i < len; i++) {
							                        if ($.trim($(".chat-group a:eq(" + i + ")", parent.document).text()) == "停留折线图") {
							                         //   $(".chat-group a:eq(" + i + ")", parent.document).attr("class", "menuaselect");
							                        }
							                    }
							                    setTimeout(function () {
							                        $(".checkbox-row input").addClass('cbr');
							                        // cbr_replace();
							                        $(".dropdown-toggle").hide();
							                    }, 0);
							                    GetCurrentUserDevicesName($("#s2example-2 optgroup"));
							                    parent.loading(100);

							                    $("#btnSearch").on("click", function () {
							                        tabEcWidth();
							                        if ($("#s2example-2").val() == "" || $("#s2example-2").val() == null) {
							                            parent.toastrMessage("opts_waming", "请至少选择一台设备", "警告"); return;
							                        }
							                        //if ($("#s2example-2").val().length > 10) {
							                        //    parent.toastrMessage("opts_waming", "最多只能选择10台设备", "警告"); return;
							                        //} 
							                        // GetStopDetail($("#s2example-2").val(), $("#reservationtime").val().split("到")[0], $("#reservationtime").val().split("到")[1]);
							                        GetSpeedReport($("#s2example-2").val(), $("#reservationtime").val() , null, $("#s2example-2").select2('data').text);
							                    });
							                    $("#btnImportExcel").on("click", function () {
							                        ImportExcel("StopDetail", $("#reservationtime").val().split("到")[0], $("#reservationtime").val().split("到")[1], $("#s2example-2").val(), "", "");
							                    });
							                });
							            </script>
								
					            
						              <div id="main" style="height:500px;width:90%;  "></div>  
				               
							</div>
							
						 
					
					</div>
				</div>
			</div>
      <script src="js/echarts-2.2.7/dist/echarts.js"></script> 

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

    <!-- Imported scripts on this page -->  
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
     
     <script type="text/javascript">
         function loadEcharts(xData, Speed, DeviceName) { 
             var xAxis24 = [];
             var jg = parseInt(Speed.length / 24); 
             require(
                 [
                     'echarts',
                     'echarts/chart/line'  // 使用柱状图就加载bar模块，按需加载
                 ], function (ec) {
                     // 基于准备好的dom，初始化echarts图表
                     var myChart = ec.init(document.getElementById('main'), 'macarons'); // 主题：macarons
                     option = {
                         tooltip: {
                             trigger: 'axis',
                             formatter: function (params, ticket, callback) { 
                                 var res = params[0].seriesName;
                                 for (var i = 0, l = params.length; i < l; i++) {
                                     res += '<br/>' + params[i].name + ' : ' + params[i].value+"KM/H";
                                 }
                                 setTimeout(function () {
                                     // 仅为了模拟异步回调
                                     callback(ticket, res);
                                 }, 200)
                                 return "loading";
                             }
                         },
                         legend: {
                             data: function () {
                                 var data = [];
                                 for (var i = 0; i < DeviceName.length; i++) {
                                     data.push(DeviceName[i]);
                                 }
                                 return data;
                             }()
                         },
                         toolbox: {
                             show: true,
                             feature: {
                                 //  mark: { show: true },
                                 dataZoom: { show: true },
                                 //  dataView: { show: true },
                                 //  magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
                                 restore: { show: true },
                                 saveAsImage: { show: true }
                             }
                         },
                         calculable: true,
                         dataZoom: {
                             show: false,
                             realtime: true,
                             start: 0,
                             end: 100
                         },
                         xAxis: [
                             {
                                 type: 'category', 
                                 boundaryGap: false, 
                                 data: function () {
                                     var list = [];
                                     for (var i = 0; i < xData.length; i++) { 
                                         list.push(xData[i]);
                                     }
                                     return list;
                                 }(),
                                 name: "时间",
                                 axisLabel: { 
                                     interval: jg , 
                                     formatter: function (value) {  
                                         var xTime = value.split(' ')[1];

                                         var hh = xTime.split(':')[0];
                                         var mm = xTime.split(':')[1];
                                      
                                         if (value.substring(10, value.length - 3) != "0:00") {
                                             if (mm > 40) {
                                                 hh++;
                                                 mm = "00";
                                             } else if (mm > 20 && mm < 40) {
                                                 mm = "30";
                                             } else
                                                 mm = "00";
                                         }
                                         return hh + ":" + mm;
                                     }
                                 }
                             }
                         ],
                         yAxis: [
                             {
                                 type: 'value',
                                 // min: 20,
                                 // max: 150,
                                 name: "时速",
                                 scale: true, 
                                 axisLabel: { formatter: '{value} KM' }
                             }
                         ],
                         series:
                             function () {
                                 var data = [];
                                 for (var i = 0; i < DeviceName.length; i++) {
                                     var obj = {
                                         name: DeviceName[i],
                                         type: 'line',
                                         symbol: "none",
                                         clickable: false,
                                         showAllSymbol: true, 
                                         markLine: {
                                             data: [
                                              //  { type: 'average', name: '平均时速' },
                                                   // { name: '标线1起点', value: 50, xAxis: '00:00', yAxis: "20" }, { name: '标线2点', xAxis: '23:59', yAxis: '20' },
                                                  //{ name: '标线1终点', xAxis: 150, yAxis: 120 }
                                             ]
                                         },
                                         markPoint: {
                                             data: [
                                                 { type: 'max', name: '最大时速' }
                                                 //,{ type: 'min', name: '最小时速' }
                                             ]
                                         },
                                         data:
                                             function () {
                                                 var list = [];
                                                 for (var j = 0; j < Speed.length; j++) {
                                                     if (parseFloat(Speed[j]) <= 7) {
                                                         Speed[j] = 0;
                                                     }
                                                     list.push(Speed[j]);
                                                 }
                                                 return list; 
                                             }()
                                     };
                                     data.push(obj);
                                 }
                                 return data;
                             }()

                     };
                     // 为echarts对象加载数据 
                     myChart.setOption(option);
                 }
                    );
         };
         $(function () {
             require.config({
                 paths: {
                     echarts: '/js/echarts-2.2.7/dist'
                 }
             });
             var mydate = new Date();
             loadEcharts([mydate.getFullYear()+"-"+(mydate.getMonth()+1)+"-"+mydate.getDay()+" "+mydate.getHours()+":"+mydate.getMinutes()+":"+mydate.getSeconds()], [1], ["请选择设备"]);
             tabEcWidth();
         }); 
         function tabEcWidth() {
             $("#main").width($(".panel-body").width());
         } 
     </script>
</body>
</html>

