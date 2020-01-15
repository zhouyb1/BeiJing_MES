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
            $('#O_Status').DataItemSelect({ code: 'ProOutStatus' });
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
            //绑定仓库
            $('#O_StockName').select({
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
            $("#O_WorkShop").select({
                type: 'default',
                value: 'W_Code',
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
            $("#O_Kind").DataItemSelect({ code: "O_Kind" });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OutNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OutWorkShop_Cancel', type: 2 }, function () {
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
                        url: top.$.rootUrl + '/MesDev/OutWorkShopManager/PostForm?keyValue=' + keyValue,
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
            //双击详情
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '详情',
                        url: top.$.rootUrl + '/MesDev/OutWorkShopManager/PostForm?keyValue=' + keyValue,
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
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/OutWorkShopManager/PostPageIndex',
                headData: [
                    {
                        label: "状态", name: "O_Status", width: 100, align: "left" ,
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
                        {
                            label: "出库类型", name: "O_Kind", width: 80, align: "left",
                            formatterAsync: function (callback, value, row) {

                                ayma.clientdata.getAsync('dataItem', {
                                    key: value,
                                    code: 'O_Kind',
                                    callback: function (_data) {
                                        callback(_data.text);
                                    }
                                });
                            }
                        },
                    { label: "出库单号", name: "O_OutNo", width: 160, align: "left" },
                    { label: "仓库编码", name: "O_StockCode", width: 100, align: "left" },
                    { label: "仓库名称", name: "O_StockName", width: 160, align: "left" },
                    { label: "调拨车间", name: "O_WorkShopName", width: 160, align: "left" },
                    { label: "备注", name: "O_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "O_CreateBy", width: 90, align: "left" },
                    { label: "创建时间", name: "O_CreateDate", width: 160, align: "left" },
                ],
                mainId: 'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'O_CreateDate',
                sord: 'DESC'
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