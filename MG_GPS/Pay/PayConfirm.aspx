<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayConfirm.aspx.cs" Inherits="MG_GPS.Pay.PayConfirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
     <title>在线支付</title>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <link rel="stylesheet" href="http://cdn.amazeui.org/amazeui/2.7.2/css/amazeui.min.css"/>
    <script src="../Scripts/jquery-1.8.3.js"></script>
    <script src="http://cdn.amazeui.org/amazeui/2.7.2/js/amazeui.min.js"></script>
    <script src="../PayActivation/assets/layer_mobile/layer.js"></script>
     <script>
           window.onload=function(){
              if('<%= state %>' != "")
              {
                  layer.open({
                      content: '<%= state %>'
                  , btn: '确定'
                      ,yes:function (index) {
                          history.back();
                      }
                  });
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
                          
                           
                               $("#loading").modal('toggle');
                               window.location.href="<%= callback_url%>";
                               //alert("支付成功");
                               //$("#loading").modal('toggle');
                              // document.getElementById("").click();
                          
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
    </script>
</head>
<body>
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
            支付金额：<font style="font-size:xx-large">¥<%= total_fee  %></font>  
        </div>
        <hr data-am-widget="divider" style="" class="am-divider am-divider-default" />
        <div class="am-g am-g-fixed">
          <div class="am-u-sm-3" style="text-align:right">收款方:</div>
          <div class="am-u-sm-9">东莞市美谷电子科技</div>
        </div>
        <div class="am-g am-g-fixed">
          <div class="am-u-sm-3" style="text-align:right">商品名:</div>
          <div class="am-u-sm-9"><%=prductName %></div>
        </div>
        <hr data-am-widget="divider" style="" class="am-divider am-divider-default" />
        <div class="am-g" align="center">
		    <br/><br/><br/> 
            <button type="button" onclick="callpay();return false;" style="width:90%; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:16px;">立即支付</button>
         
	    </div>
    </div>
</body>
</html>
