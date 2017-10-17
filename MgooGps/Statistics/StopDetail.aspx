<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopDetail.aspx.cs"  EnableSessionState="ReadOnly" Inherits="MgooGps.Statistics.StopDetail" %>
  <%@ Import Namespace="MgooGps.com" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta  http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Expires" content="0"/> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>停留详单</title>
    <script src="../js/Language/language02-<%= Utils.language %>.js"></script>  
	<link rel="stylesheet" href="../assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="../assets/css/fonts/fontawesome/css/font-awesome.min.css"/>
	<link rel="stylesheet" href="../assets/css/bootstrap.css"/>
	<link rel="stylesheet" href="../assets/css/xenon-core.css"/>  
     <script src="../js/jquery-1.8.3.js"></script>  
    <!-- Imported styles on this page -->
	<link rel="stylesheet" href="../assets/js/datatables/dataTables.bootstrap.css"/>

        <!--时间控件-->
    <link href="../js/daterangepicker/bootstrap.min.css" rel="stylesheet" />
    <link href="../js/daterangepicker/font-awesome.min.css" rel="stylesheet" />
    <link href="../js/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/daterangepicker/moment.js"></script>
    <script type="text/javascript" src="../js/daterangepicker/daterangepicker.js"></script> 

    <!-- Imported scripts on this page -->
	<script src="../assets/js/toastr/toastr.min.js"></script> 
    <script src="../js/mg_public.js"></script> 
    <script src="../js/CoureName.js"></script>
   <script src="../js/fun.js"></script>  

    <script src="../js/DeviceOper.js"></script>
   
    <link href="../js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" />  
    <script src="../js/lib/ligerUI/js/core/base.js"></script> 
    <script src="../js/lib/ligerUI/js/plugins/ligerComboBox.js"></script>
    <script src="../js/lib/ligerUI/js/plugins/ligerResizable.js"></script>
    <script src="../js/lib/ligerUI/js/plugins/ligerTree.js"></script> 

    <%--layer 弹出 --%>
    <script src="../js/lib/layer/layer.js"></script>
    <style>
        html {
            overflow-x: hidden;
        }

        #ligerTree {
            border: 1px solid #e4e4e4;
            height: 20px;
            float: left;
        }

        .panel-title {
            float:left;
            margin-top: 10px;
            font-size: 12px !important;
        }
        .l-text-wrapper {
             margin-left:70px;
             margin-top:5px;
        }
    </style>
