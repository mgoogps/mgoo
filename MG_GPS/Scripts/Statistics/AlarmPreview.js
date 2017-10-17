function formSubmit() { 
    var id = $('#sDevice').selectpicker('val');
    var name = $("#sDevice option[value='" + id + "']").text();
    var st = $("#txtStartDate").val();
    var et = $("#txtEndDate").val();
    if (id == "" || st == "" || et == "" || name == "" || name == "请选择用户") {
        $(this).alertmsg('info', '请选择用户与设备名称');
        return;
    }
    loading();
    $.ajax({
        url: "/AjaxService/Statistics.asmx/GetAlarmPreview",
        data: "{DeviceID:'" + id + "',StartTime:'" + st + "',EndTime:'" + et + "'}",
        success: function (r) {
            var d = JSON.parse(r.d); 
            var html = [];
            var rowIndex = 1;
            var a1 = 0; var a2 = 0; var a3 = 0; var a4 = 0; var a5 = 0; var a6 = 0; var a7 = 0; var a8 = 0;
            $.each(d, function (k, v) {
                html.push("<tr> <td>" + (rowIndex++) + "</td>");
                html.push("<td>" + name + "</td>");
                html.push("<td>" + v.date + "</td>");
                v.alarmtype1 = (v.alarmtype1 ? v.alarmtype1 : 0); v.alarmtype2 = (v.alarmtype2 ? v.alarmtype2 : 0); v.alarmtype3 = (v.alarmtype3 ? v.alarmtype3 : 0);
                v.alarmtype4 = (v.alarmtype4 ? v.alarmtype4 : 0); v.alarmtype7 = (v.alarmtype7 ? v.alarmtype7 : 0); v.alarmtype5 = (v.alarmtype5 ? v.alarmtype5 : 0);
                v.alarmtype6 = (v.alarmtype6 ? v.alarmtype6 : 0);
                var sum = (parseInt(v.alarmtype1) + parseInt(v.alarmtype2) + parseInt(v.alarmtype3) + parseInt(v.alarmtype4) + parseInt(v.alarmtype5) + parseInt(v.alarmtype6) + parseInt(v.alarmtype7));
                html.push("<td>" + v.alarmtype1 + "</td>");
                html.push("<td>" + v.alarmtype2 + "</td>");
                html.push("<td>" + v.alarmtype3 + "</td>");
                html.push("<td>" + v.alarmtype4 + "</td>");
                html.push("<td>" + v.alarmtype7 + "</td>");
                html.push("<td>" + v.alarmtype5 + "</td>");
                html.push("<td>" + v.alarmtype6 + "</td>");
                html.push("<td>" + sum + "次</td>");
                // var alarm = eval("(" + "[" + v + "]" + ")");
                //   "1": "震动报警";"2":"断电报警"; "3":"接通报警"; "4":"SOS"; "5": "出电子围栏报警";"6":"入电子围栏报警";"7": "超速报警";"其他报警";
                a1 = a1 + parseInt(v.alarmtype1); a2 = a2 + parseInt(v.alarmtype2); a3 = a3 + parseInt(v.alarmtype3); a4 = a4 + parseInt(v.alarmtype4); a5 = a5 + parseInt(v.alarmtype5); a6 = a6 + parseInt(v.alarmtype6); a7= a7 + parseInt(v.alarmtype7);
                a8 = a8 + sum;
                html.push("</tr>");

            });
            html.push("<tr> <td>统计</td>");
            html.push("<td>" +  "" + "</td>");
            html.push("<td>" + d.length + "天</td>");
            html.push("<td>" + a1 + "</td>");
            html.push("<td>" + a2 + "</td>");
            html.push("<td>" + a3 + "</td>");
            html.push("<td>" + a4 + "</td>");
            html.push("<td>" + a7 + "</td>");
            html.push("<td>" + a5 + "</td>");
            html.push("<td>" + a6 + "</td>");
            html.push("<td>" + a8 + "次</td>");
            $("#tbodyAlarmPreview").empty().append(html.join(''));
            removeloading(); 
        }
    });
}
 