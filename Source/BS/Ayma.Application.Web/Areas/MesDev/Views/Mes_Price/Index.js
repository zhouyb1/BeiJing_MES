/* * 创建人：超级管理员
 * 日  期：2019-12-17 12:37
 * 描  述：变价记录表
 */
var $subgridTable;//子列表
var refreshSubGirdData;
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
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            //供应商名称
            $("#P_SupplyName").select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetEffectSupplyList',
                // 访问数据接口参数
            });
            //原物料名称
            $("#P_GoodsName").select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
                // 访问数据接口参数
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                selectedRow = null;
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/Mes_Price/Form',
                    width: 700,
                    height: 400,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/Mes_Price/Form?keyValue=' + keyValue,
                        width: 700,
                        height: 400,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_Price/DeleteForm', { keyValue: keyValue}, function () {
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_Price/GetPageList',
                headData: [
                        { label: '供应商编码', name: 'P_SupplyCode', width: 200, align: "left" },
                        { label: '供应商名称', name: 'P_SupplyName', width: 200, align: "left" },
                        { label: '物料编码', name: 'P_GoodsCode', width: 150, align: "left" },
                        { label: '物料名称', name: 'P_GoodsName', width: 150, align: "left" },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                isSubGrid: true,
                subGridRowExpanded: function (subgridId, row) {
                    var P_SupplyCode = row.P_SupplyCode;
                    var P_GoodsCode = row.P_GoodsCode;
                    var subgridTableId = subgridId + "_t";
                    $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                    $subgridTable = $("#" + subgridTableId);
                    $subgridTable.jfGrid({
                        url: top.$.rootUrl + '/MesDev/Mes_Price/GetPriceBySupply?P_SupplyCode=' + P_SupplyCode + '&P_GoodsCode=' + P_GoodsCode,
                        headData: [
                        { label: "ID", name: "ID", width: 160, align: "left", hidden: true },
                        { label: '物料编码', name: 'P_GoodsCode', width: 150, align: "left" },
                        { label: '物料名称', name: 'P_GoodsName', width: 150, align: "left" },
                        { label: '价格', name: 'P_InPrice', width: 150, align: "left" }, 
                        { label: '税率', name: 'P_Itax', width: 150, align: "left" },
                        { label: '起始时间', name: 'P_StartDate', width: 150, align: "left" },
                        { label: '结束时间', name: 'P_EndDate', width: 150, align: "left" },
                        { label: '添加人', name: 'P_CreateBy', width: 150, align: "left" },
                        { label: '添加时间', name: 'P_CreateDate', width: 150, align: "left" },
                        ],
                        mainId: 'ID',
                        isPage: true,
                        sidx: "P_GoodsCode,P_CreateDate",
                        sord: 'ASC',
                        reloadSelected: false,
                    }).jfGridSet("reload");
                }
            });
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
    refreshSubGirdData = function () {
        $subgridTable.jfGridSet("reload");
    };
    page.init();
}
