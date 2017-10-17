<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MG_GPS.Index" %>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="MG_BLL" %>
<!DOCTYPE html>
<html lang="zh">
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>GPS物联在线</title>
<meta name="Keywords" content="GPS,GPS定位,GPS定位平台,美谷"/>
<meta name="Description" content="东莞美谷科技有限公司"/> 
<!-- bootstrap - css -->
<link href="Scripts/B-JUI/BJUI/themes/css/bootstrap.css" rel="stylesheet">
<!-- core - css -->
<link href="Scripts/B-JUI/BJUI/themes/css/style.css" rel="stylesheet">
<link href="Scripts/B-JUI/BJUI/themes/blue/core.css" id="bjui-link-theme" rel="stylesheet">
<!-- plug - css -->
<link href="Scripts/B-JUI/BJUI/plugins/kindeditor_4.1.10/themes/default/default.css" rel="stylesheet">
<link href="Scripts/B-JUI/BJUI/plugins/colorpicker/css/bootstrap-colorpicker.min.css" rel="stylesheet">
<link href="Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.css" rel="stylesheet">
<link href="Scripts/B-JUI/BJUI/plugins/bootstrapSelect/bootstrap-select.css" rel="stylesheet">
<link href="Scripts/B-JUI/BJUI/themes/css/FA/css/font-awesome.min.css" rel="stylesheet">
    <link href="Scripts/B-JUI/font/typicons.min.css" rel="stylesheet" />
    <a href="Scripts/B-JUI/font/typicons.eot"></a>
    <a href="Scripts/B-JUI/font/typicons.woff"></a>
    <a href="Scripts/B-JUI/font/typicons.ttf"></a>
    <a href="Scripts/B-JUI/font/typicons.svg"></a>
<!--[if lte IE 7]>
<link href="BJUI/themes/css/ie7.css" rel="stylesheet">
<![endif]-->
<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
<!--[if lte IE 9]>
    <script src="BJUI/other/html5shiv.min.js"></script>
    <script src="BJUI/other/respond.min.js"></script>
<![endif]-->
<!-- jquery -->
<script src="Scripts/B-JUI/BJUI/js/jquery-1.7.2.min.js"></script>
<script src="Scripts/B-JUI/BJUI/js/jquery.cookie.js"></script>
<!--[if lte IE 9]>
<script src="BJUI/other/jquery.iframe-transport.js"></script>    
<![endif]-->
<!-- BJUI.all 分模块压缩版 -->
<script src="Scripts/B-JUI/BJUI/js/bjui-all.js"></script>
    <script src="/Scripts/Ajax.js"></script>
