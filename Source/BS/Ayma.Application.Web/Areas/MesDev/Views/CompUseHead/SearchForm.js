/* * 创建人：超级管理员
 * 日  期：2019-07-04 16:14
 * 描  述：强制使用记录单据
 */
var acceptClick;
//var refreshGirdData;//表格商品添加
//var RemoveGridData;//移除表格
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');//上一级formId
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#C_Status').DataItemSelect({ code: 'CompUserStatus' });
            $('#C_WorkShop').select({
                type: 'default',
                value: 'W_Code',
                text: 'W_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetWorkShopList',
                // 访问数据接口参数
                param: {}
            });
            var orderNo = "";
            if (!!keyValue) {//根据主键获取生产订单号
                $.ajax({
                    url: top.$.rootUrl + '/MesDev/MaterInBill/GetOrderNoBy?keyValue=' + keyValue,
                    type: "GET",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        orderNo = data.info;
                    }
                });
            }
            //生产订单号
            $('#C_OrderNo').select({
                type: 'default',
                value: 'P_OrderNo',
                text: 'P_OrderNo',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderList?orderNo=' + orderNo,
                // 访问数据接口参数
                param: {}
            });
            var dfop = {
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
            }
            $("#C_StockName").select(dfop).on('change', function () {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#C_StockCode").val(entity.S_Code);
                    }
                });
            });
            
            $('#Mes_CompUseDetail').jfGrid({
                headData: [
                    {
                        label: '主键', name: 'ID', width: 160, align: 'left', hidden: true
                    },
                   
                    {
                        label: '物料编码', name: 'C_GoodsCode', width: 100, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'C_GoodsName', width: 160, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'C_Unit', width: 70, align: 'left', editType: 'label'
                    },
                    
                    {
                        label: '批次', name: 'C_Batch', width: 120, align: 'left', editType: 'label'
                    },
                    {
                        label: '价格', name: 'C_Price', width: 120, align: 'left', editType: 'label'
                    },
                    {
                        label: '数量', name: 'C_Qty', width: 80, align: 'left', editType: 'label'
                    },
                    {
                        label: '备注', name: 'C_Remark', width: 200, align: 'left', editType: 'label'
                    },
                ],
                isAutoHeight: true,
                isEidt: false,
                footerrow: true,
                minheight: 400,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/CompUseHead/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_CompUseDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            } else {
                $("#C_Status").selectSet(1);//新增时默认为单据生成
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_CompUseDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
   
    
    page.init();
}
