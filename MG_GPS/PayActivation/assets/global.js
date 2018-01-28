+function () {
    var global = {}; 
    layer.msg = function (msg) { 
        layer.open({
            content: msg
            , skin: 'msg'
            , time: 2 //2秒后自动关闭
        });
    }
    layer.alert = function (msg) {
        layer.open({
            content: msg
           , btn: '确定'
        });
    }
    global.ajax = function (opts) {
        var layerIndex ;
        opts.contentType = opts.contentType || "application/json";
        opts.type = opts.type || "post";
        opts.dataType = "json";
        if (opts.type=='post') {
            opts.data = JSON.stringify(opts.data);
        }
        opts.beforeSend = opts.beforeSend || function (XMLHttpRequest) { }; 
        opts.complete = opts.complete || function (XMLHttpRequest, textStatus) { };
        var before = opts.beforeSend;
        var complete = opts.complete;
        opts.beforeSend = function (XMLHttpRequest) { 
            layerIndex = layer.open({ type: 2 });
            before(XMLHttpRequest);
        }
        opts.complete = function (XMLHttpRequest, textStatus) {
            layer.close(layerIndex);
            complete(XMLHttpRequest, textStatus);
        }
        var error = opts.error || function () { };
        var success = opts.success || function () { };
        opts.success = function (res) { 
            if (res.code != 0) {
                layer.alert(res.message);
            }
            success(res);
        }
        opts.error = function (a,b,c) {
            layer.closeAll();
            layer.msg("网络超时,请稍后再试.");
            error(a, b, c);
        }
        $.ajax(opts);
    }

    global. getCode = function(inputPhone, type, _this) {

        var phone = $("#" + inputPhone).val();
 
        var data = { phone: phone, type: type };
        global.ajax({
            url: "/api/user/code",
            data:  data , 
            success: function (result) {
                //var result = r JSON.parse(r.d);
                if (result.code == 0) {
                    Countdown(_this);
                }
            },
            error: function (e) {
                console.log(e);
            }
        });
        function Countdown(_this) {
            $(_this).addClass('disabled');
            var i = 60;
            var time = setInterval(function () {
                if (i == 0) {
                    $(_this).removeClass('disabled');
                    $(_this).text("获取");
                    clearTimeout(time);
                } else {
                    $(_this).text(i + "秒");
                    i--;
                }
            }, 1000);
        }
    }
    window.global = global;
}();