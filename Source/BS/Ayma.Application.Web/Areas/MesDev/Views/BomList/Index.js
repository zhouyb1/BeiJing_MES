/* * 创建人：超级管理员
 * 日  期：2019-03-02 14:08
 * 描  述：BOM表
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 时间搜索框
            $('#datesearch').amdate({
                dfdata: [
                    { name: '今天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/BomList/Form',
                    width: 900,
                    height: 600,
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
                        url: top.$.rootUrl + '/MesDev/BomList/Form?keyValue=' + keyValue,
                        width: 900,
                        height: 600,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/BomList/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/BomList/GetPageList',
                headData: [
                    { label: "ID", name: "ID", width: 160, align: "left"},
                    { label: "BOM日期", name: "B_Date", width: 160, align: "left"},
                    { label: "BOM单号", name: "B_OrderNo", width: 160, align: "left"},
                    { label: "物料编码", name: "B_GoodsCode", width: 160, align: "left"},
                    { label: "物料名称", name: "B_GoodsName", width: 160, align: "left"},
                    { label: "单位", name: "B_Unit", width: 160, align: "left"},
                    { label: "等级", name: "B_Grade", width: 160, align: "left"},
                    { label: "下级物料编码", name: "B_SecGoodsCode", width: 160, align: "left"},
                    { label: "下级物料名称", name: "B_SecGoodsName", width: 160, align: "left"},
                    { label: "下级物料单位", name: "B_SecUnit", width: 160, align: "left"},
                    { label: "下级物料数量", name: "B_SecQty", width: 160, align: "left"},
                    { label: "转换率", name: "B_Conversion", width: 160, align: "left"},
                    { label: "添加人", name: "B_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "B_CreateDate", width: 160, align: "left"},
                    { label: "修改人", name: "B_UpdateBy", width: 160, align: "left"},
                    { label: "修改时间", name: "B_UpdateDate", width: 160, align: "left"},
                    { label: "备注", name: "B_Remark", width: 160, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
