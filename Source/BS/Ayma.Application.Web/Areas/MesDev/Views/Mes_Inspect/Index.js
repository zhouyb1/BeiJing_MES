/* * 创建人：超级管理员
 * 日  期：2019-03-13 20:54
 * 描  述：抽检记录
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
                    title: '新增抽检记录',
                    url: top.$.rootUrl + '/MesDev/Mes_Inspect/Form',
                    width: 900,
                    height: 700,
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
                        url: top.$.rootUrl + '/MesDev/Mes_Inspect/Form?keyValue=' + keyValue,
                        width: 900,
                        height: 700,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_Inspect/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/Mes_Inspect/GetPageList',
                headData: [
                    { label: "主键", name: "ID", width: 160, align: "left",hidden:"true" },
                    { label: "抽检单号", name: "I_InspectNo", width: 160, align: "left"},
                    { label: "抽检时间", name: "I_Date", width: 160, align: "left"},
                    { label: "生产订单号", name: "I_OrderNo", width: 160, align: "left"},
                   
                    {
                        label: "抽检类型", name: "I_Kind", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            switch (value) {
                                case 1:
                                    callback("人为抽检");
                                    break;
                                case 2:
                                    callback("机器生产线");
                                    break;
                            }
                        }
                    },
                    { label: "车间编码", name: "I_Class", width: 160, align: "left"},
                    { label: "物料编码", name: "I_GoodsCode", width: 160, align: "left"},
                    { label: "物料名称", name: "I_GoodsName", width: 160, align: "left"},
                    { label: "抽检数量", name: "I_GoodsQty", width: 100, align: "left"},
                    { label: "合格数量", name: "I_QualifiedQty", width: 100, align: "left"},
                    { label: "不合格原因", name: "I_Reson", width: 360, align: "left" },
                    { label: "添加人", name: "I_CreateBy", width: 120, align: "left"},
                    { label: "添加时间", name: "I_CreateDate", width: 120, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'I_InspectNo',
                sord:'desc'
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
