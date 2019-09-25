/* * 创建人：超级管理员
 * 日  期：2019-06-27 15:26
 * 描  述：班组表
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
            //编码重复验证
            $("#T_Code").on('blur', function () {
                var code = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isCode" title="编码重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsCode',
                    data: { tables: "Mes_Team", field: "T_Code", code: code, keyValue: keyValue },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#T_Code").addClass("am-field-error");
                            $("#T_Code").parent().append(html);
                            ayma.alert.error("编码重复");
                        } else {
                            $("#T_Code").removeClass("am-field-error");
                            $("#isCode").remove();
                        }
                    }
                });
            });

            //名称重复验证
            $("#T_Name").on('blur', function () {
                var name = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isName" title="名称重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsName',
                    data: { tables: "Mes_Team", field: "T_Name", names: name, keyValue: keyValue },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#T_Name").addClass("am-field-error");
                            $("#T_Name").parent().append(html);
                            ayma.alert.error("名称重复");
                        } else {
                            $("#T_Name").removeClass("am-field-error");
                            $("#isName").remove();
                        }
                    }
                });
            });
            ////仓库名称
            $("#T_StockCode").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            });
            //车间名称
            $("#T_WorkShopName").select({
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
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByNameGetWorkShopEntity',
                    data: { name: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#T_WorkShopCode").val(entity.W_Code);
                    }
                });
            });
        },
        initData: function () {
            if (!!selectedRow) {
                $('#form').SetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        var postData = $('#form').GetFormData();
        $.SaveForm(top.$.rootUrl + '/MesDev/Team/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
