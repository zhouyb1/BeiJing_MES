/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
 */
var acceptClick;
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');//上一级formId
var status = request('status');
var inputFocus;
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
                $('#M_StockName').css('background', '#f1efef');
                $('#M_StockName').attr('readonly', true);
                $('#M_OrderDate').attr('disabled', 'disabled');
                $('#M_OrderDate').attr('disabled', true);
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
                url: top.$.rootUrl + '/MesDev/Tools/GetOriginalStockList',
                // 访问数据接口参数
                param: {}
            };
            //绑定仓库
            $('#M_StockName').select(dfop).on('change', function () {
                if (status == "" || status == 1) {
                    $('#Mes_MaterInDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
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

            $("#M_SupplyCode").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetEffectSupplyList',
                // 访问数据接口参数
            });
            $('#M_SupplyCode').on('change', function () {
                if (status == "" || status == 1) {
                    $('#Mes_MaterInDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
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
                url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderListBy',
                // 访问数据接口参数
                param: { orderStartDate: getDay(-3000), orderEndDate: getDay(0) }
            });
            //单据状态
            $("#M_Status").DataItemSelect({ code: 'MaterInStatus' });
            //添加商品
            $("#am_add").on("click", function () {
                if ($("#M_StockCode").val()=="") {
                    ayma.alert.error("请选择仓库！");
                    return false;
                }
                if ($('#M_SupplyCode').selectGet()=="") {
                    ayma.alert.error("请选择供应商！");
                    return false;
                }
                var code = $('#M_SupplyCode').selectGet();
                ayma.httpAsync('get', top.$.rootUrl + '/MesDev/Tools/ByCodeGetSupplyEntity', { code: code }, function (res) {
                    if (res.S_EffectTime < ayma.getDate('yyyy-MM-dd 00:00:00',"",null)) {
                        ayma.alert.error("供应商资质过期！");
                        return false;
                    }
                    ayma.layerForm({
                        id: 'GoodsListIndexForm',
                        title: '添加物料',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/GoodsListIndex?formId=' + parentFormId + '&S_Code=' + $("#M_StockCode").val() + "&supplyCode=" + $("#M_SupplyCode").selectGet(),
                        width: 950,
                        height: 600,
                        maxmin: true,
                        callBack: function (id, index) {
                            return top[id].closeWindow();
                        }
                    });
                    return true;
                });
               
            });
            $('#Mes_MaterInDetail').jfGrid({
                headData: [
                     {
                         label: 'ID', name: 'ID', width: 160, align: 'left', editType: 'label', hidden: true
                     },

                    {
                        label: '物料编码', name: 'M_GoodsCode', width: 90, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'M_GoodsName', width: 110, align: 'left', editType: 'label'
                    },
                     {
                         label: "商品类型", name: "M_Kind", width: 80, align: "left",
                         formatterAsync: function (callback, value, row) {

                             ayma.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'GoodsType',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                         }
                     },
                    {
                        label: '单位', name: 'M_Unit', width: 60, align: 'left', editType: 'label'
                    },
                     {
                         label: '数量', name: 'M_Qty', width: 60, align: 'left', editType: 'input',
                         editOp: {
                             callback: function (rownum, row) {
                                 if (/\D/.test(row.M_Qty.toString().replace('.', ''))) { //验证只能为数字
                                     row.M_Qty = 0;
                                 }

                             }
                         }
                     },
                     {
                         label: "包装数量", name: "M_Qty2", width: 60, align: "left", editType: 'input',
                         editOp: {
                             callback: function (rownum, row) {
                                 if (row.M_Qty2 != undefined && !!row.M_Qty2) {
                                     if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.M_Qty2.toString().replace('.', ''))) {
                                         ayma.alert.error("包装数量必须是非负数.");
                                         row.M_Qty2 = 0;
                                     }
                                 }
                                 if (row.M_Qty2 > row.M_Qty) {
                                     ayma.alert.error("包装数量不能大于领料数量");
                                     row.M_Qty2 = 0;
                                 }
                             }
                         }
                     },
                     {
                         label: '价格', name: 'M_Price', width: 70, align: 'left', editType: 'label',
                         editOp: {
                             callback: function (rownum, row) {
                                 if (/\D/.test(row.M_Price.toString().replace('.', ''))) { //验证只能为数字
                                     row.M_Price = 0;
                                 }

                             }
                         }
                     },//最新维护的价格
                    {
                        label: '批次', name: 'M_Batch', width: 100, align: 'left', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.M_Batch.toString().replace('.', ''))) { //验证只能为数字
                                    row.M_Batch = 0;
                                }

                            }
                        }
                    },
                    { label: "含税价格", name: "M_TaxPrice", width: 60, align: "left" },
                    { label: "入库税率", name: "M_Tax", width: 60, align: "left" },
                    { label: "包装单位", name: "M_Unit2", width: 60, align: "left" },
                    { label: "包装规格", name: "M_UnitQty", width: 60, align: "left" },
                    { label: "仓库编码", name: "M_StockCode", width: 100, align: "left" },
                    { label: "仓库名称", name: "M_StockName", width: 100, align: "left" },
                    {
                        label: '备注', name: 'M_Remark', width: 130, align: 'left', editType: 'input'
                    }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 320,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 4
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
                        inputFocus();
                    }
                });
            } else {
                $("#M_Status").selectSet(1);//新增时默认为单据生成
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_MaterInDetail').jfGridSet('refreshdata', { rowdatas: data });
            inputFocus();
        }
    };
    function getDay(day) {
        var today = new Date();
        var targetday_milliseconds = today.getTime() + 1000 * 60 * 60 * 24 * day;
        today.setTime(targetday_milliseconds); //注意，这行是关键代码
        var tYear = today.getFullYear();
        var tMonth = today.getMonth();
        var tDate = today.getDate();
        tMonth = doHandleMonth(tMonth + 1);
        tDate = doHandleMonth(tDate);
        return tYear + "-" + tMonth + "-" + tDate;
    }
    function doHandleMonth(month) {
        var m = month;
        if (month.toString().length == 1) {
            m = "0" + month;
        }
        return m;
    }

    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_MaterInDetail').jfGridGet('rowdatas');
        if (data.length==0|| data[0].M_GoodsCode==null) {
            ayma.alert.error("请添加物料！");
            return false;
        }
        var postData = {};
        var obj = $('[data-table="Mes_MaterInHead"]').GetFormData();
        obj.M_SupplyName = $('#M_SupplyCode').selectGetText();
        postData.strEntity = JSON.stringify(obj);
        postData.strmes_MaterInDetailList = JSON.stringify($('#Mes_MaterInDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/MaterInBill/SaveForm?orderKind=0&keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_MaterInDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].M_GoodsCode == row.p_goodscode) {
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
                        if (rows[j].M_GoodsCode == data[i].p_goodscode) {
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
            return item["M_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_MaterInDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["M_GoodsCode"] == row["p_goodscode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_MaterInDetail').jfGridGet('rowdatas');
    }
    //input获取上下左右键操控焦点
    inputFocus = function () {
        $('#Mes_MaterInDetail').jfGridInputFocus(3);
    }
    page.init();
}
