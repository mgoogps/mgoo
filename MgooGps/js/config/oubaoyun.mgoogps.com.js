 
(function (win) {
    win.WelcomeMessage = "欧宝云GPS管理平台";
    $(function () {
        $("#btnDownloadApp").on("click", function () {
            self.location.href = "http://www.mgoogps.com/app/oubaoyun/download.html";
        })
    })
}(window));
