/* * 创建人：超级管理员
 * 日  期：2019-03-14 09:27
 * 描  述：出库单查询
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
            $('#I_Status').DataItemSelect({ code: 'ProOutStatus' });
            $("#I_WorkShop").select({
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
            $('#I_Status').DataItemSelect({ code: 'InXStatus' });
            //绑定仓库
            $('#I_StockCode').select({
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

            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("I_InNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_InWorkShop_Cancel', type: 2 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '详情',
                        url: top.$.rootUrl + '/MesDev/InWorkShopManager/PostForm?keyValue=' + keyValue,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        btn:null,
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
                url: top.$.rootUrl + '/MesDev/InWorkShopManager/PostPageIndex',
                headData: [
                    {
                        label: "状态", name: "I_Status", width: 90, align: "center" ,
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'InXStatus',
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
                    { label: "入库单号", name: "I_InNo", width: 160, align: "center" },
                    { label: "入往日耗库", name: "I_StockName", width: 110, align: "center" },
                    { label: "原日耗库", name: "I_WorkShop", width: 140, align: "center" },
                    { label: "添加人", name: "I_CreateBy", width: 90, align: "center" },
                    { label: "创建时间", name: "I_CreateDate", width: 160, align: "center", sort: true },
                    { label: "备注", name: "I_Remark", width: 160, align: "left" },

                ],
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'I_CreateDate',
                sord: 'DESC',
                isSubGrid: true,
                subGridRowExpanded: function(subgridId, row) {
                    var orderNo = row.I_InNo;
                    $('#' + subgridId).jfGrid({
                        url: top.$.rootUrl + '/MesDev/InWorkShopManager/GetDetail',
                        headData: [
                            { label: "物料编码", name: "I_GoodsCode", width: 130, align: "left" },
                            { label: "物料名称", name: "I_GoodsName", width: 130, align: "left" },
                            { label: "单位", name: "I_Unit", width: 60, align: "left" },
                            { label: "数量", name: "I_Qty", width: 60, align: "left" },
                            { label: "价格", name: "I_Price", width: 60, align: "left" },
                            { label: "批次", name: "I_Batch", width: 80, align: "left" }
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
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}