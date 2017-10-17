<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="MG_GPS.Monitor.History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>历史轨迹</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        body, html, #allmap {
            width: 100%;
            height: 100%;
            overflow: hidden;
            margin: 0;
            font-family: "微软雅黑";
        }

        body, html, #sosomap {
            overflow: hidden;
            margin: 0;
            font-family: "微软雅黑";
        }
        .p {
            margin:1px; padding:1px
        }
    </style>
    <script src="/Scripts/jquery-1.8.3.js"></script>
    <!-- bootstrap - css -->
    <link href="/Scripts/B-JUI/BJUI/themes/css/bootstrap.css" rel="stylesheet" />
    <!-- core - css -->
    <link href="/Scripts/B-JUI/BJUI/themes/css/style.css" rel="stylesheet" />

    
    <link href="/Scripts/B-JUI/BJUI/themes/blue/core.css"  rel="stylesheet"/>
 <%--   <link href="/Scripts/B-JUI/BJUI/themes/blue/core.css" rel="stylesheet" />--%>

    <link href="/Scripts/B-JUI/BJUI/plugins/kindeditor_4.1.10/themes/default/default.css" rel="stylesheet" />
    <link href="/Scripts/B-JUI/BJUI/plugins/colorpicker/css/bootstrap-colorpicker.min.css" rel="stylesheet" />
    <link href="/Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.css" rel="stylesheet" />
    <link href="/Scripts/B-JUI/BJUI/plugins/bootstrapSelect/bootstrap-select.css" rel="stylesheet" />
    <link href="/Scripts/B-JUI/BJUI/themes/css/FA/css/font-awesome.min.css" rel="stylesheet" />
       

    <script src="/Scripts/B-JUI/BJUI/js/bjui-all.js"></script>
    <script src="/Scripts/B-JUI/BJUI/js/bjui-alertmsg.js"></script>
    <script src="/Scripts/B-JUI/BJUI/js/bjui-taskbar.js"></script>
    <!-- bootstrap plugins -->
    <script src="/Scripts/B-JUI/BJUI/plugins/bootstrap.min.js"></script>
    <script src="/Scripts/B-JUI/BJUI/plugins/bootstrapSelect/bootstrap-select.min.js"></script>
    <script src="/Scripts/B-JUI/BJUI/plugins/bootstrapSelect/defaults-zh_CN.min.js"></script>
    
<!-- nice validate -->
<script src="/Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.js"></script>
<script src="/Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.themes.js"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script>
    <link href="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.js"></script>
  
       <script type="text/javascript" src="http://map.qq.com/api/js?v=2.exp&libraries=convertor&key=d84d6d83e0e51e481e50454ccbe8986b"></script>

    <script src="/Scripts/Ajax.js"></script>
    <script src="/Scripts/Monitor/map/map.js"></script>
    <script src="/Scripts/Monitor/History.js"></script>
    <script src="/Scripts/mg_public.js"></script>
    
      <script src="/Scripts/Language/language02-zh-cn.js"></script>
    <link href="/Scripts/daterangepicker/css/normalize.css" rel="stylesheet" />
    <link href="/Scripts/daterangepicker/css/default.css" rel="stylesheet" />
    <link href="/Scripts/daterangepicker/css/daterangepicker.css" rel="stylesheet" />
    <script src="/Scripts/daterangepicker/js/moment.min.js"></script>
    <script src="/Scripts/daterangepicker/js/jquery.daterangepicker.js"></script>
    <script src="/Scripts/Monitor/map/markerIcon.js"></script>
    <script src="/Scripts/Monitor/BDLuShu.js"></script>

    <link href="/Scripts/Slider/dist/jquery.nstSlider.css" rel="stylesheet" />
    <script src="/Scripts/Slider/dist/jquery.nstSlider.js"></script>
    
</head>
<body>
    <input type="hidden" value="<%= IMEI %>" id="DeviceID" />
    <input type="hidden" value="<%=UserID %>" id="UserID" />
    <input type="hidden" value="<%=DeviceName %>" id="DeviceName" />
        
    <div style="height: 100%; width:100%; border: 0px solid red;">
        <div style="height: 50px; border-bottom: 1px solid pink">
            <blockquote>
                <p style="border:0px solid blue;width:670px;float:left;">
                    <label for="j_custom_issuedate" class="control-label x85">选择日期：</label>
                    <input id="playDate" size="33" value=""  />
                   <%-- <label for="j_custom_issuedate" class="control-label x85">开始日期：</label>
                    <input type="text" name="custom.issuedate" id="txtStartTime" value="2016-02-21" data-toggle="datepicker" size="15" class="form-control ok" aria-required="true" style="padding-right: 15px; width: 150px;" />
                    <label for="j_custom_issuedate" class="control-label x85">结束日期：</label>
                    <input type="text" name="custom.issuedate" id="txtEndTime" value="2016-02-21" data-toggle="datepicker" size="15" class="form-control ok" aria-required="true" style="padding-right: 15px; width: 150px;" />--%>

                    <button type="button" class="btn btn-default btn-nm" data-icon="play" id="btnPlay" data-toggle="ajaxload" data-target="#myLoadDiv1"><i class="fa fa-play"></i> 播放</button>
                    <button type="button" class="btn btn-default btn-nm" data-icon="pause" id="btnPause"><i class="fa fa-pause"></i> 暂停</button>
                    <button type="button" class="btn btn-default btn-nm" data-icon="stop" id="btnStop"><i class="fa fa-stop"></i> 停止</button>
                   
                    <button type="button" class="btn-green" data-loadingmask="false" data-toggle="dialog" data-id="mydialog4" data-url="dialog-normal.html" data-title="停留地点"
                         data-on-load="doc_dialog_onLoad" data-before-close="doc_dialog_beforeClose" data-on-close="doc_dialog_onClose" style="display:none"  >打开dialog</button>
                     
                  </p>
                   <label for="j_custom_issuedate" class="control-label x85" style="float:left;margin-top:10px;">播放速度：</label>
                    <div class="nstSlider" data-range_min="1000" data-range_max="4000"
                           data-cur_min="500" data-cur_max="0" style="float:left;top:5px;"> 
                        <div class="bar"></div>
                        <div class="leftGrip"></div>
                    </div>
                    <div class="leftLabel" style="margin-top:10px;" />
            </blockquote>
        </div>
    <%--    <div class="bjui-maskProgress bjui-ajax-mask" style="z-index: 2111;display:none;"><i class="fa fa-cog fa-spin"></i>&nbsp;&nbsp;正在努力加载数据，请稍等...<div class="progressBg"><div class="progress"></div></div></div>
        <div class="bjui-maskBackground bjui-ajax-mask" style="z-index: 1111;display:none;"></div>--%>
         <div id="allmap"></div>
         <div id="sosomap" style="display:none"></div>
    </div>
         
</body>
</html>
