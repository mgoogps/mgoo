<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recharge.aspx.cs" Inherits="MG_WeChat.pay.Recharge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/> 
	<meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>在线充值</title> 
    <link type="text/css" href="pay_files/ls.css" rel="stylesheet"/>
    <script src="pay_files/hm.js"></script><script type="text/javascript" src="pay_files/jquery.js"></script>
    <script type="text/javascript" src="pay_files/publicMobile.js"></script>
       <%--<link rel="stylesheet" href="http://cdn.amazeui.org/amazeui/2.7.2/css/amazeui.min.css"/>--%>
    <style type="text/css">
        html, body {
          overflow:hidden
        }
        header font {
        font-size: 16px;
        }  header strong {
        font-size: 16px;
        }
    </style>
    <script>
var _hmt = _hmt || [];
(function() {
  var hm = document.createElement("script");
  hm.src = "https://hm.baidu.com/hm.js?6c83c0718ac733f5108b0edcab16fea5";
  var s = document.getElementsByTagName("script")[0]; 
  s.parentNode.insertBefore(hm, s);
})();
</script>

</head>
<body>
    
<header>  
   <div ><img style="height:110px;width:110px;margin-left:-15px;margin-top:-2px;" src="/images/logo.png"></div> 
	<div style="padding-left:18px;margin-top:17px;">
         <font >设备名称:</font> <strong><%= imei %></strong> 
           <br /> <font>所属用户:</font> <strong><%= user_name %></strong> <br /> 
	 <span style="font-size: small">一共<%=device_count %>台设备,有<%= expire_count %>台即将过期  </span> 
	</div>
</header>
<div class="null">&nbsp;</div>
    
<div class="pttime">
	<div class="pt1"><img src="pay_files/qqw_03.png" style="height:45px;width:50px;margin-left:9px;"></div>
	<div class="pt2">
            <div>流量卡使用情况</div>
            <div class="jdt">
            <div>47%</div>
                <div>&nbsp;<div class="yy" style="width: 47%;">&nbsp;</div></div>
            <div style="white-space:nowrap;margin-left:-21px;width:100%">剩余<%= the_rest %>个月</div>
            <input id="flow" value="<%=use_situation %>" type="hidden">
            </div>
	</div>
	<div class="pt3" style="margin-top:3px;">
          <%= expire_date %><br>到期日期
      </div>
</div>
<div class="null">&nbsp;</div>
    <nav>
	<div class="tc">GPS流量套餐充值</div>
	<ul class="appendimg">
        <%
            foreach (Dictionary<string,string> item in PriceList)
            {%>
                <li>
		            <img src="pay_files/WechatIMG69_03.png">
		            <a style="display:block" href="http://m.mgoogps.com/Pay/wxPay.aspx?deviceid=<%= device_id %>&userid=<%= user_id %>&total_fee=<%= item["Price"] %>&openid=<%= open_id %>&tariff_id=<%= item["ID"] %>">
			            <div class="money1"><%= item["TariffName"] %></div>
			            <div class="money2"><span><%= Convert.ToInt32( item["Price"])/100 %></span>元</div>
			            <div class="money3">已购：<%= item["BuyCount"] %>人 <br /> <s>原价<%= item["OldPrice"].ToString() %>元</s>  </div>
			            <p class="money4">充&nbsp;值</p>
	                </a>	
		        </li>
            <%}
        %> 
	</ul>
        <br />
       <hr style="width:100%" />
        <br />
    <div style="font-size:small;text-align:left;padding-left:20px;">
        <div>1、流量充值将在24小时内到账。</div> 
        <div>2、请仔细核对您的IMEI号，确保充值成功进行。</div> 
        <div>3、如充值遇到问题请拨打4008 6969 77。</div> 
    </div>
</nav>
   <%--  <header data-am-widget="header" class="am-header am-header-default">
        <div class="am-header-left am-header-nav">
            <a href="javascript:;" class="" onclick="javascript:history.back();">
                <i class="am-header-icon am-icon-chevron-left"></i>
            </a>
        </div>

        <h1 class="am-header-title">
            <a href="#title-link" class="">
                GPS流量充值
            </a>
        </h1>
    </header>
    <div class="am-container">
        <br />
        <div style="width:100%;text-align:center;" class="am-text-xl">充值设备: <%= imei %> </div>
        <br />
         <hr data-am-widget="divider" style="" class="am-divider am-divider-default" />
         <div class="am-g am-g-fixed am-text-center" style="height:80px">
              <%
                  foreach (KeyValuePair<string,string> item in PriceDic)
                  {%>
                       <div class="am-u-sm-4 am-text-nowrap"> <div price="<%= Convert.ToInt32(item.Value)  %>"><p> <%= item.Key %></p> <p><%= item.Value %>元</p></div> </div>  
                  <%}
                  %>  
       </div>
        <br /> 
        <hr data-am-widget="divider" style="" class="am-divider am-divider-default" />
        <button type="button" id="btnPay">立即支付</button>
          <br /> 
          <hr data-am-widget="divider" style="" class="am-divider am-divider-default" />
        <div>
           <ul>
               <li>流量充值将在24小时内到账。</li> 
                <li>请仔细核对您的IMEI号，确保充值成功进行。</li> 
                <li>如充值遇到问题,拨打4008 6969 77。</li> 
           </ul>
        </div>
    </div> --%>
    <script src="/bootstrap/js/jquery-2.1.3.min.js"></script>
    <script src="/bootstrap/js/amazeui-2.7.2.min.js"></script> 
    <script src="../bootstrap/js/public.js"></script>
    <script src="../bootstrap/js/Ajax.js"></script>
    <script type="text/javascript">
        var deviceid = GetQueryString("deviceid");
        var type = GetQueryString("type");
        var openid = GetQueryString("openid");
        
        var loginInfo = mg.GetUserInfo();  
        loginInfo.UserID  = loginInfo.UserID || "";
        loginInfo.Token = loginInfo.Token || "";    
        $(function () {
            var flow = $("#flow").val();
            var flow1 = flow + '%';
            $('.yy').css({ 'width': flow1 });
            $('.jdt').find('div').eq(0).html(flow1);

            $(".am-u-sm-4 div").on("click", function () {
                var price = $(this).attr("price");
                $(".am-u-sm-4 div").css({ "background-color": "#FFFFFF" });
                $(this).css({ "background-color": "#66CCFF" });
                $("#btnPay").text("立即支付："+price+"元");
            });
            $(".am-list li").on("click", function () {
                var price = $(this).find("span").attr("price");
                if (openid.length < 5) {
                    openid = loginInfo.Token.split('@')[0];
                } 
                window.location.href = "http://m.mgoogps.com/Pay/wxPay.aspx?deviceid=" + deviceid + "&userid=" + loginInfo.UserID + "&total_fee=" + price + "&openid=" + openid + "&type=" + type;
            });
        }); 
    </script>
</body>
</html>
