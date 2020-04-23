/* * 创建人：超级管理员
 * 日  期：2019-11-07 13:51
 * 描  述：其它入库单
 */
var selectedRow;
var refreshGirdData;
var startTime;
var endTime;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 时间搜索框
            $('#datesearch').amdate({
                dfdata: [
                    { name: '今天', begin: function () { return ayma.getDate('yyyy-MM-dd') }, end: function () { return ayma.getDate('yyyy-MM-dd') } },
                    { name: '近7天', begin: function () { return ayma.getDate('yyyy-MM-dd', 'd', -6) }, end: function () { return ayma.getDate('yyyy-MM-dd') } },
                    { name: '近1个月', begin: function () { return ayma.getDate('yyyy-MM-dd', 'm', -1) }, end: function () { return ayma.getDate('yyyy-MM-dd') } },
                    { name: '近3个月', begin: function () { return ayma.getDate('yyyy-MM-dd', 'm', -3) }, end: function () { return ayma.getDate('yyyy-MM-dd') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 180, 500);
            $('#O_Status').DataItemSelect({ code: 'MaterInStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
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
            $('#O_StockName').select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetOriginalStockList',
                // 访问数据接口参数
                param: {}

            });
            // 新增
            $('#am_add').on('click', function () {
                selectedRow = null;
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/Form?formId=form',
                    width: 900,
                    height: 600,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('O_Status');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/Form?keyValue=' + keyValue + '&status=' + statu + '&formId=form',
                        width: 900,
                        height: 600,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 双击编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('O_Status');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/Form?keyValue=' + keyValue + '&status=' + statu,
                        width: 900,
                        height: 600,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OtherInNo');
                if (ayma.checkrow(keyValue, true)) {
                    ayma.layerForm({
                        id: 'OtherReport',
                        title: '其它入库单打印',
                        url: top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/PrintReport?keyValue=' + keyValue + "&report=OtherReport&data=Other",
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OtherInNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OtherIn_Cancel', type: 2 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/GetPostPageList',
                headData: [
                                {
                                    label: "单据状态", name: "O_Status", width: 90, align: "center",
                                    formatterAsync: function (callback, value, row) {
                                        ayma.clientdata.getAsync('dataItem', {
                                            key: value,
                                            code: 'MaterInStatus',
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
                        { label: '单据编号', name: 'O_OtherInNo', width: 130, align: "center" },
                        { label: '仓库编码', name: 'O_StockCode', width: 80, align: "center" },
                        { label: '仓库名称', name: 'O_StockName', width: 130, align: "center" },
                        { label: '备注', name: 'O_Remark', width: 130, align: "center" },
                        {
                            label: '单据时间', name: 'O_OrderDate', width: 100, align: "center",
                            formatter: function (cellvalue, options, rowObject) {
                                return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                            }
                        },
                        { label: '添加人', name: 'O_CreateBy', width: 100, align: "center" },
                        { label: '创建时间', name: 'O_CreateDate', width: 130, align: "center" },
                        { label: '修改人', name: 'O_UpdateBy', width: 90, align: "center" },
                        { label: '修改时间', name: 'O_UpdateDate', width: 130, align: "center" },
                        { label: '提交人', name: 'O_UploadBy', width: 100, align: "center" },
                        { label: '提交时间', name: 'O_UploadDate', width: 130, align: "center" },
                        { label: '月结', name: 'MonthBalance', width: 90, align: "center" },
                ],
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                sidx: "O_CreateDate",
                sord: "DESC"
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
