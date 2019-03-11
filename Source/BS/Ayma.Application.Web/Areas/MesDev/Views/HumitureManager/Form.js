/* * 创建人：超级管理员
 * 日  期：2019-03-06 14:49
 * 描  述：温湿度采集参数设置
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
            $('#H_Kind').DataItemSelect({ code: 'HumitureType' });
            //上下值校验
            $("#H_Up").on('keyup', function() {
                var h_up = $.trim($(this).val()); //去除空格
                var h_down = $.trim($("#H_Down").val());
                var html = '<div class="am-field-error-info" id="isCode" title="上限值不能小于上限值！"><i class="fa fa-info-circle"></i></div>';

                if (parseFloat(h_up) <parseFloat(h_down)) {
                    $("#H_Up").addClass("am-field-error");
                    $("#H_Up").parent().append(html);
                    ayma.alert.error("上限值不能小于下限值");
                } else {
                    $("#H_Up").removeClass("am-field-error");
                    $("#isCode").remove();
                }
            });


            $("#H_Down").on('keyup', function() {
                var html = '<div class="am-field-error-info" id="isCode" title="下限值不能大于上限值！"><i class="fa fa-info-circle"></i></div>';
                var h_down = $.trim($(this).val()); //去除空格
                var h_up = $.trim($("#H_Up").val());
                if (parseFloat(h_up) < parseFloat(h_down)) {
                    $("#H_Down").addClass("am-field-error");
                    $("#H_Down").parent().append(html);
                    ayma.alert.error("下限值不能大于上限值");
                } else {
                    $("#H_Down").removeClass("am-field-error");
                    $("#isCode").remove();
                }
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/HumitureManager/GetFormData?keyValue=' + keyValue, function (data) {
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
        $.SaveForm(top.$.rootUrl + '/MesDev/HumitureManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
