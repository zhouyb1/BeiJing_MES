/* * 创建人：Yabo,Zhou
 * 描  述：商品列表
 */

var refreshGirdData;
//上级元素的刷新表格方法
var parentRefreshGirdData;
//上级元素的删除表格方法;
var parentRemoveGridData;
//班组的编号
var C_TeamCode = request('C_TeamCode');
//上级元素的id
var parentFormId = request('formId');
var newArray = [];
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
var I_GoodsName = unescape(request("I_GoodsName"));//商品名称 解码
var I_StockName =unescape(request('I_StockName'));//仓库名称
var I_Unit = unescape(request('I_Unit'));//单位名称
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
                 //输入关键字搜索
            $("#I_Batch").on('keydown', function (event) {
                if (event.keyCode == "13") {
                    $('#am_btn_querySearch').click();
                }
            });
            //查询
            $('#am_btn_querySearch').on('click', function () {
                page.search();
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 500);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //// 查看详情
            //$('#am_edit').on('click', function () {
            //    var keyValue = $('#girdtable').jfGridValue('ID');
            //    if (ayma.checkrow(keyValue)) {
            //        ayma.layerForm({
            //            id: 'form',
            //            title: '库存查询',
            //            url: top.$.rootUrl + '/MesDev/InventorySeach/Form?keyValue=' + keyValue,
            //            width: 700,
            //            height: 500,
            //            maxmin: true,
            //            btn: null,
            //            callBack: function (id) {
            //                //return top[id].acceptClick(refreshGirdData);
            //            }
            //        });
            //    }
            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/InventorySeach/GetInventoryList?I_GoodsName=' + I_GoodsName + '&I_StockName=' + I_StockName + '&I_Unit=' + I_Unit,
                headData: [
          { label: "仓库编码", name: "I_StockCode", width: 100, align: "left" },
          { label: "仓库名称", name: "I_StockName", width: 100, align: "left" },
          { label: "商品编码", name: "I_GoodsCode", width: 100, align: "left" },
          { label: "商品名称", name: "I_GoodsName", width: 100, align: "left" },
          { label: "单位", name: "I_Unit", width: 100, align: "left" },
          { label: "数量", name: "I_Qty", width: 100, align: "left" },
          { label: "批次", name: "I_Batch", width: 100, align: "left" },
          { label: "备注", name: "I_Remark", width: 100, align: "left" },
                ],
                mainId: 'ID',
                isPage: true,
                sidx: "I_StockCode",
                sord: 'ASC',
            });
            page.search();
        },
        search: function (param) {
           param || {};
           param = $("#I_Batch").val();
            $('#girdtable').jfGridSet('reload', { param: { I_Batch: param } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}