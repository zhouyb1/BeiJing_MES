/* * 创建人：超级管理员
 * 日  期：2019-08-06 10:54
 * 描  述：原物料入库价格表
 */
var SupplyCodestate;//供应商编码重复状态
var GoodsCodestate;//供应商编码重复状态
var acceptClick;
var keyValue = request('keyValue');
//批次时间
var batch = new Date();
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
            //供应商名称
            $("#P_SupplyName").select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetEffectSupplyList',
                // 访问数据接口参数
            }).bind("change", function () {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByNameGetSupplyEntity',
                    data: { name: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#P_SupplyCode").val(entity.S_Code);

            
                    }
                });
            });
            //});
            //物料名称
            $("#P_GoodsName").select({
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
                        $("#P_GoodsCode").val(entity.G_Code);
                    }
                });

            });
        },
        initData: function () {
            if (!!keyValue) { 
                $.SetForm(top.$.rootUrl + '/MesDev/InPrice/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            } else {
                $("#P_StartBatch").val(ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, ""));
                $("#P_EndBatch").val(ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, ""));
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        var code = $.trim($("#P_SupplyCode").val()); //去除空格
        var code2 = $.trim($("#P_GoodsCode").val()); //去除空格
        var html = '<div class="am-field-error-info" id="isCode" title="供应商编码重复！"></div>';
        $.ajax({
            type: "get",
            url: top.$.rootUrl + '/MesDev/Tools/IsCodeAndSupplyCode',
            data: { tables: "Mes_InPrice", field: "P_SupplyCode", code: code, field2: "P_GoodsCode", code2: code2, keyValue: keyValue },
            async:false,
            success: function (data) {
                var isOk = JSON.parse(data).data;
                if (isOk) {
                    $("#P_SupplyCode").addClass("am-field-error");
                    $("#P_SupplyCode").parent().append(html);
                    $("#P_GoodsCode").addClass("am-field-error");
                    $("#P_GoodsCode").parent().append(html);
                    ayma.alert.error("编码重复");
                    SupplyCodestate = false;
                    return false
                } else {
                    $("#P_SupplyCode").removeClass("am-field-error");
                    $("#isCode").remove();
                    $("#P_GoodsCode").removeClass("am-field-error");
                    $("#isCode2").remove();
                    SupplyCodestate = true;
                }
            }
        });
        if (SupplyCodestate == false) {
            return false;
        }
            if (!$('body').Validform()) {
                return false;
            }
            var postData = {
                strEntity: JSON.stringify($('body').GetFormData()), strEntity2: JSON.stringify($('body').GetFormData())

            };
            $.SaveForm(top.$.rootUrl + '/MesDev/InPrice/SaveForm?keyValue=' + keyValue, postData, function (res) {
                // 保存成功后才回调
                if (!!callBack) {
                    callBack();
                }
            })
    };
    page.init();
}
