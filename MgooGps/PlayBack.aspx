<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayBack.aspx.cs" Inherits="MgooGps.PlayBack" %>
 <%@ Import Namespace="MgooGps.com" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>历史轨迹回放</title> 
 <script type="text/javascript" src="js/Language/language02-<%= Utils.language %>.js"></script>
  
    <script type="text/javascript" src="js/jquery-1.8.3.js"></script>
      <style type="text/css">
	body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑";}
    td {text-overflow:ellipsis;white-space:nowrap;}
       body, html,#sosomap { overflow: hidden;margin:0;font-family:"微软雅黑";   }
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
    
<%--    <script type="text/javascript" src="js/playback/api-v.js"></script>
    <script type="text/javascript"  src="js/playback/cn.js"></script>
    <script type="text/javascript"  src="js/playback/core.js"></script>
    <script  type="text/javascript" src="js/playback/goome.maps.js"></script>
    <script  type="text/javascript" src="js/playback/playback.js"></script>
    <script  type="text/javascript" src="js/playback/underscore.js"></script>
    <script type="text/javascript"  src="js/playback/popupmarker.js"></script>--%>
   <%-- <script src="js/baidumap_lushu_min.js"></script>--%>
    <script src="js/BDLuShu.js"></script>
   <%-- <script type="text/javascript" src="http://api.map.baidu.com/library/LuShu/1.2/src/LuShu.js" ></script>--%>
  <%-- <script src="js/lushu.js"></script>--%>
  <%--   <script type="text/javascript" src="http://api.map.baidu.com/library/LuShu/1.2/src/LuShu_min.js"></script>--%>
       <script src="http://map.qq.com/api/js?v=2.exp&libraries=convertor&key=d84d6d83e0e51e481e50454ccbe8986b"></script>
  
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
<body   onload="init();">
   
    <input id="DeviceID" type="hidden" value ="<%=DeviceID %>"/>
    <input id="DeviceName" type="hidden" value="<%=DeviceName %>" />
    <input id="DeviceIMEI" type="hidden" value="<%=IMEI %>" />
    <input id="SpeedLimit" type="hidden" value="<%=SpeedLimit %>" />
    <input id="startDate" type="hidden" value="" />
    <input id="endDate" type="hidden" value="" />
    <input id="currentDay" type="hidden" value="1" />
    <input id="countDay" type="hidden" />

    <input id="dataCount" type="hidden" />

     <span id="tip" style="text-align:center;  display:none; position:absolute;z-index:5555;background-color:#fff">正在加载数据,请耐心等待.......<br /><img  src="icons/preload.gif"/></span> 
     <div class="well">

               <form class="form-horizontal"  onsubmit="return false;" > 
                     <div class="input-prepend input-group"  > 
                           <span class="add-on input-group-addon">请选择要查看的时间段 -<i class="glyphicon glyphicon-calendar fa fa-calendar"></i></span>
                           <input type="text" style="width: 250px" name="reservation" id="reservationtime" class="form-control span4" value="<%=date %>"  />
                    
                              <div style="width:800px; height:30px; margin-left:20px; float:left; padding-top:2px;">
                                    <span class="add-on input-group-addon" style="width:auto; float:left">播放速度</span>
                                                            <%-- <div style="width:200px; float:left; margin-top:10px; margin-left:10px" class="slider slider-success" data-basic="1" data-min="2" data-max="1800" data-value="120"
                                data-prefix="鈫愬揩 "   data-step="5" data-fill="#sample-field" id="slider" ></div> data-prefix="←快" --%>
                                 <div class="slider slider-warning" data-min="500" data-max="4000" data-value="500"   data-postfix="米/秒"
                                      style="width:200px; float:left; margin-top:10px; margin-left:10px"></div>
                                   
                                    <button class="btn btn-success btn-icon" style="margin-left:50px;margin-top:-10px;" id="btnPlay">
									    <i class="fa-play"></i>
									    <span id="paly">播放</span>
								    </button> 
                                    <button class="btn btn-purple btn-icon" style="margin-top:-10px;" id="btnPause"  >
										<i class="fa-pause"></i>
                                        <span id="btnText">暂停</span> 
								    </button>
                                    <button class="btn btn-blue btn-icon" style="margin-top:-10px;" id="btnStop" >
									    <i class="fa-stop"></i>
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
 
    <div id="map" style="margin-top:-20px;width:100%;height:100%;"></div> 
     <div id="sosomap" style="display:none"></div>

    <div style="position:absolute;bottom:0px;left:0px; width:auto;  ">
   <div class="panel panel-default" style=" width:auto;">
						<div class="panel-heading">
							<h3 class="panel-title">停留列表</h3>
							
							<div class="panel-options">
							 
								<a href="#" data-toggle="panel" id="qiehuan">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								
								 
							<%--	
								<a href="#" data-toggle="remove">
									&times;
								</a>--%>
							</div>
						</div>
						<div class="panel-body" style="height:250px;width:auto;overflow-y:scroll;overflow-x:hidden; ">
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
 
	<script src="assets/js/toastr/toastr.min.js"></script>
