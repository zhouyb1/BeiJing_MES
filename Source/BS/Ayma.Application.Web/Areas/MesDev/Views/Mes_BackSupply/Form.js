/* * 创建人：超级管理员
 * 日  期：2019-03-14 16:30
 * 描  述：退供应商单制作
 */
var acceptClick;
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');//上一级formId
var status = request('status');
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
            if (status==2) {
                $('#B_StockName').attr('readonly', true);
                $('#am_add').attr('disabled', true); 
                $('#B_Remark').attr('readonly', true);
            }
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
                param: {strWhere:'S_Kind = 1 '}
            }
            $("#B_StockName").select(dfop).on('change', function () {
                if (status==1) {
                    $('#Mes_BackSupplyDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#B_StockCode").val(entity.S_Code);
                    }
                });
            });
            //添加物料
            $("#am_add").on("click", function () {
                var stockCode = $("#B_StockCode").val();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsListIndexForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/Mes_BackSupply/BackGoodsList?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 750,
                    height: 600,
                    maxmin: true,
                    btn: ['关闭'],
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
                        label: '物料编码', name: 'B_GoodsCode', width: 100, align: 'center', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'B_GoodsName', width: 160, align: 'center', editType: 'label'
                    },
                     {
                         label: '单价(不含税)', name: 'B_Price', width: 80, align: 'center', editType: 'label'
                     },
                    {
                        label: '单位', name: 'B_Unit', width: 70, align: 'center', editType: 'label'
                    },
                    {
                        label: '数量', name: 'B_Qty', width: 80, align: 'center', statistics: true, editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.B_Qty.toString().replace('.', ''))) { //验证只能为数字
                                    row.B_Qty = 0;
                                }
                                if (row.B_Qty > row.Qty) {
                                    ayma.alert.error("数量不能大于库存");
                                    row.B_Qty = 0;
                                }
                            }
                        }
                    },
                     {
                         label: '库存', name: 'Qty', width: 80, align: 'center', editType: 'label',hidden:keyValue==""?false:true
                     }, {
                         label: "金额", name: "金额", width: 60, align: "left", formatter: function (value, row, dfop) {
                             if (row.M_Qty == "" || row.B_Qty == null || row.B_Qty == undefined) {
                                 return row.金额 = 0;
                             }
                             else {
                                 return row.金额 = row.B_Price * row.B_Qty;
                             }
                         }, statistics: true
                     },
                    {
                        label: '批次', name: 'B_Batch', width: 160, align: 'center', editType: 'label'
                    },
                    {
                        label: '备注', name: 'B_Remark', width: 200, align: 'left', editType: 'input'
                    },
                ],
                isAutoHeight: true,
                isEidt: status == 1 || status == "" ? true : false,
                reloadSelected: true,
                isMultiselect: status == 1 || status == "" ? true : false,
                footerrow: true,
                minheight: 400,
                inputCount: 2,
                isStatistics: true
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_BackSupply/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_BackSupplyDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_BackSupplyDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_BackSupplyDetail').jfGridGet('rowdatas');
        if (data.length == 0 || data[0].B_GoodsCode ==null) {
            ayma.alert.error('请添加物料');
            return false;
        }
        for (var i = 0; i < data.length; i++) {
            if (data[i]["B_Qty"] == "" || data[i]["B_Qty"] == "0") {
                ayma.alert.error("数量不能为空或不能为零");
                return false;
            }
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_BackSupplyHead"]').GetFormData());
        postData.strmes_BackSupplyDetailList = JSON.stringify($('#Mes_BackSupplyDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_BackSupply/SaveForm?keyValue=' + keyValue, postData, function (res) {
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
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].B_GoodsCode == row.i_goodscode & rows[k].B_Batch == row.i_batch) {
                        flagRow = false;
                    }
                }
                if (flagRow) {
                    rows.push(row);
                }
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    var flag = true;
                    //加个循环判断数组重复
                    for (var j = 0; j < rows.length; j++) {
                        if (rows[j].B_GoodsCode == data[i].i_goodscode && rows[j].B_Batch == data[i].i_batch) {
                            flag = false;
                        }
                    }
                    if (flag) {
                        rows.push(data[i]);
                    }

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
            if (rows[i]["B_GoodsCode"] == row["i_goodscode"] && rows[i]["B_Batch"] == row["i_batch"]) {
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
