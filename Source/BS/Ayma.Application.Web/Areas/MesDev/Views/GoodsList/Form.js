/* * 创建人：超级管理员
 * 日  期：2019-01-07 13:55
 * 描  述：物料列表
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
            //商品类型
            $("#G_Kind").DataItemSelect({ code: "GoodsType" }).on('change', function () {
                var value = $(this).selectGet();
                if (value == 3) {
                    $("#div_Erpcode").html("商品erp编码<font face=\"宋体\">*</font>");
                    $("#G_Erpcode").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                } else {
                    $("#div_Erpcode").html("商品erp编码");
                    $("#G_Erpcode").removeAttr("isvalid").removeAttr("checkexpession");
                }
            });
            ////商品二级分类类型
            //$("#G_TKind").DataItemSelect({ code: "GoodsTypeT" });

            $("#G_TKind").select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetGoodsKind',
                // 访问数据接口参数
                param: {}
            });
            //商品单位
            $("#G_Unit").DataItemSelect({ code: "GoodsUnit" });
            //是否自制
            $("#G_Self").DataItemSelect({ code: "YesOrNo" });
            //是否在用
            $("#G_Online").DataItemSelect({ code: "YesOrNo" });
            ////供应商列表
            //var dfop = {
            //    type: 'default',
            //    value: 'S_Code',
            //    text: 'S_Code',
            //    // 展开最大高度
            //    maxHeight: 200,
            //    // 是否允许搜索
            //    allowSearch: true,
            //    // 访问数据接口地址
            //    url: top.$.rootUrl + '/MesDev/Tools/GetSupplyList',
            //    // 访问数据接口参数
            //    param: {}
            //}
            //$("#G_SupplyCode").select(dfop).on('change', function () {
            //    var code = $(this).selectGet();
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetSupplyEntity',
            //        data: { code: code },
            //        success: function (data) {
            //            var entity = JSON.parse(data).data;
            //            $("#G_Supply").val(entity.S_Name);
            //        }
            //    });
            //});
            //编码重复验证
            $("#G_Code").on('blur', function () {
                var code = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isCode" title="编码重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsCode',
                    data: { tables: "Mes_Goods", field: "G_Code", code: code },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#G_Code").addClass("am-field-error");
                            $("#G_Code").parent().append(html);
                            ayma.alert.error("编码重复");
                        } else {
                            $("#G_Code").removeClass("am-field-error");
                            $("#isCode").remove();
                        }
                    }
                });
            });
            //名称重复验证
            $("#G_Name").on('blur', function () {
                var name = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isName" title="名称重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsName',
                    data: { tables: "Mes_Goods", field: "G_Name", names: name },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#G_Name").addClass("am-field-error");
                            $("#G_Name").parent().append(html);
                            ayma.alert.error("名称重复");
                        } else {
                            $("#G_Name").removeClass("am-field-error");
                            $("#isName").remove();
                        }
                    }
                });
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/GoodsList/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            } else {
                $("#G_Self").selectSet(1);
                $("#G_Online").selectSet(1);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var gsuper = $("#G_Super").val();
        if (gsuper != undefined && gsuper != "") {
           
            if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(gsuper.toString().replace('.', ''))) {
                ayma.alert.error("上限预警数量必须是非负数.");
                $("#G_Super").val(0);
                return false;
            }
        }
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/GoodsList/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
