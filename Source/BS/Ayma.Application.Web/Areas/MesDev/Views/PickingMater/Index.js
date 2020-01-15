/* * 创建人：超级管理员
 * 日  期：2019-03-11 19:22
 * 描  述：领料单
 */
var refreshGirdData;
var js_method;

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
            $('#P_Status').DataItemSelect({ code: 'ProOutStatus' });
         
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
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'PickMaterForm',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/PickingMater/Form?formId=PickMaterForm',
                    width: 1000,
                    height: 600,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('P_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/PickingMater/Form?status=' + statu + '&keyValue=' + keyValue + '&formId=form',
                        width: 1000,
                        height: 600,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 双击编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('P_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/PickingMater/Form?status=' + statu + '&keyValue=' + keyValue + '&formId=form',
                        width: 1000,
                        height: 600,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除单据
            $('#am_delete').on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("C_CollarNo");
                var status = $("#girdtable").jfGridValue("P_Status");

                if (ayma.checkrow(orderNo)) {
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('确认删除单据？', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_Collar_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("P_Status");
                if (status == "2") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_CollarHead', field: 'P_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            //提交单据
            $('#am_post').on('click', function() {
                var orderNo = $("#girdtable").jfGridValue("C_CollarNo");
                var status = $("#girdtable").jfGridValue("P_Status");
                if (status == "1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_Collar_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //打印
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('C_CollarNo');
                var status = $("#girdtable").jfGridValue("P_Status");
                if (status != "2") {
                    ayma.alert.error("单据未审核");
                    return false;
                }
                if (ayma.checkrow(keyValue,true)) {
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
            // 预览打印
            $('#am_printview').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('C_CollarNo');
                if (ayma.checkrow(keyValue)) {

                    ayma.layerForm({
                        id: 'SaleOutReport',
                        title: '领料单打印',
                        url: top.$.rootUrl + '/MesDev/PickingMater/PrintViewReport?keyValue=' + keyValue + "&report=CollacReport",
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
                url: top.$.rootUrl + '/MesDev/PickingMater/GetPageList',
                headData: [
                    {
                        label: "状态", name: "P_Status", width: 100, align: "center",
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
                    { label: "领料单号", name: "C_CollarNo", width: 160, align: "center"},
                    { label: "领料仓编码", name: "C_StockToCode", width: 90, align: "center" },
                    { label: "领料仓", name: "C_StockToName", width: 160, align: "center"},
                    { label: "生产订单号", name: "P_OrderNo", width: 160, align: "left",hidden:"hidden"},
                    { label: "订单时间", name: "P_OrderDate", width: 160, align: "left", hidden: "hidden" },                 
                    { label: "添加人", name: "C_CreateBy", width: 100, align: "center" },
                    { label: "添加时间", name: "C_CreateDate", width: 160, align: "center" },
                    { label: "备注", name: "C_Remark", width: 160, align: "left" },
                ],
                onRenderComplete: function (rows) {
                    var lengh = rows.length;
                    for (var i = 0; i < lengh; i++) {
                        $("[rownum='rownum_girdtable_" + i + "'][colname='C_StockName']").html("<a href =# style=text-decoration:underline  title='点击查询库存' onclick=js_method('" + rows[i].C_StockCode + "','6470af9c-c0be-4455-b8cc-164b9865bb24')>" + rows[i].C_StockName + "</a>");
                    }
                },
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'C_CreateDate',
                sord:'DESC'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        },
    };
     js_method=function(code, moduleId) {
        var module = top.ayma.clientdata.get(['modulesMap', moduleId]);
        module.F_UrlAddress = '/MesDev/InventorySeach/Index?stock=' + encodeURIComponent(code);
        top.ayma.frameTab.openNew(module);
    }
    refreshGirdData = function () {
        page.search();
    };
    
    page.init();
}

