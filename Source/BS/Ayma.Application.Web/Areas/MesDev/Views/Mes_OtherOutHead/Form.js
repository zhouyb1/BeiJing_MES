/* * 创建人：超级管理员
 * 日  期：2019-11-08 13:40
 * 描  述：其它出库单
 */
var acceptClick;
var refreshGirdData;
var RemoveGridData;
var tmp = new Map();
var parentFormId = request('formId');
var keyValue = request('keyValue');
var status = request('status');
var state = request('state');
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
            if (status == 2) {
                $('#O_StockName').attr('readonly', true);
                $("#O_DepartName").attr('readonly', true);
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
                url: top.$.rootUrl + '/MesDev/Tools/GetOtherStockList',
                // 访问数据接口参数
                param: {}
            };
            //绑定仓库
            $('#O_StockName').select(dfop).on('change', function () {
                if (status == 1) {
                    $('#Mes_OtherOutDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#O_StockCode").val(entity.S_Code);
                    }
                });
            });
            //绑定部门
            var depart = {
                type: 'default',
                value: 'F_FullName',
                text: 'F_FullName',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetDepartmentList',
                // 访问数据接口参数
                param: {}
            };
            //绑定部门
            $('#O_DepartName').select(depart).on('change', function () {
                if (status == 1) {
                    $('#Mes_OtherOutDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetDepartmentEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#O_DepartCode").val(entity.F_EnCode);
                    }
                });
            });
            //添加商品
            $("#am_add").on("click", function () {
                var stockCode = $('#O_StockCode').val();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsListIndexForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/Mes_OtherOutHead/GoodsListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 750,
                    height: 600,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            //单据状态
            $("#O_Status").DataItemSelect({ code: 'MaterInStatus' });
            $('#Mes_OtherOutDetail').jfGrid({
                headData: [
                    {
                        label: '物料编码', name: 'O_GoodsCode', width: 130, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'O_GoodsName', width: 130, align: 'left', editType: 'label'
                    },
                    {
                        label: '价格', name: 'O_Price', width: 60, align: 'left', editType: 'label',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.P_Price.toString().replace('.', ''))) { //验证只能为数字
                                    row.P_Price = 0;
                                }

                            }
                        }
                    },
                    {
                        label: '单位', name: 'O_Unit', width: 60, align: 'left', editType: 'label'
                    },
                    {
                        label: '数量', name: 'O_Qty', width: 60, align: 'left', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                //if (/\D/.test(row.P_Qty.toString().replace('.', ''))) { //验证只能为数字
                                //    row.P_Qty = 0;
                                //}
                                if (row.O_Qty != undefined && !!row.O_Qty) {
                                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.O_Qty.toString().replace('.', ''))) {
                                        ayma.alert.error("数量必须是非负数.");
                                        row.O_Qty = 0;
                                    }
                                }
                                if (row.O_Qty > row.I_Qty) {
                                    ayma.alert.error("数量不能大于库存数量");
                                    row.O_Qty = 0;
                                }
                                row.O_Qty2 = (row.O_Qty / row.O_UnitQty).toFixed(6) / 1;
                            }
                        }
                    },
             {
               label: '包装数量', name: 'O_Qty2', width: 100, align: 'left', editType: 'input',
               editOp: {
                   callback: function (rownum, row) {
                       //if (/\D/.test(row.P_Qty.toString().replace('.', ''))) { //验证只能为数字
                       //    row.P_Qty = 0;
                       //}
                       if (row.O_Qty2 != undefined && !!row.O_Qty2) {
                           if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.O_Qty2.toString().replace('.', ''))) {
                               ayma.alert.error("包装数量必须是非负数.");
                               row.O_Qty2 = 0;
                           }
                       }
                       row.O_Qty = (row.O_Qty2 * row.O_UnitQty).toFixed(6) / 1;
                       if (row.O_Qty > row.I_Qty) {
                           ayma.alert.error("数量不能大于库存数量");
                           row.O_Qty2 = 0;
                           row.O_Qty = 0;
                       }
                   }
               }
             },
                      {
                          label: '包装规格', name: 'O_UnitQty', width: 100, align: 'left', editType: 'select', editOp: {
                              width: 400,
                              height: 400,
                              colData: [
                                 { label: '物料名称', name: 'S_GoodsName', width: 100, align: 'left' },
                                 { label: '包装数', name: 'S_UnitQty', width: 100, align: 'left', },
                              ],
                              url: top.$.rootUrl + '/MesDev/Tools/ByGoodsCodeGetUnit',
                              param: { code: "1" },
                              callback: function (selectdata, rownum, row) {
                                  if (row.O_GoodsName == selectdata.S_GoodsName) {
                                      row.O_UnitQty = selectdata.S_UnitQty;
                                      ayma.alert.success("物料【" + row.O_GoodsName + "】的包装规格更改成功为【" + row.O_UnitQty + "】！");
                                  } else {
                                      ayma.alert.error("您需要更改包装规格的物料是【" + row.O_GoodsName + "】物料名称不符,请重新选择！");
                                      ayma.layer.error("")
                                  }
                              }
                          }
                      },
                     {
                         label: '包装单位', name: 'O_Unit2', width: 100, align: 'left', editType: 'label'
                     },
                    { label: '库存', name: 'I_Qty', width: 100, align: 'left', hidden: keyValue == "" ? false : true },
                    {
                        label: '批次', name: 'O_Batch', width: 100, align: 'left', editType: 'label'
                    },
                    {
                        label: '备注', name: 'O_Remark', width: 100, align: 'left', editType: 'input'
                    },
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 350,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_OtherOutHead/GetFormData?keyValue=' + keyValue + '&state=' + state, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_OtherOutDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            } else {
                $("#O_Status").selectSet(1);//新增时默认为单据生成
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_OtherOutDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_OtherOutDetail').jfGridGet('rowdatas');
        if (data.length == 0 || data[0].O_GoodsCode == null) {
            ayma.alert.error('请添加出库物料');
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_OtherOutHead"]').GetFormData());
        postData.strmes_OtherOutDetailList = JSON.stringify(data);
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_OtherOutHead/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_OtherOutDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].O_GoodsCode == row.I_GoodsCode && rows[k].O_Batch == row.I_Batch) {
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
                        if (rows[j].O_GoodsCode == data[i].I_GoodsCode && rows[j].O_Batch == data[i].I_Batch) {
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
            return item["O_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_OtherOutDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["O_GoodsCode"] == row["I_GoodsCode"] && rows[i]["O_Batch"] == row["I_Batch"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_OtherOutDetail').jfGridGet('rowdatas');
    }
    page.init();
}
