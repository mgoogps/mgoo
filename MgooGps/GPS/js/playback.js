// JavaScript Document
var map = "";
var _isBMap_ = typeof BMap !== "undefined";
var PlayBack = function (mapId, user_id, productType, sudu, url) {
    this.container = mapId;
    this.user_id = user_id;
    this.map = null;
    this.productType = productType;
    this.overSpeed = sudu < 60 ? 80 : sudu;
    this.AJAX_URL = url || "";
    this.REQUESR_URL = "";//调用别的网站的webservice以获取坐标
    this.MAP_CENTER_LAT = 31.22;
    this.MAP_CENTER_LNG = 121.38;
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
};
PlayBack.prototype.createMap = function (lang) {
    this.lang = lang;
    this.map = new goome.maps.Map({ id: "map", lang: lang, lat: this.MAP_CENTER_LAT, lng: this.MAP_CENTER_LNG, zoom: this.DEFAULT_ZOOM });
    goome.maps.event.addListener(this.map, "mousemove", PlayBack.mapMouseMove);
};
PlayBack.prototype.getDataFrist = function (from, to, q) {//第一次取GPS数据
    this.buttonAttribute(this.BUTTONS_ID[0], true, true);
    if (from == "" || to == "") return;//开始和结束日期都不能为空
    var FROM_TIME = new Date(from.replace(/-/g, "/"));
    var TO_TIME = new Date(to.replace(/-/g, "/"));
    PlayBack.RUN_TIME = 0; //每次播放重置停留时间

    var timeDiff = (TO_TIME - FROM_TIME) / 1000;
    if (timeDiff < 0) {
        alert(lg.distime);
        this.buttonAttribute(this.BUTTONS_ID[0], false, true);
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
                alert(lg.playOverTip[0] + PlayBack.FROM_TIME + lg.playOverTip[1] + PlayBack.TO_TIME);
               
                this.buttonAttribute(this.BUTTONS_ID[0], false, true);
                return;
            }
        }
    }
    this.Frequency = q || 100;
    if (!this.FRIST_LOAD && !PlayBack.PLAY_OVER) {//上次数据还没播放完毕
        this.TO_TIME = to;
        PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], false, false);
    } else {
        this.TO_TIME = to;
        this.PLAY_OVER = false;
        this.ajaxRequest();
    }
}; 
PlayBack.prototype.getDataCallBack = function (msg) {//第一次取完数据回调 
   //  msg = ("114.238785,30.612293,1336464358000,1;114.23857,30.612303,1336464377000,19;114.23769,30.612333,1336464387000,33;114.236565,30.612432,1336464397000,40;114.235535,30.612494,1336464407000,32;114.23493,30.612513,1336464417000,6;114.23471,30.612463,1336464427000,15;114.23374,30.612494,1336464437000,31;114.232216,30.612543,1336464447000,50;114.23074,30.612623,1336464457000,55;114.22919,30.612703,1336464468000,40;114.2283,30.612812,1336464478000,24;114.22779,30.613092,1336464488000,32;114.22685,30.613222,1336464499000,23;114.22604,30.613354,1336464509000,18;114.225815,30.613443,1336464519000,2;114.22576,30.613493,1336464531000,0;114.225464,30.613523,1336464561000,9;114.225334,30.613703,1336464571000,10;114.225334,30.614014,1336464581000,16;114.22537,30.614384,1336464591000,17;114.22537,30.614794,1336464641000,19;114.225494,30.615414,1336464651000,15;114.22556,30.615883,1336464661000,27;114.225624,30.616703,1336464671000,33;114.225426,30.617292,1336464681000,21;114.22462,30.617474,1336464691000,19;114.224144,30.617502,1336464701000,10;114.22359,30.617523,1336464711000,25;114.22289,30.617582,1336464721000,19;114.221985,30.617672,1336464731000,39;114.2209,30.617905,1336464741000,21;114.22073,30.618044,1336464751000,7;114.22074,30.618284,1336464761000,16;114.22093,30.618864,1336464771000,23;114.221,30.619503,1336464781000,27;114.22115,30.620203,1336464791000,18;114.22109,30.619894,1336464860000,25;114.220955,30.619225,1336464870000,24;114.22086,30.618584,1336464880000,22;114.22072,30.618155,1336464890000,2;114.22071,30.617994,1336464900000,7;114.22067,30.617905,1336464910000,9;114.22016,30.618034,1336464920000,17;114.21979,30.618093,1336464930000,0;114.21951,30.618063,1336464950000,19;114.21884,30.618174,1336464960000,27;114.21804,30.618303,1336464970000,25;114.21724,30.618355,1336464980000,24;114.21645,30.618454,1336464990000,31;114.21526,30.618574,1336465000000,46;114.21405,30.618685,1336465010000,36;114.21316,30.618744,1336465020000,27;114.21229,30.618864,1336465030000,34;114.21122,30.619024,1336465040000,30;114.21011,30.619154,1336465050000,45;114.2089,30.619385,1336465060000,31;114.2088,30.620424,1336465070000,47;114.20879,30.621544,1336465080000,50;114.20878,30.622753,1336465090000,45;114.20876,30.623884,1336465100000,52;114.20871,30.625275,1336465110000,57;114.2087,30.626675,1336465120000,51;114.20855,30.627064,1336465130000,14;114.20852,30.626244,1336465140000,38;114.20804,30.625763,1336465150000,14;114.20788,30.625494,1336465160000,13;114.20783,30.625763,1336465519000,17;114.208,30.625654,1336465678000,4;114.20841,30.625923,1336465688000,26;114.20848,30.626635,1336465698000,31;114.20859,30.626913,1336465708000,16;114.2086,30.626055,1336465718000,42;114.2086,30.624813,1336465728000,50;114.2086,30.623453,1336465738000,54;114.20861,30.621914,1336465748000,63;114.20862,30.620375,1336465758000,57;114.20864,30.619314,1336465768000,25;114.20864,30.618284,1336465778000,51;114.20865,30.617033,1336465788000,52;114.20865,30.615993,1336465798000,23;114.208664,30.615854,1336465878000,9;114.20871,30.615314,1336465888000,30;114.208664,30.614393,1336465898000,39;114.20846,30.613298,1336465908000,33;114.208374,30.612898,1336465918000,15;114.20876,30.612518,1336465928000,31;114.20857,30.611898,1336465938000,37;114.2086,30.610678,1336465948000,42;114.20859,30.609957,1336465958000,37;114.20854,30.609148,1336465968000,12;114.20856,30.609118,1336465985000,0;114.20859,30.609108,1336466127000,7;114.208595,30.608618,1336466137000,29;114.2086,30.607807,1336466147000,25;114.20803,30.607758,1336466157000,38;114.20677,30.608067,1336466167000,47;114.205605,30.608347,1336466177000,27;114.2051,30.608387,1336466217000,19;114.20449,30.607668,1336466227000,46;114.20383,30.606667,1336466237000,46;114.20316,30.605818,1336466247000,38;114.20277,30.605228,1336466257000,20;114.20252,30.604858,1336466267000,21;114.20214,30.604557,1336466277000,12;114.201546,30.604618,1336466287000,33;114.20039,30.605217,1336466297000,45;114.19941,30.60574,1336466307000,49;114.19844,30.60625,1336466317000,41;114.197464,30.60672,1336466327000,36;114.196465,30.60724,1336466337000,46;114.19518,30.60792,1336466347000,54;114.19377,30.60862,1336466357000,56;114.192444,30.60928,1336466367000,52;114.19122,30.60996,1336466377000,47;114.19071,30.61056,1336466387000,26;114.19094,30.61112,1336466397000,14;114.191345,30.612461,1336466414000,36;114.19154,30.61347,1336466424000,49;114.19178,30.61477,1336466434000,46;114.19184,30.61524,1336466444000,11;114.19167,30.61527,1336466454000,4;114.19158,30.61454,1336466464000,45;114.191414,30.61333,1336466474000,46;114.19133,30.61284,1336466478000,47;114.19103,30.6117,1336466488000,48;114.19071,30.61067,1336466498000,31;114.190605,30.61049,1336466588000,13;114.19069,30.60996,1336466598000,25;114.19158,30.6096,1336466607000,32;114.19267,30.60897,1336466617000,48;114.19404,30.60828,1336466627000,58;114.19558,30.60745,1336466637000,64;114.19718,30.60666,1336466647000,59;114.19834,30.60601,1336466657000,18;114.198784,30.60574,1336466687000,41;114.199814,30.605177,1336466697000,60;114.201416,30.604357,1336466707000,54;114.20241,30.603977,1336466717000,27;114.20303,30.603647,1336466727000,25;114.20354,30.603418,1336466737000,5;114.203835,30.603317,1336466747000,26;114.20463,30.602888,1336466757000,34;114.20555,30.602438,1336466767000,35;114.206215,30.602077,1336466777000,30;114.20715,30.601517,1336466787000,48;114.20835,30.600918,1336466797000,48;114.209496,30.600348,1336466807000,19;114.20954,30.600187,1336466837000,12;114.21005,30.599907,1336466847000,22;114.210815,30.599478,1336466857000,29;114.21185,30.598927,1336466867000,35;114.212944,30.598467,1336466877000,36;114.21417,30.597837,1336466887000,60;114.215675,30.597038,1336466897000,49;114.216736,30.596277,1336466907000,41;114.21777,30.595388,1336466917000,58;114.21842,30.594818,1336466927000,21;114.21832,30.593977,1336466937000,49;114.218056,30.593067,1336466947000,12;114.21802,30.592648,1336467006000,16;114.2181,30.592167,1336467016000,27;114.218895,30.591587,1336467026000,30;114.21942,30.591017,1336467036000,37;114.21995,30.589888,1336467046000,49;114.220146,30.588587,1336467056000,46;114.220795,30.587818,1336467066000,39;114.22179,30.587168,1336467076000,40;114.22279,30.586578,1336467086000,48;114.22401,30.585857,1336467096000,46;114.22478,30.585318,1336467106000,24;114.22553,30.584658,1336467116000,47;114.226715,30.583868,1336467126000,49;114.227806,30.583027,1336467136000,53;114.22884,30.582127,1336467146000,46;114.229454,30.581547,1336467156000,14;114.22958,30.581377,1336467166000,16;114.230255,30.580738,1336467176000,44;114.23145,30.579987,1336467186000,52;114.232765,30.579607,1336467196000,45;114.23405,30.579287,1336467206000,49;114.23527,30.579008,1336467216000,46;114.236694,30.578888,1336467226000,46;114.237816,30.578878,1336467236000,38;114.23902,30.578857,1336467246000,39;114.24033,30.578957,1336467256000,54;114.24216,30.579088,1336467266000,69;114.2439,30.579157,1336467276000,48;114.24558,30.579138,1336467286000,68;114.24768,30.578838,1336467296000,73;114.24959,30.578192,1336467306000,68;114.25086,30.57756,1336467316000,31;114.25144,30.577122,1336467326000,45;114.25255,30.57616,1336467336000,63;114.25415,30.57496,1336467346000,79;114.255714,30.574001,1336467356000,40;114.2562,30.573671,1336467366000,9;114.25641,30.57353,1336467386000,30;114.25756,30.572851,1336467396000,54;114.259094,30.57225,1336467406000,50;114.26019,30.57187,1336467416000,37;114.26124,30.57159,1336467426000,40;114.262505,30.571451,1336467436000,42;114.26369,30.571411,1336467446000,42;114.26528,30.571201,1336467456000,59;114.26713,30.570992,1336467466000,68;114.26882,30.57076,1336467476000,55;114.2702,30.57053,1336467486000,32;114.27075,30.57043,1336467496000,22;114.27166,30.570261,1336467506000,31;114.272675,30.570002,1336467515000,45;114.273705,30.569632,1336467525000,30;114.274445,30.5695,1336467535000,32;114.27558,30.569351,1336467545000,40;114.27654,30.569191,1336467555000,31;114.277,30.56909,1336467565000,13;114.27782,30.568472,1336467575000,38;114.27885,30.568312,1336467585000,24;114.27962,30.568193,1336467595000,31;114.27997,30.568153,1336467605000,0;114.27997,30.568142,1336467635000,5;114.28034,30.568113,1336467645000,19;114.28102,30.568052,1336467655000,20;114.28176,30.567972,1336467665000,23;114.28231,30.567923,1336467675000,21;114.28274,30.567873,1336467685000,12;114.28318,30.567812,1336467695000,11;114.28336,30.567793,1336467705000,10;114.28363,30.567772,1336467715000,2;114.28387,30.567753,1336467725000,12;114.28418,30.567722,1336467735000,10;114.28478,30.567663,1336467745000,14;114.28504,30.567633,1336467755000,8;114.28536,30.567623,1336467765000,10;114.28542,30.567732,1336467835000,14;114.28492,30.567762,1336467845000,4;114.28457,30.567783,1336467855000,16;114.28397,30.567823,1336467865000,17;114.28352,30.567842,1336467875000,16;114.28317,30.567892,1336467885000,5;114.28302,30.567883,1336467895000,11;114.282684,30.567923,1336467905000,14;114.28229,30.568003,1336467915000,12;114.28188,30.568073,1336467924000,24;114.28094,30.568172,1336467934000,27;114.28037,30.568213,1336467944000,8;")
    msg =(msg);
    document.getElementById('tip').style.display = "none"; 
    PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], false, true);
    if (!msg) {
        alert(lg.playOverTip[0] + PlayBack.FROM_TIME + lg.playOverTip[1] + PlayBack.TO_TIME);
        var text = PlayBack.getTxtByCount();
        if (PlayBack.MARKERS_OBJ.update) PlayBack.MARKERS_OBJ.update({ text: text });
        PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], false, true);
        PlayBack.PLAY_OVER = true;
        return;
    }
    PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[0], true, false);
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
                if (_dis_time > (PlayBack.lang == "en" ? 300 : 30)) {
                    PlayBack.EXCURSION_COUNT++;
                    rs = [PlayBack.LAST_DATA_[0], PlayBack.LAST_DATA_[1], rs[2], 0];
                    if (i == (dateLen - 1)) PlayBack.GL = true;//表示最后一个点被过滤掉了
                    continue;
                }
                if (rs[3] < 5) {//过滤速度小于5的数据。
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
        }
        var point = new goome.maps.LatLng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0]);
        PlayBack.map.setCenter(point);
        PlayBack.map.setZoom(13);
        PlayBack.MARKERS_OBJ = new PopupMarker({ position: point, map: PlayBack.map, icon: "icons/green.gif", text: txt, showpop: true });
        PlayBack.POLY_LINE_MARKER.push(PlayBack.MARKERS_OBJ);
        if (array.length == 0) return;
    }
    var i = 0; 
    var paly = function () {
        if (PlayBack.HISTORY_PLAY_FLAG) {
            if (i < array.length) {
                var color = "#488BD1";
                if (array[i][3] > PlayBack.overSpeed) {
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
   
      _.ajax.jsonp("../AjaxService/AjaxService.ashx?action=getplay" + parms + "&callback=PlayBack.getDataCallBack");

};
/* 生成弹出窗口的文字 */
PlayBack.prototype.getTxtByRecord = function (record) {
    var html = [];
    html.push('<font>');
    html.push('<span style="font-weight:bold;">' + lg.speed + ':</span>' + record[3] + lg.kPerH + '<br/>');
    html.push('<span style="font-weight:bold;">' + lg.movement + ':</span>' + formatKm2M(PlayBack.DISTANCE) + '<br/>');
    html.push('<span style="font-weight:bold;">' + lg.sign + ':</span>' + utcToLocal(record[2]) + '<br/>');  
    if (PlayBack.STOP_TIME > 0) {
        html.push('<span style="font-weight:bold;">' + lg.idle + ':</span>' + exchangeTime(PlayBack.STOP_TIME) + '<br/>'); //exchangeTime(PlayBack.STOP_TIME) 
    }
    html.push('</font>'); 
    return html.join("");
};
/* 生成弹出窗口的文字 */
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
PlayBack.prototype.play = function (array) {
   array[2] = utcToLocal(array[2]); 
    var _diffTime = getTimeDiff(array[2], PlayBack.LAST_DATA[2]);//当前点和上一个正常点之间的差,经过过滤之后的点
    var _dis_time = getTimeDiff(array[2], PlayBack.PRE_DATE_TIMR);//当前点和最近的上一个点之间的时间差
    PlayBack.PRE_DATE_TIMR = array[2]; 
    if (_diffTime > 600) {//大于600秒就画出静止点
        PlayBack.createStaticMarker(array[2], _diffTime);
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
PlayBack.prototype.createStaticMarker = function (stime, dis_time) {
    //停留点显示信息 
    var txt = lg.idle + "：" + exchangeTime(dis_time) + "<br>" + lg.start + PlayBack.LAST_DATA[2] + "<br>" + lg.end + stime + "<br>" + lg.lng + ":" +parseFloat( PlayBack.LAST_DATA[0]).toFixed(5) + "," + lg.lat + ":" + parseFloat(PlayBack.LAST_DATA[1]).toFixed(5);
  
    var point = new goome.maps.LatLng(PlayBack.LAST_DATA[1], PlayBack.LAST_DATA[0]);
    var marker = new PopupMarker({ position: point, map: PlayBack.map, icon: "icons/stoptr.png", text: txt, showpop: false });
    PlayBack.STATIC_MARKER.push(marker);
};
PlayBack.prototype.stopPlay = function () {
    PlayBack.buttonAttribute(PlayBack.BUTTONS_ID[1], true, false);
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