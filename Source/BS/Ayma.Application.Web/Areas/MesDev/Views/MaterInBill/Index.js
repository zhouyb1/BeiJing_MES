/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
 */
var refreshGirdData;
var js_method;
var js_method_stock;
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
            $('#M_Status').DataItemSelect({ code: 'MaterInStatus' });

            $('#M_StockName').select({
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
                param: {}

            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
              
                    ayma.layerForm({
                        id: 'MaterInBillForm',
                        title: '新增入库单',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?formId=MaterInBillForm',
                        width: 900,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                
            });
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
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var statu = $('#girdtable').jfGridValue('M_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterInBillForm',
                        title: '编辑入库单',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?status=' + statu + '&keyValue=' + keyValue + '&formId=MaterInBillForm',
                        width: 900,
                        height: 700,
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
                var statu = $('#girdtable').jfGridValue('M_Status');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'MaterInBillForm',
                        title: '编辑入库单',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?status=' + statu + '&keyValue=' + keyValue + '&formId=MaterInBillForm',
                        width: 900,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //删除单据
            $("#am_delete").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("M_MaterInNo");
                if (ayma.checkrow(orderNo)) {
                    var status = $("#girdtable").jfGridValue("M_Status");
                    if (status == "2") {
                        ayma.alert.error("已审核不能删除");
                        return false;
                    }
                    ayma.layerConfirm('是否确认删除该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteMaterInBill', { orderNo: orderNo, proc: 'sp_MaterIn_Delete', type: 3 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //审核单据
            $("#am_auditing").on('click', function () {
                var keyValue = $("#girdtable").jfGridValue("ID");
                var status = $("#girdtable").jfGridValue("M_Status");
                if (status == "2") {
                    ayma.alert.error("已审核");
                    return false;
                }
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/AuditingBill', { keyValue: keyValue, tables: 'Mes_MaterInHead', field: 'M_Status' }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //提交单据
            $("#am_post").on('click', function() {
                var orderNo = $("#girdtable").jfGridValue("M_MaterInNo");
                var status = $("#girdtable").jfGridValue("M_Status");
                if (status=="1") {
                    ayma.alert.error("未审核");
                    return false;
                }
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认提交该单据！', function(res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteMaterInBill', { orderNo: orderNo, proc: 'sp_MaterIn_Post', type: 1 }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //打印
            // 快速打印
            $('#am_print').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('M_MaterInNo');
                var status = $("#girdtable").jfGridValue("M_Status");
                if (status != "2") {
                    ayma.alert.error("单据未审核");
                    return false;
                }
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
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetPageList',
                headData: [
                    {
                        label: "状态", name: "M_Status", width: 100, align: "center",
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
                    { label: "入库单号", name: "M_MaterInNo", width: 160, align: "center", sort: true },
                    { label: "供应商名称", name: "M_SupplyName", width: 160, align: "center" },
                    { label: "供应商编码", name: "M_SupplyCode", width: 90, align: "center", hidden: true },
                    { label: "仓库编码", name: "M_StockCode", width: 90, align: "center" ,hidden:true},
                    { label: "仓库名称", name: "M_StockName", width: 160, align: "center", hidden: true },
                    { label: "添加人", name: "M_CreateBy", width: 100, align: "center" },
                    { label: "添加时间", name: "M_CreateDate", width: 160, align: "center", sort: true },
                    { label: "修改人", name: "M_UpdateBy", width: 100, align: "center" },
                    { label: "修改时间", name: "M_UpdateDate", width: 160, align: "center" },
                    { label: "备注", name: "M_Remark", width: 160, align: "left" },
                ],
                onRenderComplete: function (rows) {
                    var lengh = rows.length;
                    for (var i = 0; i < lengh; i++) {
                        $("[rownum='rownum_girdtable_" + i + "'][colname='M_SupplyName']").html("<a href =# style=text-decoration:underline title='点击查询供应商资料' onclick=js_method('" + rows[i].M_SupplyCode + "')>" + rows[i].M_SupplyName + "</ a>");
                        $("[rownum='rownum_girdtable_" + i + "'][colname='M_StockName']").html("<a href =# style=text-decoration:underline title='点击查询库存' onclick=js_method_stock('" + rows[i].M_StockCode + "','6470af9c-c0be-4455-b8cc-164b9865bb24')>" + rows[i].M_StockName + "</ a>");

                    }
                },
                mainId: 'ID',
                isPage: true,
                sidx: 'M_CreateDate',
                sord:'DESC'
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
    js_method = function (code) {
        ayma.layerForm({
            id: 'MaterInBillForm',
            title: '供应商资料',
            url: top.$.rootUrl + '/MesDev/SupplyList/Form?keyValue=' + code+'&type=view',
            width: 600,
            height: 400,
            btn: null,
            maxmin: true,
        });
    };
    js_method_stock = function(code,moduleId) {
        var module = top.ayma.clientdata.get(['modulesMap', moduleId]);
        module.F_UrlAddress = '/MesDev/InventorySeach/Index?stock=' + encodeURIComponent(code);
        top.ayma.frameTab.openNew(module);
        
    }
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
