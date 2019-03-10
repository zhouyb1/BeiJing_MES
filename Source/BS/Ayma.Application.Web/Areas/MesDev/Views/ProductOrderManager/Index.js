/* * 创建人：超级管理员
 * 日  期：2019-03-07 11:05
 * 描  述：生产订单管理
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            page.dbClick();
        },
        bind: function () {
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });

            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/ProductOrderManager/Form?keyValue=' + keyValue,
                        width: 800,
                        height: 600,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //审核
            $('#am_auditing').on('click', function() {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var p_status = $('#girdtable').jfGridValue('P_Status');
                if (p_status=="2") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (p_status=="0") {
                    ayma.alert.error("处于生产计划中");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核单据', function(res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/ProductOrderManager/AuditingBill?keyValue=' + keyValue, { keyValue: keyValue }, function() {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            //统计
            $('#am_sum').on('click', function() {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var p_status = $('#girdtable').jfGridValue('P_Status');
                var orderNo = $('#girdtable').jfGridValue('P_OrderNo');
                var orderDate = $('#girdtable').jfGridValue('P_OrderDate');
                var qty = $('#girdtable').jfGridValue('P_Qty');
                var code = $('#girdtable').jfGridValue('P_GoodsCode');
                if (p_status == "0") {
                    ayma.alert.error("订单处于生成计划中！");
                    return false;
                }
                if (p_status=="1") {
                    ayma.alert.error("请先审核订单！");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    //打开统计页面BomPartSum 
                    ayma.layerForm({
                        id: 'form',
                        title: '配方',
                        url: top.$.rootUrl + '/MesDev/ProductOrderManager/BomPartSum?GoodsCode=' + code+'&qty='+qty+'&orderNo='+orderNo+'&orderDate='+orderDate,
                        width: 800,
                        height: 600,
                        maxmin: true,
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/ProductOrderManager/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/ProductOrderManager/GetPageList',
                headData: [
                    { label: "生产订单号", name: "P_OrderNo", width: 160, align: "left" },
                    { label: "订单时间", name: "P_OrderDate", width: 160, align: "left" },
                    { label: "使用时间", name: "P_UseDate", width: 160, align: "left" },
                    { label: "物料编码", name: "P_GoodsCode", width: 160, align: "left" },
                    { label: "数量", name: "P_Qty", width: 160, align: "left" },
                    { label: "单位", name: "P_Unit", width: 160, align: "left" },
                    { label: "物料名称", name: "P_GoodsName", width: 160, align: "left" },
                    {
                        label: "状态", name: "P_Status", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'ProductOrderStatus',
                                callback: function(_data) {
                                    if (value == 0) {
                                        callback("<span class='label label-default'>" + _data.text + "</span>");
                                    } else if (value == 1) {
                                        callback("<span class='label label-info'>" + _data.text + "</span>");
                                    } else {
                                        callback("<span class='label label-success'>" + _data.text + "</span>");
                                    }
                                }
                            });
                        }
                    }
                ],
                mainId: 'ID',
                reloadSelected: true,
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        },

        dbClick: function () {
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/ProductOrderManager/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }

            });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
