﻿/* * 创建人：超级管理员
 * 日  期：2019-03-13 11:57
 * 描  述：出库单制作
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
           
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 450);
            $('#M_GoodsName').select({
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
                param: {}
            });
            $("#O_StockName").select({
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
                param: {}
            });
            $("#O_WorkShop").select({
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
            $('#O_Status').DataItemSelect({ code: 'ProOutStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            $("#O_Kind").DataItemSelect({ code: "O_Kind" });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'MaterForm',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/OutWorkShopManager/Form?formId=MaterForm',
                    width: 900,
                    height: 700,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('O_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterForm',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/OutWorkShopManager/Form?status=' + statu + '&keyValue=' + keyValue + '&formId=MaterForm',
                        width: 900,
                        height: 700,
                        maxmin: true,
                        btn: statu == 2 ? null : ['确认', '关闭'],
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 双击编辑
            $('#girdtable').on('dblclick', function() {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('O_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterForm',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/OutWorkShopManager/Form?status=' + statu + '&keyValue=' + keyValue + '&formId=MaterForm',
                        width: 900,
                        height: 700,
                        maxmin: true,
                        btn: statu == 2 ? null : ['确认', '关闭'],
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除单据
            $('#am_delete').on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OutNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("O_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OutWorkShop_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $('#am_submit').on('click', function() {
                var orderNo = $("#girdtable").jfGridValue("O_OutNo");
                var status = $("#girdtable").jfGridValue("O_Status");
                if (status == "1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function(res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OutWorkShop_Post', type: 1 }, function() {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("O_Status");
                if (status != "1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_OutWorkShopHead', field: 'O_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OutNo');
                if (ayma.checkrow(keyValue,true)) {
                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '出库单打印',
                        url: top.$.rootUrl + '/MesDev/OutWorkShopManager/PrintReport?keyValue=' + keyValue + "&report=OutWorkShopReport&data=OutWorkShop",
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 预览打印
            $('#am_printview').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OutNo');
                if (ayma.checkrow(keyValue)) {

                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '出库单打印',
                        url: top.$.rootUrl + '/MesDev/OutWorkShopManager/PrintViewReport?keyValue=' + keyValue + "&report=OutWorkShopReport",
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });


                } else {
                    ayma.alert.error("请选择要打印的单据！");
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/OutWorkShopManager/GetPageList',
                headData: [
                    {
                        label: "状态", name: "O_Status", width: 90, align: "center",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'ProOutStatus',
                                callback: function (_data) {
                                    if (value == 1) {
                                        callback("<span class='label label-default'>" + _data.text + "</span>");
                                    } else if (value == 2) {
                                        callback("<span class='label label-info'>" + _data.text + "</span>");
                                    } else if (value == 3) {
                                        callback("<span class='label label-success'>" + _data.text + "</span>");
                                    } else {
                                        callback("<span class='label label-danger'>" + _data.text + "</span>");
                                    }
                                }
                            });
                        }
                    },
                     {
                         label: "出库类型", name: "O_Kind", width: 100, align: "center",
                         formatterAsync: function (callback, value, row) {

                             ayma.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'O_Kind',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                         }
                     },
                    { label: "单据编号", name: "O_OutNo", width: 130, align: "center" },
                    { label: "仓库编码", name: "O_StockCode", width: 80, align: "center" },
                    { label: "仓库名称", name: "O_StockName", width: 100, align: "center" },
                    { label: "调拨车间", name: "O_WorkShopName", width: 130, align: "center" },
                    {
                        label: "单据时间", name: "O_OrderDate", width: 100, align: "center", sort: true,
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "创建时间", name: "O_CreateDate", width: 130, align: "center", sort: true },
                    { label: "添加人", name: "O_CreateBy", width: 90, align: "center" },
                    { label: "备注", name: "O_Remark", width: 130, align: "center" }
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'O_CreateDate',
                sord: 'desc'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            param.OrderDate_S = $("#OrderDate_S").val();
            param.OrderDate_E = $("#OrderDate_E").val();
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
