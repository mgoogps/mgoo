// JavaScript Document
var map = "";
var _isBMap_ = typeof BMap !== "undefined";
var PlayBack = function (mapId, user_id, productType, sudu, url) {
    this.container = mapId;
    this.user_id = user_id;
    this.map = null;
    this.productType = productType;
    this.overSpeed = sudu;// < 60 ? 80 : sudu;
    this.AJAX_URL = url || "";
    this.REQUESR_URL = "";//调用别的网站的webservice以获取坐标
    this.MAP_CENTER_LAT = 30.832635;
    this.MAP_CENTER_LNG = 113.901968; 
    this.DEFAULT_DAYS = 6 * 24 * 60 * 60;
    this.RECORDS_LENGTH = 1000;
    this.MARKERS_OBJ = {};
    this.STATIC_MARKER = [];
    this.mapType = (typeof BMap == 'undefined') ? "GOOGLE" : "BAIDU";//默认地图类型
    this.DEFAULT_ZOOM = 6;   //第一次打开地图显示级别
    this.DISTANCE = 0;
    this.POLY_LINE_MARKER = [];
    this.DATA_REQUEST = true;
    this.FROM_TIME = null;
    this.TO_TIME = null;
    this.NEXT_TIME = null;
    this.LAST_DATA = [];
    this.PRE_DATE_TIMR = null;
    this.LAST_DATA_ = [];
    this.PRE_DATE_TIMR_ = null;
    this.RUN_TIME = 0;
    this.STOP_TIME = 0;
    this.EXCURSION_COUNT = 0;
    this.FRIST_LOAD = true;
    this.TIMER = "-1";
    this.HISTORY_PLAY_FLAG = true;
    this.Frequency = null;
    this.BUTTONS_ID = ["PLAY", "STOP"];
    this.PLAY_OVER = false;
    this.CROSS_DATA_1 = null;
    this.CROSS_DATA_2 = null;
    this.GL = false;//最后一点是否被过滤掉
    this.needGetData = false;
    this.RemainStartData = null;
    this.RemainEndData = null;
    this.table = $("#StopListTable");
    this.ii = 0;
};
PlayBack.prototype.createMap = function (lang) {
    this.lang = lang;
    this.map = new goome.maps.Map({ id: "map", lang: lang, lat: this.MAP_CENTER_LAT, lng: this.MAP_CENTER_LNG, zoom: this.DEFAULT_ZOOM });
    goome.maps.event.addListener(this.map, "mousemove", PlayBack.mapMouseMove);
    //goome.maps.event.addListener(this.map, "click", PlayBack.mapMarkClick);
};
PlayBack.prototype.getDataFrist = function (from, to, q) {//第一次取GPS数据
   // this.buttonAttribute(this.BUTTONS_ID[0], true, true);
    if (from == "" || to == "") return;//开始和结束日期都不能为空
    var FROM_TIME = new Date(from.replace(/-/g, "/"));
    var TO_TIME = new Date(to.replace(/-/g, "/"));
    PlayBack.RUN_TIME = 0; //每次播放重置停留时间

    var timeDiff = (TO_TIME - FROM_TIME) / 1000;
    if (timeDiff < 0) { 
       // this.buttonAttribute(this.BUTTONS_ID[0], false, true);
        return;
    }
    if (this.FRIST_LOAD) {//第一次回放
        this.FROM_TIME = from;
        this.TO_TIME = to;
        this.NEXT_TIME = from;
    } else {//不是第一次
        PlayBack.HISTORY_PLAY_FLAG = true;
        if (getTimeDiff(to, this.TO_TIME) > 10 || !PlayBack.PLAY_OVER) {//如果修改了结束时间大于之前的结束时间并且不是第一次播放,并且结束时间大于之前的结束时间
            PlayBack.DATA_REQUEST = true;
        } else {
            PlayBack.DATA_REQUEST = false;
        }
        if (this.FROM_TIME != from) {//开始时间发生了变化,则删除上次播放的信息,重新播放
            clearTimeout(this.TIMER);
            this.GL = false;
            this.clearOverLayer();
            this.POLY_LINE_MARKER = [];
            this.FRIST_LOAD = true;
            this.DISTANCE = 0;
            this.FROM_TIME = from;
            this.NEXT_TIME = from;
            this.TO_TIME = to;
        } else {
            if (getTimeDiff(to, this.TO_TIME) <= 10 && PlayBack.PLAY_OVER) {//播放结束
                clearTimeout(this.TIMER);
                this.clearOverLayer();
                this.GL = false;
                this.DISTANCE = 0;
                this.FRIST_LOAD = true;
                this.POLY_LINE_MARKER = [];
                this.TO_TIME = to;
                this.FROM_TIME = from;
                this.NEXT_TIME = from;
                //alert(lg.playOverTip[0] + PlayBack.FROM_TIME + lg.playOverTip[1] + PlayBack.TO_TIME); 
                //this.buttonAttribute(this.BUTTONS_ID[0], false, true);
                //return;
            }
        }
    }
    this.Frequency = q || 100;
    if (!this.FRIST_LOAD && !PlayBack.PLAY_OVER) {//上次数据还没播放完毕
        this.TO_TIME = to;
        //PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], false, false);
    } else {
        this.TO_TIME = to;
        this.PLAY_OVER = false;
        this.ajaxRequest();
    }
}; 
PlayBack.prototype.getDataCallBack = function (msg) {//第一次取完数据回调  
    
  //  msg =(msg);
    document.getElementById('tip').style.display = "none"; 
   // PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], false, true);
    if (!msg) {
        alert(lg.playOverTip[0] + PlayBack.FROM_TIME + lg.playOverTip[1] + PlayBack.TO_TIME);
        var text = PlayBack.getTxtByCount();
        if (PlayBack.MARKERS_OBJ.update) PlayBack.MARKERS_OBJ.update({ text: text });
       // PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], false, true);
        PlayBack.PLAY_OVER = true;
        return;
    }
   /// PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], true, false);
    if (msg) {
        PlayBack.GL = false;
        var poly = [];
        var pArray = [];//点数组
        if (!PlayBack.FRIST_LOAD && PlayBack.LAST_DATA.length > 0) {//如果不是第一次请求数据,则把上次线条的最后一个点也加上
            poly = [new goome.maps.LatLng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0])];
        }
        var tem = msg.split(";"); 
        if (PlayBack.FRIST_LOAD) {
            PlayBack.LAST_DATA_ = tem[0].split(",");
            PlayBack.PRE_DATE_TIMR_ = PlayBack.LAST_DATA_[2];
        }
        var dateLen = tem.length - 1;
        for (var i = 0; i < dateLen; i++) {
            var flag = true;
            var rs = tem[i].split(",");
            var _diffTime = getTimeDiff(utcToLocal(rs[2]), utcToLocal(PlayBack.LAST_DATA_[2]));//当前点和上一个正常点之间的差,经过过滤之后的点
            var _dis_time = getTimeDiff(utcToLocal(rs[2]), utcToLocal(PlayBack.PRE_DATE_TIMR_));//当前点和最近的上一个点之间的时间差
            PlayBack.LAST_DATA_ = rs;
            PlayBack.PRE_DATE_TIMR_ = rs[2]; 
            if ((PlayBack.productType.indexOf("GT02") > -1 || PlayBack.productType == "LY-H810")) {//&&i<(tem.length-2)//PlayBack.productType=="GT02"||PlayBack.productType=="GT02+"||PlayBack.productType=="GT02A"gt02和gt02+ 和 gt02A过滤漂移点
                //英文版b/s和c/s版过滤时间间隔从30秒提高到300秒,中文版过滤间隔时间超过30S的数据
                //if (_dis_time > (PlayBack.lang == "en" ? 300 : 30)) { 
                //    PlayBack.EXCURSION_COUNT++;
                //    rs = [PlayBack.LAST_DATA_[0], PlayBack.LAST_DATA_[1], rs[2], 0];
                //    if (i == (dateLen - 1)) PlayBack.GL = true;//表示最后一个点被过滤掉了
                //    continue;
                //} 
                if (rs[3] < 7.5) {//过滤速度小于7.5的数据。
                   
                    PlayBack.EXCURSION_COUNT++;
                    rs = [PlayBack.LAST_DATA_[0], PlayBack.LAST_DATA_[1], rs[2], 0];
                    if (i == (dateLen - 1)) PlayBack.GL = true;//表示最后一个点被过滤掉了
                    continue;
                }
                if (rs[3] < 10 && PlayBack.EXCURSION_COUNT > 5 && rs[3] >= 5) {
                    
                    PlayBack.EXCURSION_COUNT++;
                    rs = [PlayBack.LAST_DATA_[0], PlayBack.LAST_DATA_[1], rs[2], 0];
                    if (i == (dateLen - 1)) PlayBack.GL = true;//表示最后一个点被过滤掉了
                    continue;
                }
                if (rs[3] >= 10 && rs[3] < 15 && PlayBack.EXCURSION_COUNT > 8 && PlayBack.LAST_DATA_[3] < 5) {
                  
                    PlayBack.EXCURSION_COUNT++;
                    rs = [PlayBack.LAST_DATA_[0], PlayBack.LAST_DATA_[1], rs[2], 0];
                    if (i == (dateLen - 1)) PlayBack.GL = true;//表示最后一个点被过滤掉了
                    continue;
                }
                PlayBack.EXCURSION_COUNT = 0;
                poly.push(new goome.maps.LatLng(rs[1], rs[0]));
                pArray.push(rs);
            } else {
                poly.push(new goome.maps.LatLng(rs[1], rs[0]));
                pArray.push(rs);
            }
        }
        if ((tem.length - 1) == PlayBack.RECORDS_LENGTH) {
            PlayBack.NEXT_TIME = tem[tem.length - 2].split(",")[2].split(".")[0];
            PlayBack.DATA_REQUEST = true;
            PlayBack.needGetData = true;
        } else {
            PlayBack.needGetData = false; //返回数据小于1000条将不需要再取数据
            PlayBack.DATA_REQUEST = false;
            //在轨迹播放完毕的情况下避免请求的最后一个点被过滤导致位置不准
            if (PlayBack.GL) {//如果最后请求的数据点被过滤掉了
                poly.push(new goome.maps.LatLng(PlayBack.LAST_DATA_[1], PlayBack.LAST_DATA_[0])); 
                pArray.push(PlayBack.LAST_DATA_);
            }
        }

        var flightPath = new goome.maps.Polyline({ path: poly, strokeColor: "#E7E5DC", strokeOpacity: 0.9, strokeWeight: 5 });
        flightPath.setMap(PlayBack.map);
        PlayBack.POLY_LINE_MARKER.push(flightPath);
        PlayBack.playMarker(pArray);
      
    }
};
/*实现Marker播放功能 */
PlayBack.prototype.playMarker = function (array) {//实现Marker播放功能
    if (PlayBack.FRIST_LOAD) {
        var txt = "";
        if (array.length == 0) {
            PlayBack.FRIST_LOAD = false;
            PlayBack.LAST_DATA = PlayBack.LAST_DATA_;
            PlayBack.PRE_DATE_TIMR = PlayBack.PRE_DATE_TIMR_;
            txt = PlayBack.getTxtByRecord(PlayBack.LAST_DATA);
            PlayBack.ajaxRequest();
        } else {
            PlayBack.LAST_DATA = array[0];
            PlayBack.PRE_DATE_TIMR = array[0][2];
            txt = PlayBack.getTxtByRecord(array[0]);
            $(PlayBack.table).find("tbody").empty(); 
            PlayBack.ii = 0;
          //  var geoc = new BMap.Geocoder();
            //geoc.getLocation(new BMap.Point(PlayBack.LAST_DATA[0], PlayBack.LAST_DATA[1]), function (rs) {
               // var addComp = rs.addressComponents;
               // var address = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
                var html = [];
                html.push("<tr><td>起点</td>");
                html.push("<td>" + utcToLocal(PlayBack.PRE_DATE_TIMR) + "</td>");
                html.push("<td>" + utcToLocal(PlayBack.PRE_DATE_TIMR) + "</td><td>0</td>");
                html.push("<td>" + parseFloat(array[0][1]).toFixed(5) + "," + parseFloat(array[0][0]).toFixed(5) + "</td>");
                html.push("<td><span id=\"qidianAddress" + PlayBack.ii + "\"></span></td></tr>");
                PlayBack.GetAddressByLatlng(array[0][1], array[0][0], "qidianAddress" + PlayBack.ii + "");
                $(PlayBack.table).find("tbody").append(html.join(''));
          //  }); 
        }
        var point = new goome.maps.LatLng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0]);
        PlayBack.map.setCenter(point); 
        PlayBack.map.setZoom(12);
        PlayBack.MARKERS_OBJ = new PopupMarker({ position: point, map: PlayBack.map, icon: "icons/green.gif", text: txt, showpop: true });
        PlayBack.POLY_LINE_MARKER.push(PlayBack.MARKERS_OBJ);
        if (array.length == 0) return;
    }
    var i = 0; 
    var paly = function () {
        if (PlayBack.HISTORY_PLAY_FLAG) {
           
            if (i < array.length) {
                var color = "#488BD1"; 
                if (array[i][3] > PlayBack.overSpeed && PlayBack.overSpeed > 0) {
                    var color = "#C2222A";
                    if (array[i][3] > 1.5 * PlayBack.overSpeed) {
                        color = "#FF0000";
                    } 
                }
                var points = [new goome.maps.LatLng(array[i][1], array[i][0]), new goome.maps.LatLng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0])];
                var FP = new goome.maps.Polyline({ path: points, strokeColor: color, strokeOpacity: 0.9, strokeWeight: 5 });
                FP.setMap(PlayBack.map);
                PlayBack.POLY_LINE_MARKER.push(FP);
                PlayBack.play(array[i]);
                i++;
                PlayBack.TIMER = setTimeout(paly, PlayBack.Frequency);
            } else {
                if (PlayBack.DATA_REQUEST && PlayBack.needGetData) {
                    PlayBack.ajaxRequest();
                } else {
                    alert(lg.playOverTip[0] + PlayBack.FROM_TIME + lg.playOverTip[1] + PlayBack.TO_TIME);
                    var text = PlayBack.getTxtByCount();
                    PlayBack.MARKERS_OBJ.update({ text: text });
                    PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], false, true);
                    PlayBack.PLAY_OVER = true;
                    PlayBack.NEXT_TIME = PlayBack.TO_TIME.split(".")[0];
                  //  var geoc = new BMap.Geocoder();
                  //  geoc.getLocation(new BMap.Point(PlayBack.LAST_DATA[0], PlayBack.LAST_DATA[1]), function (rs) {
                      //  var addComp = rs.addressComponents;
                    // var address = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
                    PlayBack.GetAddressByLatlng(array[0][0], array[0][1], "zdAddress" + PlayBack.ii + "");
                    $(PlayBack.table).find("tbody").append("<tr><td>终点</td><td>" + PlayBack.TO_TIME + "</td><td>" + PlayBack.TO_TIME + "</td><td>0</td><td>" + parseFloat(array[0][1]).toFixed(5) + "," + parseFloat(array[0][0]).toFixed(5) + "</td><td><span id=\"zdAddress" + PlayBack.ii + "\"></span></td></tr>");
                   // });
                }
            }
        } else {
            PlayBack.TIMER = setTimeout(paly, PlayBack.Frequency);
        }
    };
    if (PlayBack.FRIST_LOAD) {
     
        PlayBack.FRIST_LOAD = false;
        setTimeout(paly, 1000); 
    } else {
        paly();
    }
};
PlayBack.prototype.ajaxRequest = function () {
    //  _.ajax.jsonp("http://192.168.1.199:8002/?action=history&device=868120116042612&startdate=2015-4-23&enddate=2015-4-24%2015:00&callback=PlayBack.getDataCallBack");
  //  _.ajax.jsonp("../AjaxService/AjaxService.ashx?action=getplay&DeviceID=1411&startDate=" + $("#from").val() + "&endDate=" + $("#to").val() + "&t=" + new Date().getTime() + "&callback=PlayBack.getDataCallBack");
    var mdTime = document.getElementById('mdTime').value;
    PlayBack.NEXT_TIME = (PlayBack.NEXT_TIME < mdTime ? mdTime : PlayBack.NEXT_TIME);
    //var parms = "&userID=" + PlayBack.user_id + "&mapType=" + PlayBack.mapType + "&pwd=123456&from=" + localToUtc(PlayBack.NEXT_TIME) + "&to=" + localToUtc(PlayBack.TO_TIME) + "&t=" + new Date().getTime();
    var parms = "&DeviceID=" + PlayBack.user_id + "&startDate=" + PlayBack.FROM_TIME + "&endDate=" + PlayBack.TO_TIME + "&t=" + new Date().getTime();
 
    var loadDiv = document.getElementById('tip');
    loadDiv.style.display = "block"; 
    loadDiv.style.top = (document.documentElement.clientHeight - 20) / 2 + "px";
    loadDiv.style.left = (document.documentElement.clientWidth - 150) / 2 + "px";
    loadDiv.style.position = "absolute";
      
      _.ajax.jsonp("../AjaxService/AjaxService.ashx?action=getplay" + parms + "&callback=PlayBack.getDataCallBack");

};
/* 生成停留点弹出窗口的文字 */
PlayBack.prototype.getTxtByRecord = function (record) {
    var html = [];
    html.push('<font>');
    html.push('<span style="font-weight:bold;z-index:50010;">' + lg.speed + ':</span>' + record[3] + lg.kPerH + '<br/>');
    html.push('<span style="font-weight:bold;z-index:50010;">' + lg.movement + ':</span>' + formatKm2M(PlayBack.DISTANCE) + '<br/>');
    html.push('<span style="font-weight:bold;z-index:50010;">' + lg.sign + ':</span>' + utcToLocal(record[2]) + '<br/>');
    if (PlayBack.STOP_TIME > 0) {
        html.push('<span style="font-weight:bold;z-index:50010;">' + lg.idle + ':</span>' + exchangeTime(PlayBack.STOP_TIME) + '<br/>'); //exchangeTime(PlayBack.STOP_TIME) 
    }
    html.push('</font>'); 
    return html.join("");
};
/* 生成终点停止弹出窗口的文字 */
PlayBack.prototype.getTxtByCount = function () {
    var total = getTimeDiff(PlayBack.TO_TIME, PlayBack.FROM_TIME);
    var html = [];
    html.push('<font>');
    html.push('<span style="font-weight:bold;">' + lg.utime + ':</span>' + PlayBack.PRE_DATE_TIMR + '<br/>');
    html.push('<span style="font-weight:bold;">' + lg.totalTime + ':</span>' + exchangeTime(total) + '<br/>');
    html.push('<span style="font-weight:bold;">' + lg.movement + ':</span>' + formatKm2M(PlayBack.DISTANCE) + '<br/>');
    html.push('<span style="font-weight:bold;">' + lg.duration + ':</span>' + (exchangeTime(PlayBack.RUN_TIME) || 0) + '<br/>');
    html.push('<span style="font-weight:bold;">' + lg.idle + ':</span>' + exchangeTime(total - PlayBack.RUN_TIME) + '<br/>');
    html.push('</font>');
    return html.join("");
};
/* 过滤漂移数据 */
PlayBack.prototype.filterExcursion = function (array) { 
    PlayBack.EXCURSION_COUNT++;
    array[3] = 0;
    var text = PlayBack.getTxtByRecord(array);
    PlayBack.MARKERS_OBJ.update({ text: text });
};
var k = 0;
var isDrift = true;
 
