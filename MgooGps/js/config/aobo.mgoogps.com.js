 
(function (win) {
    win.WelcomeMessage = "澳博车贷GPS管理平台";
    $(function () {
        $("#btnDownloadApp").on("click", function () {
            self.location.href = "http://aobo.mgoogps.com/app/aobo/download.html";
        })
    })
}(window));
