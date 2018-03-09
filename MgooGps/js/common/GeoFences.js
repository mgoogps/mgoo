var map;

var polygon;
var LatLng = [];
var offLineMi = 20;
$(function () {
    // 百度地图API功能
    map = new BMap.Map("allmap", { enableMapClick: false });    // 创建Map实例
    map.centerAndZoom(new BMap.Point(114.32649, 30.616882), 12);
    map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放

    var top_left_control = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT });// 左上角，添加比例尺
    var top_left_navigation = new BMap.NavigationControl();  //左上角，添加默认缩放平移控件
    map.addControl(top_left_control);
    map.addControl(top_left_navigation);
    map.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP], anchor: BMAP_ANCHOR_TOP_RIGHT }, new BMap.Size(300, 200)));  //添加 右上角混合 地图
    map.enableKeyboard();//启用键盘上下左右键移动地图


    window.document.title = "电子围栏 - " + $("#DeviceName").val();
   // $("#nav").hide();
    $("input[value=" + $("#DeviceID").val() + "]").attr("checked", "checked");

    var chk = $("input[value=" + $("#DeviceID").val() + "]");
     
    LatLng.push(new BMap.Point($(chk).attr("lng"), $(chk).attr("lat")));
    $("#btnAddGeofences").click(function () {
        //  $("#btnAddGeofences").css({"visibility":"hidden"});//.hide();
        $("#nav").show(100);
        addGeofences();
    });
    var cks = $("input[name=chkDeviceName]");
    var pts = [];
    for (var i = 0; i < cks.length; i++) {
        var pt = new BMap.Point($(cks[i]).attr("lng"), $(cks[i]).attr("lat"));
        var line = "Online";
        pts.push(pt);
        var offline = parseInt($(cks[i]).attr("offline"));
        var status = parseInt($(cks[i]).attr("status"));
         
        if ($(cks[i]).attr("status") != "" && status > offline) {
            line = "Offline";
        }
      
        var myIcon = GetBaiduIcon($(cks[i]).attr("icon"), line, $(cks[i]).attr("course"));
        var marker = new BMap.Marker(pt, { icon: myIcon });  // 创建标注  
        // 显示设备上方设备名称的覆盖物
        var deviceName =  $(cks[i]).attr("devicename");
        var myCompOverlay = new ComplexCustomOverlay(pt, deviceName, deviceName, $(cks[i]).attr("value"));
        map.addOverlay(marker);
        map.addOverlay(myCompOverlay); 
    }
    map.setViewport(pts);

    $("#overlay_" + $("#DeviceID").val()).css("backgroundColor", "#000000");

    $("input[name=chkDeviceName]").on("click", function () {
        var lng = $(this).attr("lng");
        var lat = $(this).attr("lat");
        if ($(this).attr("checked")) {
            $("#overlay_" + $(this).attr("value")).css("backgroundColor", "#000000");
            var pt = new BMap.Point(lng, lat);
            LatLng.push(pt);
        } else {
            $("#overlay_" + $(this).attr("value")).css("backgroundColor", "#EE5D5B");
            for (var i = 0; i < LatLng.length; i++) {
                if (lat == LatLng[i].lat && lng == LatLng[i].lng) {
                    LatLng.splice(i, 1);
                    break;
                }
            }
        }
        map.setViewport(LatLng);
        if (LatLng.length == 1) map.setZoom(12);
    });

    $("#btnSaveGeofences").click(function () { 
        add();
    });

    $("#btnShowAddSAR").on("click", function () {
        if ($("#txtSAR").is(":hidden")) {
            function myFun(result) {
                var cityName = result.name; 
                $("#txtSAR").attr("placeholder","例如:"+cityName).val(cityName);
                getBoundary(map, $("#txtSAR").val());
            }
            var myCity = new BMap.LocalCity();  //根据ip获取所在城市
            myCity.get(myFun);
            $("#txtSAR").show(); $("#btnAddSAR").show();
            $(this).text("返回自定义");
            map.setDefaultCursor("auto");
        } else {
            $("#txtSAR").hide(); $("#btnAddSAR").hide();
            $(this).text("添加行政区域"); 
            // addGeofences();
            map.setDefaultCursor("crosshair");
        }

    });

    $("#btnAddSAR").on("click", function () {
        getBoundary(map, $("#txtSAR").val());
    });

    $("#chkSelectAll").on("change", function () {
        $("input[devicename]").attr("checked", this.checked);
        //if (this.checked) {
        //    $("input[devicename]").attr("checked", "checked"); 
        //} else {
        //    $("input[devicename]").attr("checked", this.checked);
        //}
    });

    $("a[href=#devices]").on("click", function () {
        var id = zTree_Menu.getSelectedNodes()[0].id;
        var uid = $("#UserID").val();
        if (id == uid) {
            return;
        }
        getDeviceList(uid);
    });

    $("a[href=#users]").on("click", function () {
     
        var id = zTree_Menu.getSelectedNodes()[0].id;
        if (id == $("#UserID").val()) {
            return;
        }
        
        map.setDefaultCursor("crosshair");
        getDeviceList(id);
    });

    var menu = new BMap.ContextMenu();
    var txtMenuItem = [
		{
		    text: '清除围栏',
		    callback: function () {
		        map.removeOverlay(polygon);
		        map.setDefaultCursor("crosshair");
		    }
		} 
    ];
    for (var i = 0; i < txtMenuItem.length; i++) {
        menu.addItem(new BMap.MenuItem(txtMenuItem[i].text, txtMenuItem[i].callback, 100));
    }
    map.addContextMenu(menu);

    map.setDefaultCursor("crosshair"); 
    map.addEventListener("click", function (e) {
        var cursor = map.getDefaultCursor(); 
        if (cursor == "crosshair") {
            // alert(e.point.lng + "," + e.point.lat);
            map.setDefaultCursor("auto");
            addGeofences(e.point.lat,e.point.lng)
        }
       
    });
 
    getDeviceInfo();
    
    loadTree();
});
var tempColor, row_id;

