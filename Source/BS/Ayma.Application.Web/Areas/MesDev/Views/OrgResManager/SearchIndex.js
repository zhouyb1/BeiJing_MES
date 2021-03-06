﻿/* * 创建人：超级管理员
 * 日  期：2019-03-18 15:14
 * 描  述：组装与拆分单据制作
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var type;
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                //queryJson.type = "report";
                page.search(queryJson);
            }, 320, 400);
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
            //绑定日耗库
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
            })

            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OrgResNo');
                if (ayma.checkrow(keyValue, true)) {
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
            // 查看详情
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MasterIndexForm',
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/OrgResManager/SearchForm?keyValue=' + keyValue + "&formId=MasterIndexForm",
                        width: 1000,
                        height: 750,
                        maxmin: true,
                        btn:null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //双击详情
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MasterIndexForm',
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/OrgResManager/SearchForm?keyValue=' + keyValue + "&formId=MasterIndexForm",
                        width: 1000,
                        height: 750,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OrgResNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OrgRes_CancelNew', type: 2 }, function () {
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
                url: top.$.rootUrl + '/MesDev/OrgResManager/OrgResManagerList',
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
                    { label: "单据编号", name: "O_OrgResNo", width: 130, align: "center" },
                    { label: "工序", name: "O_ProCode", width: 90, align: "center" },
                    { label: "日耗仓名称", name: "O_StockName", width: 120, align: "center" },
                    { label: "日耗仓编码", name: "O_StockCode", width: 90, align: "center" },
                    { label: "备注", name: "O_Remark", width: 130, align: "center" },
                    {
                        label: "单据时间", name: "O_OrderDate", width: 100, align: "center",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "添加人", name: "O_CreateBy", width: 90, align: "center" },
                    { label: "添加时间", name: "O_CreateDate", width: 130, align: "center" },
                    { label: "提交人", name: "O_UploadBy", width: 130, align: "center" },
                    { label: "提交时间", name: "O_UploadDate", width: 130, align: "center" },
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
            param.OrderDate_S = $("#OrderDate_S").val();//新增单据时间
            param.OrderDate_E = $("#OrderDate_E").val();
            //param.type = type;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
