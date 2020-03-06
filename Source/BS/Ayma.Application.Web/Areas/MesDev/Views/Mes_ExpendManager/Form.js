/* * 创建人：超级管理员
 * 日  期：2019-11-08 13:59
 * 描  述：消耗物料
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
        init: function() {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initData();
        },
        bind: function () {
            if (status==2) {
                $('#E_StockCode').attr('readonly', true);
            }
            $('#E_StockCode').DataSourceSelect({ code: 'StockList', value: 's_code', text: 's_name' }).on('change', function() {
                if (status == 1) {
                    $("#Mes_ExpendDetail").jfGridSet('refreshdata', { rowdatas: [] });
                }
            });

            //添加物料
            $("#am_add").on("click", function () {
                var stockCode = $("#E_StockCode").selectGet();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsListIndexForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/Mes_ExpendManager/GoodsListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 750,
                    height: 600,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_ExpendDetail').jfGrid({
                headData: [
                    {
                        label: '主键', name: 'ID', width: 160, align: 'left', editType: 'label', hidden: true
                    },

                    {
                        label: '物料编码', name: 'E_GoodsCode', width: 100, align: 'center', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'E_GoodsName', width: 160, align: 'center', editType: 'label'
                    },
                    {
                        label: '单位', name: 'E_Unit', width: 70, align: 'center', editType: 'label'
                    },
                    {
                        label: '单价', name: 'E_Price', width: 70, align: 'center', editType: 'label'
                    },
                    {
                        label: '数量', name: 'E_Qty', width: 80, align: 'center', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.E_Qty.toString().replace('.', ''))) { //验证只能为数字
                                    row.E_Qty = 0;
                                }
                                if (row.E_Qty > row.StockQty) {
                                    ayma.alert.error("数量不能大于库存");
                                    row.E_Qty = 0;
                                }
                            }
                        }
                    },
                     {
                         label: '库存', name: 'StockQty', width: 80, align: 'center', editType: 'label'
                     },
                    {
                        label: '批次', name: 'E_Batch', width: 160, align: 'center', editType: 'label'
                    },
                    {
                        label: '备注', name: 'E_Remark', width: 200, align: 'left', editType: 'label'
                    },
                ],
                isAutoHeight: true,
                isEidt: true,
                reloadSelected: true,
                isMultiselect: true,
                footerrow: true,
                minheight: 400,
                inputCount:1
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_ExpendManager/GetFormData?keyValue=' + keyValue+'&state=1', function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_ExpendDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_ExpendDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var dataList = $('#Mes_ExpendDetail').jfGridGet('rowdatas');
        if (dataList.length == 0 || dataList[0].E_GoodsCode == null) {
            ayma.alert.error('请添加物料');
            return false;
        }
        for (var i = 0; i < dataList.length; i++) {
            dataList[i].E_Price = null;
        }

        var postData = {};
        var head = $('[data-table="Mes_ExpendHead"]').GetFormData();
        head.E_StockName = $('#E_StockCode').selectGetText();
        postData.strEntity = JSON.stringify(head);
        postData.strmes_ExpendDetailEntity = JSON.stringify(dataList);
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_ExpendManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_ExpendDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].E_GoodsCode == row.I_GoodsCode & rows[k].E_Batch == row.I_Batch) {
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
                        if (rows[j].E_GoodsCode == data[i].I_GoodsCode && rows[j].E_Batch == data[i].I_Batch) {
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
            return item["E_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    RemoveGridData = function (row) {
        var rows = $('#Mes_ExpendDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["E_GoodsCode"] == row["I_GoodsCode"] && rows[i]["E_Batch"] == row["I_Batch"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_ExpendDetail').jfGridGet('rowdatas');
    }
    page.init();
}
