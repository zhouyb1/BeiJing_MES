/* * 创建人：超级管理员
 * 日  期：2019-09-16 10:59
 * 描  述：原物料统计(入库、出库、次品)
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var tabTitle = "汇总";

    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
          
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
                //var tableName = "";
                //var fileName = "";
                //if (tabTitle == "汇总") {
                //    tableName = "girdtable_sum";
                //    fileName = "原物料汇总";
                //} else {
                //    tableName = "girdtable_detail";
                //    fileName = "原物料明细";
                //}


                //ayma.layerForm({
                //    id: "ExcelExportForm",
                //    title: '导出Excel数据',
                //    url: encodeURI(top.$.rootUrl + '/Utility/ExcelExportForm?gridId=' + tableName + '&filename=' + encodeURI(fileName)),
                //    width: 500,
                //    height: 380,
                //    callBack: function (id) {
                //        return top[id].acceptClick();
                //    },
                //    btn: ['导出Excel', '关闭']
                //});
            });
        },
        // 初始化列表$('#girdtable_sum').jfGrid
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialSumList',
                headData: [
                    { label: "商品编码", name: "g_code", width: 160, align: "left" },
                    { label: "商品名称", name: "g_name", width: 160, align: "left" },
                    { label: "入库数量", name: "in_qty", width: 160, align: "left" },
                    { label: "出库数量", name: "out_qty", width: 160, align: "left" },
                    { label: "次品退库数量", name: "back_qty", width: 160, align: "left" },
                    { label: "单位", name: "g_unit", width: 160, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: 'g_code',
                sord: 'desc'
            });
            page.search();

            //$('#girdtable_detail').jfGrid({
            //    url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialDetailList',
            //    headData: [
            //        { label: "商品编码", name: "g_code", width: 160, align: "left" },
            //        { label: "商品名称", name: "g_name", width: 160, align: "left" },
            //        { label: "商品批次", name: "m_batch", width: 160, align: "left" },
            //        { label: "入库数量", name: "in_qty", width: 160, align: "left" },
            //        { label: "入库合计金额", name: "in_amount", width: 160, align: "left" },
            //        { label: "出库数量", name: "out_qty", width: 160, align: "left" },
            //        { label: "出库合计金额", name: "out_amount", width: 160, align: "left" },
            //        { label: "次品退库数量", name: "back_qty", width: 160, align: "left" },
            //        { label: "次品合计金额", name: "back_amount", width: 160, align: "left" },
            //        //{ label: "供应商编码", name: "m_supplycode", width: 160, align: "left" },
            //        { label: "供应商名称", name: "m_supplyname", width: 160, align: "left" },
            //        { label: "单价", name: "m_price", width: 160, align: "left" },
            //        { label: "单位", name: "g_unit", width: 160, align: "left" }
            //    ],
            //    reloadSelected: true,
            //    isPage: true,
            //    sidx: 'g_code,m_batch',
            //    sord: 'asc'
            //});
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            //var cksum = $('#cksum').is(':checked');
            //var ckdetail = $('#ckdetail').is(':checked');

            //if (cksum) {
            //    $('#girdtable_sum').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
            //}

            //if (ckdetail) {
            //    $('#girdtable_detail').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
            //}

            //if (ckdetail) {
            //    $('#pageTab a[href="#page_detail"]').tab('show'); // 通过名字选择
            //} else {
            //    $('#pageTab a[href="#page_sum"]').tab('show'); // 通过名字选择
            //}
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });

        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
