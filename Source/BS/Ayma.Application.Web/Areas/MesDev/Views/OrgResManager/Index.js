﻿/* * 创建人：超级管理员
 * 日  期：2019-03-18 15:14
 * 描  述：组装与拆分单据制作
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
            $('#O_SecGoodsName').select({
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
            //绑定工序
            $('#O_ProCode').select({
                type: 'default',
                value: 'P_ProNo',
                text: 'P_ProName',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProceList',
                // 访问数据接口参数
                param: {}
            });

            $("#O_StockCode").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            });
            $('#O_Status').DataItemSelect({ code: 'ProOutStatus' });

            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'MasterIndexForm',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/OrgResManager/Form?formId=MasterIndexForm',
                    width: 1000,
                    height: 750,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $('#girdtable').jfGridValue('O_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MasterIndexForm',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/OrgResManager/Form?keyValue=' + keyValue + "&formId=MasterIndexForm&status=" + status,
                        width: 1000,
                        height: 750,
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
                var status = $('#girdtable').jfGridValue('O_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MasterIndexForm',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/OrgResManager/Form?keyValue=' + keyValue + "&formId=MasterIndexForm&status=" + status,
                        width: 900,
                        height: 750,
                        maxmin: true,
                        btn: status == 2 ? null : ['确认', '关闭'],
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 审核单据
            $('#am_auditing').on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("O_Status");
                if (status != "1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_OrgResHead', field: 'O_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 删除单据
            $('#am_delete').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $("#girdtable").jfGridValue("O_Status");
                if (ayma.checkrow(keyValue)) {
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/OrgResManager/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            //提交单据
            $('#am_post').on('click', function () {
                var orderNo = $('#girdtable').jfGridValue('O_OrgResNo');
                var status = $('#girdtable').jfGridValue('O_Status');
                if (status == "1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OrgRes_PostNew', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }

            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OrgResNo');
                var status = $("#girdtable").jfGridValue("O_Status");
                if (ayma.checkrow(keyValue, true)) {
                    if (status != "2") {
                        ayma.alert.error("单据未审核");
                        return false;
                    }
                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '物料组装单打印',
                        url: top.$.rootUrl + '/MesDev/OrgResManager/PrintReport?keyValue=' + keyValue + "&report=OrgResReport&data=OrgRes",
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                } 
            });
            // 预览打印
            $('#am_printview').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OrgResNo');
                if (ayma.checkrow(keyValue)) {

                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '物料组装单打印',
                        url: top.$.rootUrl + '/MesDev/OrgResManager/PrintViewReport?keyValue=' + keyValue + "&report=OrgResReport",
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/OrgResManager/GetPageList',
                headData: [
                    {
                        label: "状态", name: "O_Status", width: 90, align: "center", formatterAsync: function (callback, value, row) {
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
                    { label: "单据编号", name: "O_OrgResNo", width: 130, align: "center", sort: true },
                    { label: "工序号", name: "O_ProCode", width: 90, align: "center" },
                    { label: "日耗库", name: "O_StockName", width: 160, align: "center" },
                    { label: "日耗库编码", name: "O_StockCode", width: 90, align: "center" },
                    {
                        label: "单据时间", name: "O_OrderDate", width: 100, align: "center", sort: true ,
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "创建时间", name: "O_CreateDate", width: 130, align: "center", sort: true },
                    { label: "添加人", name: "O_CreateBy", width: 90, align: "center" },
                    { label: "备注", name: "O_Remark", width: 150, align: "center" }

                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'O_CreateDate',
                sord: 'desc'
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
