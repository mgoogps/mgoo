<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayBack.aspx.cs" Inherits="MgooGps.PlayBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>历史轨迹回放</title> 
 
  
    <script src="js/jquery-1.8.3.js"></script>
      <style type="text/css">
	body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑";}
    td {text-overflow:ellipsis;white-space:nowrap;}
	</style>
      <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script>
  

    <!--时间控件-->
    <link href="js/daterangepicker/bootstrap.min.css" rel="stylesheet" />
    <link href="js/daterangepicker/font-awesome.min.css" rel="stylesheet" />
    <link href="js/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
    <script type="text/javascript" src="js/daterangepicker/moment.js"></script>
    <script type="text/javascript" src="js/daterangepicker/daterangepicker.js"></script>
    <script type="text/javascript" src="js/daterangepicker/moment.min.js"></script>

    <link href="assets/css/xenon-forms.css" rel="stylesheet" /> 
	<link rel="stylesheet" href="assets/css/fonts/linecons/css/linecons.css"/>
	<link rel="stylesheet" href="assets/css/fonts/fontawesome/css/font-awesome.min.css"/>
	<link rel="stylesheet" href="assets/css/bootstrap.css"/>
	<link rel="stylesheet" href="assets/css/xenon-core.css"/>
	<link rel="stylesheet" href="assets/css/xenon-forms.css"/>
	<link rel="stylesheet" href="assets/css/xenon-components.css"/>
	<link rel="stylesheet" href="assets/css/xenon-skins.css"/>
	<link rel="stylesheet" href="assets/css/custom.css"/>
    	<!-- Imported styles on this page -->
	<link rel="stylesheet" href="assets/js/jquery-ui/jquery-ui.min.css"/>

	<!-- Bottom Scripts -->
	<script src="assets/js/bootstrap.min.js"></script>
	<script src="assets/js/TweenMax.min.js"></script>
	<script src="assets/js/resizeable.js"></script>
	<script src="assets/js/joinable.js"></script>
	<script src="assets/js/xenon-api.js"></script>
	<script src="assets/js/xenon-toggles.js"></script>


	<!-- Imported scripts on this page -->
	<script src="assets/js/jquery-ui/jquery-ui.min.js"></script>
	<script src="assets/js/knob/jquery.knob.min.js"></script>


	<!-- JavaScripts initializations and stuff -->
	<script src="assets/js/xenon-custom.js"></script>

    <script src="js/CoureName.js"></script>
    
    <script type="text/javascript" src="js/playback/api-v.js"></script>
    <script type="text/javascript"  src="js/playback/cn.js"></script>
    <script type="text/javascript"  src="js/playback/core.js"></script>
    <script  type="text/javascript" src="js/playback/goome.maps.js"></script>
    <script  type="text/javascript" src="js/playback/playback.js"></script>
    <script  type="text/javascript" src="js/playback/underscore.js"></script>
    <script type="text/javascript"  src="js/playback/popupmarker.js"></script>
    <script>
        var _hmt = _hmt || [];
        (function() {
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?63805c8d49dd83eac3a21c31f34f9352";
            var s = document.getElementsByTagName("script")[0]; 
            s.parentNode.insertBefore(hm, s);
        })();
</script>

