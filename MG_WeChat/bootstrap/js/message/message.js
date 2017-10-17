 
function MessageList() {

    var info = mg.GetUserInfo();
   
    $.ajax({
        url: "MessageAjax.asmx",
        way: "GetMessageList",
        pars: { currentindex: 1, pagecount: 50, userid: info.UserID ,type: 0},
        success: function (data) {
           var json = JSON.parse(data);
           var html = [];
           $("#messagelist").empty();
           for (var i = 0; i < json.length; i++) {    
               html.push('<li style="height:45px;" onclick="window.location.href=\'msgdetail.html?id=' + json[i].ExceptionID + '\'"> ');
               html.push('<span class="am-text-primary" style="height:100%;position:absolute;top:8px;">' + json[i].rowIndex + '.</span> ');
               html.push('<div class="am-margin-left-lg"> ');
               html.push('<p class="am-text-primary">' + (json[i].DeviceName == "" ? json[i].SerialNumber : json[i].DeviceName) + '</p> ');
               html.push('<p class="am-text-primary am-text-sm" style="margin-top:-20px;"><span >' + json[i].Message + '</span></p>  ');
               html.push('</div>   <span class="am-list-date am-text-primary">' + json[i].Created + '</span> ');
               html.push(' </li> ');
               if (i%10 == 0) {
                   $("#messagelist").append(html.join(''));
                   html = [];
               }
           }
           $("#messagelist").append(html.join('')); 
        }
    });
}
function showWinFrom() { 
    var eid = GetQueryString("id");
    $.ajax({
        url: "MessageAjax.asmx",
        way: "GetMessageInfoByID",
        pars: { exceptionid: eid },
        success: function (data) {
            var d = JSON.parse(data);
            var marker = new Marker({
                map: mgoo.map, mapType: mgoo.mapType,  lng: d.OLng, lat: d.OLat,
                line: d.Status == 1 ? "online" : "offline", course: d.Course
            }); 
            marker.show({ showTitle: false, titleText: d.DeviceName });
            mgoo.panTo(d.OLng, d.OLat);

            if (parseInt(d.OLat) < 1 || parseInt(d.OLng) < 1) {
                var m = new amModal({ msg: "未获取到定位." });
                m.open();
                m.onConfirm(function () {
                    window.location.href = "msg.html";
                });
            }
            sosoInit(d.OLat,d.OLng);

            showInfoWindow(d, marker);
            marker.addEventListener("click", function () {
                showInfoWindow(d, marker);
            });
        }
    });
     
}
function showInfoWindow(v, m) {

    var info = [];
    info.push("<div style=\"padding:0px 0px 0px 4px;\"><b>" + v.DeviceName + "</b>");
    info.push("类型: " + v.Message);
    info.push("时间: " + v.Created);
    info.push("经纬度: " + parseFloat(v.OLat).toFixed(5) + "," + parseFloat(v.OLng).toFixed(5));
    info.push("地址: " + v.Address + "</div></div>");

    var infoBox = new InfoWindow({ map: mgoo.map,  marker: m });

    infoBox.addInfoWindow({
        style: {
            fontSize: "18px", height: "100px", width: "245px", "-moz-border-radius": "15px", "border-radius": "15px"
        }, html: info.join('</br>'), lng: v.OLng, lat: v.OLat, "class": "panel-body"
    });

    //var infoWindow = new BMap.InfoWindow(html.join(''), opts); // 创建信息窗口对象   
    //var pt = new BMap.Point(v.OLng, v.OLat);
    //mgoo.map.openInfoWindow(infoWindow, pt); //开启信息窗口
}
 
function sosoInit(lat, lng) {
    try {
        var latlng = new qq.maps.LatLng(lat, lng);
        var pano_service = new qq.maps.PanoramaService();
        var radius;
        pano_service.getPano(latlng, radius, function (result) {
            if (result == null || result.svid == null) {
                $("#mgooMap").height(ch - hh);
                $("#sosoMap").height(0);
                console.log("该地点没有街景.");
                return;
            }
            $("#mgooMap").height((ch - hh) / 2);
            $("#sosoMap").height((ch - hh) / 2); 
            // 创建街景   
            pano.setPano(result.svid);
            svid = result.svid; 
        });
    } catch (e) {
        console.log(e);
    }
}

