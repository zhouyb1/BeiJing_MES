﻿/* * 创建人：超级管理员
 * 日  期：2019-03-02 17:05
 * 描  述：成品出库单制作
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
            }, 220, 400);
            //仓库
            $('#P_StockName').select({
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
            $('#P_Status').DataItemSelect({ code: 'ProOutStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'ProOutMakeForm',
                    title: '新增成品出库单',
                    url: top.$.rootUrl + '/MesDev/ProOutMake/Form?formId=ProOutMakeForm',
                    width: 950,
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
                        id: 'ProOutMakeForm',
                        title: '编辑成品出库单',
                        url: top.$.rootUrl + '/MesDev/ProOutMake/Form?keyValue=' + keyValue + '&formId=ProOutMakeForm',
                        width: 950,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //双击编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'ProOutMakeForm',
                        title: '编辑成品出库单',
                        url: top.$.rootUrl + '/MesDev/ProOutMake/Form?keyValue=' + keyValue + '&formId=ProOutMakeForm',
                        width: 950,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //审核单据
            $("#am_audit").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("P_Status");
                if (status != "1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_ProOutHead', field: 'P_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            //删除单据
            $("#am_delete").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("P_ProOutNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("P_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_ProOut_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $("#am_post").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("P_ProOutNo");
                var status = $("#girdtable").jfGridValue("P_Status");
                if (status == "1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_ProOut_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('P_ProOutNo');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'ProOutMakeReport',
                        title: '成品出库单打印',
                        url: top.$.rootUrl + '/MesDev/ProOutMake/PrintReport?keyValue=' + keyValue + "&report=ProOutMakeReport&data=ProOutMake",
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
                url: top.$.rootUrl + '/MesDev/ProOutMake/GetPageList',
                headData: [
                    {
                        label: "状态", name: "P_Status", width: 100, align: "center",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'ProOutStatus',
                                callback: function (_data) {
                                    if (value == 1) {
                                        callback("<span class='label label-default'>" + _data.text + "</span>");
                                    } else if (value == 2) {
                                        callback("<span class='label label-info'>" + _data.text + "</span>");
                                    } else if(value==3){
                                        callback("<span class='label label-success'>" + _data.text + "</span>");
                                    } else {
                                        callback("<span class='label label-danger'>" + _data.text + "</span>");
                                    }
                                }
                            });
                        }
                    },
                    { label: "出库单号", name: "P_ProOutNo", width: 160, align: "center" },
                    { label: "仓库编码", name: "P_StockCode", width: 90, align: "center" },
                    { label: "仓库名称", name: "P_StockName", width: 160, align: "center" },
                    //{ label: "生产订单号", name: "P_OrderNo", width: 160, align: "left" },
                    //{ label: "订单时间", name: "P_OrderDate", width: 160, align: "left" },
                    { label: "备注", name: "P_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "P_CreateBy", width: 100, align: "center" },
                    { label: "添加时间", name: "P_CreateDate", width: 160, align: "center" },
                    { label: "修改人", name: "P_UpdateBy", width: 100, align: "center" },
                    { label: "修改时间", name: "P_UpdateDate", width: 160, align: "center" },
                    { label: "提交人", name: "P_UploadBy", width: 100, align: "center" },
                    { label: "提交时间", name: "P_UploadDate", width: 160, align: "center" }
                ],
                onRenderComplete: function (rows) {
                    var lengh = rows.length;
                    for (var i = 0; i < lengh; i++) {

                        $("[rownum='rownum_girdtable_" + i + "'][colname='P_StockName']").html("<a href =# style=text-decoration:underline  title='点击查询库存' onclick=js_method('" + rows[i].P_StockCode + "','6470af9c-c0be-4455-b8cc-164b9865bb24')>" + rows[i].P_StockName + "</a>");
                    }
                },
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'P_CreateDate',
                sord: 'desc'
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
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
