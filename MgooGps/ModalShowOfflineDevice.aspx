<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModalShowOfflineDevice.aspx.cs" Inherits="MgooGps.ModalShowOfflineDevice" %>
 <%@ Import Namespace="MgooGps.com" %>
<!DOCTYPE html> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title> 
      <script src="/js/Language/language02-zh-cn.js"></script>
</head>
<body> 
    <!--  统计页面列表 -->
        <div class="modal-dialog" style="width: 70%; height: 600px;min-width:1140px;" id="modalDialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title"style="font-weight:bold;color:#808080;">设备列表</h4>
                </div>
                <div class="modal-body">
                     <div class="row">
						    <div class="col-md-6"> 
							    <div class="form-group">
								    <label for="field-1" class="control-label"style="font-size:12px;">设备型号:</label> 
								  	<select class="form-control" id="deviceModel" multiple style="width:180px;font-size:12px;">
										<option></option>
										<optgroup label="设备型号">  
										</optgroup>
									</select> 
                                     <button class="btn btn-gray" id="btnSearchOffLine" style="margin-top:10px;height:31px;background-color:#5CB85C;font-weight:bold;color:white;font-size:12px;">查询</button>
							    </div>	 
						    </div>  
					    </div>
                    <table cellspacing="0" style="color:black;font-size:12px;" class="table table-small-font table-bordered table-striped" id="OffLineDeviceTable">
                        <thead>
                            <tr>
                                <th data-priority="1">序号</th>
                                <th >设备名字</th>
                                <th data-priority="1">IMEI号</th>
                                <th data-priority="3">型号</th> 
                                <th data-priority="3">设备SIM卡</th> 
                                <th data-priority="3">到期时间</th>
                                <th data-priority="3">与平台通信时间</th>
                                <th data-priority="3">离线时间</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th data-priority="1">序号</th>
                                <th>设备名字</th>
                                <th data-priority="1">IMEI号</th>
                                <th data-priority="3">设备型号</th> 
                                <th data-priority="3">设备SIM卡</th>
                                <th data-priority="3">到期时间</th>
                                <th data-priority="3">与平台通信时间</th>
                                <th data-priority="3">离线时间</th>
                            </tr>
                        </tfoot>
                        <tbody>
                              
                        </tbody>
                    </table>
                    <script type="text/ecmascript">
                        jQuery(document).ready(function ($) {
                            setTimeout(function () {
                                $(".checkbox-row input").addClass('cbr');
                                $(".dropdown-toggle").hide();
                            }, 0);
                        });
                    </script> 
                </div> 
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal"style="height:31px;background-color:#808080;font-weight:bold;color:white;font-size:12px;">关闭</button>
                </div>
            </div>
        </div> 
     

         
	 	   <!-- Modal 6 (Confirm)-->
  
		<div class="modal-dialog" id="DivDeviceInfo"  >
			<div class="modal-content">
			  <form role="form" id="form1" method="post" class="validate" onsubmit="return false">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">设备信息 </h4>
				</div> 
				<div class="modal-body"> 
					    <div class="row">
						    <div class="col-md-6"> 
							    <div class="form-group">  
                                    <label for="field-1" class="control-label">设备号(IMEI)</label> 
								    <input type="text" class="form-control" id="txtDeviceIMEI" placeholder="设备号(IMEI)"  disabled />
							    </div> 
						    </div> 
						    <div class="col-md-6"> 
							    <div class="form-group"> 
                                    <label for="field-1" class="control-label">到期时间</label> 
								    <input type="text" class="form-control" id="txtDeviceHireExpireDate" placeholder="到期时间" disabled />
							    </div>	
						
						    </div>
					    </div>
                    <div class="row">
						    <div class="col-md-6">
							
							    <div class="form-group">
								    <label for="field-1" class="control-label">型号</label> 
								    <input type="text" class="form-control" id="txtDeviceModel" placeholder="型号" disabled />
							    </div>	
							
						    </div>
						
						    <div class="col-md-6">
							
							    <div class="form-group">
								    <label for="field-2" class="control-label">出厂时间</label> 
								    <input type="text" class="form-control" id="txtDeviceStartDate" placeholder="出厂时间" disabled/>
							    </div>	
						
						    </div>
					    </div>
                     <div class="row">
						    <div class="col-md-6">
							
							    <div class="form-group">

								    <label for="field-1" class="control-label">设备名称</label> 
								    <input type="text" class="form-control" id="txtDeviceName" placeholder="设备名称"/>
							    </div>	
							
						    </div>
						
						    <div class="col-md-6">
							
							    <div class="form-group">
                                     <label class="cbr-inline" style ="margin-top:14px;">
                                        过滤LBS
												<input type="checkbox" id="checkDeviceLBS" class="cbr"/> 
											</label> 
							    </div>	
						
						    </div>
					    </div>
                      <div class="row">
						    <div class="col-md-6"> 
							    <div class="form-group">
								    <label for="field-1" class="control-label">设备电话</label> 
								    <input type="text" class="form-control" id="txtDevicePhoneNum" placeholder="设备电话"/>
							    </div>	 
						    </div> 
						    <div class="col-md-6"> 
							    <div class="form-group">
								    <label for="field-2" class="control-label">超速(公里/每小时)</label> 
								    <input type="text" class="form-control" id="txtSpeedLimit" placeholder="超速(公里/每小时)"/>
							    </div>	
						
						    </div>
					    </div>
                     <div class="row">
						    <div class="col-md-6"> 
							    <div class="form-group">
								    <label for="field-1" class="control-label">车牌号</label> 
								    <input type="text" class="form-control" id="txtDeviceCarNum" placeholder="车牌号"/>
							    </div>	
							
						    </div>
						
						    <div class="col-md-6"> 
							    <div class="form-group">
								    <label for="field-2" class="control-label">联系电话</label> 
								    <input type="text" class="form-control" id="txtDeviceCallPhone" placeholder="联系电话"/>
							    </div>	
						    </div>
					    </div>
                     <div class="row">
						    <div class="col-md-6"> 
							    <div class="form-group">
								    <label for="field-1" class="control-label">联系人</label> 
								    <input type="text" class="form-control" id="txtDeviceCarUserName" placeholder="联系人"/>
							    </div>	
							
						    </div>
						
						    <div class="col-md-6"> 
							    <div class="form-group">
								    <label for="field-2" class="control-label">百公里油耗系数</label> 
								    <input type="text" class="form-control" id="txtDevice100KM" placeholder="百公里油耗系数"/>
							    </div>	
						
						    </div>
					    </div>
                      <div class="row">
						    <div class="col-md-12"> 
							    <div class="form-group">
								  <p>
											<label class="radio-inline">
												<input type="radio" name="radioImg" checked value="1" />  <img src="icons/carIcon/27.gif" />
											</label>
											<label class="radio-inline">
												<input type="radio" name="radioImg" value="2"/>  <img src="icons/carIcon/2.png" />
											</label>
											<label class="radio-inline">
												<input type="radio" name="radioImg" value="21"/>   <img src="icons/carIcon/21.png" />
											</label>
                                           <label class="radio-inline">
												<input type="radio" name="radioImg" value="22"/>   <img src="icons/carIcon/22.png" />
											</label>
                                          <label class="radio-inline">
												<input type="radio" name="radioImg" value="24"/>   <img src="icons/carIcon/24.png" />
											</label>
                                             <label class="radio-inline">
												<input type="radio" name="radioImg" value="25"/>   <img src="icons/carIcon/25.png" />
											</label>
                                           <label class="radio-inline">
												<input type="radio" name="radioImg" value="23"/>   <img src="icons/carIcon/23.png" />
											</label>
                                      <label class="radio-inline">
												<input type="radio" name="radioImg" value="26"/>   <img src="icons/carIcon/26.gif" />
											</label>
                                       <label class="radio-inline">
												<input type="radio" name="radioImg" value="30"/>   <img src="icons/carIcon/30.png" />
											</label>
                                            <label class="radio-inline">
												<input type="radio" name="radioImg" value="31"/>   <img src="icons/carIcon/31.png" />
											</label>
										</p> 
							    </div>	 
						    </div>
					    </div>  
                        <div class="row">
						     <div class="col-md-6"> 
                                  <div class="form-group"> 	<label class="col-sm-4 control-label" for="field-4">图片</label><img src="" id="imgDeviceCarImg" imgName="" style="width:100px;height:100px;" /></div> 
						    </div>
                              <div class="col-md-6">  
							        <div class="form-group"> 
									<div class="col-sm-5">
										<input type="file" class="form-control" id="txtDeviceFileImg" name="txtDeviceFileImg"/>
									</div>
								</div>
						    </div>
					    </div> 
                

					    <div class="row">
						    <div class="col-md-12"> 
							    <div class="form-group">
								    <label for="field-3" class="control-label">备注</label>
								    <textarea class="form-control" cols="5" id="txtDeviceDescription"></textarea> 
							    </div> 
						    </div>
					    </div> 
                   
				</div>
				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnDeviceInfoClose">关闭</button>
					<button type="submit" class="btn btn-success" id="btnDeviceInfoSave">保存并关闭</button>
				</div>
             </form>
			</div>
		</div>
	 

     <!-- Modal 4 发送指令确定密码-->
	<div class="modal fade">
		<div class="modal-dialog" id="DivCommandQuene"  style="width:330px;">
			<div class="modal-content"  > 
				<div class="modal-header" > 
					<h4 class="modal-title" style="line-height:25px; ">提示</h4>
				</div> 
				<div class="modal-body"> 
					<div class="row">
						    <div class="col-md-12"> 
							    <div class="form-group">  
                                     <label for="field-1" class="control-label">设备名称：</label> 
								    <label id="labDeviceName"></label>
							    </div>
						    </div> 
				   </div>
                    <div class="row" name="divSetInterval" style="display:none">
						    <div class="col-md-12"> 
							    <div class="form-group">  
                                     <label for="field-1" class="control-label" style="float:left;">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 类型：</label> 
								   <select id="selectSCPL">
                                       <option value="S7122">运动上传频率</option>
                                       <option value="S7123">静止上传频率</option>
								   </select>
							    </div>	 
						    </div> 
			        </div>
                    <div class="row">
						    <div class="col-md-12"> 
							    <div class="form-group">  
                                     <label for="field-1" class="control-label" style="float:left;">登录密码：</label> 
								     <input type="password" class="form-control" id="txtLoginPassword" style="float:left;width:200px; margin-top:-10px;" />
                                    <div style="display:none" name="divSetInterval">
                                       <input type="text"  id="txtSetInterval" maxlength="4" autocomplete="off"  style="width:50px;" /> <label >(单位秒,最小5秒)</label>
                                    </div>
							    </div>	 
						    </div> 
			        </div>
                    <div class="row" style="height:25px;">
						    <div class="col-md-12"> 
							    <div class="form-group">  
                                <label for="field-1" class="control-label" id="labMessage" > 发指令前请再次确认登录账号 <font style="font-weight:bold"><%= Utils.GetSession("UserInfo").UserName  %> </font>密码  </label> 
							    </div>	 
						    </div> 
				  </div>
				</div> 
				<div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal" id="btnCommandClose">关闭</button>
					<button type="button" class="btn btn-info" id="btnCommandSubmit">确定</button>
				</div>
			</div>
		</div>
	</div>

      <!-- Modal 4 设备转移和更改到期时间窗体-->
    <div class="mode fade">
        <div class="modal-dialog" id="DivSales" >
        
             <div class="modal-content"  > 
                <div class="modal-header" ><%--height:25px;style=" margin-top:-20px;"--%>
					<h4 class="modal-title" style="line-height:25px; ">设备转移</h4>
			   </div> 
               <div class="modal-body"> 
							<form role="form" class="form-horizontal"> 
                                <div class="form-group-separator" id="field-11">客户信息</div> 
								<div class="form-group-separator"></div>
								<div class="form-group">
									<label class="col-sm-2 control-label" for="field-1" style="left:20px;" id="lableTitle">目标客户</label> 
									<div class="col-sm-6" id="divShift">
									<%--  <input type="text" id="divChangeUserBySale"/>	 
                                          <br />--%>
									</div>
                                    <div class="col-sm-6" style="display:none;" id="divExpire">
                                         <input type="text" style="width:60px;" id="txtExpireYears" />
                                        	<label class="control-label" for="field-1">天</label> 
                                         <div class="btn-group" style="top:4px;">
									        <button type="button" class="btn btn-info btn-xs" name="btnYears">一年</button>
									        <button type="button" class="btn btn-info btn-xs" name="btnYears">二年</button>
									        <button type="button" class="btn btn-info btn-xs" name="btnYears">终身</button>
								         </div> 
                                          <br />
                                    </div>
								</div> 
								<div class="form-group-separator">设备信息</div> 
								<div class="form-group-separator"></div>
								
								<div class="form-group">
									 <table class="table table-bordered table-striped table-condensed table-hover" id="ShiftTable">
										<thead>
											<tr>
												<th>设备名</th>
												<th>IMEI号</th>
												<th>所属用户</th>
                                                <th>操作</th>
											</tr>
										</thead> 
										<tbody>
											 
										</tbody>
									</table>

								</div> 
							</form> 
               </div>  
                 <div class="modal-footer">
                     <button type="button" class="btn btn-white" data-dismiss="modal" id="btnDeviceShiftOrExpireClose">关闭</button>
					 <button type="button" class="btn btn-info"   id="btnDeviceShiftOrExpireSave">确定</button>
                 </div>
           </div>  
      <div id="menuContent" class="menuContent" style="display:none; position: absolute;z-index:50010; left:785.6666870117188px;top:180px; background-color: #F0F6E4; ">
	   <ul id="UserTree" class="ztree" ></ul>
    </div>
       
            </div>
	 </div> 
    

    <!-- Modal 1 (提示框)-->
	<div class="modal fade">
		<div class="modal-dialog" id="BasicModal">
			<div class="modal-content"> 
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">提示</h4>
				</div> 
				<div class="modal-body">
					 提示内容！！！
				</div> 
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal">取消</button>
					<button type="button" class="btn btn-info" data-dismiss="modal" id="btnConfirm">确定</button>
				</div>
			</div>
		</div>
	</div>


       <!-- 用户信息 --> 
    <div class="modal fade" id="modal-6" data-backdrop="static">
		<div class="modal-dialog" id="DivDeviceUpdateUserInfo">
			<div class="modal-content">
			  <form role="form" id="form2" method="post" class="validate" onsubmit="return false;">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">用户信息</h4>
				</div>
				
				<div class="modal-body">
			
					    <div class="row">
						    <div class="col-md-6">
							
							    <div class="form-group">
								    <label for="field-1" class="control-label">客户名称</label> 
								    <input type="text" class="form-control" id="txtDeviceUserName" placeholder="name" />
							    </div>	 
						    </div> 
						    <div class="col-md-6"> 
							    <div class="form-group">
								    <label for="field-2" class="control-label">登录账号</label> 
								    <input type="text" class="form-control" id="txtDeviceLoginName" placeholder="LoginName"disabled/>
							    </div>	 
						    </div>
					    </div>
				      <div class="row">
						    <div class="col-md-4"> 
							    <div class="form-group">
								    <label for="field-4" class="control-label">联系人</label> 
								    <input type="text" class="form-control" id="txtDeviceContacts" name="name" placeholder="Contacts" data-validate="required" data-message-required="联系人不能为空." />
							    </div>	 
						    </div>
						
						    <div class="col-md-4"> 
							    <div class="form-group">
								    <label for="field-5" class="control-label">电话</label> 
								    <input type="text" class="form-control" id="txtDevicePhone" name="min_field" data-validate="required,number,minlength[6]"   placeholder="Phone"/>
							    </div>	 
						    </div> 
						    <div class="col-md-4"> 
							    <div class="form-group">
                                     <label for="field-5" class="control-label">类型</label> 
								      <p>
											<label class="radio-inline">
												<input type="radio" name="UserTypeDevice" value="1"/>
												 用户
											</label>
											<label class="radio-inline">
												<input type="radio" name="UserTypeDevice" value="2"/>
												经销商
											</label>
                                     </p>
							    </div>	 
						    </div>
					    </div>
					    <div class="row">
						    <div class="col-md-12">
							    <div class="form-group">
								    <label for="field-3" class="control-label">地址</label>
								    <input type="text" class="form-control" id="txtDeviceAddress" placeholder="Address"/>
							    </div>	 
						    </div>
					    </div>  
				</div> 
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnDeviceUserInfoClose">关闭</button>
					<button type="submit" class="btn btn-success" id="btnDeviceSaveUserInfo">保存并关闭</button>
				</div>
          </form>
			</div>
		</div>
	</div>


        <!-- 搜素设备或者搜索客户 -->
	<div class="modal fade" id="modal-5">
		<div class="modal-dialog" id="SearchDeviceOrUser">
			<div class="modal-content"> 
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">搜索设备</h4>
				</div> 
				<div class="modal-body"> 
				 
				</div> 
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal">Close</button>
					<button type="button" class="btn btn-info">Save changes</button>
				</div>
			</div>
		</div>
	</div>

     <%--  <div class="row" >
						    <div class="col-md-12" style="text-align:center;border:1px solid red"> 
							    <div class="form-group">  
                                     <label for="field-1" class="control-label" style="float:left;">&nbsp&nbsp 旧密码：</label> 
								     <input type="password" class="form-control" id="txtOldPassword" style="float:left;width:200px;  " /> 
							    </div>	 
						    </div> 
			        </div>
                     <div class="row">
						    <div class="col-md-12"> 
							    <div class="form-group">  
                                     <label for="field-1" class="control-label" style="float:left;">&nbsp&nbsp 新密码：</label> 
								     <input type="password" class="form-control" id="txtNewPassword" maxlength="15" style="float:left;width:200px; " /> 
							    </div>	 
						    </div> 
			        </div>
                     <div class="row">
						    <div class="col-md-12"> 
							    <div class="form-group">  
                                     <label for="field-1" class="control-label" style="float:left;">确认新密码：</label> 
								     <input type="password" class="form-control" id="txtConfirmNewPassword" maxlength="15" style="float:left;width:200px;" /> 
							    </div>	 
						    </div> 
			        </div>--%>
          <!-- 修改密码 -->
	<div class="modal fade" >
		<div class="modal-dialog" id="UpdatePassword" >
			<div class="modal-content"> 
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">修改密码</h4>
				</div> 
				<div class="modal-body"> 
				<table style="width:100%;">
                    <tr>
                        <td><label for="field-1" class="control-label" style="float:left;">&nbsp&nbsp 旧密码：</label> </td>
                         <td><input type="password" class="form-control" id="txtOldPassword" style="float:left;width:200px;  " /> </td>
                    </tr> 
                    <tr>
                        <td>   <label for="field-1" class="control-label" style="float:left;">&nbsp&nbsp 新密码：</label> </td>
                         <td>     <input type="password" class="form-control" id="txtNewPassword" maxlength="15" style="float:left;width:200px; " /> </td>
                    </tr>
                     <tr>
                        <td>  <label for="field-1" class="control-label" style="float:left;">确认新密码：</label> </td>
                         <td>   <input type="password" class="form-control" id="txtConfirmNewPassword" maxlength="15" style="float:left;width:200px;" /> </td>
                    </tr>
				</table>
				</div> 
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal">关闭</button>
					<button type="button" class="btn btn-info">保存并关闭</button>
				</div>
			</div>
		</div>
	</div>


    <!-- 查询设备 -->
	<div class="modal fade">
            <div class="modal-dialog" style="width: 70%; min-width:1000px;" id="DivSearchDevice">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title"style="font-weight:bold;color:#808080;">搜索设备或者搜索用户</h4>
                </div>
                <div class="modal-body">
                     <div class="row">
						    <div class="col-md-12"> 
							    <div class="form-group">
								    <label for="field-1" class="control-label"style="font-size:12px;float:left; margin-top:5px;">搜索内容:</label> 
								    <input type="text" class="form-control" style="width:250px;float:left;" id="txtSearchDeviceName" placeholder ="IMEI/客户吗/账号/设备手机号/电话"/>
                                    <button class="btn btn-gray" id="btnSearchDevices" style="margin-top:0px;height:31px;background-color:#5CB85C;font-weight:bold;color:white;font-size:12px;float:left;">查询</button>
							    </div>	 
						    </div>  
					 </div>
                    <div class="row" style="max-height:400px; overflow:auto;">
                    <table cellspacing="0" style="color:black;font-size:12px;" class="table table-small-font table-bordered table-striped" id="SearchDevicesTable">
                        <thead>
                            <tr>
                                <th style="width:50px;">序号</th>
                                <th style="width:160px;">设备名字</th>
                                <th style="width:135px;">IMEI号</th>
                                <th style="width:200px;">所属用户</th> 
                                <th style="width:100px;">手机卡号</th> 
                                <th>联系人</th>
                                <th style="width:145px;">创建时间</th>
                                <th style="width:150px;">到期时间</th> 
                                <th style="width:185px;">操作</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th>序号</th>
                                <th>设备名字</th>
                                <th>IMEI号</th>
                                <th>所属用户</th> 
                                <th>手机卡号</th> 
                                <th>联系人</th>
                                <th>创建时间</th>
                                <th>到期时间</th> 
                                <th>操作</th>
                            </tr>
                        </tfoot>
                        <tbody>
                              
                        </tbody>
                    </table>
                     </div>
                      <div class="row" style="max-height:300px; overflow:auto;">
                      <table cellspacing="0" style="color:black;font-size:12px;" class="table table-small-font table-bordered table-striped" id="SearchUsersTable">
                        <thead>
                            <tr>
                                <th style="width:8%;">序号</th>
                                <th style="width:160px;">客户名称</th>
                                <th style="width:110px;">登录名</th>
                                <th style="width:170px;">联系人</th> 
                                <th style="width:100px;">电话</th>  
                                <th style="width:85px;">操作</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                               <th>序号</th>
                                <th>客户名称</th>
                                <th>登录名</th>
                                <th>联系人</th> 
                                <th>电话</th>  
                                <th>操作</th>
                            </tr>
                        </tfoot>
                        <tbody>
                              
                        </tbody>
                    </table>
                       </div>
                    <script type="text/ecmascript">
                        jQuery(document).ready(function ($) {
                            setTimeout(function () {
                                $(".checkbox-row input").addClass('cbr');
                                $(".dropdown-toggle").hide();
                            }, 0);
                        });
                    </script> 
                </div> 
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal" >关闭</button>
                </div>
            </div>
        </div> 
     
    </div>

      <!-- 查询用户 -->
	<div class="modal fade">
            <div class="modal-dialog" style="width: 40%; height: 600px;min-width:1000px;" id="DivSearchUsers">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title"style="font-weight:bold;color:#808080;">搜索用户</h4>
                </div>
                <div class="modal-body">
                     <div class="row">
						    <div class="col-md-12"> 
							    <div class="form-group">
								    <label for="field-1" class="control-label"style="font-size:12px;float:left; margin-top:5px;">搜索内容:</label> 
								    <input type="text" class="form-control" style="width:250px;float:left;" id="txtSearchUsers"/>
                                    <button class="btn btn-gray" id="btnSearchUsers" style="margin-top:0px;height:31px;background-color:#5CB85C;font-weight:bold;color:white;font-size:12px;float:left;">查询</button>
							    </div>	 
						    </div>  
					    </div>
                    <table cellspacing="0" style="color:black;font-size:12px;" class="table table-small-font table-bordered table-striped">
                        <thead>
                            <tr>
                                <th style="width:50px;">序号</th>
                                <th style="width:160px;">客户名称</th>
                                <th style="width:110px;">登录名</th>
                                <th style="width:170px;">联系人</th> 
                                <th style="width:100px;">电话</th>  
                                <th style="width:85px;">操作</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                               <th>序号</th>
                                <th>客户名称</th>
                                <th>登录名</th>
                                <th>联系人</th> 
                                <th>电话</th>  
                                <th>操作</th>
                            </tr>
                        </tfoot>
                        <tbody>
                              
                        </tbody>
                    </table>
                    <script type="text/ecmascript">
                        jQuery(document).ready(function ($) {
                            setTimeout(function () {
                                $(".checkbox-row input").addClass('cbr');
                                $(".dropdown-toggle").hide();
                            }, 0);
                        });
                    </script> 
                </div> 
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal"style="height:31px;background-color:#808080;font-weight:bold;color:white;font-size:12px;">关闭</button>
                </div>
            </div>
        </div> 
     
    </div>



    <!-- 设置要看的报警类型-->
	<div class="modal fade" >
		<div class="modal-dialog" id="DivMsgType">
			<div class="modal-content"> 
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">请选择要查看的报警类型</h4>
				</div> 
				<div class="modal-body">
			                        	<p>
											<label class="checkbox-inline">
												<input type="checkbox" value="6"/>
												断电报警
											</label>
											<label class="checkbox-inline">
												<input type="checkbox" value="9"/>
												离线报警
											</label>
											<label class="checkbox-inline">
												<input type="checkbox" value="5"/>
												SOS报警
											</label>
                                            <label class="checkbox-inline">
												<input type="checkbox" value="8"/>
												位移报警
											</label>
										</p>
                                        <p>
											<label class="checkbox-inline">
												<input type="checkbox" value="4"/>
												超速报警
											</label>
											<label class="checkbox-inline">
												<input type="checkbox" value="1"/>
												进围栏报警
											</label>
											<label class="checkbox-inline">
												<input type="checkbox" value="2"/>
												 出围栏报警
											</label>
                                              <label class="checkbox-inline">
												<input type="checkbox" value="7"/>
												 震动报警
											</label>
										</p> 
				</div> 
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal">关闭</button>
					<button type="button" class="btn btn-info">保存并关闭</button>
				</div>
			</div>
		</div>
	</div>

 

    	 	   <!-- 新增用户 -->
    <div class="modal fade">
		<div class="modal-dialog" id="DivAddUsers">
			<div class="modal-content">
			 
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">新增客户</h4>
				</div>
				
				<div class="panel-body">
                    <form role="form" class="form-horizontal" method="post">
								

		                    	<div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">上级客户</label> 
									<div class="col-sm-9"> 
											    <input type="text" class="form-control daterange" id="txtParentUserName" placeholder="上级客户" disabled value=""/>
									</div>
								</div>
                               <div class="form-group-separator"></div> 
                                <div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">客户名称</label> 
									<div class="col-sm-9"> 
											    <input type="text" class="form-control daterange" id="txtNewUserName" placeholder="请输入客户名称"  />
									</div>
								</div>
                      <div class="form-group-separator"></div> 
                                <div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">登录账号</label> 
									<div class="col-sm-9"> 
										   <input type="text" class="form-control daterange" id="txtNewLoginName" placeholder="请输入登录账号"/>
									</div>
								</div>
                         <div class="form-group-separator"></div> 
                                <div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">密码</label> 
									<div class="col-sm-9"> 
										   <input type="password" class="form-control daterange" id="txtNewUserPassword" placeholder="请输入密码"/>
									</div>
								</div>
                          <div class="form-group-separator"></div> 
                                <div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">确认密码</label> 
									<div class="col-sm-9"> 
										   <input type="password" class="form-control daterange" id="txtNewUserPassword1" placeholder="请确认密码"/>
									</div>
								</div>
                            <div class="form-group-separator"></div> 
                                <div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">类型</label> 
									<div class="col-sm-9"> 
										 <p>
											<label class="cbr-inline">
												<input type="radio" name="cbrAddUserType" class="cbr" value="1" checked/>
												用户
											</label>
											<label class="cbr-inline">
												<input type="radio" name="cbrAddUserType" class="cbr" value="2"/>
												经销商
											</label> 
										</p> 
									</div>
								</div>
                        <div class="form-group-separator"></div> 
                                <div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">联系人</label> 
									<div class="col-sm-9"> 
										   <input type="text" class="form-control daterange" id="txtNewContacts" placeholder="请输入联系人"/>
									</div>
								</div>
                       <div class="form-group-separator"></div> 
                                <div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">电话</label> 
									<div class="col-sm-9"> 
										  <input type="text" class="form-control daterange" id="txtNewPhone" name="min_field" data-validate="required,number,minlength[6]" placeholder="请输入电话号码"/>
									</div>
								</div> 
				     <div class="form-group-separator"></div> 
                                <div class="form-group">
									<label class="col-sm-3 control-label" for="field-1">地址</label> 
									<div class="col-sm-9"> 
										  <input type="text" class="form-control daterange" id="txtNewAddress"  placeholder="请输入地址"  />
									</div>
								</div>
						 
					</form>

				</div>   
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnAddUsersClose">关闭</button>
					<button type="submit" class="btn btn-success" id="benAddUsersSave">保存并关闭</button>
				</div>
          
			</div>
		</div>
    
	</div>
    
    
    
       <!-- 查询指令记录列表 -->
	<div class="modal fade">
            <div class="modal-dialog" style="width: 40%; height: 600px;min-width:1000px;" id="DivCommandRecord">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title"style="font-weight:bold;color:#808080;">查询指令记录</h4>
                </div>
                <div class="modal-body"> 
                    <table cellspacing="0" style="color:black;font-size:12px;" class="table table-small-font table-bordered table-striped">
                        <thead>
                            <tr>
                                <th style="width:50px;">序号</th>
                                <th style="width:120px;">指令类型</th>
                                <th style="width:110px;">指令状态</th>
                                <th style="width:170px;">响应信息</th> 
                                <th style="width:100px;">响应时间</th>  
                                <th style="width:100px;">发送时间</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th>序号</th>
                                <th>指令类型</th>
                                <th>指令状态</th>
                                <th>响应信息</th> 
                                <th>响应时间</th>  
                                <th>发送时间</th>
                            </tr>
                        </tfoot>
                        <tbody>
                              
                        </tbody>
                    </table>
                    <script type="text/ecmascript">
                        jQuery(document).ready(function ($) {
                            setTimeout(function () {
                                $(".checkbox-row input").addClass('cbr');
                                $(".dropdown-toggle").hide();
                            }, 0);
                        });
                    </script> 
                </div> 
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal"style="height:31px;background-color:#808080;font-weight:bold;color:white;font-size:12px;">关闭</button>
                </div>
            </div>
        </div> 
     
    </div> 

    <div class="modal fade" >
        	<div class="modal-dialog" id="DivAddDevicesGPS">
			<div class="modal-content"> 
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">新车辆</h4>
				</div>
				
                <div class="panel-body">
                    <form role="form" class="form-horizontal" method="post">
                        <div class="form-group">
                            <label class="col-sm-3 control-label" for="field-1">车牌号:</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control daterange" id="txtAddDeviceCarNum" placeholder="请输入车牌" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label" for="field-1">联系人:</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control daterange" id="txtAddDeviceCarUser" placeholder="请输入联系人" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label" for="field-1">电话号:</label> 
                            <div class="col-sm-6">
                                <input type="text" class="form-control daterange" id="txtAddDeviceCarPhone" placeholder="请输入电话" />
                            </div> 
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">备&nbsp &nbsp注:</label>
                            <div class="col-sm-6">
                                 <textarea class="form-control" cols="5" id="txtAddDeviceDescription" placeholder="请输入备注"></textarea> 
                            </div>
                        </div>
                        <div class="form-group-separator"></div>
                        <div class="form-group"> 
                             <label class="col-sm-3 control-label" for="field-1">IMEI号:</label> 
                      
                            <div class="col-sm-6"> 
                                <select class="form-control" id="selAddDevicesGpsDeviceList">
                                    <option></option> 
                                </select>  
                               <%-- <input type="password" class="form-control daterange" id="txtAddDevice" placeholder="请输入IMEI" />--%>
                            </div>
                            <div class="col-sm-3">
                             <%--   <button type="button" class="btn  btn-success">添加</button>--%>
                            </div>
                          
                        </div>
                          <div class="form-group" >
                           
                        <table class="table table-small-font table-bordered table-striped" id="tabAddDevicesGPSTable">
                            <thead>
                                <tr>
                                    <td>IMEI</td>
                                    <td>设备名称</td>
                                    <td>操作</td>
                                </tr>
                            </thead>
                            <tbody  >
                                
                            </tbody> 
                        </table>
                        </div>
                      </form>

                </div>   
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnAddDevicesGPSClose">关闭</button>
					<button type="submit" class="btn btn-success" onclick="AddDevicesGpsBtnSave()" id="benAddDevicesGPSSave">保存并关闭</button>
				</div>
          
			</div>
		</div>
   
    </div>
</body>
</html>

 
