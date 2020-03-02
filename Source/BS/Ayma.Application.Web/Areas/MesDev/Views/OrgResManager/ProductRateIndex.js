/* * 创建人：超级管理员
 * 日  期：2020-01-15 11:29
 * 描  述：ds
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var jsonquery = {};
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
            }, 220, 300);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            $("#StockCode").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            });
            // 快速打印
            $('#am_print').on('click', function () {
                ayma.layerForm({
                    id: 'CCLCX',
                    title: '出成率实时查询打印',
                    url: top.$.rootUrl + '/MesDev/OrgResManager/PrintReport2?report=CCLCXReport&data=CCLCX&queryJson=' + encodeURIComponent(JSON.stringify(jsonquery)),
                    width: 1200,
                    height: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            //导出
            $('#am_export').on('click', function () {
                var url = top.$.rootUrl + '/MesDev/OrgResManager/Export?queryJson=' + JSON.stringify(jsonquery);

                window.location.href = url;
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/OrgResManager/GetProductRateList',
                headData: [
                {
                    label: "转换前",
                    name: "B",
                    width: 160,
                    align: "center",
                    children: [
                    { label: "物料名称", name: "O_GoodsName", width: 120, align: "center" },
                    { label: "物料编码", name: "O_GoodsCode", width: 100, align: "center" },
                    { label: "单位", name: "O_Unit", width: 50, align: "center" },
                    { label: "使用数量", name: "O_Qty", width: 90, align: "center" }]
                   }, 
                {
                    label: "转换后",
                    name: "B",
                    width: 160,
                    align: "center",
                    children: [
                    { label: "物料名称", name: "O_SecGoodsName", width: 120, align: "center" },
                    { label: "物料编码", name: "O_SecGoodsCode", width: 100, align: "center" },
                    { label: "单位", name: "O_SecUnit", width: 50, align: "center" },
                    { label: "产出数量", name: "O_SecQty", width: 90, align: "center" } ]
                },
                { label: "作业日耗库", name: "O_StockName", width: 130, align: "center", },
                { label: "作业工序", name: "O_ProName", width: 90, align: "center" },
                { label: "作业班组", name: "O_TeamName", width: 100, align: "center" },
                { label: "出成率(%)", name: "ProductRate", width: 90, align: "center" },
                { label: "制作人", name: "O_CreateBy", width: 100, align: "center" },
                ],
                mainId: 'ID',
                //reloadSelected: true,
                //isPage: true,
                //sord: 'desc'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            jsonquery = param;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}