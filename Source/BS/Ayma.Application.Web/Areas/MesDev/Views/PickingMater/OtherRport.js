﻿/* * 创建人：超级管理员
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

            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/PickingMater/GetOtherRport',
                headData: [
                    { label: "物料名称", name: "f_goodsname", width: 200, align: "left" },
                    { label: "物料编码", name: "f_goodscode", width: 100, align: "center" },
                    { label: "出库日期", name: "f_createdate", width: 100, align: "center" },
                    { label: "出库数量", name: "f_outqty", width: 100, align: "center" },
                    { label: "退库数量", name: "f_inqty", width: 100, align: "center" },
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
