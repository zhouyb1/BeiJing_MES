/* * 创建人：超级管理员
 * 日  期：2019-03-02 14:08
 * 描  述：配方详情列表页面
 */
var refreshGirdData; // 更新数据
var selectedRow;
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
            }, 220, 500);
            
            //工艺代码
            $("#B_RecordCode").select({
                type: 'default',
                value: 'R_Record',
                text: 'R_Record',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetRecordList',

            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                var id = $('#girdtable').jfGridValue('ID');
                var recordCode = $('#girdtable').jfGridValue('B_RecordCode');//工艺代码
                selectedRow = null;
                ayma.layerForm({
                    id: 'BomRecordForm',
                    title: '新增配方',
                    url: top.$.rootUrl + '/MesDev/BomHead/BomRecordForm?recordCode=' + recordCode + '&parentId=' + id,
                    width: 700,
                    height: 500,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                var keyValue = $('#girdtable').jfGridValue('ID');
                var B_ParentID = $('#girdtable').jfGridValue('B_ParentID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'BomRecordForm',
                        title: '编辑配方',
                        url: top.$.rootUrl + '/MesDev/BomHead/BomRecordForm?B_ParentID=' + B_ParentID,
                        width: 700,
                        height: 500,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/BomHead/DeleteBomRecordForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //导入配方表
            $('#am_import').on('click', function () {
                ayma.layerForm({
                    id: 'ImportForm',
                    title: '导入配方表',
                    url: top.$.rootUrl + '/MesDev/BomHead/ImportForm?formId=form',
                    width: 800,
                    height: 600,
                    maxmin: true,
                    btn: null,
                    callBack: function () {
                    }
                });
            });
        },
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/BomHead/GetBomRecordTreeList',
                headData: [
                           { label: "配方名称", name: "B_FormulaName", width: 160, align: "left" },
                           { label: "配方编码", name: "B_FormulaCode", width: 160, align: "left" },
                           { label: "工艺代码", name: "B_RecordCode", width: 160, align: "left" },      
                           { label: "物料编码", name: "B_GoodsCode", width: 160, align: "left" },
                           { label: "物料名称", name: "B_GoodsName", width: 160, align: "left" },
                           { label: '单位', name: 'B_Unit', width: 100, align: 'left' },
                           { label: '数量', name: 'B_Qty', width: 160, align: 'left' },
                           { label: "领料后仓库编码", name: "B_StockCode", width: 160, align: "left" },
                           { label: "领料后仓库名称", name: "B_StockName", width: 160, align: "left" }, 
                           {
                               label: "是否有效", name: "B_Avail", width: 160, align: "left",
                               formatterAsync: function (callback, value, row) {
                                   ayma.clientdata.getAsync('dataItem', {
                                       key: value,
                                       code: 'YesOrNo',
                                       callback: function (_data) {
                                           if (value == 1) {
                                               callback("<span class='label label-success'>" + _data.text + "</span>");
                                           } else {
                                               callback("<span class='label label-danger'>" + _data.text + "</span>");
                                           }
                                       }
                                   });
                               }
                           },
                           { label: '开始时间', name: 'B_StartTime', width: 160, align: 'left' },
                           { label: '结束时间', name: 'B_EndTime', width: 160, align: 'left' },
                           { label: '备注', name: 'B_Remark', width: 160, align: 'left' },
                           { label: "添加人", name: "B_CreateBy", width: 160, align: "left" },
                           { label: "添加时间", name: "B_CreateDate", width: 160, align: "left" },
                           { label: "修改人", name: "B_UpdateBy", width: 160, align: "left" },
                           { label: "修改时间", name: "B_UpdateDate", width: 160, align: "left" },

                ],
                isTree: true,
                mainId: 'ID',
                parentId: 'B_ParentID',
                reloadSelected: true,
                isShowNum: true
                //isAutoHeight: true,          // 自动适应表格高度
            });
            page.search();
        },
        search: function (param) {
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        page.search();
    }
    top.BomRecordIndexSelectedRow = function () {
        return selectedRow;
    }
    page.init();
}


