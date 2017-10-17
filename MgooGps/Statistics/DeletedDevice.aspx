<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeletedDevice.aspx.cs" Inherits="MgooGps.Statistics.DeletedDevice" %>
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
     <div class="col-md-12"> 
               <table id="grid" class="table table-bordered table-condensed table-hover table-striped tb" aria-busy="false">
                  <thead>
                        <tr> 
                            <th data-column-id="rowid" data-order="asc" data-formatter="rowid" data-identifier="true">序号</th>
                            <th data-column-id="DeviceName">设备名称</th>
                            <th data-column-id="SerialNumber">IMEI号</th> 
                            <th data-column-id="UserName" >所属用户</th>
                            <th data-column-id="Operating" data-formatter="Operating" data-sortable="false">操作</th>
                        </tr>
                    </thead>
                </table>
           </div>
     <script src="../js/bootgrid/jquery-bootgrid-master/lib/jquery-1.11.1.min.js"></script> 
    <script src="../js/bootgrid/js/bootstrap.js"></script> 
    <script src="../js/bootgrid/jquery-bootgrid-master/dist/jquery.bootgrid.js"></script>
     
     <script>

         $(function () {
             parent.loading(100);
             var data = [];
             var grid = $("#grid").bootgrid({
                 keepSelection: false,
                 //pagination: 50, 
                 search: "搜索..",
                 selection: true,
                 multiSelect: true,
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
                 url: "/AjaxService/AjaxService.ashx?action=getdeleteddevices",
                 formatters: {
                     "Operating": function (column, row) {
                          
                         var deleted = "<a href=\"javascript:void(0)\" onclick=\"javascript:deleted(" + row.rowid + ",[{'DeviceID':" + row.DeviceID + "}],'" + row.DeviceName + "','DestroyDevices')\" class=\"btn btn-danger btn-sm btn-icon icon-left\">删除</a> "
                         var recovery = "<a href=\"javascript:void(0)\" onclick=\"Recovery(this,[{'DeviceID':" + row.DeviceID + "}],'" + row.SerialNumber + "')\" class=\"btn btn-info btn-sm btn-icon icon-left\">恢复</a>";
                         return deleted + recovery; //"<input type=\"button\" value=\"" + row.rowId + "\"/>" // "<input id=\"" + row.id + "-received\" type=\"text\" class=\"received\" value=\"" + row.rowId + "\" />";
                     }, rowid: function (column,row){
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
             var btnImport = $(' <button class="btn btn-default" type="button" title="删除选中的数据..."><span class="icon glyphicon glyphicon-remove"></span></button>').on("click", function () { 
                 var chks = $("table tbody input[name=select][type=checkbox]:checked"); 
                 var vals = [];
                 for (var i = 0; i < chks.length; i++) {  
                     vals.push(chks[i].value);
                 }
                 chks = [];
                 var trs = $("table tbody tr");
                 for (var i = 0; i < trs.length; i++) { 
                     if (vals.indexOf($(trs[i]).children("td").eq(0).find("input").val())>=0)
                     {
                         var deviceid = {};
                         deviceid.DeviceID = $(trs[i]).children("td").eq(1).find("span").text();
                         chks.push(deviceid);
                     }
                 }
                 if (chks.length == 0) {
                     parent.toastrMessage("opts_danger", "请至少选择一个设备！", "提示"); return;
                 }
                 deleted(trs, chks, "选中的设备", "DestroyDevices");
             });
             $("div.actions.btn-group").append(btnImport); 
         });
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
