/* * 创建人：超级管理员
 * 日  期：2019-01-18 09:40
 * 描  述：容器毛重列表
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
        },
        bind: function () {
            //编码重复验证
            $("#B_BasketCode").on('keyup', function () {
                var code = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isCode" title="编码重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsCode',
                    data: { tables: "Mes_Basket", field: "B_BasketCode", code: code },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#B_BasketCode").addClass("am-field-error");
                            $("#B_BasketCode").parent().append(html);
                            ayma.alert.error("编码重复");
                        } else {
                            $("#B_BasketCode").removeClass("am-field-error");
                            $("#isCode").remove();
                        }
                    }
                });
            });
            //名称重复验证
            $("#B_BasketName").on('keyup', function () {
                var name = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isName" title="名称重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + "/MesDev/Tools/IsName",
                    data: { tables: "Mes_Basket", field: "B_BasketName", names: name },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#B_BasketName").addClass("am-field-error");
                            $("#B_BasketName").parent().append(html);
                            ayma.alert.error("名称重复");
                        } else {
                            $("#B_BasketName").removeClass("am-field-error");
                            $("#isName").remove();
                        }
                    }
                });
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/BasketList/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/BasketList/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
