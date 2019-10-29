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
            if (!!keyValue) {
                $('#I_GoodsName').attr('readonly', true);
                $('#I_StockName').attr('readonly', true);
                $('#I_Kind').attr('readonly', true);
                $('#I_OrderNo').attr('readonly', true);
                $('#I_GoodsName').attr('readonly', true);
            }
            $('#I_Batch').select();
            $('#I_GoodsCode').select();
            //抽检类型
            $('#I_Kind').DataItemSelect({ code: 'InspectType' });
            //仓库编码
            $('#I_StockCode').select({
                text: "s_name",
                value: "s_code",
                type: 'default',
                maxHeight: 200,
                allowSearch: true,
                url: top.$.rootUrl + '/AM_SystemModule/DataSource/GetDataTable',
                param: { code: "StockList", strWhere: "S_Kind = 3" },
            }).on('change', function () {
                var stock = $(this).selectGet();
                $('#I_GoodsCode').selectRefresh({
                    type: 'default',
                    url: top.$.rootUrl + '/MesDev/Tools/GetProductList',
                    value: 'i_goodscode',
                    text: 'i_goodsname',
                    maxHeight: 200,
                    allowSearch: true,
                    param: { stockCode: stock }
                }).on('change', function () {
                    var goodsCode = $('#I_GoodsCode').selectGet();
                    $('#I_Batch').selectRefresh({
                        type: 'default',
                        url: top.$.rootUrl + '/MesDev/Tools/GetProductBatchList',
                        value: 'i_batch',
                        text: 'i_batch',
                        maxHeight: 200,
                        allowSearch: true,
                        param: { goodsCode: goodsCode, stockCode: $('#I_StockCode').selectGet() }
                    });
                });
              
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

            //生产订单号
            $("#I_OrderNo").select({
                type: 'default',
                value: 'P_OrderNo',
                text: 'P_OrderNo',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderList',
                // 访问数据接口参数
                param: {}
            });
           
            //生产订单号校验
            //$('#I_OrderNo').on('blur', function () {
            //    var orderNo = $.trim($(this).val()); //生产订单号
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/IsOrderNo',
            //        data: { tables: "Mes_ProductOrderHead", field: "P_OrderNo", orderNo: orderNo },
            //        success: function (data) {
            //            var isOk = JSON.parse(data).data;
            //            if (!isOk) {
            //                ayma.alert.error("生产订单不存在");
            //                return false;
            //            }
            //        }
            //    });
            //});

            //现在只能输入数字
            $('input[type=number]').keypress(function (e) {
                if (!String.fromCharCode(e.keyCode).match(/[0-9\.]/)) {
                    return false;
                }
            });
            //验证抽检数量格式
            $('#I_GoodsQty').on('blur', function () {
                var I_GoodsQty = $.trim($('#I_GoodsQty').val()); // 抽检数量
                if ($.isEmptyObject(I_GoodsQty)) {
                    ayma.alert.error('请输入正确的抽检数量!');
                    return false;
                };
            });
            //验证合格数量是否大于抽检数量
            $('#I_QualifiedQty').on('blur', function() {
                var I_QualifiedQty = $.trim($('#I_QualifiedQty').val()); //合格数量
                var I_GoodsQty = $.trim($('#I_GoodsQty').val()); // 抽检数量
                var html = '<div class="am-field-error-info" id="I_Qty" title="合格数量大于抽检数量！"><i class="fa fa-info-circle"></i></div>';
                if ($.isEmptyObject(I_QualifiedQty)) {
                    ayma.alert.error('请输入正确的合格数量!');
                    return false;
                };
                if (parseInt(I_QualifiedQty) > parseInt(I_GoodsQty)) {
                    $("#I_QualifiedQty").addClass("am-field-error");
                    $("#I_QualifiedQty").parent().append(html);
                    ayma.alert.error('合格数量不能大于抽检数量!');
                    return false;
                } else {
                    $("#I_QualifiedQty").removeClass("am-field-error");
                    $("#I_Qty").remove();
                };
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
        if (parseInt(I_QualifiedQty) > parseInt(I_GoodsQty)) {
            ayma.alert.error('合格数量不能大于抽检数量!');
            return false;
        };
        var obj = $('body').GetFormData();
        obj.I_GoodsName = $('#I_GoodsCode').selectGetText();
        obj.I_GoodsCode = $('#I_GoodsCode').selectGet();
        obj.I_StockName = $('#I_StockCode').selectGetText();
        obj.I_Stockode = $('#I_StockCode').selectGetText();
        var postData = {
            strEntity: JSON.stringify(obj)
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
