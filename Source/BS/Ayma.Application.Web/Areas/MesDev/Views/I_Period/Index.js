/* * 创建人：超级管理员
 * 日  期：2019-09-26 10:54
 * 描  述：保质期查询
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var I_GoodsName = $('#txt_Keyword').val();
                page.search({ I_GoodsName: I_GoodsName });
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/I_Period/GetPageList',
                headData: [
                        { label: '仓库编码', name: 'I_StockCode', width: 150, align: "left" },
                        { label: '仓库名称', name: 'I_StockName', width: 150, align: "left" },
                        //{ label: '商品类型 0=原材料 1=半成品 2=成品', name: 'I_Kind', width: 200, align: "left" },
                        //{ label: '供应商编码', name: 'I_SupplyCode', width: 200, align: "left" },
                        //{ label: '供应商名称', name: 'I_SupplyName', width: 200, align: "left" },
                        { label: '商品编码', name: 'I_GoodsCode', width: 150, align: "left" },
                        { label: '商品名称', name: 'I_GoodsName', width: 150, align: "left" },
                        { label: '单位', name: 'I_Unit', width: 150, align: "left" },
                        { label: '保质期/天', name: 'G_Period', width: 150, align: "left" },
                        { label: '在库时间/天', name: 'InventoryDay', width: 150, align: "left" },
                        {
                            label: '保质期状态', name: 'GoodsState', width: 150, align: "left"
                            //formatterAsync: function (callback, value, row) {
                            //    ayma.clientdata.getAsync('dataItem', {
                            //        key: value,
                            //        code: 'RequistStatus',
                            //        callback: function (_data) {
                            //               if (value == '产品已过期') {
                            //                    callback("<span style='width:150px;height:25px;display:block;text-align:left;line-height:25px;font-size:10px;' class='label label-danger'>" + value + "</span>");
                            //               }
                            //               else if (value == '无') {
                            //                   callback(value);
                            //               }
                            //               else {
                            //                   callback("<span  style='width:150px;height:25px;display:block;text-align:left;line-height:25px;font-size:10px;' class='label label-success'>" + value + "</span>");
                            //               }
                            //        }
                            //    });
                            //}
                        },
                        //{ label: '数量', name: 'I_Qty', width: 200, align: "left" },
                        { label: '批次', name: 'I_Batch', width: 150, align: "left" },
                        //{ label: 'I_ProdDate', name: 'I_ProdDate', width: 200, align: "left" },
                        //{ label: '备注', name: 'I_Remark', width: 200, align: "left" },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                onRenderComplete: function (rows)
                {
                    for (var i = 0; i < rows.length; i++) {
                        if (rows[i].GoodsState != "产品已过期") {
                            continue;
                        }
                        else {
                            $("[rownum='rownum_girdtable_" + i + "']").css('background-color', '#d9534f');//保质期状态不正常的整行标红
                        }
                    }
                }
            })
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
