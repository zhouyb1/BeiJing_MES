/* * 创建人：Yabo,Zhou
 * 描  述：组装物料列表
 */
var workShop = request('workShop');
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

//var orderNo = request('orderNo');
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
                   {
                       label: "扫描后",
                       name: "B",
                       width: 160,
                       align: "center",
                       children: [
                           { label: "物料编码", name: "w_goodscode", width: 130, align: "left", },
                           { label: "物料名称", name: "w_goodsname", width: 130, align: "left" },
                           { label: "单价", name: "w_price", width: 60, align: "left" },
                           { label: "单位", name: "w_unit", width: 60, align: "left" },
                           {
                               label: "数量", name: "w_qty", width: 60, align: "left", editType: 'input',
                           },
                           { label: "批次", name: "w_batch", width: 80, align: "left", editType: 'input' }
                       ]
                   },
                    {
                        label: "称重后",
                        name: "B",
                        width: 160,
                        align: "center",
                        children: [
                           { label: "物料编码", name: "w_secgoodscode", width: 130, align: "left", },
                           { label: "物料名称", name: "w_secgoodsname", width: 130, align: "left" },
                           {
                               label: "数量", name: "w_secqty", width: 60, align: "left", editType: 'input',
                           },
                           { label: "批次", name: "w_secbatch", width: 80, align: "left", editType: 'input' }
                        ]
                    }
                ],
                mainId: 'id',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'W_Batch',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                    //    return;
                    //}
                    var allCheck = $("#jfgrid_all_cb_girdtable");
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {
                        if (row['G_Qty'] <= 0) {
                            isChecked.attr('checked', false);  //移除 checked 状态
                            ayma.alert.error('库存为负数');
                        }
                        else {
                        if (!allCheck.is(":checked")) {
                            //提示用户选择最早批次
                            var list = $('#girdtable').jfGridGet('rowdatas');
                            var data = [];
                            data = list.filter(function (item) {
                                if (item.w_qty > 0) {
                                    return item.w_goodscode == row['w_goodscode'];
                                }
                            })
                            var min = data[0].w_batch;
                            var len = data.length;
                            for (var i = 1; i < len; i++) {
                                if (data[i].w_batch < min) {
                                    min = data[i].w_batch;
                                }
                            }
                            for (var i = 0; i < list.length; i++) {
                                if (list[i].w_batch == min && list[i].w_goodscode == data[0].w_goodscode)
                                {
                                    var minrowid = i;
                                }
                            }
                            var minisChecked = $("[rownum='rownum_girdtable_" + minrowid + "']").find("input[role='checkbox']");
                            if (!minisChecked.is(":checked")) {
                                if (row['G_Batch'] > min) {
                                    ayma.alert.error('请优先使用最早批次为' + min + '的【' + row['w_goodscode'] + '】');
                                }
                            }
                        }
                    }
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方 
                        //称重前
                        row['O_GoodsCode'] = row['w_goodscode'];
                        row['O_GoodsName'] = row['w_goodsname'];
                        row['O_Unit'] = row['w_unit'];
                        row["O_Qty"] = quantity;
                        row["O_Price"] = row['w_price'];
                        row['O_Batch'] = row['w_batch'];
                        //称重后
                        row['O_SecGoodsCode'] = row['w_secgoodscode'];
                        row['O_SecGoodsName'] = row['w_secgoodsname'];
                        row['O_SecUnit'] = row['w_secunit'];
                        row["O_SecQty"] = quantity;
                        row['O_SecBatch'] = row['w_secbatch'];
                        row["ID"] = row["id"];
                        //组装前注释
                        
                        //$.ajax({
                        //    type:"get",
                        //    url: '/MesDev/Tools/GetMesConverEntity',
                        //    async: false,
                        //    data: { goodsCode: row['G_GoodsCode'] },
                        //    dataType:'JSON',
                        //    success: function(res) {
                        //        var result = res.data;
                        //        if (result != null) {
                        //            row['O_GoodsCode'] = result.G_Code; 
                        //            row['O_GoodsName'] = result.G_Name;
                        //            row['O_Unit'] = result.G_Unit;
                        //            row["O_Qty"] = quantity;
                        //            row["O_Price"] = result.G_Price;
                        //            row['O_Batch'] = result.G_Batch || ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");;
                        //        } else {
                        //            ayma.alert.error('请选择存在的物料关系');
                        //        }
                        //    }
                        //});
                        
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
                                if (rows[i]['w_goodscode'] == rowslist[j]['O_GoodsCode']) {
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
            param = $("#txt_Keyword").val();
            queryJson.workShop = workShop;
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
