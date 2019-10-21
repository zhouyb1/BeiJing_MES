/* * 创建人：超级管理员
 * 日  期：2019-03-15 16:11
 * 描  述：线边仓退料到仓库
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
            // 时间搜索框
            $('#datesearch').amdate({
                dfdata: [
                    { name: '今天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 500);
            $('#B_StockName').select({
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
            $('#B_StockToName').select({
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
            $('#B_Status').DataItemSelect({ code: 'MaterInStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'BackIndexform',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/BackStockManager/Form?formId=BackIndexform',
                    width: 800,
                    height: 600,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('B_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'BackIndexform',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/BackStockManager/Form?keyValue=' + keyValue + '&formId=BackIndexform'+'&status='+statu,
                        width: 800,
                        height: 600,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });

            //审核单据
            $('#am_auditing').on('click', function() {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $('#girdtable').jfGridValue('B_Status');
                if (ayma.checkrow(keyValue)) {
                    if (status != "1") {
                        ayma.alert.error("已审核");
                        return false;
                    }
                    if (ayma.checkrow(keyValue)) {
                        ayma.layerConfirm('是否确认审核该单据！', function (res) {
                            if (res) {
                                ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_BackStockHead', field: 'B_Status' }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                }
            });
            //提交单据
            $('#am_post').on('click', function() {
                var orderNo = $('#girdtable').jfGridValue('B_BackStockNo');
                var status = $('#girdtable').jfGridValue('B_Status');
                if (ayma.checkrow(orderNo)) {
                    if (status == "1") {
                        ayma.alert.error("未审核");
                        return false;
                    }
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_BackStock_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("B_BackStockNo");
                var status = $("#girdtable").jfGridValue("B_Status");
                if (ayma.checkrow(orderNo)) {
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_BackStock_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('B_BackStockNo');
                if (ayma.checkrow(keyValue,true)) {
                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '退库单打印',
                        url: top.$.rootUrl + '/MesDev/BackStockManager/PrintReport?keyValue=' + keyValue + "&report=BackStockReport&data=BackStock",
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
            $('#am_printview').on('click', function() {
                var keyValue = $('#girdtable').jfGridValue('B_BackStockNo');
                if (ayma.checkrow(keyValue)) {

                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '退库单打印',
                        url: top.$.rootUrl + '/MesDev/BackStockManager/PrintViewReport?keyValue=' + keyValue + "&report=BackStockReport",
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });

        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/BackStockManager/GetPageList',
                headData: [
                    {
                        label: "状态", name: "B_Status", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'RequistStatus',
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
                        label: "单据类型", name: "B_Kind", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'BackToStockKind',
                                callback: function (_data) {
                                    if (value == 0) {
                                        callback("<span class='label label-default'>" + _data.text + "</span>");
                                    } else {
                                        callback("<span class='label label-danger'>" + _data.text + "</span>");
                                    }
                                }
                            });
                        }
                    },
                    { label: "退仓库单号", name: "B_BackStockNo", width: 160, align: "left"},
                    { label: "仓库编码", name: "B_StockCode", width: 160, align: "left"},
                    { label: "仓库名称", name: "B_StockName", width: 160, align: "left"},
                    { label: "退库仓库编码", name: "B_StockToCode", width: 160, align: "left"},
                    { label: "退库仓库名称", name: "B_StockToName", width: 160, align: "left"},
                    { label: "备注", name: "B_Remark", width: 160, align: "left"},
                    { label: "添加人", name: "B_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "B_CreateDate", width: 160, align: "left"}
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
