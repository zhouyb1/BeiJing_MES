﻿/* * 创建人：超级管理员
 * 日  期：2019-03-18 11:16
 * 描  述：库存动态表查询
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
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/InventortTrend/Form',
                    width: 900,
                    height: 700,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 查看详情
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '库存动态详情',
                        url: top.$.rootUrl + '/MesDev/InventortTrend/Form?keyValue=' + keyValue,
                        width: 900,
                        height: 700,
                        maxmin: true,
                        btn:null,
                        callBack: function (id) {
                            //return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/InventortTrend/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/InventortTrend/GetPageList',
                headData: [
                    { label: "单据类型", name: "I_OrderKind", width: 160, align: "left"},
                    { label: "仓库编码", name: "I_StockCode", width: 160, align: "left"},
                    { label: "仓库名称", name: "I_StockName", width: 160, align: "left"},
                    { label: "物料编码", name: "I_GoodsCode", width: 160, align: "left"},
                    { label: "物料名称", name: "I_GoodsName", width: 160, align: "left"},
                    { label: "单位", name: "I_Unit", width: 160, align: "left"},
                    { label: "批次", name: "I_Batch", width: 160, align: "left"},
                    { label: "保质期", name: "I_Period", width: 160, align: "left"},
                    { label: "单据号", name: "I_OrderNo", width: 160, align: "left"},
                    { label: "初始数量", name: "I_QtyOld", width: 160, align: "left"},
                    { label: "新数量", name: "I_QtyNew", width: 160, align: "left"},
                    { label: "移动数量", name: "I_QtyTrend", width: 160, align: "left"},
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
