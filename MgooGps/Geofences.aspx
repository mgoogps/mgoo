<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Geofences.aspx.cs" Inherits="MgooGps.Geofences" %>
 <%@ Import Namespace="MgooGps.com" %>
<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>电子围栏</title>
    <script src="js/Language/language02-<%= Utils.language %>.js"></script>
      <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script>
      <style type="text/css">
	      body, html{width: 100%;height: 100%;overflow: hidden;}
          .old1 {
             background-color:#E2E2E2;
          }
          .old2 {
             background-color:#DDE5E8;
          }
          .ztree li span {
            line-height:20px!important;
          }
         
          /*.ztree li a {
            height:20px;
          }*/
	</style>
   
	<link rel="stylesheet" href="assets/css/bootstrap.css"/>
	<link rel="stylesheet" href="assets/css/xenon-core.css"/>

    <script src="js/jquery-1.8.3.js"></script>
    <script src="js/CoureName.js"></script>
   <%-- <script src="js/GeoUtils_min.js"></script>--%>

     <link href="zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" /> 
    <script src="zTree_v3/js/jquery.ztree.core-3.5.js"></script>  

    
    <script src="js/common/GeoFences.js"></script>
    <script>
        var _hmt = _hmt || [];
        (function () {
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?63805c8d49dd83eac3a21c31f34f9352";
            var s = document.getElementsByTagName("script")[0];
            s.parentNode.insertBefore(hm, s);
        })();
    </script>
    <style type="text/css">
       .nav.nav-tabs + .tab-content {
            padding: 0px;
            padding-left: 5px;
        }
       table.gridtable {
	        font-family: verdana,arial,sans-serif;
	        font-size:11px;
	        color:#333333;
	        border-width: 1px;
	        border-color: #666666;
	        border-collapse: collapse;
        }
        table.gridtable th {
	        border-width: 1px;
	        padding: 5px;
	        border-style: solid;
	        border-color: #666666;
	        background-color: #dedede;
        }
        table.gridtable td {
	        border-width: 1px;
	        padding: 3px;
	        border-style: solid;
	        border-color: #666666;
	        background-color: #ffffff;
        }
    </style>

