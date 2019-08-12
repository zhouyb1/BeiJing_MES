/* * 创建人：超级管理员
 * 日  期：2019-01-07 13:55
 * 描  述：物料列表
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //打印
            // 快速打印
            $('#am_print').on('click', function () {
                //var name = $("#G_Name").val();
                //if (ayma.checkrow(name)) {
                ayma.layerForm({
                    id: 'YieldRateReport',
                    title: '毛到净出成率打印',
                    url: top.$.rootUrl + '/MesDev/GoodsList/PrintReport?name=' + name + "&report=YieldRateReport&data=YieldRate",
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
                //} else {
                //    ayma.alert.error("请选择要打印的单据！");
                //}
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/GoodsList/GetYieldRatePageList',
                headData: [
                    {
                        label: "项目", name: "项目", width: 120, align: "center", children: [
                              { label: "物料名称", name: "o_goodsname", width: 120, align: "center" },
                              { label: "配方名称", name: "formulaname", width: 120, align: "center" }
                        ]
                    },
                    {
                        label: "毛到净出成率%", name: "毛到净出成率%", width: 100, align: "center", children: [
                                { label: "指标", name: "rate", width: 100, align: "center" },
                                { label: "1月", name: "january", width: 80, align: "center" },
                                { label: "2月", name: "february", width: 80, align: "center" },

                                {
                                    label: "3月", name: "march", width: 80, align: "center"
                                },
                                { label: "4月", name: "april", width: 80, align: "center" },
                                { label: "5月", name: "may", width: 80, align: "center" },
                                { label: "6月", name: "june", width: 80, align: "center" },
                                { label: "7月", name: "july", width: 80, align: "center" },
                                { label: "8月", name: "august", width: 80, align: "center" },
                                { label: "9月", name: "september", width: 80, align: "center" },
                                { label: "10月", name: "october", width: 80, align: "center" },
                                 { label: "11月", name: "november", width: 80, align: "center" },
                                 { label: "12月", name: "december", width: 80, align: "center" },
                                 { label: "1月差异", name: "jandiff", width: 80, align: "center" },
                                    { label: "2月差异", name: "febdiff", width: 80, align: "center" },
                                  {label: "3月差异", name: "mardiff", width: 80, align: "center"},
                                { label: "4月差异", name: "aprdiff", width: 80, align: "center" },
                                { label: "5月差异", name: "maydiff", width: 80, align: "center" },
                                { label: "6月差异", name: "jundiff", width: 80, align: "center" },
                                { label: "7月差异", name: "julydiff", width: 80, align: "center" },
                                { label: "8月差异", name: "augdiff", width: 80, align: "center" },
                                { label: "9月差异", name: "septdiff", width: 80, align: "center" },
                                { label: "10月差异", name: "octdiff", width: 80, align: "center" },
                                { label: "11月差异", name: "novdiff", width: 80, align: "center" },
                                { label: "12月差异", name: "decdiff", width: 80, align: "center" }
                        ]
                    },


                ],
                mainId: 'o_goodsname',
                isPage: false
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
