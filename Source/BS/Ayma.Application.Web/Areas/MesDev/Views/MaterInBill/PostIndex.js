/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
 */
var refreshGirdData;
var M_MaterInNo = decodeURIComponent(request('keyValue'));
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
            $('#S_Name').select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址 
                url: top.$.rootUrl + '/MesDev/Tools/GetSupplyList',
                // 访问数据接口参数
                param: {}
            });
            $('#M_Status').DataItemSelect({ code: 'MaterInStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.href = location.pathname;
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
                url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
                // 访问数据接口参数
                param: {}
            });
            // 查看详情
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('M_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?keyValue=' + keyValue + '&status=' + statu,
                        width: 1000,
                        height: 600,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {

                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('M_MaterInNo');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterInReport',
                        title: '入库单打印',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/PrintReport?keyValue=' + keyValue + "&report=MaterInReport&data=MaterIn",
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
                var statu = $('#girdtable').jfGridValue('M_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?keyValue=' + keyValue +'&status=' + statu,
                        width: 1000,
                        height: 600,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {

                        }
                    });
                }
            });
            // 退供应商
            $('#am_returnsupply').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                
                if (ayma.checkrow(keyValue)) {
                    var status = $('#girdtable').jfGridValue('M_Status');

                    if (status != 3) {
                        ayma.alert.error("单据未完成,不能退供应商.");
                        return false;
                    }
                    ayma.layerForm({
                        id: 'BackSupplyAddForm',
                        title: '退供应商单',
                        url: top.$.rootUrl + '/MesDev/Mes_BackSupply/AddForm?materInKeyValue=' + keyValue + '&formId=BackSupplyAddForm',
                        width: 900,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("M_MaterInNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_MaterIn_Cancel', type: 2 }, function () {
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
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetPostPageList?M_MaterInNo=' + M_MaterInNo,
                headData: [
                    {
                        label: "状态", name: "M_Status", width: 90, align: "center",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'MaterInStatus',
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
                    { label: "单据编号", name: "M_MaterInNo", width: 130, align: "center" },
                    { label: "供应商名称", name: "M_SupplyName", width: 140, align: "center" },
                    { label: "备注", name: "M_Remark", width: 160, align: "center" },
                    {
                        label: "单据时间", name: "M_OrderDate", width: 100, align: "center",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "添加人", name: "M_CreateBy", width: 90, align: "center" },
                    { label: "添加时间", name: "M_CreateDate", width: 130, align: "center" },
                    { label: "修改人", name: "M_UpdateBy", width: 90, align: "center" },
                    { label: "修改时间", name: "M_UpdateDate", width: 130, align: "center" },
                    { label: "提交人", name: "M_UploadBy", width: 90, align: "center" },
                    { label: "提交时间", name: "M_UploadDate", width: 130, align: "center" },
                    
                ],
                mainId: 'ID',
                isPage: true,
                sidx: 'M_CreateDate',
                sord: 'DESC'
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
