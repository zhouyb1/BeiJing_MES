/* * 创建人：Yabo,Zhou
 * 描  述：报废物料列表
 */
//仓库编码
var stockCode = request('stockCode');
var refreshGirdData;
//上级元素的刷新表格方法
var parentRefreshGirdData;
//上级元素的删除表格方法;
var parentRemoveGridData;
//上级元素的id
var parentFormId = request('formId');
var newArray = [];
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
//批次
var batch = new Date();
//订单
var orderNo = request('orderNo');
//仓库编码
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

            //输入关键字搜索
            $("#txt_Keyword").on('keydown', function (event) {
                if (event.keyCode == "13") {
                    $('#am_btn_querySearch').click();
                }
            });
            //查询
            $('#am_btn_querySearch').on('click', function () {
                page.search();
            });

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
            //$("#jfgrid_all_cb_girdtable").on('click', function () {
            //    var array = [];
            //    //获取一键数量
            //    var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
            //    for (var i = 0; i < newArray.length; i++) {
            //        //copy需要更改的地方
            //        newArray[i]['O_GoodsCode'] = newArray[i]['G_GoodsCode'];
            //        newArray[i]['O_GoodsName'] = newArray[i]['G_GoodsName'];
            //        newArray[i]['O_Unit'] = newArray[i]['G_Unit'];
            //        newArray[i]["O_Qty"] = quantity;
            //        newArray[i]['O_Batch'] = newArray[i]["G_Batch"];
            //        newArray[i]["ID"] = newArray[i]["G_ID"];
            //        array.push(newArray[i]);
            //    }
            //    parentRefreshGirdData(array);

            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/OrgResManager/GetGoodsList',
                headData: [
                    { label: "物料编码", name: "G_GoodsCode", width: 130, align: "left", },
                    { label: "物料名称", name: "G_GoodsName", width: 130, align: "left" },
                    { label: "单价", name: "G_Price", width: 130, align: "left" },
                    { label: "单位", name: "G_Unit", width: 60, align: "left" },
                    { label: "数量", name: "G_Qty", width: 60, align: "left" },
                    { label: "批次", name: "G_Batch", width: 80, align: "left" }
                ],
                mainId: 'G_ID',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'G_GoodsCode,G_Batch',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                    //    return;
                    //}
                    var allCheck = $("#jfgrid_all_cb_girdtable");
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {
                        if (!allCheck.is(":checked")) {
                            //提示用户选择最早批次
                            var list = $('#girdtable').jfGridGet('rowdatas');
                            var data = [];
                            data = list.filter(function (item) {
                                return item.G_GoodsCode == row['G_GoodsCode']
                            })
                            var min = data[0].G_Batch;
                            var len = data.length;
                            for (var i = 1; i < len; i++) {
                                if (data[i].G_Batch < min) {
                                    min = data[i].G_Batch;
                                }
                            }
                            if (row['G_Batch'] > min) {
                                ayma.alert.error('请优先使用最早批次为' + min + '的【' + row['G_GoodsName'] + '】');
                            }
                        }
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方

                        //产出物料
                        row['O_SecGoodsCode'] = row['G_GoodsCode'];
                        row['O_SecGoodsName'] = row['G_GoodsName'];
                        row['O_SecUnit'] = row['G_Unit'];
                        row["O_SecQty"] = quantity;
                        row['O_SecBatch'] = row['G_Batch'];
                        row["O_SecPrice"] = row["G_Price"];
                        row["ID"] = row["G_ID"];
                        //组装前

                        $.ajax({
                            type:"get",
                            url: '/MesDev/Tools/GetMesConverEntity',
                            async: false,
                            data: { goodsCode: row['G_GoodsCode'] },
                            dataType:'JSON',
                            success: function(res) {
                                var result = res.data;
                                if (result != null) {
                                    row['O_GoodsCode'] = result.G_Code; 
                                    row['O_GoodsName'] = result.G_Name;
                                    row['O_Unit'] = result.G_Unit;
                                    row["O_Qty"] = quantity;
                                    row["O_Price"] = result.G_Price;
                                    row['O_Batch'] = result.G_Batch || ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");;
                                } else {
                                    ayma.alert.error('请选择存在的物料关系');
                                }
                            }
                        });
                        
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
                        var rowlistlenght = rowslist[0]["ID"] == undefined ? 0 : rowslist.length;
                        for (var i = 0; i < rows.length; i++) {
                            for (var j = 0; j < rowlistlenght; j++) {
                                if (rows[i]['G_GoodsCode'] == rowslist[j]['O_GoodsCode']) {
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
            queryJson.orderNo = orderNo;
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
