/* * 创建人：超级管理员
 * 日  期：2019-03-06 12:03
 * 描  述：车间管理
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            page.dbClick();
        },
        bind: function () {
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/WorkShopManager/Form',
                    width: 600,
                    height: 400,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('Id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/WorkShopManager/Form?keyValue=' + keyValue,
                        width: 600,
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
                var keyValue = $('#girdtable').jfGridValue('Id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/WorkShopManager/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/WorkShopManager/GetPageList',
                headData: [
                    { label: "车间编码", name: "W_Code", width: 160, align: "left"},
                    { label: "车间名称", name: "W_Name", width: 160, align: "left"},
                    { label: "备注", name: "W_Remark", width: 160, align: "left"},
                    { label: "创建日期", name: "CreateDate", width: 160, align: "left"},
                    { label: "创建用户", name: "CreateUserName", width: 160, align: "left"},
                    { label: "修改日期", name: "ModifyDate", width: 160, align: "left"},
                    { label: "修改用户", name: "ModifyUserName", width: 160, align: "left"},
                ],
                mainId:'Id',
                reloadSelected: true,
                isPage: true,
                sidx: "W_Code",
                sord: "asc"
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        },

        dbClick: function () {
            $('#girdtable').on('dblclick', function() {
                var keyValue = $('#girdtable').jfGridValue('Id');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/WorkShopManager/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        maxmin: true,
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
