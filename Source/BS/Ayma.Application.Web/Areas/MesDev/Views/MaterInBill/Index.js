/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
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
            // 时间搜索框
            $('#datesearch').amdate({
                dfdata: [
                    { name: '今天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } }
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
            $('#M_Status').DataItemSelect({ code: 'MaterInStatus' });

            $('#M_StockName').select({
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
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
              
                    ayma.layerForm({
                        id: 'MaterInBillForm',
                        title: '新增入库单',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?formId=MaterInBillForm',
                        width: 900,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                
            });
            $('#M_StockCode').select({
                type: 'default',
                value: 'S_Code',
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
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('M_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterInBillForm',
                        title: '编辑入库单',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?status='+statu+'&keyValue=' + keyValue + '&formId=MaterInBillForm',
                        width: 900,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //删除单据
            $("#am_delete").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("M_MaterInNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("M_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteMaterInBill', { orderNo: orderNo, proc: 'sp_MaterIn_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("M_Status");
                if (status!="1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_MaterInHead', field: 'M_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $("#am_post").on('click', function() {
                var orderNo = $("#girdtable").jfGridValue("M_MaterInNo");
                var status = $("#girdtable").jfGridValue("M_Status");
                if (status=="1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function(res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteMaterInBill', { orderNo: orderNo, proc: 'sp_MaterIn_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //打印
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('M_MaterInNo');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterInReport',
                        title: '入库单打印',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/PrintReport?keyValue=' + keyValue + "&report=MaterInReport&data=MaterIn",
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
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetPageList',
                headData: [
                    {
                        label: "状态", name: "M_Status", width: 160, align: "left",
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
                    { label: "入库单号", name: "M_MaterInNo", width: 160, align: "left" },
                    { label: "仓库编码", name: "M_StockCode", width: 160, align: "left" },
                    { label: "仓库名称", name: "M_StockName", width: 160, align: "left" },
                    //{ label: "生产订单号", name: "M_OrderNo", width: 160, align: "left" },
                    //{ label: "订单时间", name: "M_OrderDate", width: 160, align: "left" },
                    { label: "备注", name: "M_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "M_CreateBy", width: 160, align: "left" },
                    { label: "添加时间", name: "M_CreateDate", width: 160, align: "left"},
                    { label: "修改人", name: "M_UpdateBy", width: 160, align: "left" },
                    { label: "修改时间", name: "M_UpdateDate", width: 160, align: "left" }
                    //{ label: "提交人", name: "M_UploadBy", width: 160, align: "left"},
                    //{ label: "提交时间", name: "M_UploadDate", width: 160, align: "left"},
                ],
                mainId: 'ID',
                isPage: true,
                sidx: 'M_CreateDate',
                sord:'DESC'
            });
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
