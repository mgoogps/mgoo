 

(function (win) {
    win.WelcomeMessage = "美谷北斗/GPS物联平台";
    $(function () {
        $("#btnDownloadApp").on("click", function () {
            self.location.href = "http://www.mgoogps.com/mgooapp/download.html";
        })
    })
}(window));
