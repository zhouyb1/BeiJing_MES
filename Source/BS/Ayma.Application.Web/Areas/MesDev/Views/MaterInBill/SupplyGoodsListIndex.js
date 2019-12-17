/* * 创建人：超级管理员
 * 日  期：2019-12-17 21:00
 * 描  述：供应商存货明细
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
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetSupplayGoodsList',
                headData: [
                    { label: "入库单号", name: "M_MaterInNo", width: 120, align: "left" },
                    { label: "供应商编号", name: "M_SupplyCode", width: 80, align: "center" },
                    { label: "供应商名称", name: "M_SupplyName", width: 120, align: "left" },
                    { label: "物料编码", name: "", width: 90, align: "left" },
                    { label: "物料名称", name: "M_GoodsName", width: 120, align: "left" },
                    { label: "单位", name: "M_Unit", width: 80, align: "center" },
                    { label: "数量", name: "M_Qty", width: 80, align: "center" },
                    { label: "价格", name: "M_Price", width: 160, align: "center" },
                    { label: "添加时间", name: "M_CreateDate", width: 130, align: "left" },
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
