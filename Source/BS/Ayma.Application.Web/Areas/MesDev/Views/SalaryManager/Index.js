/* * 创建人：超级管理员
 * 日  期：2019-03-06 14:22
 * 描  述：工资参数设定
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
            $('#S_UserKind').DataItemSelect({ code: 'EmployeeKind' });
            $('#S_WorkKind').DataItemSelect({ code: 'WorkType' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/SalaryManager/Form',
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
                        url: top.$.rootUrl + '/MesDev/SalaryManager/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/SalaryManager/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/SalaryManager/GetPageList',
                headData: [
                    {
                        label: "用工类型", name: "S_UserKind", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                             ayma.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'EmployeeKind',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "工作类型", name: "S_WorkKind", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                             ayma.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'WorkType',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "单位时间", name: "S_TimeUnit", width: 160, align: "left"},
                    { label: "报酬(加班按小时)", name: "S_Pay", width: 160, align: "left"},
                    { label: "备注", name: "S_Remark", width: 160, align: "left"},
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
        },
        dbClick: function() {
            $('#girdtable').on('dblclick', function() {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/SalaryManager/Form?keyValue=' + keyValue,
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
