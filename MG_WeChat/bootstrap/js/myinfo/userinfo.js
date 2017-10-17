 
function GetUserInfo() {
    var info = mg.GetUserInfo();
    $.ajax({
        url: "MessageAjax.asmx",
        way: "GetMessageList",
        pars: { currentindex: 1, pagecount: 50, userid: info.UserID },
        success: function (data) {
            var json = JSON.parse(data);
            var html = [];
            $("#messagelist").empty();
            for (var i = 0; i < json.length; i++) {
                html.push('<li style="height:40px;"> ');
                html.push('<span style="height:100%;position:absolute;top:10px;">' + json[i].rowIndex + '.</span> ');
                html.push('<div class="am-margin-left"> ');
                html.push('<p class="am-text-secondary am-text-sm">' + (json[i].DeviceName == "" ? json[i].SerialNumber : json[i].DeviceName) + '</p> ');
                html.push('<p class="am-text-secondary" style="margin-top:-28px;"><span >' + json[i].Message + '</span></p>  ');
                html.push('</div>   <span class="am-list-date am-text-primary">' + json[i].Created + '</span> ');
                html.push(' </li> ');
                if (i % 10 == 0) {
                    $("#messagelist").append(html.join(''));
                    html = [];
                }
            }
            $("#messagelist").append(html.join(''));

        }
    });
}