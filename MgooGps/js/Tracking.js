// 百度地图API功能
var map = new BMap.Map("allmap", { enableMapClick: false });    // 创建Map实例
map.centerAndZoom(new BMap.Point(116.404, 39.915), 6);  // 初始化地图,设置中心点坐标和地图级别

map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
map.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP], offset: new BMap.Size(100, 10) }));  //添加 右上角混合 地图
map.addControl(new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT }));// 左上角，添加比例尺
map.enableKeyboard();//启用键盘上下左右键移动地图
var ctrl = new BMapLib.TrafficControl({
    showPanel: true  //是否显示路况提示面板 
});
map.addControl(ctrl);
ctrl.setAnchor(BMAP_ANCHOR_TOP_RIGHT);
//ctrl.show();
var navigationControl = new BMap.NavigationControl({
    // 靠左上角位置
    anchor: BMAP_ANCHOR_TOP_LEFT,
    // LARGE类型
    type: BMAP_NAVIGATION_CONTROL_LARGE,
    // 启用显示定位
    enableGeolocation: true,
    showZoomInfo: true
});
map.addControl(navigationControl);

//var geoc = new BMap.Geocoder();
var id = -1, deviceid = -1;
$(function () {
    id = $("#id").val();
    deviceid = $("#deviceid").val();
    window.document.title = allPage.tracking + " - " + $("#DeviceName").val();
     map.enableDragging();  
     map.enableInertialDragging();
     map.setZoom(16);  
     $("#allmap").css("height", "100%");
     RefreshTime();
}); 
 
addMapControl(map, BMAP_ANCHOR_TOP_LEFT, addMapTime);
addMapControl(map, BMAP_ANCHOR_TOP_RIGHT, addMapPanoramic);


var time = -1;
var tempPoint;
var tempMarker;
var distance = 0.00;
function RefreshTime() {
    if (deviceid < 0 || id < 0)
        return;
    if (time < 0) {
        $.ajax({
            url: "AjaxService/AjaxService.ashx?action=Tracking",
            type: "POST",
            dataType: 'json',
            data: { "deviceid": deviceid, "id": id },
            error: function () { toastr.warning(mapPage.loadError + "！"); },
            success: function (resp) {
                if (tempMarker != undefined) {
                    map.removeOverlay(tempMarker);
                }
                var v = resp[""][0];
                var text = "";
                var pt = new BMap.Point(v["BaiduLng"], v["BaiduLat"]);

                var line = "Online";
                if (v["status"] != "" && parseInt(v["status"]) > 10) {
                    line = "Offline";
                }
                var myIcon = GetBaiduIcon(v.Icon, line, v.Course);//new BMap.Icon("icons/carIcon/27.gif", new BMap.Size(20, 24));

                var marker = new BMap.Marker(pt, { icon: myIcon });  // 创建标注 
                tempMarker = marker;
                map.addOverlay(marker);
                if (v.DeviceName == "") {
                    v.DeviceName = v.SerialNumber;
                }

                if (tempPoint != undefined && tempPoint != pt) {
                    distance = accAdd(distance, map.getDistance(tempPoint, pt));
                    var color = "blue";
                    if ($("#Overspeed").val() != "" && v.Speed > $("#Overspeed").val())
                        color = "red";
                    var polyline = new BMap.Polyline([tempPoint, pt], { strokeColor: color, strokeWeight: 6, strokeOpacity: 0.5 });  //定义折线 
                    map.addOverlay(polyline);     //添加折线到地图上  
                }
                tempPoint = pt;
                text += v.DeviceUtcDate + "<span style=\"position:absolute; top:12px;right:0px;\"><a href=\"#\" onclick=\"javascript:sosoMapToggle();\">" + mapPage.streetView + "</a></span></br>";
                text += allPage.distance2 + ":" + MeterToKilometer(distance) + "</br>";
                text += allPage.lat + ":" + parseFloat(v.BaiduLat).toFixed(5) + "," + allPage.lng + ":" + parseFloat(v.BaiduLng).toFixed(5) + "</br>";
                text += allPage.drection + ":" + GetCoureName(v.Course) + "," + allPage.speed + ":" + v.Speed + allPage.speedKM + "</br>";
                var opts = {
                    width: 260,     // 信息窗口宽度
                    height: 160,     // 信息窗口高度
                    title: "<font style=\"font-weight:bold\">" + v.DeviceName + "</font>", // 信息窗口标题
                    enableMessage: false,//设置允许信息窗发送短息
                    message: ""
                }
                text += allPage.address + ":<span id=\"spanAddress\"></span>";
                var infoWindow = new BMap.InfoWindow(text, opts);  // 创建信息窗口对象  
                marker.addEventListener("click", function () {
                    map.openInfoWindow(infoWindow, pt); //开启信息窗口
                    GetAddressByLatlng(pt.lat, pt.lng, "spanAddress");
                });
                map.openInfoWindow(infoWindow, pt); //开启信息窗口
                GetAddressByLatlng(pt.lat, pt.lng, "spanAddress");
                map.panTo(pt);
                //map.setCenter(pt); 
                sosoinit();
            }
        });
        time = 10;
    }
    $("#div_refreshTime").text(" " + time + trackingPage.secondMsg);
    time--;
    setTimeout("RefreshTime()", 1000);
}

