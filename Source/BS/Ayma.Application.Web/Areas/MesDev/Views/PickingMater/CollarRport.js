/* * 创建人：超级管理员
 * 日  期：2019-03-11 19:22
 * 描  述：领料单
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {

            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 200, 350);

            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });

            //绑定物料
            $('#GoodsCode').select({
                type: 'default',
                value: 'G_Code',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址 
                url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
                // 访问数据接口参数
                param: {}
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
                url: top.$.rootUrl + '/MesDev/Tools/GetStockListByParam',
                // 访问数据接口参数
                param: { strWhere: "S_Kind =1" }
            });
            //导出excel
            $('#am_export').on('click', function () {
                var tableName = "girdtable";
                var fileName = "领料出库汇总报表";
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
                var starttime = $('#StartTime').val();
                var endtime = $('#EndTime').val();
                var GoodsCode = $('#GoodsCode').selectGet();
                var StockCode = $('#StockCode').selectGet();
                ayma.layerForm({
                    id: 'LLCKHZBBReport',
                    title: '领料出库汇总报表打印',
                    url: top.$.rootUrl + '/MesDev/PickingMater/PrintReport3?starttime=' + starttime + "&endtime=" + endtime + "&GoodsCode=" + GoodsCode + "&StockCode=" + StockCode + "&report=LLCKHZBBReport&data=LLCKHZBB",
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/PickingMater/GetCollarRport',
                headData: [
                    { label: "物料名称", name: "f_goodsname", width: 200, align: "left" },
                    { label: "物料编码", name: "f_goodscode", width: 100, align: "center" },
                    { label: "领料日期", name: "f_createdate", width: 100, align: "center" },
                    { label: "领料数量", name: "f_outqty", width: 100, align: "center" },
                    { label: "退料数量", name: "f_inqty", width: 100, align: "center" },
                    //{ label: "差异数量", name: "f_diffqty", width: 100, align: "center" }
                ]
            });

        },

        search: function (param) {
            param = param || {};

            if (param.StartTime == "" || param.EndTime == "")
            {
                ayma.alert.error("请先选择日期条件");
                return;
            }

            var postData = {};
            postData.queryJson = JSON.stringify(param);
            $('#girdtable').jfGridSet('reload', { param: postData });
        }
    };

    refreshGirdData = function () {
        page.search();
    };

    page.init();
}

