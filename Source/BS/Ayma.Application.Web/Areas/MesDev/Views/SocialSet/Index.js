/* * 创建人：超级管理员
 * 日  期：2019-04-25 15:20
 * 描  述：社保设置
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
                    url: top.$.rootUrl + '/MesDev/SocialSet/Form',
                    width: 850,
                    height: 650,
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
                        url: top.$.rootUrl + '/MesDev/SocialSet/Form?keyValue=' + keyValue,
                        width: 850,
                        height: 650,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/SocialSet/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/SocialSet/GetPageList',
                headData: [
                    { label: "用户编码", name: "S_UserCode", width: 160, align: "left"},
                    { label: "用户姓名", name: "S_UserName", width: 160, align: "left"},
                    { label: "工资基数", name: "S_Wagebase", width: 160, align: "left"},
                    { label: "养老单位比例", name: "S_PensionUnitRatio", width: 160, align: "left"},
                    { label: "养老个人比例", name: "S_PensionPersonRatio", width: 160, align: "left"},
                    { label: "失业单位比例", name: "S_OutWorkUnitRatio", width: 160, align: "left"},
                    { label: "失业个人比例", name: "S_OutWorkPersonRatio", width: 160, align: "left"},
                    { label: "医疗单位比例", name: "S_MedicalUnitRatio", width: 160, align: "left"},
                    { label: "医疗个人比例", name: "S_MedicalPresonRatio", width: 160, align: "left"},
                    { label: "工伤单位比例", name: "S_InJuryUnitRatio", width: 160, align: "left"},
                    { label: "生育单位比例", name: "S_BearUnitRatio", width: 160, align: "left"},
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
