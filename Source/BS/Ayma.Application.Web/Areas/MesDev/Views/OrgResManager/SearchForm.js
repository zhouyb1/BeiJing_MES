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
            $("#O_StockCode").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            });

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

            //绑定班组
            $('#O_TeamCode').select({
                type: 'default',
                value: 'T_Code',
                text: 'T_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetTeamList',
                // 访问数据接口参数
                param: {}
            });

            //组装前
            $('#Mes_OrgResDetail_h').jfGrid({
                headData: [ {
                          label: "组装前物料",
                          name: "B",
                          width: 180,
                          align: "center",
                          children: [
                              {label: "物料名称", name: "O_GoodsName", width: 120, align: "center" },
                              { label: "物料编码", name: "O_GoodsCode", width: 90, align: "center" },
                              { label: "库存", name: "StockQty", width: 80, align: "center", hidden: keyValue == "" ? false : true },
                              { label: "单位", name: "O_Unit", width: 60, align: "center" },
                              { label: "数量", name: "O_Qty", width: 80, align: "center", statistics: true },
                              { label: "单位成本", name: "O_Price", width: 80, align: "center" },
                              { label: "金额", name: "O_Amount", width: 80, align: "center",statistics:true },

                          ]
                      }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 220,
                height: 220,
                isStatistics:true
            });
            //组装后
            $('#Mes_OrgResDetail_d').jfGrid({
                headData: [
                    {
                        label: "组装后物料",
                        name: "B",
                        width: 180,
                        align: "center",
                        children: [
                            { label: "物料名称", name: "O_SecGoodsName", width: 120, align: "center" },
                            { label: "物料编码", name: "O_SecGoodsCode", width: 90, align: "center", },
                            { label: "单位", name: "O_SecUnit", width: 80, align: "center" },
                            { label: "数量", name: "O_SecQty", width: 80, align: "center", statistics: true },
                            { label: "单位成本", name: "O_SecPrice", width: 80, align: "center" },
                            { label: "金额", name: "O_SecAmount", width: 80, align: "center", statistics: true },
                        ]
                    },
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 220,
                height: 220,
                isStatistics:true
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/OrgResManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            var tempArr = [];
                            tempArr = tempArr.concat(data[id]);
                            $('#Mes_OrgResDetail_h').jfGridSet('refreshdata', { rowdatas: tempArr });
                            var arr = [];
                            for (var i = 0; i < data[id].length; i++) {
                                if (!arr.includes(data[id][i].O_SecGoodsCode)) {
                                    arr.push(data[id][i].O_SecGoodsCode);
                                } else {
                                    data[id].splice(i, 1);
                                    i--;
                                }
                            }
                            $('#Mes_OrgResDetail_d').jfGridSet('refreshdata', { rowdatas: data[id] });
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
