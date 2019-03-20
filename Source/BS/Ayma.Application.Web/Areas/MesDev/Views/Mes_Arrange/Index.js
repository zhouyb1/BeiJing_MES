/* * 创建人：超级管理员
 * 日  期：2019-03-12 17:32
 * 描  述：排班记录
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
                    title: '新增排班记录',
                    url: top.$.rootUrl + '/MesDev/Mes_Arrange/Form',
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
                        title: '编辑排班记录',
                        url: top.$.rootUrl + '/MesDev/Mes_Arrange/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_Arrange/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/Mes_Arrange/GetPageList',
                headData: [
                    { label: "ID", name: "ID", width: 160, align: "left",hidden:"true"},
                    {
                        label: "日期", name: "A_Date", width: 160, align: "left",
                        formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    {
                        label: "时间", name: "A_DateTime", width: 160, align: "left", formatter: function (cellvalue, options, rowObject) {
                            return ayma.formatDate(cellvalue, 'hh:mm:ss');
                        }
                    },
                    { label: "生产订单号", name: "A_OrderNo", width: 160, align: "left"},
                    { label: "车间编码", name: "A_WorkShopCode", width: 160, align: "left",},
                    { label: "车间名称", name: "A_WorkShopName", width: 160, align: "left"},
                    { label: "用户编码", name: "A_F_EnCode", width: 160, align: "left",},
                    { label: "工艺代码", name: "A_Record", width: 160, align: "left",},
                    { label: "工序号", name: "A_ProCode", width: 160, align: "left",},
                    { label: "班次", name: "A_ClassCode", width: 160, align: "left",},
                    {
                        label: "是否生效", name: "A_Avail", width: 160, align: "left",
                        formatter: function (cellvalue, options, rowObject) {
                            return cellvalue == 1 ? "<span class=\"label label-success\" style=\"cursor: pointer;\">是</span>" : "<span class=\"label label-default\" style=\"cursor: pointer;\">否</span>";
                        }
                    },
                    { label: "添加人", name: "A_CreateBy", width: 160, align: "left" },
                    { label: "添加时间", name: "A_CreateDate", width: 160, align: "left" },
                    { label: "修改人", name: "A_UpdateBy", width: 160, align: "left" },
                    { label: "修改时间", name: "A_UpdateDate", width: 160, align: "left" },
                    { label: "备注", name: "A_Remark", width: 160, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'A_Date',
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
