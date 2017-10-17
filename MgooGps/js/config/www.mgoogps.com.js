 
(function (win) {
    win.WelcomeMessage = "GPS物联网监控平台";
    $(function () {
        $("#btnDownloadApp").on("click", function () {
            self.location.href = "http://www.mgoogps.com/mgooapp/download.html";
        })
    })
}(window));