///table tr 点击事件
function _select(_this) {
    if (tempColor != undefined) {
        $("#" + row_id).css("background-color", tempColor);
    }
    var fenceType = $(_this).attr("fenceType");
  
    row_id = _this.id;
    tempColor = $(_this).css("background-color");
    $(_this).removeClass().css("background-color", "#8E8E8E");
    var gid = _this.id.split('_')[1];
    GetGeofencePolygon(gid, fenceType);
}

function GetGeofencePolygon(gid, fenceType, callback) {
    callback = callback || function () { }
    $.ajax({
        url: "/AjaxService/AjaxService.ashx?action=GetGeofencePolygon",
        data: { GID:  gid},
        type: "POST",
        dataType: "json",
        success: function (reg) {
            // var a = { points: [{ lat: "23.06820", lng: "114.02260" }, { lat: "23.06487", lng: "114.01857" }, { lat: "23.06141", lng: "114.02166" }, { lat: "23.06388", lng: "114.02641" }, { lat: "23.06820", lng: "114.02260" }], deviceIDs: [{ deviceID: 188 }] };
            map.removeOverlay(polygon);
            //var pdata = eval("(" + reg + ")");

            var pdata = reg[""][0];// JSON.parse(reg);
            var strs = pdata.Bounds.split('|');
            var pts = [];
            for (var i = 0; i < strs.length; i++) {
                var ss = strs[i].split(',');
                var p = new BMap.Point(ss[1], ss[0]);
                pts.push(p);
            }
            map.setViewport(pts);
            //$("#txtName").val(pdata.FenceName);
            if (fenceType == 2) {
                curMenu = zTree_Menu.getNodeByParam("id", pdata.BoundBindIds, null);
                zTree_Menu.selectNode(curMenu);

                getDeviceList(pdata.BoundBindIds);

                $("a[href=#devices]").parent().removeClass("active");
                $("#devices").removeClass("active");
                $("a[href=#users]").parent().attr("class", "active");
                $("#users").attr("class", "tab-pane active");
            } else {
                $("a[href=#users]").parent().removeClass("active");
                $("#users").removeClass("active");
                $("a[href=#devices]").parent().attr("class", "active");
                $("#devices").attr("class", "tab-pane active");

                $("#nav table input[type=checkbox]").attr("checked", false);

                $("div[name=overlay]").css("backgroundColor", "#EE5D5B");

                $("#nav").show(100);

                var deviceIDs = pdata.BoundBindIds.split(',');
                for (var i = 0; i < deviceIDs.length; i++) {
                    $("#overlay_" + deviceIDs[i]).css("backgroundColor", "#000000");
                    $("#nav table input[type=checkbox][value=" + deviceIDs[i] + "]").attr("checked", "checked");
                }
            }

            polygon = new BMap.Polygon(pts, { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.5 });  //创建多边形 
            map.addOverlay(polygon);   //增加多边形 
        },
        error: function (err) { console.log(err); toastr.warning("    查询位置信息失败！", "失败提示", opts_waming); }
    });
}

