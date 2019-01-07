﻿/* * 创建人：超级管理员
 * 日  期：2019-01-07 17:54
 * 描  述：查询生成清单
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
                dfvalue: '0',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_ProductOrderHead/GetPageList',
                headData: [
                    { label: "ID", name: "ID", width: 160, align: "left", hidden: true },
                    { label: "生产单号", name: "P_OrderNo", width: 160, align: "left" },
                    { label: "车站ID", name: "P_OrderStationID", width: 160, align: "left", hidden: true },
                    { label: "订单时间", name: "P_OrderDate", width: 160, align: "left" },
                    { label: "物料编码", name: "P_GoodsCode", width: 160, align: "left" },
                    { label: "物料名称", name: "P_GoodsName", width: 160, align: "left" },
                    { label: "单位", name: "P_Unit", width: 160, align: "left", hidden: true },
                    { label: "数量", name: "P_Qty", width: 160, align: "left" },
                    { label: "车站名称", name: "P_OrderStationName", width: 160, align: "left" },
                    { label: "添加人", name: "P_CreateBy", width: 160, align: "left" },
                    { label: "添加时间", name: "P_CreateDate", width: 160, align: "left" },
                    { label: "修改人", name: "P_UpdateBy", width: 160, align: "left" },
                    { label: "修改时间", name: "P_UpdateDate", width: 160, align: "left" },

                ],
                mainId: 'ID',
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
