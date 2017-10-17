<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenewalsExport.aspx.cs" Inherits="MgooGps.Statistics.RenewalsExport" %>

 <%@ Import Namespace="MgooGps.com" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <link href="../js/bootgrid/css/bootstrap.css" rel="stylesheet" /> 
    <link href="../js/bootgrid/jquery-bootgrid-master/dist/jquery.bootgrid.css" rel="stylesheet" /> 
    <script src="../js/bootgrid/js/moderniz.2.8.1.js"></script>
   
</head>
<body>
       <iframe src="/404.html" id="downloadIframe" style="display:none; "></iframe>
     <div class="col-md-12"> 
               <table id="grid" class="table table-bordered table-condensed table-hover table-striped tb" aria-busy="false">
                  <thead>
                        <tr> 
                             <th data-column-id="rowid" data-order="asc" data-formatter="rowid" data-identifier="true">序号</th>
                             <th data-column-id="UserName" >所属用户</th>
                             <th data-column-id="DeviceName">设备名称</th>
                             <th data-column-id="SerialNumber">IMEI号</th> 
                             <th data-column-id="ActiveDate">激活时间</th> 
                             <th data-column-id="HireExpireDate">到期时间</th> 
                             <th data-column-id="PhoneNum">流量卡号</th> 
                             <th data-column-id="LastCommunication">最后通信时间</th>
                        </tr>
                    </thead>
                </table>
           </div>
     <script src="../js/bootgrid/jquery-bootgrid-master/lib/jquery-1.11.1.min.js"></script> 
    <script src="../js/bootgrid/js/bootstrap.js"></script> 
    <script src="../js/bootgrid/jquery-bootgrid-master/dist/jquery.bootgrid.js"></script>
     
             <!--时间控件-->
    <link href="../js/daterangepicker/bootstrap.min.css" rel="stylesheet" />
    <link href="../js/daterangepicker/font-awesome.min.css" rel="stylesheet" />
    <link href="../js/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/daterangepicker/moment.js"></script>
    <script type="text/javascript" src="../js/daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="../js/daterangepicker/moment.min.js"></script>

      
    <link href="../js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" /> 
    <script src="../js/lib/ligerUI/js/core/base.js"></script> 
    <script src="../js/lib/ligerUI/js/plugins/ligerComboBox.js"></script>
    <script src="../js/lib/ligerUI/js/plugins/ligerResizable.js"></script>
    <script src="../js/lib/ligerUI/js/plugins/ligerTree.js"></script>
    <script src="../js/DeviceOper.js"></script>
     <script> 
         var urlObj ={};
         urlObj.userid =  "";
         urlObj.date = "<%= date%>";
         window.onload = function () {

             $("#grid-header div.actionBar").prepend("<div class='search form-group'><div class='input-group'> <input type='text' class='form-control span4' style=\"width: 180px; height:31px;font-size:12px;\" id='reservationtime' value='<%= date%>' /></div><div>");
             $("#grid-header div.actionBar").prepend("<div class='search form-group'><div class='input-group'> <input type=\"text\" id=\"ligerTree\" autocomplete=\"off\" onkeyup=\"ligerTreeSearch()\" placeholder=\"输入名称搜索...\"/> </div><div>");
             $('#reservationtime').daterangepicker({
                 timePicker: false,
                 timePickerIncrement: 10,
                 format: 'YYYY-MM-DD', separator: " ~ "
             }, function (start, end, label) {
                 urlObj.date = $("#reservationtime").val();
                 $('#grid').bootgrid('reload');
             });

             var ligerObj = new ligerTreeObject();
             ligerObj.getLigerTreeData(function (dataList) {
                 var treeData = [];
                 $(dataList).each(function (k, v) {
                     treeData.push({ id: v["UserID"], pid: v["ParentID"], text: v["UserName"], icon: (v.UserType == 1 ? "/js/lib/ligerUI/skins/icons/memeber.gif" : "/js/lib/ligerUI/skins/icons/customers.gif") });
                 });
                 ligerObj.ligerTreeInit($("#ligerTree"), treeData, function (newValue) {
                     urlObj.userid = newValue;
                     $('#grid').bootgrid('reload');
                 });
                 $("#ligerTree").attr("readonly", false).css({ "height": "25px", "width": "200px", "border-bottom ": "1px solid" });
                 $(".l-tree").css({ "width": "100%" });
                 $(".l-box-select-inner").css({ "height": "200px" });
                 $(".l-text-combobox").css({ "height": "25px", "width": "200px" }).find(".l-trigger").css({ "height": "25px" });
                 $(".l-trigger.l-trigger-cancel").css("margin-top", "0px");
                 $(".l-tree .l-tree-icon-none img").css({ "width": "16px", "height": "16px", "margin": "3px" });
             });
         }
         $(function () { 
             parent.loading(100);
             var data = [];
             var grid = $("#grid").bootgrid({
                 keepSelection: false,
                 //pagination: 50, 
                 search: "搜索..",
                 selection: false,
                 multiSelect: false,
                 rowSelect: false,
                 width: 600, 
                 ajax: true,
                 padding: 5,
                 styling: {
                     pagination: 'pagination pagination-sm' //{ "uid": $("#userid").val(),"day":$("#day").val() },
                 }, requestHandler: function (request) {
                     if (request.sort) {
                         request.sortBy = Object.keys(request.sort)[0]; //this only gets first sort param
                         request.sortDir = request.sort[request.sortBy];
                         delete request.sort
                     }
                     return request;
                 },
                 rowCount: [ 10, 20, 50, 100, 200], 
                 url: function () { 
                     return "/AjaxService/AjaxService.ashx?action=renewalsexport&userid=" + urlObj.userid + "&date=" + urlObj.date;
                 },
                 formatters: {
                     //"LastCommunication": function (column, row) { 
                     //    var deleted = "<a href=\"javascript:void(0)\" onclick=\"javascript:deleted(" + row.rowid + ",[{'DeviceID':" + row.DeviceID + "}],'" + row.DeviceName + "','DestroyDevices')\" class=\"btn btn-danger btn-sm btn-icon icon-left\">删除</a> "
                     //    var recovery = "<a href=\"javascript:void(0)\" onclick=\"Recovery(this,[{'DeviceID':" + row.DeviceID + "}],'" + row.SerialNumber + "')\" class=\"btn btn-info btn-sm btn-icon icon-left\">恢复</a>";
                     //    return deleted + recovery; //"<input type=\"button\" value=\"" + row.rowId + "\"/>" // "<input id=\"" + row.id + "-received\" type=\"text\" class=\"received\" value=\"" + row.rowId + "\" />";
                     //},
                     rowid: function (column, row) {
                         var html = row.rowid+"<span style='display:none'>"+row.DeviceID+"</span>";
                         return html;
                     }
                 }
             }).on("loaded.rs.jquery.bootgrid", function (e) {
                 //grid.find(".btn-danger").on("click", function (e) {
                 //    console.log(e);
                 //  //  alert("You1 pressed edit on row: " + $(this).data("row-id"));
                 //}).end().find(".btn-info").on("click", function (e) {
                 //    alert("You2 pressed delete on row: " + $(this).data("row-id"));
                 //});
                 // Resets the selected data array on reload, searching, sorting or changing page
                 //data = [];
                 //grid.on("keyup", "input.received", function () {
                 //    e.stopPropagation();

                 //    // Array.first is an extension of bootgrid
                 //    var $this = $(this),
                 //        rowId = +$this.attr("id").split("-")[0], // "+" to convert the a string to an integer
                 //        item = data.first(function (item) { return item.id === rowId; });
                 //    if (item != null) {
                 //        item.received = $this.val();
                 //    }
                 //});
             }).on("selected.rs.jquery.bootgrid", function (e, selectedRows) {
                
                 //var row, received;
                 //for (var i = 0; i < selectedRows.length; i++) {
                 //    row = selectedRows[i]; 
                 //    // Array.contains is an extension of bootgrid
                 //    if (!data.contains(function (item) { return item.id === row.id; })) {
                 //        received = grid.find("#" + row.id + "-received").val();
                 //        data.push({ id: row.id, received: received || 0 });
                 //    }
                 //} 
             }).on("deselected.rs.jquery.bootgrid", function (e, deselectedRows) {
                 //var row;
                 //for (var i = 0; i < deselectedRows.length; i++) {
                 //    row = deselectedRows[i];
                 //    for (var j = 0; j < data.length; j++) {
                 //        if (data[j].id === row.id) {
                 //            data.splice(j, 1);
                 //            return;
                 //        }
                 //    }
                 //}
             });
             var btnImport = $(' <button class="btn btn-default" type="button" title="导出到Excel"><span class="icon glyphicon glyphicon-import"></span></button>').on("click", function () {
                 $.ajax({
                     url: "/AjaxService/AjaxService.ashx?action=importexcel",
                     data: { "type": "RenewalsExport", "userid": urlObj.userid, "st": urlObj.date.split('~')[0],"et":urlObj.date.split('~')[1] },
                     type: "POST",
                     success: function (data) {
                         data = JSON.parse(data);
                         if (data.success) {
                             var src = "/AjaxService/Log/" + data.FileName;
                             $("#downloadIframe").attr("src", src);
                         }
                     },
                     error: function (err) { console.log(err); }
                 });
             });
             $("div.actions.btn-group").append(btnImport);
         
         });
      
         function getUrl(UserID, Date) {
             var url = "";
             return "/AjaxService/AjaxService.ashx?action=renewalsexport&userid=" + UserID + "&date=" + Date;
             function getUrla(UserID,Date) {
               
             }
            // return getUrla;
         }
         function deleted(tr, data, name, type) {
             if (!isNaN(tr)) { 
                 tr = $("tr[data-row-id=" + tr + "]") ;
             } 
             window.parent.loadModalBasic(data, name, type, tr);
         }
         function reload() {
             $("#grid").bootgrid("reload");
         }
         function Recovery(tr, deviceid, imei) {
             window.parent.loadModalBasic(deviceid, imei, "RecoveryDevices", tr);
         }
    </script>
</body>
</html>
