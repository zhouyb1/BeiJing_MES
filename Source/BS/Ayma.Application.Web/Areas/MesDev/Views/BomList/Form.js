/* * 创建人：超级管理员
 * 日  期：2019-01-07 15:39
 * 描  述：物料清单列表
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
            //物料列表
            var dfop = {
                type: 'default',
                value: 'G_Code',
                text: 'G_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetGoodsList',
                // 访问数据接口参数
                param: {}
            }
            $("#B_GoodsCode").select(dfop).on('change',function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetGoodsEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#B_GoodsName").val(entity.G_Name);
                        $("#B_Unit").val(entity.G_Unit);
                    }
                });
            });
            $("#B_SecGoodsCode").select(dfop).on('change',function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetGoodsEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#B_SecGoodsName").val(entity.G_Name);
                        $("#B_SecUnit").val(entity.G_Unit);
                        $("#B_SecQty").val(entity.G_Qty);
                    }
                });
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/BomList/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                    $("#B_Date").val(data.Mes_BomData.B_Date);
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        if ($("#B_GoodsCode").selectGet() == $("#B_SecGoodsCode").selectGet()) {
            ayma.alert.error("下级物料编码跟上级物料编码不能一致");
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/BomList/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
