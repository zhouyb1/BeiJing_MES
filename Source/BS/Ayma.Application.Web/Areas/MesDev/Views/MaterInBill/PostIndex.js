﻿/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
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
            }, 180, 500);
            $('#M_Status').DataItemSelect({ code: 'MaterInStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 查看详情
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '详情',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?keyValue=' + keyValue,
                        width: 700,
                        height: 600,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {

                        }
                    });
                }
            });
            // 退供应商
            $('#am_returnsupply').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                
                if (ayma.checkrow(keyValue)) {
                    var status = $('#girdtable').jfGridValue('M_Status');

                    if (status != 3) {
                        ayma.alert.error("单据未完成,不能退供应商.");
                        return false;
                    }
                    ayma.layerForm({
                        id: 'BackSupplyAddForm',
                        title: '退供应商单',
                        url: top.$.rootUrl + '/MesDev/Mes_BackSupply/AddForm?materInKeyValue=' + keyValue + '&formId=BackSupplyAddForm',
                        width: 900,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("M_MaterInNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_MaterIn_Cancel', type: 2 }, function () {
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
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetPostPageList',
                headData: [
                    {
                        label: "状态", name: "M_Status", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'MaterInStatus',
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
                    { label: "入库单号", name: "M_MaterInNo", width: 160, align: "left" },
                    { label: "仓库编码", name: "M_StockCode", width: 160, align: "left" },
                    { label: "仓库名称", name: "M_StockName", width: 160, align: "left" },
                    //{ label: "生产订单号", name: "M_OrderNo", width: 160, align: "left" },
                    //{ label: "订单时间", name: "M_OrderDate", width: 160, align: "left" },
                    { label: "备注", name: "M_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "M_CreateBy", width: 160, align: "left" },
                    { label: "添加时间", name: "M_CreateDate", width: 160, align: "left" },
                    { label: "修改人", name: "M_UpdateBy", width: 160, align: "left" },
                    { label: "修改时间", name: "M_UpdateDate", width: 160, align: "left" },
                    { label: "提交人", name: "M_UploadBy", width: 160, align: "left"},
                    { label: "提交时间", name: "M_UploadDate", width: 160, align: "left"}
                ],
                mainId: 'ID',
                isPage: true,
                sidx: 'M_MaterInNo',
                sord: 'DESC'
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
