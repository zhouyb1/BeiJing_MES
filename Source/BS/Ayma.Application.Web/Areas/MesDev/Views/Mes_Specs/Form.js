/* * 创建人：超级管理员
 * 日  期：2019-12-28 19:24
 * 描  述：物料包装数
 */
var Goodsstate;
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
            $("#S_GoodsName").select({
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
                        $("#S_GoodsCode").val(entity.G_Code);
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
        var code = $.trim($("#S_GoodsCode").val()); //去除空格
        var code2 = $.trim($("#S_UnitQty").val()); //去除空格
        $.ajax({
            type: "get",
            url: top.$.rootUrl + '/MesDev/Tools/IsCodeAndSupplyCode',
            data: { tables: "Mes_Specs", field: "S_GoodsCode", code: code, field2: "S_UnitQty", code2: code2, keyValue: keyValue },
            async: false,
            success: function (data) {
                var isOk = JSON.parse(data).data;
                if (isOk) {
                    $("#S_UnitQty").addClass("am-field-error");
                    ayma.alert.error("该物料已有此包装规格数");
                    Goodsstate = false;
                } else {
                    $("#S_UnitQty").removeClass("am-field-error");
                    Goodsstate = true;
                }
            }
        });
        if (Goodsstate == false) {
            return false;
        }
        //包装规格数 不小于0
        var S_UnitQty = $.trim($("#S_UnitQty").val()); //去除空格
        if (S_UnitQty != undefined && S_UnitQty != "") {
            if (!/^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(S_UnitQty.toString())) {
                ayma.alert.error("包装规格数只能为大于0的数字");
                $("#S_UnitQty").val(1);
                $("#S_UnitQty").addClass("am-field-error");
                return false;
            }
            else {
                $("#S_UnitQty").removeClass("am-field-error");
            }
        }
        var postData = $('#form').GetFormData();
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_Specs/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
