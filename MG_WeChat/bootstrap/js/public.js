

function WxConfig(callback) {
    alert("2");
    $.ajax({
        url: "UsersAjax.asmx",
        way: "GetWxConfig",
        pars: {},
        success: function (data) {
            callback(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function GetModelSelects() {

    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetModelList",
        pars: { },
        success: function (data) {
            var models = JSON.parse(data);
            var html = [];
            html.push('<option value="">-请选择-</option>');
            var $selected = $("#model-selected");
            for (var i = 0; i < models.length; i++) {
                html.push(' <option value="' + models[i].DataValue + '">' + models[i].DataText + '</option> ');
            }
            $selected.empty().append(html.join(''));
            if (!$.AMUI.support.mutationobserver) {
                $selected.trigger('changed.selected.amui');
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function GetGroupsSelects(callback) {
    var info = mg.GetUserInfo();
    $.ajax({
        url: "DevicesAjax.asmx",
        way: "GetGroupList",
        async:false,
        pars: { userid: info.UserID },
        success: function (data) {
            var groups = JSON.parse(data);
            var html = [];
            var $selected = $("#group-selected");
            for (var i = 0; i < groups.length; i++) {
                html.push(' <option value="' + groups[i].GroupID + '">' + groups[i].GroupName + '</option> ');
            }
            $selected.empty().append(html.join(''));
            if (!$.AMUI.support.mutationobserver) {
                $selected.trigger('changed.selected.amui');
            }
            try { 
                if (callback)
                    callback();
            } catch (e) {
                console.log(e);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}



function getCode(inputPhone,type, _this) {
 
    var phone = $("#" + inputPhone).val();
   
    //var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
    //if (!myreg.test(phone)) {
    //    alert('请输入有效的手机号码！');
    //    return;
    //}

    var data = { phone: phone, type:type };
    $.ajax({
        url: "/ajax/Login.asmx/SMSCodes",
        data: JSON.stringify(data),
        type: "post",
        dataType: "json",
        contentType: "application/json",
        success: function (r) {
            var result = JSON.parse(r.d);
            if (result.StatusCode === 200) {
                Countdown(_this);
            }
            else {
                alert(result.Message);
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}
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

function verificationCode(phone, code, callback)
{
    var data = { phone: phone,code:code };
    $.ajax({
        url: "/ajax/Login.asmx/VerificationCode",
        data: JSON.stringify(data),
        type: "post",
        dataType: "json",
        async:false,
        contentType: "application/json",
        success: function (r) {
            var result = JSON.parse(r.d); 
            if (result.StatusCode === 200) { 
                callback(phone,result.Result);
            }
            else {
                alert(result.Message);
            }
            return result;
        },
        error: function (e) {
            console.log(e);
        }
    });
}



function amModal(opts) { 
    if ($("#your-modal").length > 0) {
        $("div[name=amModalTitle]").html(opts.title || "提示");
        $("div[name=amModalMsg]").html(opts.msg);
        var html = []; 
        if (opts.cancel) {
            html.push('<span class="am-modal-btn" data-am-modal-cancel>取消</span>');
        }
        html.push('<span class="am-modal-btn" id="amModalBtnConfirm" data-am-modal-confirm>确定</span>');
        $("#your-modal .am-modal-footer").html(html.join(''));
    } else {
        var html = [];
        html.push(' <div class="am-modal am-modal-alert" tabindex="-1" id="your-modal">');
        html.push(' <div class="am-modal-dialog">');
        html.push(' <div class="am-modal-hd" name="amModalTitle">' + (opts.title || "提示") + '</div>');

        html.push(' <div class="am-modal-bd" name="amModalMsg"> ' + opts.msg + ' </div>  ');
        html.push(' <div class="am-modal-footer"> ');
        if (opts.cancel) {
            html.push('  <span class="am-modal-btn" data-am-modal-cancel>取消</span>  ');
        }
        html.push('<span class="am-modal-btn" id="amModalBtnConfirm" data-am-modal-confirm>确定</span>  '); 
        html.push(' </div></div> </div>');
        html.push(' <div class="am-modal-dialog"></div>');
        $("body").append(html.join(''));
    }
}
amModal.prototype.open = function () {
    $('#your-modal').modal({ relatedTarget: this, closeViaDimmer: false });
    //closeViaDimmer：点击遮罩层时关闭 Modal，默认为 true
}
amModal.prototype.close = function () {
    $('#your-modal').modal('close')
}
amModal.prototype.onConfirm = function (callback) {
    var _this = this;
    $("#amModalBtnConfirm").unbind("click").on("click", function (e) {
         callback();
        _this.close();
    });
}
amModal.prototype.onCancel = function (callback) {
    //$('#your-modal').modal({
    //    relatedTarget: this,
    //    onCancel: callback()
    //})
    $("#your-modal .am-modal-btn").eq(1).unbind("click").click(function (e) {
        callback();
    });
}