<!-- 以下是B-JUI的分模块未压缩版，建议开发调试阶段使用下面的版本 -->
<!--
<script src="BJUI/js/bjui-core.js"></script>
<script src="BJUI/js/bjui-regional.zh-CN.js"></script>
<script src="BJUI/js/bjui-frag.js"></script>
<script src="BJUI/js/bjui-extends.js"></script>
<script src="BJUI/js/bjui-basedrag.js"></script>
<script src="BJUI/js/bjui-slidebar.js"></script>
<script src="BJUI/js/bjui-contextmenu.js"></script>
<script src="BJUI/js/bjui-navtab.js"></script>
<script src="BJUI/js/bjui-dialog.js"></script>
<script src="BJUI/js/bjui-taskbar.js"></script>
<script src="BJUI/js/bjui-ajax.js"></script>
<script src="BJUI/js/bjui-alertmsg.js"></script>
<script src="BJUI/js/bjui-pagination.js"></script>
<script src="BJUI/js/bjui-util.date.js"></script>
<script src="BJUI/js/bjui-datepicker.js"></script>
<script src="BJUI/js/bjui-ajaxtab.js"></script>
<script src="BJUI/js/bjui-datagrid.js"></script>
<script src="BJUI/js/bjui-tablefixed.js"></script>
<script src="BJUI/js/bjui-tabledit.js"></script>
<script src="BJUI/js/bjui-spinner.js"></script>
<script src="BJUI/js/bjui-lookup.js"></script>
<script src="BJUI/js/bjui-tags.js"></script>
<script src="BJUI/js/bjui-upload.js"></script>
<script src="BJUI/js/bjui-theme.js"></script>
<script src="BJUI/js/bjui-initui.js"></script>
<script src="BJUI/js/bjui-plugins.js"></script>
-->
<!-- plugins -->
<!-- swfupload for uploadify && kindeditor -->
<script src="Scripts/B-JUI/BJUI/plugins/swfupload/swfupload.js"></script>
<!-- kindeditor -->
<script src="Scripts/B-JUI/BJUI/plugins/kindeditor_4.1.10/kindeditor-all.min.js"></script>
<script src="Scripts/B-JUI/BJUI/plugins/kindeditor_4.1.10/lang/zh_CN.js"></script>
<!-- colorpicker -->
<script src="Scripts/B-JUI/BJUI/plugins/colorpicker/js/bootstrap-colorpicker.min.js"></script>
<!-- ztree -->
<script src="Scripts/B-JUI/BJUI/plugins/ztree/jquery.ztree.all-3.5.js"></script>
<!-- nice validate -->
<script src="Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.js"></script>
<script src="Scripts/B-JUI/BJUI/plugins/niceValidator/jquery.validator.themes.js"></script>
<!-- bootstrap plugins -->
<script src="Scripts/B-JUI/BJUI/plugins/bootstrap.min.js"></script>
<script src="Scripts/B-JUI/BJUI/plugins/bootstrapSelect/bootstrap-select.min.js"></script>
<script src="Scripts/B-JUI/BJUI/plugins/bootstrapSelect/defaults-zh_CN.min.js"></script>
<!-- icheck -->
<script src="Scripts/B-JUI/BJUI/plugins/icheck/icheck.min.js"></script>
<!-- dragsort -->
<script src="Scripts/B-JUI/BJUI/plugins/dragsort/jquery.dragsort-0.5.1.min.js"></script>
<!-- HighCharts -->
<script src="Scripts/B-JUI/BJUI/plugins/highcharts/highcharts.js"></script>
<script src="Scripts/B-JUI/BJUI/plugins/highcharts/highcharts-3d.js"></script>
<script src="Scripts/B-JUI/BJUI/plugins/highcharts/themes/gray.js"></script>
<!-- ECharts -->
<%--<script src="Scripts/B-JUI/BJUI/plugins/echarts/echarts.js"></script>--%>
<!-- other plugins -->
<script src="Scripts/B-JUI/BJUI/plugins/other/jquery.autosize.js"></script>
<link href="Scripts/B-JUI/BJUI/plugins/uploadify/css/uploadify.css" rel="stylesheet">
<script src="Scripts/B-JUI/BJUI/plugins/uploadify/scripts/jquery.uploadify.min.js"></script>
<script src="Scripts/B-JUI/BJUI/plugins/download/jquery.fileDownload.js"></script>
  
    <script src="Scripts/DevicesManage/DevicesTotal.js"></script>
    <script src="Scripts/Language/language02-zh-cn.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=SAbCayX7PG5UMsqW6d1DZ9K0"></script> 
    <link href="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://api.map.baidu.com/library/TrafficControl/1.4/src/TrafficControl_min.js"></script>
    <link href="Scripts/Monitor/map.css" rel="stylesheet" />

    <script src="/Scripts/mg_public.js"></script> 
    <script src="/Scripts/Monitor/map/goome.maps.js"></script>
    <script src="/Scripts/Monitor/map/markerIcon.js"></script>
    
    <script src="/Scripts/Monitor/map/map.js"></script>  
    <script src="/Scripts/ZeroClipboard/ZeroClipboard.js"></script>
    <script src="/Scripts/Statistics/StatisticsPublic.js"></script>
    <script type="text/javascript">
    var postload = true;
    //onerror = handleErr 
    //function handleErr(msg,url,l)
    //{ 
    //    console.log("Handle the error here......");
    //    console.log(msg ,url, l);
    //    return true;
    //}
    $(function () { 
        BJUI.init({
            JSPATH: 'Scripts/B-JUI/BJUI/',         //[可选]框架路径
            PLUGINPATH: 'Scripts/B-JUI/BJUI/plugins/', //[可选]插件路径
            loginInfo: { url: 'login_timeout.html', title: '登录', width: 400, height: 200 }, // 会话超时后弹出登录对话框
            statusCode: { ok: 200, error: 300, timeout: 301 }, //[可选]
            ajaxTimeout: 50000, //[可选]全局Ajax请求超时时间(毫秒)
            pageInfo: { total: 'total', pageCurrent: 'pageCurrent', pageSize: 'pageSize', orderField: 'orderField', orderDirection: 'orderDirection' }, //[可选]分页参数
            alertMsg: { displayPosition: 'topcenter', displayMode: 'slide', alertTimeout: 3000 }, //[可选]信息提示的显示位置，显隐方式，及[info/correct]方式时自动关闭延时(毫秒)
            keys: { statusCode: 'statusCode', message: 'message' }, //[可选]
            ui: {
                windowWidth: 0,    //框架可视宽度，0=100%宽，> 600为则居中显示
                showSlidebar: true, //[可选]左侧导航栏锁定/隐藏
                clientPaging: true, //[可选]是否在客户端响应分页及排序参数
                overwriteHomeTab: true //[可选]当打开一个未定义id的navtab时，是否可以覆盖主navtab(我的主页)
            },
            debug: true,    // [可选]调试模式 [true|false，默认false]
            theme: 'orange' // 若有Cookie['bjui_theme'],优先选择Cookie['bjui_theme']。皮肤[五种皮肤:default, orange, purple, blue, red, green]
        })
         
        $('#bjui-accordionmenu')
            .collapse()
            .on('hidden.bs.collapse', function (e) { 
                $(this).find('> .panel > .panel-heading').each(function () {
                     var $heading = $(this), $a = $heading.find('> h4 > a')
                     if ($a.hasClass('collapsed')) $heading.removeClass('active')
                     $heading.addClass('active')
                })
            })
            .on('shown.bs.collapse', function (e) { 
                $(this).find('> .panel > .panel-heading').each(function () {
                    var $heading = $(this), $a = $heading.find('> h4 > a')
                    if (!$a.hasClass('collapsed')) $heading.addClass('active')
                    $heading.addClass('active')
                })
            })

        $(document).on('click', 'ul.menu-items > li > a', function (e) { 
            var $a = $(this), $li = $a.parent(), options = $a.data('options').toObj()
            var onClose = function () {
                $li.removeClass('active')
            }
            var onSwitch = function () {
                $('#bjui-accordionmenu').find('ul.menu-items > li').removeClass('switch')
                $li.addClass('switch')
            }

            $li.addClass('active')
            if (options) {
                options.url = $a.attr('href')
                options.onClose = onClose
                options.onSwitch = onSwitch
                if (!options.title)
                    options.title = $a.text()
                if (!options.target)
                    $a.navtab(options)
                else
                    $a.dialog(options)
            } 
            e.preventDefault()
        })

        //时钟
        var today = new Date(), time = today.getTime()
        $('#bjui-date').html(today.formatDate('yyyy/MM/dd'))
        setInterval(function () {
            today = new Date(today.setSeconds(today.getSeconds() + 1))
            $('#bjui-clock').html(today.formatDate('HH:mm:ss'))
        }, 1000);
      
        $("#aMonitor").on("click", function () {
            if (!postload) { 
                $("body").navtab({
                    id: '', url: '/Monitor/bdmap.html', title: '我的主页', onLoad: function () {  
                        mgoo.RefreshTime = 0;
                        mgoo.loadMap(); refreshTime();
                    }
                }); 
            } postload = false; 
        });
        $("#aStatistic").on("click", function (e) {
            $("body").navtab({
                id: '', url: '/Statistics/DeviceCount.html', title: '设备统计', onLoad: function () { 
                    $("#ztree-user-name_1_a").trigger("click"); 
                    $("#j_ztree_menus2").val($("#ztree-user-name_1_span").text());
                }
            });
        });
   
        $("#aDeviceManager").on("click", function (e) {
            $("body").navtab({
                id: '', url: '/DevicesManage/DevicesList.html', title: '设备信息', onLoad: function () { 
                    //默认展开树形菜单
                    var zTree_Menu = $.fn.zTree.getZTreeObj("mg-monitor-tree-devices");
                    var curNode = zTree_Menu.getNodes()[0];
                    zTree_Menu.selectNode(curNode);
                    zTree_Menu.expandNode(curNode);
                }
            });
        });

        $("#mg-monitor-users-list input").live("blur", function () {
          
            console.log("失去焦点....", $(this).val()  );
            var zTree = $.fn.zTree.getZTreeObj("mg-monitor-users-list");
            nodes = zTree.getSelectedNodes(),
            treeNode = nodes[0];
            AddGroups($(this).val(), treeNode.pId);
           // console.log(treeNode);
        });
        $("#mg-monitor-users-list input").live("focus", function () {
            //console.log ("获得焦点....", $(this).parent().parent().html());
        });

        $("#aDeviceManager").on("click", function () { 
            $("#mg-monitor-tree-devices_1_a").trigger("click"); 
        });
    })

    var mgoo;
    //菜单-事件
    function MainMenuClick(event, treeId, treeNode) {
         
        event.preventDefault() 
        if (treeId == "mg-monitor-users-list") {
            mgoo.CurrentzTreeUserID = treeNode.id;
            mgoo.clearOverlays({ clearAll: true }); 
            userliclick(treeNode.id,"ztreeclick");
            return;
        }
        if (treeId == "mg-monitor-tree-devices") {
            mgoo.CurrentzTreeUserID = treeNode.id; 
        }
        //if (treeNode.isParent) {
        //    var zTree = $.fn.zTree.getZTreeObj(treeId) 
        //    zTree.expandNode(treeNode, !treeNode.open, false, true, true)
        //    return
        //}
        if (treeNode.tabid == "devicecount") {
         
            $(event.target).navtab({
                url: treeNode.url, title: treeNode.name, fresh: treeNode.fresh, external: treeNode.external, onLoad: function () { 
                    $("#ztree-user-name_1_a").trigger("click");
                    $("#j_ztree_menus2").val($("#ztree-user-name_1_span").text());
                }
            })
            return;
        }
       
        if (treeNode.target && treeNode.target == 'dialog')
            $(event.target).dialog({   url: treeNode.url, title: treeNode.name })
        else
            $(event.target).navtab({   url: treeNode.url, title: treeNode.name, fresh: treeNode.fresh, external: treeNode.external })
    }
  
    $(window).on("bjui.resizeGrid", function () { 
        if ($("div.panel-heading").text().indexOf("定位监控") > 0) {
            var h = $("#bjui-sidebar").height() - 200;
            $("#bjui-collapse0").css("height", "200px").find(".panel-body").css("height", "200px");
            $("#bjui-collapse1").css({ "height": h + "px" });
            $("#div-device-list").css({ "height": (h - 140) + "px", " max-height": (h - 140) + "px" });
        } 
    });
   
    function mginit(_this)
    {
        $("#bjui-collapse0").css("height", "200px").find(".panel-body").css("height", "200px"); 
        var h = $("#bjui-sidebar").height() - 200;     
        $("#bjui-collapse1").css({ "display": "block", "height": h  }).removeClass("collapse").find(".panel-body").css({ "height": (h - 140)+"px", "overflow": "auto" }).attr("id", "div-device-list");
        $("#bjui-accordionmenu a[href=#bjui-collapse1]").attr("href", "");
        $("#bjui-accordionmenu a[href=#bjui-collapse0]").attr("href", "");
       // $($("#bjui-accordionmenu h4.panel-title")[0]).find("a b").after("<button class='btn btn-default btn-sm' style='margin-left:100px' id='btnAddGroup'>添加分组</button>");

        $("#btnAddGroup").bind("click", { isParent: false }, add);
        var select2 = '<select id="select2Device" class="selectpicker" data-live-search="true" data-width="100%">  </select>';
        $("#div-device-list").before(select2);
        $('#select2Device').selectpicker('refresh');
        $("#select2Device").selectpicker({
            // style: 'btn-info',
            size: 10,
            noneSelectedText: "搜索设备...",
            header: true,
        });
        $('#select2Device').on('change', function (e) { 
            $("#mg-monitor-device-list_" + $('#select2Device').selectpicker('val') + "_a").trigger("click"); 
        });

        var tab = [];
        tab.push('<ul class="nav nav-tabs" id="mgTabs" role="tablist"> <li class="active"><a data-toggle="tab" role="tab" href="#alldevice" onclick="tabClick(this)">全部</a></li>');
        tab.push('<li><a data-toggle="tab" role="tab" href="#online" onclick="tabClick(this)">在线</a></li>');
        tab.push('<li><a data-toggle="tab" role="tab" href="#offline" onclick="tabClick(this)">离线</a></li>');
        tab.push('<li><a data-toggle="tab" role="tab" href="#activation" onclick="tabClick(this)">未激活</a></li></ul>');

        $("#div-device-list").before(tab.join(''));
       
        //默认展开树形菜单
        var zTree_Menu = $.fn.zTree.getZTreeObj("mg-monitor-users-list");
        mgoo = new mgMap("allmap", "BAIDU"); 
        mgoo.loadMap();
        var curNode = zTree_Menu.getNodes()[0];
        zTree_Menu.selectNode(curNode);
        zTree_Menu.expandNode(curNode);
        mgoo.CurrentzTreeUserID = zTree_Menu.getSelectedNodes()[0].id; 
        refreshTime();
        userliclick(mgoo.CurrentzTreeUserID, "onload");

        $("#aDeviceInfo").on("click", function () {
            editDeviceInfo();
        });
    }
    function copya() {
        var clip = new ZeroClipboard(document.getElementById("Copy_A"), {
            moviePath: "/Scripts/ZeroClipboard/ZeroClipboard.swf"
        });
        clip.on('complete', function (client, args) {
            alert("复制成功，复制内容为：" + args.text);
        });
    }

    function doc_navtab_onLoad() {
        mginit();
    }
    function devices_navtab_onLoad() {
        var zTree_Menu = $.fn.zTree.getZTreeObj("mg-monitor-tree-devices");
        var curNode = zTree_Menu.getNodes()[0];
        zTree_Menu.selectNode(curNode);
        zTree_Menu.expandNode(curNode); 
    }
    function M_BeforeRemove(treeId, treeNode) {
        //alertMsg.error('未选中任何菜单！')
        console.log("删除...");
        return false;
    }
    //删除结束事件
    function M_NodeRemove(event, treeId, treeNode) {

        console.log("删除成功...");
        return false;
    }
    function M_beforeEditName(treeId, treeNode, newName, isCancel) {
        console.log(treeNode.name);
        return false;
    }
