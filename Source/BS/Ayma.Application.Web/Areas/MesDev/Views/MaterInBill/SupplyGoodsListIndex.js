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
            //导出excel
            $('#am_export').on('click', function () {
                if ($('#girdtable').jfGridGet('rowdatas').length == 0) {
                    ayma.alert.warning('当前没有数据行！');
                    return false;
                }
                var tableName = "girdtable";
                var fileName = "供应商存货明细";
                ayma.layerForm({
                    id: "ExcelExportForm",
                    title: '导出Excel数据',
                    url: encodeURI(top.$.rootUrl + '/Utility/ExcelExportForm?gridId=' + tableName + '&filename=' + encodeURI(fileName)),
                    width: 500,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick();
                    },
                    btn: ['导出Excel', '关闭']
                });
            });
            // 快速打印
            $('#am_print').on('click', function () {
                if ($('#girdtable').jfGridGet('rowdatas').length == 0) {
                    ayma.alert.warning('当前没有数据行！');
                    return false;
                }
                ayma.layerForm({
                    id: 'GYSCHMX',
                    title: '供应商存货明细打印',
                    url: top.$.rootUrl + '/MesDev/MaterInBill/PrintReport2?startTime=' + startTime+ "&endTime=" + endTime+ "&report=GYSCHMXReport&data=GYSCHMX",
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetSupplyGoodsList',
                headData: [
                    { label: "入库单号", name: "m_materinno", width: 150, align: "center" },
                    { label: "供应商编号", name: "m_supplycode", width: 80, align: "center" },
                    { label: "供应商名称", name: "m_supplyname", width: 180, align: "center" },
                    { label: "物料编码", name: "", width: 90, align: "left" },
                    { label: "物料名称", name: "m_goodsname", width: 120, align: "center" },
                    { label: "单位", name: "m_unit", width: 80, align: "center" },
                    { label: "包装单位", name: "m_unit2", width: 80, align: "center" },
                    { label: "税率(%)", name: "m_tax", width: 80, align: "center" },
                    { label: "包装数量", name: "m_unitqty", width: 80, align: "center" },
                    { label: "数量", name: "row_qty", width: 80, align: "center" },
                    { label: "价格", name: "m_price", width: 80, align: "center" },
                    { label: "不含税金额(元)", name: "row_amount", width: 100, align: "center" },
                   
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
