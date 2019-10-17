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
            //供应商名称
            $("#G_SupplyName").select({
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
                        $("#G_SupplyCode").val(entity.S_Code);
                    }
                });
            });
            //商品类型
            $("#G_Kind").DataItemSelect({ code: "GoodsType" }).on('change', function () {
                var value = $(this).selectGet();
                if (value == 3) {
                    $("#div_Erpcode").html("商品erp编码<font face=\"宋体\">*</font>");
                    $("#G_Erpcode").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                    $('#prev_div').css("display", "block");
                    $("#G_Barcode").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                } else {
                    $("#div_Erpcode").html("商品erp编码");
                    $("#G_Erpcode").removeAttr("isvalid").removeAttr("checkexpession");
                    $('#prev_div').css("display", "none");
                    $("#G_Barcode").removeAttr("isvalid").removeAttr("checkexpession");
                }
                if (value == 1) {
                    $("#div_Prepareday").html("备用天数<font face=\"宋体\">*</font>");
                    $("#div_G_Super").html("上限预警数量<font face=\"宋体\">*</font>");
                    $("#div_G_Lower").html("下限预警数量<font face=\"宋体\">*</font>");
                    $("#G_Prepareday").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                    $("#G_Super").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                    $("#G_Lower").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                } else {
                    $("#div_Prepareday").html("备用天数");
                    $("#G_Prepareday").removeAttr("isvalid").removeAttr("checkexpession");
                    $("#div_G_Super").html("上限预警数量");
                    $("#G_Super").removeAttr("isvalid").removeAttr("checkexpession");
                    $("#div_G_Lower").html("下限预警数量");
                    $("#G_Lower").removeAttr("isvalid").removeAttr("checkexpession");
                }
            });
            $('#G_Barcode').select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetBarCodeList',
                // 访问数据接口参数
                param: {}
            });
            ////商品二级分类类型
            //$("#G_TKind").DataItemSelect({ code: "GoodsTypeT" });

            $("#G_TKind").select({
                type: 'default',
                value: 'G_Code',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetGoodsKind',
                // 访问数据接口参数
                param: {}
            }); 
            ////班组的分类
            $("#G_TeamCode").select({
                type: 'default',
                value: 'T_Code',
                text: 'T_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetTeamList',
                // 访问数据接口参数
                param: {}
            });
            ////仓库的分类
            $("#G_StockCode").select({
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
            //商品单位
            $("#G_Unit").DataItemSelect({ code: "GoodsUnit" });
            $("#G_Unit2").DataItemSelect({ code: "GoodsUnit" });
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
                    data: { tables: "Mes_Goods", field: "G_Code", code: code, keyValue: keyValue },
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
                    data: { tables: "Mes_Goods", field: "G_Name", names: name, keyValue: keyValue },
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
            //保质时间验证 不小于0
            $("#G_Period").on('blur', function () {
                var period = $.trim($(this).val()); //去除空格
                if (period != undefined && period != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(period.toString().replace('.', ''))) {
                        ayma.alert.error("保质时间必须是非负数.");
                        $("#G_Period").val(0);
                    }

                }
            });
            //下限预警验证 不小于0
            $("#G_Lower").on('blur', function () {
                var period = $.trim($(this).val()); //去除空格
                if (period != undefined && period != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(period.toString().replace('.', ''))) {
                        ayma.alert.error("下限预警必须是非负数.");
                        $("#G_Lower").val(0);
                    }

                }
            });
            //价格验证 不小于0
            $("#G_Price").on('blur', function () {
                var price = $.trim($(this).val()); //去除空格
                if (price != undefined && price != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(price.toString().replace('.', ''))) {
                        ayma.alert.error("价格必须是非负数.");
                        $("#G_Price").val(0);
                    }

                }
            });
            //备用天数验证 不小于0
            $("#G_Prepareday").on('blur', function () {
                var prepareday = $.trim($(this).val()); //去除空格
                if (prepareday != undefined && prepareday != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(prepareday.toString().replace('.', ''))) {
                        ayma.alert.error("备用天数必须是非负数.");
                        $("#G_Prepareday").val(0);
                    }

                }
            });
            //上限预警数量验证 不小于0
            $("#G_Super").on('blur', function () {
                var gsuper = $.trim($(this).val()); //去除空格
                if (gsuper != undefined && gsuper != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(gsuper.toString().replace('.', ''))) {
                        ayma.alert.error("上限预警数量必须是非负数.");
                        $("#G_Super").val(0);
                    }

                }
            });
            //下限预警数量验证 不小于0
            $("#G_Lower").on('blur', function () {
                var lower = $.trim($(this).val()); //去除空格
                if (lower != undefined && lower != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(lower.toString().replace('.', ''))) {
                        ayma.alert.error("下限预警数量必须是非负数.");
                        $("#G_Lower").val(0);
                    }

                }
            });
            //单位重量验证 不小于0
            $("#G_UnitWeight").on('blur', function () {
                var unitWeight = $.trim($(this).val()); //去除空格
                if (unitWeight != undefined && unitWeight != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(unitWeight.toString().replace('.', ''))) {
                        ayma.alert.error("单位重量必须是非负数.");
                        $("#G_UnitWeight").val(0);
                    }

                }
            });
            //销售税率验证 不小于0
            $("#G_Otax").on('blur', function () {
                var otax = $.trim($(this).val()); //去除空格
                if (otax != undefined && otax != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(otax.toString().replace('.', ''))) {
                        ayma.alert.error("销售税率必须是非负数.");
                        $("#G_Otax").val(0);
                    }

                }
            });
            //购进税率验证 不小于0
            $("#G_Itax").on('blur', function () {
                var itax = $.trim($(this).val()); //去除空格
                if (itax != undefined && itax != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(itax.toString().replace('.', ''))) {
                        ayma.alert.error("购进税率必须是非负数.");
                        $("#G_Itax").val(0);
                    }

                }
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
        var G_Lower = $("#G_Lower").val();
        var gsuper = $("#G_Super").val();
        if (gsuper != undefined && gsuper != "") {

            if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(gsuper.toString().replace('.', ''))) {
                ayma.alert.error("上限预警数量必须是非负数.");
                $("#G_Super").val(0);
                return false;
            }
        }
        if (G_Lower >= gsuper) {        
                ayma.alert.error("下限预警数量必须小于上限预警数量.");
                $("#G_Lower").addClass("am-field-error");
                return false;
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