</head>
<body onresize="restFrame();" onload="init();">
   
    <input id="DeviceID" type="hidden" value ="<%=DeviceID %>"/>
    <input id="DeviceName" type="hidden" value="<%=DeviceName %>" />
    <input id="DeviceIMEI" type="hidden" value="<%=IMEI %>" />
    <input id="SpeedLimit" type="hidden" value="<%=SpeedLimit %>" />
    <input id="startDate" type="hidden" value="" />
    <input id="endDate" type="hidden" value="" />
    <input id="currentDay" type="hidden" value="1" />
    <input id="countDay" type="hidden" />

    <input id="dataCount" type="hidden" />

     <span id="tip" style="text-align:center;  display:none; position:absolute;z-index:5555">正在加载数据,请耐心等待.......<br /><img  src="icons/preload.gif"/></span> 
     <div class="well">

               <form class="form-horizontal"  onsubmit="return false;" > 
                     <div class="input-prepend input-group"  > 
                           <span class="add-on input-group-addon">请选择要查看的时间段 -<i class="glyphicon glyphicon-calendar fa fa-calendar"></i></span>
                           <input type="text" style="width: 250px" name="reservation" id="reservationtime" class="form-control span4" value="<%=date %>"  />
                    
                              <div style="width:800px; height:30px; margin-left:20px; float:left; padding-top:2px;">
                                    <span class="add-on input-group-addon" style="width:auto; float:left">播放速度</span>
                                                            <%-- <div style="width:200px; float:left; margin-top:10px; margin-left:10px" class="slider slider-success" data-basic="1" data-min="2" data-max="1800" data-value="120"
                                data-prefix="鈫愬揩 "   data-step="5" data-fill="#sample-field" id="slider" ></div> --%>
                                 <div class="slider slider-warning" data-min="10" data-max="500" data-value="10"  data-prefix="←快"  data-postfix="慢→"
                                      style="width:200px; float:left; margin-top:10px; margin-left:10px"></div>
                                   
                                    <button class="btn btn-success btn-icon" style="margin-left:50px;margin-top:-10px;" id="btnPlay">
									    <i class="fa-check"></i>
									    <span id="paly">播放</span>
								    </button> 
                                    <button class="btn btn-purple btn-icon" style="margin-top:-10px;" onclick="pause()">
										<i class="fa-pencil"></i>
                                        <span id="btnText">暂停</span> 
								    </button>
                                    <button class="btn btn-blue btn-icon" style="margin-top:-10px; display:none;" onclick="stop()" >
									    <i class="fa-envelope-o"></i>
							  		    <span>停止</span>
								    </button>
                                </div>

                           </div> 
                    <input id="mdTime" type="hidden" value="" />
               </form>
         
               <script type="text/javascript">

                   $(document).ready(function () {

                       $('#reservationtime').daterangepicker({
                           timePicker: true,
                           timePickerIncrement: 10,
                           format: 'YYYY-MM-DD H:mm'
                       }, function (start, end, label) {
                           $("#startDate").val($("#reservationtime").val().split("到")[0]);
                           $("#endDate").val($("#reservationtime").val().split("到")[1]); 

                           $("#countDay").val(Math.ceil(DateDiff($("#startDate").val(), $("#endDate").val()))); 

                       });
                      // $("a[data-toggle=panel]").trigger("click");
                   });
               </script> 
          
            </div>      
 
    <div id="map" style="margin-top:-20px;"></div> 
    <div style="position:absolute;bottom:0px;left:0px; width:auto; ">
   <div class="panel panel-default" style=" width:auto;">
						<div class="panel-heading">
							<h3 class="panel-title">停留列表</h3>
							
							<div class="panel-options">
								<a href="#">
									<i class="linecons-cog"></i>
								</a>
								
								<a href="#" data-toggle="panel" id="qiehuan">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								
								<a href="#" data-toggle="reload">
									<i class="fa-rotate-right"></i>
								</a>
							<%--	
								<a href="#" data-toggle="remove">
									&times;
								</a>--%>
							</div>
						</div>
						<div class="panel-body" style="height:250px;width:auto;overflow-y:scroll;overflow-x:hidden;">
						<table class="table table-bordered table-striped table-condensed table-hover" id="StopListTable" style="width:auto;">
				        	<thead>
								<tr>
                                    <th>序号</th>
									<th>开始时间</th>
									<th>结束时间</th>
									<th>停留时长</th>
									<th>经纬度</th>
                                    <th>地址</th>
								</tr>
							</thead> 
                          
							<tbody> 
							</tbody>
                           
						</table>
                            </div>
					</div>
					
				</div>      
  
</body>
</html> 
<%--<script src="js/baidumap_lushu_min.js"></script>
<script src="js/CoureName.js"></script>--%>
	<script src="assets/js/toastr/toastr.min.js"></script>
