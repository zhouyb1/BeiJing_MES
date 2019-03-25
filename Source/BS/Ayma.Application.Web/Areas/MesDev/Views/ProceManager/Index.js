/* * 创建人：超级管理员
 * 日  期：2019-03-06 14:55
 * 描  述：工序管理
 */
var refreshGirdData;
var refreshSubGirdData;//子列表刷新
var $subgridTable;//子列表
var recordEdit;//编辑子列表
var recordDel;//删除子列表
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
            // 新增工艺
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增工艺',
                    url: top.$.rootUrl + '/MesDev/ProceManager/Form',
                    width: 500,
                    height: 300,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 新增工序
            $('#am_addProce').on('click', function () {
                var record = $('#girdtable').jfGridValue('R_Record');
                if (ayma.checkrow(record)) {
                    ayma.layerForm({
                        id: 'ProceForm',
                        title: '新增工序',
                        url: top.$.rootUrl + '/MesDev/ProceManager/ProceForm?record=' + record,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 编辑工艺
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑工艺',
                        url: top.$.rootUrl + '/MesDev/ProceManager/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/ProceManager/DeleteRecordAndProceForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/ProceManager/GetRecordList',
                headData: [
                    { label: "工艺代码", name: "R_Record", width: 200, align: "left" },
                    { label: "工艺名称", name: "R_Name", width: 200, align: "left" },
                    { label: "成品物料编码", name: "R_GoodsCode", width: 200, align: "left" },
                    { label: "成品物料名称", name: "GoodsName", width: 200, align: "left" }
                ],
                mainId: 'ID',
                reloadSelected: false,
                sidx: "R_Record",
                isPage: true,
                isSubGrid: true,             // 是否有子表editType 
                subGridRowExpanded: function (subgridId, row) {// 子表展开后调用函数

                    var id = row.R_Record;
                    var subgridTableId = subgridId + "_t";
                    $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                    $subgridTable = $("#" + subgridTableId);
                    $subgridTable.jfGrid({
                        url: top.$.rootUrl + '/MesDev/ProceManager/GetProceListBy?record=' + id,
                        headData: [
                            {
                                label: '操作', name: '', index: '', width: 120, align: 'left', frozen: true,
                                formatter: function (value, grid, rows) {
                                    var result = "<a href=\"javascript:;\" style=\"color:#f60\" onclick=\"recordEdit('" + grid.ID + "')\">编辑</a>" + "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:;\" style=\"color:#f60\" onclick=\"recordDel('" + grid.ID + "')\">删除</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                    return result;
                                }
                            },
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
                                                callback("<span class='label label-danger'>" + _data.text + "</span>");
                                            }
                                        }
                                    });
                                }
                            },
                            { label: '备注', name: 'P_Remark', width: 200, align: 'left' },

                        ],
                        mainId: 'ID',
                        reloadSelected: false
                    }).jfGridSet("reload");
                }
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    //编辑工序
    recordEdit = function (keyValue) {
        if (ayma.checkrow(keyValue)) {
            ayma.layerForm({
                id: 'form',
                title: '编辑工序',
                url: top.$.rootUrl + '/MesDev/ProceManager/ProceForm?keyValue=' + keyValue,
                width: 700,
                height: 500,
                maxmin: true,
                callBack: function (id) {
                    return top[id].acceptClick(refreshSubGirdData);
                }
            });
        }
    }
    //删除工序
    recordDel = function (keyValue) {
        if (ayma.checkrow(keyValue)) {
            ayma.layerConfirm('是否确认删除该项！', function (res) {
                if (res) {
                    ayma.deleteForm(top.$.rootUrl + '/MesDev/ProceManager/DeleteForm', { keyValue: keyValue }, function () {
                        refreshSubGirdData();
                    });
                }
            });
        }
    }
    //子列表刷新
    refreshSubGirdData = function () {
        $subgridTable.jfGridSet("reload");
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
