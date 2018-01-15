<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_success.aspx.cs" Inherits="MG_WeChat.pay.pay_success" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>支付完成</title>
    <link rel="stylesheet" href="http://cdn.amazeui.org/amazeui/2.7.2/css/amazeui.min.css">
       <script src="../bootstrap/js/jquery-2.1.3.min.js"></script>
    <script src="http://cdn.amazeui.org/amazeui/2.7.2/js/amazeui.min.js"></script>
    <script src="../bootstrap/js/Ajax.js"></script>
    <script src="../bootstrap/js/public.js"></script>
    <style type="text/css">
        html, body  {
        height:100%;overflow:hidden
        }
        .am-container {
            padding-top:49px;
            height:100%;
            text-align:center;
            line-height:100%
        }
    </style>
    <script type="text/javascript">
        var index = 0;
        $(function () {
            $("#loading").modal('toggle');
            query();
        });
        function query()
        {
             $.ajax({
                url: "Orders.asmx",
                way: "QueryOrder",
                pars: { orderno: '<%= order_no %>', transactionid:'' },
                success: function (res) {
                    if (res.StatusCode == 300) {
                        index++;
                        if (index > 3) {
                            $(".am-modal-hd").text("支付失败...");
                            setTimeout(function () {
                                window.location.href = "../device/tracking.html?deviceid=" + <%=deviceid%>;
                            }, 500); 
                            return;
                        }
                        setTimeout(function(){
                            query();
                        },200);
                        
                    } else {
                        $(".am-modal-hd").text("支付成功,正在跳转...");
                        setTimeout(function () {
                            window.location.href = "../myinfo/orders-detail.html?t=pay&o=" +res.Result;
                        }, 500); 
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    </script>
</head>
<body>
    <!--<header data-am-widget="header" class="am-header am-header-default">
        <div class="am-header-left am-header-nav">
            <a href="javascript:;" class="" onclick="javascript:history.back();">
                <i class="am-header-icon am-icon-chevron-left"></i>
            </a>
        </div>

        <h1 class="am-header-title">
            <a href="#title-link" class="">
                支付完成
            </a>
        </h1>
    </header>-->
    <div class="am-container" style="border:1px solid red;">
       <%-- <button type="button">正在跳转</button>--%>
    </div>
    <div class="am-modal am-modal-loading am-modal-no-btn" tabindex="-1" id="loading">
        <div class="am-modal-dialog">
            <div class="am-modal-hd">正在确认结果...</div>
            <div class="am-modal-bd">
                <span class="am-icon-spinner am-icon-spin"></span>
            </div>
        </div>
    </div>

</body>
</html>
