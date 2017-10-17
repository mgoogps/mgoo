<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unlock.aspx.cs" Inherits="MG_GPS.Unlock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>美谷-北斗防盗箱</title>
    <script src="http://webapi.amap.com/maps?v=1.3&key=e138c930eb6b372d830b3e8e3fbc0bb0&&plugin=AMap.Scale,AMap.OverView,AMap.ToolBar"></script>   
     
    <script src="Scripts/map.js"></script>
      
<%--    <script src="http://code.jquery.com/jquery-latest.js"></script>--%>
    <script src="Scripts/jquery-1.8.3.js"></script>
    <link rel="stylesheet" href="http://cdn.amazeui.org/amazeui/2.7.2/css/amazeui.min.css" />
    <script src="http://cdn.amazeui.org/amazeui/2.7.2/js/amazeui.min.js"></script>

    <style type="text/css">
        html, body, #divMap {
            height:100%;
            width:100%;
            overflow:hidden;
            margin:0;
            padding:0;
        }
        #divMap {
             /*margin-top:-19px;*/
           
        }
        #divUnlock {
            position:absolute;
            bottom:20px;
            left:calc(50% - 25px); 
        }
        #divMenu {
            position:absolute;
            bottom:20px;
            right:20px; 
        }
        .header {
            position:absolute;
            height:30px;
            width:100%;
            background-color:#F2F2F2;
             filter:alpha(opacity=50);  
            -moz-opacity:0.5;  
            -khtml-opacity: 0.5;  
            opacity: 0.5;  
            top:0px;
            left:0px;
            right:0px;
             z-index:1000;
                
        }
        .header p {
            line-height:1.0; 
        }
        .am-offcanvas-bar {
            width:170px;
        }
    </style>
