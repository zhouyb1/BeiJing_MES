/* * 创建人：超级管理员
 * 日  期：2019-11-06 16:30
 * 描  述：客户表
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initData();
        },
        bind: function () {
            //客户名称验证
            //$("#C_Name").on('blur', function () {
            //    var code = $.trim($(this).val()); //去除空格
            //    var html = '<div class="am-field-error-info" id="isCode" title="该客户名称已存在！"><i class="fa fa-info-circle"></i></div>';
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/IsCode',
            //        data: { tables: "Mes_Customer", field: "C_Name", code: code, keyValue: keyValue },
            //        success: function (data) {
            //            var isOk = JSON.parse(data).data;
            //            if (isOk) {
            //                $("#C_Name").addClass("am-field-error");
            //                $("#C_Name").parent().append(html);
            //                ayma.alert.error("该客户名称已存在");
            //            } else {
            //                $("#C_Name").removeClass("am-field-error");
            //                $("#isCode").remove();
            //            }
            //        }
            //    });
            //});
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Custom/GetFormData?keyValue=' + keyValue, function (data) {
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
        if (!$('#form').Validform()) {
            return false;
        }
        var postData = $('#form').GetFormData();
        $.SaveForm(top.$.rootUrl + '/MesDev/Custom/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
