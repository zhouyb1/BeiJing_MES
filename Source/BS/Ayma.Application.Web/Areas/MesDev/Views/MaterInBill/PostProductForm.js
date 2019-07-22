/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：成品入库单查询
 */
var acceptClick;
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');//上一级formId
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initData();
        },
        bind: function () {
            //绑定仓库
            var dfop = {
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProjStockList',
                // 访问数据接口参数
                param: {}
            };
            //绑定仓库
            $('#M_StockName').select(dfop).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#M_StockCode").val(entity.S_Code);
                    }
                });
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
            $('#M_OrderNo').select({
                type: 'default',
                value: 'P_OrderNo',
                text: 'P_OrderNo',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderList',
                // 访问数据接口参数
                param: { orderNo: orderNo }
            });
            //单据状态
            $("#M_Status").DataItemSelect({ code: 'MaterInStatus' });
            $('#Mes_MaterInDetail').jfGrid({
                headData: [
                     {
                         label: 'ID', name: 'ID', width: 160, align: 'left', editType: 'label',hidden:true
                     },
                    {
                        label: '物料编码', name: 'M_GoodsCode', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'M_GoodsName', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'M_Unit', width: 60, align: 'left', editType: 'label'
                    },
                     {
                         label: '数量', name: 'M_Qty', width: 60, align: 'left', editType: 'label'
                     },
                      {
                          label: '价格', name: 'M_Price', width: 60, align: 'left', editType: 'label'
                      },
                    {
                        label: '批次', name: 'M_Batch', width: 100, align: 'left', editType: 'label'
                    },
                    {
                        label: '备注', name: 'M_Remark', width: 160, align: 'left', editType: 'label'
                    },
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: true,
                isMultiselect: true,
                height: 350,
                inputCount: 2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/MaterInBill/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_MaterInDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_MaterInDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    
    page.init();
}
