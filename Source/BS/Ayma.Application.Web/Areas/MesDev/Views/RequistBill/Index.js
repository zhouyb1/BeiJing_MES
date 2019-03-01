/* * 创建人：超级管理员
 * 日  期：2019-01-09 10:20
 * 描  述：调拨单制作
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
            }, 220, 500);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'RequistBill',
                    title: '新增调拨单',
                    url: top.$.rootUrl + '/MesDev/RequistBill/Form?formId=RequistBill',
                    width: 800,
                    height: 600,
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
                        id: 'RequistBill',
                        title: '编辑调拨单',
                        url: top.$.rootUrl + '/MesDev/RequistBill/Form?keyValue=' + keyValue + '&formId=RequistBill',
                        width: 800,
                        height: 600,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                if (ayma.checkrow(keyValue)) {
                    var status = $("#girdtable").jfGridValue("R_Status");
                    if (status != "1") {
                        ayma.alert.error("已审核");
                        return false;
                    }
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_RequistHead', field: 'R_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //删除单据
            $("#am_delete").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("R_RequistNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("R_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_MaterTransfer_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $("#am_post").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("R_RequistNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("R_Status");
                    if (status == "1") {
                        ayma.alert.error("未审核");
                        return false;
                    }
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_MaterTransfer_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("R_RequistNo");
                var status = $("#girdtable").jfGridValue("R_Status");
                if (status != "3") {
                    ayma.alert.error("单据没提交,不能撤销");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_MaterTransfer_Cancel', type: 2 }, function () {
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
                url: top.$.rootUrl + '/MesDev/RequistBill/GetPageList',
                headData: [
                     {
                         label: "状态",
                         name: "R_Status",
                         width: 160,
                         align: "left",
                         formatterAsync: function (callback, value, row) {
                             if (value == 1) {
                                 callback("<span class='label label-info'>" + "待审核" + "</span>");
                             } else {
                                 callback("<span class='label label-success'>" + "审核通过" + "</span>");
                             }
                         }
                     },
                    { label: "调拨单号", name: "R_RequistNo", width: 160, align: "left"},
                    { label: "原仓库编码", name: "R_StockCode", width: 160, align: "left"},
                    { label: "原仓库名称", name: "R_StockName", width: 160, align: "left"},
                    { label: "调拨仓库编码", name: "R_StockToCode", width: 160, align: "left"},
                    { label: "调拨仓库名称", name: "R_StockToName", width: 160, align: "left"},
                    { label: "生产订单号", name: "P_OrderNo", width: 160, align: "left"},
                    { label: "订单时间", name: "P_OrderDate", width: 160, align: "left"},
                    { label: "备注", name: "R_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "R_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "R_CreateDate", width: 160, align: "left"},
                    { label: "修改人", name: "R_UpdateBy", width: 160, align: "left"},
                    { label: "修改时间", name: "R_UpdateDate", width: 160, align: "left"}

                    //{ label: "提交人", name: "R_UploadBy", width: 160, align: "left"},
                    //{ label: "提交时间", name: "R_UploadDate", width: 160, align: "left"},
                ],
                mainId:'ID',
                isPage: true,
                sidx: 'R_RequistNo',
                sord: 'DESC'
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
