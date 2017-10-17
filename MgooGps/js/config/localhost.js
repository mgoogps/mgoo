
(function (win) {
    win.WelcomeMessage = "本地测试";
    $(function () {
        $("#btnDownloadApp").on("click", function () {
            self.location.href = "http://aobo.mgoogps.com/app/aobo/download.html";
        })
    })
   
}(window));
