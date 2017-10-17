

function MinuteToHour(mi) {
    if (mi == 0)
        return "1分";
    if (mi <= 60) {
        return parseInt(mi) + "分";
    }
    var day = parseInt(mi / 60 / 24);
    var h = parseInt(mi / 60 % 24);
    var m = parseInt(mi % 60);
    mi = "";
    if (day > 0) {
        mi = day + "天";
    } if (h > 0) {
        mi += h + "时";
    } if (m > 0) {
        mi += parseFloat(m) + "分";
    }
    return mi;
}

function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
    return currentdate;
}

function comptime(beginTime,endTime) {
    //var beginTime = "2009-09-21 00:00:00";
    //var endTime = "2009-09-21 00:00:01";
    var beginTimes = beginTime.substring(0, 10).split('-');
    var endTimes = endTime.substring(0, 10).split('-');

    beginTime = beginTimes[1] + '-' + beginTimes[2] + '-' + beginTimes[0] + ' ' + beginTime.substring(10, 19);
    endTime = endTimes[1] + '-' + endTimes[2] + '-' + endTimes[0] + ' ' + endTime.substring(10, 19);

    //alert(beginTime + "aaa" + endTime);
    //alert(Date.parse(endTime));
    //alert(Date.parse(beginTime));
    var a = (Date.parse(endTime) - Date.parse(beginTime)) / 1000 / 60; //分
    //alert(a);
    //if (a < 0) {
    //    alert("endTime小!");
    //} else if (a > 0) {
    //    alert("endTime大!");
    //} else if (a == 0) {
    //    alert("时间相等!");
    //} else {
    //    return 'exception'
    //}
    return parseInt(  a);
}
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),   //day
        "h+": this.getHours(),  //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
        RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return new Date(format);
}