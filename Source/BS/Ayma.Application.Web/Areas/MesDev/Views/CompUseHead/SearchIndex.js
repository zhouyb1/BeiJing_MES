/* * 创建人：超级管理员
 * 日  期：2019-07-04 16:14
 * 描  述：强制使用记录单据
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
            }, 250, 400);
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

            //仓库
            $('#C_StockName').select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            });
            //车间
            $('#C_WorkShopName').select({
                type: 'default',
                value: 'W_Name',
                text: 'W_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetWorkShopList',
                // 访问数据接口参数
                param: {}
            });
            $('#C_Status').DataItemSelect({ code: 'CompUserStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
           
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('C_No');
                if (ayma.checkrow(keyValue, true)) {
                    ayma.layerForm({
                        id: 'CompUseHead',
                        title: '强制使用记录单',
                        url: top.$.rootUrl + '/MesDev/CompUseHead/PrintReport?keyValue=' + keyValue + "&report=CompUseHeadReport&data=CompUseHead",
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
                var status = $('#girdtable').jfGridValue('C_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'compUserForm',
                        title: '查看详情',
                        url: top.$.rootUrl + '/MesDev/CompUseHead/SearchForm?formId=compUserForm&keyValue=' + keyValue + '+&status=' + status,
                        width: 900,
                        height: 700,
                        btn: null,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //双击详情
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $('#girdtable').jfGridValue('C_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'compUserForm',
                        title: '查看详情',
                        url: top.$.rootUrl + '/MesDev/CompUseHead/SearchForm?formId=compUserForm&keyValue=' + keyValue + '+&status=' + status,
                        width: 900,
                        height: 700,
                        btn: null,
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
                url: top.$.rootUrl + '/MesDev/CompUseHead/CompUseHeadList',
                headData: [
                    
                    {
                        label: "状态", name: "C_Status", width: 90, align: "center",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'CompUserStatus',
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
                    { label: "单据编号", name: "C_No", width: 130, align: "center" },
                    { label: "车间编号", name: "C_WorkShop", width: 100, align: "center" },
                    { label: "车间名称", name: "C_WorkShopName", width: 130, align: "center" },
                    { label: "仓库编号", name: "C_StockCode", width: 100, align: "center" },
                    { label: "仓库名称", name: "C_StockName", width: 130, align: "center" },
                    {
                        label: "单据时间", name: "C_OrderDate", width: 100, align: "center",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "添加人", name: "C_CreateBy", width: 100, align: "center" },
                    { label: "添加人", name: "C_CreateDate", width: 100, align: "center" },
                    { label: "提交人", name: "C_UploadBy", width: 100, align: "center" },
                    { label: "提交时间", name: "C_UploadDate", width: 100, align: "center" },
                    { label: "备注", name: "C_Remark", width: 130, align: "center" },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'C_CreateDate',
                sord: 'desc'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
