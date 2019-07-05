/* * 创建人：超级管理员
 * 日  期：2019-03-14 16:30
 * 描  述：退供应商单制作
 */
var acceptClick;
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var tmp = new Map();
var materInKeyValue = request('materInKeyValue');
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
            var dfop = {
                type: 'default',
                value: 'S_Code',
                text: 'S_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            }
            $("#b_stockcode").select(dfop).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#b_stockname").val(entity.S_Name);
                    }
                });
            });
            //添加物料
            $("#am_add").on("click", function () {
                var stockCode = $("#b_stockcode").selectGet();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsListIndexForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/Mes_BackSupply/BackGoodsList?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_BackSupplyDetail').jfGrid({
                headData: [
                    {
                        label: '主键', name: 'ID', width: 160, align: 'left', editType: 'label', hidden: 'true'
                    },
                    {
                        label: '退供应商单号', name: 'B_BackSupplyNo', width: 160, align: 'left', editType: 'label', hidden: 'true'
                    },
                    {
                        label: '物料编码', name: 'b_goodscode', width: 100, align: 'center', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'b_goodsname', width: 160, align: 'center', editType: 'label'
                    },
                    {
                        label: '单位', name: 'b_unit', width: 70, align: 'center', editType: 'label'
                    },
                    {
                        label: '数量', name: 'b_qty', width: 80, align: 'center', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.B_Qty.toString().replace('.', ''))) { //验证只能为数字
                                    row.B_Qty = 0;
                                }

                            }
                        }
                    },
                    {
                        label: '批次', name: 'b_batch', width: 160, align: 'center', editType: 'label'
                    },
                    {
                        label: '备注', name: 'B_Remark', width: 200, align: 'left',editType: 'input'
                    },
                ],
                isAutoHeight: true,
                isEidt: true,
                reloadSelected: true,
                isMultiselect: true,
                footerrow: true,
                minheight: 400
            });
        },
        initData: function () {
            if (!!materInKeyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_BackSupply/GetAddFormData?materInKeyValue=' + materInKeyValue, function (data) {
                   
                    for (var id in data) {
                        
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_BackSupplyDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                          
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id].Table[0]);
                        }
                    }
                });
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_BackSupplyDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        //var data = $('#Mes_BackSupplyDetail').jfGridGet('rowdatas');
        //if (data[0].B_GoodsCode == undefined || data[0].B_GoodsCode == "") {
        //    ayma.alert.error('请添加物料');
        //    return false;
        //}
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_BackSupplyHead"]').GetFormData());
        postData.strmes_BackSupplyDetailList = JSON.stringify($('#Mes_BackSupplyDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_BackSupply/SaveForm?keyValue=' + "", postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_BackSupplyDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                rows.push(row);
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    rows.push(data[i]);
                }
            }
        }
        //数组过滤
        var filterarray = $.grep(rows, function (item) {
            return item["B_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_BackSupplyDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["B_GoodsCode"] == row["i_goodscode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_BackSupplyDetail').jfGridGet('rowdatas');
    }
    page.init();
}