</script>
<!-- for doc begin -->
<link type="text/css" rel="stylesheet" href="/Scripts/B-JUI/js/syntaxhighlighter-2.1.382/styles/shCore.css"/>
<link type="text/css" rel="stylesheet" href="/Scripts/B-JUI/js/syntaxhighlighter-2.1.382/styles/shThemeEclipse.css"/>
<script type="text/javascript" src="/Scripts/B-JUI/js/syntaxhighlighter-2.1.382/scripts/brush.js"></script>
<script src="/Scripts/Monitor/monitorOper.js"></script>
<link href="/Scripts/B-JUI/doc/doc.css" rel="stylesheet">                                      
<script type="text/javascript">
    $(function () {
        SyntaxHighlighter.config.clipboardSwf = 'Scripts/B-JUI/js/syntaxhighlighter-2.1.382/scripts/clipboard.swf'
        $(document).on(BJUI.eventType.initUI, function (e) {
            SyntaxHighlighter.highlight();
        })
    })
    var newCount = 1;
    function add(e) {
        var zTree = $.fn.zTree.getZTreeObj("mg-monitor-users-list"),
        isParent = e.data.isParent,
        nodes = zTree.getSelectedNodes(),
        treeNode = nodes[0];
        if (treeNode) {
            treeNode = zTree.addNodes(treeNode, { id: (100 + newCount), pId: treeNode.id, isParent: isParent, name: "new node" + (newCount++) });
        } else {
            treeNode = zTree.addNodes(null, { id: (100 + newCount), pId: 0, isParent: isParent, name: "new node" + (newCount++) });
        }
        zTree.editName(treeNode[0]);
        if (treeNode) {
          
        } else {
           // alert("叶子节点被锁定，无法增加子节点");
        }
    };
  
