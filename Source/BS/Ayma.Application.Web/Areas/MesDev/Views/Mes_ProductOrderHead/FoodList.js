/* * 创建人：超级管理员
 * 日  期：2018-10-17 16:48
 * 描  述：商品改价表
 */
var acceptClick;
var keyValue = "";// request('keyValue');
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    $("#F_OrderDate").val(top.ayma.formatDate(new Date(), "yyyy-MM-dd"));//价格有效日期 当天00:00:00

    var page = {
        init: function () {
            keyValue = $("#F_OrderDate").val();
            page.bind();
            page.initData();
            ////获取父级iframe中的刷新商品列表方法
            //parentRefreshGirdData = $(top[parentFormId]).context.firstChild.contentWindow.refreshGirdData;
        },
        bind: function () {
          
            $('#ERPFoodList').jfGrid({
                headData: [
                    {
                        label: 'company', name: 'company', width: 160, align: 'left', editType: 'label', hidden: true
                    },
                     {
                         label: '公司名称', name: 'com_name', width: 100, align: 'left'
                     },
                    {
                        label: 'stock', name: 'stock', width: 160, align: 'left', hidden: true
                    },
                    {
                        label: '车站名称', name: 'name', width: 160, align: 'left', 
                    },
                    
                    {
                        label: '使用日期', name: 'use_date', width: 100, align: 'left',
                    },
                    {
                        label: '商品编码', name: 'pratno', width: 100, align: 'left', hidden:true
                    },
                    {
                        label: '商品名称', name: 'pname', width: 100, align: 'left', 
                    },
                     {
                         label: '数量', name: 'qty', width: 60, align: 'left', statistics: true
                     },
                    {
                        label: '计划日期', name: 't_date', width: 100, align: 'left', 
                    },
                     {
                         label: '外局名称', name: 'wj', width: 160, align: 'left', 
                     },
                ],
                isAutoHeight: true,
                isEidt: false,
                footerrow: true,
                minheight: 400,
                isStatistics: true,
            });

            $("#am_add").on("click", function () {
                keyValue = $("#F_OrderDate").val();
                page.initData();

            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_ProductOrderHead/GetErpFoodList?useDate=' + keyValue, function (data) {
                    if (data.ERPFoodList != null)
                    {
                        for (var id in data) {
                            $('#ERPFoodList').jfGridSet('refreshdata', { rowdatas: data[id] });
                            //if (!!data[id].length && data[id].length > 0) {
                          
                            //}
                            //else {
                            //    $('[data-table="' + id + '"]').SetFormData(data[id]);
                            
                            //}
                        }
                    }
                });
            }
        },
        search: function (data) {
            data = data || {};
            $('#ERPFoodList').jfGridSet('refreshdata', { rowdatas: data });
        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {};

        var foodData = JSON.stringify($('#ERPFoodList').jfGridGet('rowdatas'));
        console.log("ERPFoodList:" + JSON.stringify(ERPFoodList));

        //var entityPost = { F_ReviewMark: entityGoodsPriceDrive.F_ReviewMark, F_ReviewRemark: entityGoodsPriceDrive.F_ReviewRemark, F_ReviewFileId: entityGoodsPriceDrive.F_ReviewFileId };
        postData.ERPFoodListEntity = foodData;

        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_ProductOrderHead/SaveERPFoodList', postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();


    

}
