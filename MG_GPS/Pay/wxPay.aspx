<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxPay.aspx.cs" Inherits="MG_GPS.Pay.wxPay" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>在线支付</title>
  <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
   <link rel="stylesheet" href="http://cdn.amazeui.org/amazeui/2.7.2/css/amazeui.min.css"/>
    <script src="../Scripts/jquery-1.8.3.js"></script>
    <script src="http://cdn.amazeui.org/amazeui/2.7.2/js/amazeui.min.js"></script>
     
</head>
      <script type="text/javascript"> 
          window.onload=function(){
              if('<%= state %>' != "")
              {
                  $("#my-alert .am-modal-bd").text('<%= state %>');
                  $("#my-alert").modal('toggle');
              }       
          }
          //调用微信JS api 支付
          function jsApiCall()
          {
              WeixinJSBridge.invoke(
                  'getBrandWCPayRequest',
                  <%= wxJsApiParam ??"''" %>,//josn串
                   function (res)
                   {
                       if(res.err_msg == "get_brand_wcpay_request:ok" ) { 
                           // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 
                           var type = $("#type").val();
                           
                           if (type == 1) { 
                               sendCommand(); 
                           }else
                           {
                               $("#loading").modal('toggle');
                               window.location.href="<%= callback_url%>";
                               //alert("支付成功");
                               //$("#loading").modal('toggle');
                              // document.getElementById("").click();
                           }
                       }else {
                           alert("支付失败");
                       }
                       WeixinJSBridge.log(res.err_msg);
                   }
               );
          }

          function callpay()
          {
              if (typeof WeixinJSBridge == "undefined")
              {
                  if (document.addEventListener)
                  {
                      document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                  }
                  else if (document.attachEvent)
                  {
                      document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                      document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                  }
              }
              else
              {
                  jsApiCall();
              }
          }
         
          function sendCommand() {
           
              var userid = $("#userid").val();
              var deviceid = $("#deviceid").val();
              $.ajax({
                  url: "/AjaxService/DeviceManager.asmx/SendCommand",
                  type:"POST",
                  contentType: "application/json; charset=utf-8",
                  dataType:"JSON",
                  data: "{ command :'testty',userid:'" + userid + "',id:'" + deviceid + "',phone:'',mode:'',sens:''}", 
                  success: function (r) {
                      if (r.d=="success") {
                          alert("通油指令发送成功！");
                      } else {
                          alert("通油指令发送失败！");
                      }
                  },
                  error: function (error) { 
                      alert("指令出错");
                  }
              });
          }
      
         
     </script>
<body>
      <input type="text" style="display:none" id="userid" value="<%= userid %>" />
      <input type="text" style="display:none" id="deviceid" value="<%= deviceid %>" />
      <input type="text" style="display:none" id="type" value="<%= type %>" />
       
       <header data-am-widget="header" class="am-header am-header-default">
        <div class="am-header-left am-header-nav">
            <a href="javascript:;" class="" onclick="javascript:history.back();">
                <i class="am-header-icon am-icon-chevron-left"></i>
            </a>
        </div> 
        <h1 class="am-header-title">
            <a href="#title-link" class="">
                交易确认
            </a>
        </h1>
    </header>
    <div class="am-container" style="border:0px solid red">
        <div style="width:100%;height:100px;text-align:center;padding-top:20px;" >
            支付金额：<font style="font-size:xx-large">¥<%= Convert.ToInt32(total_fee)/100 %></font>  
        </div>
        <hr data-am-widget="divider" style="" class="am-divider am-divider-default" />
        <div class="am-g am-g-fixed">
          <div class="am-u-sm-3" style="text-align:right">收款方:</div>
          <div class="am-u-sm-9">美谷科技</div>
        </div>
        <div class="am-g am-g-fixed">
          <div class="am-u-sm-3" style="text-align:right">商品名:</div>
          <div class="am-u-sm-9"><%=tariff_name %></div>
        </div>
        <hr data-am-widget="divider" style="" class="am-divider am-divider-default" />
        <div class="am-g" align="center">
		    <br/><br/><br/> 
            <button type="button" onclick="callpay();return false;" style="width:90%; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:16px;">立即支付</button>
         
	    </div>
    </div>
    <div class="am-modal am-modal-loading am-modal-no-btn" tabindex="-1" id="loading">
        <div class="am-modal-dialog">
            <div class="am-modal-hd">正在确认结果...</div>
            <div class="am-modal-bd">
                <span class="am-icon-spinner am-icon-spin"></span>
            </div>
        </div>
    </div>

    <div class="am-modal am-modal-alert" tabindex="-1" id="my-alert">
      <div class="am-modal-dialog">
        <div class="am-modal-hd">提示</div>
        <div class="am-modal-bd">
         下单失败,请稍后再试！
        </div>
        <div class="am-modal-footer">
          <span class="am-modal-btn">确定</span>
        </div>
      </div>
    </div>
</body>
</html>
