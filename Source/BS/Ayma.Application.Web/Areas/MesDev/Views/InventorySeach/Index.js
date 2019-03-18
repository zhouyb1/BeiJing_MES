/* * 创建人：超级管理员
 * 日  期：2019-01-08 17:17
 * 描  述：库存查询
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
            }, 220, 500);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 查看详情
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '库存查询',
                        url: top.$.rootUrl + '/MesDev/InventorySeach/Form?keyValue=' + keyValue,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                            //return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/InventorySeach/GetPageList',
                headData: [
                    { label: "仓库编码", name: "I_StockCode", width: 160, align: "left"},
                    { label: "仓库名称", name: "I_StockName", width: 160, align: "left"},
                    { label: "商品编码", name: "I_GoodsCode", width: 160, align: "left"},
                    { label: "商品名称", name: "I_GoodsName", width: 160, align: "left"},
                    { label: "单位", name: "I_Unit", width: 160, align: "left"},
                    { label: "数量", name: "I_Qty", width: 160, align: "left"},
                    { label: "批次", name: "I_Batch", width: 160, align: "left"},
                    { label: "备注", name: "I_Remark", width: 160, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true
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
