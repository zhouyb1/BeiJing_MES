/* * 创建人：超级管理员
 * 日  期：2019-01-09 15:20
 * 描  述：同步ERP成品商品资料
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
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '查询ERP商品资料',
                    url: top.$.rootUrl + '/MesDev/Mes_ProductGoods/ERPTgoodsList',
                    width: 800,
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
                        url: top.$.rootUrl + '/MesDev/Mes_ProductGoods/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_ProductGoods/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/Mes_ProductGoods/GetPageList',
                headData: [
                    { label: "ID", name: "ID", width: 160, align: "left"},
                    { label: "商品编码", name: "G_Code", width: 160, align: "left"},
                    { label: "商品名称", name: "G_Name", width: 160, align: "left"},
                    { label: "保质时间", name: "G_Period", width: 160, align: "left"},
                    { label: "价格", name: "G_Price", width: 160, align: "left"},
                    { label: "单位", name: "G_Unit", width: 160, align: "left"},
                    { label: "添加人", name: "G_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "G_CreateDate", width: 160, align: "left"},
                    { label: "修改人", name: "G_UpdateBy", width: 160, align: "left"},
                    { label: "修改时间", name: "G_UpdateDate", width: 160, align: "left"},
                    { label: "备注", name: "G_Remark", width: 160, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true
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