PlayBack.prototype.play = function (array) {
 
        array[2] = utcToLocal(array[2]);
        var _diffTime = getTimeDiff(array[2], PlayBack.LAST_DATA[2]);//当前点和上一个正常点之间的差,经过过滤之后的点
     
       // var _dis_time = getTimeDiff(array[2], PlayBack.PRE_DATE_TIMR);//当前点和最近的上一个点之间的时间差
        PlayBack.PRE_DATE_TIMR = array[2]; 
        if (_diffTime > 600) {//大于600秒就画出静止点 
            PlayBack.createStaticMarker(array[2], PlayBack.LAST_DATA[2], _diffTime);
            k++;            
        } 
    var latlng = new goome.maps.LatLng(array[1], array[0]);
    PlayBack.DISTANCE = distance(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0], array[1], array[0], PlayBack.DISTANCE);
    var text = PlayBack.getTxtByRecord(array);
    PlayBack.MARKERS_OBJ.update({ position: latlng, text: text });
    var bound = PlayBack.map.getBounds();
    if (!bound.contains(latlng)) {//如果监控车俩没有在地图范围内就重设地图中心
        if (_isBMap_) {
            PlayBack.map.setCenter(latlng);
        } else {
            PlayBack.map.panTo(latlng);
        }
    }
    if (array[3] > 0) {
        if (_diffTime > 180) {//考虑在速度比较慢的情况下,会隔点传.
            PlayBack.STOP_TIME += _diffTime;
        } else {//运行时间
            PlayBack.RUN_TIME += _diffTime;
        }
        PlayBack.LAST_DATA = array;
    }
};

