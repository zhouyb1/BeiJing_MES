﻿/* * 创建人：超级管理员
 * 日  期：2019-11-08 13:59
 * 描  述：消耗物料
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 250, 480);
            $('#M_GoodsName').select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址 
                url: top.$.rootUrl + '/MesDev/Tools/GetGoodsList',
                // 访问数据接口参数
                param: {}
            });
            $('#E_Status').DataItemSelect({ code: 'ProOutStatus' });
            $('#E_StockCode').DataSourceSelect({ code: 'StockList', value: 's_code', text: 's_name' })
            //$('#MonthBalance').DataItemSelect({ code: 'MonthBalance' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/Mes_ExpendManager/Form?formId=form',
                    width: 1000,
                    height: 700,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $('#girdtable').jfGridValue('E_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/Mes_ExpendManager/Form?status=' + status + '&keyValue=' + keyValue + '&formId=form',
                        width: 1000,
                        height: 700,
                        maxmin: true,
                        btn: status == 2 ? null : ['确认', '关闭'],
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 双击编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $('#girdtable').jfGridValue('E_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/Mes_ExpendManager/Form?status=' + status + '&keyValue=' + keyValue + '&formId=form',
                        width: 1000,
                        height: 700,
                        maxmin: true,
                        btn: status == 2 ? null : ['确认', '关闭'],
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("E_Status");
                if (status == "2") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_ExpendHead', field: 'E_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $('#am_post').on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("E_Status");
                var orderNo = $("#girdtable").jfGridValue("E_ExpendNo");
                if (status == 1) {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_Expend_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                var status = $("#girdtable").jfGridValue("E_Status");
                var orderNo = $("#girdtable").jfGridValue("E_ExpendNo");
                var keyValue = $("#girdtable").jfGridValue("ID");
                if (ayma.checkrow(orderNo)) {
                    if (status == 2) {
                        ayma.alert.error("已审核的不能删除");
                        return false;
                    }
                    ayma.layerConfirm('确认删除单据？', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_Expend_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('E_ExpendNo');
                var status = $("#girdtable").jfGridValue("E_Status");
                if (ayma.checkrow(keyValue)) {
                    if (status != "2") {
                        ayma.alert.error("单据未审核");
                        return false;
                    }
                    ayma.layerForm({
                        id: 'ExpendManagerReport',
                        title: '消耗单打印',
                        url: top.$.rootUrl + '/MesDev/Mes_ExpendManager/PrintReport?keyValue=' + keyValue + "&report=MesExpendManagerReport&data=ExpendManager",
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                } else {
                    ayma.alert.error("请选择要打印的单据！");
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_ExpendManager/GetPageList',
                headData: [
                    { label: "主键", name: "ID", width: 160, align: "center", hidden: true },
                    {
                        label: "状态", name: "E_Status", width: 90, align: "center",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'ProOutStatus',
                                callback: function (_data) {
                                    if (value == 1) {
                                        callback("<span class='label label-default'>" + _data.text + "</span>");
                                    } else if (value == 2) {
                                        callback("<span class='label label-info'>" + _data.text + "</span>");
                                    } else if (value == 3) {
                                        callback("<span class='label label-success'>" + _data.text + "</span>");
                                    } else {
                                        callback("<span class='label label-danger'>" + _data.text + "</span>");
                                    }
                                }
                            });
                        }
                    },
                    { label: "单据编号", name: "E_ExpendNo", width: 130, align: "center"},
                    { label: "仓库名称", name: "E_StockName", width: 130, align: "center" },
                    { label: "仓库编码", name: "E_StockCode", width: 90, align: "center" },
                    {
                        label: "单据时间", name: "E_OrderDate", width: 100, align: "center", sort: true,
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "创建时间", name: "E_CreateDate", width: 130, align: "center", sort: true },
                    { label: "添加人", name: "E_CreateBy", width: 90, align: "center" },
                    { label: "修改时间", name: "E_UpdateDate", width: 130, align: "center" },
                    { label: "修改人", name: "E_UpdateBy", width: 90, align: "center" },
                    { label: "备注", name: "E_Remark", width: 130, align: "left" }
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            param.OrderDate_S = $("#OrderDate_S").val();
            param.OrderDate_E = $("#OrderDate_E").val();
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
