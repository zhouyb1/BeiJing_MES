/* * 创建人：超级管理员
 * 日  期：2018-08-16 20:02
 * 描  述：商品列表
 */
var refreshGirdData;
//上级元素的刷新表格方法
var parentRefreshGirdData;
//上级元素的删除表格方法;
var parentRemoveGridData;
//上级元素的id
var parentFormId = request('formId');
var newArray = [];
//仓库的编码
//var S_Code = request('S_Code');
//供应商编码
var supplyCode = request('supplyCode');
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
//批次时间
var batch = new Date();
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            //获取父级iframe中的刷新商品列表方法
            parentRefreshGirdData = $(top[parentFormId]).context.firstChild.contentWindow.refreshGirdData;
            parentRemoveGridData = $(top[parentFormId]).context.firstChild.contentWindow.RemoveGridData;
        },
        bind: function () {
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 200, 350);
            $('#txt_Keyword').bind('keydown', function (event) {
                if (event.keyCode == "13") {
                    $('#am_btn_querySearch').click();
                }
            });
            $('#am_btn_querySearch').on('click', function () {
                page.search();
            });
            //输入关键字搜索
            //$("#txt_Keyword").on('keyup', function () {
            //    page.search();
            //});
            //商品类型
            $("#GoodsType").DataItemSelect({ code: "GoodsType" });
            //数量验证
            $("#quantity").on('keyup', function () {
                var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                if (quantity < 0) {
                    ayma.alert.error("请别输入负数！");
                    $("#quantity").val("");
                    return;
                }
                if (quantity == "0") {
                    return;
                } else {
                    var newQty = parseInt(quantity);
                    ayma.loading(true);
                    for (var i = 0; i < newArray.length; i++) {
                        if (newQty > newArray[i]["i_qty"]) {
                            ayma.alert.error("其中有输入数量大于库存数量的库存，不能一键设置");
                            $("#quantity").val("");
                            ayma.loading(false);
                            return;
                        }
                    }
                    ayma.loading(false);
                }
            });
            //全选
            $("#jfgrid_all_cb_girdtable").on('click', function () {
                var array = [];
                //获取一键数量
                var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                for (var i = 0; i < newArray.length; i++) {
                    //copy需要更改的地方
                    newArray[i]["M_Unit2"] = newArray[i]['g_unit2'];
                    newArray[i]["M_UnitQty"] = newArray[i]['g_unitqty'];
                    newArray[i]["M_StockCode"] = newArray[i]['c_stockcode'];
                    newArray[i]["M_StockName"] = newArray[i]['c_stockName'];
                    newArray[i]["M_TaxPrice"] = newArray[i]['p_taxprice'];
                    newArray[i]['M_GoodsCode'] = newArray[i]['p_goodscode'];
                    newArray[i]['M_GoodsName'] = newArray[i]['p_goodsname'];
                    newArray[i]['M_Kind'] = newArray[i]['g_kind'];
                    newArray[i]['M_Unit'] = newArray[i]['g_unit'];
                    newArray[i]['M_Price'] = newArray[i]['p_inprice'];
                    newArray[i]['M_Tax'] = newArray[i]['p_itax'];
                    newArray[i]['M_Batch'] = ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");
                    newArray[i]["ID"] = newArray[i]['id'];
                    array.push(newArray[i]);
                }
                parentRefreshGirdData(array);

            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetGoodsList',
                headData: [
                    { label: "主键", name: "id", width: 130, align: "left",hidden:true},
                    { label: "物料编码", name: "p_goodscode", width: 80, align: "left" },
                    { label: "物料名称", name: "p_goodsname", width: 80, align: "left" },
                    { label: "供应商编码", name: "p_supplycode", width: 80, align: "left" },
                    { label: "供应商名称", name: "p_supplyname", width: 80, align: "left" },
                     {
                         label: "商品类型", name: "g_kind", width: 80, align: "left",
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
                    { label: "保质时间", name: "g_period", width: 80, align: "left" },
                    {
                        label: "开始时间", name: "p_startdate", width: 80, align: "left",
                        formatter: function (value, row, dfop) {
                            if (row.p_startdate != null) {
                                return row.p_startdate.substr(0, 10);
                            }
                        }
                    },
                    {
                        label: "到期时间", name: "p_enddate", width: 80, align: "left",
                        formatter: function (value, row, dfop) {
                            if (row.p_enddate != null) {
                                return row.p_enddate.substr(0, 10);
                            }
                        }
                    },
                   { label: "不含税价格", name: "p_inprice", width: 80, align: "left" },
                   { label: "含税价格", name: "p_taxprice", width: 100, align: "left" },
                   { label: "包装单位", name: "g_unit2", width: 60, align: "left" },
                   { label: "包装规格", name: "g_unitqty", width: 60, align: "left" },
                    //{ label: "包装数量", name: "G_Qty2", width: 60, align: "left" },
                    { label: "原仓库编码", name: "g_stockcode", width: 100, align: "left" },
                    { label: "原仓库名称", name: "g_stockname", width: 100, align: "left" },
                    { label: "入库税率", name: "p_itax", width: 80, align: "left" },
                    { label: "单位", name: "g_unit", width: 80, align: "left" }
                ],
                mainId: 'ID',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'p_goodscode',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                    //    return;
                    //}
                    var now = new Date();
                    var year = now.getFullYear(); //得到年份
                    var month = now.getMonth() + 1;//得到月份
                    var date = now.getDate();//得到日期
                    var time = year + '-' + month + '-' + date
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {
                        if (row['p_inprice'] == 0 || row['p_inprice']==null)
                        {
                            isChecked.attr('checked', false);
                            ayma.alert.error('物料【' + row['p_goodsname'] + '】不含税价格为0请及时维护价格！');
                        }
                        if (row['p_taxprice'] == 0 || row['p_taxprice']==null) {
                            isChecked.attr('checked', false);
                            ayma.alert.error('物料【' + row['p_goodsname'] + '】含税价格为0请及时维护价格！');
                        }
                        if (row['p_enddate'] == null || row['p_enddate'].substr(0, 10) < time) {
                            isChecked.attr('checked', false);
                            ayma.alert.error('物料【' + row['p_goodsname'] + '】有效期已到请及时维护！');
                        }
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方
                        row["M_Unit2"] = row['g_unit2'];
                        row["M_UnitQty"] = row['g_unitqty'];
                        row["M_StockCode"] = row['g_stockcode'];
                        row["M_StockName"] = row['g_stockname'];
                        row["M_TaxPrice"] = row['p_taxprice'];
                        row['M_GoodsCode'] = row['p_goodscode'];
                        row['M_GoodsName'] = row['p_goodsname'];
                        row['M_SupplyCode'] = row['p_supplycode'];
                        row['M_SupplyName'] = row['p_supplyname'];
                        row['M_Tax'] = row['p_itax'];
                        row['M_Unit'] = row['g_unit'];
                        row['M_Kind'] = row['g_kind'];
                        row['M_Price'] = row['p_inprice'];
                        row["M_Qty"] = quantity;
                        row['M_Batch'] = ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");
                            //batch.getFullYear().toString() + (batch.getMonth() + 1).toString() + batch.getDate().toString();
                        row["ID"] = row['id'];
                        parentRefreshGirdData([], row);
                    }
                    if (!isChecked.is(":checked")) {
                        parentRemoveGridData(row);
                    }
                },
                onRenderComplete: function (rows) {
                    newArray = rows;
                    var rowslist = top.NewGirdData();
                    if (JSON.stringify(rowslist) !== '[]') {
                        var rowlistlenght = rowslist[0]["id"] == undefined ? 0 : rowslist.length;
                        for (var i = 0; i < rows.length; i++) {
                            for (var j = 0; j < rowslist.length; j++) {
                                if (rows[i]['p_goodscode'] == rowslist[j]['M_GoodsCode']) {
                                    $("[rownum='rownum_girdtable_" + i + "']").eq(2).children().attr("checked", "checked");
                                    break;
                                }
                            }
                        }
                    }

                }
            });
            page.search();
        },
        search: function (param) {
            queryJson = param || {};
            queryJson.G_SupplyCode = supplyCode;
            param = $("#txt_Keyword").val();
           
            $('#girdtable').jfGridSet('reload', { param: { keyword: param, queryJson: JSON.stringify(queryJson) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    closeWindow = function () {
        top.layer.close(parent.layer.getFrameIndex(window.name));
    }
    page.init();
}
