/* * 创建人：超级管理员
 * 日  期：2019-03-06 14:55
 * 描  述：工序管理
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
                var id = $('#girdtable').jfGridValue('ID');
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/ProceManager/Form?parentId=' + id,
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
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/ProceManager/Form?keyValue=' + keyValue,
                        width: 700,
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
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/ProceManager/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/ProceManager/GetTreeList',
                headData: [
                    { label: "工艺代码", name: "P_RecordCode", width: 160, align: "left"},
                    { label: "工序号", name: "P_ProNo", width: 160, align: "left"},
                    { label: "工序名称", name: "P_ProName", width: 160, align: "left"},
                    { label: "车间", name: "P_WorkShop", width: 160, align: "left"},
                    { label: "备注", name: "P_Remark", width: 160, align: "left"},
                ],
                isTree: true,
                mainId: 'ID',
                parentId: 'P_ParentId',
                reloadSelected: true,
                isShowNum: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        },

        dbClick: function () {
            $('#girdtable').on('dblclick', function() {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/ProceManager/Form?keyValue=' + keyValue,
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
