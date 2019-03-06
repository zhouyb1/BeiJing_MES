/* * 创建人：超级管理员
 * 日  期：2019-03-06 17:41
 * 描  述：配方表
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
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/BomHead/Form',
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
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/BomHead/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/BomHead/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/BomHead/GetPageList',
                headData: [
                    { label: "工艺代码", name: "B_Record", width: 160, align: "left"},
                    { label: "工序号", name: "B_ProCode", width: 160, align: "left"},
                    { label: "配方编码", name: "B_FormulaCode", width: 160, align: "left"},
                    { label: "配方名称", name: "B_FormulaName", width: 160, align: "left"},
                    { label: "物料编码", name: "B_GoodsCode", width: 160, align: "left"},
                    { label: "物料名称", name: "B_GoodsName", width: 160, align: "left"},
                    { label: "是否生效", name: "B_Avail", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                             ayma.clientdata.getAsync('dataItem', {
                                 key: value,
                                 itemCode: 'YesOrNo',
                                 callback: function (_data) {
                                     callback(_data.F_ItemName);
                                 }
                             });
                        }},
                    { label: "开始时间", name: "B_StartTime", width: 160, align: "left"},
                    { label: "截止时间", name: "B_EndTime", width: 160, align: "left"},
                    { label: "单位", name: "B_Unit", width: 160, align: "left"},
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
