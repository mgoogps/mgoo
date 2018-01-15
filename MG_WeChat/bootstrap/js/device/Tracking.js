var mgoo;
var imei;
var isPostBack =1;
var markerList = [];
var position;
var temp1;
var selectcommand;
var defaultType = 0;
var traffic = false;
var interval = null;
$(function () {
    mgoo = new mgMap("mgooMap", "GAODE");
    mgoo.DEFAULT_ZOOM = 15;
   // mgoo.panToLocate = true;
    mgoo.onLocateComplete = function (data) {
        
        //console.log("onLocateComplete."); 
         
       // console.log(isPostBack);
      
        position = [data.position.getLng(), data.position.getLat()];
        var lineArr = [
         position,
         markerList[0].getPosition()
        ];
         mgoo.clearOverlays({ clearMarker: [markerList[1]] });
       //  console.log(markerList[1]);
        line(lineArr);
       // if (isPostBack == 0) {
           // mgoo.setFitView();
          //  isPostBack = 1;
      //  } else
            if (isPostBack == 1) {
            mgoo.panTo(data.position.getLng(), data.position.getLat());
            isPostBack = 2;
        } else if (isPostBack == 2) {
            var mar = markerList[0].getPosition(); 
            mgoo.panTo(mar.lng, mar.lat);
            isPostBack = 1;
        }
    }
   // mgoo.panToLocate = true;
    mgoo.loadMap();
    mgoo.setZoom(16);
    // mgoo.style('googlelite');
    getData();
    interval = setInterval(function () {
        getData()
    }, 5000);
    
    $('#command-prompt').modal({
        relatedTarget: this,
        onConfirm: function (e) {
           
        },
        onCancel: function (e) {
            selectcommand = undefined;
        }
    });
    $('#command-prompt').modal('close');
    $("#command-prompt .btn-loading").on("click", function () {
        var $btn = $(this)
        $btn.button('loading');
        $(this).val("发送中...")
        var modal;
        var selectcommand=$(this).attr('data-cmd');
        if (selectcommand == "" || selectcommand == undefined || selectcommand == 'undefined') {
            modal = new amModal({ msg: "请选择要发送的指令." });
            modal.open();
            return;
        }
        $.ajax({
            url: "DevicesAjax.asmx",
            way: "SendCommand",
            pars: { command: imei + "," + selectcommand },
            success: function (data) {
                modal = new amModal({ msg: data.Message });
                modal.open();
                if (data.StatusCode === 200) {
                    modal.onConfirm(function () {
                       // $('#command-prompt').modal('close');
                    });
                }
                $btn.button('reset');
            },
            error: function (err) {
                console.log(err);
                $btn.button('reset');
            },
            complete: function (xmlHttpRequest, status) {
                selectcommand = undefined;
            
            }
        });


        setTimeout(function () {
            $btn.button('reset');
        }, 5000);
    });
    $("#mapPlugin div").on("click", function () {
        var bg = $(this).css("background-image");
        if (bg.indexOf("type1") > 0) {
            $(this).css("background-image", "url(/images/map/type2.png)");
            defaultType = 1;
            traffic = false;
        } else if (bg.indexOf("type2") > 0) {
            $(this).css("background-image", "url(/images/map/type1.png)");
            defaultType = 0;
            traffic = false;
        } else if (bg.indexOf("traffic1") > 0) {
            $(this).css("background-image", "url(/images/map/traffic2.png)");
            traffic = true;
            defaultType = 0;
        } else if (bg.indexOf("traffic2") > 0) {
            $(this).css("background-image", "url(/images/map/traffic1.png)");
            traffic = false;
        } else if (bg.indexOf("street") > 0) {

            var url = "http://apis.map.qq.com/uri/v1/streetview?location=" + temp1[1] + "," + temp1[0] + "&heading=236&pitch=8&zoom=1&referer=mgoogps";
            window.location.href = url;

        }
        mgoo.setMapType([defaultType, traffic]);
    });
    
    $("#aPay").on("click", function () {
        var loginInfo = mg.GetUserInfo();
        var openid = loginInfo.Token.split('@')[0]; 
        window.location.href = "../pay/Recharge.aspx?deviceid=" + deviceid + "&openid=" + openid + "&imei=" + imei + "&t=" + new Date().getTime();
    });

    $("#aLogout").on("click", function () {
        var info = mg.GetUserInfo(); 
        if (info.LoginType == 1) {
            mg.SetUserInfo(undefined);
            window.location.replace("../Login.aspx");
        }
    });

});

