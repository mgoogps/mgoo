<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlarmMessage.aspx.cs" Inherits="MgooGps.AlarmMessage" %>
 <%@ Import Namespace="MgooGps.com" %> 
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>报警消息</title> 
	<link rel="stylesheet" href="assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="assets/css/fonts/fontawesome/css/font-awesome.min.css"/>
	<link rel="stylesheet" href="assets/css/bootstrap.css"/>
	<link rel="stylesheet" href="assets/css/xenon-core.css"/>
    <script src="js/Language/language02-<%= Utils.language %>.js"></script>

     <script src="js/jquery-1.8.3.js"></script> 
   
    <!-- Imported styles on this page -->
	<link rel="stylesheet" href="assets/js/datatables/dataTables.bootstrap.css"/>

        <link href="js/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
    <script type="text/javascript" src="js/daterangepicker/moment.js"></script>
    <script type="text/javascript" src="js/daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="js/daterangepicker/moment.min.js"></script>
          <%--layer 弹出 --%>
    <script src="js/lib/layer/layer.js"></script>
    <!-- Imported scripts on this page -->
	<script src="assets/js/toastr/toastr.min.js"></script> 
    <script src="js/mg_public.js"></script> 
    
    <link href="js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" /> 
    <script src="js/lib/ligerUI/js/core/base.js"></script> 
    <script src="js/lib/ligerUI/js/plugins/ligerComboBox.js"></script>
    <script src="js/lib/ligerUI/js/plugins/ligerResizable.js"></script>
    <script src="js/lib/ligerUI/js/plugins/ligerTree.js"></script> 
    <script src="js/DeviceOper.js"></script>
    <script src="js/fun.js"></script> 
	
    <style>
        html { overflow-x:hidden; }
        .l-text-wrapper {
            left:60px; 
              margin-top:5px;
        }
        #ligerTree {
            border: 1px solid #e4e4e4;
            height: 20px;
            float: left; 
        }
        .panel-title {
            float:left; margin-top:10px;
            font-size:12px;
        }
        .col-md-3 {
            border: 0px solid red;
        }
        .col-md-9 {
         border:0px solid blue;
        }
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
<body id="body">
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
                        <h3 class="panel-title">选择型号:  </h3>
                        <select class="form-control" id="selectDictionary" style="width: 140px; font-size: 12px; float: left">
                            <option value="">---请选择---</option>
                            <%foreach (DataRow dr in DictionaryTable.Rows)
                                {
                            %>
                            <option value="<%=dr["DataValue"] %>"><%=dr["DataText"] %></option>
                            <%
                                }
                            %>
                        </select>
                        <h3 class="panel-title">&nbsp&nbsp&nbsp 报警类型: 
                        </h3>
                        <select class="form-control" id="selectMsgType" style="width: 140px; font-size: 12px; float: left">
                            <option value="">---请选择---</option>
                            <%foreach (DataRow dr in MessageTypeList.Rows)
                                {
                            %>
                            <option value="<%=dr["NotificationType"] %>"><%=dr["Message"] %></option>
                            <%
                                }
                            %>
                        </select>

                        <h3 class="panel-title" style="margin-top: 10px; font-size: 12px; float: left">&nbsp&nbsp&nbsp 报警时间：</h3>
                        <input type="text" style="width: 180px; float: left; font-size: 12px; height: 31px;" name="reservation" id="reservationtime" class="form-control span4" value="<%=date %>" />
                        &nbsp&nbsp&nbsp&nbsp 
                            <button type="button" class="btn btn-gray" id="btnSearch" style="background-color: #5CB85C; height: 31px; font-size: 12px; color: white; font-weight: bold;">查询</button>
                    </div>
                </div>

             
                <div class="panel-body">
                    <div class="table-responsive" style="color: black;" data-pattern="priority-columns" data-focus-btn-icon="fa-asterisk" data-sticky-table-header="false" data-add-display-all-btn="false" data-add-focus-btn="false">

                        <table cellspacing="0" class="table table-small-font table-bordered table-striped" id="EMTable">
                            <thead>
                                <tr>
                                    <th data-priority="1" style="width: 10px;"></th>
                                    <th data-priority="1">所属用户</th>
                                    <th data-priority="1">设备名字</th>
                                    <th data-priority="3">IMEI号</th>
                                    <th data-priority="1">报警类型</th>
                                    <th data-priority="3">报警时间</th>
                                    <th data-priority="3">定位时间</th>
                                    <th data-priority="5">设备型号</th>
                                    <%-- <th data-priority="3">状态</th>--%>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                                <tr style="height: 30px; border: 0px;">
                                    <th>
                                        <button type="button" class="btn btn-success btn-icon" id="btnImportExcel" style="height: 31px; font-size: 12px; font-weight: bold;">
                                            <i class="fa-check"></i>
                                            <span>导出Excel</span>
                                        </button>
                                        <iframe src="/404.html" id="downloadIframe" style="display: none"></iframe>
                                    </th>
                                </tr>
                            </tfoot>
                        </table>

                    </div>

                    <script type="text/javascript">
                        var UserID = "";
                        // This JavaScript Will Replace Checkboxes in dropdown toggles
                        jQuery(document).ready(function ($) {
                           
                            var ligerObj = new ligerTreeObject();
                            ligerObj.getLigerTreeData(function (dataList) {
                                var treeData = [];
                                $(dataList).each(function (k, v) {
                                    treeData.push({ id: v["UserID"], pid: v["ParentID"], text: v["UserName"], icon: (v.UserType == 1 ? "js/lib/ligerUI/skins/icons/memeber.gif" : "js/lib/ligerUI/skins/icons/customers.gif") });
                                });
                                ligerObj.ligerTreeInit($("#ligerTree"), treeData, function (newValue) {
                                    UserID = newValue;
                                }); 
                            });
                            parent.loading(100);
                            $('#reservationtime').daterangepicker({
                                timePicker: false,
                                timePickerIncrement: 10,
                                format: 'YYYY-MM-DD'
                            }, function (start, end, label) {
                                $("#startDate").val($("#reservationtime").val().split("到")[0]);
                                $("#endDate").val($("#reservationtime").val().split("到")[1]);
                            });

                            var tb = $("#EMTable thead tr th");
                            tb[0].innerText = allPage.num;
                            tb[1].innerText = dealerPage.belongTo;
                            tb[2].innerText = allPage.deviceName;
                            tb[3].innerText = allPage.imeiNo;
                            tb[4].innerText = warnMessagePage.alarmType;
                            tb[5].innerText = warnMessagePage.alarmTime;
                            tb[6].innerText = allPage.positionTime;
                            tb[7].innerText = allPage.modelName;
                            //tb[8].innerText = allPage.state;

                            var len = $(".chat-group a", parent.document).length;
                            for (var i = 0; i < len; i++) {
                                if ($.trim($(".chat-group a:eq(" + i + ")", parent.document).text()) == warnMessagePage.warnMsg) {
                                    $(".chat-group a:eq(" + i + ")", parent.document).attr("class", "menuaselect");
                                }
                            }
                            setTimeout(function () {
                                //  $(".checkbox-row input").addClass('cbr');
                                //  cbr_replace();
                                $(".dropdown-toggle").hide();

                            }, 0);
                            //getAlarmList($("#userid").val(), true, $("#selectStatus").val());
                            $("#btnImportExcel").on("click", function () {
                                ImportExcel("AlarmMessage", "", "", "", "", "", $("#userid").val());
                            });
                            $("#btnSearch").on("click", function () {

                                GetMessageList();
                            });

                            //$("#btnAllClear").on("click", function () {
                            //    jQuery('#modal-1', window.parent.document).modal('show', { backdrop: 'fade' });
                            //});
                            //$("#selectStatus").on("change", function () {
                            //    getAlarmList($("#userid").val(), true, $(this).val());
                            //});

                        });
                    </script>
                </div>

            </div>
        </div>
    </div>

    <!-- Imported styles on this page -->
    <link rel="stylesheet" href="assets/js/select2/select2.css" />
    <link rel="stylesheet" href="assets/js/select2/select2-bootstrap.css" />
    <link rel="stylesheet" href="assets/js/multiselect/css/multi-select.css" />

    <script src="assets/js/select2/select2.min.js"></script>
    <!-- Bottom Scripts -->
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/TweenMax.min.js"></script>
    <script src="assets/js/joinable.js"></script>
    <script src="assets/js/xenon-api.js"></script>
    <script src="assets/js/xenon-toggles.js"></script>
    <script src="assets/js/jquery-ui/jquery-ui.min.js"></script>



    <script src="assets/js/datatables/js/jquery.dataTables.min.js"></script>
    <!-- Imported scripts on this page -->
    <script src="assets/js/rwd-table/js/rwd-table.min.js"></script>

    <!-- JavaScripts initializations and stuff -->
    <script src="assets/js/xenon-custom.js"></script>

    <!-- Imported scripts on this page -->
    <script src="assets/js/datatables/dataTables.bootstrap.js"></script>
    <script src="assets/js/datatables/yadcf/jquery.dataTables.yadcf.js"></script>
    <script src="assets/js/datatables/tabletools/dataTables.tableTools.min.js"></script>

    <script src="js/common/GlarmMessage.js"></script>

    <script src="assets/js/resizeable.js"></script>

</body>
</html>
