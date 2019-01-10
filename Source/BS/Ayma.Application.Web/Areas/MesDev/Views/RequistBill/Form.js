/* * 创建人：超级管理员
 * 日  期：2019-01-09 10:20
 * 描  述：调拨单制作
 */
var acceptClick;
var refreshGirdData;
var RemoveGridData;
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');
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
            //原仓库列表
            var dfop = {
                type: 'default',
                value: 'S_Code',
                text: 'S_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            }
            $("#R_StockCode").select(dfop).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#R_StockName").val(entity.S_Name);
                    }
                });
            });
            //调拨仓库列表
            var dfop1 = {
                type: 'default',
                value: 'S_Code',
                text: 'S_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            }
            $("#R_StockToCode").select(dfop1).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#R_StockToName").val(entity.S_Name);
                    }
                });
            });
            //添加商品
            $("#am_add").on("click", function () {
                var stockCode = $('#R_StockCode').selectGet();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/RequistBill/GoodsListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_RequistDetail').jfGrid({
                headData: [
                    {
                        label: '物料编码', name: 'R_GoodsCode', width: 140, align: 'left', editType: 'label', frozen: true
                    },
                    {
                        label: '物料名称', name: 'R_GoodsName', width: 140, align: 'left', editType: 'label', frozen: true
                    },
                    {
                        label: '单位', name: 'R_Unit', width: 60, align: 'left', editType: 'label'
                    },
                    {
                        label: '数量', name: 'R_Qty', width: 60, align: 'left', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.R_Qty.toString().replace('.', ''))) { //验证只能为数字
                                    row.R_Qty = 0;
                                }
                                if (row.R_Qty < 0 || ('' + row.R_Qty + '').substring(0, 1) == '0') {
                                    row.R_Qty = 0;
                                }
                                if (row.R_Qty > row.R_SQty) {
                                    ayma.alert.error("填入数量不能大于库存数量");
                                    row.R_Qty = 0;                                
                                }
                            }
                        }
                    },
                    {
                        label: '库存数量', name: 'R_SQty', width: 60, align: 'left', editType: 'label'
                    },
                    {
                        label: '批次', name: 'R_Batch', width: 100, align: 'left', editType: 'label'
                    },
                    {
                        label: '备注', name: 'R_Remark', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: 'ID', name: 'ID', width: 160, align: 'left', editType: 'label',hidden:true
                    }

                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/RequistBill/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_RequistDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_RequistDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var rowlist = $('#Mes_RequistDetail').jfGridGet('rowdatas');
        if ($("#R_StockCode").selectGet() == $("#R_StockToCode").selectGet()) {
            ayma.alert.error("原仓库与调拨仓库不能一致");
            return false;
        }
        if (rowlist[0]["ID"] == undefined) {
            ayma.alert.error("请选择商品");
            return false;
        }
        for (var i = 0; i < rowlist.length; i++) {
            if (rowlist[i]["R_Qty"] == "" || rowlist[i]["R_Qty"]=="0") {
                ayma.alert.error("数量不能为空");
                return false;
            }
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_RequistHead"]').GetFormData());
        postData.strmes_RequistDetailList = JSON.stringify($('#Mes_RequistDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/RequistBill/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_RequistDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                rows.push(row);
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    rows.push(data[i]);
                }
            }
        }
        //数组过滤
        var filterarray = $.grep(rows, function (item) {
            return item["R_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_RequistDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["R_GoodsCode"] == row["g_code"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_RequistDetail').jfGridGet('rowdatas');
    }
    page.init();
}
