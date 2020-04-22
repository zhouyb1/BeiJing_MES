/* * 创建人：超级管理员
 * 日  期：2019-11-08 13:40
 * 描  述：其它出库单
 */
var selectedRow;
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
            //仓库
            $('#O_StockName').select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetOtherStockList',
                // 访问数据接口参数
                param: {}
            });
            $('#O_Status').DataItemSelect({ code: 'ProOutStatus' });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                selectedRow = null;
                ayma.layerForm({
                    id: 'OtherHead',
                    title: '新增其它出库单',
                    url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/Form?formId=OtherHead',
                    width: 900,
                    height: 650,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                 var status = $('#girdtable').jfGridValue('O_Status');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'OtherHead',
                        title: '编辑其它出库单',
                        url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/Form?status=' + status + '&keyValue=' + keyValue + '&formId=OtherHead' + '&state=1',
                        width: 900,
                        height: 650,
                        maxmin: true,
                        btn: status == 2 ? null : "",
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $('#girdtable').jfGridValue('O_Status');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'OtherHead',
                        title: '编辑其它出库单',
                        url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/Form?status=' + status + '&keyValue=' + keyValue + '&formId=OtherHead' + '&state=1',
                        width: 900,
                        height: 650,
                        maxmin: true,
                        btn: status == 2 ? null : "",
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //删除单据
            $("#am_delete").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OtherOutNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("O_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OtherOut_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //审核单据
            $("#am_audit").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("O_Status");
                if (status != "1") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_OtherOutHead', field: 'O_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $("#am_post").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OtherOutNo");
                var status = $("#girdtable").jfGridValue("O_Status");
                if (status == "1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OtherOut_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('O_OtherOutNo');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'OtherOutReport',
                        title: '其它出库单打印',
                        url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/PrintReport?keyValue=' + keyValue + "&report=OtherOutReport&data=OtherOut",
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
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/GetPageList',
                headData: [
                                 {
                                     label: "状态", name: "O_Status", width: 100, align: "left",
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
                        { label: '单据编号', name: 'O_OtherOutNo', width: 130, align: "left" },
                        { label: '仓库编码', name: 'O_StockCode', width: 100, align: "left" },
                        { label: '仓库名称', name: 'O_StockName', width: 130, align: "left" },
                        { label: '部门编码', name: 'O_DepartCode', width: 100, align: "left" },
                        { label: '部门名称', name: 'O_DepartName', width: 130, align: "left" },
                        { label: '单据时间', name: 'O_OrderDate', width: 130, align: "left",sort:true },
                        { label: '创建时间', name: 'O_CreateDate', width: 130, align: "left" ,sort:true},
                        { label: '添加人', name: 'O_CreateBy', width: 130, align: "left" },
                        { label: '修改人', name: 'O_UpdateBy', width: 130, align: "left" },
                        { label: '修改时间', name: 'O_UpdateDate', width: 130, align: "left" },
                        { label: '备注', name: 'O_Remark', width: 100, align: "left" },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'O_CreateDate',
                sord: 'desc'
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
