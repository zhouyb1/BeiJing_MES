/* * 创建人：超级管理员
 * 日  期：2019-03-12 17:32
 * 描  述：排班记录
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
                value: 'W_Code',
                text: 'W_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetWorkShopList',
                // 访问数据接口参数
                param: {}
            }
            $('#A_WorkShopCode').select(dfop).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetWorkShopEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#A_WorkShopName").val(entity.W_Name);
                    }
                });
            });
            //是否有效
            $('#A_Avail').DataItemSelect({ code: 'YesOrNo' });
            $('#A_ProCode').select();//工序号下拉初始化
            $('#A_Record').select({
                type: 'default',
                value: 'R_Record',
                text: 'R_Record',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetRecordList',
                // 访问数据接口参数
                param: {}
            }).on('change', function () {
                var code = $(this).selectGet();
                $("#A_ProCode").selectRefresh({
                    type: 'default',
                    value: 'P_ProNo',
                    text: 'P_ProNo',
                    // 展开最大高度
                    maxHeight: 200,
                    // 是否允许搜索
                    allowSearch: true,
                    // 访问数据接口地址
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetProceEntity',
                    // 访问数据接口参数
                    param: { code: code }
                });
            });


            $('#A_F_EnCode').select({
                type: 'default',
                value: 'F_EnCode',
                text: 'F_EnCode',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetUserList',
                // 访问数据接口参数
                param: {}
            });
            $('#A_ClassCode').select({
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
            //生产订单号校验
            $('#A_OrderNo').on('blur', function () {
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
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_Arrange/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                    $('#A_DateTime').val(data.time);  //给时间框赋值
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
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_Arrange/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