PlayBack.prototype.createPointMark = function (lan,lat) {
   // var geoc = new BMap.Geocoder();
   // geoc.getLocation(new BMap.Point(PlayBack.LAST_DATA[0], PlayBack.LAST_DATA[1]), function (rs) {
        //var addComp = rs.addressComponents;
        //var address = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
        var d_time = exchangeTime(dis_time);
        var txt = lg.idle + "：" + d_time + "<br>" + lg.start + etime + "<br>" + lg.end + stime + "<br>" + lg.lng + "：" + parseFloat(PlayBack.LAST_DATA[0]).toFixed(5) + "," + lg.lat + "：" + parseFloat(PlayBack.LAST_DATA[1]).toFixed(5) + "</br>地址：<span id=\"markAddress" + PlayBack.ii + "\"></span>" + address;;
        PlayBack.GetAddressByLatlng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0], "markAddress" + PlayBack.ii + "" );  
        var point = new goome.maps.LatLng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0]);
        PlayBack.MARKERS_OBJ.setZIndex(9999999);
        var marker = new PopupMarker({ position: point, map: PlayBack.map, icon: "icons/stoptr.png", text: txt, showpop: false });
        goome.maps.event.addListener(marker, "click", PlayBack.mapMarkClick);
        PlayBack.STATIC_MARKER.push(marker); 
   // });
};
PlayBack.prototype.mapMarkClick = function(event) {
    alert(event);
};
PlayBack.prototype.createStaticMarker = function (stime, etime, dis_time) {
    
  // var geoc = new BMap.Geocoder();
   //var bdMapPoint = new BMap.Point(PlayBack.LAST_DATA[0], PlayBack.LAST_DATA[1]);
   //geoc.getLocation(bdMapPoint, function (rs) {
      //  var addComp = rs.addressComponents;
    //var address = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber; 

        var d_time = exchangeTime(dis_time);
        var txt = lg.idle + "：" + d_time + "<br>" + lg.start + etime + "<br>" + lg.end + stime + "<br>" + lg.lng + "：" + parseFloat(PlayBack.LAST_DATA[0]).toFixed(5) + "," + lg.lat + "：" + parseFloat(PlayBack.LAST_DATA[1]).toFixed(5) + "</br>地址：<span name=\"pAddress" + PlayBack.ii + "\"></span>";
        PlayBack.GetAddressByLatlng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0], "pAddress" + PlayBack.ii + "", "name");
        var point = new goome.maps.LatLng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0]);
         PlayBack.MARKERS_OBJ.setZIndex(99999);
         var marker = new PopupMarker({ position: point, map: PlayBack.map, icon: "icons/stoptr.png", text: txt, showpop: false });
        // marker.addEventListener("click", PlayBack.mapMarkClick);
        // goome.maps.event.addListener(this.map, "click", PlayBack.mapMarkClick);
        // var marker1 = new BMap.Marker(bdMapPoint);  // 创建标注
        // marker1.addEventListener("click", function () { alert("a");});
       //  bdMap.addOverlay(marker1);              // 将标注添加到地图中
        
        PlayBack.STATIC_MARKER.push(marker); 
        var html = [];
        html.push("<tr>");
        html.push("<td>" + (++PlayBack.ii) + "</td>");
        html.push("<td>"+stime+"</td>");
        html.push("<td>"+etime+"</td>");
        html.push("<td>" + d_time + "</td>");
        html.push("<td>" + parseFloat(PlayBack.LAST_DATA[1]).toFixed(5) + "," + parseFloat(PlayBack.LAST_DATA[0]).toFixed(5) + "</td>");
        html.push("<td><span name=\"pAddress" + PlayBack.ii + "\"></span></td>");
        html.push("</tr>");
        $(PlayBack.table).find("tbody").append(html.join(''));
        $(PlayBack.table).find("tbody").find("tr").unbind("click").click(function () {
            PlayBack.mapStopListClick(this);
        }); 
  //  });
};
PlayBack.prototype.stopPlay = function () {
    //PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[1], true, false);
    PlayBack.HISTORY_PLAY_FLAG = false;
};
/* 重新播放,清楚地图上所有的线条和图标 */
PlayBack.prototype.clearOverLayer = function () {//重新播放,清楚地图上所有的线条和图标
    for (var i = 0; i < PlayBack.POLY_LINE_MARKER.length; i++) {
        PlayBack.POLY_LINE_MARKER[i].setMap(null);
    }
    for (var j = 0; j < PlayBack.STATIC_MARKER.length; j++) {
        PlayBack.STATIC_MARKER[j].setMap(null);
    }
};
PlayBack.prototype.buttonAttribute = function (id, bool, show) { 
    //for (var i = 0; i < this.BUTTONS_ID.length; i++) {
    //    if (this.BUTTONS_ID[i] == id) {
    //        document.getElementById(this.BUTTONS_ID[i]).disabled = bool;
    //        if (show)
    //            document.getElementById(this.BUTTONS_ID[i]).style.display = "";
    //        else
    //            document.getElementById(this.BUTTONS_ID[i]).style.display = "none";
    //    } else {
    //        if (!show) document.getElementById(this.BUTTONS_ID[i]).style.display = "";
    //        else document.getElementById(this.BUTTONS_ID[i]).style.display = "none";
    //        document.getElementById(this.BUTTONS_ID[i]).disabled = false;
    //    }
    //}
};

