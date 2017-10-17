<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tracking.aspx.cs" Inherits="MgooGps.Tracking" %>
 <%@ Import Namespace="MgooGps.com" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
 
    <title></title>
    <script src="js/Language/language02-<%= Utils.language %>.js"></script>
       <link href="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.js"></script> 
    <style type="text/css">
	body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑"; }
    body, html,#sosomap { overflow: hidden;margin:0;font-family:"微软雅黑";   }
	</style>
    <script src="js/jquery-1.8.3.js"></script>
    <script src="js/CoureName.js"></script>
    <script src="js/fun.js"></script>

    <script src="http://map.qq.com/api/js?v=2.exp&libraries=convertor&key=d84d6d83e0e51e481e50454ccbe8986b"></script>
   

     
	<link rel="stylesheet" href="assets/css/xenon-components.css" /> 
     
   
    	<!-- Bottom Scripts -->  
     
<script src="js/mg_public.js"></script>
    <script src="assets/js/toastr/toastr.min.js"></script>
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
<body onload="init()"> 
    <input value="<%=DeviceID %>" id="deviceid" type="hidden" />
    <input value ="<%=UserID %>" id="id" type="hidden" />
    <input value="<%=DeviceName %>" id="DeviceName" type="hidden" />
    <input value="<%=SpeedLimit %>" id="Overspeed" type="hidden" />
      <div id="allmap" style="height:100%;width:100%;"></div>
    <div id="sosomap" style="display:none; "></div>
     <script src="js/Tracking.js"></script>
</body>
</html>
 
