<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scanCode.aspx.cs" Inherits="MG_GPS.scanCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="description" content=""/>
    <meta name="keywords" content=""/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="http://code.jquery.com/jquery-latest.js"></script>

   <link rel="stylesheet" href="http://cdn.amazeui.org/amazeui/2.7.2/css/amazeui.min.css" />
    <script src="http://cdn.amazeui.org/amazeui/2.7.2/js/amazeui.min.js"></script>
     
    <title></title>
</head>
<body> 
        <div class="am-modal am-modal-loading am-modal-no-btn" tabindex="-1" id="loading">
          <div class="am-modal-dialog">
            <div class="am-modal-hd">请扫描VC码...</div>
            <div class="am-modal-bd">
              <span class="am-icon-spinner am-icon-spin"></span>
            </div>
          </div>
        </div>

    <script type="text/javascript">
        $("#loading").modal('toggle');
        wx.config({
            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: '<%= appid%>', // 必填，公众号的唯一标识
            timestamp: '<%= times%>', // 必填，生成签名的时间戳
            nonceStr:'<%= noncestr%>' , // 必填，生成签名的随机串
            signature: '<%= signature%>',// 必填，签名，见附录1
            jsApiList: ["scanQRCode"] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        });
        wx.ready(function () {
            wx.scanQRCode({
                needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
                success: function (res) {
                    var result = res.resultStr; // 当needResult 为 1 时，扫码返回的结果
                    // alert(result);
                    window.location.href = "http://m.mgoogps.com:8070/myinfo/adddevice.html?scancoderesult="+result;
                }
            });
            // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
        });
        function scanClick()
        {
           
        }

        //////////// 添加设备

        function scanning() {

          
        }
    </script>
</body>
</html>
