/* * 创建人：超级管理员
 * 日  期：2019-01-07 15:11
 * 描  述：人员走动记录列表
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
                    title: '新增人员走动记录',
                    url: top.$.rootUrl + '/MesDev/MoveRecordList/Form',
                    width: 700,
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
                        title: '编辑人员走动记录',
                        url: top.$.rootUrl + '/MesDev/MoveRecordList/Form?keyValue=' + keyValue,
                        width: 700,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/MoveRecordList/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/MoveRecordList/GetPageList',
                headData: [
                    { label: "用户编码", name: "M_UserCode", width: 160, align: "left"},
                    { label: "用户名称", name: "M_UserName", width: 160, align: "left"},
                    { label: "iP", name: "M_IP", width: 160, align: "left"},
                    { label: "RFID编码", name: "M_RFIDCode", width: 160, align: "left"},
                    { label: "门编码", name: "M_DoorCode", width: 160, align: "left"},
                    { label: "门名称", name: "M_DoorName", width: 160, align: "left"},
                    { label: "记录时间", name: "M_Date", width: 160, align: "left"},
                    { label: "备注", name: "M_Remark", width: 160, align: "left"},
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
