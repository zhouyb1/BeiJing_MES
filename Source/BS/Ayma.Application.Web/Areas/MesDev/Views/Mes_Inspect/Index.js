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
                    width: 700,
                    height: 450,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status =$('#girdtable').jfGridValue('I_Status');
                if (ayma.checkrow(keyValue)) {
                    if (status == 2) {
                        ayma.alert.warning('抽检已完成，请勿重复修改！');
                        return false;
                    }
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/Mes_Inspect/Form?keyValue=' + keyValue,
                        width: 700,
                        height: 450,
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

            //提交
            $('#am_post').on('click', function() {
                var keyValue = $('#girdtable').jfGridValue('ID');
                var status = $('#girdtable').jfGridValue('I_Status');
                var orderNo = $('#girdtable').jfGridValue('I_InspectNo');
                if (ayma.checkrow(keyValue)) {
                    if (status == 2) {
                        ayma.alert.warning('请勿重复提交！');
                        return false;
                    } else {
                        ayma.layerConfirm('是否确认提交该项！', function(res) {
                            if (res) {
                                ayma.postForm(top.$.rootUrl + '/MesDev/Tools/PostOrCancelOrDeleteBill', { orderNo: orderNo, proc: 'sp_Inspect_Post', type: 1 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_Inspect/GetPageList',
                headData: [
                    { label: "主键", name: "ID", width: 160, align: "left", hidden: "true" },
                    {
                        label: "状态", name: "I_Status", width: 160, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'BackSupplyStatus',
                                callback: function (_data) {
                                    if (value == 1) {
                                        callback("<span class='label label-default'>生成抽检单</span>");
                                    } else  {
                                        callback("<span class='label label-info'>完成抽检单</span>");
                                    } 
                                }
                            });
                        }
                    },
                    { label: "抽检单号", name: "I_InspectNo", width: 160, align: "left"},
                    { label: "生产订单号", name: "I_OrderNo", width: 160, align: "left"},
                    { label: "仓库", name: "I_StockName", width: 160, align: "left" },
                    { label: "仓库编码", name: "I_StockCode", width: 160, align: "left" },
                    { label: "物料名称", name: "I_GoodsName", width: 160, align: "left" },
                    { label: "物料编码", name: "I_GoodsCode", width: 160, align: "left" }, 
                    { label: "抽检批次", name: "I_Batch", width: 160, align: "left" },
                    { label: "抽检数量", name: "I_GoodsQty", width: 100, align: "left" },
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
                    { label: "合格数量", name: "I_QualifiedQty", width: 100, align: "left"},
                    { label: "不合格原因", name: "I_Reson", width: 120, align: "left" },
                    { label: "抽检时间", name: "I_Date", width: 120, align: "left" },
                    { label: "添加人", name: "I_CreateBy", width: 120, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'I_Date',
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
