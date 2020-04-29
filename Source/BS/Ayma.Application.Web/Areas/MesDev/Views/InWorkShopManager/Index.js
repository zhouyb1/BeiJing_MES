/* * 创建人：超级管理员
 * 日  期：2019-03-13 11:57
 * 描  述：车间入库到线边仓
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
            $('#I_Status').DataItemSelect({ code: 'ProOutStatus' });
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
            $("#I_StockName").select({
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
            $("#I_WorkShop").select({
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
            //$('#O_Status').DataItemSelect({ code: 'ProOutStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'MaterForm',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/InWorkShopManager/Form?formId=MaterForm',
                    width: 900,
                    height: 700,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('I_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterForm',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/InWorkShopManager/Form?keyValue=' + keyValue + '&formId=MaterForm&status='+statu,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        btn: statu == 2 ? null : ['确认', '关闭'],
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //双击编辑
            $('#girdtable').on('dblclick', function() {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('I_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterForm',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/InWorkShopManager/Form?keyValue=' + keyValue + '&formId=MaterForm&status='+statu,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        btn: statu == 2 ? null : ['确认', '关闭'],
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除单据
            $('#am_delete').on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("I_InNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("I_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_InWorkShop_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $('#am_submit').on('click', function() {
                var orderNo = $("#girdtable").jfGridValue("I_InNo");
                var status = $("#girdtable").jfGridValue("I_Status");
                if (status == "1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function(res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_InWorkShop_Post', type: 1 }, function() {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("I_Status");
                if (status != "1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_InWorkShopHead', field: 'I_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('I_InNo');
                var status = $("#girdtable").jfGridValue("I_Status");
                if (ayma.checkrow(keyValue)) {
                    if (status != "2") {
                        ayma.alert.error("单据未审核");
                        return false;
                    }
                    ayma.layerForm({
                        id: 'SaleInReport',
                        title: '车间入库日耗库单打印',
                        url: top.$.rootUrl + '/MesDev/InWorkShopManager/PrintReport?keyValue=' + keyValue + "&report=InWorkShopReport&data=InWorkShop",
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
            // 预览打印
            $('#am_printview').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OutNo');
                if (ayma.checkrow(keyValue)) {

                    ayma.layerForm({
                        id: 'SaleInReport',
                        title: '入库单打印',
                        url: top.$.rootUrl + '/MesDev/InWorkShopManager/PrintViewReport?keyValue=' + keyValue + "&report=InWorkShopReport",
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
                url: top.$.rootUrl + '/MesDev/InWorkShopManager/GetPageList',
                headData: [
                    {
                        label: "状态", name: "I_Status", width: 90, align: "center",
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
                    { label: "单据编号", name: "I_InNo", width: 140, align: "center" },
                    { label: "原日耗仓", name: "I_WorkShop", width: 130, align: "center" },
                    { label: "入往日耗仓", name: "I_StockName", width: 130, align: "center" },
                    {
                        label: "单据时间", name: "I_OrderDare", width: 100, align: "center",sort:true,
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "创建时间", name: "I_CreateDate", width: 130, align: "center", sort: true ,},
                    { label: "添加人", name: "I_CreateBy", width: 90, align: "center" },
                    { label: "备注", name: "I_Remark", width: 160, align: "center" },

                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'I_CreateDate',
                sord: 'DESC'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            param.OrderDate_S = $("#OrderDate_S").val();
            param.OrderDate_E = $("#OrderDate_E").val();
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
