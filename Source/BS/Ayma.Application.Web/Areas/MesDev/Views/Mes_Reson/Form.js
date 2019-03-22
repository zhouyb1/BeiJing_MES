/* * 创建人：超级管理员
 * 日  期：2019-03-13 19:30
 * 描  述：不合格原因表
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
            if (keyValue != "") {
                $("#R_Code").attr("readOnly", "true");
            }
            //编码重复验证
            $("#R_Code").on('blur', function () {
                var code = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isCode" title="编码重复！"><i class="fa fa-info-circle"></i></div>';
                    $.ajax({
                        type: "get",
                        url: top.$.rootUrl + '/MesDev/Tools/IsCode',
                        data: { tables: "Mes_Reson", field: "R_Code", code: code, keyValue: keyValue },
                        success: function(data) {
                            var isOk = JSON.parse(data).data;
                            if (isOk) {
                                $("#R_Code").addClass("am-field-error");
                                $("#R_Code").parent().append(html);
                                ayma.alert.error("编码重复");
                                return false;
                            } else {
                                $("#R_Code").removeClass("am-field-error");
                                $("#isCode").remove();
                            }
                        }
                    });
            });
            //名称重复验证
            $("#R_Name").on('blur', function () {
                var name = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isName" title="名称重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsName',
                    data: { tables: "Mes_Reson", field: "R_Name", names: name, keyValue: keyValue },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#R_Name").addClass("am-field-error");
                            $("#R_Name").parent().append(html);
                            ayma.alert.error("名称重复");
                            return false;
                        } else {
                            $("#R_Name").removeClass("am-field-error");
                            $("#isName").remove();
                        }
                    }
                });
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_Reson/GetFormData?keyValue=' + keyValue, function (data) {
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
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_Reson/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
