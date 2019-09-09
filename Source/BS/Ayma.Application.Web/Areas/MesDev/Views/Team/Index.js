/* * 创建人：超级管理员
 * 日  期：2019-06-27 15:26
 * 描  述：班组表
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
            }, 220, 400);
           
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
                    url: top.$.rootUrl + '/MesDev/Team/Form',
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
                        url: top.$.rootUrl + '/MesDev/Team/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Team/DeleteForm', { keyValue: keyValue}, function () {
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Team/GetPageList',
                headData: [
                        //{ label: 'ID', name: 'ID', width: 200, align: "left" },
                        { label: '班组编码', name: 'T_Code', width: 200, align: "left" },
                        { label: '班组名称', name: 'T_Name', width: 200, align: "left" },
                        { label: '车间编码', name: 'T_WorkShopCode', width: 200, align: "left" },
                        { label: '负责人', name: 'T_UserName', width: 200, align: "left" },
                         { label: '仓库名称', name: 'S_Name', width: 200, align: "left" },
                         //{ label: '仓库编号', name: 'T_StockCode', width: 200, align: "left" },
                        { label: '备注', name: 'T_Remark', width: 200, align: "left" },
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
