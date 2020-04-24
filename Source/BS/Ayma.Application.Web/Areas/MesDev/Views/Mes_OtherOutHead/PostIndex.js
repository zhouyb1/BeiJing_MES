/* * 创建人：超级管理员
 * 日  期：2019-11-08 13:40
 * 描  述：其它出库单
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, ayma) {
    var startTime;
    var endTime;
    "use strict";
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
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/Form?keyValue=' + keyValue + '&formId=OtherHead' + '+&status=' + status,
                        width: 900,
                        height: 650,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });    // 双击编辑
            $('#girdtable').on('dblclick', function () {
                var status = $('#girdtable').jfGridValue('O_Status');
                var keyValue = $('#girdtable').jfGridValue('ID');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'OtherHead',
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/Form?keyValue=' + keyValue + '&formId=OtherHead'+'+&status=' + status,
                        width: 900,
                        height: 650,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
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
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("O_OtherOutNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_OtherOut_Cancel', type: 2 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/GetPostPageList',
                headData: [
                                 {
                                     label: "状态", name: "O_Status", width: 90, align: "center",
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
                        { label: '单据编号', name: 'O_OtherOutNo', width: 130, align: "center" },
                        { label: '仓库编码', name: 'O_StockCode', width: 80, align: "center" },
                        { label: '仓库名称', name: 'O_StockName', width: 150, align: "center" },
                        { label: '部门编码', name: 'O_DepartCode', width: 80, align: "center" },
                        { label: '部门名称', name: 'O_DepartName', width: 130, align: "center" },
                        { label: '备注', name: 'O_Remark', width: 130, align: "center" },
                        {
                            label: '单据时间', name: 'O_OrderDate', width: 100, align: "center",
                            formatter: function (cellvalue, options, rowObject) {
                                return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                            }
                        },
                        { label: '添加人', name: 'O_CreateBy', width: 100, align: "center" },
                        { label: '添加时间', name: 'O_CreateDate', width: 130, align: "center" },
                        { label: '修改人', name: 'O_UpdateBy', width: 100, align: "center" },
                        { label: '修改时间', name: 'O_UpdateDate', width: 130, align: "center" },
                        { label: '提交人', name: 'O_UploadBy', width: 100, align: "center" },
                        { label: '提交时间', name: 'O_UploadDate', width: 130, align: "center" }
                ],
                mainId: 'ID',
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
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
