﻿/* * 创建人：超级管理员
 * 日  期：2019-03-11 19:22
 * 描  述：领料单
 */
var refreshGirdData;
var js_method;

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
            }, 280, 350);
            $('#P_Status').DataItemSelect({ code: 'ProOutStatus' });

            $('#C_StockToCode').select({
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
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('P_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/PickingMater/TempForm?status=' + statu + '&keyValue=' + keyValue + '&formId=form',
                        width: 1000,
                        height: 600,
                        btn: null,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 双击编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('P_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/PickingMater/TempForm?status=' + statu + '&keyValue=' + keyValue + '&formId=form',
                        width: 1000,
                        height: 600,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/PickingMater/GetTempPageList',
                headData: [
                    {
                        label: "状态", name: "P_Status", width: 100, align: "center",
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
                    { label: "领料单号", name: "C_CollarNo", width: 180, align: "center" },
                    { label: "领料计划单号", name: "C_CollarNoZS", width: 160, align: "center" },
                    { label: "领料仓编码", name: "C_StockToCode", width: 90, align: "center" },
                    { label: "领料仓", name: "C_StockToName", width: 160, align: "center" },
                    { label: "生产订单号", name: "P_OrderNo", width: 160, align: "left", hidden: "hidden" },
                    { label: "订单时间", name: "P_OrderDate", width: 160, align: "left", hidden: "hidden" },
                    { label: "添加人", name: "C_CreateBy", width: 100, align: "center" },
                    { label: "添加时间", name: "C_CreateDate", width: 160, align: "center" },
                    { label: "修改人", name: "C_UpdateBy", width: 100, align: "center" },
                    { label: "修改时间", name: "C_UpdateDate", width: 160, align: "center" },
                    { label: "备注", name: "C_Remark", width: 160, align: "left" },
                ],
                onRenderComplete: function (rows) {
                    var lengh = rows.length;
                    for (var i = 0; i < lengh; i++) {
                        $("[rownum='rownum_girdtable_" + i + "'][colname='C_StockName']").html("<a href =# style=text-decoration:underline  title='点击查询库存' onclick=js_method('" + rows[i].C_StockCode + "','6470af9c-c0be-4455-b8cc-164b9865bb24')>" + rows[i].C_StockName + "</a>");
                    }
                },
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'C_CreateDate',
                sord: 'DESC'
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        },
    };
    js_method = function (code, moduleId) {
        var module = top.ayma.clientdata.get(['modulesMap', moduleId]);
        module.F_UrlAddress = '/MesDev/InventorySeach/Index?stock=' + encodeURIComponent(code);
        top.ayma.frameTab.openNew(module);
    }
    refreshGirdData = function () {
        page.search();
    };

    page.init();
}