//在地图上创建多边形
function addGeofences(_lat,_lng) {
    if (polygon) {
        map.removeOverlay(polygon);
    }
    if (!_lat) {
        _lat = $("#lat").val();
    }
    if (!_lng) {
        _lng = $("#lng").val();
    }
    
    var size = 0.1;
    polygon = new BMap.Polygon([
       new BMap.Point(accAdd(_lng, size), accAdd(_lat, size)),
       new BMap.Point(accSub(_lng, size), accAdd(_lat, size)),
       new BMap.Point(accSub(_lng, size), accSub(_lat, size)),
       new BMap.Point(accAdd(_lng, size), accSub(_lat, size))
    ], { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.5 });  //创建多边形

    map.addOverlay(polygon);   //增加多边形
    polygon.enableEditing();
    map.setViewport(polygon.getPath());    //调整视野  
    
}

function deleted(gid) {
    $.ajax({
        url: "AjaxService/AjaxService.ashx?action=deleteGeofencesById",
        data: { "GeofenceID": gid },
        dataType: "json",
        type: "POST",
        error: function (error) { toastr.error("    删除失败！", "失败提示", opts_danger); },
        success: function (data) {

            if (data.success) {
                $("#row_" + gid).remove();
                toastr.success("     删除成功！", "成功提示", opts_success);
            }
        }
    });
}

//添加行政区域
function getBoundary(_map, _SARName) { 
    var bdary = new BMap.Boundary();
    bdary.get(_SARName, function (rs) {       //获取行政区域
        //map.clearOverlays();        //清除地图覆盖物    
        var count = rs.boundaries.length; //行政区域的点有多少个

        if (count === 0) {
            alert('未能获取当前输入行政区域');
            return;
        }
        if (polygon) {
            map.removeOverlay(polygon);
            polygon = undefined;
        }
        //var overlays = map.getOverlays();
        //for (var i = 0; i < overlays.length; i++) {

        //    if (overlays[i].constructor.name == "yc") { //清除所有线状覆盖物
        //        map.removeOverlay(overlays[i]);
        //    }
        //}
        var pointArray = [];// polygon = [];
        var boundarie = rs.boundaries[0];
        for (var i = 1; i < count; i++) {
            var latlng = rs.boundaries[i];
            if (latlng.length > boundarie.length) {
                boundarie = latlng;
            }
        }
        polygon = new BMap.Polygon(boundarie, { strokeWeight: 2, strokeColor: "#ff0000" }); //建立多边形覆盖物
        map.addOverlay(polygon);  //添加覆盖物
        
        pointArray = pointArray.concat(polygon.getPath()); 
        map.setViewport(pointArray);    //调整视野                 
    });
}

//添加电子围栏
function add( ) {
    var name = $("#txtName").val();
    var verify_msg;
    if (name.length == 0) {
        verify_msg = "请输入围栏名称.";
    }
    if (name.length > 20 ) {
        verify_msg = "电子围栏名称不能为空且小于20个字";
    }
   
    if  (!polygon || polygon.getPath().length == 0 ) {
        verify_msg = "请在地图上画出围栏.";
    }
    var activeId = $(".tab-content div.active").attr("id");
    var fenceType;
    var BindIds;
    if (activeId == "devices") {
        var chk = $("input[name=chkDeviceName]:checked");
        if (chk.length == 0 && verify_msg) { 
            verify_msg = "请至少选择一个设备";
        } else {
            var ids = [];
            for (var i = 0; i < chk.length; i++) {
                ids.push($(chk[i]).val());
            }
            BindIds = ids.join(',');
            fenceType = 1;
        } 
    } else {
        BindIds = zTree_Menu.getSelectedNodes()[0].id;
        fenceType = 2;
    } 
    if (verify_msg) {
        $("#modalInfoText").text(verify_msg);
        $("#modalInfo").modal('show', { backdrop: 'static' });
        return;
    }
    var pointsStr = [];
   
    var pts = polygon.getPath(); 

    var bounds = polygon.getBounds();
    var south_west = bounds.getSouthWest();
    var north_east = bounds.getNorthEast();

    for (var i = 0; i < pts.length; i++) {
        pointsStr.push(pts[i].lat + "," + pts[i].lng);
    }
    latLngs = pointsStr.join('|'); 
   
    show_loading_bar(60);

    toastr.success("     请稍等,正在发送数据...", "提示", opts_success);
    //{UserID:7,ZoneID:0,Name:'33333',LatLngs:'27.002196,111.198659|26.976507,111.214756|26.981336,111.2596|27.001552,111.258881|27.012431,111.22259',DeviceIDs:'57,'} 
    var sws = south_west.lat + "," + south_west.lng;
    var nes = north_east.lat + "," + north_east.lng;
    var ajaxData = {
        pointData: latLngs,
        name: name,
        userid: $("#UserID").val(),
        DeviceID: BindIds,
        south_west: sws,
        north_east: nes,
        type: fenceType
    };
 
    $.ajax({
        url: "AjaxService/AjaxService.ashx?action=addGeofences",
        type: "POST",
        dataType: "json",
        data: ajaxData,
        error: function () { toastr.error("出现异常，请检查数据是否输入正确！", "错误提示", opts_danger); },
        success: function (result) {
            if (result.success) {
                show_loading_bar(100);
                toastr.success("     添加成功！", "成功提示", opts_success);
               
                location.reload();
            } else {
                toastr.warning("    添加失败！", "警告", opts_waming);
            }
        }
    });
}

