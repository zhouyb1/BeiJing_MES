/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
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
            }, 180, 500);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 查看详情
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '查看详情',
                        url: top.$.rootUrl + '/MesDev/MaterInBill/Form?keyValue=' + keyValue,
                        width: 800,
                        height: 600,
                        maxmin: true,
                        btn: null,
                        callBack: function (id) {

                        }
                    });
                }
            });
            //撤销单据
            $("#am_cancel").on('click', function () {
                var orderNo = $("#girdtable").jfGridValue("M_MaterInNo");
                if (ayma.checkrow(orderNo)) {
                    ayma.layerConfirm('是否确认审核该单据！', function (res) {
                        if (res) {
                            ayma.postForm(top.$.rootUrl + '/MesDev/MaterInBill/CancelBill', { orderNo: orderNo }, function () {
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
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetPostPageList',
                headData: [

                    { label: "入库单号", name: "M_MaterInNo", width: 160, align: "left" },
                    { label: "物料编码", name: "M_StockCode", width: 160, align: "left" },
                    { label: "物料名称", name: "M_StockName", width: 160, align: "left" },
                    { label: "生产订单号", name: "M_OrderNo", width: 160, align: "left" },
                    { label: "订单时间", name: "M_OrderDate", width: 160, align: "left" },
                    { label: "备注", name: "M_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "M_CreateBy", width: 160, align: "left" },
                    { label: "添加时间", name: "M_CreateDate", width: 160, align: "left" },
                    { label: "修改人", name: "M_UpdateBy", width: 160, align: "left" },
                    { label: "修改时间", name: "M_UpdateDate", width: 160, align: "left" },
                    { label: "提交人", name: "M_UploadBy", width: 160, align: "left"},
                    { label: "提交时间", name: "M_UploadDate", width: 160, align: "left"}
                ],
                mainId: 'ID',
                isPage: true,
                sidx: 'M_MaterInNo',
                sord: 'DESC'
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
