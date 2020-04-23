/* * 创建人：超级管理员
 * 日  期：2019-01-09 10:20
 * 描  述：调拨单制作
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
            $('#R_Status').DataItemSelect({ code: 'ProOutStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
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
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'RequistBill',
                    title: '新增调拨单',
                    url: top.$.rootUrl + '/MesDev/RequistBill/Form?formId=RequistBill',
                    width: 950,
                    height: 700,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            $('#R_StockCode').select({
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
            $('#R_StockToCode').select({
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
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('R_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'RequistBill',
                        title: '编辑调拨单',
                        url: top.$.rootUrl + '/MesDev/RequistBill/Form?keyValue=' + keyValue + '&formId=RequistBill' + '&status=' + statu + '&state=1',
                        width: 950,
                        height: 700,
                        maxmin: true,
                        btn: statu == 2 ? null : "",
                        callBack: function(id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //双击编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('R_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'RequistBill',
                        title: '编辑调拨单',
                        url: top.$.rootUrl + '/MesDev/RequistBill/Form?keyValue=' + keyValue + '&formId=RequistBill' + '&status=' + statu + '&state=1',
                        width: 950,
                        height: 700,
                        maxmin: true,
                        btn: statu == 2 ? null : "",
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                if (ayma.checkrow(keyValue)) {
                    var status = $("#girdtable").jfGridValue("R_Status");
                    if (status != "1") {
                        ayma.alert.error("已审核");
                        return false;
                    }
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_RequistHead', field: 'R_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //删除单据
            $("#am_delete").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("R_RequistNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("R_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_MaterTransfer_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $("#am_post").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("R_RequistNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("R_Status");
                    if (status == "1") {
                        ayma.alert.error("未审核");
                        return false;
                    }
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_MaterTransfer_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('R_RequistNo');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'RequistReport',
                        title: '调拨单打印',
                        url: top.$.rootUrl + '/MesDev/RequistBill/PrintReport?keyValue=' + keyValue + "&report=RequistReport&data=Requist",
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
                url: top.$.rootUrl + '/MesDev/RequistBill/GetPageList',
                headData: [
                      {
                          label: "状态", name: "R_Status", width: 100, align: "left",
                          formatterAsync: function (callback, value, row) {
                              ayma.clientdata.getAsync('dataItem', {
                                  key: value,
                                  code: 'RequistStatus',
                                  callback: function (_data) {
                                      console.log(_data)
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
                    { label: "调拨单号", name: "R_RequistNo", width: 160, align: "center" },
                    { label: "原仓库编码", name: "R_StockCode", width: 90, align: "center" },
                    { label: "原仓库名称", name: "R_StockName", width: 160, align: "center" },
                    { label: "调拨仓库编码", name: "R_StockToCode", width: 90, align: "center" },
                    { label: "调拨仓库名称", name: "R_StockToName", width: 160, align: "center" },
                    {
                        label: "单据时间", name: "P_OrderDate", width: 160, align: "center",sort:true,
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "创建时间", name: "R_CreateDate", width: 160, align: "center",sort:true },
                    { label: "添加人", name: "R_CreateBy", width: 100, align: "center" },
                    { label: "修改时间", name: "R_UpdateDate", width: 160, align: "center" },
                    { label: "修改人", name: "R_UpdateBy", width: 100, align: "center" },
                    { label: "备注", name: "R_Remark", width: 160, align: "left" },
            
                ],
                mainId: 'ID',
                isPage: true,
                sidx: 'R_CreateDate',
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
