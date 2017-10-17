<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeviceShift.aspx.cs" Inherits="MgooGps.DeviceShift" %>

 <%@ Import Namespace="MgooGps.com" %>
<!DOCTYPE html>
 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="Xenon Boostrap Admin Panel" />
	<meta name="author" content="" />
	
	<title> </title>
    <script src="js/Language/language02-<%= Utils.language %>.js"></script> 
	<link rel="stylesheet" href="assets/css/fonts/linecons/css/linecons.css">
	<link rel="stylesheet" href="assets/css/fonts/fontawesome/css/font-awesome.min.css">
	<link rel="stylesheet" href="assets/css/bootstrap.css">
	<link rel="stylesheet" href="assets/css/xenon-core.css">
	<link rel="stylesheet" href="assets/css/xenon-forms.css">
	<link rel="stylesheet" href="assets/css/xenon-components.css">
	<link rel="stylesheet" href="assets/css/xenon-skins.css">
	<link rel="stylesheet" href="assets/css/custom.css">

	<script src="assets/js/jquery-1.11.1.min.js"></script>
 
 
    <script type="text/javascript">
        var zTree_Menu;

        var setting = {
            view: {
                showLine: false,
                showIcon: true,
                selectedMulti: false,
                dblClickExpand: false,
                addDiyDom: addDiyDom
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
                beforeClick: beforeClick
            }
        };

        var zNodes = new Array();

        $.ajax({
            url: "AjaxService/AjaxService.ashx?action=getTree",
            type: 'POST',
            dataType: 'json',
            data: {},
            success: function (dataList) {
                var i = 0;
                $(dataList[""]).each(function (k, v) {
                    zNodes[i] = { id: v["UserID"], pId: v["ParentID"], name: v["UserName"] }; i++;
                });
                var treeObj = $("#treeDemo");
                $.fn.zTree.init(treeObj, setting, zNodes);
                zTree_Menu = $.fn.zTree.getZTreeObj("treeDemo");

                curMenu = zTree_Menu.getNodes()[0];

                if (curMenu != undefined && zTree_Menu.getNodes().length > 0) {
                    if (zTree_Menu.getNodes()[0].children != undefined && zTree_Menu.getNodes()[0].children[0] != null) {
                        curMenu = zTree_Menu.getNodes()[0].children[0];   //默认展开到第几级  在后面加.children[0] 
                    }
                }
                zTree_Menu.selectNode(curMenu);
                beforeClick(zTree_Menu.getSelectedNodes()[0].id, zTree_Menu.getSelectedNodes()[0]);
                // zTree_Menu.selectNode(zTree_Menu.getNodes()[0]);

                treeObj.addClass("showIcon");
            }
        });

        function filter(treeId, parentNode, childNodes) {
            if (!childNodes) return null;
            for (var i = 0, l = childNodes.length; i < l; i++) {
                childNodes[i].name = childNodes[i].name.replace(/\.n/g, '.');
            }
            return childNodes;
        }
        function addDiyDom(treeId, treeNode) {
            var spaceWidth = 5;
            var switchObj = $("#" + treeNode.tId + "_switch"),
            icoObj = $("#" + treeNode.tId + "_ico");
            switchObj.remove();
            icoObj.before(switchObj);
            if (treeNode.level > 1) {
                var spaceStr = "<span style='display: inline-block;width:" + (spaceWidth * treeNode.level) + "px'></span>";
                switchObj.before(spaceStr);
            }
        }
        var isLoadDevicesList = true;
        function beforeClick(treeId, treeNode) {
            $("#iframepage").attr("src", "AllDevices.aspx?u=" + treeNode.id);
            return true;
        }
</script>
    <script>
        var _hmt = _hmt || [];
        (function () {
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?63805c8d49dd83eac3a21c31f34f9352";
            var s = document.getElementsByTagName("script")[0];
            s.parentNode.insertBefore(hm, s);
        })();
</script>