</head>
<body>
    <div style="width: 100%; height: 100%;">

        <div style="width: 450px; height: 100%;   min-width: 250px;">
          <%--  <button class="btn btn-info" style="margin-left: 20px; margin-top: 10px" id="btnAddGeofences">新增电子围栏</button>
                    <br />--%>
            <div style="margin-top:30px;"></div>
            <% if (isCreateFence)
                {%>
                    <input type="text" class="form-control" id="txtName" placeholder="请输入电子围栏名称..." style="width: 200px; margin-left: 5px;" />
                    <button class="btn btn-turquoise" style="left: 205px; position: absolute; top: 30px;" id="btnSaveGeofences">保 存</button>
                    <button class="btn btn-purple" style="left: 253px; position: absolute; top: 30px;" id="btnShowAddSAR">添加行政区域</button>

                    <input type="text" class="form-control" id="txtSAR" placeholder="例如：广东省东莞市" style="width: 230px; margin-left: 5px; display: none" />
                    <button class="btn btn-turquoise" style="left: 240px; position: absolute; top: 62px; display: none" id="btnAddSAR">添加</button>
                <%} %>
           

            <ul class="nav nav-tabs" style="padding-top: 5px;">
                <li class="active">
                    <a href="#devices" data-toggle="tab">
                        <span class="visible-xs"><i class="fa-home"></i></span>
                        <span class="hidden-xs">车辆</span>
                    </a>
                </li>
                <li class="">
                    <a href="#users" data-toggle="tab">
                        <span class="visible-xs"><i class="fa-home"></i></span>
                        <span class="hidden-xs">用户</span>
                    </a>
                </li>
            </ul>
            <div class="tab-content">
                <div id="devices" class="tab-pane active">
                   
                    <div id="nav" style="max-height: 300px; overflow: auto;">
                       
                        <div style="height: 250px; overflow: auto;">
                            <table class="gridtable" style="width: 100%; height: 200px; table-layout: fixed;">
                                <thead>
                                    <tr>
                                        <th style="width: 60px;">
                                            <input type="checkbox" id="chkSelectAll" />全选</th>
                                        <th>名称</th>
                                        <th>IMEI</th>
                                    </tr>
                                </thead>
                                <%
                                    for (int i = 0; i < DeviceDt.Rows.Count; i++)
                                    {
                                %>
                                <tr>
                                    <td style="padding-left: 10px;">
                                        <input type="checkbox" name="chkDeviceName" value="<%=DeviceDt.Rows[i]["DeviceID"] %>" course="<%=DeviceDt.Rows[i]["Course"] %>"
                                            icon="<%= DeviceDt.Rows[i]["Icon"] %>" status="<%= DeviceDt.Rows[i]["status"]%>" lat="<%= DeviceDt.Rows[i]["BaiduLat"]%>"
                                            lng="<%= DeviceDt.Rows[i]["BaiduLng"] %>" devicename="<%=DeviceDt.Rows[i]["DeviceName"] %> " offline ="<%= DeviceDt.Rows[i]["offLineMi"] %>" />
                                        <%= (i+1) %>
                                    </td>
                                    <td>
                                        <%=DeviceDt.Rows[i]["DeviceName"] %> 
                                    </td>
                                    <td>
                                        <%=DeviceDt.Rows[i]["SerialNumber"] %> 
                                    </td>
                                    <%--  <td>
                                               <% 
                                                    i++;
                                                    if ( i < DeviceDt.Rows.Count)
                                                    {
                                                        %>
                                                             <input type="checkbox" name="chkDeviceName" value="<%= DeviceDt.Rows[i]["DeviceID"] %>" course="<%=DeviceDt.Rows[i]["Course"] %>"
                                                                  icon="<%= DeviceDt.Rows[i]["Icon"] %>" status="<%= DeviceDt.Rows[i]["status"]%>" lat="<%=DeviceDt.Rows[i]["BaiduLat"] %>"
                                                                  lng ="<%= DeviceDt.Rows[i]["BaiduLng"] %>"devicename="<%=DeviceDt.Rows[i]["DeviceName"] %> "  /><%= DeviceDt.Rows[i]["DeviceName"] %> 
                                                       <%
                                                    }
                                                 %> 
                                             </td> --%>
                                </tr>
                                <%
                                    }
                                %>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="users" class="tab-pane">
                    <div style="border: 1px solid #666666; width: 100%; height: 250px; min-height: 250px;max-height:250px;overflow-x:hidden;overflow-y:scroll;">
                        <ul id="ulTree" class="ztree" style="min-height: 200px; max-height:250px;"></ul>
                    </div>
                </div>
              </div>
            <div style="height:calc(100% - 383px); width: 100%; overflow-y: auto">
                <table class="table table-bordered table-condensed table-hover" id="geofencesDataTable" style="table-layout: fixed; width: 100%;">
                    <thead style="background-color: #ABBED0">
                        <tr>
                            <th style="width: 8%">#</th>
                            <th style="width: 25%">名称</th>
                            <th style="width: 20%">对象</th>
                            <th style="width: 32%">时间</th>
                            <% if (isCreateFence)
                                { %>
                            <th style="width: 15%">操作</th>
                            <% } %>
                        </tr>
                    </thead>
                    <tbody >
                        <% if (dt != null)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {//,#allmap 
                                    String old = "";
                                    if (i % 2 == 0)
                                        old = "old1";
                                    else
                                        old = "old2";
                        %>
                        <tr onclick="_select(this)" id='row_<%=dt.Rows[i]["GeofenceID"].ToString() %>' name="trUserID_<%=  isAobo ? dt.Rows[i]["BoundBindIds"].ToString():"" %>" fencetype="<%= dt.Rows[i]["FenceType"].ToString() %>" class="<%= old %>">
                            <td><%= i+1%>
                                <input type="hidden" id='point_row_<%=dt.Rows[i]["GeofenceID"].ToString() %>' value='<%= dt.Rows[i]["Description"].ToString() %>' />
                            </td>
                            <td style="word-wrap: break-word;">
                                <%=dt.Rows[i]["FenceName"].ToString() %> 
                            </td>
                            <td>
                                <%= dt.Rows[i]["FenceType"].ToString() == "1" ? "设备" : "用户" %>
                            </td>
                            <td>
                                <%=dt.Rows[i]["Created"].ToString()  %> 
                            </td>
                            <% if (isCreateFence)
                                { %>
                            <td>
                                <button onclick="deleted(<%=dt.Rows[i]["GeofenceID"].ToString() %>)" class="btn btn-gray" style="height: 25px; line-height: 10px; overflow: hidden; background-color: #DBEAF9;">删除</button>
                            </td>
                            <% } %>
                        </tr>
                        <% }
                            }   %>
                    </tbody>
                </table>

            </div>
    
        </div>
        <div id="allmap" style="left: 450px; top: 0px; bottom: 0px; right: 0px; position: absolute;"></div>

        <div class="modal fade" id="modalInfo">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">提示</h4>
                    </div>

                    <div class="modal-body" id="modalInfoText">
                        Hello I am a Modal!
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">取消</button>
                        <button type="button" class="btn btn-info">确定</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <input type="hidden" value="<%=DeviceID %>" id="DeviceID" />  
    <input type="hidden" value="<%=UserID %>" id="UserID" />
    <input type="hidden" value="<%=DeviceName %>" id="DeviceName" />
    <input type="hidden" value="" id="lat" />
    <input type="hidden" value="" id="lng" />
</body>
</html>
    <!-- Bottom Scripts and 弹出框 -->
	<script src="assets/js/bootstrap.min.js"></script> 

 <!--    进度条-->
 	<script src="assets/js/TweenMax.min.js"></script>  
 
 
<!-- Imported scripts on this page -->
	<script src="assets/js/toastr/toastr.min.js"></script> 
    <script src="js/mg_public.js"></script> 
	<!-- JavaScripts initializations and stuff -->
	<script src="assets/js/xenon-custom.js"></script>

<script src="assets/js/xenon-api.js"></script>
 