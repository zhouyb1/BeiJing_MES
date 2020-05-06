/* * 创建人：超级管理员
 * 日  期：2019-03-20 09:36
 * 描  述：物料转换对应表
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
            
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 250, 480);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/Convert/Form',
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
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/Convert/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Convert/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/Convert/GetPageList',
                headData: [
                    { label: "物料编码", name: "C_Code", width: 120, align: "left"},
                    { label: "物料名称", name: "C_Name", width: 160, align: "left"},
                    { label: "转换后物料编码", name: "C_SecCode", width: 120, align: "left" },
                    { label: "转换后物料名称", name: "C_SecName", width: 160, align: "left" },
                    { label: "工序号", name: "C_ProNo", width: 100, align: "left" },
                    { label: "工序名称", name: "C_ProName", width: 120, align: "left" },
                    { label: "最大转化率(%)", name: "C_Max", width: 100, align: "left" },
                    { label: "最小转化率(%)", name: "C_Min", width: 100, align: "left" },
                    { label: "添加人", name: "C_CreateBy", width: 120, align: "left" },
                    { label: "添加时间", name: "C_CreateDate", width: 120, align: "left" },
                    { label: "修改人", name: "C_UpdateBy", width: 120, align: "left" },
                    { label: "修改时间", name: "C_UpdateDate", width: 120, align: "left" },
                    { label: "备注", name: "C_Remark", width: 120, align: "left" },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            param.C_Code = $("#C_Code").val();
            param.C_Name = $("#C_Name").val();
            param.C_SecCode = $("#C_SecCode").val();
            param.C_SecName = $("#C_SecName").val();
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
