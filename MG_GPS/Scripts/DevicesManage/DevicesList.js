 
function GetDeviceList() { 
    $.ajax({
        url: "/AjaxService/DeviceManager.asmx/GetDeviceList",
        data: "{user:'" + mgoo.CurrentzTreeUserID + "',devicename:'" + $("#txtDeviceName").val() + "'}",
        success: function (r) { 
            var d = JSON.parse(r.d);
            var html = [];
            var rowIndex = 0;
            //"[{"id":"4210038045","name":"4210038045","model":"","simnumber":null,"carnumber":null,"contact":null,"cellphone":null,"creattime":"2015-11-26T09:26:44.358Z","activetime":"2015-12-09T09:54:22.323Z","endtime":"2016-12-09T09:54:22.323Z","userid":"captain","image":"","islbs":false},{"id":"4209809647","name":"4209809647","model":"X83","simnumber":null,"carnumber":null,"contact":null,"cellphone":null,"creattime":"2015-11-27T07:04:56.425Z","activetime":"0001-01-01T00:00:00Z","endtime":"0001-01-01T00:00:00Z","userid":"captain","image":"","islbs":false},{"id":"3303003813","name":"3303003813","model":"X83","simnumber":"","carnumber":null,"contact":null,"cellphone":null,"creattime":"2015-11-27T07:04:56.425Z","activetime":"2015-12-22T01:40:26.776Z","endtime":"2016-12-22T01:40:26.776Z","userid":"captain","image":"","islbs":false},{"id":"4206031237","name":"奥迪Q3","model":"X21","simnumber":"13459175281","carnumber":null,"contact":null,"cellphone":null,"creattime":"2015-12-01T09:52:11.637Z","activetime":"0001-01-01T00:00:00Z","endtime":"0001-01-01T00:00:00Z","userid":"captain","image":"","islbs":false},{"id":"4109151598","name":"4109151598","model":"X83","simnumber":"18210480847","carnumber":null,"contact":null,"cellphone":null,"creattime":"2015-12-21T10:07:10.885Z","activetime":"2015-12-21T10:07:21.688Z","endtime":"2016-12-21T10:07:21.688Z","userid":"captain","image":"","islbs":false},{"id":"4210029119","name":"4210029119","model":"X21","simnumber":null,"carnumber":null,"contact":null,"cellphone":null,"creattime":"2015-12-23T03:14:48.299Z","activetime":"2015-12-23T03:15:09.926Z","endtime":"2016-12-23T03:15:09.926Z","userid":"captain","image":"","islbs":false},{"id":"352888800020397","name":"352888800020397","model":"Concox","simnumber":null,"carnumber":null,"contact":null,"cellphone":null,"creattime":"2016-01-13T02:59:36.575Z","activetime":"2016-01-13T02:59:43.807Z","endtime":"2017-01-13T02:59:43.807Z","userid":"captain","image":"","islbs":false},{"id":"300800000034939","name":"300800000034939","model":"Concox","simnumber":null,"carnumber":null,"contact":null,"cellphone":null,"creattime":"2016-01-13T10:09:17.098Z","activetime":"2016-01-13T10:09:25.655Z","endtime":"2017-01-13T10:09:25.655Z","userid":"captain","image":"","islbs":false},{"id":"4210000307","name":"文楼海立星x10","model":"X21","simnumber":"14753351681","carnumber":"","contact":"5","cellphone":"","creattime":"2015-12-01T09:52:11.637Z","activetime":"2015-12-02T02:44:57.719Z","endtime":"2016-12-02T02:44:57.719Z","userid":"captain","image":"S-160302021133.png","islbs":true},{"id":"4210032550","name":"董全惠卡罗拉","model":"X21","simnumber":"13416477868","carnumber":"","contact":"","cellphone":"","creattime":"2015-12-01T09:52:11.637Z","activetime":"2015-12-01T10:19:49.236Z","endtime":"2016-12-01T10:19:49.236Z","userid":"captain","image":"MG-160318022040.png","islbs":false},{"id":"352888800021111","name":"352888800021111","model":"Concox","simnumber":null,"carnumber":null,"contact":null,"cellphone":null,"creattime":"2016-01-13T10:09:17.098Z","activetime":"2016-01-13T10:09:25.655Z","endtime":"2017-01-13T10:09:25.655Z","userid":"captain","image":"","islbs":false},{"id":"352888800013709","name":"352888800013709","model":"Concox","simnumber":null,"carnumber":null,"contact":null,"cellphone":null,"creattime":"2016-01-13T10:09:17.098Z","activetime":"2016-01-13T10:09:25.655Z","endtime":"2017-01-13T10:09:25.655Z","userid":"captain","image":"","islbs":false},{"id":"4210009523","name":"李总","model":"X21","simnumber":"13552527045","carnumber":"","contact":"","cellphone":"","creattime":"2015-12-01T09:52:11.637Z","activetime":"2015-12-01T10:22:25.405Z","endtime":"2016-12-01T10:22:25.405Z","userid":"captain","image":"undefined","islbs":false},{"id":"4210039536","name":"A6","model":"X21","simnumber":"13520726064","carnumber":"","contact":"","cellphone":"","creattime":"2015-12-01T09:52:11.637Z","activetime":"2015-12-01T10:14:51.443Z","endtime":"2016-12-01T10:14:51.444Z","userid":"captain","image":"MG-160318045723.png","islbs":false}]"
            //"[{     47","Model":"X83","SimNumber":null,"CarNumber":null,"Contact":null,"CellPhone":null,"CreatTime":"2015-11-27 15:04:56","ActiveTime":"0001-01-01 08:00:00","EndTime":"0001-01-01 08:00:00","UserID":"captain","Image":"","Islbs":false},{"Id":"3303003813","Name":"3303003813","Model":"X83","SimNumber":"","CarNumber":null,"Contact":null,"CellPhone":null,"CreatTime":"2015-11-27 15:04:56","ActiveTime":"2015-12-22 09:40:26","EndTime":"2016-12-22 09:40:26","UserID":"captain","Image":"","Islbs":false},{"Id":"4206031237","Name":"奥迪Q3","Model":"X21","SimNumber":"13459175281","CarNumber":null,"Contact":null,"CellPhone":null,"CreatTime":"2015-12-01 17:52:11","ActiveTime":"0001-01-01 08:00:00","EndTime":"0001-01-01 08:00:00","UserID":"captain","Image":"","Islbs":false},{"Id":"4210029119","Name":"4210029119","Model":"X21","SimNumber":null,"CarNumber":null,"Contact":null,"CellPhone":null,"CreatTime":"2015-12-23 11:14:48","ActiveTime":"2015-12-23 11:15:09","EndTime":"2016-12-23 11:15:09","UserID":"captain","Image":"","Islbs":false},{"Id":"352888800020397","Name":"352888800020397","Model":"Concox","SimNumber":null,"CarNumber":null,"Contact":null,"CellPhone":null,"CreatTime":"2016-01-13 10:59:36","ActiveTime":"2016-01-13 10:59:43","EndTime":"2017-01-13 10:59:43","UserID":"captain","Image":"","Islbs":false},{"Id":"300800000034939","Name":"300800000034939","Model":"Concox","SimNumber":null,"CarNumber":null,"Contact":null,"CellPhone":null,"CreatTime":"2016-01-13 18:09:17","ActiveTime":"2016-01-13 18:09:25","EndTime":"2017-01-13 18:09:25","UserID":"captain","Image":"","Islbs":false},{"Id":"4210000307","Name":"文楼海立星x10","Model":"X21","SimNumber":"14753351681","CarNumber":"","Contact":"5","CellPhone":"","CreatTime":"2015-12-01 17:52:11","ActiveTime":"2015-12-02 10:44:57","EndTime":"2016-12-02 10:44:57","UserID":"captain","Image":"S-160302021133.png","Islbs":true},{"Id":"4210032550","Name":"董全惠卡罗拉","Model":"X21","SimNumber":"13416477868","CarNumber":"","Contact":"","CellPhone":"","CreatTime":"2015-12-01 17:52:11","ActiveTime":"2015-12-01 18:19:49","EndTime":"2016-12-01 18:19:49","UserID":"captain","Image":"MG-160318022040.png","Islbs":false},{"Id":"352888800021111","Name":"352888800021111","Model":"Concox","SimNumber":null,"CarNumber":null,"Contact":null,"CellPhone":null,"CreatTime":"2016-01-13 18:09:17","ActiveTime":"2016-01-13 18:09:25","EndTime":"2017-01-13 18:09:25","UserID":"captain","Image":"","Islbs":false},{"Id":"352888800013709","Name":"352888800013709","Model":"Concox","SimNumber":null,"CarNumber":null,"Contact":null,"CellPhone":null,"CreatTime":"2016-01-13 18:09:17","ActiveTime":"2016-01-13 18:09:25","EndTime":"2017-01-13 18:09:25","UserID":"captain","Image":"","Islbs":false},{"Id":"4210009523","Name":"李总","Model":"X21","SimNumber":"13552527045","CarNumber":"","Contact":"","CellPhone":"","CreatTime":"2015-12-01 17:52:11","ActiveTime":"2015-12-01 18:22:25","EndTime":"2016-12-01 18:22:25","UserID":"captain","Image":"undefined","Islbs":false},{"Id":"4210039536","Name":"A6","Model":"X21","SimNumber":"13520726064","CarNumber":"","Contact":"","CellPhone":"","CreatTime":"2015-12-01 17:52:11","ActiveTime":"2015-12-01 18:14:51","EndTime":"2016-12-01 18:14:51","UserID":"captain","Image":"MG-160318045723.png","Islbs":false},{"Id":"4109151598","Name":"4109151598","Model":"X83","SimNumber":"18210480847","CarNumber":"","Contact":"","CellPhone":"","CreatTime":"2015-12-21 18:07:10","ActiveTime":"2015-12-21 18:07:21","EndTime":"2016-12-21 18:07:21","UserID":"captain","Image":"undefined","Islbs":false}]"
            for (var i = 0; i < d.length; i++) {
                html.push("<tr><td>" + (++rowIndex) + "</td>");
                html.push("<td>" + d[i].Name + "</td>");
                html.push("<td>" + d[i].Model + "</td>");
                html.push("<td>" + d[i].SimNumber + "</td>");
                html.push("<td>" + d[i].CarNumber + "</td>");
                html.push("<td>" + d[i].Contact + "</td>");
                html.push("<td>" + d[i].CellPhone + "</td>");
                html.push("<td>" + d[i].CreatTime + "</td>");
                html.push("<td>" + ( d[i].ActiveTime == "0001-01-01 08:00:00" ?"未激活":d[i].ActiveTime) + "</td>");
                html.push("<td>" + (d[i].EndTime == "0001-01-01 08:00:00" ?"未激活": d[i].EndTime)  + "</td>");
                html.push("<td>" + d[i].UserID + "</td>");
                html.push("<td>" + (d[i].Islbs ? "启用" : "关闭") + "</td>");
                html.push(' <td><input type="checkbox" name="ids" data-toggle="icheck" value="' + d[i].Id + '"></td>');
                html.push(' <td>'); //data-id="DeviceInfo" data-title="设备信息"  data="{'Imei':'123'}"  
                html.push(' <a href="/Monitor/deviceinfo.html?id=' + d[i].Id + '" onclick="setDeviceID(' + d[i].Id + ')" data-width="600" data-height="440" class="btn btn-green" data-toggle="dialog" id="aDeviceInfo" data-on-load="editDeviceInfo" data-before-close="deviceinfo_navtab_beforeClose" data-id="DeviceInfo" data-reload-warn="本页已有打开的内容，确定将刷新本页内容，是否继续？">编辑</a>');
             //   html.push(' <a href="ajaxDone2.html" class="btn btn-red" data-toggle="doajax" data-confirm-msg="确定要删除该行信息吗？" onclick=del(this)>删除</a>');
                html.push(' </td>');
                html.push("</tr>");
            }
            $("#tBodyDeviceList").empty().append(html.join(''));          
        }
    });
}

function BtnClear() {
    $("#clear").val('');
            
}
function del(aa) {
    var tr = $(aa).parent().parent();
    tr.remove();
}

//批量删除事件**未完成
//function SelDel() {
 
//    $("input[type='checkbox']").on('ifChanged',function(e) {
//        alert("进入方法");
//        var checked = $(this).is(':checked'),val = $(this).val()                
//        if (checked) {
           
//            $(this).parent().remove();
//        }
//        else{
//            $(this).alertmsg('info', '你取消了' + val)
//        }
//    })

//}
function SelDel() {
    $("input[type='checkbox']:checked").each(function () {
        
        $(this).parent().parent().remove();
    });
}
function setDeviceID(DeviceID) {
  
    mgoo.Update_DeviceID = DeviceID;
}