/* * 创建人：超级管理员
 * 日  期：2019-03-11 19:22
 * 描  述：领料单
 */
var acceptClick;
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var parentFormId = request('formId');//父级窗口id
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
            $("#C_StockName").select(dfop).on('change', function() {
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
            ////绑定班组
            //$("#G_TeamCode").select(dfop).on('change', function () {
            //    var code = $(this).selectGet();
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetTeamEntity',
            //        data: { code: code },
            //        success: function (data) {
            //            var entitys = JSON.parse(data).data;
            //            $("#C_Teamcode").val(entitys == null ? "" : entitys.T_Code);
            //        }
            //    });
            //});
           
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
                if ($("#C_StockName").selectGet()=="") {
                    ayma.alert.error("请选择原料仓库！");
                    return false;
                }
                ayma.layerForm({
                    id: 'OrderMaterListForm',
                    title: '添加订单物料',
                    url: top.$.rootUrl + '/MesDev/PickingMater/OrderMaterList?formId=' + parentFormId + '&stockCode=' + $("#C_StockCode").val() + '&C_TeamCode=' + $("#C_TeamCode").selectGet(),
                    width: 100,
                    height: 1000,
                    maxmin: true,
                    callback: function(id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_CollarDetail').jfGrid({
                headData: [
                    { label: "领料单", name: "C_CollarNo", width: 130, align: "left", hidden: true },
                    { label: "生产订单", name: "C_OrderNo", width: 130, align: "left", hidden: true },
                    { label: "物料编码", name: "C_GoodsCode", width: 130, align: "left"},
                    { label: "单价", name: "C_Price", width: 130, align: "left" },
                    { label: "物料名称", name: "C_GoodsName", width: 130, align: "left" },
                    { label: "单位", name: "C_Unit", width: 60, align: "left" },
                    {
                        label: "数量", name: "C_Qty", width: 60, align: "left", editType: 'numinput',
                        editOp: {
                            callback: function (rownum, row) {
                                if (row.C_Qty != undefined && !!row.C_Qty) {
                                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.C_Qty.toString().replace('.', ''))) {
                                        ayma.alert.error("数量必须是非负数.");
                                        row.C_Qty = 0;
                                    }
                                }
                            }
                        }
                    },
                    { label: "批次", name: "C_Batch", width: 80, align: "left" },
                    { label: "班组名称", name: "C_TeamName", width: 80, align: "left" , hidden:true },
                    { label: "班组编号", name: "C_TeamCode", width: 80, align: "left",hidden:true },
                    {
                        label: '供应商编码', name: 'C_SupplyCode', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '供应商名称', name: 'C_SupplyName', width: 140, align: 'left', editType: 'label'
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
        if (data[0].C_GoodsCode == undefined || data[0].C_GoodsCode == "") {
            ayma.alert.error('请添加物料');
            return false;
        }

        if ($("#C_StockName").selectGet() == $("#C_StockToName").selectGet()) {
            
            ayma.alert.error('领料仓库与原料仓库相同');
            return false;
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
    page.init();
}
