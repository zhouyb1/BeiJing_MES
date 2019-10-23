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
                dfvalue: '1',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    //生产订单号
                    $('#OrderNo').selectRefresh({
                        type: 'default',
                        value: 'P_OrderNo',
                        text: 'P_OrderNo',
                        // 展开最大高度
                        maxHeight: 200,
                        // 是否允许搜索
                        allowSearch: true,
                        // 访问数据接口地址
                        url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderListBy',
                        // 访问数据接口参数
                        param: { orderStartDate: startTime, orderEndDate: endTime }
                    });
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
            //生产订单号
            $('#OrderNo').select({
                type: 'default',
                value: 'P_OrderNo',
                text: 'P_OrderNo',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderListBy',
                // 访问数据接口参数
                param: { orderStartDate: startTime, orderEndDate: endTime }
            });
            //物料
            $('#SecGoodsCode').select({
                type: 'default',
                value: 'G_Code',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetGoodsList',
                // 访问数据接口参数
                param: { }
            });

        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/InventorySeach/GetPricePageList',
                headData: [
                    { label: "商品编码", name: "O_SecGoodsCode", width: 160, align: "left" },
                    { label: "商品名称", name: "O_SecGoodsName", width: 160, align: "left" },
                    { label: "价格", name: "O_SecPrice", width: 160, align: "left" },
                    { label: "批次", name: "O_Batch", width: 160, align: "left" },
                ],
                mainId: 'O_SecGoodsCode',
                reloadSelected: true,
                isPage: true,
                sidx: "O_Batch",
                sord: 'DESC',
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
