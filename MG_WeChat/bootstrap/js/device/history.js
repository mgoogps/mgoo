var lushu;
var playSpeed = 750;
var distance;
var deviceName=""
$(function () {

    var currYear = (new Date()).getFullYear();
    var opt = {};
    opt.date = { preset: 'date' };
    opt.datetime = { preset: 'datetime' };
    opt.time = { preset: 'time' };
    opt.default = {
        theme: 'android-ics light', //皮肤样式
        display: 'modal', //显示方式
        mode: 'scroller', //日期选择模式
        dateFormat: 'yyyy-mm-dd',
        lang: 'zh',
        showNow: true,
        nowText: "今天",
        startYear: currYear - 1, //开始年份
        endYear: currYear  //结束年份
    };
    $("#txtDate").mobiscroll($.extend(opt['date'], opt['default']));//.datetime($.extend(opt['date'], opt['default']));
    //设置默认值
    var defaultdate = new Date(currYear + "/" + (new Date().getMonth() + 1) + "/" + new Date().getDate()).format("yyyy-MM-dd");

    $("#txtDate").val(defaultdate).scroller('setDate', defaultdate, true);


    $('.nstSlider').nstSlider({
        "left_grip_selector": ".leftGrip",
        "value_bar_selector": ".bar",
        "value_changed_callback": function (cause, leftValue, rightValue) {
            var $container = $(this).parent(),
                g = 255 - 127 + leftValue,
                r = 255 - g,
                b = 0;
            //$container.find('.leftLabel').text("速度:" + leftValue + "米/秒");
            if (lushu && lushu._opts)
                lushu._opts.speed = leftValue;
            playSpeed = leftValue; 
            $(this).find('.bar').css('background', '#5456BE');//rgb(' + [r, g, b].join(',') + ')

            $(this).find('.bar').parent().css('background', '#DFDFDF');
        }
    });
    $("#btnStop").on("touchstart", function () { 
        lushu.pause();
    });
    $("#btnPlay").on("click", function () {
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
                //mgoo.clearOverlays();
                mgoo.clearOverlays({ clearAll: true });
            }
        } catch (e) {

        }
        getHistoryData()
    })

   
    // mgoo.style('googlelite');
   // getData();
 

});

function getHistoryData() {
 
    mg.loading("加载中...");
    var info = mg.GetUserInfo();
    var date = $("#txtDate").val();
    
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetHistoryLocus",
        pars: { deviceid: deviceid, date: date,maptype:"BAIDU" },
        success: function (data) {

            var d = JSON.parse(data);
            if (d.length == 0) {
                new amModal({msg:"没有查询到数据"}).open();
                return;
            } 
            deviceName = sessionStorage.getItem("DeviceName");
            var datalist = [];
            showInfoWindowData = [];
            var len = d.length- 1;
            for (var i = 0; i < len ; i++) {
                var v = d[i];
                var point = mgoo.Point(parseFloat(v.OLng).toFixed(5), parseFloat(v.OLat).toFixed(5)); 
                var distance = mgoo.getDistance(point, mgoo.Point(d[i + 1].OLng, d[i + 1].OLat));
                datalist.push(point);
                showInfoWindowData.push({ "time": v.DeviceTime, "speed": v.Speed + "Km", "Course": GetCoureName(v.Course) });
            }
            mgoo.polyLine(datalist, { strokeColor: "#735F25", strokeWeight: 6, strokeOpacity: 0.5 });
            lushu = new BMapLib.LuShu(mgoo.map, datalist, {
                defaultContent: "请稍后...",
                autoView: true,//是否开启自动视野调整，如果开启那么路书在运动过程中会根据视野自动调整
                icon: new BMap.Icon('/images/map/car.png', new BMap.Size(32, 26), { anchor: new BMap.Size(16, 13) }),
                speed: playSpeed,
                enableRotation: true,//是否设置marker随着道路的走向进行旋转
                landmarkPois: [],
                interval: 60
            }); 
            mgoo.centerAndZoom(parseFloat(d[0].OLng).toFixed(5), parseFloat(d[0].OLat).toFixed(5), 16);
            lushu.start(); 
        },
        error: function (err) {
            console.log(err);
           
        },
        complete: function (xmlHttpRequest, status) {
            mg.loading("加载中...");
        }
    });
    
}

function movingInfoBox(d) {
    marker.setTitle("1" + new Date().getTime());// = "1" + new Date().getTime();
    console.log(marker);
}