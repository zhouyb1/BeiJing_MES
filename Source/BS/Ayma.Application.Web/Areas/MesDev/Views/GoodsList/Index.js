﻿/* * 创建人：超级管理员
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
            }, 220, 500);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //商品类型
            $("#G_Kind").DataItemSelect({ code: "GoodsType" });
            //供应商列表
            var dfop = {
                type: 'default',
                value: 'ID',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetSupplyList',
                // 访问数据接口参数
                param: {}
            }
            $("#G_Supply").select(dfop);
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增物料',
                    url: top.$.rootUrl + '/MesDev/GoodsList/Form',
                    width: 700,
                    height: 500,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑物料',
                        url: top.$.rootUrl + '/MesDev/GoodsList/Form?keyValue=' + keyValue,
                        width: 800,
                        height: 500,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/GoodsList/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/GoodsList/GetPageList',
                headData: [
                    { label: "商品编码", name: "g_code", width: 160, align: "left" },
                    { label: "商品名称", name: "g_name", width: 160, align: "left" },
                    {
                        label: "商品类型", name: "g_kind", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'GoodsType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "保质时间", name: "g_period", width: 160, align: "left" },
                    { label: "价格", name: "g_price", width: 160, align: "left" },
                    {
                        label: "单位", name: "g_unit", width: 160, align: "left",
                        formatterAsyns:function(callback,value,row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'GoodsUnit',
                                callback:function(_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    //{ label: "供应商编码", name: "G_SupplyCode", width: 160, align: "left"},
                    { label: "供应商名称", name: "supplyname", width: 160, align: "left" },
                    { label: "数量", name: "g_qty", width: 160, align: "left" },
                    { label: "上限预警比例", name: "g_super", width: 160, align: "left" },
                    { label: "下限预警比例", name: "g_lower", width: 160, align: "left" },
                    { label: "备注", name: "G_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "g_createby", width: 160, align: "left" },
                    { label: "添加时间", name: "g_createdate", width: 160, align: "left" },
                    { label: "修改人", name: "g_updateby", width: 160, align: "left" },
                    { label: "修改时间", name: "g_updatedate", width: 160, align: "left" }

                ],
                mainId:'ID',
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