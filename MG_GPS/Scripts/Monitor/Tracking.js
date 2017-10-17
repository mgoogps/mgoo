var pano;
var obj = {};
var distance = 0.00;
var mgoo;
$(function  () {
    BJUI.init({
        JSPATH: 'Scripts/B-JUI/BJUI/',         //[可选]框架路径
        PLUGINPATH: 'Scripts/B-JUI/BJUI/plugins/', //[可选]插件路径
        loginInfo: { url: 'login_timeout.html', title: '登录', width: 400, height: 200 }, // 会话超时后弹出登录对话框
        statusCode: { ok: 200, error: 300, timeout: 301 }, //[可选]
        ajaxTimeout: 50000, //[可选]全局Ajax请求超时时间(毫秒)
        pageInfo: { total: 'total', pageCurrent: 'pageCurrent', pageSize: 'pageSize', orderField: 'orderField', orderDirection: 'orderDirection' }, //[可选]分页参数
        alertMsg: { displayPosition: 'topcenter', displayMode: 'slide', alertTimeout: 3000 }, //[可选]信息提示的显示位置，显隐方式，及[info/correct]方式时自动关闭延时(毫秒)
        keys: { statusCode: 'statusCode', message: 'message' }, //[可选]
        ui: {
            windowWidth: 0,    //框架可视宽度，0=100%宽，> 600为则居中显示
            showSlidebar: true, //[可选]左侧导航栏锁定/隐藏
            clientPaging: true, //[可选]是否在客户端响应分页及排序参数
            overwriteHomeTab: true //[可选]当打开一个未定义id的navtab时，是否可以覆盖主navtab(我的主页)
        },
        debug: true,    // [可选]调试模式 [true|false，默认false]
        theme: 'sky' // 若有Cookie['bjui_theme'],优先选择Cookie['bjui_theme']。皮肤[五种皮肤:default, orange, purple, blue, red, green]
    })
});
function init() { 
    mgoo = new mgMap("allmap", "BAIDU");
    mgoo.loadMap();
 
    addMapControl(mgoo.map, BMAP_ANCHOR_TOP_RIGHT, addMapPanoramic);
    mgoo.RefreshTime = 0;
     mgoo.setZoom(8);
    refreshTime(); 
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
function refreshTime() {
    if (!mgoo.islogin) {
        return;
    }
    mgoo.RefreshTime--;
    if (mgoo.RefreshTime < 0) {
        mgoo.clearOverlays(  );
        loadData();
        mgoo.RefreshTime = 10;
    }
    var chked = " ";
    if ($("#chkRefresh").attr("checked") || $("#chkRefresh").attr("checked") == "checked") {
        chked = "checked='true'";
        setTimeout("refreshTime()", 1000);
    }
    $("#div_refreshTime").html(mgoo.RefreshTime + trackingPage.secondMsg + " <input type='checkbox' onclick='startTime()' id='chkRefresh' " + chked + " />");
}

function loadData() {
    var deviceid = $("#deviceid").val();
    $.ajax({
        url: "/AjaxService/DeviceMonitor.asmx/Tracking", 
        data: "{ DeviceID :'" + deviceid + "',UserID:'" + $("#id").val() + "'}",
        success: function (r) {
            var d = JSON.parse(r.d); 
            if (obj != undefined && obj.Lat != undefined && obj.Lat != d.Lat && obj.Lon != d.Lon) {
                var p1 = new BMap.Point(obj.Lon, obj.Lat);
                var p2 = new BMap.Point(d.Lon, d.Lat);
               
                distance = accAdd(distance, mgoo.getDistance(p1,p2));
                var color = "blue";
                if ($("#Overspeed").val() != "" && v.Speed > $("#Overspeed").val())
                    color = "red";
                mgoo.polyLine([p1, p2], { strokeColor: color, strokeWeight: 6, strokeOpacity: 0.5 }); 
            }
            obj.Lat = d.Lat; obj.Lon = d.Lon; 
            var marker = new Marker({ map: mgoo.map, DeviceID: deviceid, course: d.Direction, lat: d.Lat, lng: d.Lon, titleText: d.Name });
      
            mgoo.panTo(new BMap.Point(d.Lon, d.Lat));

            marker.show({ showTitle: false });
            marker.addEventListener("click", function () {
                InfoBox(d);
            });
            InfoBox(d);
        },
        error: function (error) {
            console.log("错误！！！");
        }
    });
}

function InfoBox(v) {
    var opts = {
        width: 220,     // 信息窗口宽度
        height: 120,     // 信息窗口高度
        title: "<font style=\"font-weight:bold\">" + v.Name + "</font>", // 信息窗口标题
        enableMessage: false,//设置允许信息窗发送短息
        message: ""
    }
    var text = ""; //MeterToKilometer(distance)
    text += v.DeviceTime + "<span style=\"position:absolute; top:12px;right:0px;\"><a href=\"#\" onclick=\"javascript:sosoMapToggle();\">" + mapPage.streetView + "</a></span></br>";
    text += allPage.distance2 + ":" +0  + "</br>";
    text += allPage.lat + ":" + parseFloat(v.Lat).toFixed(5) + "," + allPage.lng + ":" + parseFloat(v.Lon).toFixed(5) + "</br>";
    text += allPage.drection + ":" + GetCoureName(v.Direction) + "," + allPage.speed + ":" + v.Speed + allPage.speedKM + "</br>";
    text += "地址："+v.Address;
    var infoWindow = new BMap.InfoWindow(text, opts); // 创建信息窗口对象   
    var pt = new BMap.Point(v.Lon,v.Lat);
    mgoo.map.openInfoWindow(infoWindow, pt); //开启信息窗口
}
var svid = "";
function sosoMapToggle() {
    $("#sosomap").toggle(); 
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
    qq.maps.convertor.translate(new qq.maps.LatLng(obj.Lat, obj.Lon), 3, function (res) {
        latlng = res[0];
        pano_service = new qq.maps.PanoramaService();
        var radius;
        pano_service.getPano(latlng, radius, function (result) {
            if (result == null || result.svid == null) { 
                $($("body")).alertmsg('warn', mapPage.noStreetView);
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
        div.innerHTML = "<table style=\"margin-top:-5px;\"><tr ><td><img src=\"/Scripts/icons/streetview-ani.gif\"/> </td><td><strong >" + mapPage.streetView + "</strong></td></tr></table>  ";
        div.onclick = function () {
            sosoMapToggle();
        }
        // 添加DOM元素到地图中
        _map.getContainer().appendChild(div);
        // 将DOM元素返回
        return div;
    }
}