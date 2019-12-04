/* * 创建人：超级管理员
 * 日  期：2019-12-04 09:28
 * 描  述：原物料售卖价格表
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            //物料名称
            $("#O_GoodsName").select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
                // 访问数据接口参数
            }).bind("change", function () {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByNameGetGoodsEntity',
                    data: { name: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#O_GoodsCode").val(entity.G_Code);
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
        ////编码重复验证
        //$("#O_GoodsCode").on('blur', function () {
        //    var code = $.trim($(this).val()); //去除空格
        //    var html = '<div class="am-field-error-info" id="isCode" title="编码重复！"><i class="fa fa-info-circle"></i></div>';
        //    $.ajax({
        //        type: "get",
        //        url: top.$.rootUrl + '/MesDev/Tools/IsCode',
        //        data: { tables: "Mes_OutPrice", field: "O_GoodsCode", code: code, keyValue: keyValue },
        //        success: function (data) {
        //            var isOk = JSON.parse(data).data;
        //            if (isOk) {
        //                $("#O_GoodsCode").addClass("am-field-error");
        //                $("#O_GoodsCode").parent().append(html);
        //                ayma.alert.error("编码重复");
        //            } else {
        //                $("#O_GoodsCode").removeClass("am-field-error");
        //                $("#isCode").remove();
        //            }
        //        }
        //    });
        //});
        //物料价格 不小于0
        var period = $.trim($("#O_SalePrice").val()); //去除空格
        if (period != undefined && period != "") {
            if (!/^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(period.toString().replace('.', ''))) {
                ayma.alert.error("物料价格必须是非负数.");
                $("#O_SalePrice").val(1);
                return false;
            }
        }
        var postData = $('#form').GetFormData();
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_OutPrice/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
