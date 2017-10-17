
  
    function mgMap(_mapId, _mapType) { 
        this.map = null;
        this.mapId = _mapId;
        this.mapType = _mapType;
        this.MAP_CENTER_LAT = 30.832635;
        this.MAP_CENTER_LNG = 113.901968;
        this.DEFAULT_ZOOM = 6;   //第一次打开地图显示级别
        this.RefreshTime = 10;
        this.CurrentzTreeUserID;
        this.DeviceID;
        this.contentType = "application/json; charset=utf-8";
        this.ajaxUrl = "/AjaxService/";
        this.InfoWindow;
        this.islogin = true;
        this.isAutoSetZoom = true;
        this.isRefresh = true; //是否显示倒计时跟新控件
    };
  
    //调整地图显示级别
    mgMap.prototype.setZoom = function (Zoom) {
        var _this = this;
        switch (_this.mapType) {
            case "BAIDU":
                _this.map.setZoom(Zoom);
                break;
        }
    };
    mgMap.prototype.panTo = function (point) {
        var _this = this;
        switch (_this.mapType) {
            case "BAIDU":
                _this.map.panTo(point);
                break;
        }
    };
    ///设置地图中心
    mgMap.prototype.setCenter = function (point) {
        var _this = this;
        switch (_this.mapType) {
            case "BAIDU":
                _this.map.setCenter(point);
                break;
        }
    };
    ///设置地图中心已经级别
    mgMap.prototype.centerAndZoom = function (point, zoom) {
        var _this = this;
        switch (_this.mapType) {
            case "BAIDU":
                _this.map.centerAndZoom(point, zoom);
                break;
        }
    }
    ///清除地图上的覆盖物
    mgMap.prototype.clearOverlays = function (opts) {
        switch (this.mapType)
        {
            case "BAIDU":
               
                if (opts && opts.clearAll) { 
                    this.map.clearOverlays();
                    break;
                }
                var mkrs = this.map.getOverlays(); 
                for (var i = 0; i < mkrs.length; i++) { 
                    if (mkrs[i].constructor.name == "ic" || mkrs[i].constructor.name == "zc") {
                        continue;
                    }
                    this.map.removeOverlay(mkrs[i]);
                }
                //this.map.addOverlay(this.marker);
                break;
        }
    }
    ///画线
    mgMap.prototype.polyLine = function (points, args) {
        switch (mgoo.mapType) {
            case "BAIDU":
                var polyline = new BMap.Polyline(points, args);  //定义折线 
                mgoo.map.addOverlay(polyline);     //添加折线到地图上  
                break;
        }
    }
    //计算2点之间的距离
    mgMap.prototype.getDistance = function (point1, point2) {
        switch (mgoo.mapType) {
            case "BAIDU": 
                return mgoo.map.getDistance(point1, point2);
                break;
        }
    }

    mgMap.prototype.Point = function(lon,lat) {
        switch (mgoo.mapType) {
            case "BAIDU":
                return new BMap.Point(lon,lat);
                break;
        }
    }

    mgMap.prototype.loadMap = function (lang) {
        this.lang = lang;
        var _this = this; 
        switch (_this.mapType) {
            case "BAIDU":
                // 百度地图API功能
                _this.map = new BMap.Map(_this.mapId, { enableMapClick: false });    // 创建Map实例
                _this.centerAndZoom(new BMap.Point(116.404, 39.91500), 9);  // 初始化地图,设置中心点坐标和地图级别
                _this.setCenter(new BMap.Point(116.404, 39.91500)); _this.setZoom(8);
                // bdMap.addControl(new BMap.MapTypeControl());   //添加地图类型控件 
                _this.map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
                _this.map.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP], offset: new BMap.Size(100, 10) }));  //添加 右上角混合 地图
                _this.map.addControl(new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT }));// 左下角，添加比例尺
                _this.map.enableKeyboard();//启用键盘上下左右键移动地图
                var ctrl = new BMapLib.TrafficControl({
                    showPanel: true  //是否显示路况提示面板 
                });
                _this.map.addControl(ctrl);
                ctrl.setAnchor(BMAP_ANCHOR_TOP_RIGHT);
                var navigationControl = new BMap.NavigationControl({
                    // 靠左上角位置
                    anchor: BMAP_ANCHOR_TOP_LEFT,
                    // LARGE类型
                    type: BMAP_NAVIGATION_CONTROL_LARGE,
                    // 启用显示定位
                    enableGeolocation: true,
                    showZoomInfo: true
                });
                _this.map.addControl(navigationControl);

                var myCity = new BMap.LocalCity();  //根据ip获取所在城市
                myCity.get(function (result) { 
                    _this.map.setCenter(result.name);
                });
                if (_this.isRefresh) {
                    // 定义一个控件类,即function
                    function ZoomControl() {
                        // 默认停靠位置和偏移量
                        this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
                        this.defaultOffset = new BMap.Size(100, 20); //(x,y)
                    }

                    // 通过JavaScript的prototype属性继承于BMap.Control
                    ZoomControl.prototype = new BMap.Control();

                    // 自定义控件必须实现自己的initialize方法,并且将控件的DOM元素返回
                    // 在本方法中创建个div元素作为控件的容器,并将其添加到地图容器中
                    ZoomControl.prototype.initialize = function (map) {
                        // 创建一个DOM元素
                        var div = document.createElement("div");
                        div.id = "div_refreshTime";
                        // 添加文字说明
                        div.appendChild(document.createTextNode(""));
                        // 设置样式
                        div.style.width = "200px";
                        div.style.cursor = "pointer";
                        div.style.border = "1px solid gray";
                        div.style.backgroundColor = "white";
                        // 添加DOM元素到地图中 
                        map.getContainer().appendChild(div);
                        // 将DOM元素返回 
                        return div;
                    }

                    // 创建控件
                    var myZoomCtrl = new ZoomControl();
                    // 添加到地图当中
                    _this.map.addControl(myZoomCtrl);
                    $("#div_refreshTime").html(" <strong>" + _this.RefreshTime + trackingPage.secondMsg + "<strong/>  <input type='checkbox' id='chkRefresh' checked='true' /> ");
                }
                break;
        }
    };

    function Marker(options) {
        this.map = options.map;
        this.mapType = mgoo.mapType;
        this.DeviceID = options.DeviceID;
        switch (this.mapType) {
            case "BAIDU":
                if (options.iconId == undefined) {
                    this.iconId = 1; //图标ＩＤ
                } else {
                    this.iconId = options.iconId;
                } if (options.line == undefined) {
                    this.line = "Online";// 是否在线
                } else {
                    this.line = options.line;
                } if (options.course == undefined) {
                    this.course = "0";  //方向
                } else {
                    this.course = options.course;  //方向
                } if (options.lat == undefined || options.lng == undefined) { 
                    return;
                } 
                this.lat = options.lat;
                this.lng = options.lng;
                this.titleText = options.titleText;
                if (options.marker) {
                   this.marker = options.marker;
                }
                //var myIcon = GetBaiduIcon(options.iconId, options.line, options.course); //new BMap.Icon("http://developer.baidu.com/map/jsdemo/img/fox.gif", new BMap.Size(300, 157));
                //var point = new BMap.Point(options.lng, options.lat);
                //this.marker = new BMap.Marker(point, { icon: myIcon });  // 创建标注 
               
              //  if (options.titleText != undefined) { 
                 //   this.setMarkerTitle({ marker: marker, text: options.titleText });
               // }
                //_this.map.addOverlay(marker);
                //marker.addEventListener("click", function (e) {
                    //$("#a_device_" + v["SerialNumber"]).trigger("click");
                    //options.callBack();
                //});
                break;
        }
    }

    Marker.prototype.show = function (opts) { 
        switch (this.mapType) {
            case "BAIDU": 
                var point = new BMap.Point(this.lng, this.lat); 
                var myIcon = GetBaiduIcon(this.iconId, this.line, this.course);
                var marker = new BMap.Marker(point, { icon: myIcon });  // 创建标注
                this.marker = marker;
                this.map.addOverlay(marker);
         
                var showTitle =  opts != undefined && opts.showTitle != undefined  ? opts.showTitle : true; 
                if (showTitle) { 
                    var title = opts && opts.titleText || this.titleText;
                    this.setMarkerTitle({ marker: marker, text: title });
                } 
                break;
        }
    };
 
    Marker.prototype.addEventListener = function (type,callBack) { 
        var _this = this;
        if (this.mapType == "BAIDU") { 
            this.marker.addEventListener(type, function (e) { 
                callBack(_this.DeviceID);
            });
        }
    }

    Marker.prototype.clearOverlays = function (opts) {
        switch (this.mapType) {
            case "BAIDU":
                if (opts.marker) {
                    this.map.removeOverlay(opts.marker);
                    return;
                }
                var mkrs = this.map.getOverlays();
                for (var i = 0; i < mkrs.length; i++) { 
                    if (mkrs[i].constructor.name == "ic" && !opts.clearAll) {
                        continue;
                    }
                    this.map.removeOverlay(mkrs[i]); 
                }
                //this.map.clearOverlays();
                break;
        }
    }

    Marker.prototype.setMarkerTitle = function (opts) {
        var text = opts.text == undefined ? this.titleText : opts.text;
        var marker = opts.marker == undefined ? this : opts.marker;
        var label = new BMap.Label(text, { offset: new BMap.Size(-63, -40) });
        label.setStyle({
            display: "block",
            textAlign: "center",
            width: "152px",
            lineHeight: "35px",
             border: "0px solid blue",
           // backgroundColor:"pink",
            background: "url(/Scripts/icons/label_bg.png)"
        });
        marker.setLabel(label);
    }

    function InfoWindow() {
        this.DeviceId;
        this.map;
    }
    InfoWindow.prototype.addInfoWindow = function (opts) {  
        opts.map = this.map; 
        var myCompOverlay = new ComplexCustomOverlay(opts);
        this.CompOverlay = myCompOverlay;
        this.map.addOverlay(myCompOverlay);
    }
    InfoWindow.prototype.setInfoWindowPixel = function () {
        var _this = this; 
        this.CompOverlay.setPixel(new BMap.Point(116.407845, 39.914101));
    }
    InfoWindow.prototype.remove = function () {
        $("#divInfoWindow").parent().remove();
    }
    function setInfoWindow()
    {
        //id、connectTime心跳时间，设备与服务器连接的UTC时间   deviceTime设备定位时间UTC 
        var showText = [];
        showText.push("");
    }
 
    function ComplexCustomOverlay(opts) { 
        if (opts.isOnly || opts.isOnly == undefined) {
            var mkrs = opts.map.getOverlays();
            for (var i = 0; i < mkrs.length; i++) {
                if (mkrs[i].constructor.name == "ic") {
                    opts.map.removeOverlay(mkrs[i]);
                } 
            }
            $("#divInfoWindow") .remove();
           // $("#divInfoWindow").parent().remove(); 
        } 
        this.opts = opts;
        this._point = opts.point;
        this._html = opts.html;
        this._overText = opts.mouseoverText;
    }
    ComplexCustomOverlay.prototype = new BMap.Overlay();
    ComplexCustomOverlay.prototype.initialize = function (z_map) {
        this._map = z_map;
        var div = this._div = document.createElement("div");
        div.id = "divInfoWindow";
        div.style.position = "absolute";
        div.style.zIndex = BMap.Overlay.getZIndex(this._point.lat);
        div.style.backgroundColor = "#EFEFEF";
        div.style.border = "0px solid #A7C0E0";
        div.style.color = "#000000";
        // div.style.height = "27px";
        // div.style.width = "240px";
        //(div).css({ height: this.opts.height, width: this.opts.width });
        div.style.padding = "2px";
        div.style.lineHeight = "20px";
        //div.style.whiteSpace = "nowrap";
        div.style.MozUserSelect = "none";
        div.style.zIndex = 5001;
        div.style.fontSize = "12px"

        //div.style = opts.style; 
        var span = this._span = document.createElement("span");
        div.appendChild(span);
        span.appendChild(document.createTextNode(this._html));
        var that = this;
        var close = this._close = document.createElement("a");
        close.style.right = "0px";
        close.style.top = "0px";
        close.style.position = "absolute";
        close.style.fontSize = "20px";
        close.style.cursor = "pointer";
        close.style.textDecoration = "none";
        close.title = "关闭";
        close.id = "infoWiindowClose";
        close.onclick = function () {
            div.style.display = "none";
        }
        close.appendChild(document.createTextNode("×"));
        div.appendChild(close);

        var arrow = this._arrow = document.createElement("div");
        arrow.style.background = "url(/Scripts/icons/label.png) no-repeat";
        arrow.style.position = "absolute";
        arrow.style.width = "11px";
        arrow.style.height = "10px";
        arrow.style.top = "150px";
        arrow.style.left = "10px";
        arrow.style.overflow = "hidden";
        arrow.style.zIndex = 5002;
        // div.appendChild(arrow);

        //div.onmouseover = function () {
        //    this.style.backgroundColor = "#6BADCA";
        //    this.style.borderColor = "#0000ff";
        //    z_index = this.style.zIndex;
        //    this.style.zIndex = 5000;
        //    this.getElementsByTagName("span")[0].innerHTML = that._overText;
        //    arrow.style.backgroundPosition = "0px -10px";
        //}

        //div.onmouseout = function () {
        //    this.style.backgroundColor = "#EE5D5B";
        //    this.style.borderColor = "#BC3B3A";
        //    this.style.zIndex = z_index;
        //    this.getElementsByTagName("span")[0].innerHTML = that._html;
        //    arrow.style.backgroundPosition = "0px 0px";
        //}
        this.opts.map.getPanes().labelPane.appendChild(div);
        //div.parentNode.appendChild(arrow);

        if (this.opts.class) {
            $(div).addClass(this.opts.class);
        }
        return div;
    }
    ComplexCustomOverlay.prototype.draw = function () { 
        var z_map = this._map;
        var pixel = z_map.pointToOverlayPixel(this._point);
        $(this._div).css(this.opts.style);
        this._div.style.left = pixel.x - (this.opts.style.width.replace('px', '') / 2)  + "px";
        this._div.style.top = pixel.y - (parseInt(this._div.style.height) + 20) + "px";
        var l = '<a class="lock" href="javascript:;" style="position:absolute;z-index:5002;font-size:20px;top:' + (this.opts.style.height.replace('px', '') - 10) + 'px;left:' + (this.opts.style.width.replace('px', '') / 2 - 7) + 'px"> <i class="fa fa-caret-down"></i></a>';
        $(this._div).find("span").html(this.opts.html + l);
      
        if (this.opts.style && this.opts.style.height) {
            this._div.style.height = this.opts.style.height;
        } 
    }

   
   