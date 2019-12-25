/* * 创建人：超级管理员
 * 日  期：2019-03-18 15:14
 * 描  述：组装与拆分单据制作
 */
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var stockCode;
var parentFormId = request('formId');
var acceptClick;
var keyValue = request('keyValue');
var tmp = new Map();
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
           var  dfop = {
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
           }
            //绑定车间
            $('#O_WorkShopCode').select(dfop);
           
            //绑定工序
            $('#O_ProCode').select({
                type: 'default',
                value: 'P_ProNo',
                text: 'P_ProName',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProceList',
                // 访问数据接口参数
                param: {}
            });
            $('#Mes_OrgResDetail').jfGrid({
                headData: [
                {
                    label: "组装前",
                    name: "B",
                    width: 160,
                    align: "center",
                    children: [
                        { label: "物料编码", name: "O_GoodsCode", width: 90, align: "center", },
                        { label: "物料名称", name: "O_GoodsName", width: 130, align: "center" },
                        { label: "单价", name: "O_Price", width: 80, align: "center" },
                        { label: "单位", name: "O_Unit", width: 80, align: "center" },
                        {
                            label: "数量", name: "O_Qty", width: 100, align: "center"
                        },
                        { label: "批次", name: "O_Batch", width: 100, align: "center"}
                    ]
                },
                    {
                        label: "组装后",
                        name: "B",
                        width: 160,
                        align: "center",
                        children: [
                            { label: "物料编码", name: "O_SecGoodsCode", width: 90, align: "center", },
                            { label: "物料名称", name: "O_SecGoodsName", width: 130, align: "center" },
                            { label: "单价", name: "O_SecPrice", width: 80, align: "center" },
                            { label: "单位", name: "O_SecUnit", width: 80, align: "center" },
                            { label: "数量", name: "O_SecQty", width: 100, align: "center" },
                            { label: "批次", name: "O_SecBatch", width: 90, align: "center" }
                        ]
                    }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/OrgResManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_OrgResDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        },
        search: function(data) {
            data = data || {};
            $('#Mes_OrgResDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };

    
    page.init();
}
