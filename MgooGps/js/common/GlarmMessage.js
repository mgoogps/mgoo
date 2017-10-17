function GetMessageList() {
    var model = $("#selectDictionary option:selected").val();
    var msgType = $("#selectMsgType option:selected").val();
    var time = $("#reservationtime").val();
    var startTime = time.split('到')[0];
    var endTime = time.split('到')[1];

    var postData = { "userid": UserID, "model": model, msgtype: msgType, "starttime": startTime, "endtime": endTime };
 
    $.ajax({
        url: "AjaxService/AjaxService.ashx?action=get_statistics_alarmlist",
        type: 'POST',
        dataType: 'json',
        data: postData,
        success: function (dataList) {
            var i = 0;
            dataList = dataList[""];
            var emHtml = []; 
            if (dataList.length == 0) {
                emHtml.push('<tr>  <td colspan="8">没查询到数据.</td>  </tr>')
            }
            $.each(dataList, function (k, v) { 
                emHtml.push("<tr>");
                emHtml.push("<td>" + v.rowIndex + "</td>");
                emHtml.push("<td>" + v.UserName + "</td>");
                emHtml.push("<td>" + (v.DeviceName.length > 0 ? v.DeviceName : v.SerialNumber) + "</td>");
                emHtml.push("<td>" + v.SerialNumber + "</td>");
                emHtml.push("<td>" + v.Message + "</td>");
                emHtml.push("<td class=\"center\">" + v.Created + "</td>");
                emHtml.push("<td class=\"center\">" + v.DeviceUTCTime + "</td>");
                emHtml.push("<td class=\"center\">" + v.DataText + "</td>");
                //emHtml.push("<td class=\"center\">" + v.status + "</td>");
                emHtml.push("</tr>");
            });
            parent.loading(100); 
            $("#EMTable tbody").empty(); 
            $("#EMTable tbody").append(emHtml.join(''));
        }
    });

}