/* * 创建人：超级管理员
 * 日  期：2019-10-09 16:16
 * 描  述：保质期
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var M_GoodsName = $('#txt_Keyword').val();
                page.search({ M_GoodsName: M_GoodsName });
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Period/GetPageList',
                headData: [
                        { label: '仓库名称', name: 'M_StockName', width: 150, align: "left" },
                        { label: '物料名称', name: 'M_GoodsName', width: 200, align: "left" },
                        { label: '单位', name: 'M_Unit', width: 200, align: "left" },
                          { label: '保质期/天', name: 'G_Period', width: 150, align: "left" },
                        { label: '在库时间/天', name: 'InventoryDay', width: 150, align: "left" },
                        { label: '保质期状态', name: 'GoodsState', width: 150, align: "left"},
                        { label: '批次', name: 'M_Batch', width: 200, align: "left" }
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: "M_Batch",
                sord: 'DESC',
                onRenderComplete: function (rows) {
                    for (var i = 0; i < rows.length; i++) {
                        if (rows[i].GoodsState != "产品已过期") {
                            continue;
                        }
                        else {
                            $("[rownum='rownum_girdtable_" + i + "']").css('background-color', '#d9534f');//保质期状态不正常的整行标红
                        }
                    }
                }
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
