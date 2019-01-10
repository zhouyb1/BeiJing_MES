/* * 创建人：超级管理员
 * 日  期：2018-10-17 16:48
 * 描  述：商品改价表
 */
var acceptClick;
var keyValue = "1";// request('keyValue');
var ERPTgoodsListEntity = [];
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
            ////获取父级iframe中的刷新商品列表方法
            //parentRefreshGirdData = $(top[parentFormId]).context.firstChild.contentWindow.refreshGirdData;
        },
        bind: function () {
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            $('#ERPTgoodsList').jfGrid({
                headData: [
                    {
                        label: '商品编码', name: 'partno', width: 100, align: 'left',
                    },
                     {
                         label: '商品名称', name: 'pname', width: 100, align: 'left'
                     },
                    {
                        label: '规格', name: 'style', width: 60, align: 'left',
                    },
                    {
                        label: '采购单价(元)', name: 'pur_price', width: 80, align: 'left',
                    },

                    {
                        label: '销售单价(元)', name: 'price', width: 80, align: 'left',
                    },
                    {
                        label: '单位', name: 'pack_uom', width: 60, align: 'left', 
                    },
                    {
                        label: '包装数量', name: 'pack_qty', width: 60, align: 'left',
                    },
                     {
                         label: '税率', name: 'shuilv', width: 60, align: 'left'
                     },
                    {
                        label: '销售税率', name: 'shuilv_out', width: 60, align: 'left',
                    },
                ],
                isAutoHeight: false,
                isEidt: false,
                footerrow: true,
                isMultiselect: true,
                height: 300,
                minheight: 400,

                onSelectRow: function (rowdata, row, rowid) {
                    //console.log("rowdata:"+JSON.stringify(rowdata));
                    //console.log("row:" + JSON.stringify(rowdata));
                    //console.log("rowid:" + rowid);
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                     //   return false;
                   // }
                    //var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    //if (isChecked.is(":checked")) {
                    //    array.push(row);
                    //}

                    //if (!isChecked.is(":checked")) {
 
                    //}
                    ERPTgoodsListEntity = []
                    
                    ERPTgoodsListEntity.push(rowdata);
                
                },
                onRenderComplete: function (rows) {
                    //全选
                    $("#jfgrid_all_cb_ERPTgoodsList").on('click', function (row) {

                    });
                },
            });

            $("#am_add").on("click", function () {
                keyValue = $("#F_OrderDate").val();
                page.initData(keyValue);
            });

           
        },
        initData: function () {
            $.SetForm(top.$.rootUrl + '/MesDev/Mes_ProductGoods/GetErpTgoodsList?keyValue=' + keyValue, function (data) {
                for (var id in data) {
                    $('#ERPTgoodsList').jfGridSet('refreshdata', { rowdatas: data[id] });
                }
            });

        },
        search: function (data) {
            data = data || {};
            $('#ERPTgoodsList').jfGridSet('refreshdata', { rowdatas: data });
        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {};
        if ("[]" == JSON.stringify(ERPTgoodsListEntity))
        {
            ayma.alert.error("没有选中任何行!");
            return;
        }
        postData.ERPTgoodsListEntity = JSON.stringify(ERPTgoodsListEntity).replace("[[","[").replace("]]","]");
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_ProductGoods/SaveERPTgoodsList', postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();




}
