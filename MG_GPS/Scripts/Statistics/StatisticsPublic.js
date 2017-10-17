$(function () { 
    $("#sDevice").selectpicker({
        // style: 'btn-info',
        size: 10,
        noneSelectedText: "搜索设备...",
        getText: function () {
            return this.text;
        }
        //header: true, 
    });
    var date = new Date();//today.formatDate('yyyy/MM/dd')
    $("#txtEndDate").val(date.formatDate("yyyy-MM-dd")).datepicker({
        minDate: "2014-01-01", maxDate: "2029-01-01"
    });
    date.setDate(date.getDate() - 1);
    if ($("#txtStartDate").length > 0) {
        $("#txtStartDate").val(date.formatDate("yyyy-MM-dd")).datepicker({
            minDate: "2014-01-01", maxDate: "2029-01-01"
        });
    } 
    $("#bjui-calendar").hide(); 
});

function ztree_returnjson() {
    return function () {
        var str = [];
        $.ajax({
            url: "/AjaxService/Statistics.asmx/GetUsers",
            async: false,
            dataType: "json",
            success: function (r) {
                var json = JSON.parse(r.d);
                for (var i = 0; i < json.length; i++) {
                    str.push({ id: json[i]._id, pId: json[i].parent, name: (json[i].username == "" ? json[i]._id : json[i].username), open: (i == 0 ? true : false), faicon: "user" });
                }
            }
        });
        return str;
    }();
}

function S_NodeCheck(e, treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj(treeId),
        nodes = zTree.getCheckedNodes(true)
    var ids = '', names = ''

    for (var i = 0; i < nodes.length; i++) {
        ids += ',' + nodes[i].id
        names += ',' + nodes[i].name
    }
    if (ids.length > 0) {
        ids = ids.substr(1), names = names.substr(1)
    }

    var $from = $('#' + treeId).data('fromObj')

    if ($from && $from.length) $from.val(names)

    getDevicesByUserId(treeNode.id);
}
//单击事件
function S_NodeClick(event, treeId, treeNode) {
    var zTree = $.fn.zTree.getZTreeObj(treeId)

    zTree.checkNode(treeNode, !treeNode.checked, true, true)

    // getDevicesByUserId(treeNode.id);

    event.preventDefault()
}

function getDevicesByUserId(id) {

    var html = [];
    html.push(' <option value="">--选择--</option>');
    $.ajax({
        url: "/AjaxService/Statistics.asmx/GetDevicesName",
        data: "{UserID:'" + id + "'}",
        success: function (r) {
            var d = JSON.parse(r.d);

            for (var i = 0; i < d.length; i++) {
                html.push(' <option value="' + d[i].Id + '">' + d[i].Name + '</option>');
            }
            $("#sDevice").empty().append(html.join(''));
            $('#sDevice').selectpicker('refresh');
        }
    });

}
