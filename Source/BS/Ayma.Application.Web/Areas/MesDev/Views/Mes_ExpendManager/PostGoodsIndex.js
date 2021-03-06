﻿/* * 创建人：超级管理员
 * 日  期：2019-03-18 10:37
 * 描  述：原物料消耗单据查询
 */
var refreshGirdData;
var $subgridTable;//子列表
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
            $('#E_StockCode').DataSourceSelect({ code: 'StockList', value: 's_code', text: 's_name' });
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
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });

            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("E_ExpendNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_Expend_Cancel', type: 2 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('E_ExpendNo');
                if (ayma.checkrow(keyValue)) {
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
                url: top.$.rootUrl + '/MesDev/Mes_ExpendManager/GetPostGoodsList',
                headData: [
                    { label: "主键", name: "ID", width: 160, align: "center", hidden: true },
                    {
                        label: "状态", name: "E_Status", width: 100, align: "center",
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
                    { label: "单据编号", name: "E_ExpendNo", width: 130, align: "center" },
                    { label: "仓库名称", name: "E_StockName", width: 130, align: "center" },
                    { label: "仓库编码", name: "E_StockCode", width: 90, align: "center" },
                    { label: "备注", name: "  ", width: 130, align: "center" },
                    {
                        label: "单据时间", name: "E_OrderDate", width: 100, align: "center",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "添加时间", name: "E_CreateDate", width: 130, align: "center" },
                    { label: "添加人", name: "E_CreateBy", width: 130, align: "center" },
                    { label: "修改人", name: "E_UpdateBy", width: 130, align: "center" },
                    { label: "修改时间", name: "E_UpdateDate", width: 130, align: "left" },
                    { label: "提交人", name: "E_UploadBy", width: 130, align: "center" },
                    { label: "提交时间", name: "E_UploadDate", width: 130, align: "left" },
                ],
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                isSubGrid: true, // 是否有子表editType 
                sidx: 'E_CreateDate',
                sord:'DESC',
                subGridRowExpanded: function (subgridId, row) {
                    var orderNo = row.E_ExpendNo;
                    var subgridTableId = subgridId + "_t";
                    $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                    $subgridTable = $("#" + subgridTableId);
                    $subgridTable.jfGrid({
                        url: top.$.rootUrl + '/MesDev/Mes_ExpendManager/GetDetailList?expendNo=' + orderNo,
                        headData: [

                            { label: "物料名称", name: "E_GoodsName", width: 130, align: "left" },
                            { label: "物料编码", name: "E_GoodsCode", width: 90, align: "left" },
                            { label: "批次", name: "E_Batch", width: 100, align: "left" },
                            { label: "单价", name: "E_Price", width: 80, align: "left"},
                            { label: "数量", name: "E_Qty", width: 90, align: "left", statistics: true },
                   {
                       label: "金额", name: "金额", width: 60, align: "left", formatter: function (value, row, dfop) {
                           if (row.E_Qty == "" || row.E_Qty == null || row.E_Qty == undefined) {
                               return row.金额 = 0;
                           }
                           else {
                               return row.金额 = row.E_Price * row.E_Qty;
                           }
                       }, statistics: true
                   },
                    { label: "单位", name: "E_Unit", width: 80, align: "left" }
                        ],
                    footerrow: true,
                    isStatistics:true	

                    }).jfGridSet("reload");
                }
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            param.OrderDate_S = $("#OrderDate_S").val();//新增单据时间
            param.OrderDate_E = $("#OrderDate_E").val();

            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
