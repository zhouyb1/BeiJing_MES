/* * 创建人：超级管理员
 * 日  期：2019-11-08 14:02
 * 描  述：财务月结
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
                    url: top.$.rootUrl + '/MesDev/MonthBalance/Form',
                    width: 400,
                    height: 300,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            
            //月结
            $('#am_post').on('click', function () {
                var month = $('#girdtable').jfGridValue('M_Months');
                var status = $('#girdtable').jfGridValue('M_Status');
            
                if (ayma.checkrow(month)) {
                    if (status == 1) {
                        ayma.alert.warning('无法重复月结！');
                        return false;
                    } else {
                        ayma.layerConfirm('是否确认月结？', function (res) {
                            if (res) {
                                ayma.deleteForm(top.$.rootUrl + '/MesDev/MonthBalance/PostOrCancel', { month: month,type:1}, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                }
            });

            //反月结
            $("#am_cancel").on('click', function () {
                var month = $('#girdtable').jfGridValue('M_Months');
                var status = $('#girdtable').jfGridValue('M_Status');

                if (ayma.checkrow(month)) {
                    if (status == 2) {
                        ayma.alert.warning('未月结凭证，无法反月结！');
                        return false;
                    } else {
                        ayma.layerConfirm('是否确认反月结？', function (res) {
                            if (res) {
                                ayma.deleteForm(top.$.rootUrl + '/MesDev/MonthBalance/PostOrCancel', { month: month, type: 2}, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                }
            });

            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/MonthBalance/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/MonthBalance/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/MonthBalance/GetPageList',
                headData: [
                    { label: "主键", name: "ID", width: 160, align: "left",hidden:true},
                    { label: "月结日期", name: "M_Months", width: 100, align: "left" },
                    { label: "月结人", name: "M_MonthBalanceBy", width: 120, align: "left" },
                    { label: "月结时间", name: "M_MonthBalanceTime", width: 120, align: "left"},
                    {
                        label: "状态", name: "M_Status", width: 100, align: "left",
                        formatterAsync: function (callback, value, row) {
                            if (value != 1) {
                                callback("<span class='label label-default'>待月结</span>");
                            } else {
                                callback("<span class='label label-info'>已月结</span>");
                            }
                        }
                    },
                    { label: "备注", name: "M_Remark", width: 250, align: "left"}
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
