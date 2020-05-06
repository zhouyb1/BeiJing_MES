/* * 创建人：超级管理员
 * 日  期：2019-03-20 09:36
 * 描  述：物料转换对应表
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
            //物料名称
            $("#C_Name").select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetGoodsList',
                // 访问数据接口参数
            }).bind("change", function () {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByNameGetGoodsEntity',
                    data: { name: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#C_Code").val(entity.G_Code);
                    }
                });
            });
            //转换后物料名称
            $("#C_SecName").select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetGoodsList',
                // 访问数据接口参数
            }).bind("change", function () {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByNameGetGoodsEntity',
                    data: { name: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#C_SecCode").val(entity.G_Code);
                    }
                });
            });
            //工序名称
            $("#C_ProName").select({
                type: 'default',
                value: 'P_ProName',
                text: 'P_ProName',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProceList',
                // 访问数据接口参数
            }).bind("change", function () {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByGetProceEntity',
                    data: {code: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#C_ProNo").val(entity.P_ProNo);
                    }
                });
            });
           
            //最大转化率 不小于0
            $("#C_Max").on('blur', function () {
                var max = $.trim($(this).val()); //去除空格
                if (max != undefined && max != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(max.toString().replace('.', ''))) {
                        ayma.alert.error("最大转化率必须是非负数.");
                        $("#C_Max").val(0);
                    }

                }
            });
            //最小转化率 不小于0
            $("#C_Min").on('blur', function () {
                var min = $.trim($(this).val()); //去除空格
                if (min != undefined && min != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(min.toString().replace('.', ''))) {
                        ayma.alert.error("最小转化率必须是非负数.");
                        $("#C_Min").val(0);
                    }

                }
            });
            ////编码重复验证
            //$("#C_SecCode").on('blur', function () {
            //    var code = $.trim($(this).val()); //去除空格
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/IsCode',
            //        data: { tables: "Mes_Convert", field: "C_SecCode", code: code },
            //        success: function (data) {
            //            var isOk = JSON.parse(data).data;
            //            if (isOk) {
            //                ayma.alert.error("编码重复");
            //            } 
            //        }
            //    });
            //});
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Convert/GetFormData?keyValue=' + keyValue, function (data) {
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
        //最大转化率要大于最小转化率
        if ($("#C_Min").val() > $("#C_Max").val()) {
            ayma.alert.error("最大转化率要大于最小转化率.");
          
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/Convert/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
