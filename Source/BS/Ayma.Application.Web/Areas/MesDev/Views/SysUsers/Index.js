/* * 创建人：超级管理员
 * 日  期：2019-03-05 20:34
 * 描  述：用户表
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
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/SysUsers/Form',
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
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/SysUsers/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/SysUsers/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/SysUsers/GetPageList',
                headData: [
                    { label: "编码", name: "U_Code", width: 160, align: "left"},
                    { label: "名称", name: "U_Name", width: 160, align: "left"},
                    { label: "密码", name: "U_Pass", width: 160, align: "left"},
                    { label: "部门", name: "U_Department", width: 160, align: "left"},
                    { label: "岗位", name: "U_Post", width: 160, align: "left"},
                    { label: "角色ID", name: "U_Ralecode", width: 160, align: "left"},
                    { label: "员工类型", name: "U_Kind", width: 160, align: "left"},
                    { label: "电话", name: "U_Telephone", width: 160, align: "left"},
                    { label: "RFID芯片编码", name: "U_RFIDCode", width: 160, align: "left"},
                    { label: "组别", name: "U_Group", width: 160, align: "left"},
                    { label: "入职日期", name: "U_Indate", width: 160, align: "left"},
                    { label: "离职日期", name: "U_Outdate", width: 160, align: "left"},
                    { label: "身份证", name: "U_Cert", width: 160, align: "left"},
                    { label: "性别", name: "U_Sex", width: 160, align: "left"},
                    { label: "民族", name: "U_Nation", width: 160, align: "left"},
                    { label: "学历", name: "U_Record", width: 160, align: "left"},
                    { label: "籍贯", name: "U_Origin", width: 160, align: "left"},
                    { label: "地址", name: "U_Address", width: 160, align: "left"},
                    { label: "照片", name: "U_Picture1", width: 160, align: "left"},
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
