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
                    id: 'ProceForm',
                    title: '新增工序',
                    url: top.$.rootUrl + '/MesDev/ProceManager/ProceForm',
                    width: 500,
                    height: 300,
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
                        title: '编辑工序',
                        url: top.$.rootUrl + '/MesDev/ProceManager/ProceForm?keyValue=' + keyValue,
                        width: 500,
                        height: 300,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/ProceManager/DeleteProceForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/ProceManager/GetProceList',
                headData: [
                    { label: "工序号", name: "P_ProNo", width: 160, align: "left" },
                    { label: "工序名称", name: "P_ProName", width: 200, align: "left" },
                    { label: '车间', name: 'P_WorkShop', width: 200, align: 'left' },
                    {
                        label: "是否最后一道工序", name: "P_Kind", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'YesOrNo',
                                callback: function (_data) {
                                    console.log(value)
                                    if (value == 1) {
                                        callback("<span class='label label-success'>" + _data.text + "</span>");
                                    } else {
                                        callback("<span class='label label-default'>" + _data.text + "</span>");
                                    }
                                }
                            });
                        }
                    },
                   { label: '备注', name: 'P_Remark', width: 200, align: 'left' },
                ],
                mainId: 'ID',
                reloadSelected: false,
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