//获取当前设备的位置信息
function getDeviceInfo() {
    if ($("#DeviceID").val() == "" || $("#DeviceID").val().length == 0)
        return;
    $.ajax({
        url: "AjaxService/AjaxService.ashx?action=getdeviceinfo",
        data: { "DeviceID": $("#DeviceID").val() },
        dataType: "json",
        type: "POST",
        error: function (error) { toastr.error("    数据初始化失败！", "错误提示", opts_danger); },
        success: function (data) {
            var v = data[""][0];
            $("#lng").val(v["BaiduLng"]);
            $("#lat").val(v["BaiduLat"]); 
            var pt = new BMap.Point(v["BaiduLng"], v["BaiduLat"]);
            var line = "Online";
            if (v["status"] != "" && parseInt(v["status"]) > 10) {
                line = "Offline";
            }
            var myIcon = GetBaiduIcon(v["Icon"], line, v["Course"]); //new BMap.Icon("icons/carIcon/27.gif", new BMap.Size(20, 24));
            var marker = new BMap.Marker(pt, { icon: myIcon });  // 创建标注 
            map.setZoom(12);
            map.panTo(pt);
            // map.addOverlay(marker);
            //  alert(v["BaiduLng"] + "，" + v["BaiduLat"]); 
        }
    });
}
var zTree, zTree_Menu, curMenu;
function loadTree() {
    var setting = {
        view: {
            showLine: false,
            showIcon: true,
            selectedMulti: false,
            dblClickExpand: false,
            addDiyDom: addDiyDom
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            beforeClick: function (treeId, treeNode) {
                map.setDefaultCursor("crosshair");

                map.clearOverlays();
               
                var geoTr = $("tr[name=trUserID_" + treeNode.id+"]");
               
                for (var i = 0; i < geoTr.length; i++) {
                 
                    _select(geoTr[i]);
                }
                 
                getDeviceList(treeNode.id);
                return true;
            }
        }
    };
    var zNodes = new Array();
    $.ajax({
        url: "AjaxService/AjaxService.ashx?action=getTree",
        type: 'POST',
        dataType: 'json',
        data: {},
        async: false,
        success: function (dataList) {
            var i = 0;
            $(dataList[""]).each(function (k, v) {
                zNodes[i] = { id: v["UserID"], pId: v["ParentID"], name: v["UserName"], icon: (v.UserType == 1 ? "js/lib/ligerUI/skins/icons/memeber.gif" : "js/lib/ligerUI/skins/icons/customers.gif") };
                i++;
            });
            var treeObj = $("#ulTree");
            zTree = $.fn.zTree.init(treeObj, setting, zNodes);
            zTree_Menu = $.fn.zTree.getZTreeObj("ulTree");
             
            treeObj.addClass("showIcon");

            if ($("#UserID").val() != "") {
                curMenu = zTree_Menu.getNodeByParam("id", $("#UserID").val(), null);
                zTree_Menu.selectNode(curMenu);
                zTree_Menu.expandNode(curMenu);
            } else {
                curMenu = zTree_Menu.getNodes()[0];
                zTree_Menu.selectNode(curMenu);
                zTree_Menu.expandNode(curMenu); 
            } 
        }
    });
}
function addDiyDom(treeId, treeNode) {
    var spaceWidth = 5;
    var switchObj = $("#" + treeNode.tId + "_switch"),
    icoObj = $("#" + treeNode.tId + "_ico");
    switchObj.remove();
    icoObj.before(switchObj);
    if (treeNode.level > 1) {
        var spaceStr = "<span style='display: inline-block;width:" + (spaceWidth * treeNode.level) + "px'></span>";
        switchObj.before(spaceStr);
    }
}