<script src="js/mg_public.js"></script>
<script type="text/javascript"> 
    function restFrame() {
        var w = document.documentElement.clientWidth;
        var h = document.documentElement.clientHeight;
        var topMenuHeight = 32;
        var divCanvas = document.getElementById("map");
        divCanvas.style.height = (h - topMenuHeight) + "px";
        divCanvas.style.width = w + "px";
    }
    var PlayBack = new PlayBack("map",<%=DeviceID %>, "GT02", <%=SpeedLimit %>);
    function init() {
        window.document.title="历史轨迹回放 - "+$("#DeviceName").val();
        restFrame();
        PlayBack.createMap("cn");
    }
    document.onmousemove = mouseCoords;

    //getMaxDate鐢熸垚瀹㈡埛绔湰鍦版椂闂?
    function getMaxDate() {
        var t = new Date();
        var maxDate = [t.getFullYear(), t.getMonth() + 1, t.getDate()].join('-');
        maxDate += ' ' + t.toLocaleTimeString();
        return maxDate;
    }
    //getMinDate鐢熸垚瀹㈡埛绔湰鍦版椂闂?
    function getMinDate() {
        var t = new Date();
        t.setMonth(t.getMonth() - 2);//鏈€灏忔椂闂村皯2涓湀
        var maxDate = [t.getFullYear(), t.getMonth() + 1, t.getDate()].join('-');
        maxDate += ' ' + t.toLocaleTimeString();
        return maxDate;
    }
     
    function getPlayback2() {  
        if (DateDiff($("#startDate").val(), $("#endDate").val()) > 90) {
            toastr.warning("    您最多只能查看前90天的历史轨迹！", "失败提示", opts_waming);  
            $("#btnPlay").removeClass("disabled"); 
            return;
        }
        PlayBack.getDataFrist($("#startDate").val(), $("#endDate").val(), speed) 
        $("#btnPlay").removeClass("disabled");//.addClass("disabled");
    }

     
    var lushu;
    var speed = 10;
    $(function () {
        $("#startDate").val($("#reservationtime").val().split("到")[0]);
        $("#endDate").val($("#reservationtime").val().split("到")[1]);
        $("#currentDay").val("1");

        $(".slider").slider({
            // slide: function (event, ui) { //婊戝姩鐨勬椂鍊欒Е鍙?
            //},
            change: function (event, ui) { //婊戝姩缁撴潫鍚庤Е鍙?
                // lushu._opts.speed = ui.value;
                speed = ui.value;
                PlayBack.Frequency =  ui.value;
            }
        });

        $("#btnPlay").click(function () {
            isPost = true;
            $(this).addClass("disabled");
            getPlayback2();
        });
    });

    // 鐧惧害鍦板浘API鍔熻兘
    var bdMap = new BMap.Map("map", { enableMapClick: false });    // 鍒涘缓Map瀹炰緥
    bdMap.centerAndZoom(new BMap.Point(114.32649, 30.616882), 6);
    bdMap.enableScrollWheelZoom(true);     //寮€鍚紶鏍囨粴杞缉鏀?

    var top_left_control = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT });// 宸︿笂瑙掞紝娣诲姞姣斾緥灏?
    var top_left_navigation = new BMap.NavigationControl();  //宸︿笂瑙掞紝娣诲姞榛樿缂╂斁骞崇Щ鎺т欢
    bdMap.addControl(top_left_control);
    bdMap.addControl(top_left_navigation);
    bdMap.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP], anchor: BMAP_ANCHOR_TOP_RIGHT }, new BMap.Size(300, 200)));  //娣诲姞 鍙充笂瑙掓贩鍚?鍦板浘
    bdMap.enableKeyboard();//鍚敤閿洏涓婁笅宸﹀彸閿Щ鍔ㄥ湴鍥?
    var geoc = new BMap.Geocoder();
    var isPost = true;
      
    function getPlayback() { 
        if (isPost)
            $("#currentDay").val("1");

        if (parseInt($("#currentDay").val()) > parseInt($("#countDay").val())) { 
            return;
        }
        $.ajax({
            url: "AjaxService/AjaxService.ashx?action=HistoryPlayback",
            data: { "DeviceID": $("#DeviceID").val(), "startDate": $("#startDate").val(), "endDate": $("#endDate").val(), "currentDay": $("#currentDay").val() },
            dataType: "json",
            Type: "POST",
            error: function (error) {  },
            success: function (data) {
                var points = [];
                $.each(data[""], function (k, v) {
                    points.push(new BMap.Point(v["BaiduLng"], v["BaiduLat"]));
                });
                if (points.length == 0) {
                    $("#currentDay").val(parseInt($("#currentDay").val()) + 1);
                    isPost = false; 
                    getPlayback();
                } 
                if (lushu != undefined) {
                    if (lushu._opts != undefined) { 
                        lushu._opts.icon = null;
                        lushu._opts.defaultContent = "aaaaaaaaaaaaaaa:" + $("#currentDay").val();
                    }
                    lushu.stop();
                }
                lushu = new BMapLib.LuShu(map, points, {
                    defaultContent: "鍟婂晩鍟?",
                    autoView: true,
                speed: speed,
                icon: new BMap.Icon("icons/lushu_car.png", new BMap.Size(52, 26), { anchor: new BMap.Size(27, 13) }),
                enableRotation: true, //鏄惁璁剧疆marker闅忕潃閬撹矾鐨勮蛋鍚戣繘琛屾棆杞?
                landmarkPois: []
            });
        $("#currentDay").val(parseInt($("#currentDay").val()) + 1);
        lushu._opts.defaultContent = "adfasdf";
        lushu.start();
        lushu.showInfoWindow();
    }
    });
    }

    var lng = 0;
    var lat = 0;

    function play() {
        //$("#startDate").val($("#reservationtime").val().split('鍒?)[0]); $("#endDate").val($("#reservationtime").val().split('鍒?)[1]); $("#currentDay").val("1");
        //map.clearOverlays();
        //map.setZoom(12);
        //if (lushu != undefined && lushu._path != undefined)
        //    lushu._path = [];//鍦╛path涓嶄负绌虹殑鎯呭喌涓嬶紝鍦ㄧ户缁挱鏀撅紝浼氬嚭鐜拌鐩栫嚎浼氱户缁鐩栬€岃溅閲嶆柊寮€濮?

        //$.ajax({
        //    url: "AjaxService/AjaxService.ashx?action=HistoryPlayback",
        //    data: { "DeviceID": $("#DeviceID").val(), "startDate": $("#reservationtime").val().split('鍒?)[0], "endDate": $("#reservationtime").val().split('鍒?)[1] },
        //    dataType: "json",
        //    Type: "POST",
        //    error: function (error) { alert("鍑洪敊浜?);},
        //    success: function (data) {
        //        points = data[""];
        //        var datalist = []; 
        //        $.each(data[""], function (k, v) {
        //            if (points.length == 1) {
        //                var pt = new BMap.Point(points[0].lng, points[0].lat);
        //                var marker = new BMap.Marker(pt);  // 鍒涘缓鏍囨敞
        //                map.addOverlay(marker);               // 灏嗘爣娉ㄦ坊鍔犲埌鍦板浘涓?
        //                marker.setAnimation(BMAP_ANIMATION_BOUNCE); //璺冲姩鐨勫姩鐢?
        //                var opts = {
        //                    width: 200,     // 淇℃伅绐楀彛瀹藉害
        //                    height: 100,     // 淇℃伅绐楀彛楂樺害 
        //                    title: v["DeviceName"], // 淇℃伅绐楀彛鏍囬
        //                    enableMessage: false,//璁剧疆鍏佽淇℃伅绐楀彂閫佺煭鎭?
        //                    message: ""
        //                }
        //                //geoc.getLocation(pt, function (rs) {
        //                //    var addComp = rs.addressComponents;
        //                //    var address = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;

        //                var infoWindow = new BMap.InfoWindow(v["DeviceUTCTime"] + "</br>缁忓害:" + parseFloat(v["BaiduLat"]).toFixed(5) + ",绾害:" + parseFloat(v["BaiduLat"]).toFixed(5) + "</br>鏂瑰悜:" + GetCoureName(v["Course"]) + ",閫熷害:" + v["Speed"] + "</br>", opts);
        //                marker.addEventListener("click", function () {
        //                    map.openInfoWindow(infoWindow, pt); //寮€鍚俊鎭獥鍙?
        //                });
        //                // });

        //            }
        //            // points.push(new BMap.Point(v["BaiduLng"], v["BaiduLat"]));
        //            datalist.push(new BMap.Point(v["BaiduLng"], v["BaiduLat"]));
        //        });

        //        var polyline = new BMap.Polyline(datalist, { strokeColor: "#735F25", strokeWeight: 6, strokeOpacity: 0.5 }); //鍒涘缓鎶樼嚎
        //         map.addOverlay(polyline); //缁樺埗鎶樼嚎 

        //        var list = runArr(0, 2000); 
        //        lushu = new BMapLib.LuShu(map, list, {
        //            defaultContent: "鍟婂晩鍟?,
        //            autoView: true,
        //            speed: speed, //15000,
        //            icon: new BMap.Icon("icons/lushu_car.png", new BMap.Size(52, 26), { anchor: new BMap.Size(27, 13) }),
        //            enableRotation: true, //鏄惁璁剧疆marker闅忕潃閬撹矾鐨勮蛋鍚戣繘琛屾棆杞?
        //            landmarkPois: []
        //        });
        //        lushu._opts.speed=10000;
        //        lushu._opts.defaultContent = "adfasdf";
        //        lushu.start();
        //        lushu.showInfoWindow();
        //    }
        //});
    }
     
    
    function pause() {
        PlayBack.stopPlay();
    }
    function stop() {
        lushu.stop();
    }
</script>


 