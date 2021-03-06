﻿/* * 创建人：超级管理员
 * 日  期：2019-03-07 11:05
 * 描  述：生产订单管理
 */
var refreshGirdData;
var refreshSubGirdData;//子列表刷新
var $subgridTable;//子列表
var recordEdit;//编辑子列表

var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            //page.dbClick();
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
            }, 220, 350);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });

            // 编辑
            //$('#am_edit').on('click', function () {
            //    var keyValue = $('#girdtable').jfGridValue('ID');
            //    if (ayma.checkrow(keyValue)) {
            //        ayma.layerForm({
            //            id: 'form',
            //            title: '编辑',
            //            url: top.$.rootUrl + '/MesDev/ProductOrderManager/Form?keyValue=' + keyValue,
            //            width: 700,
            //            height: 500,
            //            maxmin: true,
            //            callBack: function (id) {
            //                return top[id].acceptClick(refreshGirdData);
            //            }
            //        });
            //    }
            //});
            //审核
            $('#am_auditing').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var p_status = $('#girdtable').jfGridValue('temp_status');
                if (p_status == "2") {
                    ayma.alert.error("已审核");
                    return false;
                }
                //if (p_status == "0") {
                //    ayma.alert.error("处于生产计划中");
                //    return false;
                //}
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核单据', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/ProductOrderManager/AuditingBill?keyValue=' + keyValue, { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //生成领料单
            $('#am_Col').on('click', function () {
                ayma.layerForm({
                    id: 'PlanForm',
                    title: '生成领料单',
                    url: top.$.rootUrl + '/MesDev/ProductOrderManager/PlanForm',
                    width: 400,
                    height: 300,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            //统计
            $('#am_sum').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $('#girdtable').jfGridValue('temp_status');
                var orderNo = $('#girdtable').jfGridValue('P_OrderNo');
                var orderDate = $('#girdtable').jfGridValue('P_OrderDate');
              
                if (status == "0") {
                    ayma.alert.error("请先审核订单！");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    //打开统计页面BomPartSum 
                    ayma.layerForm({
                        id: 'form',
                        title: '物料统计',
                        url: top.$.rootUrl + '/MesDev/ProductOrderManager/BomPartSum?orderNo=' + orderNo + '&orderDate=' + orderDate,
                        width: 850,
                        height: 600,
                        maxmin: true,
                        btn:["生成领料单","关闭"],
                        callBack: function (id) {
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
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/ProductOrderManager/GetPageList',
                headData: [
                    { label: "订单号", name: "P_OrderNo", width: 160, align: "left" },
                    //{ label: "站点名称", name: "P_OrderStationName", width: 160, align: "left" },
                    { label: "订单时间", name: "P_OrderDate", width: 160, align: "left" },
                    { label: "使用时间", name: "P_UseDate", width: 160, align: "left" },
                    { label: "创建人", name: "P_CreateBy", width: 160, align: "left" },
                    { label: "临时状态", name: "temp_status", width: 160, align: "left", hidden: true },
                    { label: "创建时间", name: "P_CreateDate", width: 160, align: "left" },
                    //{ label: "物料编码", name: "P_GoodsCode", width: 160, align: "left", hidden: true },
                    {
                        label: "状态",
                        name: "P_Status",
                        width: 160,
                        align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'ProductOrderStatus',
                                callback: function (_data) {
                                    if (value == 0) {
                                        callback("<span class='label label-default'>" + _data.text + "</span>");
                                    } else if (value == 2) {
                                        callback("<span class='label label-info'>" + _data.text + "</span>");
                                    } else if (value == 3) {
                                        callback("<span class='label label-success'>" + _data.text + "</span>");
                                    }
                                }
                            });
                        }
                    }
                ],
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                isSubGrid: true, // 是否有子表editType 
                subGridRowExpanded: function (subgridId, row) {
                    var orderNo = row.P_OrderNo;
                    var P_Status = row.P_Status;
                    var subgridTableId = subgridId + "_t";
                    $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                    $subgridTable = $("#" + subgridTableId);
                    $subgridTable.jfGrid({
                        url: top.$.rootUrl + '/MesDev/ProductOrderManager/GetOrderDetail?orderNo=' + orderNo,
                        headData: [
                            {
                                label: '操作', name: '', index: '', width: 120, align: 'left', frozen: true, hidden: P_Status == 2 ? true : false,
                                formatter: function (value, grid, rows) {
                                    var result = "<a href=\"javascript:;\" style=\"color:#f60\" onclick=\"recordEdit('" + grid.ID + "')\">编辑</a>";
                                    return result;
                                }
                            },
                            { label: "物料编码", name: "P_GoodsCode", width: 160, align: "left" },
                            { label: "物料名称", name: "P_GoodsName", width: 160, align: "left" },
                            { label: "数量", name: "P_Qty", width: 160, align: "left" },
                            { label: "单位", name: "P_Unit", width: 160, align: "left" },
                            { label: "订单时间", name: "P_OrderDate", width: 160, align: "left" }
                        ],
                        mainId: 'ID',
                        reloadSelected: false,
                        sidx: "P_OrderDate",
                        sord: 'ASC',
                    }).jfGridSet("reload");
                }
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
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
                        width: 700,
                        height: 500,
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
    recordEdit = function (keyValue) {
        if (ayma.checkrow(keyValue)) {
            ayma.layerForm({
                id: 'form',
                title: '编辑订单',
                url: top.$.rootUrl + '/MesDev/ProductOrderManager/Form?keyValue=' + keyValue,
                width: 700,
                height: 500,
                maxmin: true,
                callBack: function (id) {
                    return top[id].acceptClick(refreshSubGirdData);
                }
            });
        }

    }
    //子列表刷新
    refreshSubGirdData = function () {
        $subgridTable.jfGridSet("reload");
    };

    page.init();
}