function getDeviceList(_UserID,isClearOverlays) {
   
    $.ajax({
        url: "AjaxService/AjaxService.ashx?action=getDevicesByUserID",
        type: "POST",
        dataType: "json",
        data: { UserID: _UserID },
        error: function (error) {
            // jQuery('#modal-4').modal('show', { backdrop: 'static' });
        },
        success: function (res) {
            res = res[""];
          
            addMarkers(res);
        }
    });
   
}
var listMarker = [];
function addMarkers(data) {
    var pts = [];
 
  
    $.each(data, function (k, v) {
        if (v["BaiduLng"] != "" && v["BaiduLat"] != "" && v["BaiduLng"] > 0 && v["BaiduLat"] > 0) {
            var pt = new BMap.Point(v["BaiduLng"], v["BaiduLat"]);
            var line = "Online";
           
            if (v["status"] != "" && parseInt(v["status"]) > v.offLineMi) {
                line = "Offline";
            }
            var myIcon = GetBaiduIcon(v["Icon"], line, v["Course"]); //new BMap.Icon("icons/carIcon/27.gif", new BMap.Size(20, 24));
            var marker = new BMap.Marker(pt, { icon: myIcon });  // 创建标注 

            var deviceName = v["DeviceName"] == "" ? v["SerialNumber"] : v.DeviceName;
            var myCompOverlay = new ComplexCustomOverlay(pt, deviceName, deviceName);

            map.addOverlay(myCompOverlay);
            map.addOverlay(marker);
            pts.push(pt);
            listMarker.push(myCompOverlay);
            listMarker.push(myCompOverlay);
        } 
    });
    if (pts.length == 0) {
        $("#modalInfoText").text("该用户下面没有可用车辆.");
        $("#modalInfo").modal('show', { backdrop: 'static' });
        return;
    }
    map.setViewport(pts);
}

function ComplexCustomOverlay(point, text, mouseoverText, id) { 
    this._point = point;
    this._text = text;
    this._overText = mouseoverText;
    this._id = id;
}
ComplexCustomOverlay.prototype = new BMap.Overlay();
ComplexCustomOverlay.prototype.initialize = function (z_map) {
    this._map = z_map;
    var div = this._div = document.createElement("div");
    div.id = "overlay_" + this._id;
    div.name = "overlay";
    div.style.position = "absolute";
    div.style.zIndex = BMap.Overlay.getZIndex(this._point.lat);
    div.style.backgroundColor = "#EE5D5B";
    div.style.border = "1px solid #BC3B3A";
    div.style.color = "white";
    div.style.height = "25px";
    div.style.padding = "2px";
    div.style.lineHeight = "18px";
    div.style.whiteSpace = "nowrap";
    div.style.MozUserSelect = "none";
    div.style.fontSize = "12px"
    var span = this._span = document.createElement("span");
    div.appendChild(span);
    span.appendChild(document.createTextNode(this._text));
    var that = this;

    var arrow = this._arrow = document.createElement("div");
    arrow.style.background = "url(icons/label.png) no-repeat";
    arrow.style.position = "absolute";
    arrow.style.width = "11px";
    arrow.style.height = "10px";
    arrow.style.top = "24px";
    arrow.style.left = "10px";
    arrow.style.overflow = "hidden";
    div.appendChild(arrow);

    div.onmouseover = function () {
        this.bc = this.style.backgroundColor;
        this.style.backgroundColor = "#6BADCA";
        this.style.borderColor = "#0000ff";
        z_index = this.style.zIndex;
        this.style.zIndex = 5000;
        this.getElementsByTagName("span")[0].innerHTML = that._overText;
        arrow.style.backgroundPosition = "0px -10px";
    }

    div.onmouseout = function () {
        this.style.backgroundColor = this.bc;
        this.style.borderColor = "#BC3B3A";
        this.style.zIndex = z_index;
        this.getElementsByTagName("span")[0].innerHTML = that._text;
        arrow.style.backgroundPosition = "0px 0px";
    }

    map.getPanes().labelPane.appendChild(div);

    return div;
}
ComplexCustomOverlay.prototype.draw = function () {
    var z_map = this._map;
    var pixel = z_map.pointToOverlayPixel(this._point);
    this._div.style.left = pixel.x - parseInt(this._arrow.style.left) + "px";
    this._div.style.top = pixel.y - 40 + "px";
}