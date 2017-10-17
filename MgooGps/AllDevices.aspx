<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllDevices.aspx.cs" Inherits="MgooGps.AllDevices"%>
 <%@ Import Namespace="MgooGps.com" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/> 
    <meta http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Expires" content="0"/> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>所有设备</title>
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

    <!-- Imported scripts on this page -->
	<script src="assets/js/toastr/toastr.min.js"></script> 
    <script src="js/mg_public.js"></script> 
    
   <%--<script src="js/fun.js"></script>  --%>
     <style>
    html { overflow-x:hidden; }
    </style>

     
    <link href="js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" /> 
    <script src="js/lib/ligerUI/js/core/base.js"></script> 
    <script src="js/lib/ligerUI/js/plugins/ligerComboBox.js"></script>
    <script src="js/lib/ligerUI/js/plugins/ligerResizable.js"></script>
    <script src="js/lib/ligerUI/js/plugins/ligerTree.js"></script>
    <script src="js/CoureName.js"></script>
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
    <input type="hidden" id="userid" value="<%= Utils.GetSession("UserInfo").UserID %>" />
    <input type="hidden" id="txtImei" value="<%= imei %>" />
    <div class="panel panel-default">
        <div class="panel-heading" style="width: 100%; font-size: 12px; height: 70px;">
            <table class="table table-striped" style="width: 70%;">
                <tr>
                    <td style="width: 30%">
                        <script type="text/javascript">writePage(userInfoPage.account)</script>
                        ：<%=DeviceInfoRow == null ?"":  DeviceInfoRow["LoginName"]   %></td>
                    <td style="width: 20%">
                        <script type="text/javascript">writePage(allPage.type2)</script>
                        ：<%= DeviceInfoRow == null ?"":DeviceInfoRow["UserTypeName"]  %></td>
                    <td style="width: 40%">
                        <script type="text/javascript">writePage(homePage.deviceCount)</script>
                        ：<%=DeviceInfoRow == null ?"":DeviceInfoRow["InStock"]   %>   </td>
                </tr>
                <tr>
                    <td>
                        <script type="text/javascript">writePage(allPage.cellName)</script>
                        ：<%=DeviceInfoRow == null ?"":DeviceInfoRow["FirstName"]  %> </td>
                    <td>
                        <script type="text/javascript">writePage(allPage.phone)</script>
                        ：<%= DeviceInfoRow == null ?"":DeviceInfoRow["CellPhone"]   %> </td>
                    <td>
                        <script type="text/javascript">writePage(allPage.address)</script>
                        ：<%= DeviceInfoRow == null ?"":DeviceInfoRow["Address1"]   %> </td>
                </tr>
            </table>
            <%--  <%= DeviceInfoRow == null ?"":DeviceInfoRow["UserName"]   %> <a href="javascript:void(0)" id="dingwei" >定位监控</a>--%>
        </div>
    </div>

    <div class="col-md-6" style="width: 100%; margin-top: -10px;">

        <ul class="nav nav-tabs">
            <li class="active">
                <a href="#home" data-toggle="tab" onclick="reloadDevice()">
                    <span class="visible-xs"><i class="fa-home"></i></span>
                    <span class="hidden-xs">
                        <script type="text/javascript">writePage(allPage.device)</script>
                    </span>
                </a>
            </li>
            <% if (Utils.GetSession("UserInfo").UserType == "2" && DeviceInfoRow["UserType"].ToString() == "2")
                {
            %>
            <li>
                <a href="#profile" data-toggle="tab">
                    <span class="visible-xs"><i class="fa-user"></i></span>
                    <span class="hidden-xs">
                        <script type="text/javascript">writePage(cusPage.childCus)</script>
                    </span>
                </a>
            </li>
            <%
                } %>

            <% if (Utils.GetSession("UserInfo").UserType == "2") //如果是经销商不能修改自己的信息
                {
            %>
            <li>
                <a href="#messages" data-toggle="tab">
                    <span class="visible-xs"><i class="fa-envelope-o"></i></span>
                    <span class="hidden-xs">
                        <script type="text/javascript">writePage(allPage.information)</script>
                    </span>
                </a>
            </li>
            <% } %>
            <% if (Utils.GetSession("UserInfo").SuperAdmin == "1") //只有超级管理员才有添加设备的权限
                {
            %>
            <li>
                <a href="#settings" data-toggle="tab">
                    <span class="visible-xs"><i class="fa-cog"></i></span>
                    <span class="hidden-xs">
                        <script type="text/javascript">writePage(homePage.addDevice)</script>
                    </span>
                </a>
            </li>
            <%
                } %>
        </ul>

        <div class="tab-content">
            <input type="file" id="txtFileUploadSearch" name="txtFileUploadSearch" style="display: none;" accept=" application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" />
            <iframe src="/404.html" id="downloadIframe" style="display: none;"></iframe>
            <div class="tab-pane active" id="home">

                <!-- Table exporting -->
                <div class="panel panel-default" style="margin-left: -25px; margin-top: -25px;">
                    <div class="panel-body" style="margin-top: -30px;">
                        <script src="/js/ajaxfileupload.js"></script>
                        <script type="text/javascript">
                            function tableTitle(tb) {
                                for (var i = 1; i < tb.length; i++) {
                                    switch (i) {
                                        case 0:
                                            tb[i].innerText = allPage.num;
                                            break;
                                        case 1:
                                            //  tb[i].innerText = "";
                                            tb[i].innerText = "所属用户";
                                            break;
                                        case 2:
                                            //  tb[i].innerText = "";
                                            tb[i].innerText = allPage.deviceName;
                                            break;
                                        case 3:
                                            tb[i].innerText = allPage.imeiNo;
                                            break;
                                        case 4:
                                            tb[i].innerText = allPage.modelName;
                                            break;
                                        case 5:
                                            tb[i].innerText = "流量卡号";
                                            break;
                                        case 6:
                                            tb[i].innerText = allPage.createTime;
                                            break;
                                        case 7:
                                            tb[i].innerText = allPage.activeTime;
                                            break;
                                        case 8:
                                            tb[i].innerText = allPage.hireExpireTime;
                                            break;
                                        case 9:
                                            tb[i].innerText = "通信时间";
                                            break;
                                        case 10:
                                            tb[i].innerText = "状态";
                                            break;
                                        case 11:
                                            tb[i].innerText = allPage.operation;
                                            break;
                                    }
                                }
                            }

                            function reloadDevice() {
                                parent.reloadDevice(<%= zTreeSelectUserID%>);
                            }
                            jQuery(document).ready(function ($) {

                                var tb = $("#example-4 thead tr th");
                                tableTitle(tb);
                                tb = $("#example-4 tfoot tr th");
                                tableTitle(tb);

                                parent.loading(40);

                                parent.loading(100);
                                $("#example-4").dataTable({
                                    dom: "<'row'<'col-sm-5'l><'col-sm-7'Tf>r>" +
                                           "t" +
                                           "<'row'<'col-xs-6'i><'col-xs-6'p>>",
                                    aoColumns: [
                                                { bSortable: false },
                                                    null, null, null, null, null, null, null, null, null, null, null
                                    ],
                                    tableTools: {
                                        sSwfPath: "assets/js/datatables/tabletools/copy_csv_xls_pdf.swf"
                                    }
                                });

                                var $state = $("#example-4 thead input[type='checkbox']");
                                $("#example-4").on('draw.dt', function () {
                                    cbr_replace();
                                    $state.trigger('change');
                                });
                                // Script to select all checkboxes
                                $state.on('change', function (ev) {
                                    var $chcks = $("#example-4 tbody input[type='checkbox']");
                                    if ($state.is(':checked')) {
                                        $chcks.prop('checked', true).trigger('change');
                                    }
                                    else {
                                        $chcks.prop('checked', false).trigger('change');
                                    }
                                });
                                // class = "paginate_button active" 选中的页数onclick="Sales1(<% //= //dr["DeviceID"] %>)" 
					                            //  $("#example-4 tbody tr>td>a:contains('销售')").on("click",function(){ 
					                            //   Sales1();
					                            // }); 
					                            <%
                            if (Utils.GetSession("UserInfo").UserType == "2")
                            {
                                            %>
					                            var html = [];
					                            html.push("&nbsp&nbsp&nbsp&nbsp <div class=\"btn-group\">  <button type=\"button\" class=\"btn btn-purple btn-sm\">" + cusPage.changeDevices + "</button> ");

					                            <%  if (Utils.GetSession().SuperAdmin == "1")
                            {%>
					                            html.push("<button type=\"button\" class=\"btn btn-purple btn-sm\">" + cusPage.updateExpDevices + "</button>");
					                            html.push("<button type=\"button\" class=\"btn btn-purple btn-sm\">" + cusPage.deletedDevices + "</button>");
					                            // 只有超级管理员才有 上传excel根据IMEI号查询的权限
					                            html.push("<button type=\"button\" class=\"btn btn-purple btn-sm\" id=\"uploadSearch\">上传批量查询</button>");
                                                 <%}%>
					                            html.push("  </div>");

					                            $("#example-4_length").append(html.join(''));
					                            $("#example-4_length button:contains('" + cusPage.changeDevices + "')").on("click", function () {
					                                var chcks = $("#example-4 tbody input[type='checkbox']:checked");
					                                var arr = [];
					                                for (var i = 0; i < chcks.length; i++) {
					                                    arr.push({ DeviceID: $(chcks[i]).attr("deviceid"), UserName: $(chcks[i]).attr("username"), IMEI: $(chcks[i]).attr("imei"), DeviceName: $(chcks[i]).attr("devicename") });
					                                }
					                                ShiftOrExpire('shift', arr);
					                            });
					                            $("#example-4_length button:contains('" + cusPage.updateExpDevices + "')").on("click", function () {
					                                var chcks = $("#example-4 tbody input[type='checkbox']:checked");
					                                var arr = [];
					                                for (var i = 0; i < chcks.length; i++) {
					                                    arr.push({ DeviceID: $(chcks[i]).attr("deviceid"), UserName: $(chcks[i]).attr("username"), IMEI: $(chcks[i]).attr("imei"), DeviceName: $(chcks[i]).attr("devicename") });
					                                }
					                                ShiftOrExpire('expire', arr);
					                            });
					                            $("#example-4_length button:contains('" + cusPage.deletedDevices + "')").on("click", function () {
					                                var chcks = $("#example-4 tbody input[type='checkbox']:checked");
					                                var arr = [];
					                                for (var i = 0; i < chcks.length; i++) {
					                                    arr.push({ DeviceID: $(chcks[i]).attr("deviceid") });
					                                }
					                                window.parent.loadModalBasic(arr, "", "DeleteDevices");
					                            });
					                            $("#uploadSearch").on("click", function () {
					                                var fileInput = $('#txtFileUploadSearch').on("change", function () {
					                                    $.ajaxFileUpload({
					                                        url: '/AjaxService/AjaxService.ashx?uploadType=fileuploadsearch&action=upload',//处理图片脚本
					                                        secureuri: false,
					                                        //FileName: "importexcelsim",
					                                        fileElementId: "txtFileUploadSearch",//file控件id、name
					                                        dataType: 'json',
					                                        success: function (data, status) {
					                                            if (data.success) {
					                                                var src = "/AjaxService/Log/" + data.FileName;
					                                                $("#downloadIframe").attr("src", src);
					                                            } else {
					                                                // toastr.warning(data.msg, "提示", opts_waming);
					                                            }
					                                        },
					                                        error: function (data, status, e) {
					                                            toastr.warning(data.msg, "提示", opts_waming);
					                                        }
					                                    });
					                                });
					                                $("body").append(fileInput);
					                                fileInput.trigger("click");
					                            });
					                            <%
                            }
                                             %>

					                            $("#example-4_filter input[type=search]").after("<button class=\"btn btn-gray\" onclick=\"searchImeiBatch()\">" + allPage.ImeiBatchSearch + "</button>");

					                            $("span:contains('Copy')").parent().remove();

					                            $("span:contains('Print')").parent().remove();

					                            parent.loading(100);
					                            if ($("#txtImei").val() != "") {
					                                $("#example-4_wrapper .dataTables_filter input[type=search]").val($("#txtImei").val()).trigger("keyup");
					                                $("#txtImei").val("");
					                            }
					                        });
					                        function ResetPassword(d, name) {
					                            window.parent.loadModalBasic(d, name, "ResetPassword");
					                        }
					                        function searchImeiBatch() {
					                            window.top.searchImeiBatch(<%= zTreeSelectUserID%>);
					                        }
					                        function ShiftOrExpire(type, data) {
					                            window.parent.DeviceShiftOrExpire(type, data)
					                        }
                        </script>
                        <table class="table table-bordered table-striped" id="example-4" style="color: black;">
                            <thead>
                                <tr>
                                    <th class="no-sorting">
                                        <input type="checkbox" class="cbr" />
                                    </th>
                                    <th>所属用户</th>
                                    <th>设备名称</th>
                                    <th>IMEI号</th>
                                    <th>型号</th>
                                    <th>流量卡号</th>
                                    <th>创建时间</th>
                                    <th>激活时间</th>
                                    <th>到期时间</th>
                                    <th>通信时间</th>
                                    <th>状态</th>
                                    <th>操作</th>

                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th class="no-sorting"></th>
                                    <th>所属用户</th>
                                    <th>设备名称</th>
                                    <th>IMEI号</th>
                                    <th>型号</th>
                                    <th>所属账户</th>
                                    <th>流量卡号</th>
                                    <th>激活时间</th>
                                    <th>到期时间</th>
                                    <th>通信时间</th>
                                    <th>状态</th>
                                    <th>操作</th>

                                </tr>
                            </tfoot>
                            <tbody class="middle-align">
                                <%int i = 0;

                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        dr["DeviceName"] = dr["DeviceName"].ToString() == "" ? dr["SerialNumber"] : dr["DeviceName"];
                                %>
                                <tr name="<%=  dr["DeviceName"] %>" imei="<%=  dr["SerialNumber"] %>">
                                    <td>
                                        <input type="checkbox" deviceid="<%=dr["DeviceID"] %>" username="<%=dr["UserName"] %>" imei="<%=dr["SerialNumber"] %>" devicename="<%= dr["DeviceName"] %>" class="cbr" />
                                        <%= ++i %>
                                    </td>
                                    <td><%=dr["UserName"] %></td>
                                    <td><%= dr["DeviceName"]  %></td>
                                    <td><%= dr["SerialNumber"] %></td>
                                    <td><%= dr["DataText"] %></td>
                                    <td><%=dr["PhoneNum"] %></td>
                                    <td><%= dr["Created"] %></td>
                                    <td><%= dr["ActiveDate"] %></td>
                                    <td><%= dr["HireExpireDate"] %></td>
                                    <td><%= dr["LastCommunication"] %></td>
                                    <%
                                        string line = "";
                                        if (string.IsNullOrEmpty(dr["LastCommunication"].ToString()))
                                        {
                                            line = "未激活";
                                        }
                                        else
                                        {
                                            TimeSpan ts = (DateTime.Now - Convert.ToDateTime(dr["LastCommunication"]));
                                            int mi = Convert.ToInt32(ts.TotalMinutes);
                                            int offlineMi = Convert.ToInt32(dr["offLineMi"]);
                                            if (mi <= offlineMi && mi >= 0)
                                            {
                                                line = "在线";
                                            }
                                            else
                                            {
                                                string offlinestr = "";
                                                if (mi <= 60)
                                                {
                                                    offlinestr = mi + "分钟";
                                                }
                                                var day = mi / 60 / 24;
                                                var h = mi / 60 % 24;
                                                var m = mi % 60;
                                                if (day > 0)
                                                {
                                                    offlinestr = day + "天";
                                                }
                                                else if (h > 0)
                                                {
                                                    offlinestr = h + "小时";
                                                }
                                                line = "离线-" + offlinestr;
                                            }
                                        }
                                    %>
                                    <td><%=line  %></td>
                                    <td>
                                        <a href="javascript:void(0)" onclick="window.parent.UpdateDevice(<%=dr["DeviceID"] %>)" class="btn btn-blue btn-sm btn-icon icon-left">修改</a>

                                        <% if (Utils.GetSession("UserInfo").UserType == "2")
                                            {
                                        %>
                                        <div class="btn-group">
                                            <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown">
                                                更多  <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu dropdown-blue" role="menu">
                                                <li>
                                                    <a href="javascript:void(0)" onclick="window.parent.loadModalBasic(<%=dr["DeviceID"] %>,'<%= dr["DeviceName"].ToString() =="" ?dr["SerialNumber"]:dr["DeviceName"] %>','ResetPassword')">重置密码</a>
                                                </li>
                                                <li>
                                                    <a href="javascript:void(0)" onclick="ShiftOrExpire('shift',[{DeviceID:'<%=dr["DeviceID"] %>',UserName:'<%= dr["UserName"] %>',IMEI:'<%=dr["SerialNumber"] %>',DeviceName:'<%= dr["DeviceName"] %>'}])">设备转移</a>
                                                </li>
                                                <%  if (Utils.GetSession("UserInfo").SuperAdmin == "1")
                                                    {%>
                                                <li>
                                                    <a href="javascript:void(0)" onclick="ShiftOrExpire('expire',[{DeviceID:'<%=dr["DeviceID"] %>',UserName:'<%= dr["UserName"] %>',IMEI:'<%=dr["SerialNumber"] %>',DeviceName:'<%= dr["DeviceName"] %>'}])">到期时间</a>
                                                </li>
                                                <%} %>
                                            </ul>
                                        </div>
                                        <%
                                            }
                                        %>
                                        <% if (Utils.GetSession("UserInfo").SuperAdmin == "1")
                                            { //只有超级管理员才有删除权限%>
                                        <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon icon-left" onclick="javascript:window.parent.loadModalBasic([{'DeviceID':<%=dr["DeviceID"] %>}],'<%= dr["DeviceName"].ToString() =="" ?dr["SerialNumber"]:dr["DeviceName"] %>','DeleteDevice')">删除 </a>
                                        <%}%>
                                    </td>
                                </tr>
                                <%
                                    } %>
                            </tbody>
                        </table>

                    </div>
                </div>
                <!-- Table end -->


            </div>
            <div class="tab-pane" id="profile">
                <!-- Table exporting -->
                <div class="panel panel-default" style="margin-left: -25px; margin-top: -25px;">

                    <div class="panel-body" style="margin-top: -30px;">

                        <script type="text/javascript">
                            jQuery(document).ready(function ($) {
                                $("#Table1").dataTable({
                                    dom: "<'row'<'col-sm-5'l><'col-sm-7'Tf>r>" +
                                         "t" +
                                         "<'row'<'col-xs-6'i><'col-xs-6'p>>",
                                    aoColumns: [
                                           { bSortable: false },
                                               null, null, null, null, null, null, null],
                                    tableTools: {
                                        sSwfPath: "assets/js/datatables/tabletools/copy_csv_xls_pdf.swf"
                                    }
                                });
                                var $state = $("#Table1 thead input[type='checkbox']");
                                $("#Table1").on('draw.dt', function () {
                                    cbr_replace();
                                    $state.trigger('change');
                                });
                                // Script to select all checkboxes
                                $state.on('change', function (ev) {
                                    var $chcks = $("#Table1 tbody input[type='checkbox']");
                                    if ($state.is(':checked')) {
                                        $chcks.prop('checked', true).trigger('change');
                                    }
                                    else {
                                        $chcks.prop('checked', false).trigger('change');
                                    }
                                });

                                jQuery('#modal-6').modal('show', { backdrop: 'static' });

                                $("span:contains('Copy')").parent().remove();
                                $("span:contains('Print')").parent().remove();
                                var html = "&nbsp&nbsp&nbsp&nbsp <div class=\"btn-group\"><button type=\"button\" class=\"btn btn-purple btn-sm\" onclick=\"addUsers()\">" + allPage.addUser + "</button>" +
                                     "<button type=\"button\" class=\"btn btn-purple btn-sm\" onclick=\"shiftUser()\">转移用户</button></div>";

                                $("#Table1_length").append(html);


                            });

                            function addUsers() {
                                parent.addUsers('<%= zTreeSelectUserID%>', '<%= DeviceInfoRow == null ?"":DeviceInfoRow["UserName"]  %>');
                            }
                            function shiftUser() {
                                var chcks = $("#Table1 tbody input[type='checkbox']:checked");
                                var arr = [];
                                for (var i = 0; i < chcks.length; i++) {
                                    arr.push({ UserID: $(chcks[i]).attr("UserID"), LoginName: $(chcks[i]).attr("LoginName"), UserName: $(chcks[i]).attr("UserName"), UserType: $(chcks[i]).attr("UserType") });
                                }
                                parent.ShiftUsers(arr);
                            }
                        </script>

                        <table class="table table-bordered table-striped" id="Table1">
                            <thead>
                                <tr>
                                    <th class="no-sorting">
                                        <input type="checkbox" class="cbr" />
                                    </th>
                                    <th>客户名</th>
                                    <th>登录名</th>
                                    <th>类型</th>
                                    <th>联系人</th>
                                    <th>电话</th>
                                    <th>设备数量</th>
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th></th>
                                    <th>客户名</th>
                                    <th>登录名</th>
                                    <th>类型</th>
                                    <th>联系人</th>
                                    <th>电话</th>
                                    <th>设备数量</th>
                                    <th>操作</th>
                                </tr>
                            </tfoot>
                            <tbody>
                                <%
                                    int j = 0;
                                    string UserType = default(string);
                                    foreach (DataRow dr in UserTable.Rows)
                                    {
                                        UserType = dr["UserType"].ToString() == "2" ? "经销商" : "用户";
                                %>
                                <tr name="<%= dr["UserName"] %>" userid="<%=dr["UserID"]%>">
                                    <td>
                                        <input type="checkbox" class="cbr" userid="<%=dr["UserID"]%>" loginname="<%= dr["LoginName"] %>" username="<%=dr["UserName"]%>" usertype="<%= UserType %>" />
                                        <%= ++j %> 
                                    </td>
                                    <td><%= dr["UserName"] %></td>
                                    <td><%= dr["LoginName"] %></td>
                                    <td><%= UserType %></td>
                                    <td><%= dr["FirstName"] %></td>
                                    <td><%= dr["CellPhone"] %></td>
                                    <td><%= dr["DevicesCount"] %></td>
                                    <td>
                                        <a href="javascript:void(0)" onclick="window.parent.UpdateUserInfo(this)" class="btn btn-info btn-sm btn-icon icon-left">修改</a>
                                        <% if (Utils.GetSession("UserInfo").UserType == "2")
                                            { %>
                                        <a href="javascript:void(0)" onclick="window.parent.loadModalBasic('<%=dr["UserID"]%>','<%= dr["UserName"] %>','DeleteUser')" class="btn btn-danger btn-sm btn-icon icon-left">删除 </a>
                                        <%}%>
                                    </td>
                                </tr>
                                <%
                                    } %>
                            </tbody>
                        </table>

                    </div>
                </div>
                <!-- Table end -->
            </div>
            <div class="tab-pane" id="messages" style="width: 600px;">

                <script type="text/javascript">
                    $(function () {

                        $("a[href=#messages]").on("click", function () {
                            $.ajax({
                                url: "AjaxService/AjaxService.ashx?action=SearchUserInfo",
                                type: "post",
                                data: { "userid": "<%=zTreeSelectUserID %>" },
                                dataType: "json",
                                error: function () { },
                                success: function (data) {
                                    var v = data[""][0];
                                    $("#txtUserName").val(v.UserName);
                                    $("#txtLoginName").val(v.LoginName);
                                    $("input[name=usertype]:eq(" + (v.UserType - 1) + ")").attr("checked", "checked");
                                    $("#txtContact").val(v.FirstName);
                                    $("#txtPhone").val(v.CellPhone);
                                    $("#txtAddress").val(v.Address1);
                                }
                            });
                        });

                        $("#btnSaveDeviceUserInfo").on("click", function () {
                            var data = {
                                "UserName": $("#txtUserName").val(),   "userType": $("input[name=usertype]:checked").val(), "FirstName": $("#txtContact").val()
                                , "CellPhone": $("#txtPhone").val(), "Address1": $("#txtAddress").val(), "userid": "<%=zTreeSelectUserID %>"
						            }
						            $.ajax({
						                url: "AjaxService/AjaxService.ashx?action=SaveDeviceUserInfo",
						                type: "post",
						                data: { "data": JSON.stringify(data) },
						                dataType: "json",
						                error: function () { },
						                success: function (reg) {
						                    if (reg.success) {
						                        parent.toastrMessage("opts_success", reg.msg, "提示");
						                        $("#btnCommandClose").trigger("click");
						                    } else {
						                        parent.toastrMessage("opts_waming", reg.msg, "提示");

						                    }
						                }
						            });
						        });

                    });
                            function SaveNewDevice() {
                                $("#ErrorImei").remove();
                                $("#lableMsg").remove();
                                // $("#ModalShowMap", window.parent.document);
                                var imeis = $("#txtIMEI").val();
                                if ($("#selectDictionary>option:selected").val() == "") {
                                    parent.toastrMessage("opts_waming", "请选择设备型号", "提示"); return;
                                } if (imeis.length == 0) {
                                    parent.toastrMessage("opts_waming", "请输入IMEI号", "提示"); return;
                                }
                                if (isNaN($("#txtSpeeding").val())) {
                                    parent.toastrMessage("opts_waming", "请输入有效的速度", "提示"); return;
                                }
                                $("#btnAddDevice").attr({ "disabled": "disabled" });
                                var newData = {
                                    Model: $("#selectDictionary>option:selected").val(),
                                    Imei: $("#txtIMEI").val(),
                                    SpeedLimit: $("#txtSpeeding").val(),
                                    UserID: "<%=zTreeSelectUserID %>"
                                };

                                $.ajax({
                                    url: "AjaxService/AjaxService.ashx?action=adddevice",
                                    type: "post",
                                    data: { "data": JSON.stringify(newData) },
                                    dataType: "text",
                                    error: function () { parent.toastrMessage("opts_waming", "添加设备时出错，请检查数据是否输入正确", "提示"); $("#btnAddDevice").removeAttr("disabled"); },
                                    success: function (reg) {
                                        var regData = JSON.parse(reg);
                                        var ErrorImeiText = "";
                                        if (regData.success) {
                                            parent.toastrMessage("opts_success", "共" + regData.SumLength + "条数据，成功" + regData.successLength + "条", "提示");
                                        } else {
                                            parent.toastrMessage("opts_waming", "添加失败！！", "失败提示");
                                        }
                                        $("#btnAddDevice").removeAttr("disabled");
                                        if (regData.errorImei && regData.errorImei.length > 0) {
                                            for (var i = 0; i < regData.errorImei.length; i++) {
                                                ErrorImeiText += regData.errorImei[i].imei + "\n";
                                            }
                                            var textarea = $(" <label for=\"field-7\" class=\"control-label\" id=\"lableMsg\">共" + regData.SumLength + "条数据，有" + (regData.SumLength - regData.successLength) + "条失败，</label><textarea class=\"form-control autogrow\" id=\"ErrorImei\"> </textarea>").val(ErrorImeiText);
                                            $("#txtIMEI").parent().append(textarea);
                                        }

                                    }
                                });
                            }
                            function ExcelImportClick() {
                                ///  $("body").append('<form id="frmUpload" action="post" method="post" enctype="multipart/form-data"> <input type="file" id="txtExcelImport"  style="visibility:hidden"/></form>');
                                // $("#txtExcelImport").trigger("click");
                                // $("#frmUpload").submit();
                                parent.fileUpload();
                            }
                </script>
                <!-- Modal 6 (Long Modal)-->
                <%--<div class="modal-header">
				 	<h4 class="modal-title">Modal Content is Responsive - 资料</h4> 
				</div>--%>
                <form action="/" method="post" onsubmit="return false">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="field-1" class="control-label">客户名称</label>
                                    <input type="text" class="form-control" id="txtUserName" placeholder="客户名称" />
                                </div>
                            </div>
                            <div class="col-md-6">

                                <div class="form-group">
                                    <label for="field-2" class="control-label">登录名</label>

                                    <input type="text" class="form-control" id="txtLoginName" placeholder="登录名" disabled />
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">

                                <div class="form-group">
                                    <label for="field-4" class="control-label">类型</label><br />
                                    <% 
                                        string disabled = "";
                                        if (zTreeSelectUserID == Utils.GetSession("UserInfo").UserID)
                                        {
                                            disabled = "disabled";
                                        } %>
                                    <label class="radio-inline">
                                        <input type="radio" name="usertype" value="1" checked <%=disabled %> />
                                        用户
                                    </label>
                                    <label class="radio-inline">
                                        <input type="radio" name="usertype" value="2" <%=disabled %> />
                                        经销商
                                    </label>

                                </div>

                            </div>

                            <div class="col-md-4">

                                <div class="form-group">
                                    <label for="field-5" class="control-label">联系人</label>

                                    <input type="text" class="form-control" id="txtContact" placeholder="联系人" />
                                </div>

                            </div>

                            <div class="col-md-4">

                                <div class="form-group">
                                    <label for="field-6" class="control-label">电话</label>

                                    <input type="text" class="form-control" id="txtPhone" placeholder="联系电话" />
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">

                                <div class="form-group">
                                    <label for="field-3" class="control-label">地址</label>
                                    <input type="text" class="form-control" id="txtAddress" placeholder="地址" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <%--  <button type="reset" class="btn btn-white" data-dismiss="modal">重置</button>--%>
                        <button type="button" class="btn btn-info" id="btnSaveDeviceUserInfo">保存</button>
                    </div>
                </form>
            </div>
            <div class="tab-pane" id="settings" style="width: 600px;">
                <form>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-md-12">
                                <label for="field-7" class="control-label">选择设备</label>
                                <select class="form-control" id="selectDictionary">
                                    <option value=""></option>
                                    <%foreach (DataRow dr in DictionaryTable.Rows)
                                        {
                                    %>
                                    <option value="<%=dr["DataValue"] %>"><%=dr["DataText"] %></option>
                                    <%
                                        }
                                    %>
                                </select>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12">

                                <div class="form-group no-margin">
                                    <label for="field-7" class="control-label">超速</label>
                                    <input type="text" class="form-control" id="txtSpeeding" placeholder="超速：多少公里每小时" />
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">

                                <div class="form-group no-margin">
                                    <label for="field-7" class="control-label">IMEI号</label>
                                    <textarea class="form-control autogrow" id="txtIMEI" placeholder="请输入IMEI号，多个IMEI号一行一个"></textarea>
                                </div>

                            </div>
                        </div>


                    </div>

                    <div class="modal-footer">
                        <button type="reset" class="btn btn-white" data-dismiss="modal">重置</button>
                        <button type="button" class="btn btn-info" id="btnExcelImport" onclick="ExcelImportClick()">Excel导入</button>
                        <button type="button" class="btn btn-primary" id="btnAddDevice" onclick="SaveNewDevice()">保存</button>
                    </div>
                </form>
            </div>
        </div>


    </div>




    <div class="modal fade" id="ModalFormSales" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">用户信息</h4>
                </div>

                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-6">

                            <div class="form-group">
                                <label for="field-1" class="control-label">客户名称</label>
                                <input type="text" class="form-control" id="field-1" placeholder="name">
                            </div>

                        </div>

                        <div class="col-md-6">

                            <div class="form-group">
                                <label for="field-2" class="control-label">登录账号</label>
                                <input type="text" class="form-control" id="field-2" placeholder="LoginName">
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">

                            <div class="form-group">
                                <label for="field-4" class="control-label">联系人</label>
                                <input type="text" class="form-control" id="field-4" placeholder="Contacts" />
                            </div>

                        </div>

                        <div class="col-md-4">

                            <div class="form-group">
                                <label for="field-5" class="control-label"></label>
                                <input type="text" class="form-control" id="field-5" placeholder="Phone" />
                            </div>

                        </div>

                        <div class="col-md-4">

                            <div class="form-group">
                                <label for="field-6" class="control-label">邮箱</label>

                                <input type="text" class="form-control" id="field-6" placeholder="E-Mail" />
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">

                            <div class="form-group">
                                <label for="field-3" class="control-label">地址</label>
                                <input type="text" class="form-control" id="field-3" placeholder="Address" />
                            </div>

                        </div>
                    </div>


                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal">关闭</button>
                    <button type="button" class="btn btn-info">保存并关闭</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Imported styles on this page -->
    <link rel="stylesheet" href="assets/js/select2/select2.css" />
    <link rel="stylesheet" href="assets/js/select2/select2-bootstrap.css" />
    <link rel="stylesheet" href="assets/js/multiselect/css/multi-select.css" />
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
<%--    <script src="assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>--%>
    <script src="assets/js/typeahead.bundle.js"></script>
    <script src="assets/js/handlebars.min.js"></script>
    <script src="assets/js/multiselect/js/jquery.multi-select.js"></script>



    <script src="assets/js/datatables/js/jquery.dataTables.min.js" charset="utf-8"></script>
    <!-- Imported scripts on this page -->
    <script src="assets/js/rwd-table/js/rwd-table.min.js"></script>

    <!-- JavaScripts initializations and stuff -->
    <script src="assets/js/xenon-custom.js"></script>

    <!-- Imported scripts on this page -->
    <script src="assets/js/datatables/dataTables.bootstrap.js"></script>
    <script src="assets/js/datatables/yadcf/jquery.dataTables.yadcf.js"></script>
    <script src="assets/js/datatables/tabletools/dataTables.tableTools.min.js"></script>
</body>
</html>