</head>
<body>
		<div class="row">
				<div class="col-sm-12">
					
					<div class="panel panel-default">
						<div class="panel-heading">
							<h3 class="panel-title">客户信息</h3>
							<div class="panel-options">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
						</div>
						<div class="panel-body">
							
							<form role="form" class="form-horizontal">
								
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-1">目标客户</label>
									
									<div class="col-sm-10">
										<input type="text" class="form-control" id="field-1" placeholder="Placeholder">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-2">Password</label>
									
									<div class="col-sm-10">
										<input type="password" class="form-control" id="field-2" placeholder="Placeholder (Password)">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Disabled input</label>
									
									<div class="col-sm-10">
										<input type="text" class="form-control" placeholder="Placeholder" disabled>
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-3">Email field</label>
									
									<div class="col-sm-10">
										<input type="email" class="form-control" id="field-3" placeholder="Enter your email&hellip;">
										<p class="help-block">Example block-level help text here. Emails inputs are validated on native HTML5 forms.</p>
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-4">File Field</label>
									
									<div class="col-sm-10">
										<input type="file" class="form-control" id="field-4">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-5">Text area</label>
									
									<div class="col-sm-10">
										<textarea class="form-control" cols="5" id="field-5"></textarea>
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-5">Auto grow</label>
									
									<div class="col-sm-10">
										<textarea class="form-control autogrow" cols="5" id="Textarea1" placeholder="I will grow as you enter new lines."></textarea>
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group has-error">
									<label class="col-sm-2 control-label" for="field-6">Error field</label>
									
									<div class="col-sm-10">
										<input type="text" class="form-control" id="field-6" placeholder="Hello I am an error">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group has-warning">
									<label class="col-sm-2 control-label" for="field-7">Warning field</label>
									
									<div class="col-sm-10">
										<input type="text" class="form-control" id="field-7" placeholder="Hello I am a warning">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group has-success">
									<label class="col-sm-2 control-label" for="field-8">Success field</label>
									
									<div class="col-sm-10">
										<input type="text" class="form-control" id="field-8" placeholder="Hello I am a success message">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group has-info">
									<label class="col-sm-2 control-label" for="field-9">Info field</label>
									
									<div class="col-sm-10">
										<input type="text" class="form-control" id="field-9" placeholder="Hello I am an info message">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-11">Input size</label>
									
									<div class="col-sm-10">
										<input type="text" class="form-control input-sm" id="field-13" placeholder=".input-sm">
										<br />
										<input type="text" class="form-control" id="field-14" placeholder="Normal input">
										<br />
										<input type="text" class="form-control input-lg" id="field-15" placeholder=".input-lg">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-11">Input text position</label>
									
									<div class="col-sm-10">
										<input type="text" class="form-control text-center" id="field-11" placeholder="Placeholder">
										<br />
										<input type="text" class="form-control text-right" id="field-12" placeholder="Placeholder">
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Select list</label>
									
									<div class="col-sm-10">
										<select class="form-control">
											<option>Option 1</option>
											<option>Option 2</option>
											<option>Option 3</option>
											<option>Option 4</option>
											<option>Option 5</option>
										</select>
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Checkboxes</label>
									
									<div class="col-sm-10">
										<div class="checkbox">
											<label>
												<input type="checkbox">
												Default checkbox
											</label>
										</div>
										<div class="checkbox">
											<label>
												<input type="checkbox">
												Another one
											</label>
										</div>
										<div class="checkbox">
											<label>
												<input type="checkbox" disabled>
												Disabled option
											</label>
										</div>
										<div class="checkbox">
											<label>
												<input type="checkbox" checked disabled>
												Checked and disabled option
											</label>
										</div>
										
										<br />
										<strong>Inline checkboxes</strong>
										
										<p>
											<label class="checkbox-inline">
												<input type="checkbox">
												Inline checkbox 1
											</label>
											<label class="checkbox-inline">
												<input type="checkbox">
												Inline checkbox 2
											</label>
											<label class="checkbox-inline">
												<input type="checkbox">
												Inline checkbox 3
											</label>
										</p>
										
									</div>
								</div>
								
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Radio buttons</label>
									
									<div class="col-sm-10">
										<div class="radio">
											<label>
												<input type="radio" name="radio-1" checked>
												Default radio
											</label>
										</div>
										<div class="radio">
											<label>
												<input type="radio" name="radio-1">
												Another one
											</label>
										</div>
										<div class="radio">
											<label>
												<input type="radio" disabled>
												Disabled option
											</label>
										</div>
										<div class="radio">
											<label>
												<input type="radio" checked disabled>
												Checked and disabled option
											</label>
										</div>
										
										<br />
										<strong>Inline radio buttons</strong>
										
										<p>
											<label class="radio-inline">
												<input type="radio" name="radio-2" checked>
												Inline radio button 1
											</label>
											<label class="radio-inline">
												<input type="radio" name="radio-2">
												Inline radio button 2
											</label>
											<label class="radio-inline">
												<input type="radio" name="radio-2">
												Inline radio button 3
											</label>
										</p>
										
									</div>
								</div>
								
								
							</form>
							
						</div>
					</div>
					
				</div>
			</div>
			
			<!-- Bottom Scripts -->
	<script src="assets/js/bootstrap.min.js"></script>
	<script src="assets/js/TweenMax.min.js"></script>
	<script src="assets/js/resizeable.js"></script>
	<script src="assets/js/joinable.js"></script>
	<script src="assets/js/xenon-api.js"></script>
	<script src="assets/js/xenon-toggles.js"></script>


	<!-- JavaScripts initializations and stuff -->
	<script src="assets/js/xenon-custom.js"></script>
</body>
</html>
