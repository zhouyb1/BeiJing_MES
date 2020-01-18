/* * 创建人：超级管理员
 * 日  期：2019-01-07 11:04
 * 描  述：门列表
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
            $("#D_Code").on('blur', function () {
                var code = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isCode" title="编码重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsCode',
                    data: { tables: "Mes_Door", field: "D_Code", code: code, keyValue: keyValue },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#D_Code").addClass("am-field-error");
                            $("#D_Code").parent().append(html);
                            ayma.alert.error("编码重复");
                        } else {
                            $("#D_Code").removeClass("am-field-error");
                            $("#isCode").remove();
                        }
                    }
                });
            });
            //名称重复验证
            $("#D_Name").on('blur', function () {
                var name = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isName" title="名称重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + "/MesDev/Tools/IsName",
                    data: { tables: "Mes_Door", field: "D_Name", names: name, keyValue: keyValue },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#D_Name").addClass("am-field-error");
                            $("#D_Name").parent().append(html);
                            ayma.alert.error("名称重复");
                        } else {
                            $("#D_Name").removeClass("am-field-error");
                            $("#isName").remove();
                        }
                    }
                });
            });
            //车间编码
            $("#W_Name").select({
                type: 'default',
                value: 'W_Name',
                text: 'W_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetWorkShopList',
                // 访问数据接口参数
            }).bind("change", function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetWorkShopEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#D_WorkShopCode").val(entity.W_Code);
                    }
                });
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/DoorList/GetFormData?keyValue=' + keyValue, function (data) {
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
        $.SaveForm(top.$.rootUrl + '/MesDev/DoorList/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
