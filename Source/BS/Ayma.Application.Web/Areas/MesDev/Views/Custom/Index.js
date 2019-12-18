/* * 创建人：超级管理员
 * 日  期：2019-11-06 16:30
 * 描  述：客户表
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
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 180, 600);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                selectedRow = null;
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/Custom/Form',
                    width: 700,
                    height: 400,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/Custom/Form?keyValue=' + keyValue,
                        width: 700,
                        height: 400,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Custom/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Custom/GetPageList',
                headData: [
                        { label: '客户编码', name: 'C_Code', width: 200, align: "left" },
                        { label: '客户名称', name: 'C_Name', width: 200, align: "left" },
                        { label: '联系人', name: 'S_Person', width: 200, align: "left" },
                        { label: '联系电话', name: 'S_Telephone', width: 200, align: "left" },
                        { label: '法人', name: 'S_Corp', width: 200, align: "left" },
                        { label: '地址', name: 'S_Address', width: 200, align: "left" },
                        { label: '税号', name: 'S_TaxCode', width: 200, align: "left" },
                        { label: '资质1', name: 'S_Effect1', width: 200, align: "left" },
                        { label: '资质2', name: 'S_Effect2', width: 200, align: "left" },
                        { label: '资质3', name: 'S_Effect3', width: 200, align: "left" },
                        { label: '资质4', name: 'S_Effect4', width: 200, align: "left" },
                        { label: '资质5', name: 'S_Effect5', width: 200, align: "left" },
                        { label: '添加人', name: 'C_CreateBy', width: 200, align: "left" },
                        { label: '添加时间', name: 'C_CreateDate', width: 200, align: "left" },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'C_Code',
                sord: 'asc'
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
