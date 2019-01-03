﻿/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.17
 * 描 述：选项卡编辑	
 */
var selectedRow;


var bootstrap = function ($, ayma) {
    "use strict";
    var tabList = top.layer_CustmerCodeIndex.tabList[0].ChildNodes;

    console.log(tabList);


    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 新增
            $('#am_add').on('click', function () {
                selectedRow = null;
                ayma.layerForm({
                    id: 'TabEditForm',
                    title: '添加选项卡',
                    url: top.$.rootUrl + '/AM_CodeGeneratorModule/TemplatePC/TabEditForm',
                    width: 400,
                    height: 200,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            tabList.push(data);
                            tabList = tabList.sort(function (a, b) {
                                return parseInt(a.sort) - parseInt(b.sort);
                            });
                            $('#girdtable').jfGridSet('refreshdata', { rowdatas: tabList });
                        });
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                var keyValue = $('#girdtable').jfGridValue('id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'TabEditForm',
                        title: '编辑选项卡',
                        url: top.$.rootUrl + '/AM_CodeGeneratorModule/TemplatePC/TabEditForm',
                        width: 400,
                        height: 200,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                $.each(tabList, function (id, item) {
                                    if (item.id == data.id) {
                                        tabList[id] = data;
                                        return false;
                                    }
                                });
                                tabList = tabList.sort(function (a, b) {
                                    return parseInt(a.sort) - parseInt(b.sort);
                                });
                                $('#girdtable').jfGridSet('refreshdata', { rowdatas: tabList });
                            });
                        }
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                if (tabList.length == 1) {
                    ayma.alert.warning('必须保留一个选项卡！');
                    return false;
                }
                var _id = $('#girdtable').jfGridValue('id');
                if (ayma.checkrow(_id)) {
                    ayma.layerConfirm('是否确认删除该选项卡', function (res, index) {
                        if (res) {
                            $.each(tabList, function (id, item) {
                                if (item.id == _id) {
                                    tabList.splice(id, 1);
                                    return false;
                                }
                            });
                            $('#girdtable').jfGridSet('refreshdata', { rowdatas: tabList });
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#girdtable').jfGrid({
                headData: [
                    { label: "名称", name: "text", width: 340, align: "left" },
                    { label: "序号", name: "sort", width: 80, align: "center" }
                ],
                mainId: 'id',
                reloadSelected: true
            });

            $('#girdtable').jfGridSet('refreshdata', { rowdatas: tabList });
        }
    };
    page.init();
}