PlayBack.prototype.mapMouseMove = function (event) {
    var latlng = event.latLng;
    var markpoint = this.fromLatLngToDivPixel(latlng);
    for (var i = 0; i < PlayBack.STATIC_MARKER.length; i++) {
        var point = this.fromLatLngToDivPixel(PlayBack.STATIC_MARKER[i].latlng());
        if (point.x < (markpoint.x + 7) && point.x > (markpoint.x - 7) && point.y < (markpoint.y + 15) && point.y > (markpoint.y - 5)) { 
            PlayBack.STATIC_MARKER[i].show();
            return;
        } else { 
            PlayBack.STATIC_MARKER[i].hide();
        }
    }
};
PlayBack.prototype.mapStopListClick = function (tr) { 
    for (var i = 0; i < PlayBack.STATIC_MARKER.length; i++) {
        PlayBack.STATIC_MARKER[i].hide();
    }
    var index = parseFloat($(tr).find("td").eq(0).text());
   if (isNaN(index)){
        return;
    }
   PlayBack.STATIC_MARKER[index - 1].show(); 
   PlayBack.map.setCenter(PlayBack.STATIC_MARKER[index - 1].latlng());
};
PlayBack.prototype.GetAddressByLatlng = function(lat, lng, id,idOrName) {
    $.ajax({
        url: "AjaxService/AjaxService.ashx?action=getaddressbylatlng",
        type: "post",
        data: { "lat": lat, "lng": lng },
        dataType: "text",
        error: function () { },
        success: function (address) {
            
            if (idOrName == undefined || idOrName =="id") {
                if (id != undefined) $("#" + id).text(address);
            } else {
                $("[name=" + id + "]").text(address);
            }
          
        }
    });
}