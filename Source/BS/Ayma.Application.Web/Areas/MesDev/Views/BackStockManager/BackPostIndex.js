var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var type;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                queryJson.type = "report";
                page.search(queryJson);
            }, 280, 400);
                  $('#B_StockName').select({
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
            $('#B_StockToName').select({
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
            //仓库
            $('#B_StockName').select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/AM_SystemModule/DataSource/GetDataTable',
                // 访问数据接口参数
                param: { code: "StockList", strWhere: "S_Kind =1 " }
            });
            $('#B_Status').DataItemSelect({ code: 'MaterInStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('B_BackStockNo');
                if (ayma.checkrow(keyValue, true)) {
                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '退库单打印',
                        url: top.$.rootUrl + '/MesDev/BackStockManager/PrintReport?keyValue=' + keyValue + "&report=BackStockReport&data=BackStock",
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
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/BackStockManager/PostPageForm?keyValue=' + keyValue,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        btn: null,
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
                        id: 'form',
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/BackStockManager/PostPageForm?keyValue=' + keyValue,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 撤销单据
            $('#am_cancel').on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("B_BackStockNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_BackStock_Cancel', type: 2 }, function () {
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
                url: top.$.rootUrl + '/MesDev/BackStockManager/GetBacStockList',
                headData: [
                    {
                        label: "状态", name: "B_Status", width: 90, align: "center",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'RequistStatus',
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
                    { label: "单据编号", name: "B_BackStockNo", width: 130, align: "center" },
                    { label: "仓库编码", name: "B_StockCode", width: 90, align: "center" },
                    { label: "仓库名称", name: "B_StockName", width: 130, align: "center" },
                    { label: "退库仓库编码", name: "B_StockToCode", width: 90, align: "center" },
                    { label: "退库仓库名称", name: "B_StockToName", width: 120, align: "center" },
                    { label: "备注", name: "B_Remark", width: 130, align: "center" },
                    {
                        label: "单据时间", name: "B_OrderDate", width: 100, align: "center", sort: true,
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "添加人", name: "B_CreateBy", width: 90, align: "center" },
                    { label: "添加时间", name: "B_CreateDate", width: 130, align: "center", sort: true },
                    { label: "提交人", name: "B_UploadBy", width: 90, align: "center" },
                    { label: "提交时间", name: "B_UploadDate", width: 130, align: "center", sort: true },
                ],
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'B_CreateDate',
                sord: 'desc',
                isSubGrid: true,
                subGridRowExpanded: function (subgridId, row) {
                    var orderNo = row.B_BackStockNo;
                    $('#' + subgridId).jfGrid({
                        url: top.$.rootUrl + '/MesDev/BackStockManager/GetBackStockDetailList',
                        headData: [
                         { label: "物料编码", name: "B_GoodsCode", width: 130, align: "left", },
                         { label: "物料名称", name: "B_GoodsName", width: 130, align: "left" },
                         { label: "单价", name: "B_Price", width: 130, align: "left" },
                         { label: "单位", name: "B_Unit", width: 60, align: "left" },
                         { label: "返回数量", name: "B_Qty", width: 60, align: "left", },
                         { label: "批次", name: "B_Batch", width: 90, align: "left" }
                        ],
                    });
                    $('#' + subgridId).jfGridSet('reload', { param: { orderNo: orderNo } });

                }
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            param.type = type;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}