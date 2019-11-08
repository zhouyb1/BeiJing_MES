﻿/* * 创建人：超级管理员
 * 日  期：2019-03-18 10:37
 * 描  述：原物料销售单据查询
 */
var refreshGirdData;
var $subgridTable;//子列表
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
            $('#S_StockCode').DataSourceSelect({ code: 'StockList', value: 's_code', text: 's_name' });
            $('#S_CostomCode').DataSourceSelect({ code: 'CustomerList', value: 'c_code', text: 'c_name' }); 

            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            
            //撤销单据
            $("#am_cancle").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("S_SaleNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_Sale_Cancel', type: 2 }, function () {
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
                url: top.$.rootUrl + '/MesDev/Mes_SaleManager/GetPostList',
                headData: [
                    { label: "主键", name: "ID", width: 100, align: "left", hidden: true },
                    {
                        label: "状态", name: "S_Status", width: 90, align: "left",
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
                    { label: "是否已月结", name: "MonthBalance", width: 80, align: "left" },
                    { label: "单据编码", name: "S_SaleNo", width: 130, align: "left" },
                    { label: "仓库名称", name: "S_StockName", width: 130, align: "left" },
                    { label: "仓库编码", name: "S_StockCode", width: 90, align: "left" },
                    { label: "客户名称", name: "S_CostomName", width: 130, align: "left" },
                    { label: "客户编码", name: "S_CostomCode", width: 90, align: "left" },
                    { label: "备注", name: "S_Remark", width: 130, align: "left" },
                    { label: "添加人", name: "S_CreateBy", width: 100, align: "left" },
                    { label: "添加时间", name: "S_CreateDate", width: 130, align: "left" },
                    { label: "修改人", name: "S_UpdateBy", width: 100, align: "left" },
                    { label: "修改时间", name: "S_UpdateDate", width: 130, align: "left" },
                ],
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'S_CreateDate',
                sord: 'desc',
                isSubGrid: true, // 是否有子表editType 
                subGridRowExpanded: function (subgridId, row) {
                    var orderNo = row.S_SaleNo;
                    var subgridTableId = subgridId + "_t";
                    $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                    $subgridTable = $("#" + subgridTableId);
                    $subgridTable.jfGrid({
                        url: top.$.rootUrl + '/MesDev/Mes_SaleManager/GetDetailList?SaleNo=' + orderNo,
                        headData: [
                           
                            { label: "物料名称", name: "S_GoodsName", width: 130, align: "left" },
                            { label: "物料编码", name: "S_GoodsCode", width: 90, align: "left" },
                            { label: "批次", name: "S_Batch", width: 100, align: "left" },
                            { label: "单价", name: "S_Price", width: 80, align: "left" },
                            { label: "数量", name: "S_Qty", width: 90, align: "left" },
                            { label: "单位", name: "S_Unit", width: 80, align: "left" }
                        ],
                       
                    }).jfGridSet("reload");
                }
                
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
