//groupmanager
function GetGroupsList() { 
    var info = mg.GetUserInfo();
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetGroupList",
        pars: { userid: info.UserID },
        success: function (data) {
            var groups = JSON.parse(data);
            var html = [];
            var $selected = $(".am-list");
            for (var i = 0; i < groups.length; i++) {
                sessionStorage.setItem("gid_"+groups[i].GroupID,null);
                html.push(' <li> <a onclick="groupDetail(' + groups[i].GroupID + ',\'' + groups[i].GroupName + '\')" href="#"><i class="am-icon-users am-icon-fw"></i>' + groups[i].GroupName + '</a></li>');
            }
            $selected.empty().append(html.join(''));
          
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function groupDetail(id,name) {
    sessionStorage.setItem("gid_"+id,name);
    window.location.href="addgroup.html?id="+ id
}

//addgroup 

function frmsumbit() { 
    if ( name!= 'null') {
        UpdateGroupName()
    } else {
        AddGroup();
    }
}

function UpdateGroupName()
{
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "UpdateGroups",
        pars: { groupid: groupid , groupname: $("#txtGroupName").val() },
        success: function (data) {
            var m = new amModal({ msg:data.Message});
            m.open();
            if (data.StatusCode === 200) {
                m.onConfirm(function () {
                    history.back();
                });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function AddGroup() {
    var info = mg.GetUserInfo();
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "AddGroups",
        pars: { userid: info.UserID, groupname: $("#txtGroupName").val() },
        success: function (data) {
            var m = new amModal({msg:data.Message});
            m.open();
            if (data.StatusCode === 200) {
                m.onConfirm(function () {
                    history.back();
                });
            } 
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function DeleteGroups() {
   var name = $("#txtGroupName").val();
   var modal = new amModal({ msg: "确定删除该分组:" + name, cancel: true });
    modal.open(); 
    modal.onConfirm(function () { 
        $.ajax({
            url: "DevicesAjax.asmx",
            way: "DeleteGroups",
            pars: { groupid: groupid },
            success: function (data) { 
              // modal.close(); 
               var m = new amModal({ msg: data.Message ,title:"提示"}); 
               m.open();
               m.onConfirm(function () {
                    if (data.StatusCode === 200) {
                        history.back();
                    } else {
                        m.close();
                    }
                });
            },
            error: function (err) {
                console.log(err);
            }
        });
    });
}