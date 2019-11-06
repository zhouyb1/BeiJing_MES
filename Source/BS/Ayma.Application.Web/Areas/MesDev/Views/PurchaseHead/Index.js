/* * 创建人：超级管理员
 * 日  期：2019-03-05 11:20
 * 描  述：采购单制作及查询
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
            }, 250, 400);
            $('#P_Status').DataItemSelect({ code: 'PurchaseStatus' });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增采购单',
                    url: top.$.rootUrl + '/MesDev/PurchaseHead/Form',
                    width: 950,
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
                        title: '编辑采购单',
                        url: top.$.rootUrl + '/MesDev/PurchaseHead/Form?keyValue=' + keyValue,
                        width: 950,
                        height: 700,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 双击编辑
            $('#girdtable').on('dblclick', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑采购单',
                        url: top.$.rootUrl + '/MesDev/PurchaseHead/Form?keyValue=' + keyValue,
                        width: 950,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/PurchaseHead/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/PurchaseHead/GetPageList',
                headData: [
                    { label: "采购单号", name: "P_PurchaseNo", width: 160, align: "left"},
                    { label: "仓库编码", name: "P_StockCode", width: 160, align: "left"},
                    { label: "仓库名称", name: "P_StockName", width: 160, align: "left"},
                    { label: "供应商编码", name: "P_SupplyCode", width: 160, align: "left"},
                    { label: "供应商名称", name: "P_SupplyName", width: 160, align: "left"},
                    { label: "生产订单号", name: "P_OrderNo", width: 160, align: "left"},
                    { label: "订单时间", name: "P_OrderDate", width: 160, align: "left"},
                    { label: "状态", name: "P_Status", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                             ayma.clientdata.getAsync('dataItem', {
                                 key: value,
                                 itemCode: 'PurchaseStatus',
                                 callback: function (_data) {
                                     callback(_data.F_ItemName);
                                 }
                             });
                        }},
                    { label: "添加人", name: "P_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "P_CreateDate", width: 160, align: "left"},
                    { label: "修改人", name: "P_UpdateBy", width: 160, align: "left"},
                    { label: "修改时间", name: "P_UpdateDate", width: 160, align: "left"},
                    { label: "备注", name: "P_Remark", width: 160, align: "left"},
                    { label: "提交人", name: "P_UploadBy", width: 160, align: "left"},
                    { label: "提交时间", name: "P_UploadDate", width: 160, align: "left"},
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