<script src="js/mg_public.js"></script>
<script src="js/fun.js"></script>
<script type="text/javascript"> 
    var bdMap = new BMap.Map("map", { enableMapClick: false });
    bdMap.centerAndZoom(new BMap.Point(114.32649, 30.616882), 6);
    bdMap.enableScrollWheelZoom(true);
    bdMap.addControl(new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT }));
    bdMap.addControl(new BMap.NavigationControl());
    bdMap.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP], anchor: BMAP_ANCHOR_TOP_RIGHT }, new BMap.Size(300, 200)));
    bdMap.enableKeyboard(); 
    var geoc = new BMap.Geocoder();

    function restFrame() {
        var w = document.documentElement.clientWidth;
        var h = document.documentElement.clientHeight;
        var topMenuHeight = 32;
        var divCanvas = document.getElementById("map");
        divCanvas.style.height = (h - topMenuHeight) + "px";
        divCanvas.style.width = w + "px"; 
    }
    //var PlayBack = new PlayBack("map",<%=DeviceID %>, "GT02", <%=SpeedLimit %>);
    var deviceName = "";
    var pano;
    function init() {
        $("#tip").css({ "left": ($("body").width() / 2), "top": ($("body").height() / 2) });

        deviceName = $("#DeviceName").val();
        window.document.title = "历史轨迹回放 - " + deviceName; 
        pano = new qq.maps.Panorama(document.getElementById('sosomap'), {
            pano: '',
            disableMove: false,
            disableFullScreen: false,
            pov: {
                heading: 20,
                pitch: 15
            },
            zoom: 1
        });
    }
   // document.onmousemove = mouseCoords;

    //getMaxDate 
    function getMaxDate() {
        var t = new Date();
        var maxDate = [t.getFullYear(), t.getMonth() + 1, t.getDate()].join('-');
        maxDate += ' ' + t.toLocaleTimeString();
        return maxDate;
    }
    //getMinDate 
    function getMinDate() {
        var t = new Date();
        t.setMonth(t.getMonth() - 2);// 
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
    var speed = 500;
    var stopTable;
    var num = 0;
    $(function () {
        $("#startDate").val($("#reservationtime").val().split("到")[0]);
        $("#endDate").val($("#reservationtime").val().split("到")[1]);
       // $("#currentDay").val("1");
   
        $(".slider").slider({
            // slide: function (event, ui) { //拖动时
            //},
            change: function (event, ui) { //拖动后
                if (lushu._opts) {
                    lushu._opts.speed = ui.value;

                }
                speed = ui.value;
                //  speed = ui.value;
                // PlayBack.Frequency =  ui.value; 
            }
        });
        
        stopTable = $("#StopListTable tbody");
        $("#btnPlay").click(function () {
            isPost = true;
            $(this).addClass("disabled"); 
            play();
           // getPlayback2();
        }); 
    });
     
   
    var isPost = true;
      
    function getPlayback() {
        //if (isPost)
        //    $("#currentDay").val("1");

        //if (parseInt($("#currentDay").val()) > parseInt($("#countDay").val())) {
        //    return;
        //}
        //$.ajax({
        //    url: "AjaxService/AjaxService.ashx?action=HistoryPlayback",
        //    data: { "DeviceID": $("#DeviceID").val(), "startDate": $("#startDate").val(), "endDate": $("#endDate").val(), "currentDay": $("#currentDay").val() },
        //    dataType: "json",
        //    Type: "POST",
        //    error: function (error) { },
        //    success: function (data) {
        //        var lushu;
        //        var points = [];
        //        $.each(data[""], function (k, v) {
        //            points.push(new BMap.Point(v["BaiduLng"], v["BaiduLat"]));
        //        });
        //        if (points.length == 0) {
        //            $("#currentDay").val(parseInt($("#currentDay").val()) + 1);
        //            isPost = false;
        //            getPlayback();
        //        }
        //        if (lushu != undefined) {
        //            if (lushu._opts != undefined) {
        //                lushu._opts.icon = null;
        //                lushu._opts.defaultContent = "aaaaaaaaaaaaaaa:" + $("#currentDay").val();
        //            }
        //            lushu.stop();
        //        }
        //        lushu = new BMapLib.LuShu(bdMap, points, {
        //            defaultContent: "鍟婂晩鍟?",
        //            autoView: true,
        //            speed: speed,
        //            icon: new BMap.Icon("icons/lushu_car.png", new BMap.Size(52, 26), { anchor: new BMap.Size(27, 13) }),
        //            enableRotation: true, //鏄惁璁剧疆marker闅忕潃閬撹矾鐨勮蛋鍚戣繘琛屾棆杞?
        //            landmarkPois: []
        //        });
        //        $("#currentDay").val(parseInt($("#currentDay").val()) + 1);
        //       // lushu._opts.defaultContent = "adfasdf";
        //        lushu.start();
        //        lushu.showInfoWindow();
        //    }
        //});
    }

    var lng = 0;
    var lat = 0;
   
    var showInfoWindowData = [];
    var distance = 0;
   
    function play() {
        if (DateDiff($("#startDate").val(), $("#endDate").val()) > 90) {
            toastr.warning("    您最多只能查看前90天的历史轨迹！", "失败提示", opts_waming);
            $("#btnPlay").removeClass("disabled");
            return;
        }
        var day = DateDiff($("#reservationtime").val().split('到')[0], $("#reservationtime").val().split('到')[1]);
        if (day > 31) {
            toastr.warning("    一次最多只能播放一个月的轨迹！", "失败提示", opts_waming);
            $("#btnPlay").removeClass("disabled");
            return;
        }
        $("#startDate").val($("#reservationtime").val().split('到')[0]); $("#endDate").val($("#reservationtime").val().split('到')[1]); $("#currentDay").val("1"); 
       // bdMap.setZoom(16);
     
        try { 
            if (lushu.i && lushu.i < lushu._path.length - 1) {
                //没按pause再按start不做处理 
                if (!lushu._fromPause) {
                    $("#btnPlay").removeClass("disabled");
                    return;
                } else if (!lushu._fromStop) {
                    //按了pause按钮,并且再按start，直接移动到下一点
                    //并且此过程中，没有按stop按钮
                    //防止先stop，再pause，然后连续不停的start的异常 
                    $("#btnPlay").removeClass("disabled"); 
                    lushu.start();
                    return;
                } return;
            } else {
                bdMap.clearOverlays(); 
            }
        } catch (e) {
            
        } 
        $("#tip").show();
        $.ajax({
            url: "AjaxService/AjaxService.ashx?action=getlushu",
            data: { "DeviceID": $("#DeviceID").val(), "startDate": $("#reservationtime").val().split('到')[0], "endDate": $("#reservationtime").val().split('到')[1] ,"callback":"playBack"},
            dataType: "json",
            type: "POST",
            error: function (error) { alert("出错");},
            success: function (data) {
                distance = 0;
                playBack(data[""]); 
            }
        });
    }
     
    function playBack(data) {
       
        if (<%= modal%> == "X83") {
           X83Point(data);
           return;
        }
        var datalist = [];
        showInfoWindowData = [];
        num = 0;
        var len = data.length;
        for (var i = 0; i < len - 2; i++) {
            var v = data[i];
            var point = new BMap.Point(parseFloat(v["BaiduLng"]).toFixed(5), parseFloat(v["BaiduLat"]).toFixed(5))
            var distance = bdMap.getDistance(point, new BMap.Point(data[i + 1]["BaiduLng"], data[i + 1]["BaiduLat"]));
            //两点距离小于5米，认为是同一个点
            // if (distance < 5) { 
            //  continue;
            // } 
                    
            //过滤速度小于 7.5 的点
            if (parseFloat(v["Speed"]) < 7.5) {
                continue;
            }
            datalist.push(point);
            showInfoWindowData.push({ "time": v["DeviceUTCTime"], "speed": v["Speed"] + "Km", "Course": GetCoureName(v["Course"]) });
        } 
        var polyline = new BMap.Polyline(datalist, { strokeColor: "#735F25", strokeWeight: 6, strokeOpacity: 0.5 }); //画线
        bdMap.addOverlay(polyline); //先把整个轨迹画出来
        lushu = new BMapLib.LuShu(bdMap, datalist, {
            defaultContent: "1",
            autoView: true,//是否开启自动视野调整，如果开启那么路书在运动过程中会根据视野自动调整
            icon: new BMap.Icon('icons/lushu_car.png', new BMap.Size(52, 26), { anchor: new BMap.Size(27, 13) }),
            speed: speed,
            enableRotation: true,//是否设置marker随着道路的走向进行旋转
            landmarkPois: [],
            interval:60
        });
        $("#tip").hide();
        $("#StopListTable tbody").empty();
        var point = new BMap.Point(parseFloat(data[0]["BaiduLng"]).toFixed(5), parseFloat(data[0]["BaiduLat"]).toFixed(5))
        bdMap.centerAndZoom(point, 12);
        start();
        $("#btnPlay").removeClass("disabled");
    }
    
    $1("btnPause").onclick = function  (){
        lushu.pause(); 
    }
    $1("btnStop").onclick = function (){
        lushu.stop(); 
    }
    var isPostBack = "";
    function start() {
        lushu.start();
    }
    function $1(element) {
        return document.getElementById(element);
    }
   
    function sosoinit(bdlat,bdlng)
    {
        var width = 100;
        $("#sosomap").toggle(); 
        if ($("#sosomap").is(":hidden")) { 
            $("#map").css({ "width": width + "%" }); 
            return;
        } else {
            width = 50;
            $("#sosomap").css({ "position": "absolute", "right": "0px", "top": "73px", "bottom": "0px", "left": (100 - width) + "%" });
            $("#map").css({ "width": (100 - width) + "%" });
        } 
      
        //转换百度坐标为腾讯坐标 
        qq.maps.convertor.translate(new qq.maps.LatLng(bdlat, bdlng), 3, function (res) {
            latlng = res[0];
            //var marker = new qq.maps.Marker({
            //    map: map,
            //    position: latlng
            //});
            // map.panTo(latlng);

            pano_service = new qq.maps.PanoramaService();
            var radius;
            pano_service.getPano(latlng, radius, function (result) { 
                if (result == null || result.svid == null) {
                    opts_waming.timeOut = "9000";
                    toastr.success('该地点没有街景图', "", opts_waming);
                    //$("#allmap").css({ "width":  "100%" });
                    //$("#sosomap").hide();
                    return;
                } 
              
                    // 创建街景   
                    pano.setPano(result.svid);
                    svid = result.svid
              
            });
        });
    }
    var adds = {};
    function X83Point(point_lsit) {
        if (point_lsit.length <= 0){
            alert("没查询到数据."); 
        }
        bdMap.clearOverlays();
        $("#StopListTable tbody").empty();
        $("#tip").hide();
        $("#btnPlay").removeClass("disabled"); 
        var viewList = [];
        var opts = {
            width : 200,     // 信息窗口宽度
            height: 120,     // 信息窗口高度
            title : deviceName , // 信息窗口标题
            enableMessage:false,//设置允许信息窗发送短息
            message:""
        } 
        $.each (point_lsit,function(index,point) { 
            var pt = new BMap.Point(point.BaiduLng, point.BaiduLat);
            viewList.push(pt);
            var type ="GPS";
            var myIcon;
            if (point.Type == 1) {
                myIcon  = new BMap.Icon("icons/gps.png", new BMap.Size(45,60),{anchor:new BMap.Size(20,60)});
            }else {
                type="LBS";
                myIcon  = new BMap.Icon("icons/lbs.png", new BMap.Size(45,60),{anchor:new BMap.Size(20,60)});
            }
            var marker = new BMap.Marker(pt,{icon:myIcon});  // 创建标注
            bdMap.addOverlay(marker);             // 将标注添加到地图中
            var address = "",addsKey=  ("key_"+ point.BaiduLat+""+point.BaiduLng).replace('.','_').replace('.','_');

            var spanId = "spanAddress"+index;
            
            var html = [];
            html.push("<tr> <td> "+(index+1)+" </td> ");
            html.push("<td> "+point.DeviceUTCTime+" </td>");
            html.push("<td> "+point.DeviceUTCTime+" </td>");
            html.push("<td> "+0+" </td>");
            html.push("<td> "+point.BaiduLat+","+point.BaiduLng+" </td>");
            html.push("<td> <span id='"+spanId+"' name='spanAddress'> <a href='#' onclick=''>解析</a> </span> </td>");
            html.push("</tr>");

            $("#StopListTable tbody").append(html.join(''));
            $("#"+spanId).on("click",function () {  
                if (adds[addsKey]) {
                    $(this).text(adds[addsKey]);
                }else{
                    GetAddressByLatlng(point.BaiduLat,point.BaiduLng,spanId,function (_address) { 
                        adds[addsKey] = _address; 
                    });
                }
            }); 
            marker.addEventListener("click",function () { 
                var txts=[];
                var sid = "spanPlayBackAddress";//+($.guid++)
                txts.push("时间: "+point.DeviceUTCTime+" <br />");
                txts.push("方式: "+ type +" <br />");
                txts.push("地址: <span id='"+sid+"'></span> "); 
                var infoWindow = new BMap.InfoWindow(txts.join(''), opts);  // 创建信息窗口对象 
                bdMap.openInfoWindow(infoWindow,pt); //开启信息窗口
                if (adds[addsKey]) { 
                    $("#"+sid).html(adds[addsKey]);
                }else{
                    GetAddressByLatlng(point.BaiduLat,point.BaiduLng,sid,function (_address) {
                        adds[addsKey] = _address; 
                    })
                }
            });
        });  
        bdMap.setViewport(viewList); 
    }
</script>


 