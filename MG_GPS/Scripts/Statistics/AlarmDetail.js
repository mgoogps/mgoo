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
        url: "/AjaxService/Statistics.asmx/GetAlarmDetail",
        data: "{DeviceID:'" + id + "',StartTime:'" + st + "',EndTime:'" + et + "'}",
        success: function (r) {
            var d = JSON.parse(r.d); 
            var html = [];
            var rowIndex = 1; 
            $.each(d, function (k, v) {
                html.push("<tr> <td>" + (rowIndex++) + "</td>");
                html.push("<td>" + name + "</td>");
                html.push("<td>" + msgName(v.alarmtype) + "</td>");
                html.push("<td>" +  v.alarmtime.replace('T', ' ').replace('Z', '').split('.')[0]  + "</td>");
                html.push("<td>" + v.lat + "," + v.lon + "</td>");
                html.push("<td> <a href='#' onclick='getAddress(" + v.lat + " , " + v.lon +",this)'>解析</a></td>");//
                html.push("</tr>"); 
            });
          
            $("#tbodyAlarmDetail").empty().append(html.join(''));
            removeloading(); 
        }
    });
}