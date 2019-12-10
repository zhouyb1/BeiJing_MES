/* * 创建人：超级管理员
 * 日  期：2019-12-10 15:07
 * 描  述：IP与RFID对应表
 */
var selectedRow;
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
            }, 180, 300);
            //状态
            $("#I_Status").DataItemSelect({ code: 'IPToRFID' });
            //绑定门
            $('#I_DoorName').select({
                type: 'default',
                value: 'D_Name',
                text: 'D_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetDoorList',
                // 访问数据接口参数
                param: {}

            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                selectedRow = null;
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/Mes_IPToRFID/Form',
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
                selectedRow = $('#girdtable').jfGridGet('rowdata');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/Mes_IPToRFID/Form?keyValue=' + keyValue,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_IPToRFID/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_IPToRFID/GetPageList',
                headData: [
                        { label: 'RFID编码', name: 'I_RFIDCode', width: 200, align: "left" },
                        { label: 'IP地址', name: 'I_IP', width: 200, align: "left" },                  
                        { label: '门编码', name: 'I_DoorCode', width: 200, align: "left" },
                        { label: '门名称', name: 'I_DoorName', width: 200, align: "left" },
                        {
                            label: '状态', name: 'I_Status', width: 200, align: "left",
                            formatterAsync: function (callback, value, row) {
                                ayma.clientdata.getAsync('dataItem', {
                                    key: value,
                                    code: 'IPToRFID',
                                    callback: function (_data) {
                                        if (value == 1) {
                                            callback("<span class='label label-success'>" + _data.text + "</span>");                                        
                                        } else {
                                            callback("<span class='label label-danger'>" + _data.text + "</span>");
                                        }
                                    }
                                });
                            }
                        },
                        { label: '备注', name: 'I_Remark', width: 200, align: "left" },
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                sidx: 'I_RFIDCode',
                sord: 'ASC'

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
