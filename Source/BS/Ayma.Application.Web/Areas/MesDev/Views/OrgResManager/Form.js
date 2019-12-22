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
                value: 'W_Name',
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
            $('#O_WorkShopName').select(dfop).on('change', function() {
                var name = $(this).selectGet();
                //绑定车间编码
                ayma.httpAsyncGet(top.$.rootUrl + '/MesDev/Tools/ByNameGetWorkShopEntity?name='+name, function(result) {
                    $('#O_WorkShopCode').val(result.data.W_Code);
                });
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
            //$('#O_ProCode').select();

            //var orderNo = "";
            //if (!!keyValue) {//根据主键获取生产订单号
            //    $.ajax({
            //        url: top.$.rootUrl + '/MesDev/OrgResManager/GetOrderNoBy?keyValue=' + keyValue,
            //        type: "GET",
            //        dataType: "json",
            //        async: false,
            //        success: function (data) {
            //            orderNo = data.info;
            //        }
            //    });
            //}
            //$('#O_OrderNo').select({
            //    type: 'default',
            //    value: 'P_OrderNo',
            //    text: 'P_OrderNo',
            //    // 展开最大高度
            //    maxHeight: 200,
            //    // 是否允许搜索
            //    allowSearch: true,
            //    // 访问数据接口地址
            //    url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderList?orderNo=' + orderNo,
            //    // 访问数据接口参数
            //    param: {}
            //});

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

            //添加物料
            $('#am_add').on('click', function () {

                if ($('#O_ProCode').selectGet()=="") {
                    ayma.alert.error('请先选择工序');
                    return false;
                }
                if ($('#O_WorkShopName').selectGet()=="") {
                    ayma.alert.error('请先选择车间');
                    return false;
                }
               
                ayma.layerForm({
                    id: 'MaterListForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/OrgResManager/GoodsListIndex?formId=' + parentFormId + '&workShop=' + $('#O_WorkShopCode').val() + '&proNo=' + $('#O_ProCode').selectGet(),
                    width: 900,
                    height: 700,
                    maxmin: true,
                    callback: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
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
                        { label: "物料名称", name: "O_GoodsName", width: 120, align: "center" },
                        {
                            label: "数量", name: "O_Qty", width: 80, align: "center", editType: 'input',
                            editOp: {
                                callback: function (rownum, row) {
                                    if (/\D/.test(row.O_Qty.toString().replace('.', ''))) { //验证只能为数字
                                        row.O_Qty = 0;
                                    }
                                    if (row.O_Qty >row.stockQty) {
                                        ayma.alert.error('不能大于库存数量');
                                        row.O_Qty = 0;
                                    } 
                                }
                            }
                        },
                        { label: "单价", name: "O_Price", width: 60, align: "center" },
                        { label: "单位", name: "O_Unit", width: 60, align: "center" },
                        { label: "批次", name: "O_Batch", width: 80, align: "center", editType: 'input' }
                    ]
                },
                    {
                        label: "组装后",
                        name: "B",
                        width: 160,
                        align: "center",
                        children: [
                            { label: "物料编码", name: "O_SecGoodsCode", width: 90, align: "center", },
                            { label: "物料名称", name: "O_SecGoodsName", width: 120, align: "center" },
                            {
                                label: "数量", name: "O_SecQty", width: 80, align: "center", editType: 'input',
                                editOp: {
                                    callback: function (rownum, row) {
                                        if (/\D/.test(row.O_SecQty.toString().replace('.', ''))) { //验证只能为数字
                                            row.O_SecQty = 0;
                                        } else {
                                            row.O_SecPrice = (row.O_Qty * row.O_Price / row.O_SecQty).toFixed(2);
                                        }
                                    }
                                }
                            },
                             { label: "单价", name: "O_SecPrice", width: 80, align: "center" },
                             { label: "单位", name: "O_Unit", width: 80, align: "center" },
                             { label: "批次", name: "O_SecBatch", width: 80, align: "center" }
                        ]
                    }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount:3
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

    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_OrgResDetail').jfGridGet('rowdatas');
        if (data[0].O_GoodsCode == undefined || data[0].O_GoodsCode == "") {
            ayma.alert.error('请添加物料');
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('[data-table="Mes_OrgResHead"]').GetFormData()),
            detailList: JSON.stringify(data)
        };
       
        $.SaveForm(top.$.rootUrl + '/MesDev/OrgResManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };

    top.NewGirdData = function () {
        return $('#Mes_OrgResDetail').jfGridGet('rowdatas');
    }
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_OrgResDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row,1);
                var rowFlag = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].O_GoodsCode == row.O_GoodsCode && rows[k].O_Batch == row.O_Batch) {
                        rowFlag = false;
                    }
                }
                if (rowFlag) {
                    rows.push(row);
                }
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    var isExist = true;
                    for (var j = 0; j < rows.length; j++) {
                        if (data[i].O_GoodsCode == rows[j].O_GoodsCode && data[i].O_Batch == rows[j].O_Batch) {
                            isExist = false;
                        }
                    }
                    if (isExist) {
                        rows.push(data[i]);
                    }
                }
            }
        }
        //数组过滤
        var filterarray = $.grep(rows, function (item) {
            return item["O_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_OrgResDetail').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["O_GoodsCode"] == row["w_goodscode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    page.init();
}
