﻿/* * 创建人：超级管理员
 * 日  期：2019-07-04 16:14
 * 描  述：强制使用记录单据
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
            }, 220, 400);
            //仓库
            $('#C_StockName').select({
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
            });
            //车间
            $('#C_WorkShopName').select({
                type: 'default',
                value: 'W_Name',
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
            $('#C_Status').DataItemSelect({ code: 'CompUserStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/CompUseHead/Form?formId=compUserForm',
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
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'compUserForm',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/CompUseHead/Form?formId=compUserForm&keyValue=' + keyValue,
                        width: 900,
                        height: 700,
                        maxmin: true,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/CompUseHead/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("C_Status");
                if (status != "1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_CompUseHead', field: 'C_Status' }, function () {
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
                url: top.$.rootUrl + '/MesDev/CompUseHead/GetPageList',
                headData: [
                    
                    {
                        label: "状态", name: "C_Status", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'CompUserStatus',
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
                    { label: "单号", name: "C_No", width: 160, align: "left"},
                    { label: "车间编号", name: "C_WorkShop", width: 160, align: "left" },
                    { label: "车间名称", name: "C_WorkShopName", width: 160, align: "left" },
                    { label: "仓库编号", name: "C_StockCode", width: 160, align: "left" },
                    { label: "仓库名称", name: "C_StockName", width: 160, align: "left" },
                    { label: "订单号", name: "C_OrderNo", width: 160, align: "left"},
                    { label: "订单时间", name: "C_OrderDate", width: 160, align: "left"},                    
                    { label: "添加人", name: "C_CreateBy", width: 160, align: "left"},
                    { label: "备注", name: "C_Remark", width: 160, align: "left"},
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
            param.State = "0";
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
