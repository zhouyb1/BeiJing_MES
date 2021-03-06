﻿/* * 创建人：超级管理员
 * 日  期：2019-09-25 14:42
 * 描  述：供应商进货数据汇总
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var supplyCode = "";
    var tabTitle = "汇总";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            page.doubleClick();
        },
        bind: function () {
            // 时间搜索框
            $('#datesearch').amdate({
                dfdata: [
                    { name: '今天', begin: function () { return ayma.getDate('yyyy-MM-dd') }, end: function () { return ayma.getDate('yyyy-MM-dd') } },
                    { name: '近7天', begin: function () { return ayma.getDate('yyyy-MM-dd', 'd', -6) }, end: function () { return ayma.getDate('yyyy-MM-dd') } },
                    { name: '近1个月', begin: function () { return ayma.getDate('yyyy-MM-dd', 'm', -1) }, end: function () { return ayma.getDate('yyyy-MM-dd') } },
                    { name: '近3个月', begin: function () { return ayma.getDate('yyyy-MM-dd', 'm', -3) }, end: function () { return ayma.getDate('yyyy-MM-dd') } }
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
            //双击
            $('#girdtable_sum').on('dblclick', function() {
                var dateParam = { StartTime: startTime, EndTime: endTime };
                supplyCode = $('#girdtable_sum').jfGridValue('m_supplycode');
                $('#girdtable_detail').jfGridSet('reload', { param: { supplyCode: supplyCode, queryJson: JSON.stringify(dateParam) } });
                $('#pageTab a[href="#page_detail"]').tab('show'); // 通过名字选择
            });
            
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            //绑定供应商
            $("#M_SupplyCode").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetSupplyList',
                // 访问数据接口参数
                param: {}
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                // 获取已激活的标签页的名称
                var activeTab = $(e.target).text();
                // 获取前一个激活的标签页的名称
                //var previousTab = $(e.relatedTarget).text();
                tabTitle = activeTab;
            });
           
            // 导出excel
            $('#am_export').on('click', function () {
                var tableName = "";
                var fileName = "";
                if (tabTitle == "汇总") {
                    tableName = "girdtable_sum";
                    fileName = "供应商进货汇总";
                } else {
                    tableName = "girdtable_detail";
                    fileName = "供应商进货明细";
                }


                ayma.layerForm({
                    id: "ExcelExportForm",
                    title: '导出Excel数据',
                    url: encodeURI(top.$.rootUrl + '/Utility/ExcelExportForm?gridId=' + tableName + '&filename=' + encodeURI(fileName)),
                    width: 500,
                    height: 380,
                    callBack: function (id) {
                        return top[id].acceptClick();
                    },
                    btn: ['导出Excel', '关闭']
                });
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable_sum').jfGridValue('m_supplycode');
                    ayma.layerForm({
                        id: 'GYSJHSJHZ',
                        title: '供应商供货数据汇总打印',
                        url: top.$.rootUrl + '/MesDev/SupplyGoodsCountRep/PrintReport?keyValue=' + keyValue + "&endtime=" + endTime + "&starttime=" + startTime + "&report=GYSJHSJHZReport&data=GYSJHSJHZ",
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
            $('#girdtable_sum').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/SupplyGoodsCountRep/GetPageList',
                headData: [
                    //{ label: "ID", name: "ID", width: 160, align: "left"},
                    { label: "供应商编码", name: "m_supplycode", width: 90, align: "center" },
                    { label: "供应商名称", name: "m_supplyname", width: 160, align: "center" },
                    { label: "数量", name: "in_qty", width: 160, align: "center", statistics: true },
                    //{ label: "单位成本(元)", name: "avg_price", width: 160, align: "center", statistics: true },
                    { label: "进货总成本(元)", name: "in_amount", width: 160, align: "center", statistics: true },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                isStatistics: true,
                footerrow: true,
                sidx: 'm_supplycode',                 // 排序列
                sord: 'desc'              // 排序类型
            });

            //明细
            $('#girdtable_detail').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/SupplyGoodsCountRep/GetSupplyGoodsDetail',
                headData: [
                    {
                        label: "单据日期", name: "m_orderdate", width: 160, align: "center",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "单据编号", name: "m_materinno", width: 160, align: "center" },
                    { label: "物料名称", name: "m_goodsname", width: 160, align: "center" },
                    { label: "基本单位", name: "m_unit", width: 90, align: "left" },
                    { label: "数量", name: "m_qty", width: 160, align: "center", statistics: true },
                    { label: "税额(%)", name: "m_tax", width: 90, align: "center" },
                    { label: "单价(元)", name: "m_price", width: 90, align: "center" },
                    { label: "不含税金额(元)", name: "amount", width: 160, align: "center", statistics: true },
                    { label: "存货仓库", name: "m_stockname", width: 160, align: "center" },
                    { label: "经办人", name: "m_createby", width: 100, align: "center" },
                ],
                mainId: 'ID',
                isPage: false,
                isStatistics: true,
                footerrow: true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable_sum').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
            //$('#girdtable_detail').jfGridSet('reload', { param: { supplyCode: supplyCode } });

            $('#pageTab a[href="#page_sum"]').tab('show'); // 通过名字选择

            //$('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
