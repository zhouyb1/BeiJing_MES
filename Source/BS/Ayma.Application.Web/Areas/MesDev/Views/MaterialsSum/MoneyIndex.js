/* * 创建人：超级管理员
 * 日  期：2019-09-16 10:59
 * 描  述：原物料统计(入库、出库、次品)
 */
var refreshSubGirdData;
var $subgridTable;//子列表
var refreshGirdData;
var warehousingdetail;
var Detaile;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var tabTitle = "汇总";
    var jsonquery = {};
    var data;
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
            //双击  
            //$('#girdtable_sum').on('dblclick', function () {
            //    var dateParam = { StartTime: startTime, EndTime: endTime };
            //    var M_GoodsCode = $('#girdtable_sum').jfGridValue('m_goodscode');
            //    $('#girdtable_detail').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
            //    $('#girdtable_Outbounddetails').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
            //    $('#girdtable_withdrawingdetails').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
            //    $('#girdtable_materialssales').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
            //    $('#girdtable_scrapnumber').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
            //    $('#girdtable_other').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
            //    $('#girdtable_otherout').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
            //    $('#girdtable_supplierback').jfGridSet('reload', { param: { M_GoodsCode: M_GoodsCode, queryJson: JSON.stringify(dateParam) } });
            //    $('#pageTab a[href="#page_detail"]').tab('show'); // 通过名字选择
            //}); 
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 180, 500);
            // 刷新
            $('#am_refresh').on('click', function() {
                location.reload();
            });
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                // 获取已激活的标签页的名称
                var activeTab = $(e.target).text();
                // 获取前一个激活的标签页的名称
                //var previousTab = $(e.relatedTarget).text();
                tabTitle = activeTab;
            });
            //原物料名称
            $("#M_GoodsName").select({
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
            $("#S_Name").select({
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
            //导出excel
            $('#am_export').on('click', function () {
                var tableName = "";
                var fileName = "";
                if (tabTitle == "汇总") {
                    var url = top.$.rootUrl + '/MesDev/MaterialsSum/Export?queryJson=' + JSON.stringify(jsonquery);
                    window.location.href = url;
                }
                else if (tabTitle == "入库明细") {
                    tableName = "girdtable_detail";
                    fileName = "原物料入库明细";
                }
                else if (tabTitle == "出库明细") {
                    tableName = "girdtable_Outbounddetails";
                    fileName = "原物料出库明细";
                }
                else if (tabTitle == "退库明细") {
                    tableName = "girdtable_withdrawingdetails";
                    fileName = "原物料退库明细";
                }
                else if (tabTitle == "原物料销售明细") {
                    tableName = "girdtable_materialssales";
                    fileName = "原物料销售明细";
                }
                else if (tabTitle == "报废数量明细") {
                    tableName = "girdtable_scrapnumber";
                    fileName = "报废数量明细";
                }
                else if (tabTitle == "其它入库明细") {
                    tableName = "girdtable_other";
                    fileName = "其它入库明细";
                }
                else if (tabTitle == "其它出库明细") {
                    tableName = "girdtable_otherout";
                    fileName = "其它出库明细";
                }
                else {
                    tableName = "girdtable_supplierback";
                    fileName = "退供应商明细";
                }
                if (tabTitle != "汇总")
                {
                    ayma.layerForm({
                        id: "ExcelExportForm",
                        title: '导出Excel数据',
                        url: encodeURI(top.$.rootUrl + '/Utility/ExcelExportForm?gridId=' + tableName + '&filename=' + encodeURI(fileName)),
                        width: 500,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick();
                        },
                        btn: ['导出Excel', '关闭']
                    });
                }
            });
            $('#girdtable_sum').on('dblclick', function () {
                var g_code = $('#girdtable_sum').jfGridValue('g_code');
                var g_stockcode = $('#girdtable_sum').jfGridValue('g_stockcode');
                var module = top.ayma.clientdata.get(['modulesMap', '6c1dfb3e-9f16-40db-9d72-4fe5d604b06f']);//本地ID
                //var module = top.ayma.clientdata.get(['modulesMap', '2f8b20ae-293b-4431-9a11-16a3e801f019']);//京铁列服ID
                module.F_UrlAddress = '/MesDev/MaterialsSum/InventoryDetail?g_code=' + encodeURIComponent(g_code) + '&g_stockcode=' + encodeURIComponent(g_stockcode) + '&startTime=' + encodeURIComponent(startTime) + '&endTime=' + encodeURIComponent(endTime);
               top.ayma.frameTab.openNew(module);
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var ToDate = getNextDate(startTime, -1);
                var M_GoodsName = $('#M_GoodsName').selectGet();
                var S_Name = $('#S_Name').selectGet();
                    ayma.layerForm({
                        id: 'YWCRKTJ',
                        title: '原物料出入库打印',
                        url: top.$.rootUrl + '/MesDev/MaterialsSum/PrintReport?startTime=' + startTime + "&endTime=" + endTime + "&M_GoodsName=" + M_GoodsName + "&S_Name=" + S_Name + "&ToDate=" + ToDate + "&report=YWLCRKTJReport&data=YWLCRKTJ",
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });              
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable_sum').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialSumListByDate',
                headData: [
                    { label: "商品编码", name: "g_code", width: 130, align: "center" },
                    { label: "商品名称", name: "g_name", width: 130, align: "center" },
                    { label: "仓库名称", name: "s_name", width: 130, align: "center" },
                    { label: "仓库编码", name: "g_stockcode", width: 130, align: "center" },
                    //{ label: "供应商名称", name: "m_supplyname", width: 130, align: "center" },
                    { label: "单位", name: "g_unit", width: 130, align: "center" },
                    {
                        label: "入库数量", name: "inventoryquantity", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                            if (row.inventoryquantity != undefined && !!row.inventoryquantity) {
                                return row.inventoryquantity.toFixed(2) / 1;
                            }
                            if (row.inventoryquantity == "") {
                                return row.inventoryquantity = 0;
                            }
                        }
                    },
                    {
                        label: "出库数量", name: "delivery", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                            if (row.delivery != undefined && !!row.delivery) {
                                return row.delivery.toFixed(6) / 1;
                            }
                            if (row.delivery == "") {
                                return row.delivery = 0;
                            }
                        }
                    },
                    {
                        label: "期初库存", name: "期初库存", width: 90, align: "center", children: [
                         {
                             label: "数量", name: "initialinventory", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                 if (row.initialinventory != undefined && !!row.initialinventory) {
                                     return row.initialinventory.toFixed(6) / 1;
                                 }
                                 if (row.initialinventory == "") {
                                     return row.initialinventory = 0;
                                 }
                             }
                         },
                         {
                             label: "加权平均价(元)", name: "price", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                 if (row.price != undefined && !!row.price) {
                                     return row.price.toFixed(6) / 1;
                                 }
                                 if (row.price == "") {
                                     return row.price = 0;
                                 }
                             }
                         },
                          {
                              label: "期初金额", name: "initialamount", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                  if (row.initialamount != undefined && !!row.initialamount) {
                                      return row.initialamount.toFixed(6) / 1;
                                  }
                                  if (row.initialamount == "") {
                                      return row.initialamount = 0;
                                  }
                              }
                          },
                        ]
                    },
                    //{ label: "期初金额", name: "initialamount", width: 90, align: "center", statistics: true },
                    {
                        label: "期末库存", name: "期末库存", width: 90, align: "center",
                        children: [
                         {
                             label: "数量", name: "endinginventory", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                 if (row.endinginventory != undefined && !!row.endinginventory) {
                                     return row.endinginventory.toFixed(6) / 1;
                                 }
                                 if (row.endinginventory == "") {
                                     return row.endinginventory = 0;
                                 }
                             }
                         },
                         {
                             label: "加权平均价(元)", name: "price", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                 if (row.price != undefined && !!row.price) {
                                     return row.price.toFixed(6) / 1;
                                 }
                                 if (row.price == "") {
                                     return row.price = 0;
                                 }
                             }
                         },
                         {
                             label: "期末金额", name: "finalamount", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                 if (row.finalamount != undefined && !!row.finalamount) {
                                     return row.finalamount.toFixed(6) / 1;
                                 }
                                 if (row.finalamount == "") {
                                     return row.finalamount = 0;
                                 }
                             }
                         },
                        ]
                    },
                    //{ label: "期末金额", name: "finalamount", width: 160, align: "center", statistics: true },
                    //{ label: "加权平均价", name: "price", width: 90, align: "center" },
                    {
                        label: "次品退库", name: "次品退库", width: 90, align: "center", children: [
                         {
                             label: "数量", name: "back_qty", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                 if (row.back_qty != undefined && !!row.back_qty) {
                                     return row.back_qty.toFixed(6) / 1;
                                 }
                                 if (row.back_qty == "") {
                                     return row.back_qty = 0;
                                 }
                             }
                         },
                        ]
                    },
                    {
                        label: "退回仓库", name: "退回仓库", width: 90, align: "center", children: [
                            {
                                label: "数量", name: "withdrawingnumber", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                    if (row.withdrawingnumber != undefined && !!row.withdrawingnumber) {
                                        return row.withdrawingnumber.toFixed(6) / 1;
                                    }
                                    if (row.withdrawingnumber == "") {
                                        return row.withdrawingnumber = 0;
                                    }
                                }
                            },
                        ]
                    },
                    {
                        label: "原物料销售", name: "原物料销售", width: 90, align: "center", children: [
                            {
                                label: "数量", name: "materialssales", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                    if (row.materialssales != undefined && !!row.materialssales) {
                                        return row.materialssales.toFixed(6) / 1;
                                    }
                                    if (row.materialssales == "") {
                                        return row.materialssales = 0;
                                    }
                                }
                            },
                            {
                                label: "销售单价(元)", name: "outprice", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                    if (row.outprice != undefined && !!row.outprice) {
                                        return row.outprice.toFixed(6) / 1;
                                    }
                                    if (row.outprice == "" || row.outprice == undefined) {
                                        return row.outprice = 0;
                                    }
                                }
                            },
                            {
                                label: "金额(元)", name: "outamount", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                    return (row.materialssales * row.outprice).toFixed(6) / 1;
                                }
                            },
                        ]
                    },
                    {
                        label: "报废物料", name: "报废单据", width: 90, align: "center", children: [
                              {
                                  label: "数量", name: "scrapist", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                      if (row.C_PlanQty != undefined && !!row.C_PlanQty) {
                                          return row.C_PlanQty.toFixed(6) / 1;
                                      }
                                      if (row.C_PlanQty == "") {
                                          return row.C_PlanQty = 0;
                                      }
                                  }
                              },
                        ]
                    },
                    {
                        label: "其它入库", name: "其它入库", width: 90, align: "center", children: [
                                {
                                    label: "数量", name: "otherwarehouse", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                        if (row.otherwarehouse != undefined && !!row.otherwarehouse) {
                                            return row.otherwarehouse.toFixed(6) / 1;
                                        }
                                        if (row.otherwarehouse == "") {
                                            return row.otherwarehouse = 0;
                                        }
                                    }
                                },
                        ]
                    },
                    {
                        label: "其它出库", name: "其它出库", width: 90, align: "center", children: [
                                  {
                                      label: "数量", name: "otheroutbound", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                          if (row.otheroutbound != undefined && !!row.otheroutbound) {
                                              return row.otheroutbound.toFixed(6) / 1;
                                          }
                                          if (row.otheroutbound == "") {
                                              return row.otheroutbound = 0;
                                          }
                                      }
                                  },
                        ]
                    },
                    {
                        label: "退供应商", name: "退供应商", width: 90, align: "center", children: [
                                    {
                                        label: "数量", name: "supplierback", width: 90, align: "center", statistics: true, formatter: function (value, row, dfop) {
                                            if (row.supplierback != undefined && !!row.supplierback) {
                                                return row.supplierback.toFixed(6) / 1;
                                            }
                                            if (row.supplierback == "") {
                                                return row.supplierback = 0;
                                            }
                                        }
                                    },
                        ]
                    },
                   {
                        label: "开始时间", name: "startTime", width: 130, align: "center", formatter: function (value, row, dfop) {
                                                 return startTime;
                                             }
                                         },
                        {
                            label: "结束时间", name: "end", width: 130, align: "center",
                            formatter: function (value, row, dfop) {
                                return endTime;
                            }
                        },
                    //{ label: "入库时间", name: "m_createdate", width: 160, align: "left",hidden:true}    
                ],        
                onRenderComplete: function (rows)
                {
                    //var list = $('#girdtable_sum').jfGridGet('rowdatas');
                    //var data = [];inet
                    //data = list;
                    //for (var i = 0; i < data.length; i++) {
                    //    alert(data[i].end);
                    //}
                    var StartTime = encodeURIComponent(startTime);
                    var EndTime = encodeURIComponent(endTime);
                    var lengh = rows.length;
                    for (var i = 0; i < lengh; i++) {
                        //var a = $("[rownum='rownum_girdtable_sum_" + i + "'][colname='end']").text();
                        //var time = encodeURIComponent(a);
                        $("[rownum='rownum_girdtable_sum_" + i + "'][colname='inventoryquantity']").html("<a href =# style=text-decoration:underline title='点击查询入库明细' onclick=Detaile('" + rows[i].g_code + "','入库明细')>" + rows[i].inventoryquantity + "</ a>");
                        $("[rownum='rownum_girdtable_sum_" + i + "'][colname='delivery']").html("<a href =# style=text-decoration:underline title='点击查询出库明细' onclick=Detaile('" + rows[i].g_code + "','出库明细')>" + rows[i].delivery + "</ a>");
                        $("[rownum='rownum_girdtable_sum_" + i + "'][colname='withdrawingnumber']").html("<a href =# style=text-decoration:underline title='点击查询退库明细' onclick=Detaile('" + rows[i].g_code + "','退库明细')>" + rows[i].withdrawingnumber + "</ a>");
                        $("[rownum='rownum_girdtable_sum_" + i + "'][colname='materialssales']").html("<a href =# style=text-decoration:underline title='点击查询原物料销售明细' onclick=Detaile('" + rows[i].g_code + "','原物料销售明细')>" + rows[i].materialssales + "</ a>");
                        $("[rownum='rownum_girdtable_sum_" + i + "'][colname='scrapist']").html("<a href =# style=text-decoration:underline title='点击查询报废数量明细' onclick=Detaile('" + rows[i].g_code + "','报废数量明细')>" + rows[i].scrapist + "</ a>");
                        $("[rownum='rownum_girdtable_sum_" + i + "'][colname='otherwarehouse']").html("<a href =# style=text-decoration:underline title='点击查询其它入库明细' onclick=Detaile('" + rows[i].g_code + "','其它入库明细')>" + rows[i].otherwarehouse + "</ a>");
                        $("[rownum='rownum_girdtable_sum_" + i + "'][colname='otheroutbound']").html("<a href =# style=text-decoration:underline title='点击查询其它出库明细' onclick=Detaile('" + rows[i].g_code + "','其它出库明细')>" + rows[i].otheroutbound + "</ a>");
                        $("[rownum='rownum_girdtable_sum_" + i + "'][colname='supplierback']").html("<a href =# style=text-decoration:underline title='点击查询退供应商明细' onclick=Detaile('" + rows[i].g_code + "','退供应商明细')>" + rows[i].supplierback + "</ a>");
                    }
                },
                mainId: 'ID',
                reloadSelected: true,
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: 'g_code',
                sord: 'desc',
            });
            //入库明细
            $('#girdtable_detail').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialDetailListByDate',
                headData: [
                               { label: "入库单号", name: "m_materinno", width: 130, align: "left" },
                               { label: "供应商编码", name: "m_supplycode", width: 80, align: "left" },
                               { label: "供应商名称", name: "m_supplyname", width: 150, align: "left" },
                               { label: "商品编码", name: "m_goodscode", width: 80, align: "left" },
                               { label: "商品名称", name: "m_goodsname", width: 150, align: "left" },
                               { label: "仓库名称", name: "m_stockname", width: 150, align: "left" },
                               { label: "入库数量", name: "m_qty", width: 70, align: "left", statistics: true },
                               { label: "入库金额", name: "amount", width: 70, align: "left", statistics: true },
                               { label: "批次", name: "m_batch", width: 80, align: "left" },
                               { label: "商品税率", name: "m_goodsitax", width: 80, align: "left" },
                               { label: "价格", name: "m_price", width: 70, align: "left" },
                               { label: "备注", name: "m_remark", width: 70, align: "left" },
                               { label: "入库时间", name: "m_createdate", width: 150, align: "left" },
                               { label: "添加人", name: "m_createby", width: 80, align: "left" },
                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "m_qty",
                sord: 'ASC',
                reloadSelected: false,
            });
            //出库明细
            $('#girdtable_Outbounddetails').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialOutDetailListByDate',
                headData: [
                               { label: "领料单号", name: "c_collarno", width: 130, align: "left" },
                               //{ label: "原仓库编码", name: "c_stockcode", width: 80, align: "left" },
                               { label: "原仓库名称", name: "c_stockname", width: 130, align: "left" },
                               //{ label: "领料仓库编码", name: "c_stocktocode", width: 80, align: "left" },
                               { label: "领料仓库名称", name: "c_stocktoname", width: 130, align: "left" },
                               { label: "出库数量", name: "c_qty", width: 80, align: "center", statistics: true },
                               { label: "出库金额", name: "amount", width: 70, align: "center", statistics: true },
                               //{ label: "供应商编码", name: "c_supplycode", width: 160, align: "left" },
                               { label: "供应商名称", name: "c_supplyname", width: 130, align: "left" },
                               { label: "商品编码", name: "c_goodscode", width: 100, align: "left" },
                               { label: "商品名称", name: "c_goodsname", width: 130, align: "left" },
                               { label: "单位", name: "c_unit", width: 80, align: "left" },
                               { label: "批次", name: "c_batch", width: 80, align: "left" },
                               { label: "价格", name: "c_price", width: 70, align: "left" },
                               { label: "备注", name: "c_remark", width: 70, align: "left" },
                               { label: "出库时间", name: "c_createdate", width: 140, align: "left" },
                               { label: "添加人", name: "c_createby", width: 80, align: "left" },
                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "c_qty",
                sord: 'ASC',
                reloadSelected: false,
            });
            //退库明细
            $('#girdtable_withdrawingdetails').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialBackDetailListByDate',
                headData: [
                               { label: "退库单号", name: "b_backstockno", width: 130, align: "left" },
                               { label: "仓库编码", name: "b_stockcode", width: 80, align: "left" },
                               { label: "仓库名称", name: "b_stockname", width: 130, align: "left" },
                               { label: "退料仓库编码", name: "b_stocktocode", width: 130, align: "left" },
                               { label: "退料仓库名称", name: "b_stocktoname", width: 130, align: "left" },
                               { label: "退库数量", name: "b_qty", width: 80, align: "left", statistics: true },
                               { label: "退库金额", name: "amount", width: 70, align: "center", statistics: true },
                               { label: "商品编码", name: "b_goodscode", width: 130, align: "left" },
                               { label: "商品名称", name: "b_goodsname", width: 130, align: "left" },
                               { label: "单位", name: "b_unit", width: 80, align: "left" },
                               { label: "批次", name: "b_batch", width: 80, align: "left" },
                               { label: "价格", name: "b_price", width: 80, align: "left" },
                               { label: "备注", name: "b_remark", width: 80, align: "left" },
                               { label: "退库时间", name: "b_createdate", width: 130, align: "left" },
                               { label: "添加人", name: "b_createby", width: 80, align: "left" },
                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "b_qty",
                sord: 'ASC',
                reloadSelected: false,
            });
            //原物料销售明细
            $('#girdtable_materialssales').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialSaleDetailListByDate',
                headData: [
                               { label: "销售单号", name: "s_saleno", width: 130, align: "left" },
                               { label: "仓库编码", name: "s_stockcode", width: 130, align: "left" },
                               { label: "仓库名称", name: "s_stockname", width: 130, align: "left" },
                               { label: "客户名称", name: "s_costomname", width: 130, align: "left" },
                               { label: "销售数量", name: "s_qty", width: 80, align: "left", statistics: true },
                               { label: "销售金额", name: "amount", width: 70, align: "center", statistics: true },
                               { label: "商品编码", name: "s_goodscode", width: 130, align: "left" },
                               { label: "商品名称", name: "s_goodsname", width: 130, align: "left" },
                               { label: "税率", name: "s_otax", width: 80, align: "left" },
                               { label: "单位", name: "s_unit", width: 80, align: "left" },
                               { label: "批次", name: "s_batch", width: 80, align: "left" },
                               { label: "价格", name: "s_price", width: 80, align: "left" },
                               { label: "备注", name: "s_remark", width: 70, align: "left" },
                               { label: "销售时间", name: "s_createdate", width: 130, align: "left" },
                               { label: "添加人", name: "s_createby", width: 130, align: "left" },
                              
                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "s_qty",
                sord: 'ASC',
                reloadSelected: false,
            });
            //报废数量明细
            $('#girdtable_scrapnumber').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialScrapDetailListByDate',
                headData: [
                               { label: "报废单号", name: "s_scrapNo", width: 130, align: "left" },
                               { label: "仓库编码", name: "s_stockcode", width: 80, align: "left" },
                               { label: "仓库名称", name: "s_stockname", width: 130, align: "left" },
                               { label: "报废数量", name: "s_qty", width: 90, align: "center", statistics: true },
                               { label: "报废金额", name: "amount", width: 70, align: "center", statistics: true },
                               { label: "商品编码", name: "s_goodscode", width: 90, align: "left" },
                               { label: "商品名称", name: "s_goodsname", width: 90, align: "left" },
                               { label: "单位", name: "s_unit", width: 80, align: "left" },
                               { label: "批次", name: "s_batch", width: 80, align: "left" },
                               { label: "价格", name: "s_price", width: 80, align: "left" },
                               { label: "备注", name: "s_remark", width: 130, align: "left" },
                               { label: "报废时间", name: "s_createdate", width: 130, align: "left" },
                               { label: "添加人", name: "s_createby", width: 130, align: "left" },

                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "s_qty",
                sord: 'ASC',
                reloadSelected: false,
            });
            //其它入库数量明细
            $('#girdtable_other').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialOtherDetailListByDate',
                headData: [
                               { label: "其它入库单号", name: "o_otherinno", width: 130, align: "left" },
                               { label: "仓库编码", name: "o_stockcode", width: 80, align: "left" },
                               { label: "仓库名称", name: "o_stockname", width: 130, align: "left" },
                               { label: "其它入库数量", name: "o_qty", width: 80, align: "center", statistics: true },
                               { label: "其它入库金额", name: "amount", width: 90, align: "center", statistics: true },
                               { label: "商品编码", name: "o_goodscode", width: 100, align: "left" },
                               { label: "商品名称", name: "o_goodsname", width: 130, align: "left" },
                               { label: "单位", name: "o_unit", width: 80, align: "left" },
                               { label: "批次", name: "o_batch", width: 80, align: "left" },
                               { label: "价格", name: "o_price", width: 80, align: "left" },
                               { label: "备注", name: "o_remark", width: 80, align: "left" },
                               { label: "入库时间", name: "o_createdate", width: 130, align: "left" },
                               { label: "添加人", name: "o_createby", width: 130, align: "left" },

                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "o_qty",
                sord: 'ASC',
                reloadSelected: false,
            });
            //其它出库数量明细
            $('#girdtable_otherout').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialOtherOutDetailListByDate',
                headData: [
                               { label: "其它出库单号", name: "o_otheroutno", width: 130, align: "left" },
                               { label: "仓库编码", name: "o_stockcode", width: 80, align: "left" },
                               { label: "仓库名称", name: "o_stockname", width: 130, align: "left" },
                               { label: "部门编码", name: "o_departcode", width: 80, align: "left" },
                               { label: "部门名称", name: "o_departname", width: 90, align: "left" },
                               { label: "其它出库数量", name: "o_qty", width: 80, align: "center", statistics: true },
                               { label: "其它出库金额", name: "amount", width: 90, align: "center", statistics: true },
                               { label: "商品编码", name: "o_goodscode", width: 80, align: "left" },
                               { label: "商品名称", name: "o_goodsname", width: 80, align: "left" },
                               { label: "单位", name: "o_unit", width: 80, align: "left" },
                               { label: "批次", name: "o_batch", width: 80, align: "left" },
                               { label: "价格", name: "o_price", width: 80, align: "left" },
                               { label: "备注", name: "o_remark", width: 130, align: "left" },
                               { label: "出库时间", name: "o_createdate", width: 130, align: "left" },
                               { label: "添加人", name: "o_createby", width: 130, align: "left" },

                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "o_qty",
                sord: 'ASC',
                reloadSelected: false,
            });
            //退供应商数量明细
            $('#girdtable_supplierback').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterialsSum/GetMaterialBackSupplyDetailListByDate',
                headData: [
                               { label: "退供应商单号", name: "b_backsupplyno", width: 130, align: "left" },
                               { label: "仓库编码", name: "b_stockcode", width: 80, align: "left" },
                               { label: "仓库名称", name: "b_stockname", width: 130, align: "left" },
                               { label: "退供应商数量", name: "b_qty", width: 80, align: "center", statistics: true },
                               { label: "退供应商金额", name: "amount", width: 90, align: "center", statistics: true },
                               { label: "商品编码", name: "b_goodscode", width: 100, align: "left" },
                               { label: "商品名称", name: "b_goodsname", width: 130, align: "left" },
                               { label: "单位", name: "b_unit", width: 80, align: "left" },
                               { label: "批次", name: "b_batch", width: 80, align: "left" },
                               { label: "价格", name: "b_price", width: 80, align: "left" },
                               { label: "备注", name: "b_remark", width: 80, align: "left" },
                               { label: "退供应商时间", name: "b_createdate", width: 130, align: "left" },
                               { label: "添加人", name: "b_createby", width: 80, align: "left" },
                ],
                mainId: 'ID',
                footerrow: true,
                isPage: true,
                isStatistics: true,
                sidx: "b_qty",
                sord: 'ASC',
                reloadSelected: false,
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            param.data = $("#Date").val();
            jsonquery = param;
            $('#girdtable_sum').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
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
    warehousingdetail = function (startTime,endTime,moduleId) {
        var module = top.ayma.clientdata.get(['modulesMap', moduleId]);
        module.F_UrlAddress = '/MesDev/MaterInBill/PostIndex?startTime=' + startTime + '&endTime=' + endTime;
        top.ayma.frameTab.openNew(module);       
    }
    //rkmx = function (m_goodscode)
    // {
    //     var dateParam = { StartTime: startTime, EndTime: endTime };
    //     $('#girdtable_detail').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
    //     $('#pageTab a[href="#page_detail"]').tab('show'); // 通过名字选择
    //}
    Detaile = function (m_goodscode, type) {
        var dateParam = { StartTime: startTime, EndTime: endTime };
        if (type == '入库明细') {
            $('#girdtable_detail').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
            $('#pageTab a[href="#page_detail"]').tab('show'); // 通过名字选择
        }
        if (type == '出库明细') {
            $('#girdtable_Outbounddetails').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
            $('#pageTab a[href="#page_Outbounddetails"]').tab('show'); // 通过名字选择
        } 
        if (type == '退库明细') {
            $('#girdtable_withdrawingdetails').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
            $('#pageTab a[href="#page_withdrawingdetails"]').tab('show'); // 通过名字选择
        }
        if (type == '原物料销售明细') {
            $('#girdtable_materialssales').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
            $('#pageTab a[href="#page_materialssales"]').tab('show'); // 通过名字选择
        }
        if (type == '报废数量明细') {
            $('#girdtable_scrapnumber').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
            $('#pageTab a[href="#page_scrapnumber"]').tab('show'); // 通过名字选择
        }
        if (type == '其它入库明细') {
            $('#girdtable_other').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
            $('#pageTab a[href="#page_other"]').tab('show'); // 通过名字选择
        }
        if (type == '其它出库明细') {
            $('#girdtable_otherout').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
            $('#pageTab a[href="#page_otherout"]').tab('show'); // 通过名字选择
        }
        if (type == '退供应商明细') {
            $('#girdtable_supplierback').jfGridSet('reload', { param: { M_GoodsCode: m_goodscode, queryJson: JSON.stringify(dateParam) } });
            $('#pageTab a[href="#page_supplierback"]').tab('show'); // 通过名字选择
        }
    }
    page.init();
    function getNextDate(date,day) {  
        var dd = new Date(date);
        dd.setDate(dd.getDate() + day);
        var y = dd.getFullYear();
        var m = dd.getMonth() + 1 < 10 ? "0" + (dd.getMonth() + 1) : dd.getMonth() + 1;
        var d = dd.getDate() < 10 ? "0" + dd.getDate() : dd.getDate();
        return y + "-" + m + "-" + d;
    };
}