function getData() {
   
    var info = mg.GetUserInfo();
    
    if (info.LoginType == 1) {
        $("#aLogout").show();
        $("#divBack").hide();
    }
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetMonitorByDeviceID",
        pars: { deviceid: deviceid },
        success: function (data) { 
            var d = JSON.parse(data);
            if (d.Status == 4) {
                clearInterval(interval);
                var opts = { msg: "该设备已欠费,请充值." };
                if (info.LoginType == 1) {
                    opts.cancel = true;
                }
                var m = new amModal(opts);
                m.open();
                m.onConfirm(function () {
                    var o = info.Token.split('@')[0]; 
                    window.location.href = "../pay/Recharge.aspx?deviceid=" + d.DeviceID + "&openid=" + o + "&imei=" + d.SerialNumber + "&t=" + new Date().getTime();
                });
                return;
            }
            CommandButton(d);
            if (d.Status == 3) {
                var opts = { msg: "该设备未激活." };
                if (info.LoginType == 1) {
                    opts.cancel = true;
                }
                var m = new amModal(opts); 
                m.open();
                m.onConfirm(function () {
                    history.back();
                });
            }
            carMarker(d); 
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function carMarker(d) {
  
    var temp = mgoo.map.getAllOverlays("marker")[0]; 
    mgoo.clearOverlays({clearMarker: markerList });
    var marker = new Marker({
        map: mgoo.map, mapType: mgoo.mapType, DeviceID: deviceid, lng: d.OLng, lat: d.OLat,
        line: d.Status == 1 ? "online" : "offline", course: d.Course
    });
    //point-offline  point-online
    var iconurl = '/images/map/point-online.gif';
    if (d.Status != 1) {
        var iconurl = '/images/map/point-offline.gif';
    }
    marker.show({ showTitle: false, titleText: d.DeviceName, icon: iconurl });
    markerList[0] = marker.marker;
    temp1 = [d.OLng, d.OLat];
    if (position) {
        var lineArr = [
            position,
            [markerList[0].getPosition().lng, markerList[0].getPosition().lat]
        ]; 
        line(lineArr);
    }
 
    if ((temp && temp.getPosition().lat.toFixed(5) != parseFloat(d.OLat).toFixed(5) && temp.getPosition().lng.toFixed(5) != parseFloat(d.OLng).toFixed(5)) || temp == undefined) {
        mgoo.panTo(d.OLng, d.OLat);
    }
    $("title").text(d.DeviceName);
    //$(".am-header-title a").text(d.DeviceName);
    var status = '';
   
    if (d.Status == 1) {
        status = "<font color='blue'>在线</font>";
    } else if (d.Status == 2 ) { 
        status = "<font color='red'>离线</font>";
    } else if (d.Status == 3) {
        status = "<font color='red'>未激活</font>";
    } else {
        status = "<font color='red'>已到期</font>";
    }

    sessionStorage.setItem("DeviceName", d.DeviceName);
    $("#showinfo").html('<span>时间:' + d.DeviceDate + '; 速度:' + d.Speed + 'km/h; 方向:' + d.CourseName + ',状态:' + status + '</span><br /><span>地址:' + d.Address + '</span>');

    $("#mgooMap").height($(window).height() - $(".am-header").height() - $("#showinfo").height());
    $("#content").height($(window).height() - $(".am-header").height() - $("#showinfo").height());

  
}

function showInfoWindow(v,m) {
  
    var info = [];
    info.push("<div style=\"padding:0px 0px 0px 4px;\"><b>"+v.DeviceName+"</b>");
    info.push("时间: " + v.DeviceDate);
    info.push("GPS定位,速度:" + v.Speed + "km/h,方向:" + v.CourseName);
    info.push("地址: "+v.Address+"</div></div>");
 
    var infoBox = new InfoWindow({map:mgoo.map, DeviceID: 1046, marker:m});

    infoBox.addInfoWindow({
        style: { 
            fontSize: "18px", height: "100px", width: "245px", "-moz-border-radius": "15px", "border-radius": "15px"
        }, html: info.join('</br>'), lng:v.OLng,lat:v.OLat,  "class": "panel-body"
    }); 
}

function line(lineArr) {
 
    var polyline = new AMap.Polyline({
        path: lineArr,          //设置线覆盖物路径
        strokeColor: "#3366FF", //线颜色
        strokeOpacity: 1,       //线透明度
        strokeWeight: 1,        //线宽
        strokeStyle: "solid",   //线样式
        strokeDasharray: [10, 5] //补充线样式
    });
    polyline.setMap(mgoo.map);
    markerList[1] = polyline;
}

function CommandButton(res) {
   
    imei = res.SerialNumber;
    if (imei.indexOf('3008000000') == 0 || imei.indexOf('3528888000') == 0) {
        $("#command-prompt div.am-modal-bd input[value=设防]").val("震动开"); //.parent().hide();
        $("#command-prompt div.am-modal-bd input[value=撤防]").val("震动关");//.parent().hide();
    }
    //仓库门锁
    if (res.Model == "MG-X30B") {
        $("#command-prompt div.am-modal-bd input[value=设防]").val("震动开").parent().hide();
        $("#command-prompt div.am-modal-bd input[value=撤防]").val("震动关").parent().hide();
        $("#command-prompt div.am-modal-bd input[value=开门]").parent().show();
        $("#command-prompt div.am-modal-bd input[value=锁门]").parent().show();
    }
    else if (res.Model == "MG-X11BDY") {
        $(".box-8,.box-7,.box-9,.box-10").show();
        $(".box-3,.box-4, .box-1, .box-2").hide();
  
        var context = res.DataContext.split('-');
        if (context[0] == 1) {
            $(".box-8 input").attr("data-cmd", "POWER/OFF#").val("关闭电源");
        } else {
            $(".box-8 input").attr("data-cmd", "POWER/ON#").val("开启电源");
        }
        if (context[11] == 0) {
            $(".box-7 input").attr("data-cmd", "TY").val("已 刹 车");
        } else {
            $(".box-7 input").attr("data-cmd", "DY").val("未 刹 车");
        }
        if (context[1] == 1) {
            $(".box-10 input").attr("data-cmd", "CF").val(" 撤  防 ");
        } else {
            $(".box-10 input").attr("data-cmd", "SF").val(" 设  防 ");
        }
    }
}

function navigation() {
    if (position == undefined) { 
        mgoo.getGeolocation(geoconv);
    } else {
        geoconv(temp1  );
    }
}
function geoconv(data) { 
    var lat = temp1[1], lng = temp1[0];
    if (position == undefined) {
        position = [data.position.getLng(), data.position.getLat()];
    } 
    //http://api.map.baidu.com/geoconv/v1/?coords=114.21892734521,29.575429778924;114.21892734521,29.575429778924&ak=SAbCayX7PG5UMsqW6d1DZ9K0&output=json
    var url = "http://api.map.baidu.com/geoconv/v1/?coords=" + position[0] + "," + position[1] + ";" + lng + "," + lat + "&ak=SAbCayX7PG5UMsqW6d1DZ9K0&output=json&from=3&callback=geoconvCallback";
   
    $.ajax({ 
        url:url, 
        dataType: "JSONP",
        jsonp: 'jsoncallback', // 自动生成回调函数时的名称
        type: "POST",
        success: function () {
            //console.log(JSON.parse(result.d).result[0]);
            //result = JSON.parse(result.d).result[0];
           
            //window.location.href = "http://api.map.baidu.com/direction?origin=latlng:" + position[1] + "," + position[0] + "|name:我的位置&destination=latlng:" + result.y + "," + result.x + "|name:爱车位置&mode=driving&region=当前位置&output=html&src=yourCompanyName|yourAppName";
        }
    });
  
}

function geoconvCallback(data) {
    var res = data.result;
    window.location.href = "http://api.map.baidu.com/direction?origin=latlng:" + res[0].y + "," + res[0].x + "|name:我的位置&destination=latlng:" + res[1].y + "," + res[1].x + "|name:爱车位置&mode=driving&region=当前位置&output=html&src=mgoo|mgoogps";
}

function translate()
{
    $.ajax({ 
        url: "http://apis.map.qq.com/ws/coord/v1/translate?locations=type=", 
        dataType: "JSONP",
        jsonp: 'jsoncallback', // 自动生成回调函数时的名称
        type: "POST",
        success: function () {
            //console.log(JSON.parse(result.d).result[0]);
            //result = JSON.parse(result.d).result[0];

            //window.location.href = "http://api.map.baidu.com/direction?origin=latlng:" + position[1] + "," + position[0] + "|name:我的位置&destination=latlng:" + result.y + "," + result.x + "|name:爱车位置&mode=driving&region=当前位置&output=html&src=yourCompanyName|yourAppName";
        }
    });
}

function showCityInfo() {
    //加载城市查询插件 
    AMap.service(["AMap.CitySearch"], function () {
        //实例化城市查询类 
        var citysearch = new AMap.CitySearch();
        //自动获取用户IP，返回当前城市 
        citysearch.getLocalCity(function (status, result) {
            if (status === 'complete' && result.info === 'OK') {
                if (result && result.city && result.bounds) {
                    var cityinfo = result.city;
                    var citybounds = result.bounds;
                    city = cityinfo;
                    citycode = cityinfo.replace("市", "");
                    $('#market option:contains("' + citycode + '")').attr('selected', 'selected');


                    alert("您当前所在城市：" + cityinfo + ",code:" + cityinfo.replace("市", ""));

                    //   toast("您当前所在城市：" + cityinfo + ",code:"+ cityinfo.replace("市","")); 
                }
            } else {
                alert("您当前所在城市：" + result.info + "");
            }
        });
    });
}

 
