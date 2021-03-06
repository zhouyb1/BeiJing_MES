﻿/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
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
            //仓库
            $('#M_StockName').select({
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
            $('#M_Status').DataItemSelect({ code: 'ProOutStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 查看详情
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '详情',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/PostProductForm?keyValue=' + keyValue,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {

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
                        url: top.$.rootUrl + '/MesDev/MaterInBill/PostProductForm?keyValue=' + keyValue,
                        width: 700,
                        height: 500,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {

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
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetPostProductPageList',
                headData: [
                    {
                        label: "状态", name: "M_Status", width: 90, align: "left",
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
                    { label: "入库单号", name: "M_MaterInNo", width: 160, align: "left" },
                    { label: "物料编码", name: "M_StockCode", width: 90, align: "left" },
                    { label: "物料名称", name: "M_StockName", width: 160, align: "left" },
                    //{ label: "生产订单号", name: "M_OrderNo", width: 160, align: "left" },
                    //{ label: "订单时间", name: "M_OrderDate", width: 160, align: "left" },
                    { label: "备注", name: "M_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "M_CreateBy", width: 90, align: "left" },
                    { label: "添加时间", name: "M_CreateDate", width: 160, align: "left" },
                    { label: "修改人", name: "M_UpdateBy", width: 90, align: "left" },
                    { label: "修改时间", name: "M_UpdateDate", width: 160, align: "left" },
                    { label: "提交人", name: "M_UploadBy", width: 90, align: "left"},
                    { label: "提交时间", name: "M_UploadDate", width: 160, align: "left"}
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
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
