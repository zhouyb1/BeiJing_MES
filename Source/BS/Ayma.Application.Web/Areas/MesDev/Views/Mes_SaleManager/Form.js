/* * 创建人：超级管理员
 * 日  期：2019-11-07 10:38
 * 描  述：原物料(半成品)销售
 */
var acceptClick;
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var parentFormId = request('formId');//父级窗口id
var keyValue = request('keyValue');
var status = request('status');
var tmp = new Map();
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
        },
        bind: function () {
            if (status == 2) {
                $('#S_StockCode').attr('readonly', true);
                $("#S_CostomCode").attr('readonly', true);
            }
            //绑定仓库
            var dfop = {
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetOtherStockList',
                // 访问数据接口参数
                param: {}
            };
            //绑定仓库
            $('#S_StockCode').select(dfop).on('change', function () {
                if (status == 1) {
                    $('#Mes_SaleDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
            });
            //绑定客户
            $("#S_CostomCode").select({
                type: 'default',
                value: 'C_Code',
                text: 'C_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetCustomerList',
                // 访问数据接口参数
                param: {}
            });
            //添加物料
            $("#am_add").on("click", function () {
                var stockCode = $("#S_StockCode").selectGet();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsListIndexForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/Mes_SaleManager/GoodsListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 750,
                    height: 600,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });

            $('#Mes_SaleDetail').jfGrid({
                headData: [
                    {
                        label: '主键', name: 'ID', width: 160, align: 'left', editType: 'label', hidden:true
                    },
                   
                    {
                        label: '物料编码', name: 'S_GoodsCode', width: 100, align: 'center', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'S_GoodsName', width: 160, align: 'center', editType: 'label'
                    },
                    {
                        label: '价格', name: 'S_Price', width: 70, align: 'center', editType: 'label',
                        //editOp: {
                        //    callback: function (rownum, row) {
                        //        if (/\D/.test(row.S_Price.toString().replace('.', ''))) { //验证只能为数字
                        //            row.S_Price = 0;
                        //        }
                        //        if (row.S_Price<0) {
                        //            ayma.alert.error("价格不能为0或负数");
                        //            row.S_Qty = 0;
                        //        }
                        //    }
                        //}
                    },
                    {
                        label: '销售税率', name: 'S_Otax', width: 160, align: 'center', editType: 'label'
                    },
                    {
                        label: '单位', name: 'S_Unit', width: 70, align: 'center', editType: 'label'
                    },
                    {
                        label: '数量', name: 'S_Qty', width: 80, align: 'center', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.S_Qty.toString().replace('.', ''))) { //验证只能为数字
                                    row.S_Qty = 0;
                                }
                                if (row.S_Qty > row.StockQty) {
                                    ayma.alert.error("数量不能大于库存");
                                    row.S_Qty = 0;
                                }
                            }
                        }
                    },
                     {
                         label: '库存', name: 'StockQty', width: 80, align: 'center', editType: 'label',hidden:keyValue==""?false:true
                     },
                    {
                        label: '批次', name: 'S_Batch', width: 160, align: 'center', editType: 'label'
                    },
                    {
                        label: '备注', name: 'S_Remark', width: 200, align: 'left', editType: 'input'
                    },
                ],
                isAutoHeight: true,
                isEidt: true,
                reloadSelected: true,
                isMultiselect: true,
                footerrow: true,
                minheight: 400,
                inputCount:2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_SaleManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_SaleDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        },
        search: function(data) {
            data = data || {};
            $('#Mes_SaleDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_SaleDetail').jfGridGet('rowdatas');
        if (data.length == 0 || data[0].S_GoodsCode == null) {
            ayma.alert.error('请添加物料');
            return false;
        }
        var postData = {};
        var head = $('[data-table="Mes_SaleHead"]').GetFormData();
        head.S_StockName = $('#S_StockCode').selectGetText();
        head.S_CostomName = $('#S_CostomCode').selectGetText();
        postData.strEntity = JSON.stringify(head);
        postData.strmes_SaleDetailEntity = JSON.stringify(data);
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_SaleManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_SaleDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].S_GoodsCode == row.I_GoodsCode & rows[k].S_Batch == row.I_Batch) {
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
                        if (rows[j].S_GoodsCode == data[i].I_GoodsCode && rows[j].S_Batch == data[i].I_Batch) {
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
            return item["S_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_SaleDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["S_GoodsCode"] == row["I_GoodsCode"] && rows[i]["S_Batch"] == row["I_Batch"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_SaleDetail').jfGridGet('rowdatas');
    }
    page.init();
}
