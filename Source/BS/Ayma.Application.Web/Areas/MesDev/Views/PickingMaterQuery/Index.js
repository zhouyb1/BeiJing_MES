﻿/* * 创建人：超级管理员
 * 日  期：2019-03-13 10:06
 * 描  述：领料单查询
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
            }, 220, 300);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 详情
            $('#am_detail').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('ID');
                if (ayma.checkrow(keyValue)) {
                    ayma.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/MesDev/PickingMaterQuery/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        maxmin: true,
                        btn:null,
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
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/PickingMaterQuery/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/MesDev/PickingMaterQuery/GetPageList',
                headData: [
                      {
                          label: "状态", name: "P_Status", width: 160, align: "left",
                          formatterAsync: function (callback, value, row) {
                              ayma.clientdata.getAsync('dataItem', {
                                  key: value,
                                  code: 'RequistStatus',
                                  callback: function (_data) {
                                      if (value == 1) {
                                          callback("<span class='label label-default'>" + _data.text + "</span>");
                                      } else if (value == 2) {
                                          callback("<span class='label label-info'>" + _data.text + "</span>");
                                      } else if (value == 3) {
                                          callback("<span class='label label-success'>" + _data.text + "</span>");
                                      } else {
                                          callback("<span class='label label-danger'>" + _data.text + "</span>");
                                      }
                                  }
                              });
                          }
                      },
                    { label: "领料单号", name: "C_CollarNo", width: 160, align: "left"},
                    { label: "生产订单", name: "P_OrderNo", width: 160, align: "left" },
                    { label: "仓库编码", name: "C_StockCode", width: 160, align: "left" },
                    { label: "仓库名称", name: "C_StockName", width: 160, align: "left" },
                    { label: "领料仓编码", name: "C_StockToCode", width: 160, align: "left" },
                    { label: "领料仓名称", name: "C_StockToName", width: 160, align: "left" },
                    { label: "备注", name: "C_Remark", width: 160, align: "left" },
                    { label: "添加人", name: "C_CreateBy", width: 160, align: "left"},
                    { label: "添加时间", name: "C_CreateDate", width: 160, align: "left"},
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
