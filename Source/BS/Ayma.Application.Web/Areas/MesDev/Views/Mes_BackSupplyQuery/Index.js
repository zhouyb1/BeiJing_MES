/* * 创建人：超级管理员
 * 日  期：2019-03-18 10:37
 * 描  述：退供应商单查询
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
            }, 220, 400);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //查看详情
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '查看详情',
                        url: top.$.rootUrl + '/MesDev/Mes_BackSupplyQuery/Form?keyValue=' + keyValue,
                        width: 900,
                        height: 700,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                        }
                    });
                }
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("B_BackSupplyNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_BackSupply_Cancel', type: 2 }, function () {
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
                url: top.$.rootUrl + '/MesDev/Mes_BackSupplyQuery/GetPageList',
                headData: [
                    { label: "  主键", name: "ID", width: 160, align: "left", hidden: "true" },
                    {
                        label: "状态", name: "B_Status", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'BackSupplyStatus',
                                callback: function (_data) {
                                    console.log(value)
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
                    { label: "退供应商单号", name: "B_BackSupplyNo", width: 160, align: "left"},
                    { label: "仓库编码", name: "B_StockCode", width: 160, align: "left"},
                    { label: "仓库名称", name: "B_StockName", width: 160, align: "left"},
                    { label: "时间", name: "B_OrderDate", width: 160, align: "left"},
                    { label: "添加人", name: "B_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "B_CreateDate", width: 160, align: "left"},
                    { label: "修改人", name: "B_UpdateBy", width: 160, align: "left"},
                    { label: "修改时间", name: "B_UpdateDate", width: 160, align: "left"},
                    { label: "删除人", name: "B_DeleteBy", width: 160, align: "left"},
                    { label: "删除时间", name: "B_DeleteDate", width: 160, align: "left"},
                    { label: "提交人", name: "B_UploadBy", width: 160, align: "left"},
                    { label: "提交时间", name: "B_UploadDate", width: 160, align: "left"},
                    { label: "备注", name: "B_Remark", width: 160, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'B_BackSupplyNo',
                sord:'desc'
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