</script>
<!-- for doc end -->
</head>
<body>
     <!--[if lte IE 7]>
        <div id="errorie"><div>您还在使用老掉牙的IE，正常使用系统前请升级您的浏览器到 IE8以上版本 <a target="_blank" href="http://windows.microsoft.com/zh-cn/internet-explorer/ie-8-worldwide-languages">点击升级</a>&nbsp;&nbsp;强烈建议您更改换浏览器：<a href="http://down.tech.sina.com.cn/content/40975.html" target="_blank">谷歌 Chrome</a></div></div>
    <![endif]-->
    <div id="bjui-window">
    <header id="bjui-header">
        <div class="bjui-navbar-header">
            <button type="button" class="bjui-navbar-toggle btn-default" data-toggle="collapse" data-target="#bjui-navbar-collapse">
                <i class="fa fa-bars"></i>
            </button>
          <%--<span style="font-size:30px;color:#ffffff">GPS 物联网平台</span>--%>  <a class="bjui-navbar-logo" href="Index.aspx"><img src="/Scripts/B-JUI/images/logo.png" height="35">  </a>
        </div>
        <nav id="bjui-navbar-collapse">
            <ul class="bjui-navbar-right">
                <li class="datetime"><div><span id="bjui-date"></span> <span id="bjui-clock"></span></div></li>
                <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" onclick="msgClick()">消息 <span class="badge" id="msgCount">0</span><span class="caret"></span></a>
       
                           <div class="dropdown-menu" role="menu" style="color:black; width:auto;left:-800px;">
                       <table class="table table-bordered" style="overflow-y:auto;width:auto;height:100%;" data-toggle="tablefixed" data-height="300">
                       <thead>
                           <%--<th width="80">设备名称</th><th width="145">定位时间</th>--%>
                           <tr> <th width="35">序号</th><th width="115">IMEI号</th><th width="120">所属用户</th><th width="60">报警类型</th><th width="145">报警时间</th>
                               <th width="60">设备型号</th><th width="60"> 状态</th> </tr>
                       </thead>
                        <tbody  id="msgTable"  style="background-color: #ffffff">
                        
                        </tbody>
                    </table>
                    <div class="resizable_f_r" tra="se"></div>
                     </div>
                 <%--   <ul class="dropdown-menu" role="menu">
                        <li><a href="changepwd.html" data-toggle="dialog" data-id="changepwd_page" data-mask="true" data-width="400" data-height="260">&nbsp;<span class="glyphicon glyphicon-lock"></span> 修改密码&nbsp;</a></li>
                        <li><a href="#">&nbsp;<span class="glyphicon glyphicon-user"></span> 我的资料</a></li>
                        <li class="divider"></li>
                        <li><a href="login.aspx?action=logout" class="red">&nbsp;<span class="glyphicon glyphicon-off"></span> 注销登陆</a></li>
                    </ul>--%>
                </li>
                <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><%= Utils.GetSessionUserName() %> <span class="caret"></span></a>
                    <ul class="dropdown-menu" role="menu">
                        <li><a href="/Monitor/changepwd.html" data-toggle="dialog" data-id="changepwd_page" data-width="400" data-height="260">&nbsp;<span class="glyphicon glyphicon-lock"></span> 修改密码&nbsp;</a></li>
                        <li><a href="/Monitor/userinfo.html" data-toggle="dialog" data-id="userinfo_page" data-on-load="userinfo_dialog_onload" data-before-close="userinfo_navtab_beforeClose" data-width="410" data-height="250">&nbsp;<span class="glyphicon glyphicon-user"></span> 我的资料</a></li>
                        <li class="divider"></li>
                        <li><a href="login.aspx?action=logout" class="red">&nbsp;<span class="glyphicon glyphicon-off"></span> 注销登陆</a></li>
                    </ul>
                </li>
              <%--  <li><a href="index.html" title="切换为列表导航(窄版)" style="background-color:#ff7b61;">列表导航栏(窄版)</a></li>--%>
                <li class="dropdown"><a href="#" class="dropdown-toggle theme blue" data-toggle="dropdown" title="切换皮肤"><i class="fa fa-tree"></i></a>
                    <ul class="dropdown-menu" role="menu" id="bjui-themes">
                        <li><a href="javascript:;" class="theme_default" data-toggle="theme" data-theme="default">&nbsp;<i class="fa fa-tree"></i> 黑白分明&nbsp;&nbsp;</a></li>
                        <li><a href="javascript:;" class="theme_orange" data-toggle="theme" data-theme="orange">&nbsp;<i class="fa fa-tree"></i> 橘子红了</a></li>
                        <li><a href="javascript:;" class="theme_purple" data-toggle="theme" data-theme="purple">&nbsp;<i class="fa fa-tree"></i> 紫罗兰</a></li>
                        <li class="active"><a href="javascript:;" class="theme_blue" data-toggle="theme" data-theme="blue">&nbsp;<i class="fa fa-tree"></i> 天空蓝</a></li>
                        <li><a href="javascript:;" class="theme_green" data-toggle="theme" data-theme="green">&nbsp;<i class="fa fa-tree"></i> 绿草如茵</a></li>
                    </ul>
                </li>
            </ul>
        </nav>
        <div id="bjui-hnav">
            <button type="button" class="btn-default bjui-hnav-more-left" title="导航菜单左移"><i class="fa fa-angle-double-left"></i></button>
            <div id="bjui-hnav-navbar-box">
                <ul id="bjui-hnav-navbar">         
                    <li class="active"><a href="javascript:;" data-toggle="slidebar" id="aMonitor"><i class="fa fa-map-marker"></i> 定位监控</a>
                        <div class="items hide" data-noinit="true"> 
                             <ul id="mg-monitor-users-list" class="ztree ztree_main" data-toggle="ztree" data-before-rename="M_beforeEditName"  data-edit-enable="true" data-options="
                                 { expandAll:false }"  data-before-remove="M_BeforeRemove" data-on-click="MainMenuClick" data-expand-all="false" data-faicon="list">                                
                                <%for (int i = 0; i < zTree.Count; i++) //data-show-remove-btn="true" data-show-rename-btn="true" 
                                  {   %>
                                       <li data-id="<%=zTree[i]["_id"] %>" data-pid="<%= zTree[i]["parent"] %>" data-tabid="form"><%= string.IsNullOrWhiteSpace(zTree[i]["username"]) ? zTree[i]["_id"]:zTree[i]["username"] %></li>                               
                                  <% } %>                            
                            </ul> 
                             <ul id="mg-monitor-device-list" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="list" data-tit="设备列表"> 
                                                        
                            </ul> 
                        </div>
                       
                    </li>
                    <li><a href="javascript:;" data-toggle="slidebar" id="aStatistic"><i class="fa fa-table"></i> 统计报表</a>
                        <div class="items hide" data-noinit="true">
                            <ul id="bjui-hnav-yxtj" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="table">
                                <li data-id="4" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">运行统计</li>
                                <li data-id="40" data-pid="4" data-url="/Statistics/DeviceCount.html" data-tabid="devicecount" data-faicon="plane" id="liDeviceCount">设备统计</li>
                           <%--     <li data-id="40" data-pid="4" data-url="dialog.html" data-tabid="dialog" data-faicon="plane">设备过期提示</li>
                                <li data-id="41" data-pid="4" data-url="alert.html" data-tabid="alert" data-faicon="info-circle">已删除设备</li>--%>
                                <li data-id="2" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">运行总览</li>
                                <li data-id="20" data-pid="2" data-url="/Statistics/Mileage.html" data-faicon="table" >里程统计</li>
                                <li data-id="21" data-pid="2" data-url="/Statistics/StopDetail.html" data-faicon="list-alt">停留详单</li>
                                <li data-id="22" data-pid="2" data-url="/Statistics/Echarts-line.html" data-faicon="indent">停留折线图</li>
                                <li data-id="6" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">报警统计</li><%-- 报警总览跟报警统计合并为 报警统计， 统计表下 加一列总计， 列出各报警类型在 查询的时间段内的次数--%>
                           <%--     <li data-id="20101" data-pid="6" data-faicon="folder-open-o" data-faicon-close="folder-o">报警消息</li>--%>
                                <li data-id="20120" data-pid="6" data-url="/Statistics/AlarmPreview.html" data-tabid="table" data-faicon="table">报警统计</li>
                                <li data-id="20121" data-pid="6" data-url="/Statistics/AlarmDetail.html" data-tabid="table-fixed" data-faicon="list-alt">报警详单</li>
                                <li data-id="20122" data-pid="6" data-url="/Statistics/GeoFencesView.html" data-tabid="table-edit" data-faicon="indent">电子围栏</li> 
                            </ul>
                        </div>
                    </li>
                    <li><a href="javascript:;" data-toggle="slidebar" id="aDeviceManager"><i class="fa fa-car"></i> 设备管理</a>
                        <div class="items hide" data-noinit="true">
                            <ul id="mg-monitor-tree-devices" class="ztree ztree_main" data-toggle="ztree" data-options="{expandAll:false}" data-on-click="MainMenuClick"  data-expand-all="false" data-faicon="list">
                                           <%for (int i = 0; i < zTree.Count; i++)
                                  {  
                                      %>
                                          <li data-id="<%=zTree[i]["_id"] %>"  data-pid="<%= zTree[i]["parent"] %>" data-url="/DevicesManage/DevicesList.html?id=<%=zTree[i]["_id"] %>" data-tabid="form"><%= string.IsNullOrWhiteSpace(zTree[i]["username"]) ? zTree[i]["_id"]:zTree[i]["username"] %></li>
                                <%   } %>
                            </ul>
                        </div>
                    </li>
                  <%--  <li><a href="javascript:;" data-toggle="slidebar"><i class="fa fa-line-chart"></i> 分析统计</a>
                        <div class="items hide" data-noinit="true">
                            <ul id="bjui-hnav-tree4" class="ztree ztree_main" data-toggle="ztree" data-on-click="MainMenuClick" data-expand-all="true" data-faicon="plane">
                                <li data-id="4" data-pid="0" data-faicon="folder-open-o" data-faicon-close="folder-o">设备统计</li>
                                <li data-id="40" data-pid="4" data-url="dialog.html" data-tabid="dialog" data-faicon="plane">设备过期提示</li>
                                <li data-id="41" data-pid="4" data-url="alert.html" data-tabid="alert" data-faicon="info-circle">已删除设备</li>
                            </ul>
                        </div>
                    </li> --%>
                   <%-- <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-cog"></i> 系统设置 <span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu">
                            <li><a href="#">角色权限</a></li>
                            <li><a href="#">用户列表</a></li>
                            <li class="divider"></li>
                            <li><a href="#">关于我们</a></li>
                            <li class="divider"></li>
                            <li><a href="#">友情链接</a></li>
                        </ul>
                    </li>--%>
                </ul>
            </div>
            <button type="button" class="btn-default bjui-hnav-more-right" title="导航菜单右移"><i class="fa fa-angle-double-right"></i></button>
        </div>
    </header>
    <div id="bjui-container">
        <div id="bjui-leftside">
            <div id="bjui-sidebar-s">
                <div class="collapse"></div>
            </div>
            <div id="bjui-sidebar" >
                <div class="toggleCollapse"><h2><i class="fa fa-bars"></i> 导航栏 <i class="fa fa-bars"></i></h2><a href="javascript:;" class="lock"><i class="fa fa-lock"></i></a></div>
                <div class="panel-group panel-main" data-toggle="accordion" id="bjui-accordionmenu" data-heightbox="#bjui-sidebar" data-offsety="26">
                </div>
            </div>
        </div>
        <div id="bjui-navtab" class="tabsPage">
            <div class="tabsPageHeader">
                <div class="tabsPageHeaderContent">
                    <ul class="navtab-tab nav nav-tabs">
                        <li data-url="Monitor/bdmap.html" data-on-load="doc_navtab_onLoad"><a href="javascript:;"><span><i class="fa fa-home"></i> #maintab#</span></a></li>
                    </ul>
                </div>
                <div class="tabsLeft"><i class="fa fa-angle-double-left"></i></div>
                <div class="tabsRight"><i class="fa fa-angle-double-right"></i></div>
                <div class="tabsMore"><i class="fa fa-angle-double-down"></i></div>
            </div>
            <ul class="tabsMoreList">
                <li><a href="javascript:;">#maintab#</a></li>
            </ul>
            <div class="navtab-panel tabsPageContent">
                <div class="navtabPage unitBox">
                    <div class="bjui-pageContent" style="background:#FFF;">
                        Loading...
                    </div>
                </div>
            </div>
        </div>
    </div>
        <input type="hidden" value="<%= Utils.GetSessionUserID() %>" id="txtSessionID" />
    <footer id="bjui-footer">Copyright &copy; 2015　<a href="http://www.mgoo.net/" target="_blank">美谷科技</a>　
        <!--  
        <script type="text/javascript">var cnzz_protocol = (("https:" == document.location.protocol) ? " https://" : " http://");document.write(unescape("%3Cspan id='cnzz_stat_icon_1252983288'%3E%3C/span%3E%3Cscript src='" + cnzz_protocol + "s23.cnzz.com/stat.php%3Fid%3D1252983288%26show%3Dpic' type='text/javascript'%3E%3C/script%3E"));</script>
        -->
    </footer>
    </div>
</body>
</html>
