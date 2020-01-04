/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.05
 * 描 述：配方管理	
 */
var parentId = request('parentId');
var recordCode = request('recordCode');//工艺代码
var selectedRow = top.BomRecordIndexSelectedRow();
var B_ParentID = request('B_ParentID');
var keyValue = '';
var acceptClick;
var G_Kind;
var bootstrap = function ($, ayma) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {          
            if (parentId != '' && parentId != undefined && parentId!=0) {
                $("#B_RecordCode").attr("disabled", "disabled");
                $("#ProceName").css("display", "block");
                $("#ProceCode").css("display", "block");
                $("#B_ProceName").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                $("#B_Unit").css("display", "none");
                $("#B_Unit1").css("display", "block");
                $("#B_Unit1").attr("value", "g");
                $("#B_Unit").removeAttr("isvalid").removeAttr("checkexpession");
                $("#b_unit").html("单位");
                $("#B_FormulaCode").removeAttr("isvalid").removeAttr("checkexpession");
                $("#div_FormulaCode").html("配方编码");
                $("#B_FormulaName").removeAttr("isvalid").removeAttr("checkexpession");
                $("#div_FormulaName").html("配方名称");
                $("#B_Code").css("display", "none");
                $("#B_Name").css("display", "none");
            } else {
                $("#B_RecordCode").removeAttr("disabled");
                $("#ProceName").css("display", "none");
                $("#ProceCode").css("display", "none");
                $("#B_ProceName").removeAttr("isvalid").removeAttr("checkexpession");
                $("#B_Unit").css("display", "block");
                $("#B_Unit1").css("display", "none");
                $("#B_FormulaCode").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                $("#div_FormulaCode").html("配方编码<font face=\"宋体\">*</font>");
                $("#B_FormulaName").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                $("#div_FormulaName").html("配方名称<font face=\"宋体\">*</font>");
                $("#B_Code").css("display", "block");
                $("#B_Name").css("display", "block ");
            }
            //if (B_ParentID != '' && B_ParentID != undefined) {
            //    $("#B_RecordCode").attr("disabled", "disabled");
            //} else {
            //    $("#B_RecordCode").removeAttr("disabled");
            //}
            //单位
            $("#B_Unit").DataItemSelect({ code: "GoodsUnit" });
            //是否有效
            $('#B_Avail').DataItemSelect({ code: 'YesOrNo' });
            // 上级
            $('#B_ParentID').select({
                url: top.$.rootUrl + '/MesDev/Tools/GetBomRecordTree',
                type: 'tree',
                allowSearch: true,
                maxHeight: 225
            }).selectSet(parentId);
            //上级改变事件
            $('#B_ParentID').bind("change", function() {
                var value = $(this).selectGet();
                if (value != "") {
                    $("#ProceName").css("display", "block");
                    $("#ProceCode").css("display", "block");
                    $("#B_ProceName").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                    $("#B_Unit").css("display", "none");
                    $("#B_Unit1").css("display", "block");
                    $("#B_Unit1").attr("value", "g");
                    $("#B_Unit").removeAttr("isvalid").removeAttr("checkexpession");
                    $("#b_unit").html("单位");
                    $("#B_FormulaCode").removeAttr("isvalid").removeAttr("checkexpession");
                    $("#div_FormulaCode").html("配方编码");
                    $("#B_FormulaName").removeAttr("isvalid").removeAttr("checkexpession");
                    $("#div_FormulaName").html("配方名称");
                    $("#B_Code").css("display", "none");
                    $("#B_Name").css("display", "none");
                }
                else {
                    $("#ProceName").css("display", "none");
                    $("#ProceCode").css("display", "none");
                    $("#B_ProceName").removeAttr("isvalid").removeAttr("checkexpession");
                    $("#B_Unit").css("display", "block");
                    $("#B_Unit1").css("display", "none");
                    $("#B_FormulaCode").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                    $("#div_FormulaCode").html("配方编码<font face=\"宋体\">*</font>");
                    $("#B_FormulaName").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                    $("#div_FormulaName").html("配方名称<font face=\"宋体\">*</font>");
                    $("#B_Code").css("display", "block");
                    $("#B_Name").css("display", "block ");

                }
                if (value != '' && value != undefined) {
                    $.ajax({
                        type: "get",
                        url: top.$.rootUrl + '/MesDev/BomHead/GetBomRecordEntity',
                        data: { keyValue: value },
                        success: function (data) {
                            var entity=JSON.parse(data).data;
                            //工艺代码赋值
                            $("#B_RecordCode").selectSet(entity.B_RecordCode);
                        }
                    });
                    $("#B_RecordCode").attr("disabled", "disabled");
                } else {
                    $("#B_RecordCode").removeAttr("disabled");
                }
            });
            //工艺代码
            $("#B_RecordCode").select({
                type: 'default',
                value: 'R_Record',
                text: 'R_Record',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetRecordList'
               
            }).selectSet(recordCode);
            //物料名称
            $("#B_GoodsName").select({
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
            }).bind("change",function() {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByNameGetGoodsEntity',
                    data: { name: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#B_GoodsCode").val(entity.G_Code);
                        G_Kind = entity.G_Kind;
                        if (G_Kind == 1) {
                            $("#StockName").css("display", "block");
                            $("#StockCode").css("display", "block");
                            $("#B_StockName").attr("isvalid", "yes").attr("checkexpession", "NotNull");
                        }
                        else {
                            $("#StockName").css("display", "none");
                            $("#StockCode").css("display", "none");
                            $("#B_StockName").removeAttr("isvalid").removeAttr("checkexpession");
                        }
                    }
                });

            });
            //仓库名称
            $("#B_StockName").select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
            }).bind("change", function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#B_StockCode").val(entity == null ? "" : entity.S_Code);
                    }
                });
            });
            //工序名称
            $("#B_ProceName").select({
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
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByGetProceEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#B_ProceCode").val(entity == null ? "" : entity.P_ProNo);
                    }
                });
            });
            //数量验证 不小于0
            $("#B_Qty").on('blur', function () {
                var qty = $.trim($(this).val()); //去除空格
                if (qty != undefined && qty != "") {
                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(qty.toString().replace('.', ''))) {
                        ayma.alert.error("数量必须是非负数.");
                        $("#B_Qty").val(0);
                    }

                }
            });
            /*检测重复项*/
            //$('#F_ItemName').on('blur', function () {
            //    $.ExistField(keyValue, 'F_ItemName', top.$.rootUrl + '/AM_SystemModule/DataItem/ExistItemName');
            //});
            //$('#F_ItemCode').on('blur', function () {
            //    $.ExistField(keyValue, 'F_ItemCode', top.$.rootUrl + '/AM_SystemModule/DataItem/ExistItemCode');
            //});
        },
        initData: function () {
            
            if (!!selectedRow) {
                keyValue = selectedRow.ID || '';
                $('#form').SetFormData(selectedRow);

            } else {
                $('#B_Avail').selectSet(1);
            }

        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        var postData = $('#form').GetFormData(keyValue);
        if (postData["B_ParentID"] == '' || postData["B_ParentID"] == '&nbsp;') {
            postData["B_ParentID"] = '0';
        }
        if (postData["B_Unit"] == '' || postData["B_Unit"] == '&nbsp;') {
            postData["B_Unit"] = 'g';
        }
        else if (postData["B_ParentID"] == keyValue) {
            ayma.alert.error('上级不能是自己本身');
            return false;
        }
        $.SaveForm(top.$.rootUrl + '/MesDev/BomHead/SaveBomRecordForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };

    page.init();
}