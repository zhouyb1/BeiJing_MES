/* * 创建人：超级管理员
 * 日  期：2019-01-07 12:47
 * 描  述：仓库列表
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
            $('#S_Kind').DataItemSelect({ code: 'StockType' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增仓库',
                    url: top.$.rootUrl + '/MesDev/StockList/Form',
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
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑仓库',
                        url: top.$.rootUrl + '/MesDev/StockList/Form?keyValue=' + keyValue,
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
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/StockList/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //导入仓库表
            $('#am_import').on('click', function () {
                ayma.layerForm({
                    id: 'ImportForm',
                    title: '导入仓库表',
                    url: top.$.rootUrl + '/MesDev/StockList/ImportForm?formId=form',
                    width: 800,
                    height: 600,
                    maxmin: true,
                    btn: null,
                    callBack: function () {
                    }
                });
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/StockList/GetPageList',
                headData: [
                    { label: "仓库编码", name: "S_Code", width: 160, align: "left"},
                    { label: "仓库名称", name: "S_Name", width: 160, align: "left"},
                    { label: "仓库类型", name: "S_Kind", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                             ayma.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'StockType',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "仓库负责人", name: "S_Peson", width: 160, align: "left"},
                    { label: "添加人", name: "S_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "S_CreateDate", width: 160, align: "left"},
                    { label: "修改人", name: "S_UpdateBy", width: 160, align: "left"},
                    { label: "修改时间", name: "S_UpdateDate", width: 160, align: "left"},
                    { label: "备注", name: "S_Remark", width: 160, align: "left"},
                ],
                mainId:'ID',
                isPage: true,
                sidx: "S_Code",
                sord: "ASC"
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