</head>
<body>
    <input type="hidden" id="userid" value="<%= Utils.GetSession("UserInfo").UserID %>" />
    <div class="row">

        <div class="col-md-12">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-3 col-lg-3">
                        <h3 class="panel-title">选择用户：</h3>
                        <input type="text" id="ligerTree" autocomplete="off" onkeyup="ligerTreeSearch()" placeholder="输入名称搜索..." />
                    </div>
                    <div class="col-md-9 col-lg-9" style="margin-left: -30px;">
                        <h3 class="panel-title">设备名称：</h3>
                        <select class="form-control" id="s2example-2" style="width: 20%; float: left; max-width: 250px; font-size: 12px; height: 34px;">
                            <%--加上multiple就是多选，去掉就是单选--%>
                            <option></option>
                            <optgroup label="设备名称">
                            </optgroup>
                        </select>
                        <h3 class="panel-title">&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp 时间段：</h3>
                        <input type="text" style="width: 180px; float: left; font-size: 12px; height: 34px;" name="reservation" id="reservationtime" class="form-control span4" value="<%=date %>" />
                        &nbsp&nbsp&nbsp&nbsp 
                         <h3 class="panel-title" style="white-space: nowrap;">&nbsp&nbsp&nbsp 
                              停留大于(小时)
                             <input type="number" class="form-control" id="txtFilterDay" value="1" style="width: 70px; margin-top: -23px; margin-left: 93px;" />
                         </h3>
                        <h3 class="panel-title">&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp 过滤无线： 
                             <input type="checkbox" id="chkFilter" value="" style="margin-top: -3px;" checked />
                        </h3>
                        <h3 class="panel-title">&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp 最后停留点： 
                             <input type="checkbox" id="chkLastStop" value="" style="margin-top: -3px;" />
                        </h3>

                        <button class="btn btn-gray" id="btnSearch" style="background-color: #5CB85C; height: 34px; font-size: 12px; font-weight: bold; color: white;">查询</button>

                    </div>
                    <script type="text/javascript">
                        var UserID;
                        var queryData;
                        jQuery(document).ready(function ($) {
                            
                            var ligerObj = new ligerTreeObject();
                            var timeout;
                            ligerObj.getLigerTreeData(function (dataList) {
                                var treeData = [];
                                $(dataList).each(function (k, v) {
                                    treeData.push({ id: v["UserID"], pid: v["ParentID"], text: v["UserName"], icon: (v.UserType == 1 ? "/js/lib/ligerUI/skins/icons/memeber.gif" : "/js/lib/ligerUI/skins/icons/customers.gif") });
                                });
                                ligerObj.ligerTreeInit($("#ligerTree"), treeData, function (newValue) {
                                    UserID = newValue;
                                    if (timeout) {
                                        clearTimeout(timeout);
                                    }
                                    timeout = setTimeout(function () {
                                        var sel = $("#s2example-2 optgroup");
                                        $("#select2-chosen-1").text("");
                                        //$("#s2example-2").select2('data').text = "";
                                        GetCurrentUserDevicesName(sel, UserID);
                                    }, 200);
                                });
                            });

                            $("#s2example-2").select2({
                                placeholder: '请输入设备名称查询...',
                                allowClear: false
                            }).on('select2-open', function () {
                                // Adding Custom Scrollbar
                                //  $(this).data('select2').results.addClass('overflow-hidden').perfectScrollbar(); //多选去掉注释
                            }).on("change", function () {


                            });

                            $('#reservationtime').daterangepicker({
                                timePicker: false,
                                timePickerIncrement: 10,
                                format: 'YYYY-MM-DD'
                            }, function (start, end, label) {
                                $("#startDate").val($("#reservationtime").val().split("到")[0]);
                                $("#endDate").val($("#reservationtime").val().split("到")[1]);
                            });
                            var len = $(".chat-group a", parent.document).length;
                            for (var i = 0; i < len; i++) {
                                if ($.trim($(".chat-group a:eq(" + i + ")", parent.document).text()) == "停留详单") {
                                    $(".chat-group a:eq(" + i + ")", parent.document).attr("class", "menuaselect");
                                }
                            }
                            setTimeout(function () {
                                $(".checkbox-row input").addClass('cbr');
                                // cbr_replace();
                                $(".dropdown-toggle").hide();
                            }, 0);
                           // GetCurrentUserDevicesName($("#s2example-2 optgroup"));

                            parent.loading(100);

                            $("#btnSearch").on("click", function () {
                                var $s2 = $("#s2example-2");

                                if ($s2.val() == "" || $s2.val() == null) {
                                    //parent.toastrMessage("opts_waming", "请至少选择一台设备", "警告"); return;
                                }
                                var selValue = $s2.val();
                                $("#txtStartDate").val($("#reservationtime").val().split("到")[0]);
                                $("#txtEndDate").val($("#reservationtime").val().split("到")[1]);

                                var text = "";
                                if ($s2.select2("data")) {
                                    text = $s2.select2("data").text;
                                }
                                var chkFilter = $("#chkFilter").attr("checked") ? 1 : 0;
                                var chkLastStop = $("#chkLastStop").attr("checked") ? 1 : 0;
                                var DeviceID = $("#s2example-2 option[value=" + selValue + "]").attr("deviceid") || 0;
                                GetStopDetail(DeviceID, $("#txtStartDate").val(), $("#txtEndDate").val(), $("#txtFilterDay").val(), chkFilter, chkLastStop);
                                //GetSpeedReport(selValue, $("#txtStartDate").val(), $("#txtEndDate").val(), text);
                            });
                            $("#btnImportExcel").on("click", function () {
                                queryData = queryData || {};
                                queryData.userid = queryData.userid || 0;
                                queryData.deviceid = queryData.deviceid || 0;
                                queryData.starttime = queryData.starttime || '';
                                queryData.endtime = queryData.endtime || '';
                                queryData.stopday = queryData.stopday || 0;
                                queryData.wireless = queryData.wireless || 0;
                                queryData.laststop = queryData.laststop || 0;
                                ImportExcel("StopDetail", $("#txtStartDate").val(), $("#txtEndDate").val(), $("#s2example-2").val(), "", "", "", queryData);
                            });
                            var layerLoad;
                            function GetStopDetail(DeviceID, StartTime, EndTime, Day, Wireless, LastStop) {
                                if (layerLoad) {
                                    return;
                                }
                                if (LastStop == 0) {
                                    $("#thDeviceStatus").hide();
                                } else {
                                    $("#thDeviceStatus").show();
                                }
                                layerLoad = layer.msg('正在统计中，请稍后...', {
                                    icon: 16,
                                    shade: 0.3,
                                    time: 0
                                });
                                queryData = {
                                    userid: UserID,
                                    deviceid: DeviceID,
                                    starttime: StartTime,
                                    endtime: EndTime,
                                    stopday: Day,
                                    wireless: Wireless,
                                    laststop: LastStop,
                                }
                                $.ajax({
                                    url: "/AjaxService/AjaxService.ashx?action=getstopdetail",
                                    type: "POST",
                                    data: queryData,
                                    dataType: "json",
                                    error: function (err) { parent.toastrMessage("opts_waming", "查询停留详单时出错", allPage.toastrTitle1); },
                                    success: function (res) {

                                        if (res.Message) {
                                            layer.alert(res.Message, { icon: 6 });
                                            layer.close(layerLoad);
                                            layerLoad = null;
                                            return;
                                        }
                                        var html = [];
                                        var $table = $("#SDTable tbody");
                                        var list = JSON.parse(res.Result);
                                        $("#divCount").text("一共查询出" + list.length+"条数据.");
                                        $table.empty();
                                        for (var i = 0; i < list.length; i++) {
                                            var rowIndex = i + 1;
                                            var d = list[i];
                                            html.push("<tr> <td>" + rowIndex + "</td>");
                                            html.push(" <td>" + d.UserName + "</td>");
                                            html.push(" <td>" + d.SerialNumber + "</td>");
                                            html.push(" <td>" + d.DeviceName + "</td>");
                                            html.push(" <td>" + d.startTime + "</td>");
                                            html.push(" <td>" + d.endTime + "</td>");
                                            html.push(" <td> <a title=\"查看地图\" href=\"javascript:void(0)\"  style=\"cursor:pointer\" onclick='openMap(\"" + d.DeviceName + "\",\"" + d.startTime + "\",\"" + d.endTime + "\"," + d.OLat + "," + d.OLng + ",\"" + d.time + "\",\"" + d.address + "\",\"StopDetail\")'>" +
                                            parseFloat(d.OLat).toFixed(5) + "," + parseFloat(d.OLng).toFixed(5) + " </a> </td>");
                                            //html.push("  <td>" + d.OLat + "," + d.OLng + "</td>");
                                            html.push(" <td>" + d.time + "</td>");
                                            var _id = "address_" + rowIndex + "_" + new Date().getTime();
                                            d.address = GetAddressByLatlng(d.OLat, d.OLng, _id);
                                            if (LastStop == 1) {
                                                html.push("<td> " + d.Status + " </td>");
                                            }
                                            html.push(" <td><span id=\"" + _id + "\">" + (d.address || "") + "</span></td>");
                                            html.push(" </tr>");
                                            if (html.length % 30 == 0) {
                                                $table.append(html.join(''));
                                                html = [];
                                            }
                                        }
                                        if (list.length == 0) {
                                            html.push("<tr> <td colspan='9'>没有查询到数据.</td> </tr>");
                                        }
                                        $table.append(html.join(''));
                                        layer.close(layerLoad);
                                        layerLoad = null;
                                    }
                                });
                            }
                        });
                    </script>

                </div>
                <div class="panel-body">

                    <div style="text-align:right" id="divCount"></div>
                    <div class="table-responsive" style="color: black;" data-pattern="priority-columns" data-focus-btn-icon="fa-asterisk" data-sticky-table-header="false" data-add-display-all-btn="false" data-add-focus-btn="false">

                        <table cellspacing="0" class="table table-small-font table-bordered table-striped" id="SDTable">
                            <thead>
                                <tr>
                                    <th data-priority="1">序号</th>
                                    <th data-priority="1">用户</th>
                                    <th data-priority="1">IMEI号</th>
                                    <th data-priority="1">设备名字</th>
                                    <th data-priority="3">开始时间</th>
                                    <th data-priority="1">结束时间</th>
                                    <th data-priority="3">纬度,经度</th>
                                    <th data-priority="6">停留时间</th>
                                    <th data-priority="6" id="thDeviceStatus" style="display:none;">状态</th>
                                    <th data-priority="3">地址</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                                <tr style="height: 30px; border: 0px;">
                                    <th>
                                        <button class="btn btn-success btn-icon" id="btnImportExcel">
                                            <i class="fa-check"></i>
                                            <span>导出Excel</span>
                                        </button>
                                        <iframe src="/404.html" id="downloadIframe" style="display: none"></iframe>
                                    </th>
                                </tr>
                            </tfoot>
                        </table>

                    </div>
                    <input type="hidden" id="txtStartDate" />
                    <input type="hidden" id="txtEndDate" />


                </div>

            </div>
        </div>
    </div>

    <!-- Imported styles on this page -->
    <link rel="stylesheet" href="../assets/js/select2/select2.css" />
    <link rel="stylesheet" href="../assets/js/select2/select2-bootstrap.css" />
    <link rel="stylesheet" href="../assets/js/multiselect/css/multi-select.css" />
    <!-- Bottom Scripts -->
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/TweenMax.min.js"></script>
    <script src="../assets/js/resizeable.js"></script>
    <script src="../assets/js/joinable.js"></script>
    <script src="../assets/js/xenon-api.js"></script>
    <script src="../assets/js/xenon-toggles.js"></script>

    <!-- Imported scripts on this page -->
    <script src="../assets/js/select2/select2.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>
    <script src="../assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>
    <script src="../assets/js/typeahead.bundle.js"></script>
    <script src="../assets/js/handlebars.min.js"></script>
    <script src="../assets/js/multiselect/js/jquery.multi-select.js"></script>



    <script src="../assets/js/datatables/js/jquery.dataTables.min.js"></script>
    <!-- Imported scripts on this page -->
    <script src="../assets/js/rwd-table/js/rwd-table.min.js"></script>

    <!-- JavaScripts initializations and stuff -->
    <%--	<script src="../assets/js/xenon-custom.js"></script>--%>

    <!-- Imported scripts on this page -->

    <script src="../assets/js/datatables/dataTables.bootstrap.js"></script>
    <script src="../assets/js/datatables/yadcf/jquery.dataTables.yadcf.js"></script>
    <script src="../assets/js/datatables/tabletools/dataTables.tableTools.min.js"></script>

</body>
</html>

