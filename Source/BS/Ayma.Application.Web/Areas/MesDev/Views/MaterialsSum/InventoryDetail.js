    /* * 创建人：超级管理员
 * 日  期：2019-09-16 10:59
 * 描  述：原物料统计(入库、出库、次品)
 */
var refreshSubGirdData;
var $subgridTable;//子列表
var refreshGirdData;
var warehousingdetail;
var Detaile;
var g_code = decodeURIComponent(request('g_code'));
var g_stockcode = decodeURIComponent(request('g_stockcode'));
var start = decodeURIComponent(request('startTime'));
var end = decodeURIComponent(request('endTime'));
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var tabTitle = "汇总";
    var data;
    var jsonquery = {};
    var page = {
        init: function () {

            page.initGird();
            page.bind();
            //page.doubleClick();
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
            }, 180, 300);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.href = location.pathname;
            });
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                // 获取已激活的标签页的名称
                var activeTab = $(e.target).text();
                // 获取前一个激活的标签页的名称
                //var previousTab = $(e.relatedTarget).text();
                tabTitle = activeTab;
            });
            //原物料名称
            $("#G_Code").select({
                type: 'default',
                value: 'G_Code',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
                // 访问数据接口参数
            });
            //原物料仓库
            $("#S_Code").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetOriginalStockList',
                // 访问数据接口参数
            });
            //绑定供应商
            $("#G_SupplyCode").select({
                type: 'default',
                value: 'S_Code',
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
            //$('#am_export').on('click', function () {
            //    var tableName = "girdtable";
            //    var fileName = "库存明细统计";
            //    ayma.layerForm({
            //        id: "ExcelExportForm",
            //        title: '导出Excel数据',
            //        url: encodeURI(top.$.rootUrl + '/Utility/ExcelExportForm?gridId=' + tableName + '&filename=' + encodeURI(fileName)),
            //        width: 500,
            //        height: 380,
            //        callBack: function (id) {
            //            return top[id].acceptClick();
            //        },
            //        btn: ['导出Excel', '关闭']
            //    });
            //});
            // 导出
            $('#am_export').on('click', function () {
                var url = top.$.rootUrl + '/MesDev/MaterialsSum/Export2?queryJson=' + JSON.stringify(jsonquery);
                window.location.href = url;
            });
            $('#girdtable').on('dblclick', function ()
            {
                var keyValue = $('#girdtable').jfGridValue('F_OrderNo');
                var statu = $('#girdtable').jfGridValue('F_Status');
                if (statu == "R") {
                    var module = top.ayma.clientdata.get(['modulesMap', '4609c64f-8086-4edb-9f4d-decb50899f74']);
                    module.F_UrlAddress = '/MesDev/MaterInBill/PostIndex?keyValue=' + encodeURIComponent(keyValue);
                    top.ayma.frameTab.openNew(module);
                }
                if (statu == "C") {
                    var module = top.ayma.clientdata.get(['modulesMap', '00e1077b-7ecc-4bea-b9df-9a56e285e4ff']);
                    module.F_UrlAddress = '/MesDev/PickingMaterQuery/Index?keyValue=' + encodeURIComponent(keyValue);
                    top.ayma.frameTab.openNew(module);
                }
                else {

                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetInventoryDetail',
                headData: [
                    { label: "日期", name: "F_CreateDate", width: 130, align: "center" },
                    { label: "摘要", name: "F_Remark", width: 300, align: "center" },
                    { label: "商品编码", name: "F_GoodsCode", width: 115, align: "center" },
                    { label: "商品名称", name: "F_GoodsName", width: 115, align: "center" },
                    { label: "单位", name: "F_Unit", width: 50, align: "center" },
                    { label: "单据编号", name: "F_OrderNo", width: 130, align: "center" },
                     //{ label: "无税价格", name: "M_Price", width: 130, align: "center" },
                    {
                        label: "收入", name: "收入", width: 90, align: "center", children: [
                         {
                             label: "数量", name: "F_InQty", width: 90, align: "center",
                             formatter: function (value, row, dfop) {
                                 if (row.F_InQty != undefined && !!row.F_InQty) {
                                     return row.F_InQty.toFixed(6) / 1;
                                 }
                                 if (row.F_InQty == null) {
                                     return row.F_InQty = 0;
                                 }
                             }
                         },
                         {
                             label: "单位成本", name: "G_Price", width: 90, align: "center", formatter: function (value, row, dfop) {
                                 if (row.G_Price != undefined && !!row.G_Price) {
                                     return row.G_Price.toFixed(6) / 1;
                                 }
                                 if (row.G_Price == "") {
                                     return row.G_Price = 0;
                                 }
                             }
                         },
                          {
                              label: "金额", name: "金额", width: 90, align: "center",
                              formatter: function (value, row, dfop) {
                                  return (row.F_InQty * row.G_Price).toFixed(6) / 1;
                              }
                          },
                          {
                              label: "无税金额", name: "Aoumount", width: 90, align: "center",
                              //formatter: function (value, row, dfop) {
                              //    return (row.F_InQty * row.G_Price).toFixed(6) / 1;
                              //}
                          },
                        ]
                    },
                    {
                        label: "发出", name: "发出", width: 90, align: "center",
                        children: [
                         {
                             label: "数量", name: "F_OutQty", width: 90, align: "center",
                             formatter: function (value, row, dfop) {
                                 if (row.F_OutQty != undefined && !!row.F_OutQty) {
                                     return row.F_OutQty.toFixed(6) / 1;
                                 }
                                 if (row.F_OutQty == null) {
                                     return row.F_OutQty = 0;
                                 }
                             }
                         },
                         {
                             label: "单位成本", name: "G_Price", width: 90, align: "center", formatter: function (value, row, dfop) {
                                 if (row.G_Price != undefined && !!row.G_Price) {
                                     return row.G_Price.toFixed(6) / 1;
                                 }
                                 if (row.G_Price == "") {
                                     return row.G_Price = 0;
                                 }
                             }
                         },
                         {
                             label: "金额", name: "finalamount", width: 90, align: "center",
                             formatter: function (value, row, dfop) {
                                 return (row.F_OutQty * row.G_Price).toFixed(6) / 1;
                             }
                         },
                        ]
                    },
                       {
                           label: "结存", name: "结存", width: 90, align: "center",
                           children: [
                            {
                                label: "数量", name: "IntervoryQty", width: 90, align: "center", formatter: function (value, row, dfop) {
                                    if (row.IntervoryQty != undefined && !!row.IntervoryQty) {
                                        return row.IntervoryQty.toFixed(6) / 1;
                                    }
                                    if (row.IntervoryQty == "") {
                                        return row.IntervoryQty = 0;
                                    }
                                }
                            },
                            {
                                label: "单位成本", name: "G_Price", width: 90, align: "center", formatter: function (value, row, dfop) {
                                    if (row.G_Price != undefined && !!row.G_Price) {
                                        return row.G_Price.toFixed(6) / 1;
                                    }
                                    if (row.G_Price == "") {
                                        return row.G_Price = 0;
                                    }
                                }
                            },
                            {
                                label: "金额", name: "finalamount", width: 90, align: "center",
                                formatter: function (value, row, dfop) {
                                    return (row.IntervoryQty * row.G_Price).toFixed(6) / 1;
                                }
                            },
                           ]
                       }
                ],
                mainId: 'ID',
                reloadSelected: true,
                footerrow: true,
                isPage: true,
                isStatistics: true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            param.g_codes = g_code;
            param.g_stockcode = g_stockcode;
            param.start = start;
            param.end = end;
            jsonquery = param;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
            $('#pageTab a[href="#page_sum"]').tab('show'); // 通过名字选择
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    //子列表刷新
    refreshSubGirdData = function () {
        $subgridTable.jfGridSet("reload");
    };
    page.init();
}
