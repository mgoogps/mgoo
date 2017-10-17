function GetGeoFenceList() {
    localStorage.setItem("geofenceid",null);
    var info = mg.GetUserInfo();
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetGeoFenceList",
        pars: { userid: info.UserID },
        success: function (data) {
            var list = JSON.parse(data);
            var html = [];
            var $selected = $(".am-list");
            for (var i = 0; i < list.length; i++) {
                html.push(' <li> <a onclick="GeoFenceDetail(' + list[i].GeofenceID + ',' + list[i].DeviceID + ')" href="#"><i class="am-icon-circle-o am-icon-fw"></i>' + list[i].FenceName + '</a>   <span class="am-list-date">' + list[i].Created + '</span></li>');
            }
            $selected.empty().append(html.join(''));
        },
        error: function (err) {
            console.log(err);
        }
    });
}


function GeoFenceDetail(id,did) {
    localStorage.setItem("geofenceid", id);
    window.location.href = "addgeofence.html?geofenceid="+id+"&deviceid="+did;
}

 
//以下是 addgeofence.html

var carMarker;
function GetDeviceDetail() {
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetMonitorByDeviceID",
        pars: { deviceid: deviceid },
        success: function (data) {
            var d = JSON.parse(data);
            if (carMarker) { 
                mgoo.map.remove([carMarker.marker]);
            }
            carMarker = new Marker({
                map: mgoo.map, mapType: mgoo.mapType, DeviceID: deviceid, lng: d.OLng, lat: d.OLat,
                line: d.Status == 1 ? "online" : "offline", course: d.Course
            });
            carMarker.show({ showTitle: false, titleText: d.DeviceName });
            
            addCircle([d.OLng, d.OLat]);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function stepClick(t) {
    if (t==1) {
        $("#step1").text("第一步:标记地图");
        $("#step2").text("下一步");
        var did = $("#select-device").val(); 
        if (did != deviceid) {
            deviceid = did;
            GetDeviceDetail();
        }
                
    } else if(t==2){
        $("#step1").text("上一步");
        $("#step2").text("第二步:填写信息"); 
        $("#txtRadius").val(editor._circle.getRadius());
        $("#txtLng").val(editor._circle.getCenter().getLng());
        $("#txtLat").val(editor._circle.getCenter().getLat());
    }
}

function frmsumbit() { 
    if (geofenceid != 'null' && geofenceid) {
        UpdateGeoFence();
    } else {
        AddGeoFence();
    }
}

function UpdateGeoFence() {
    var name = $("#txtGenFenceName").val();
    deviceid = $("#select-device").val()
    var lng = $("#txtLng").val();
    var lat = $("#txtLat").val();
    var radius = $("#txtRadius").val();
    var description = $("#txtDescription").val();
    
    if (name.length == 0) {
        new amModal({ msg: "名称不能为空." }).open();
        return;
    }
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "UpdateGeoFence",
        pars: { fenceid: geofenceid, fencename: name,  latitude: lat, longitude: lng, radius: radius, description: description },
        success: function (data) {

            var modal = new amModal({ msg: data.Message })
            modal.open();
            modal.onConfirm(function () {
                if (data.StatusCode === 200) {
                    history.back();
                }
            });
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function AddGeoFence() {
    var name = $("#txtGenFenceName").val(); 
    deviceid = $("#select-device").val();
    var lng = $("#txtLng").val();
    var lat = $("#txtLat").val();
    var radius = $("#txtRadius").val();
    var description = $("#txtDescription").val();
    var info = mg.GetUserInfo();
    if (name.length == 0) {
        new amModal({ msg: "名称不能为空." }).open();
        return;
    }
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "AddGeoFence",
        pars: { fencename: name, userid: info.UserID, deviceid: deviceid, latitude: lat, longitude: lng, radius: radius, description: description },
        success: function (data) {
            
            var modal = new amModal({ msg: data.Message })
            modal.open();
            modal.onConfirm(function () {
                if (data.StatusCode === 200) {
                    history.back();
                }
            });
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function addCircle(point,radius) {
    if (editor._circleEditor) {
        editor._circleEditor.close();
    }
    mgoo.map.remove(mgoo.map.getAllOverlays("circle"));
   
    editor._circle = (function () {
        var circle = new AMap.Circle({
            center: point || mgoo.map.getCenter(),//  圆心位置
            radius: radius || 1000, //半径
            strokeColor: "#F33", //线颜色
            strokeOpacity: 1, //线透明度
            strokeWeight: 3, //线粗细度
            fillColor: "#ee2200", //填充颜色
            fillOpacity: 0.35//填充透明度
        });
        circle.setMap(mgoo.map);
        return circle;
    })();
    new AMap.CircleEditor(mgoo.map, editor._circle);
   
    editor._circleEditor = new AMap.CircleEditor(mgoo.map, editor._circle);
    editor._circleEditor.open();
    mgoo.map.setFitView();
}

function GetGeoFenceInfo() {
    var id = localStorage.getItem("geofenceid");
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetGeoFenceInfoByID",
        pars: { fenceid: id },
        success: function (data) {
            var d = JSON.parse(data);
            //{"GeofenceID":"891","FenceName":"测试","Latitude":"23.0650072184429","Longitude":"114.018587772872","Radius":"1255.00","Created":"2016-9-25 11:00:52","UserID":"6","DeviceID":"2635","Description":"11111备注描述"}
            $("#txtGenFenceName").val(d.FenceName);
            // var $curr = $("#select-device").find('option[value=' + d.DeviceID + ']');
           // $curr.attr("selected", !$curr.get(0).selected);
            // 不支持 MutationObserver 的浏览器使用 JS 操作 select 以后需要手动触发 `changed.selected.amui` 事件
           // if (!$.AMUI.support.mutationobserver) {
           //     $selected.trigger('changed.selected.amui');
           // }

            $("#txtLng").val(d.Longitude);
             $("#txtLat").val(d.Latitude);
            $("#txtRadius").val(d.Radius);
            $("#txtDescription").val(d.Description);

            addCircle([d.Longitude, d.Latitude],d.Radius);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function GetGroupsList() {
    var info = mg.GetUserInfo();
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetGroupList",
        pars: { userid: info.UserID },
        success: function (data) {
            var groups = JSON.parse(data);
            
            var html = [];
            for (var i = 0; i < groups.length; i++) { 
                html.push(' <optgroup label=" ' + groups[i].GroupName + '" id="group-'+groups[i].GroupID+'"> </optgroup> ');
            }
            $("#select-device").empty().append(html.join(''));

            GetDevicesList();
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function GetDevicesList() {
    var info = mg.GetUserInfo();

    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetDevicesList",
        pars: { userid: info.UserID },
        success: function (data) {
            var deviceList = JSON.parse(data);
        
            for (var i = 0; i < deviceList.length; i++) {
                var device = deviceList[i];
                var html = [];
                var selected = "";
                if (deviceid && deviceid == device.DeviceID) {
                    console.log(".........")
                    selected="selected"
                }
                html.push(' <option value="' + device.DeviceID + '" ' + selected + '>' + device.DeviceName + '</option>');
               
                $("#group-" + device.GroupID).append(html.join(''));
            }
            deviceid = $("#select-device").val();
           
            if (geofenceid != "null" && geofenceid) {
               
                GetGeoFenceInfo();
            } else if (deviceid) {
                var $curr = $("#select-device").find('option[value=' + deviceid + ']');
                $curr.attr("selected", !$curr.get(0).selected);
                // 不支持 MutationObserver 的浏览器使用 JS 操作 select 以后需要手动触发 `changed.selected.amui` 事件
                if (!$.AMUI.support.mutationobserver) {
                    $selected.trigger('changed.selected.amui');
                }
              
                GetDeviceDetail();
            }
        },
        error: function () {

        }
    });
}


function DeletedGeoFence() {
    var modal = new amModal({ msg: "确定删除该围栏？", cancel: true });
    modal.open();
    modal.onConfirm(function () {
        $.ajax({
            url: "DevicesAjax.asmx",
            way: "DeleteGeoFence",
            pars: { fenceid: geofenceid },
            success: function (data) {
                modal = new amModal({ msg: data.Message });
                modal.open();
                modal.onConfirm(function () {
                    if (data.StatusCode === 200) {
                        history.back();
                    }
                });

            },
            error: function (err) {
                console.log(err);
            }
        });
    }); 
}