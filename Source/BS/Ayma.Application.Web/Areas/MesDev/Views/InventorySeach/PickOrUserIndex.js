/* * 创建人：超级管理员
 * 日  期：2019-01-08 17:17
 * 描  述：物料领用和使用情况查询
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
                dfvalue: 'currentD',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 150, 400);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            
            //绑定仓库
            $('#StockCode').select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetNoProjStockList',
                // 访问数据接口参数
                param: {}
            });

        },
        // 初始化列表
        initGird: function () {
            $('#pickgirdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/InventorySeach/GetPickPageList',
                headData: [
                    { label: "商品编码", name: "GoodsCode", width: 160, align: "left"},
                    { label: "商品名称", name: "GoodsName", width: 160, align: "left"},
                    { label: "领用数量", name: "Qty", width: 160, align: "left"},
                    { label: "批次", name: "Batch", width: 160, align: "left"},
                ],
                mainId: 'GoodsCode',
                reloadSelected: true,
                isPage: true
            });
            $('#usedgirdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/InventorySeach/GetUsedPageList',
                headData: [
                    { label: "商品编码", name: "GoodsCode", width: 160, align: "left"},
                    { label: "商品名称", name: "GoodsName", width: 160, align: "left"},
                    { label: "使用数量", name: "Qty", width: 160, align: "left"},
                    { label: "批次", name: "Batch", width: 160, align: "left"},
                ],
                mainId: 'GoodsCode',
                reloadSelected: true,
                isPage: true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#pickgirdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
            $('#usedgirdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
