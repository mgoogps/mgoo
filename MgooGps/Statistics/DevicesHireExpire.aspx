<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DevicesHireExpire.aspx.cs" Inherits="MgooGps.Statistics.DevicesHireExpire" %>

 <%@ Import Namespace="MgooGps.com" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/js/Language/language02-<%= Utils.language %>.js"></script>
     
	<link rel="stylesheet" href="../assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="../assets/css/fonts/fontawesome/css/font-awesome.min.css"/> 
    
       <script src="../js/bootgrid/jquery-bootgrid-master/lib/jquery-1.11.1.min.js"></script> 
    <link href="../js/bootgrid/css/bootstrap.css" rel="stylesheet" /> 
    <link href="../js/bootgrid/jquery-bootgrid-master/dist/jquery.bootgrid.css" rel="stylesheet" /> 
    <script src="../js/bootgrid/js/moderniz.2.8.1.js"></script>
           <%--layer 弹出 --%>
    <script src="../js/lib/layer/layer.js"></script>
    <script src="../js/mg_public.js"></script> 
    <script src="../js/CoureName.js"></script>
   <script src="../js/fun.js"></script>
      <script src="../js/Statistic.js"></script>
     <style>
html { overflow-x:hidden; }
  @-webkit-viewport {
            width: device-width;
        }

        @-moz-viewport {
            width: device-width;
        }

        @-ms-viewport {
            width: device-width;
        }

        @-o-viewport {
            width: device-width;
        }

        @viewport {
            width: device-width;
        } 
        body {
            padding-top: 0px;
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
<body>
    <input type="hidden" id="userid" value="<%= Utils.GetSession("UserInfo").UserID %>" />
    <input type="hidden" id="day" value="<%= day %>" />
    <input type="hidden" id="type" value="<%=type %>" />
     <header id="header" class="navbar navbar-default navbar-fixed-top" style="visibility:hidden; ">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <span class="navbar-brand" data-i18n="title"></span>
            </div>
            <nav id="menu" class="navbar-collapse collapse" role="navigation">
              <%--  <ul class="nav navbar-nav navbar-right">
                   <%-- <li><a href="#">Basic Demo</a></li>--%>
                </ul>
            </nav>
        </div>
    </header>
    
        <div class="container-fluid">
  	<div class="row"  >
				<div class="col-md-12"  > 
					<div class="panel panel-default">
						<div class="panel-body"> 
							<div class="table-responsive" data-pattern="priority-columns" data-focus-btn-icon="fa-asterisk" data-sticky-table-header="false"
                                 data-add-display-all-btn="false" data-add-focus-btn="false">							
								<table  style="color:black;" class="table table-small-font table-bordered table-striped" data-toggle="table" id="DHETable">
									<thead>
										<tr> 
                                            <th data-column-id="rowid">序号</th> 
                                            <th data-column-id="DeviceName">设备名称</th> 
                                            <th data-column-id="SerialNumber" data-formatter="SerialNumber">IMEI号</th>
                                            <th data-column-id="CarNum" data-visible="false">车牌号</th> 
											<th data-column-id="Model"data-sortable="false" data-visible="false">型号</th> 
                                            <th data-column-id="PhoneNum" data-visible="false">设备号码</th>
                                            <th data-column-id="CellPhone">联系号码</th> 
                                            <th data-column-id="UserName">所属用户</th>
                                            <th data-column-id="UserCellPhone">用户号码</th>
                                            <th data-column-id="Created">创建时间</th>
											<th data-column-id="ActiveDate">激活时间</th>
											<th data-column-id="HireExpireDate">到期时间</th> 
                                            <th data-column-id="LastCommunication" data-visible="false">与平台通信时间</th> 
                                            <th data-column-id="LastCommunication" data-order="asc" data-formatter="LastCommunication">离线时间</th> 
                                            <th data-column-id="Operating" data-formatter="Operating" data-sortable="false">操作</th> 
										</tr>
									</thead>
									  
								</table>
							
							</div> 
						</div>
					
					</div>
				</div>
			</div> 
         </div>
     <iframe src="/404.html" id="downloadIframe" style="display:none; "></iframe>
    <script src="../js/bootgrid/js/bootstrap.js"></script> 
    <script src="../js/bootgrid/jquery-bootgrid-master/dist/jquery.bootgrid.js"></script>
     <script>
         $(function () {
             // $("th[data-column-id=CarNum]").attr("data-visible", "false");
          
             if ($("#type").val() != "") {
                 var returnBtn = $("<input type=\"button\" value=\"返回统计\"/> ").click(function () {
                     $("#iframepage", parent.document).attr("src", "Statistics/Statistics.aspx"); //alert( $("#type").val());
                 });
                 $("#header .navbar-brand").html(returnBtn).append("  <strong>&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp  " + ret($("#type").val()) + "</strong>");
                 $("#header").css("visibility", "visible");
                 $("body").css("padding-top", "70px");
               
             }
             parent.loading(100);
             var data = [];
             var grid;
             loadGrid();
             //根据用户所选值导出不同数据
             var type;
             //alert($("#type").val());
             if ($("#type").val() == "6") {
                 type = "overdue";
             } else if ($("#type").val() =="") {
                 type = "alldevices";
             } else if ($("#type").val() == "1") {
                 type="adevices"
             } else if ($("#type").val()=="2") {
                 type="onlinedevices"
             } else if ($("#type").val()=="3") {
                 type = "overtime"
             } else if ($("#type").val() == "4") {
                 type="offlinedevices"
             } else if ($("#type").val() == "5") {
                 type="use"
             } else if ($("#type").val() == "7") {
                 type = "notenabled"
             }
             var btnImport = $(' <button class="btn btn-default" type="button" title="导出到Excel"><span class="icon glyphicon glyphicon-import"></span></button>').on("click", function () {
                 $.ajax({
                     url: "/AjaxService/AjaxService.ashx?action=importexcel",
                     data: { "type": type, "userid": $("#userid").val(), "day": $("#day").val() },
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
             if ($("#type").val() == "") { 
                 var html = [];
                 html.push(' <div class="dropdown btn-group"><button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" title="多少天内过期设备"> <span class="dropdown-text" id="spanSelected">10天</span> <span class="caret"></span></button>');
                 html.push(' <ul class="dropdown-menu pull-right" role="menu" id="ulDayHire"> <li class="active" aria-selected="true"><a data-action="10" class="dropdown-item dropdown-item-button">10天内过期的设备</a></li> ');
                 html.push(' <li aria-selected="false"><a data-action="30" class="dropdown-item dropdown-item-button">30天内过期的设备</a></li> ');
                 html.push(' <li aria-selected="false"><a data-action="60" class="dropdown-item dropdown-item-button">60天内过期的设备</a></li> ');
                 html.push(' <li aria-selected="false"><a data-action="90" class="dropdown-item dropdown-item-button">90天内过期的设备</a></li> ');
                 html.push(' <li aria-selected="false"><a data-action="auto" class="dropdown-item dropdown-item-button">自定义</a></li></ul></div> ');
                 $("div.actions.btn-group").append(html.join(''));
                 if ($("#day").val() != "") {
                     $("#spanSelected").text($("#day").val() + "天");
                     $("#ulDayHire li").attr("aria-selected", "false").removeClass("active");
                     $("#ulDayHire li a[data-action=" + $("#day").val() + "]").parent().attr({ "aria-selected": "true", "class": "active" });
                 }
                 $("#ulDayHire li").on("click", function () {
                     $("#ulDayHire li").attr("aria-selected", "false");
                     $(this).attr("aria-selected", "true");
                     var day = $(this).find("a").attr("data-action");
                     $("#spanSelected").text(day);
                     var offset = $("#spanSelected").offset();
                     if (day == "auto") {
                         var dvi = '<div id="divCustomDay" style="position:absolute;top:' + (offset.top + 28) + 'px;left:' + (offset.left - 10) + 'px;height:auto;width:113px;background-color:#D6D6D6; border:1px #3276B1 solid"><table><tr><td> <input type="number" max="365" min="1" style="width:60px;float:left;height:25px;" id="txtDay"/><input type="button" id="btnSumbit" value="确定" style="float:left;height:25px" /></td></tr></table> </div>';
                         $("body").append(dvi).one("mousedown", function () { $("#divCustomDay").remove(); });

                         $("#divCustomDay").hover(function () {
                             $('body').unbind('mousedown');
                         }, function () {
                             $('body').one('mousedown', function () {
                                 $("#divCustomDay").remove();
                             });
                         });
                         $("#btnSumbit").on("click", function () {
                             day = $("#txtDay").val()
                             if (isNaN(day) || day > 365 || day <= 0) {
                                 parent.toastrMessage("opts_waming", "请输入1-365之间的有效数字", "提示");
                                 return;
                             }
                             window.location.href = "DevicesHireExpire.aspx?day=" + day;
                         });
                         if ($("#day").val() != "") {

                             $("#txtDay").val($("#day").val());
                             $("#spanSelected").text($("#day").val() + "天");
                         }
                         return;
                     } else {
                         window.location.href = "DevicesHireExpire.aspx?day=" + day;
                     }
                 });
             } else if ($("#type").val() == 4) {
                 <% if (Utils.GetSession("UserInfo").UserType =="2")
                    {%> 
                 $.ajax({
                     url: "/AjaxService/AjaxService.ashx?action=GetUsersOffLineDevicCount",
                     type: "POST",
                     data: {  },
                     dataType: "json",
                   //  error: function (err) { parent.toastrMessage("opts_waming", "查询停留详单时出错", allPage.toastrTitle1); },
                     success: function (res) {
                         var userHtml = [];
                         userHtml.push("<select class='form-control' id='selectByUsers'  style='float:left;width:20%; font-size:12px;'> ")
                         userHtml.push("<option></option>");
                         userHtml.push("<optgroup label=\"用户名称\">");
                         $.each(res[""], function (k,v) {
                             userHtml.push("<option value=\""+v.UserID+"\">"+v.UserName+"</option>");
                         }); 
                         userHtml.push("</optgroup>");
                         userHtml.push("</select>");
                         $("#DHETable-header .row .actionBar").append(userHtml.join(''));
                         $("#selectByUsers").select2({
                             placeholder: '输入用户名称查询...',
                             allowClear: true
                         }).on('select2-open', function () {
                         }).on("change", function () { 
                             $("#DHETable").bootgrid("reload"); 
                         });
                     }
                 });  
                   <% }%>
             }
         });
      
         function loadGrid( ) { 
               grid = $("#DHETable").bootgrid({
                 keepSelection: true,
                 //pagination: 50,
                 selection: false,
                 search: "搜索..",
                 multiSelect: false,
                 width: 600,
                 rowSelect: false,
                 ajax: true,
                 padding: 5,
                 styling: {
                     pagination: 'pagination pagination-sm'
                 },
                 requestHandler: function (request) {
                     if (request.sort) {
                         request.sortBy = Object.keys(request.sort)[0]; //this only gets first sort param
                         request.sortDir = request.sort[request.sortBy];
                         delete request.sort
                     }
                     return request;
                 },
                 rowCount: [10, 20, 50, 100, 200],
                 url: function () {
                     var url = "/AjaxService/AjaxService.ashx?action=GetDeviceHireExpire&uid=" + $("#userid").val() + "&day=" + $("#day").val();
                     if ($("#selectByUsers").length > 0) {
                         url += "&byUser=" + $("#selectByUsers").select2("val");
                     }
                     if ($("#type").val() != "") {
                         url += "&t=" + $("#type").val();
                     }
                     return url;
                 } ,
                 formatters: {
                     "Operating": function (column, row) {
                         var update = "<a href=\"javascript:void(0)\" onclick=\"window.parent.UpdateDevice(" + row.DeviceID + ")\" class=\"btn btn-primary btn-sm btn-icon icon-left\">修改</a> "
                         var hire = "";
                         <%  if (Utils.GetSession("UserInfo").SuperAdmin == "1")
	                      {%>
                         hire = "<a href=\"javascript:void(0)\" onclick=\"window.parent.DeviceShiftOrExpire('expire',[{DeviceID:'" + row.DeviceID + "',UserName:'" + row.UserName + "',IMEI:'" + row.SerialNumber + "',DeviceName:'" + row.DeviceName + "'}])\" class=\"btn btn-info btn-sm btn-icon icon-left\">到期时间</a>";
                         <%}%>
                         return update + hire;
                     },
                     "SerialNumber": function (column, row) {
                         var row = "<a href=\"/main.aspx?imei=" + row.SerialNumber + "&userid=" + row.UserID + "\" target=\"_blank\" title=\"点击监控该设备\">" + row.SerialNumber + "</a>";
                         return row;
                     }, "LastCommunication": function (column, row) {
                         if ($("#type").val() == 4) {
                             var difftime = (new Date((GetCurrentDate().split("."))[0].replace(/-/g, "/")).getTime() - new Date((row.LastCommunication.split("."))[0].replace(/-/g, "/")).getTime()) / 1000;

                             return MinuteToHour(difftime / 60);
                         } else {
                             return row.LastCommunication;
                         }
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
                     var row, received;
                     for (var i = 0; i < selectedRows.length; i++) {
                         row = selectedRows[i];
                         // Array.contains is an extension of bootgrid
                         if (!data.contains(function (item) { return item.id === row.id; })) {
                             received = grid.find("#" + row.id + "-received").val();
                             data.push({ id: row.id, received: received || 0 });
                         }
                     }
                 }).on("deselected.rs.jquery.bootgrid", function (e, deselectedRows) {
                     var row;
                     for (var i = 0; i < deselectedRows.length; i++) {
                         row = deselectedRows[i];
                         for (var j = 0; j < data.length; j++) {
                             if (data[j].id === row.id) {
                                 data.splice(j, 1);
                                 return;
                             }
                         }
                     }
                 });
         }

         function deleted(tr, data, name, type) {
             window.parent.loadModalBasic(data, name, type, tr);
         }
        
         function Recovery(tr, deviceid, imei) {
             window.parent.loadModalBasic(deviceid, imei, "RecoveryDevices", tr);
         }
         function ret(t) { 
             if (t == 1) {
                 return "全部设备";
             } else if (t == 2) {
                 return "当前在线设备数";
             } else if (t == 3) {
                 return "超过七天设备数";
             } else if (t == 4) {
                 return "离线设备数";
             } else if (t == 5) {
                 return "使用设备数";
             } else if (t == 6) {
                 return "已过期设备数";
             } else if (t == 7) {
                 return "未启用设备数";
             } 
         }
    </script>

            <!-- Imported styles on this page --> 
	<link rel="stylesheet" href="/assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="/assets/js/select2/select2-bootstrap.css"/>
	<link rel="stylesheet" href="/assets/js/multiselect/css/multi-select.css"/>  
    <!-- Imported scripts on this page -->  
	<script src="/assets/js/select2/select2.min.js"></script>
</body>
</html>
