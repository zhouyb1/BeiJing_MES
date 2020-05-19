/* * 创建人：超级管理员
 * 日  期：2019-03-13 11:57
 * 描  述：出库单制作
 */
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var stockCode;
var parentFormId = request('formId');
var acceptClick;
var keyValue = request('keyValue');
var status = request('status');
var tmp = new Map();
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
            if (status==2) {
                $('#am_add').attr('disabled', true);
                $('#I_WorkShop').attr('disabled', true);
                $('#I_StockCode').attr('disabled', true);
                $('#I_Remark').attr('disabled', true);
            }

            //绑定线边仓仓库
            var dfop = {
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            };
            //入往日耗库
            $('#I_StockCode').select(dfop);
          
            //原日耗库
            $('#I_WorkShop').select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            }).on('change', function () {
                     if (status == "" || status == 1) {
                    $('#Mes_InWorkShopDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
            });

            $('#Mes_InWorkShopDetail').jfGrid({
                headData: [
                    { label: "物料名称", name: "I_GoodsName", width: 130, align: "center" },
                    { label: "物料编码", name: "I_GoodsCode", width: 130, align: "center" },
                    { label: "单价", name: "I_Price", width: 60, align: "center"},
                    { label: "库存", name: "stockQty", width: 60, align: "center", hidden: keyValue == "" ? false : true },
                    {
                        label: "数量", name: "I_Qty", width: 90, align: "center", statistics: true, editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.I_Qty.toString().replace('.', ''))) { //验证只能为数字
                                    row.I_Qty = 0;
                                }
                                if (row.I_Qty > row.stockQty) {
                                    ayma.alert.error("数量不能大于库存");
                                    row.I_Qty = 0;
                                }
                            }
                        }
                    }, {
                        label: "金额", name: "金额", width: 80, align: "left", formatter: function (value, row, dfop) {
                            if (row.I_Qty == "" || row.I_Qty == null || row.I_Qty == undefined) {
                                return row.金额 = 0;
                            }
                            else {
                                return row.金额 = row.I_Price * row.I_Qty;
                            }
                        }, statistics: true
                    },
                    {
                        label: "批次", name: "I_Batch", width: 90, align: "center", editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.I_Batch.toString().replace('.', ''))) { //验证只能为数字
                                    row.I_Batch = 0;
                                }
                                
                            }
                        }
                    },
                   { label: "单位", name: "I_Unit", width: 60, align: "left" }

                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 330,
                isEidt: status == 1 || status == "" ? true : false,
                isMultiselect: status == 1 || status == "" ? true : false,
                height: 300,
                inputCount: 2,
                isStatistics: true
            });
            //添加物料
            $('#am_add').on('click', function () {
                if ($('#I_WorkShop').selectGet() == "") {
                    ayma.alert.error("请选择原日耗仓！");
                    return false;
                }
                ayma.layerForm({
                    id: 'MaterListForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/InWorkShopManager/MaterListIndex?formId=' + parentFormId + '&stock=' + $('#I_WorkShop').selectGet(),
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: ['关闭'],
                    callback: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/InWorkShopManager/GetFormData?keyValue=' + keyValue+'&state=1', function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_InWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_InWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');

        if (data.length==0||data[0].I_GoodsCode == undefined || data[0].I_GoodsCode == "") {
            ayma.alert.error('请添加物料');
            return false;
        }
        if ($('#I_StockCode').selectGet()==$('#I_WorkShop').selectGet()) {
            ayma.alert.error('不能指定同一个日耗仓！');
            return false;
        }
        for (var i = 0; i < data.length; i++) {
            data[i].I_Price = null;
        }
        var postData = {};
        var head = $('[data-table="Mes_InWorkShopHead"]').GetFormData();
        head.I_StockName = $("#I_StockCode").selectGetText();
        postData.strEntity = JSON.stringify(head);
        postData.strmes_InWorkShopDetailList = JSON.stringify(data);
        $.SaveForm(top.$.rootUrl + '/MesDev/InWorkShopManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    top.NewGirdData = function () {
        return $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');
    }
    //表格商品添加
    top.refreshGirdData = function (data, row) {
        var rows = $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].I_GoodsCode == row.o_secgoodscode) {
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
                        if (rows[j].I_GoodsCode == data[i].o_secgoodscode) {
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
            return item["I_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    top.RemoveGridData = function (row) {
        var rows = $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["I_GoodsCode"] == row["o_secgoodscode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
  
    page.init();
}
