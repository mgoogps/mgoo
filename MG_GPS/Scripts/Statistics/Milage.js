 
///里程统计
 
function formSubmit() { 
    var id = $('#sDevice').selectpicker('val');
    var name = $("#sDevice option[value='" + id + "']").text();
    var st = $("#txtStartDate").val();
    var that = this;
    var et = $("#txtEndDate").val();
    if (id == "" || st == "" || et == "" || name == "" || name == "请选择用户") {
        $(that).alertmsg('info', '请选择用户与设备名称');
        return ;
    }
    loading();

    $.ajax({
        url: "/AjaxService/Statistics.asmx/GetMileage",
        data: "{DeviceID:'" + id + "',StartTime:'" + st + "',EndTime:'" + et + "'}",
        success: function (r) {
            removeloading();
            if (r.d == "") {
                $(that).alertmsg("info", "查询数据时出错"); 
                return;
            }
            var d = JSON.parse(r.d);
            var html = []; var rowIndex = 0;
            var yh = $("#txtYh").val();
            $.each(d, function (k, v) {
                html.push('<tr data-id="' + (rowIndex++) + '">');
                html.push(' <td>' + rowIndex + '</td>');
                html.push(' <td>' + name + '</td>');
                html.push(' <td>' + k + '</td>');
                var j = JSON.parse(v);
                var points = JSON.parse(j.points);
                var y = pointsoper(j.points, yh);
                html.push(' <td>' + y.distance + '</td>');
                html.push(' <td>' + j.alarmcount + '</td>');
                html.push(' <td>' + j.stopcount + '</td>');
                html.push(' <td>' + y.yh + '</td>  </tr>');
            });
            $("#tbodyMileage").empty().append(html.join('')); 
        }
    });
}

function pointsoper(points, yh) {
    var p = JSON.parse(points);
    var distance = 0;
    for (var i = 1; i < p.length; i++) {
        var point1 = new mgoo.Point(p[i - 1].lon, p[i - 1].lat);
        var point2 = new mgoo.Point(p[i].lon, p[i].lat);
        distance += mgoo.getDistance(point1, point2);
    }
    var km = MeterToKilometer(distance);
    km = km.substring(0, km.length - 2);
    return { distance: km, yh: ((yh / 100) * km).toFixed(2) };
}



//导出 
function DerivedData() {
    var DeviceID = $('#sDevice').selectpicker('val');
    var name = $("#sDevice option[value='" + DeviceID + "']").text();
    var StartTime = $("#txtStartDate").val();
    var EndTime = $("#txtEndDate").val();
    var OilWear = $("#txtYh").val();

    if (DeviceID == "" || StartTime == "" || EndTime == "" || OilWear == "" || name == "请选择用户") {
        return;
    }
    $.ajax({
        url: "/AjaxService/Statistics.asmx/GetExecl",
        data: "{DeviceID:'" + DeviceID + "',StartTime:'" + StartTime + "',EndTime:'" + EndTime + "',OilWear:'"+OilWear+"'}",
        success: function (r) {
            var d = JSON.parse(r.d);
            console.log(d);
            alert("导出成功");       
        },
        error: function () {
            alert("导出失败");
        }
    });
}


function loading1()
{ 
    var zIndex = 99999;
    $(this).find('> .bjui-maskBackground').show();
    $(this).find('> .bjui-maskProgress').show();
    var loadBackground = $(this).find('> .bjui-maskBackground');
    var loadProgress = $(this).find('> .bjui-maskProgress'); 
    if (!loadBackground.length) {
        loadBackground = $(FRAG.maskBackground);
        loadProgress = $(FRAG.maskProgress.replace('#msg#'),BJUI.regional.progressmsg);
      //  <div class="bjui-maskProgress bjui-ajax-mask" style="z-index: 2;"><i class="fa fa-cog fa-spin"></i>&nbsp;&nbsp;正在努力加载数据，请稍等...<div class="progressBg"><div class="progress"></div></div></div>
     //       <div class="bjui-maskBackground bjui-ajax-mask" style="z-index: 1;"></div>
    }
    $("#2323").prepend(loadBackground).prepend(loadProgress);
    loadBackground.css("zIndex", zIndex + 1);
    loadProgress.css("zIndex", zIndex + 2);
    return {sssbg:loadBackground,ssspr:loadProgress};
}
