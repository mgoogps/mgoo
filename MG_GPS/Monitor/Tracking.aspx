<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tracking.aspx.cs" Inherits="MG_GPS.Monitor.Tracking" %>
  
<%@ Import Namespace="MG_BLL" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
 
    <title>实时跟踪</title> 
    <script src="/Scripts/Language/language02-zh-cn.js"></script>
       <link href="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.js"></script> 
    <style type="text/css">
	body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑"; }
    body, html,#sosomap { overflow: hidden;margin:0;font-family:"微软雅黑";   }
	</style> 
    <!-- bootstrap - css -->
<link href="/Scripts/B-JUI/BJUI/themes/css/bootstrap.css" rel="stylesheet"/>
<!-- core - css -->
<link href="/Scripts/B-JUI/BJUI/themes/css/style.css" rel="stylesheet"/>    
<!-- core - css --> 
<link href="/Scripts/B-JUI/BJUI/themes/blue/core.css" id="bjui-link-theme" rel="stylesheet">


    <script src="/Scripts/Monitor/map/map.js"></script>
    <script src="/Scripts/jquery-1.8.3.js"></script> 
    <script src="http://map.qq.com/api/js?v=2.exp&libraries=convertor&key=d84d6d83e0e51e481e50454ccbe8986b"></script>

    <script src="/Scripts/B-JUI/BJUI/js/bjui-core.js"></script>
<script src="/Scripts/B-JUI/BJUI/js/bjui-regional.zh-CN.js"></script>
<script src="/Scripts/B-JUI/BJUI/js/bjui-frag.js"></script>
<script src="/Scripts/B-JUI/BJUI/js/bjui-extends.js"></script>

    <script src="/Scripts/B-JUI/BJUI/js/bjui-all.js"></script> 
     <script src="/Scripts/B-JUI/BJUI/js/bjui-alertmsg.js"></script>
    <script src="/Scripts/B-JUI/BJUI/js/bjui-taskbar.js"></script>
    <!-- bootstrap plugins -->
<script src="/Scripts/B-JUI/BJUI/plugins/bootstrap.min.js"></script>
<script src="/Scripts/B-JUI/BJUI/plugins/bootstrapSelect/bootstrap-select.min.js"></script>
<script src="/Scripts/B-JUI/BJUI/plugins/bootstrapSelect/defaults-zh_CN.min.js"></script>
    
<link href="/Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.css" rel="stylesheet"/>
<script src="/Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.js"></script>
<script src="/Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.themes.js"></script>
<script src="/Scripts/mg_public.js"></script>
    <script src="/Scripts/Monitor/map/markerIcon.js"></script>
    <script src="/Scripts/Ajax.js"></script>
</head>
<body onload="init()"> 
    <input value="<%= DeviceID %>" id="deviceid" type="hidden" />
    <input value ="<%= UserID %>" id="id" type="hidden" />
    <input value="<%= DeviceName %>" id="DeviceName" type="hidden" />
    <input value="<%= SpeedLimit %>" id="Overspeed" type="hidden" />
    <div id="allmap" style="height:100%;width:100%;"></div>
    <div id="sosomap" style="display:none; "></div> 
    
    <input type="hidden" value="<%= Utils.GetSessionUserID() %>" id="txtSessionID" />

    <script src="/Scripts/Monitor/Tracking.js"></script>
</body>
</html>
 
