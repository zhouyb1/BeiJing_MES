/* * 创建人：超级管理员
 * 日  期：2019-03-14 16:30
 * 描  述：退供应商单制作
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
            $('#B_Status').DataItemSelect({ code: 'ProOutStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'BackSupplyBillForm',
                    title: '新增退供应商单',
                    url: top.$.rootUrl + '/MesDev/Mes_BackSupply/Form?formId=BackSupplyBillForm',
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
                var statu = $('#girdtable').jfGridValue('B_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'BackSupplyEditForm',
                        title: '编辑退供应商单',
                        url: top.$.rootUrl + '/MesDev/Mes_BackSupply/Form?keyValue=' + keyValue + '&formId=BackSupplyEditForm'+'&status='+statu,
                        width: 900,
                        height: 700,
                        maxmin: true,
                        btn: statu == 2 ? null : "",
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //双击编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('B_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'BackSupplyEditForm',
                        title: '编辑退供应商单',
                        url: top.$.rootUrl + '/MesDev/Mes_BackSupply/Form?keyValue=' + keyValue + '&formId=BackSupplyEditForm' + '&status=' + statu,
                        width: 900,
                        height: 700,
                        maxmin: true,
                        btn: statu == 2 ? null : "",
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                //var keyValue = $('#girdtable').jfGridValue('ID');
                //if (ayma.checkrow(keyValue)) {
                //    ayma.layerConfirm('是否确认删除该项！', function (res) {
                //        if (res) {
                //            ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_BackSupply/DeleteForm', { keyValue: keyValue}, function () {
                //                refreshGirdData();
                //            });
                //        }
                //    });
                //}
                var orderNo = $("#girdtable").jfGridValue("B_BackSupplyNo");
                var status = $("#girdtable").jfGridValue("B_Status");
                if (status == "2") {
                    ayma.alert.error("该单据已审核,不能删除!");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_BackSupply_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("B_Status");
                if (status != "1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_BackSupplyHead', field: 'B_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $('#am_post').on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("B_BackSupplyNo");
                var status = $("#girdtable").jfGridValue("B_Status");
                if (status == "1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_BackSupply_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //打印
            $('#am_print').on('click', function() {
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
        },

        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_BackSupply/GetPageList',
                headData: [
                    { label: "主键", name: "ID", width: 160, align: "left", hidden: "true" },
                    {
                        label: "状态", name: "B_Status", width: 90, align: "center",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'BackSupplyStatus',
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
                    { label: "单据编号", name: "B_BackSupplyNo", width: 120, align: "center"},
                    { label: "仓库编码", name: "B_StockCode", width: 90, align: "center" },
                    { label: "仓库名称", name: "B_StockName", width: 130, align: "center" },
                    {
                        label: "单据时间", name: "B_CreateDate", width: 100, align: "center",sort:true,
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "创建时间", name: "B_CreateDate", width: 130, align: "center",sort:true },
                    { label: "添加人", name: "B_CreateBy", width: 90, align: "center" },
                    { label: "修改人", name: "B_UpdateBy", width: 90, align: "center" },
                    { label: "修改时间", name: "B_UpdateDate", width: 130, align: "left" },
                    { label: "备注", name: "B_Remark", width: 130, align: "center" },
                    
                       
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
