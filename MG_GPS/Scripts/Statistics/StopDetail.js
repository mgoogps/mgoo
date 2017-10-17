
function formSubmit() { 
    var id = $('#sDevice').selectpicker('val');
    var name = $("#sDevice option[value='" + id + "']").text();
    var st = $("#txtStartDate").val();
    var et = $("#txtEndDate").val();
    if (id == "" || st == "" || et == "" || name == "" || name == "请选择用户") {
        $(this).alertmsg('info', '请选择用户与设备名称');
        return ;
    }
    loading();
    $.ajax({
        url: "/AjaxService/Statistics.asmx/GetStopDetail",
        data: "{DeviceID:'" + id + "',StartTime:'" + st + "',EndTime:'" + et + "'}",
        success: function (r) {         
            if (r.d == "") {
               $(this).alertmsg('info', '查询数据出错');
                removeloading();
                return;
            }
            var d = JSON.parse(r.d);
            var html = []; var rowIndex = 0;
            var xAxis = []; var speeds = [];
            $.each(d, function (k, v) {
                xAxis.push(v.starttime);
                html.push('<tr data-id="' + (rowIndex++) + '">');
                html.push(' <td>' + rowIndex + '</td>');
                html.push(' <td>' + name + '</td>');
                html.push(' <td>' + v.starttime + '</td>');
                html.push(' <td>' + v.endtime + '</td>');
                html.push(' <td>' + v.lat + "," + v.lon + '</td>');
                html.push(' <td>' + v.time + '</td> ');
                html.push(" <td><a href='#' onclick='getAddress(" + v.lat + " , " + v.lon +",this)'>解析</a></td> </tr>");
            });

            $("#tbodyStopDetail").empty().append(html.join(''));
            removeloading();
        },
        error: function () {
            $(this).alertmsg('info', '查询数据出错');
            removeloading();

        }
    });
}
