﻿/* * 创建人：超级管理员
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
var tmp_d = new Map();
var stock;
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
            $('.fa-ellipsis-h').data("tip", "alert");
        },
        bind: function () {
            $("#O_StockName").select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            }).on('change', function() {
                //$("#Mes_OrgResDetail_h").jfGridSet('refreshdata', { rowdatas: [] });
                //$("#Mes_OrgResDetail_d").jfGridSet('refreshdata', { rowdatas: [] });
                var name = $(this).selectGet();
                $('.fa-ellipsis-h').data("code", name);
                $('.fa-ellipsis-h').data("tip", "alert");
                //绑定仓库编码
                ayma.httpAsyncGet(top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity?code=' + name, function (result) {
                    $('#O_StockCode').val(result.data.S_Code);
                    stock = result.data.S_Code;
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
            }).on('change', function() {
                //$("#Mes_OrgResDetail_h").jfGridSet('refreshdata', { rowdatas: [] });
                //$("#Mes_OrgResDetail_d").jfGridSet('refreshdata', { rowdatas: [] });
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
            }).on('change', function() {
                //$("#Mes_OrgResDetail_h").jfGridSet('refreshdata', { rowdatas: [] });
                //$("#Mes_OrgResDetail_d").jfGridSet('refreshdata', { rowdatas: [] });
                $('#O_TeamName').val($(this).selectGetText());
            });

            //添加物料
            //$('#am_add').on('click', function () {

            //    if ($('#O_ProCode').selectGet()=="") {
            //        ayma.alert.error('请先选择工序');
            //        return false;
            //    }
            //    if ($('#O_StockName').selectGet()=="") {
            //        ayma.alert.error('请先选择日耗库');
            //        return false;
            //    }
            //    ayma.layerForm({
            //        id: 'MaterListForm',
            //        title: '添加物料',
            //        url: top.$.rootUrl + '/MesDev/OrgResManager/GoodsListIndex?formId=' + parentFormId + '&stock=' + $('#O_StockCode').val() + '&proNo=' + $('#O_ProCode').selectGet(),
            //        width: 900,
            //        height: 700,
            //        maxmin: true,
            //        callback: function (id, index) {
            //            return top[id].closeWindow();
            //        }
            //    });
            //});
            $('#Mes_OrgResDetail_h').jfGrid({
                headData: [

                      {
                          label: "组装前物料",
                          name: "B",
                          width: 180,
                          align: "center",
                          children: [
                              {
                                  label: "物料名称", name: "O_GoodsName", width: 120, align: "center", editType: 'select',
                                  editOp: {
                                      width: 580,
                                      height: 500,
                                      colData: [
                                          { label: "物料名称", name: "o_goodsname", width: 100, align: "left" },
                                          { label: "物料编码", name: "o_goodscode", width: 80, align: "center" },
                                          { label: "批次", name: "o_batch", width: 90, align: "center" },
                                          { label: "转换后的物料",name: "o_secgoodsname", width: 90, align: "left" },
                                          { label: "转换后的物料", name: "o_secgoodscode", width: 90, align: "left", hidden: true },
                                          { label: "单价", name: "o_price", width: 80, align: "center" },
                                          { label: "单位", name: "o_unit", width: 60, align: "center" },
                                          { label: "库存", name: "o_qty", width: 100, align: "left" },
                                      ],
                                      url: top.$.rootUrl + '/MesDev/OrgResManager/GetGoodsList',
                                      isPage: true,
                                      isMultiselect:false,
                                      callback: function (selectdata, rownum, row, isChecked) {
                                          row.O_GoodsCode = selectdata.o_goodscode;
                                          row.O_GoodsName = selectdata.o_goodsname;
                                          row.StockQty = selectdata.o_qty;
                                          row.O_Unit = selectdata.o_unit;
                                          //row.O_Batch = selectdata.o_batch;整合批次
                                          row.O_Price = selectdata.o_price;
                                          row.O_SecGoodsCode = selectdata.o_secgoodscode;

                                          row.O_SecUnit = selectdata.o_unit;
                                          row.O_SecGoodsName = selectdata.o_secgoodsname;
                                          //带出转换后的物料

                                          top.FormRefreshGirdDataDetails(row);
                                      
                                      }
                                  }
                              },
                              { label: "物料编码", name: "O_GoodsCode", width: 90, align: "center" },
                              { label: "库存", name: "StockQty", width: 80, align: "center", hidden: keyValue==""?false:true },
                              {
                                  label: "数量", name: "O_Qty", width: 80, align: "center", editType: 'input', 
                                  editOp: {
                                      callback: function (rownum, row) {
                                          if (/\D/.test(row.O_Qty.toString().replace('.', ''))) { //验证只能为数字
                                              row.O_Qty = 0;
                                          }
                                          if (row.O_Qty > row.StockQty) {
                                              ayma.alert.error('不能大于库存数量');
                                              row.O_Qty = 0;
                                          }
                                      }
                                  }
                              },
                              { label: "单价", name: "O_Price", width: 80, align: "center" },
                              { label: "单位", name: "O_Unit", width: 60, align: "center" },
                              //{ label: "批次", name: "O_Batch", width: 80, align: "center", editType: 'input' },
                              { label: "转换后的数量", name: "O_SecQty", width: 90, align: "center", hidden: true },
                              { label: "转换后单价", name: "O_SecPrice", width: 90, align: "center", hidden: true },
                              { label: "转换后单位", name: "O_SecUnit", width: 90, align: "center", hidden: true },
                              { label: "转换后的批次", name: "O_SecBatch", width: 90, align: "center", hidden: true },
                              { label: "转换后的物料编码", name: "O_SecGoodsCode", width: 90, align: "center", hidden: true },
                              { label: "转换后的物料", name: "O_SecGoodsName", width: 90, align: "center", hidden: true },
                          ]
                      },
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 220,
                isEidt: true,
                height: 220,
                rowCount: 2,
                inputCount: 2,
                isMultiselect: true,

                //isStatistics: true,

            });

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
                    {
                        label: "数量",
                        name: "O_SecQty",
                        width: 80,
                        align: "center",
                        editType: 'input',
                        editOp: {
                            callback: function(rownum, row) {
                                if (/\D/.test(row.O_SecQty.toString().replace('.', ''))) { //验证只能为数字
                                    row.O_SecQty = 0;
                                } else {
                                    var rowheadataList = $("#Mes_OrgResDetail_h").jfGridGet('rowdatas');
                                    var amount = 0;


                                    for (var i = 0, j = rowheadataList.length; i < j; i++) {
                                        if (rowheadataList[i].O_SecGoodsCode == row.O_SecGoodsCode) {
                                            amount += rowheadataList[i].O_Price * rowheadataList[i].O_Qty;
                                            row.O_SecPrice = (amount / row.O_SecQty).toFixed(6);

                                            rowheadataList[i].O_SecQty = row.O_SecQty;
                                            rowheadataList[i].O_SecUnit = row.O_SecUnit;
                                            rowheadataList[i].O_SecBatch = ayma.formatDate(new Date(), "yyyy-MM-dd").toString().replace(/-/g, "");
                                            rowheadataList[i].O_SecGoodsCode = row.O_SecGoodsCode;
                                            rowheadataList[i].O_SecGoodsName = row.O_SecGoodsName;
                                        }
                                    }
                                    for (var k = 0, m = rowheadataList.length; k < m; k++) {
                                        if (rowheadataList[k].O_SecGoodsCode == row.O_SecGoodsCode) {
                                            rowheadataList[k].O_SecPrice = row.O_SecPrice;
                                        }
                                    }
                                    $('#Mes_OrgResDetail_h').jfGridSet('refreshdata', { rowdatas: rowheadataList });
                                }
                            }
                        }
                    },
                    { label: "单价", name: "O_SecPrice", width: 80, align: "center" },
                    { label: "单位", name: "O_SecUnit", width: 80, align: "center" },
                    {
                        label: "批次",
                        name: "O_SecBatch",
                        width: 80,
                        align: "center",
                        hidden:true,
                                formatter: function (cellvalue, row) {
                                   return  row.O_SecBatch = ayma.formatDate(new Date(), "yyyy-MM-dd").toString().replace(/-/g, "");
                                }
                            }
                        ]
                    }
                ],
                isAutoHeight: false,
                footerrow: true,
                rowCount:2,
                minheight: 220,
                isEidt: true,
                height: 220,
                inputCount: 1,
                isMultiselect: true,

                //isStatistics: true,
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
        search: function(data1,data2) {
            data1 = data1 || {};
            data2 = data2 || {};
            $('#Mes_OrgResDetail_h').jfGridSet('refreshdata', { rowdatas: data1 });
            $('#Mes_OrgResDetail_d').jfGridSet('refreshdata', { rowdatas: data2 });
        }
    };

    // 保存数据
    acceptClick = function(callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_OrgResDetail_h').jfGridGet('rowdatas');
        data = $.grep(data, function(item) {
            return item.O_GoodsCode != undefined;
        });

        if (data[0].O_GoodsCode == undefined || data[0].O_GoodsCode == "") {
            ayma.alert.error('请添加物料');
            return false;
        }

        //var num = 0;
        for (var i = 0, j = data.length; i < j; i++)
        {
            if (data[i].O_Qty == undefined || data[i].O_Qty == 0) {
                ayma.alert.error('请录入组装前的数量！');
                return false;

            }
            if (data[i].O_SecQty == undefined || data[i].O_SecQty == 0) {
                ayma.alert.error('请录入组装后的数量！');
                return false;
            }
            //data[i].O_Index = num;
            //num++;
        };
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
        return $('#Mes_OrgResDetail_h').jfGridGet('rowdatas');
    }

    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_OrgResDetail_h').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row,1);
                var rowFlag = true;
                rows.push(row);
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

        //组装后gird 
        var row_d = $('#Mes_OrgResDetail_d').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp_d.get(row)) {
                tmp_d.set(row, 1);
                var rowFlag = true;
                //加个循环判断数组重复
                for (var k = 0; k < row_d.length; k++) {
                    if (row_d[k].O_SecGoodsCode == row.O_SecGoodsCode && row_d[k].O_SecBatch == row.O_SecBatch) {
                        rowFlag = false;
                    }
                }
                if (rowFlag) {
                    row_d.push(row);
                }
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp_d.get(data[i])) {
                    tmp_d.set(data[i], 1);
                    var isExist = true;
                    for (var j = 0; j < row_d.length; j++) {
                        if (data[i].O_SecGoodsCode == row_d[j].O_SecGoodsCode && data[i].O_SecBatch == row_d[j].O_SecBatch) {
                            isExist = false;
                        }
                    }
                    if (isExist) {
                        row_d.push(data[i]);
                    }
                }
            }
        }

        //数组过滤
        var filterarray1 = $.grep(rows, function (item) {
            return item["O_GoodsCode"] != undefined;
        });
        var filterarray2 = $.grep(row_d, function (item) {
            return item["O_SecGoodsCode"] != undefined;
        });
        page.search(filterarray1, filterarray2);
    };

    //表格商品删除
    RemoveGridData = function (row) {

        ////组装前
        //row['O_GoodsCode'] = row['w_goodscode'];
        //row['O_GoodsName'] = row['w_goodsname'];
        //row['O_Unit'] = row['w_unit'];
        //row["O_Qty"] = quantity;
        //row["O_Price"] = row['w_price'];
        //row['O_Batch'] = row['w_batch'];
        //row["StockQty"] = row["w_qty"];

        ////组装后
        //row['O_SecGoodsCode'] = row['c_seccode'];
        //row['O_SecGoodsName'] = row['c_secname'];
        //row['O_SecUnit'] = row['w_unit'];
        //row["O_SecQty"] = quantity;
        //row['O_SecBatch'] = ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");
        //row["ID"] = row["id"];

        var rows = $('#Mes_OrgResDetail_h').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["O_GoodsCode"] == row["O_GoodsCode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                //page.search(rows);
            }
        }

        var row_d= $('#Mes_OrgResDetail_d').jfGridGet('rowdatas');
        for (var i = 0; i < row_d.length; i++) {
            if (row_d[i]["O_SecGoodsCode"] == row["O_GoodsCode"]) {
                row_d.splice(i, 1);
                tmp_d.delete(row);
                //page.search(row_d);
            }
        }

        //page.search(rows,row_d);
    };

    //获取处理前物料
    top.GetGoodsListHead = function () {
        var rows = $('#Mes_OrgResDetail_h').jfGridGet('rowdatas');
        return rows;
    };

    //获取处理后物料
    top.GetGoodsListDetails = function () {
        var rows = $('#Mes_OrgResDetail_d').jfGridGet('rowdatas');
        return rows;
    };

    //刷新处理前物料列表
    top.FormRefreshGirdDataHead = function (data) {

        var rows = $('#Mes_OrgResDetail_h').jfGridGet('rowdatas');
        rows.push(data);

        var filterarray = $.grep(rows, function (item) {
            return item["O_GoodsCode"] != undefined;
        });

        $('#Mes_OrgResDetail_h').jfGridSet('refreshdata', { rowdatas: filterarray });
    };

    //刷新处理后物料列表
    top.FormRefreshGirdDataDetails = function (data) {

        var rows = $('#Mes_OrgResDetail_d').jfGridGet('rowdatas');
        //循环判断有无同一产出物的物料
        var isExist = false;
        for (var i = 0,j=rows.length; i < j; i++) {
            if (data.O_SecGoodsCode==rows[i].O_SecGoodsCode) {
                isExist = true;
            }
        }
        if (!isExist) {
            rows.push(data);
        }
        //rows.push(data);

        var filterarray = $.grep(rows, function (item) {
            return item["O_SecGoodsCode"] != undefined;
        });

        $('#Mes_OrgResDetail_d').jfGridSet('refreshdata', { rowdatas: filterarray });
    };


    //移除处理前物料列表
    top.FormRemoveGirdDataHead = function (data) {


        var rows = $('#Mes_OrgResDetail_h').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i].O_GoodsCode == data.O_GoodsCode && rows[i].O_Batch == data.O_Batch&& rows[i].O_SecGoodsCode==data.O_SecGoodsCode) {
                rows.splice(i, 1);
                break;
            }
        }

        var filterarray = $.grep(rows, function (item) {
            return item["O_GoodsCode"] != undefined;
        });

        $('#Mes_OrgResDetail_h').jfGridSet('refreshdata', { rowdatas: filterarray });

    };

    //移除处理后物料列表
    top.FormRemoveGirdDataDetails = function (data) {

        var rows = $('#Mes_OrgResDetail_d').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i].O_SecGoodsCode == data.O_SecGoodsCode && rows[i].O_SecBatch == data.O_SecBatch&& rows[i].O_Batch==data.O_Batch) {
                rows.splice(i, 1);
                break;
            }
        }

        var filterarray = $.grep(rows, function (item) {
            return item["O_SecGoodsCode"] != undefined;
        });

        $('#Mes_OrgResDetail_d').jfGridSet('refreshdata', { rowdatas: filterarray });
    };

    page.init();
}
