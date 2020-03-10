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
           
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 250, 480);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //绑定物料
            $("#GoodsCode").select({
                type: 'default',
                value: 'G_Code',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 150,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetGoodsList',
                // 访问数据接口参数
                param: {}
            });
            //绑定仓库
            $('#StockCode').select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 150,
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
                    { label: "领用数量", name: "Qty", width: 160, align: "left" },
                    { label: "仓库", name: "StockCode", width: 160, align: "left" },
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
                    { label: "使用数量", name: "Qty", width: 160, align: "left" },
                    { label: "仓库", name: "StockCode", width: 160, align: "left" },
                    { label: "批次", name: "Batch", width: 160, align: "left"},
                ],
                mainId: 'GoodsCode',
                reloadSelected: true,
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            $('#pickgirdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
            $('#usedgirdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