</head>
<body>
    <div class="header">
        <p>
             
        </p>
        <p style="margin-top:-15px;  border-bottom:1px solid #D6D6D6">
       
        </p> 
    </div>
    <div id="divMap"></div>
    <form action="Unlock.aspx?action=unlock" method="post" id="frm">
       <input type="hidden" name="DeviceID" value="<%= DeviceID %>" />
      <%--   <input type="hidden" name="SerialNumber" value="<%= dic["SerialNumber"] %>" />--%>
        <input type="hidden" name="Password" id="txtPassword" value="" />
    </form>
    
     
    <div id="divUnlock" > 
         <img src="Images/icons/unlock.png" style="width:50px;" id="imageUnlock" />
    </div>
    <div id="divMenu" class="am-dropdown am-dropdown-up" data-am-dropdown-toggle>
         <img src="Images/icons/menu.png" style="width:50px;" id="imageMenu" />
          <ul class="am-dropdown-content"> 
            <li class="am-dropdown-header">请选择</li>
            <li class="am-divider"></li>
            <li><a href="#">设备信息</a></li>
            <li class="am-divider"></li>
            <li><a href="#">开锁记录</a></li>
            <li class="am-divider"></li>
            <li><a href="#">报警记录</a></li>
            <li class="am-divider"></li>
            <li><a href="#" id="aFence">开启围栏</a></li> 
            <li class="am-divider"></li> 
            <li><a href="#">历史轨迹</a></li> 
        </ul>
    </div>

    <%--弹出文本框供输入--%>
    <div class="am-modal am-modal-prompt" tabindex="-1" id="my-prompt">
        <div class="am-modal-dialog">
            <div class="am-modal-hd">请输入授权码</div>
            <div class="am-modal-bd">
                <br />
                 <input type="text" class="am-modal-prompt-input" maxlength="10">
            </div>
            <div class="am-modal-footer">
                <span class="am-modal-btn" data-am-modal-cancel>取消</span>
                <span class="am-modal-btn" data-am-modal-confirm>提交</span>
            </div>
        </div>
    </div>
    <button type="button"  class="am-btn am-btn-success" id="doc-prompt-toggle">  </button>

    
    <%--提示框--%>
    <div class="am-modal am-modal-alert" tabindex="-1" id="my-alert">
        <div class="am-modal-dialog">
            <div class="am-modal-hd">提示</div>
            <div class="am-modal-bd">
                 
            </div>
            <div class="am-modal-footer">
                <span class="am-modal-btn">确定</span>
            </div>
        </div>
    </div>
 
    <%--报警记录--%>
    <div class="am-modal am-modal-no-btn" tabindex="-1" id="modalMessageList">
        <div class="am-modal-dialog">
            <div class="am-modal-hd">
                 报警记录
                <a href="javascript: void(0)" class="am-close am-close-spin" data-am-modal-close>&times;</a>
            </div>
            <div class="am-modal-bd">
               <div style="overflow:auto;"><%--am-collapse--%>
                     <ul class="am-list  admin-sidebar-sub" id="messagelist" style="height:400px;">
               <%-- <li style="height:45px;" onclick="window.location.href='msgdetail.html?id=24544028'"> <span class="am-text-primary" style="height:100%;position:absolute;top:8px;">2.</span> <div class="am-margin-left-lg"> <p class="am-text-primary">1号智运箱</p> <p class="am-text-primary am-text-sm" style="margin-top:-20px;"><span>断电报警</span></p>  </div>   <span class="am-list-date am-text-primary">2017-7-6 9:05:08</span>  </li>--%>
                </ul>  
               </div> 
            </div>
        </div>
    </div><%--, closeViaDimmer: 0, width: 400, height: 225--%>
    <button  type="button"  class="am-btn am-btn-primary" id="btnModalMessageList" data-am-modal="{target: '#modalMessageList'}"> </button>

      <%--开锁记录--%>
    <div class="am-modal am-modal-no-btn" tabindex="-1" id="modalUnlockList">
        <div class="am-modal-dialog">
            <div class="am-modal-hd">
                 开锁记录
                <a href="javascript: void(0)" class="am-close am-close-spin" data-am-modal-close>&times;</a>
            </div>
            <div class="am-modal-bd">
               <div style="overflow:auto;"><%--am-collapse--%>
                     <ul class="am-list  admin-sidebar-sub" id="unlocklist" style="height:400px;">
             
                     </ul>  
               </div> 
            </div>
        </div>
    </div><%--, closeViaDimmer: 0, width: 400, height: 225--%>
    <button  type="button"  class="am-btn am-btn-primary" id="btnModalUnlockList" data-am-modal="{target: '#modalUnlockList'}"> </button>

   
    <script type="text/javascript"> 
        var mgoo = new mgMap("divMap", "GAODE");
       
        mgoo.DEFAULT_ZOOM = 17;
        mgoo.loadMap();
          
       
        var com ={
            DeviceID:<%= DeviceID%>,
            DeviceName:"" ,
            FenceID :"",
            Lat:0,
            Lng:0
        };
       
        document.getElementById("divUnlock").addEventListener("click",function () { 
            $("#my-alert .am-modal-hd").text("正在开锁......");
            $("#my-alert .am-modal-bd").html(' <span class="am-icon-spinner am-icon-spin"></span>');
            $("#my-alert .am-modal-footer").hide();
            $("#my-prompt .am-modal-hd").text("请输入授权码");
            $("#my-prompt .am-modal-prompt-input").val("");
            $("#my-prompt").attr("type","1");
            $('#doc-prompt-toggle').trigger("click"); 
        });
        document.getElementById("divMenu").addEventListener("click",function () { 
            $("#divMenu").dropdown('toggle');// 下拉状态交替；
        });
       
        $(function () {
            $(".am-dropdown-content li[class!=am-dropdown-header][class!=am-divider]").on("click",function () { 
                $(".am-dropdown-content li[class!=am-divider][class!=am-dropdown-header]").attr("class","");
                $(this).attr("class","am-active");
                var text = $(this).text();
                switch (text) {
                    case"报警记录": 
                        loading();
                        MessageList(function () {
                            loading();
                            $("#btnModalMessageList").trigger("click");
                        });
                        break;
                    case"开锁记录":
                        loading();
                        UnLockList(function () {
                            loading();
                            $("#btnModalUnlockList").trigger("click");
                        });
                        break;
                    case"设备信息":
                        window.location.href="Unlock/DeviceInfo.html?deviceid="+com.DeviceID;
                        break;
                    case"开启围栏":
                        $("#my-prompt .am-modal-hd").text("请输入半径(米)");
                        $("#my-prompt .am-modal-prompt-input").val("100");
                        $('#doc-prompt-toggle').trigger("click");
                        $("#my-prompt").attr("type","2");
                        break;
                    case"关闭围栏":
                        DelFence();
                        break;
                    case"历史轨迹":
                        window.location.href="Unlock/History.html?deviceid="+com.DeviceID;
                        break;
                }
            });

            $('#doc-prompt-toggle').on('click', function() {
                $('#my-prompt').modal({
                    relatedTarget: this,
                    onConfirm: function(e) {
                        if (e.data) { 
                            var type = $('#my-prompt').attr("type")
                            if (type == 1) {
                                $("#my-alert").modal("toggle");
                                Unlock(e.data);
                            }else {
                                Fence(e.data);
                            } 
                        } 
                    },
                    onCancel: function(e) {
                       
                    }
                });
            });

            GetTracking(); 
            setInterval("GetTracking()",10000);
        });

        function GetTracking( ) {
          
            $.ajax({
                url: "/AjaxService/UnlockAction.ashx?action=gettracking&DeviceID=" + com.DeviceID,
                data: {},
               // contentType: "application/json", 
                dataType:"JSON",
                type: "POST",
                success:function (res) {
                    com.DeviceName = res.DeviceName ;
                    if (com.marker) {
                        mgoo.clearOverlays({clearMarker:[com.marker]});
                    }
                    com.Lat = res.OLat;
                    com.Lng = res.OLng;
                    var marker = new Marker({ lat:  res.OLat, lng:res.OLng, mapType: mgoo.mapType, map: mgoo.map, course: 90 });
                    marker.show({ icon: "/Images/icons/box2.png", showTitle: true, titleText: res.DeviceName });
                    com.marker = marker.marker;
                    var point = mgoo.Point(res.OLat, res.OLng);
                    mgoo.panTo(res.OLng, res.OLat);
                    var s = [];
                    var speed = "";
                    if  (parseInt( res.Speed) > 0) 
                    {
                        speed = ",速度:"+res.Speed+"km/h";
                    }
                    s.push("<p> 时间:" + res.DeviceDate + speed + ",方向:" + res.CourseName + " </p>");
                    var p1 = "<p> 时间:" + res.DeviceDate + speed + ",方向:" + res.CourseName + ",状态:" + res.DataContext + " </p>";
                    var hp = $(".header p");
                    $(hp[0]).html(p1);
                    $(hp[1]).html("地址:"+res.Address); 

                    DrawCircle(res.GeofenceID,res.Radius,res.Latitude,res.LongItude) 
                 
                }
            });
        }

        function UnLockList(callback)
        {
            $.ajax({
                url: "/AjaxService/UnlockAction.ashx?action=unlocklist&DeviceID=" + com.DeviceID,
                data: {},
                // contentType: "application/json", 
                dataType:"JSON",
                type: "POST",
                success:function (res) {
                    var json = res;// JSON.parse(data);
                    var html = [];
                    $("#unlocklist").empty();
                    for (var i = 0; i < json.length; i++) {
                        var isResponse="成功";
                        if (json[i].IsResponse == "False" || json[i].isResponse=="0") {
                            isResponse ="失败";
                        }
                        html.push('<li style="height:45px;" > ');
                        html.push('<span class="am-text-primary" style="height:100%;position:absolute;top:8px;margin-left:-120px;">' + json[i].rowIndex + '.</span> ');
                        html.push('<div class="am-margin-left-lg"> ');
                        html.push('<p class="am-text-primary" style="margin-left:-140px">' + com.DeviceName + '</p> ');
                        html.push('<p class="am-text-primary am-text-sm" style="margin-top:-20px;margin-left:-120px;"><span >' + json[i].CommandName+"-"+isResponse + '</span></p>  ');
                        html.push('</div>   <span class="am-list-date am-text-primary">' + json[i].CreateDate + '</span> ');
                        html.push(' </li> ');
                        if (i%10 == 0) {
                            $("#unlocklist").append(html.join(''));
                            html = [];
                        }
                    }
                    $("#unlocklist").append(html.join('')); 
                    callback();
                }
            });
        }
        
        function MessageList(callback) {
 
            $.ajax({
                url: "/AjaxService/UnlockAction.ashx?action=getmessagelist&DeviceID=" + com.DeviceID+"&currentIndex=1&pagecount=50&type=0",
                dataType:"JSON",
                type: "POST",
                success: function (data) {
                    
                    var json = data;// JSON.parse(data);
                    var html = [];
                    $("#messagelist").empty();
                    for (var i = 0; i < json.length; i++) {    
                        html.push('<li style="height:45px;" > ');
                        html.push('<span class="am-text-primary" style="height:100%;position:absolute;top:8px;margin-left:-120px;">' + json[i].rowIndex + '.</span> ');
                        html.push('<div class="am-margin-left-lg"> ');
                        html.push('<p class="am-text-primary" style="margin-left:-140px">' + (json[i].DeviceName == "" ? json[i].SerialNumber : json[i].DeviceName) + '</p> ');
                        html.push('<p class="am-text-primary am-text-sm" style="margin-top:-20px;margin-left:-150px;"><span >' + json[i].Message + '</span></p>  ');
                        html.push('</div>   <span class="am-list-date am-text-primary">' + json[i].Created + '</span> ');
                        html.push(' </li> ');
                        if (i%10 == 0) {
                            $("#messagelist").append(html.join(''));
                            html = [];
                        }
                    }
                    $("#messagelist").append(html.join('')); 
                    callback();
                },
                error:function (a,b,c) {
                    console.log("error....");
                }
            });
        }
     
        function Unlock(password) {
            $.ajax({
                url: "/AjaxService/UnlockAction.ashx?action=unlock&DeviceID=" + com.DeviceID+"&password="+password,
                data: {},
                // contentType: "application/json", 
                dataType:"JSON",
                type: "POST",
                success:function (res) {
                    var status = res.Result;
                    if (status) {
                        var msg= "";
                        if (status == 1) {
                            msg = "开锁成功.";
                        }else if (status == 2) {
                            msg = "密码错误,开锁失败.";
                        } else if(status == 3){
                            msg = "参数错误.";
                        }else {
                            msg = "开锁失败.";
                        } 
                        $("#my-prompt .am-modal-prompt-input").val("");
                        $("#my-alert .am-modal-hd").text("提示");
                        $("#my-alert .am-modal-footer").show();
                        $("#my-alert .am-modal-bd").text(msg);
                        //$("#my-alert").modal("toggle");
                    }         
                }
            });
        }
        
        function Fence(radius)
        { 
            $.ajax({
                url: "/AjaxService/UnlockAction.ashx?action=addfence&DeviceID=" + com.DeviceID+"&Radius="+radius,
                data: {},
                // contentType: "application/json", 
                dataType:"JSON",
                type: "POST",
                success:function (res) {
                    amAlert(res.Message);
                    if (res.StatusCode === 200) { 
                        DrawCircle(res. Result,radius,com.Lat ,com.Lng );
                    }
                }
            }); 
        }

        function DrawCircle(fenceid,radius,lat,lng) {
            if (fenceid && fenceid != com.FenceID) {
                $("#aFence").text("关闭围栏");
                var point = mgoo.Point(lng,lat);
                var circle = new Circle({mapType: mgoo.mapType,map:mgoo.map,point:point,radius:radius});
                com.circle = circle.circle;
                mgoo.setFitView();
            }else if(com.circle && !fenceid){ 
                mgoo.clearOverlays({ clearMarker:[com.circle]});
                com.circle = undefined;
                $("#aFence").text("开启围栏");
            }
            com.FenceID = fenceid;
        }

        function DelFence(fenceID) {
            $.ajax({
                url: "/AjaxService/UnlockAction.ashx?action=closefence&DeviceID=" + com.DeviceID+"&fenceID="+  com.FenceID,
                data: {},
                // contentType: "application/json", 
                dataType:"JSON",
                type: "POST",
                success:function (res) {
                    amAlert(res.Message);
                    if (res.StatusCode === 200) {
                        DrawCircle("");
                    }
                }
            }); 
        }
        function loading() {
            $("#my-alert .am-modal-hd").text("正在请求数据......");
            $("#my-alert .am-modal-bd").html(' <span class="am-icon-spinner am-icon-spin"></span>');
            $("#my-alert .am-modal-footer").hide();
            $("#my-alert").modal("toggle");
        }
        function amAlert(msg){
            $("#my-alert .am-modal-hd").text("提示");
            $("#my-alert .am-modal-footer").show();
            $("#my-alert .am-modal-bd").text(msg);
            $("#my-alert").modal("toggle");
        }
    </script>
</body>
</html>
