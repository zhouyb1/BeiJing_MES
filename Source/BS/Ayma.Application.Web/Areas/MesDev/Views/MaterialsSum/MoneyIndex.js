/* * 创建人：超级管理员
 * 日  期：2019-09-16 10:59
 * 描  述：原物料统计(入库、出库、次品)
 */
var refreshSubGirdData;
var $subgridTable;//子列表
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var tabTitle = "汇总";
    var data;
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
            //双击  
            $('#girdtable_sum').on('dblclick', function () {
                var dateParam = { StartTime: startTime, EndTime: endTime };
                var M_GoodsCode = $('#girdtable_sum').jfGridValue('m_goodscode');
                $('#girdtable_detail').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
                $('#pageTab a[href="#page_detail"]').tab('show'); // 通过名字选择
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 180, 500);
            // 刷新
            $('#am_refresh').on('click', function() {
                location.reload();
            });
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                // 获取已激活的标签页的名称
                var activeTab = $(e.target).text();
                // 获取前一个激活的标签页的名称
                //var previousTab = $(e.relatedTarget).text();
                tabTitle = activeTab;
            });
            //绑定供应商
            $("#G_SupplyCode").select({
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
            //导出excel
            $('#am_export').on('click', function () {
                var tableName = "";
                var fileName = "";
                if (tabTitle == "汇总") {
                    tableName = "girdtable_sum";
                    fileName = "原物料出入库汇总";
                } else {
                    tableName = "girdtable_detail";
                    fileName = "原物料出入库明细";
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
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable_sum').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialSumListByDate',
                headData: [
                    { label: "商品编码", name: "m_goodscode", width: 160, align: "left" },
                    { label: "商品名称", name: "m_goodsname", width: 160, align: "left" },
                    { label: "入库数量", name: "inventoryquantity", width: 160, align: "left", statistics: true },
                    { label: "出库数量", name: "delivery", width: 160, align: "left",statistics: true },
                    { label: "次品退库数量", name: "back_qty", width: 160, align: "left",statistics: true },
                    { label: "期初库存", name: "initialinventory", width: 160, align: "left",statistics: true },
                    { label: "期初金额", name: "initialamount", width: 160, align: "left",statistics: true },
                    { label: "期末库存", name: "endinginventory", width: 160, align: "left",statistics: true },
                    { label: "期末金额", name: "finalamount", width: 160, align: "left",statistics: true },
                    { label: "单价", name: "price", width: 160, align: "left" },
                    //{ label: "入库时间", name: "m_createdate", width: 160, align: "left",hidden:true}    
                ],
                mainId:'ID',
                reloadSelected: true,
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: 'm_goodscode',
                sord: 'desc',
            });
            //明细
            $('#girdtable_detail').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialDetailListByDate',
                headData: [
                               { label: "单据号", name: "M_MaterInNo", width: 160, align: "left" },
                               { label: "供应商编码", name: "M_SupplyCode", width: 80, align: "left" },
                               { label: "供应商名称", name: "M_SupplyName", width: 160, align: "left" },
                               { label: "商品编码", name: "M_GoodsCode", width: 80, align: "left" },
                               { label: "商品名称", name: "M_GoodsName", width: 160, align: "left" },
                               { label: "数量", name: "M_Qty", width: 160, align: "left",statistics: true },
                               { label: "批次", name: "M_Batch", width: 90, align: "left" },
                               { label: "商品税率", name: "M_GoodsItax", width: 160, align: "left" },
                               { label: "备注", name: "M_Remark", width: 90, align: "left" },
                               { label: "入库时间", name: "M_CreateDate", width: 160, align: "left" }
                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "M_Qty",
                sord: 'ASC',
                reloadSelected: false,
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            param.data = $("#Date").val();
            $('#girdtable_sum').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
            $('#pageTab a[href="#page_sum"]').tab('show'); // 通过名字选择
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    //子列表刷新
    refreshSubGirdData = function () {
        $subgridTable.jfGridSet("reload");
    };
    page.init();
}
