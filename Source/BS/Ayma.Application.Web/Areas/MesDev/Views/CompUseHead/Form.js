/* * 创建人：超级管理员
 * 日  期：2019-07-04 16:14
 * 描  述：强制使用记录单据
 */
var acceptClick;
//var refreshGirdData;//表格商品添加
//var RemoveGridData;//移除表格
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');//上一级formId
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
            $('#C_Status').DataItemSelect({ code: 'CompUserStatus' });
            $('#C_WorkShop').select({
                type: 'default',
                value: 'W_Code',
                text: 'W_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetWorkShopList',
                // 访问数据接口参数
                param: {}
            });
            var orderNo = "";
            if (!!keyValue) {//根据主键获取生产订单号
                $.ajax({
                    url: top.$.rootUrl + '/MesDev/MaterInBill/GetOrderNoBy?keyValue=' + keyValue,
                    type: "GET",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        orderNo = data.info;
                    }
                });
            }
            //生产订单号
            $('#C_OrderNo').select({
                type: 'default',
                value: 'P_OrderNo',
                text: 'P_OrderNo',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderList?orderNo=' + orderNo,
                // 访问数据接口参数
                param: {}
            });
            var dfop = {
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            }
            $("#C_StockName").select(dfop).on('change', function () {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#C_StockCode").val(entity.S_Code);
                    }
                });
            });
            //添加商品
            $("#am_add").on("click", function () {
                var stockCode = $("#C_StockCode").val();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsListIndexForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/CompUseHead/GoodsListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_CompUseDetail').jfGrid({
                headData: [
                    {
                        label: '主键', name: 'ID', width: 160, align: 'left', hidden: true
                    },
                   
                    {
                        label: '物料编码', name: 'C_GoodsCode', width: 100, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'C_GoodsName', width: 160, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'C_Unit', width: 70, align: 'left', editType: 'label'
                    },
                    
                    {
                        label: '批次', name: 'C_Batch', width: 80, align: 'left', editType: 'label'
                    },
                    {
                        label: '价格', name: 'C_Price', width: 100, align: 'left', editType: 'label'
                    },
                    {
                        label: '库存', name: 'Qty', width: 100, align: 'left', editType: 'label'
                    },
                    {
                        label: '数量', name: 'C_Qty', width: 100, align: 'left', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.C_Qty.toString().replace('.', ''))) { //验证只能为数字
                                    row.C_Qty = 0;
                                }

                            }
                        }
                    },
                    {
                        label: '备注', name: 'C_Remark', width: 150, align: 'left',editType: 'input'
                    },
                ],
                isAutoHeight: true,
                isEidt: true,
                footerrow: true,
                minheight: 330,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/CompUseHead/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_CompUseDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            } else {
                $("#C_Status").selectSet(1);//新增时默认为单据生成
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_CompUseDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_CompUseHead"]').GetFormData());
        postData.strmes_CompUseDetailList = JSON.stringify($('#Mes_CompUseDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/CompUseHead/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    top.refreshGirdData = function (data, row) {
        var rows = $('#Mes_CompUseDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].C_GoodsCode == row.g_code && rows[k].C_Batch == row.batch) {
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
                        if (rows[j].C_GoodsCode == data[i].g_code && rows[j].C_Batch == data[i].batch) {
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
            return item["C_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    top.RemoveGridData = function (row) {
        var rows = $('#Mes_CompUseDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["C_GoodsCode"] == row["g_code"] && rows[i]["C_Batch"] == row["batch"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_CompUseDetail').jfGridGet('rowdatas');
    }
    page.init();
}
