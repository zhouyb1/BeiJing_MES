﻿/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.17
 * 描 述：表单设计	
 */
var refreshGirdData; // 更新数据
var categoryId;
var nowschemeId;

var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 左侧数据加载
            $('#am_left_tree').amtree({
                url: top.$.rootUrl + '/AM_SystemModule/DataItem/GetDetailTree',
                param: { itemCode: 'FormSort' },
                nodeClick: function (item) {
                    categoryId = item.value;
                    $('#titleinfo').text(item.text);
                    page.search();
                }
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'Form',
                    title: '自定义表单新增',
                    url: top.$.rootUrl + '/AM_FormModule/Custmerform/Form?categoryId=' + categoryId,
                    width: 1200,
                    height: 700,
                    maxmin: true,
                    btn:null
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('F_Id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'Form',
                        title: '自定义表单编辑',
                        url: top.$.rootUrl + '/AM_FormModule/Custmerform/Form?categoryId=' + categoryId + "&keyValue=" + keyValue,
                        width: 1200,
                        height: 700,
                        maxmin: true,
                        btn: null
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('F_Id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/AM_FormModule/Custmerform/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            // 预览表单
            $('#am_preview').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('F_Id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'custmerForm_PreviewForm',
                        title: '预览当前表单',
                        url: top.$.rootUrl + '/AM_FormModule/Custmerform/PreviewForm?schemeInfoId=' + keyValue,
                        width: 800,
                        height: 600,
                        maxmin: true,
                        btn: null
                    });
                }
            });

            // 启用
            $('#am_enabled').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('F_Id');
                var enabledMark = $('#girdtable').jfGridValue('F_EnabledMark');

                var type = $('#girdtable').jfGridValue('F_Type');
                if (type == 2) {
                    ayma.alert.warning('草稿模板不能启用!');
                    return false;
                }

                if (ayma.checkrow(keyValue)) {
                    if (enabledMark != 1) {
                        ayma.layerConfirm('是否确认启用该表单模板！', function (res) {
                            if (res) {
                                ayma.postForm(top.$.rootUrl + '/AM_FormModule/Custmerform/UpDateSate', { keyValue: keyValue, state: 1 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        ayma.alert.warning('该表单已启用!');
                    }
                }
            });
            // 禁用
            $('#am_disabled').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('F_Id');
                var enabledMark = $('#girdtable').jfGridValue('F_EnabledMark');

                var type = $('#girdtable').jfGridValue('F_Type');
                if (type == 2) {
                    ayma.alert.warning('草稿模板不能禁用!');
                    return false;
                }

                if (ayma.checkrow(keyValue)) {
                    if (enabledMark == 1) {
                        ayma.layerConfirm('是否确认禁用该表单模板！', function (res) {
                            if (res) {
                                ayma.postForm(top.$.rootUrl + '/AM_FormModule/Custmerform/UpDateSate', { keyValue: keyValue, state: 0 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        ayma.alert.warning('该表单已禁用!');
                    }
                }
            });
            // 查看历史记录
            $('#am_history').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('F_Id');
                var Name = $('#girdtable').jfGridValue('F_Name');
                nowschemeId = $('#girdtable').jfGridValue('F_SchemeId');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'HistoryForm',
                        title: '表单模板历史记录-' + Name,
                        url: top.$.rootUrl + '/AM_FormModule/Custmerform/HistoryForm?&keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        maxmin: true,
                        btn: null
                    });
                }
            });

            /*分类管理*/
            $('#am_category').on('click', function () {
                ayma.layerForm({
                    id: 'ClassifyIndex',
                    title: '分类管理',
                    url: top.$.rootUrl + '/AM_SystemModule/DataItem/DetailIndex?itemCode=FormSort',
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null,
                    end: function () {
                        ayma.clientdata.update('dataItem');
                        location.reload();
                    }
                });
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/AM_FormModule/Custmerform/GetPageList',
                headData: [
                    { label: "名称", name: "F_Name", width: 150, align: "left" },
                    {
                        label: "分类", name: "F_Category", width: 150, align: "center",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'FormSort',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: "状态", name: "F_EnabledMark", width: 50, align: "center",
                        formatter: function (cellvalue, row) {
                            if (row.F_Type == 1) {
                                if (cellvalue == 1) {
                                    return '<span class=\"label label-success\" style=\"cursor: pointer;\">启用</span>';
                                } else if (cellvalue == 0) {
                                    return '<span class=\"label label-default\" style=\"cursor: pointer;\">禁用</span>';
                                }
                            }
                            else {
                                return '<span class=\"label label-info\" style=\"cursor: pointer;\">草稿</span>';
                            }
                        }
                    },
                    { label: "创建人", name: "F_CreateUserName", width: 100, align: "center" },
                    {
                        label: "创建时间", name: "F_CreateDate", width: 150, align: "left",
                        formatter: function (cellvalue) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "备注", name: "F_Description", width: 500, align: "left" }
                ],
                mainId: 'F_Id',
                reloadSelected: true,
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.category = categoryId;
            $('#girdtable').jfGridSet('reload', { param: param });
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        page.search();
    }

    page.init();
}