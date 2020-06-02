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

            $("#B_StockName").select({
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
                        title: '单据详情',
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
            //打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('B_BackSupplyNo');
                if (keyValue == "") {
                    ayma.alert.error("请选择要打印的单据！");
                } else {
                    ayma.layerForm({
                        id: 'BackSupplyReport',
                        title: '退供应商单打印',
                        url: top.$.rootUrl + '/MesDev/Mes_BackSupply/PrintReport?keyValue=' + keyValue + "&report=BackSupply&data=BackSupply",
                        width: 1000,
                        height: 800,
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
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '单据详情',
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
            //撤销
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
                    {
                        label: "状态", name: "B_Status", width: 90, align: "center",
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
                    { label: "单据编号", name: "B_BackSupplyNo", width: 130, align: "center" },
                    { label: "仓库名称", name: "B_StockName", width: 130, align: "center" },
                    { label: "仓库编码", name: "B_StockCode", width: 70, align: "center" },
                    {
                        label: "单据时间", name: "B_OrderDate", width: 100, align: "center",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "添加人", name: "B_CreateBy", width: 90, align: "center" },
                    { label: "添加时间", name: "B_CreateDate", width: 130, align: "center" },
                    { label: "修改人", name: "B_UpdateBy", width: 90, align: "center" },
                    { label: "修改时间", name: "B_UpdateDate", width: 130, align: "center" },
                    { label: "提交人", name: "B_UploadBy", width: 90, align: "center" },
                    { label: "提交时间", name: "B_UploadDate", width: 130, align: "center" },
                    { label: "备注", name: "B_Remark", width: 160, align: "left" },
                     { label: "  主键", name: "ID", width: 160, align: "left", hidden: "true" },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'B_CreateDate',
                sord:'desc'
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
