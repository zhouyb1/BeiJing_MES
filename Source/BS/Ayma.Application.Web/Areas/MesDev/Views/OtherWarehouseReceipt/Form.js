/* * 创建人：超级管理员
 * 日  期：2019-11-07 13:51
 * 描  述：其它入库单
 */
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var acceptClick;
var keyValue = request('keyValue');
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');//上一级formId
var status = request('status');
var state = request('state');
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
            if (status == 2) {
                $('#O_StockName').attr('readonly', true);
                $('#am_add').attr('disabled', true);
                $('#O_Remark').attr('disabled', true);
                $('#O_StockName').attr('disabled', true);
            }
            if (status == 3) {
                $('#am_add').css('display', 'none')
                $('#O_Remark').attr('disabled', true);
                $('#O_StockName').attr('disabled', true);
            }
            //绑定仓库
            var dfop = {
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetOriginalStockList',
                // 访问数据接口参数
                param: {}
            };
            //绑定仓库
            $('#O_StockName').select(dfop).on('change', function () {
                if (status == 1) {
                    $('#Mes_OtherInDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#O_StockCode").val(entity.S_Code);
                    }
                });
            });
            //添加商品
            $("#am_add").on("click", function () {
                var code = $('#M_SupplyCode').selectGet();
                    ayma.layerForm({
                        id: 'GoodsListIndexForm',
                        title: '添加物料',
                        url: top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/GoodsListIndex?formId=' + parentFormId,
                        width: 750,
                        height: 600,
                        maxmin: true,
                        btn: ['关闭'],
                        callBack: function (id, index) {
                            return top[id].closeWindow();
                        }
                    });
                    return true;             
            });
            //单据状态
            $("#O_Status").DataItemSelect({ code: 'MaterInStatus' });
            $('#Mes_OtherInDetail').jfGrid({
                headData: [
                     {
                         label: 'ID', name: 'ID', width: 160, align: 'left', editType: 'label', hidden: true
                     },

                    {
                        label: '物料编码', name: 'O_GoodsCode', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'O_GoodsName', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'O_Unit', width: 80, align: 'left', editType: 'label'
                    },
                     {
                         label: '数量', name: 'O_Qty', width: 80, align: 'left', editType: 'input',
                         editOp: {
                             callback: function (rownum, row) {
                                 if (/\D/.test(row.O_Qty.toString().replace('.', ''))) { //验证只能为数字
                                     row.O_Qty = 0;
                                 }
                                 row.O_Qty2 = (row.O_Qty / row.O_UnitQty).toFixed(6) / 1;
                             }
                         }
                     },
                     {
                         label: '包装数量', name: 'O_Qty2', width: 80, align: 'left', editType: 'input',
                         editOp: {
                             callback: function (rownum, row) {
                                 if (/\D/.test(row.O_Qty2.toString().replace('.', ''))) { //验证只能为数字
                                     row.O_Qty2 = 0;
                                 }
                                 row.O_Qty = (row.O_Qty2 * row.O_UnitQty).toFixed(6) / 1;
                             }
                         }
                     },
                    {
                        label: '包装规格', name: 'O_UnitQty', width: 80, align: 'left', editType: 'select', editOp: {
                            width: 400,
                            height: 400,
                            colData: [
                               { label: '物料名称', name: 'S_GoodsName', width: 100, align: 'left' },
                               { label: '包装数', name: 'S_UnitQty', width: 100, align: 'left', },
                            ],
                            url: top.$.rootUrl + '/MesDev/Tools/ByGoodsCodeGetUnit',
                            param: { code: "1" },
                            callback: function (selectdata, rownum, row) {
                                if (row.O_GoodsName == selectdata.S_GoodsName) {
                                    row.O_UnitQty = selectdata.S_UnitQty;
                                    ayma.alert.success("物料【" + row.O_GoodsName + "】的包装规格更改成功为【" + row.O_UnitQty + "】！");
                                } else {
                                    ayma.alert.error("您需要更改包装规格的物料是【" + row.O_GoodsName + "】物料名称不符,请重新选择！");
                                    ayma.layer.error("")
                                }
                            }
                        }
                    },
                     {
                         label: '价格', name: 'O_Price', width: 80, align: 'left', editType: 'label',
                         editOp: {
                             callback: function (rownum, row) {
                                 if (/\D/.test(row.O_Price.toString().replace('.', ''))) { //验证只能为数字
                                     row.O_Price = 0;
                                 }

                             }
                         }
                     },
                    {
                        label: '包装单位', name: 'O_Unit2', width: 80, align: 'left', editType: 'label'
                    },//最新维护的价格
                    {
                        label: '批次', name: 'O_Batch', width: 100, align: 'left', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.O_Batch.toString().replace('.', ''))) { //验证只能为数字
                                    row.O_Batch = 0;
                                }

                            }
                        }
                    },
                    {
                        label: '备注', name: 'O_Remark', width: 160, align: 'left', editType: 'input'
                    },
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 320,
                isEidt: status == 1 || status == "" ? true : false,
                isMultiselect: status == 1 || status == "" ? true : false,
                height: 300,
                inputCount: 3
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/GetFormData?keyValue=' + keyValue+'&state='+state, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_OtherInDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            } else {
                $("#O_Status").selectSet(1);//新增时默认为单据生成
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_OtherInDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_OtherInDetail').jfGridGet('rowdatas');
        if (data.length == 0 || data[0].O_GoodsCode == null) {
            ayma.alert.error("请添加物料！");
            return false;
        }
        for (var i = 0; i < data.length; i++) {
            data[i].O_Price = null;
        }
        var postData = {};
        var obj = $('[data-table="Mes_OtherInHead"]').GetFormData();
        postData.strEntity = JSON.stringify(obj);
        postData.strmes_MaterOtherInDetailList = JSON.stringify($('#Mes_OtherInDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_OtherInDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].O_GoodsCode == row.G_Code) {
                        flagRow = false;
                    }
                }
                if (flagRow) {
                    rows.push(row);
                }
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    var flag = true;
                    //加个循环判断数组重复
                    for (var j = 0; j < rows.length; j++) {
                        if (rows[j].O_GoodsCode == data[i].G_Code) {
                            flag = false;
                        }
                    }
                    if (flag) {
                        rows.push(data[i]);
                    }
                }
            }
        }

        //数组过滤
        var filterarray = $.grep(rows, function (item) {
            return item["O_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_OtherInDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["O_GoodsCode"] == row["G_Code"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_OtherInDetail').jfGridGet('rowdatas');
    }
    page.init();
}