var pano;
function init() {
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

function addMapControl(_map, Location, callback) {
    // 定义一个控件类,即function
    function ZoomControl() {
        // 默认停靠位置和偏移量
        this.defaultAnchor = Location;
        this.defaultOffset = new BMap.Size(260, 10); //(x,y) 
    }
    // 通过JavaScript的prototype属性继承于BMap.Control
    ZoomControl.prototype = new BMap.Control();

    // 自定义控件必须实现自己的initialize方法,并且将控件的DOM元素返回
    // 在本方法中创建个div元素作为控件的容器,并将其添加到地图容器中

    callback(ZoomControl, _map);
    // 创建控件
    var myZoomCtrl = new ZoomControl();
    // 添加到地图当中
    _map.addControl(myZoomCtrl);
}

function addMapPanoramic(ZoomControl, _map) {
    ZoomControl.prototype.initialize = function (_map) {
        // 创建一个DOM元素
        var div = document.createElement("div");
        // div.id = "div_refreshTime";
        // 添加文字说明
        div.appendChild(document.createTextNode(""));
        // 设置样式
        div.style.width = "70px";
        div.style.height = "26px";
        div.style.cursor = "pointer";
        div.style.border = "1px solid gray";
        div.style.backgroundColor = "white";
        div.innerHTML = "<table style=\"margin-top:-5px;\"><tr ><td><img src=\"icons/streetview-ani.gif\"/> </td><td><strong >" + mapPage.streetView + "</strong></td></tr></table>  ";
        div.onclick = function () {
            sosoMapToggle();
        }
        // 添加DOM元素到地图中
        _map.getContainer().appendChild(div);
        // 将DOM元素返回
        return div;
    }
}
function sosoMapToggle() {
    $("#sosomap").toggle();
    sosoinit();
}
function addMapTime(ZoomControl, _map) {
    ZoomControl.prototype.initialize = function (_map) {
        // 创建一个DOM元素
        var div = document.createElement("div");
        div.id = "div_refreshTime";
        // 添加文字说明
        div.appendChild(document.createTextNode(""));
        // 设置样式
        div.style.width = "120px";
        div.style.cursor = "pointer";
        div.style.border = "1px solid gray";
        div.style.backgroundColor = "white";
        // 添加DOM元素到地图中
        _map.getContainer().appendChild(div);
        // 将DOM元素返回
        return div;
    }
}

var svid = "";

function sosoinit() {
    var width = 100;
    if ($("#sosomap").is(":hidden")) {
        $("#allmap").css({ "width": width + "%" });
        return;
    } else {
        width = 50;
        $("#allmap").css({ "width": width + "%" });
    }
    $("#sosomap").css({ "position": "absolute", "right": "0px", "top": "0px", "bottom": "0px", "left": (100 - width) + "%" });

    //转换百度坐标为腾讯坐标 
    qq.maps.convertor.translate(new qq.maps.LatLng(tempPoint.lat, tempPoint.lng), 3, function (res) {
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
                toastr.success(mapPage.noStreetView, "", opts_waming);
                return;
            }

            if (result.svid != svid) {
                // 创建街景   
                pano.setPano(result.svid);
                svid = result.svid
            }

        });
    });
}