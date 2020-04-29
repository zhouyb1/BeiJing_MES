/* * 创建人：超级管理员
 * 日  期：2019-03-11 19:22
 * 描  述：领料单
 */
var js_method_stock;
var acceptClick;
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var parentFormId = request('formId');//父级窗口id
var keyValue = request('keyValue');
var status = request('status');
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
            if (status==2) {
                $('#C_StockName').css('background', '#f1efef');
                $('#C_StockName').attr('readonly', true);
                $("#C_StockToName").css('background', '#f1efef');
                $('#C_StockToName').attr('readonly', true);
                $('#am_add').attr('disabled', true);
            }
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
                url: top.$.rootUrl + '/MesDev/Tools/GetStockListByParam',
                // 访问数据接口参数
                param: { strWhere: "S_Kind =1" }
            }
            $("#C_StockName").select(dfop).on('change', function () {
                if (status == "" || status==1) {
                    $('#Mes_CollarDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function(data) {
                        var entity = JSON.parse(data).data;
                        $("#C_StockCode").val(entity == null ? "" : entity.S_Code);
                    }
                });
            });
             dfop = {
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockListByParam',
                // 访问数据接口参数
                param: { strWhere: "S_Kind =4" }
            }
            //绑定目标仓
            $("#C_StockToName").select(dfop).on('change', function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function(data) {
                        var entity = JSON.parse(data).data;
                        $("#C_StockToCode").val(entity==null?"":entity.S_Code);
                    }
                });
            });
                    ////班组的分类
            $("#C_TeamCode").select({
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
           
            //绑定订单号
            $("#P_OrderNo").select({
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
                param: {}
            });
             
            //添加订单原物料
            $('#am_add').on('click', function () {
                if ($('#C_StockToName').selectGet()=="") {
                    ayma.alert.error('请先选择领料仓库！');
                    return false;
                }
                ayma.layerForm({
                    id: 'OrderMaterListForm',
                    title: '添加订单物料',
                    url: top.$.rootUrl + '/MesDev/PickingMater/OrderMaterList?formId=' + parentFormId + '&stockCode=' + $("#C_StockCode").val()+ '&C_StockName=' + $("#C_StockName").selectGet(),
                    width: 1000,
                    height: 600,
                    maxmin: true,
                    btn: ['关闭'],
                    callback: function(id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_CollarDetail').jfGrid({
                headData: [
                { label: "领料单",   name: "C_CollarNo", width: 130, align: "left", hidden: true },
                { label: "生产订单", name: "C_OrderNo", width: 130, align: "left", hidden: true },
                { label: "物料编码", name: "C_GoodsCode", width: 80, align: "left" },
                { label: "物料名称", name: "C_GoodsName", width: 100, align: "left" },
                {
                    label: "单价", name: "C_Price", width: 50, align: "left", formatter: function (value, row, dfop) {
                    if (row.C_Price != undefined && !!row.C_Price) {
                        return row.C_Price.toFixed(6) / 1;
                        }
                    if (row.C_Price == "") {
                        return row.C_Price = 0;
                        }
                    }
                },
                { label: "单位", name: "C_Unit", width: 40, align: "left" },
                { label: "批次", name: "C_Batch", width: 80, align: "left" },
                {
                    label: "领料数量",
                    name: "C_Qty",
                    width: 60,
                    align: "left",
                    editType: 'input',
                    editOp: {
                        callback: function(rownum, row) {
                            if (row.C_Qty != undefined && !!row.C_Qty) {
                                if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.C_Qty.toString().replace('.', ''))) {
                                    ayma.alert.error("数量必须是非负数.");
                                    row.C_Qty = 0;
                                }
                            }
                            if (row.C_Qty > row.StockQty) {
                                ayma.alert.error("领料数量不能大于库存数量");
                                row.C_Qty = row.StockQty;
                            }
                            row.C_Qty2 = (row.C_Qty / row.C_UnitQty).toFixed(6) / 1;
                        }
                    }
                },
                {
                    label: "包装数量",
                    name: "C_Qty2",
                    width: 60,
                    align: "left",
                    editType: 'input',
                    editOp: {
                        callback: function(rownum, row) {
                            if (row.C_Qty2 != undefined && !!row.C_Qty2) {
                                if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.C_Qty2.toString().replace('.', ''))) {
                                    ayma.alert.error("包装数量必须是非负数.");
                                    row.C_Qty2 = 0;
                                }
                            }
                            row.C_Qty = row.C_Qty2 * row.C_UnitQty;
                            if (row.C_Qty > row.StockQty) {
                                ayma.alert.error("领料数量不能大于库存数量");
                                row.C_Qty = row.StockQty;
                                row.C_Qty2 = (row.C_Qty / row.C_UnitQty).toFixed(6) / 1;
                            }
                        }
                    }
                },
                {
                    label: "包装规格", name: "C_UnitQty", width: 60, align: "left", editType: 'select', editOp: {
                        width: 400,
                        height: 400,
                        colData: [
                           { label: '物料名称', name: 'S_GoodsName', width: 100, align: 'left' },
                           { label: '包装数', name: 'S_UnitQty', width: 100, align: 'left', },
                        ],
                        url: top.$.rootUrl + '/MesDev/Tools/ByGoodsCodeGetUnit',
                        callback: function (selectdata, rownum, row) {
                            if (row.C_GoodsName == selectdata.S_GoodsName) {
                                row.C_UnitQty = selectdata.S_UnitQty;
                                ayma.alert.success("物料【" + row.C_GoodsName + "】的包装规格更改成功为【" + row.C_UnitQty + "】！");
                            } else {
                                ayma.alert.error("您需要更改包装规格的物料是【" + row.C_GoodsName + "】物料名称不符,请重新选择！");
                                ayma.layer.error("")
                            }
                        }
                    }
                },
                { label: "包装单位", name: "C_Unit2", width: 60, align: "left" },
                { label: "库存", name: "StockQty", width: 40, align: "left", hidden: keyValue == "" ? false : true },
            
                { label: "原仓库编码", name: "C_StockCode", width: 90, align: "left" },
                {
                    label: "原仓库名称",
                    name: "C_StockName",
                    width: 90,
                    align: "left",
                    formatter: function(value, row, dfop) {
                        if (row != null && row.C_StockName != undefined) {
                            if (row != null && row.C_StockName != undefined) {
                                return "<a href =# style=text-decoration:underline title='点击查询库存' onclick=js_method_stock('" + row.C_StockCode + "','9b04a0f2-28c0-4a58-973d-47bd51944a1c')>" + row.C_StockName + "</ a>";
                            } else {
                                return row.C_StockName;
                            }
                            if (row.C_StockName == "蔬菜库")
                            {
                               return row.C_Qty = row.StockQty;
                            }
                        }
                    }
                },
                {
                    label: "计划数量", name: "C_PlanQty", width: 100, align: "left"
                    , formatter: function (value, row, dfop) {
                        if (row.C_PlanQty != undefined && !!row.C_PlanQty) {
                            return row.C_PlanQty.toFixed(6) / 1;
                        }
                        if (row.C_PlanQty == "") {
                            return row.C_PlanQty = 0;
                        }
                    }
                },
                {
                    label: "建议数量", name: "C_SuggestQty", width: 100, align: "left", formatter: function (value, row, dfop) {
                        if (row.C_SuggestQty != undefined && !!row.C_PlanQty) {
                            return row.C_SuggestQty.toFixed(6) / 1;
                        }
                        if (row.C_SuggestQty == "") {
                            return row.C_SuggestQty = 0;
                        }
                    }
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
                $.SetForm(top.$.rootUrl + '/MesDev/PickingMater/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_CollarDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_CollarDetail').jfGridSet('refreshdata', { rowdatas: data });
        }

    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_CollarDetail').jfGridGet('rowdatas');
        if (data.length==0||data[0].C_GoodsCode == null) {
            ayma.alert.error('请添加物料');
            return false;
        }
        if ($("#C_StockName").selectGet() == $("#C_StockToName").selectGet()) {
            
            ayma.alert.error('领料仓库与原料仓库相同');
            return false;
        }
        for (var i = 0; i < data.length; i++) {
            data[i].C_Price = null;        
        }
       
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_CollarHead"]').GetFormData());
        postData.strmes_CollarDetailEntity = JSON.stringify(data);
        $.SaveForm(top.$.rootUrl + '/MesDev/PickingMater/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {

        var rows = $('#Mes_CollarDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var rowFlag = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].I_GoodsCode == row.C_GoodsCode && rows[k].I_Batch == row.C_Batch) {
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
                        if (data[i].I_GoodsCode == rows[j].C_GoodsCode && data[i].I_Batch == rows[j].C_Batch) {
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
            return item["C_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_CollarDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["C_GoodsCode"] == row["I_GoodsCode"] && rows[i]["C_Batch"] == row["I_Batch"]) {
                rows.splice(i, 1);
                //tmp.delete(row);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_CollarDetail').jfGridGet('rowdatas');
    }
    js_method_stock = function (code, moduleId) {
        var module = top.ayma.clientdata.get(['modulesMap', moduleId]);
        module.F_UrlAddress = '/MesDev/InventorySeach/Index?stock=' + encodeURIComponent(code);
        top.ayma.frameTab.openNew(module);
        var index = window.parent.layer.getFrameIndex(window.name);
        window.parent.layer.close(index);//关闭layer
    }
    page.init();
}
