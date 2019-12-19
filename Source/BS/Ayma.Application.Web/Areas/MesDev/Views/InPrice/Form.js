/* * 创建人：超级管理员
 * 日  期：2019-08-06 10:54
 * 描  述：原物料入库价格表
 */
var GoodsCodestate;//供应商编码重复状态
var acceptClick;
var keyValue = request('keyValue');
var SupplyCodestate;
//批次时间
var batch = new Date();
var G_SupplyName = "";
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
            //编辑禁用
            if (keyValue) {
                $("#P_SupplyName").attr('disabled', 'disabled'); 
                $("#P_GoodsName").attr('disabled', 'disabled');    
            }
            if (!keyValue) {
                $("#InPrice").css('display', 'none');
            }
            //$('#P_GoodsName').select();
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
                    async: false,
                    url: top.$.rootUrl + '/MesDev/Tools/ByNameGetSupplyEntity',
                    data: { name: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#P_SupplyCode").val(entity.S_Code);
                        //$("#P_GoodsCode").val("");
                    }
                });
                //$.ajax({
                //    type: "get",
                //    url: top.$.rootUrl + '/MesDev/Tools/GetGoodsList',//先查所有的物料
                //    data: { G_SupplyName: $('#P_SupplyName').selectGet() },
                //    dataType: "json",
                //    success: function (res) {
                //        G_SupplyName = $('#P_SupplyName').selectGet();
                //        $('#P_GoodsName').selectRefresh({
                //            url: top.$.rootUrl + '/MesDev/Tools/GetGoodsListBySupplyName?G_SupplyName=' + G_SupplyName,//根据供应商查物料
                //            type: 'default',
                //            value: 'G_Name',
                //            text: "G_Name",
                //            maxHeight: 225,
                //            param: {}
                //        });
                //    }
                //});
            });
            //$('#P_GoodsName').on('change', function () {
            //    var name = $(this).selectGet();
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/ByNameGetGoodsEntity',
            //        data: { name: name },
            //        success: function (data) {
            //            var entity = JSON.parse(data).data;
            //            $("#P_GoodsCode").val(entity.G_Code);
            //        }
            //    });
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
                        $("#P_GoodsCode").val(entity.G_Code);
                    }
                });

            });
            ////物料价格 不小于0
            //$("#P_InPrice").on('blur', function () {
            //    var period = $.trim($(this).val()); //去除空格
            //    if (period != undefined && period != "") {
            //        if (!/^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(period.toString().replace('.', ''))) {
            //            ayma.alert.error("物料价格必须是非负数.");
            //            $("#P_InPrice").val(1);
            //        }
            //    }
            //});
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
            if (!$('body').Validform()) {
                return false;
            }
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
        //不含税 不小于0
            var period = $.trim($("#P_InPrice").val()); //去除空格
            if (period != undefined && period != "") {
                if (!/^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(period.toString())) {
                    ayma.alert.error("不含税价格必须是非负数.");
                    $("#P_InPrice").val(1);
                    return false;
                }
                if (period == 0)
                {
                    ayma.alert.error("不含税价格不能为0.");
                    $("#P_InPrice").val(1);
                    return false;
                }
            }
        //含税价格 不小于0
            var P_TaxPrice = $.trim($("#P_TaxPrice").val()); //去除空格
            if (P_TaxPrice != undefined && P_TaxPrice != "") {
                if (!/^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(P_TaxPrice.toString())) {
                    ayma.alert.error("含税价格必须是非负数.");
                    $("#P_TaxPrice").val(1);
                    return false;
                }
                if (P_TaxPrice == 0) {
                    ayma.alert.error("含税价格不能为0.");
                    $("#P_TaxPrice").val(1);
                    return false;
                }
            }

        //不含税价格 小于 含税价格
            if (parseFloat(period) > parseFloat(P_TaxPrice))
            {
                ayma.alert.error("不含税价格不能大于含税价格.");
                $("#P_InPrice").val(P_TaxPrice);
                return false;
            }
        //税率 不小于0
            var itax = $.trim($("#P_Itax").val()); //去除空格
            if (itax != undefined && itax != "") {
                if (!/^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(itax.toString())) {
                    ayma.alert.error("购进税率必须是非负数.");
                    $("#P_Itax").val(1);
                    return false;
                }
                if (itax == 0) {
                    ayma.alert.error("税率不能为0.");
                    $("#P_Itax").val(1);
                    return false;
                }
            }
        //到期时间 不小于开始时间
            var StartDate = $.trim($("#P_StartDate").val()); //去除空格
            var EndDate = $.trim($("#P_EndDate").val()); //去除空格
            if (StartDate != undefined && StartDate != "" && EndDate != undefined && EndDate != "") {
                if (EndDate.toString("yyyy-mm-dd") <= StartDate.toString("yyyy-mm-dd"))
                {
                    ayma.alert.error("到期时间只能大于开始时间.");
                    return false;
                }
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
