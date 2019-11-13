﻿/* * 创建人：超级管理员
 * 日  期：2019-11-08 13:40
 * 描  述：其它出库单
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, ayma) {
    var startTime;
    var endTime;
    "use strict";
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
            $('#O_StockName').select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetOtherStockList',
                // 访问数据接口参数
                param: {}
            });
            $('#O_Status').DataItemSelect({ code: 'ProOutStatus' });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                selectedRow = null;
                ayma.layerForm({
                    id: 'OtherHead',
                    title: '新增其它出库单',
                    url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/Form?formId=OtherHead',
                    width: 900,
                    height: 650,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                 var status = $('#girdtable').jfGridValue('O_Status');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'OtherHead',
                        title: '编辑其它出库单',
                        url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/Form?status=' + status + '&keyValue=' + keyValue + '&formId=OtherHead',
                        width: 900,
                        height: 650,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });    // 编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'OtherHead',
                        title: '编辑其它出库单',
                        url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/Form?keyValue=' + keyValue + '&formId=OtherHead',
                        width: 900,
                        height: 650,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //删除单据
            $("#am_delete").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OtherOutNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("O_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OtherOut_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //审核单据
            $("#am_audit").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("O_Status");
                if (status != "1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_OtherOutHead', field: 'O_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $("#am_post").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OtherOutNo");
                var status = $("#girdtable").jfGridValue("O_Status");
                if (status == "1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OtherOut_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OtherOutNo');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'OtherOutHeadReport',
                        title: '其它出库单打印',
                        url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/PrintReport?keyValue=' + keyValue + "&report=OtherOutHeadReport&data=Mes_OtherOutHead",
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
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/GetPageList',
                headData: [
                                 {
                                     label: "状态", name: "O_Status", width: 100, align: "left",
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
                        { label: '单号', name: 'O_OtherOutNo', width: 150, align: "left" },
                        { label: '仓库编码', name: 'O_StockCode', width: 150, align: "left" },
                        { label: '仓库名称', name: 'O_StockName', width: 150, align: "left" },
                        { label: '部门编码', name: 'O_DepartCode', width: 150, align: "left" },
                        { label: '部门名称', name: 'O_DepartName', width: 150, align: "left" },
                        { label: '添加人', name: 'O_CreateBy', width: 150, align: "left" },
                        { label: '添加时间', name: 'O_CreateDate', width: 150, align: "left" },
                        { label: '修改人', name: 'O_UpdateBy', width: 150, align: "left" },
                        { label: '修改时间', name: 'O_UpdateDate', width: 150, align: "left" },
                        { label: '备注', name: 'O_Remark', width: 150, align: "left" },
                        { label: '月结', name: 'MonthBalance', width: 200, align: "left" },
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