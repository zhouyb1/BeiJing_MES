/* * 创建人：超级管理员
 * 日  期：2019-03-12 17:32
 * 描  述：排班记录
 */
var refreshGirdData;
var refreshSubGirdData;//子列表刷新
var $subgridTable;//子列表
var ArrangeEdit;//编辑子列表
var ArrangeDel;//删除子列表
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
                    title: '新增排班记录',
                    url: top.$.rootUrl + '/MesDev/Mes_Arrange/Form?formId=Mes_ArrangeForm',
                    width: 900,
                    height: 700,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            //$('#am_edit').on('click', function () {
            //    var keyValue = $('#girdtable').jfGridValue('ID');
            //    if (ayma.checkrow(keyValue)) {
            //        ayma.layerForm({
            //            id: 'form',
            //            title: '编辑排班记录',
            //            url: top.$.rootUrl + '/MesDev/Mes_Arrange/Form?keyValue=' + keyValue,
            //            width: 900,
            //            height: 700,
            //            maxmin: true,
            //            callBack: function (id) {
            //                return top[id].acceptClick(refreshGirdData);
            //            }
            //        });
            //    }
            //});
            // 删除
            //$('#am_delete').on('click', function () {
            //    var keyValue = $('#girdtable').jfGridValue('ID');
            //    if (ayma.checkrow(keyValue)) {
            //        ayma.layerConfirm('是否确认删除该项！', function (res) {
            //            if (res) {
            //                ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_Arrange/DeleteForm', { keyValue: keyValue}, function () {
            //                    refreshGirdData();
            //                });
            //            }
            //        });
            //    }
            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_Arrange/GetDataList',
                headData: [
                    {
                        label: "日期", name: "a_date", width: 160, align: "left",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    {
                        label: "时间", name: "a_datetime", width: 160, align: "left", formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'hh:mm:ss');
                        }
                    },
                    { label: "生产订单号", name: "a_orderno", width: 160, align: "left"},
                    { label: "车间编码", name: "a_workshopcode", width: 160, align: "left", },
                    { label: "班次", name: "a_classcode", width: 160, align: "left",}
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'a_date',
                sord: 'desc',
                isSubGrid: true,
                subGridRowExpanded: function(subgridId, row) {
                    var date = row.a_date;
                    var time = row.a_datetime;
                    var orderno = row.a_orderno;
                    var workshopcode = row.a_workshopcode;
                    var classcode = row.a_classcode;
                var subgridTableId = subgridId + "_t";
                $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                $subgridTable = $("#" + subgridTableId);
                $subgridTable.jfGrid({
                    url: top.$.rootUrl + '/MesDev/Mes_Arrange/GetSubDataList?date=' + date + '&time=' + time + '&orderno=' + orderno + '&workshopcode=' + workshopcode + '&classcode=' + classcode,
                    headData: [
                        {
                            label: '操作', name: '', index: '', width: 120, align: 'left', frozen: true,
                            formatter: function (value, grid, rows) {
                                var result = "<a href=\"javascript:;\" style=\"color:#f60\" onclick=\"ArrangeEdit('" + grid.id + "')\">编辑</a>" + "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:;\" style=\"color:#f60\" onclick=\"ArrangeDel('" + grid.id + "')\">删除</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                return result;
                            }
                        },
                        { label: "ID", name: "id", width: 160, align: "left",hidden:"true" },
                        { label: "用户编码", name: "a_f_encode", width: 100, align: "left" },
                        { label: "工艺代码", name: "a_record", width: 100, align: "left" },
                        { label: '工序号', name: 'a_procode', width: 100, align: 'left' },
                        {
                            label: "是否有效", name: "a_avail", width: 80, align: "left",
                             formatter: function(cellvalue, options, rowObject) {
                                return cellvalue == 1 ? "<span class=\"label label-success\" style=\"cursor: pointer;\">是</span>" : "<span class=\"label label-default\" style=\"cursor: pointer;\">否</span>";
                            }
                        },
                        { label: '备注', name: 'a_remark', width: 200, align: 'left' }
                    ],
                    mainId: 'id',
                    reloadSelected: true
                }).jfGridSet("reload");
                }
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    //编辑排班记录
    ArrangeEdit = function(keyValue) {
        if (ayma.checkrow(keyValue)) {
            ayma.layerForm({
                id: 'form',
                title: '编辑排班记录',
                url: top.$.rootUrl + '/MesDev/Mes_Arrange/Form?keyValue=' + keyValue,
                width: 900,
                height: 700,
                maxmin: true,
                callBack: function(id) {
                    return top[id].acceptClick(refreshSubGirdData);
                }
            });
        }
    };
    //删除排班记录
    ArrangeDel = function(keyValue) {
        if (ayma.checkrow(keyValue)) {
            ayma.layerConfirm('是否确认删除该项！', function(res) {
                if (res) {
                    ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_Arrange/DeleteForm', { keyValue: keyValue }, function() {
                        refreshSubGirdData();
                    });
                }
            });
        }
    };
    //子列表刷新
    refreshSubGirdData = function () {
        $subgridTable.jfGridSet("reload");
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
