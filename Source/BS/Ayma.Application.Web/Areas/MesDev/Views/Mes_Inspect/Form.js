/* * 创建人：超级管理员
 * 日  期：2019-03-13 20:54
 * 描  述：抽检记录
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
            $('#I_GoodsCode').select(dfop).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetGoodsEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#I_GoodsName").val(entity.G_Name);
                    }
                });
            });
            //抽检类型
            $('#I_Kind').DataItemSelect({ code: 'InspectType' });
            //车间编码
            $('#I_Class').select({
                type: 'default',
                value: 'C_Code',
                text: 'C_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetClassList',
                // 访问数据接口参数
                param: {}
            });

            //不合格原因
            $('#I_Reson').select({
                type: 'default',
                value: 'R_Code',
                text: 'R_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetReasonList',
                // 访问数据接口参数
                param: {}
            });
            //生产订单号校验
            $('#I_OrderNo').on('blur', function () {
                var orderNo = $.trim($(this).val()); //生产订单号
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsOrderNo',
                    data: { tables: "Mes_ProductOrderHead", field: "P_OrderNo", orderNo: orderNo },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (!isOk) {
                            ayma.alert.error("生产订单不存在");
                            return false;
                        }
                    }
                });
            });

            $('input[type=number]').keypress(function (e) {
                if (!String.fromCharCode(e.keyCode).match(/[0-9\.]/)) {
                    return false;
                }
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_Inspect/GetFormData?keyValue=' + keyValue, function (data) {
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
        var I_QualifiedQty = $.trim($('#I_QualifiedQty').val()); //合格数量
        var I_GoodsQty = $.trim($('#I_GoodsQty').val()); // 抽检数量
        //console.log(I_QualifiedQty);
        //if (isNaN(I_QualifiedQty)) {
        //    ayma.alert.error('合格数量输入的字符不合法!');
        //    return false;
        //}
        if (I_QualifiedQty > I_GoodsQty) {
            ayma.alert.error('合格数量不能大于抽检数量!');
            return false;
        };
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_Inspect/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
