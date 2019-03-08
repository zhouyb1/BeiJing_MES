/* * 创建人：超级管理员
 * 日  期：2019-01-07 09:31
 * 描  述：供应商列表
 */
var refreshGirdData;
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
            }, 180, 400);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增供应商',
                    url: top.$.rootUrl + '/MesDev/SupplyList/Form',
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
                        title: '编辑供应商',
                        url: top.$.rootUrl + '/MesDev/SupplyList/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/SupplyList/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/SupplyList/GetPageList',
                headData: [
                    { label: "供应商编码", name: "S_Code", width: 160, align: "left"},
                    { label: "供应商名称", name: "S_Name", width: 160, align: "left"},
                    { label: "资质期限", name: "S_EffectTime", width: 160, align: "left" },
                    { label: "备注", name: "S_Remark", width: 160, align: "left"},
                    { label: "添加人", name: "S_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "S_CreateDate", width: 160, align: "left"},
                    { label: "修改人", name: "S_UpdateBy", width: 160, align: "left"},
                    { label: "修改时间", name: "S_UpdateDate", width: 160, align: "left"}
                ],
                mainId:'ID',
                isPage: true,
                sidx: "S_CreateBy",
                sord: "DESC"
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
