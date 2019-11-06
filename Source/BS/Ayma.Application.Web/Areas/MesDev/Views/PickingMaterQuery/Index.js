/* * 创建人：超级管理员
 * 日  期：2019-03-13 10:06
 * 描  述：领料单查询
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
            // 时间搜索框
            $('#datesearch').amdate({
                dfdata: [
                    { name: '今天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 300);
            //仓库
            $('#C_StockCode').select({
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
            $('#C_StockToCode').select({
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
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 详情
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '详情',
                        url: top.$.rootUrl + '/MesDev/PickingMaterQuery/Form?keyValue=' + keyValue,
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
                        url: top.$.rootUrl + '/MesDev/PickingMaterQuery/Form?keyValue=' + keyValue,
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
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("C_CollarNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认撤销该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_Collar_Cancel', type: 2 }, function () {
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
                url: top.$.rootUrl + '/MesDev/PickingMaterQuery/GetPageList',
                headData: [
                      {
                          label: "状态", name: "P_Status", width: 100, align: "left",
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
                    { label: "领料单号", name: "C_CollarNo", width: 160, align: "left"},
                    { label: "生产订单", name: "P_OrderNo", width: 160, align: "left" },
                    { label: "原仓库编码", name: "C_StockCode", width: 100, align: "left" },
                    { label: "原仓库名称", name: "C_StockName", width: 160, align: "left" },
                    { label: "调拨仓库编码", name: "C_StockToCode", width: 100, align: "left" },
                    { label: "调拨仓库名称", name: "C_StockToName", width: 160, align: "left" },
                    //{ label: "订单时间", name: "P_OrderDate", width: 160, align: "left" },                  
                    { label: "添加人", name: "C_CreateBy", width: 90, align: "left"},
                    { label: "添加时间", name: "C_CreateDate", width: 160, align: "left" },
                    { label: "备注", name: "C_Remark", width: 160, align: "left" },
                ],
                
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'C_CreateDate',
                sord: 'desc'
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
