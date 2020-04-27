/* * 创建人：超级管理员
 * 日  期：2019-03-13 10:06
 * 描  述：领料单查询
 */
var C_CollarNo = decodeURIComponent(request('keyValue'));
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
            }, 220, 500);
            $('#M_GoodsName').select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许索搜
                allowSearch: true,
                // 访问数据接口地址 
                url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
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
            }).on('change', function () {          
                var code = $(this).selectGet();
                if (code == "")
                {
                    $('#M_GoodsName').selectRefresh({
                        type: 'default',
                        value: 'G_Name',
                        text: 'G_Name',
                        // 展开最大高度
                        maxHeight: 200,
                        // 是否允许索搜
                        allowSearch: true,
                        // 访问数据接口地址 
                        url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
                        // 访问数据接口参数
                        param: {}
                    });
                }
                else {
                    $('#M_GoodsName').selectRefresh({
                        type: 'default',
                        value: 'G_Name',
                        text: 'G_Name',
                        // 展开最大高度
                        maxHeight: 200,
                        // 是否允许索搜
                        allowSearch: true,
                        // 访问数据接口地址 
                        url: top.$.rootUrl + '/MesDev/Tools/ByStokcGetGoodsEntity?code=' + code,
                        // 访问数据接口参数
                        param: {}
                    });
                }
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.href = location.pathname;
            });
            // 编辑
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '单据详情',
                        url: top.$.rootUrl + '/MesDev/PickingMaterQuery/Form?keyValue=' + keyValue + '&formId=""',
                        width: 800,
                        height: 600,
                        maxmin: true,
                        btn: null,
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
                        url: top.$.rootUrl + '/MesDev/PickingMaterQuery/Form?keyValue=' + keyValue + '&formId=""',
                        width: 800,
                        height: 600,
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
                var keyValue = $('#girdtable').jfGridValue('C_CollarNo');
                var status = $("#girdtable").jfGridValue("P_Status");
                if (ayma.checkrow(keyValue, true)) {
                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '领料单打印',
                        url: top.$.rootUrl + '/MesDev/PickingMater/PrintReport?keyValue=' + keyValue + "&report=CollacReport&data=Picking",
                        width: 1000,
                        height: 800,
                        maxmin: true,
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
                url: top.$.rootUrl + '/MesDev/PickingMaterQuery/GetPageList?C_CollarNo=' + C_CollarNo,
                headData: [
                      {
                          label: "状态", name: "P_Status", width: 90, align: "center",
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
                    { label: "单据编号", name: "C_CollarNo", width: 130, align: "center"},
                    { label: "领料仓编码", name: "C_StockToCode", width: 90, align: "center" },
                    { label: "领料仓名称", name: "C_StockToName", width: 130, align: "center" },
                    { label: "备注", name: "C_Remark", width: 130, align: "center" },
                    {
                        label: "单据时间", name: "P_OrderDate", width: 100, align: "center",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "提交人", name: "M_UploadBy", width: 90, align: "center" },
                    { label: "提交时间", name: "M_UploadDate", width: 90, align: "center" },
                    { label: "添加人", name: "C_CreateBy", width: 90, align: "center" },
                    { label: "创建时间", name: "C_CreateDate", width: 130, align: "center" },
                  
                ],
                
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'C_CreateDate',
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
