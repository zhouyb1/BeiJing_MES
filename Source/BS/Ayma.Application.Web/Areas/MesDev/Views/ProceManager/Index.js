﻿/* * 创建人：超级管理员
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/ProceManager/DeleteRecordForm', { keyValue: keyValue }, function () {
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
