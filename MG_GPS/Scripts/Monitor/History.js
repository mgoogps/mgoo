var lushu;
var distance = 0;
var deviceName = "";
var playSpeed = 1000;
var tableRemain;
var pano;
$(function () {
    $('#playDate').dateRangePicker({
        startOfWeek: 'monday',
        separator: ' 至 ',
        format: 'YYYY-MM-DD HH:mm',
        autoClose: false,
        showShortcuts: true,
        maxDays: 90,
        language: 'cn',
        shortcuts: {
            'prev-days': [1, 3, 5, 7],
            'next-days': [0],
            'prev': ['week', 'month'],
            'next': ['week', 'month', 'year']
        },
        time: {
            enabled: true
        },
        getValue: function () { 
            return this.value;
        },
        setValue: function (s) {
            this.value = s;
        },
    });
    $('#playDate').data("dateRangePicker").setDateRange(getNowFormatDate(1), getNowFormatDate());
    $('.nstSlider').nstSlider({
        "left_grip_selector": ".leftGrip",
        "value_bar_selector": ".bar",
        "value_changed_callback": function (cause, leftValue, rightValue) {
            var $container = $(this).parent(),
                g = 255 - 127 + leftValue,
                r = 255 - g,
                b = 0;
            $container.find('.leftLabel').text("速度:" + leftValue + "米/秒"); 
            if (lushu && lushu._opts) lushu._opts.speed = leftValue;
            playSpeed = leftValue;
            
            $(this).find('.bar').css('background', '#5456BE');//rgb(' + [r, g, b].join(',') + ')
            
            $(this).find('.bar').parent().css('background', '#DFDFDF');
        }
    });
    tableRemain = $("#tableRemain body");
 
    openDialog();

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
    //$("body").dialog({
   //     id: 'mydialog', url: 'dialog-normal.html', title: '测试弹窗', "width": "600", "height": "200", "left": "5", "bottom": "30", "top": "",
   //     loadingmask:"false",
   //     onload: "dialogLoad"
   // });
      
 
     
    $("span.next-days").hide(); $("span.next-buttons").hide();
    mgoo = new mgMap("allmap", "BAIDU");
    mgoo.isRefresh = false;
    mgoo.loadMap();
    deviceName = $("#DeviceName").val();
    $("#btnPlay").on("click", function () {
        $("body").bjuiajax('doLoad', {})

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
                mgoo.clearOverlays({ clearAll:true});
            }
        } catch (e) {

        }
        history();

    });
    $("#btnPause").on("click", function () {
        lushu.pause();
    });
    $("#btnStop").on("click", function () {
        lushu.stop();
    });
});

function history() {
    console.log("historyloading");
    historyloading();
    var deviceid = $("#DeviceID").val(); var userid = $("#UserID").val();
    var time = $("#playDate").val();
    var stime = time.split('至')[0]; var etime = time.split('至')[1];
    $.ajax({ 
        url: "/AjaxService/DeviceMonitor.asmx/GetHistory",
        type:"POST",
        data: "{ ID :'" + deviceid + "',StartTime:'" + stime + "',EndTime:'" + etime + "'}", 
        success: function (r) {
            var d = JSON.parse(r.d);
            var datalist = [];
            showInfoWindowData = [];
            num = 0;
            var len = d.length;
            for (var i = 0; i < len - 2; i++) {
                var v = d[i];
                var point = mgoo.Point(parseFloat(v.Lon).toFixed(5), parseFloat(v.Lat).toFixed(5));// new BMap.Point(parseFloat(v.Lon).toFixed(5), parseFloat(v.Lat).toFixed(5));
                var distance = mgoo.getDistance(point, mgoo.Point(d[i + 1].Lon, d[i + 1].Lat));
                //两点距离小于5米，认为是同一个点
                // if (distance < 5) { 
                //  continue;
                // }
                //过滤速度小于 10 的点
                if (parseFloat(v.Speed) < 10) {
                    continue;
                }
                datalist.push(point);
                showInfoWindowData.push({ "time": v.DeviceTime, "speed": v.Speed + "Km", "Course": GetCoureName(v.Direction) });
            } 
            mgoo.polyLine(datalist, { strokeColor: "#735F25", strokeWeight: 6, strokeOpacity: 0.5 });
           
            lushu = new BMapLib.LuShu(mgoo.map, datalist, {
                defaultContent: "请稍后...",
                autoView: true,//是否开启自动视野调整，如果开启那么路书在运动过程中会根据视野自动调整
                icon: new BMap.Icon('/Scripts/icons/lushu_car.png', new BMap.Size(52, 26), { anchor: new BMap.Size(27, 13) }),
                speed: playSpeed,
                enableRotation: true,//是否设置marker随着道路的走向进行旋转
                landmarkPois: [],
                interval: 60
            });
            $("#tip").hide();
            $("#StopListTable tbody").empty(); 
            var point = mgoo.Point(parseFloat(d[0].Lon).toFixed(5), parseFloat(d[0].Lat).toFixed(5))
            mgoo.map.centerAndZoom(point, 12);
            lushu.start();
            $("#btnPlay").removeClass("disabled");
            historyremoveloading();
        },
        error: function (error) {
            historyremoveloading();
            console.log("错误！！！");
        }
    });
   
}

function doc_dialog_onLoad($dialog) {
  
    setTimeout(function () {
    var top = ($(document).height() - $(".bjui-dialog") .height() - 30);
 
    $("div.bjui-dialog").attr("style", "").css({ left: "4px", "z-index": "31", height: "302px", width: "802px", opacity: "1", bottom: "30px", "top": top });
    },1000)
    
}
function doc_dialog_beforeClose($dialog) { 
    //设置窗体不可关闭
    return false;
}
function doc_dialog_onClose() {
   // $(this).alertmsg('info', 'onClose回调：你刚刚关闭了一个dialog。') 
}

function openDialog() {
    $(".btn-green").trigger("click");
}

function sosoinit(bdlat, bdlng) {
    var width = 100;
    $("#sosomap").toggle();
    if ($("#sosomap").is(":hidden")) {
        console.log("11111");
        $("#map").css({ "width": width + "%" });
        return;
    } else { 
        width = 50;
        $("#sosomap").css({ "position": "absolute", "right": "0px", "top": "50px", "bottom": "0px", "left": (100 - width) + "%" });
        $("#map").css({ "width": (100 - width) + "%" });
    }
    if (!bdlat && !bdlng) { 
        return;
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
                $($("body")).alertmsg('warn', mapPage.noStreetView);
                return;
            } 
            // 创建街景   
            pano.setPano(result.svid);
            svid = result.svid

        });
    });
}


function historyloading() { 
    var html = [];
    html.push('<div class="bjui-maskProgress bjui-ajax-mask" style="z-index: 2;"><i class="fa fa-cog fa-spin"></i>&nbsp;&nbsp;正在努力加载数据，请稍等...<div class="progressBg"><div class="progress"></div></div></div>');
    html.push(' <div class="bjui-maskBackground bjui-ajax-mask" style="z-index: 1;"></div>');
    console.log($("body").find('div.bjui-pageHeader').length);
    $("body").append(html.join(''));
}
function historyremoveloading() {
    $("body").find('div.bjui-maskProgress').remove();
    $("body").find('div.bjui-maskBackground').remove();
}