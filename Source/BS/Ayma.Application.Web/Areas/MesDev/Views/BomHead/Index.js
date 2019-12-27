/* * 创建人：超级管理员
 * 日  期：2019-03-06 17:41
 * 描  述：配方表
 */
var refreshGirdData;
var $subgridTable;//子列表
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
            //工艺代码
            $("#B_RecordCode").select({
                type: 'default',
                value: 'P_RecordCode',
                text: 'P_RecordCode',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProceList',
                // 访问数据接口参数
                param: {}
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/BomHead/Form',
                    width: 600,
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
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/BomHead/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 配方详情
            $('#am_detail').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '配方详情',
                    url: top.$.rootUrl + '/MesDev/BomHead/BomRecordIndex',
                    width: 950,
                    height: 700,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });

            });
            // 删除
            $('#am_delete').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/BomHead/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/BomHead/GetPageList',
                headData: [
                    { label: "工艺代码", name: "B_RecordCode", width: 160, align: "left" },
                    { label: "工序号", name: "B_ProNo", width: 160, align: "left" },
                    { label: "配方编码", name: "B_FormulaCode", width: 160, align: "left" },
                    { label: "配方名称", name: "B_FormulaName", width: 160, align: "left" },
                    { label: "物料编码", name: "B_GoodsCode", width: 160, align: "left" },
                    { label: "物料名称", name: "B_GoodsName", width: 160, align: "left" },
                    { label: "领料后仓库名称", name: "B_StockName", width: 160, align: "left" },
                    { label: "领料后仓库编码", name: "B_StockCode", width: 160, align: "left" },
                    {
                        label: "是否生效", name: "B_Avail", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'YesOrNo',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "开始时间", name: "B_StartTime", width: 160, align: "left" },
                    { label: "截止时间", name: "B_EndTime", width: 160, align: "left" },
                    { label: "单位", name: "B_Unit", width: 160, align: "left" },
                ],
                mainId: 'ID',
                reloadSelected: false,
                sidx: "B_ProNo",
                isPage: true,
                isSubGrid: true,             // 是否有子表editType 
                subGridRowExpanded: function (subgridId, row) {// 子表展开后调用函数

                    var id = row.B_FormulaCode;
                    var subgridTableId = subgridId + "_t";
                    $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                    $subgridTable = $("#" + subgridTableId);
                    $subgridTable.jfGrid({
                        url: top.$.rootUrl + '/MesDev/BomHead/GetBomRecordListBy?formulaCode=' + id,
                        headData: [
                            { label: "物料编码", name: "B_GoodsCode", width: 160, align: "left" },
                            { label: "物料名称", name: "B_GoodsName", width: 160, align: "left" },
                            { label: '单位', name: 'B_Unit', width: 100, align: 'left' },
                            { label: '数量', name: 'B_Qty', width: 160, align: 'left' },
                            { label: '下级物料编码', name: 'B_SecGoodsCode', width: 100, align: 'left' },
                            { label: '下级物料名称', name: 'B_SecGoodsName', width: 100, align: 'left' },
                            { label: '下级物料数量', name: 'B_SecQty', width: 100, align: 'left' },
                            { label: '下级物料单位', name: 'B_SecUnit', width: 160, align: 'left' }

                        ],
                        isTree: true,
                        mainId: 'B_SecGoodsCode',
                        parentId: 'B_GoodsCode',
                        //reloadSelected: true,
                        isShowNum: true,
                        isAutoHeight: true,          // 自动适应表格高度
                        //isPage: true,
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
    page.init();
}
