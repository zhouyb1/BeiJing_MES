﻿/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.17
 * 描 述：自定义查询
 */
var keyValue = "";
var moduleId = "";
var queryDataList = [];
var acceptClick;
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var refreshData = function (label, data, rowid) {// 刷新条件
        if (rowid != "") {
            queryDataList[rowid] = data;
        }
        else {
            rowid = queryDataList.length;
            queryDataList.push(data);
            var $item = $('<div class="am-query-item" id="am_query_item_' + rowid + '"><div class="am-query-item-text"></div><div class="btn-group btn-group-sm"><a class="btn btn-default btn-edit">编辑</a><a class="btn btn-default btn-delete">删除</a></div></div>');
            $item.find('.btn-edit')[0].rowid = rowid;
            $item.find('.btn-delete')[0].rowid = rowid;
            $('#querylist').append($item);
        }
        $('#am_query_item_' + rowid).find('.am-query-item-text').html('<div class="am-query-item-num">' + (rowid + 1) + '</div>' + label);
    };

    var loadData = function () {
        $('#querylist').html("");
        for (var i = 0, l = queryDataList.length; i < l; i++) {
            var _item = queryDataList[i];
            var $item = $('<div class="am-query-item" id="am_query_item_' + i + '"><div class="am-query-item-text"></div><div class="btn-group btn-group-sm"><a class="btn btn-default btn-edit">编辑</a><a class="btn btn-default btn-delete">删除</a></div></div>');
            $item.find('.btn-edit')[0].rowid = i;
            $item.find('.btn-delete')[0].rowid = i;
            $('#querylist').append($item);

            var _value = _item.value;
            if (!!_item.date) {
                _value = "(" + _value + _item.date + ")";
            }
            $('#am_query_item_' + i).find('.am-query-item-text').html('<div class="am-query-item-num">' + (i + 1) + '</div>' + "【" + _item.fieldname + "】 " + _item.conditionname + " " + _value);
        }
    }

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 功能选择
            $('#F_ModuleId').select({
                url: top.$.rootUrl + '/AM_SystemModule/Module/GetModuleTree',
                type: 'tree',
                maxHeight: 250,
                allowSearch: true
            }).on('change', function () {
                moduleId = $(this).selectGet();
                var module = ayma.clientdata.get(['modulesMap', moduleId]);
                $('#F_ModuleUrl').val(module.F_UrlAddress);
            });
            // 条件行
            $('#querylist').on('click', function (e) {
                var et = e.target || e.srcElement;
                var $et = $(et);
                if ($et.hasClass('btn-edit')) {
                    var _rowid = $et[0].rowid;
                    ayma.layerForm({
                        id: 'QueryForm',
                        title: '添加自定义查询条件',
                        url: top.$.rootUrl + '/AM_SystemModule/CustmerQuery/QueryForm?moduleId=' + moduleId + '&rowid=' + _rowid,
                        width: 500,
                        height: 300,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshData);
                        }
                    });
                }
                else if ($et.hasClass('btn-delete')) {
                    var _rowid = $et[0].rowid;
                    queryDataList.splice(_rowid, 1);
                    loadData();
                }
            });

            // 添加条件
            $('#am_query_add').on('click', function () {
                if (!moduleId) {
                    ayma.alert.warning('请选择所属功能！');
                    return false;
                }
                ayma.layerForm({
                    id: 'QueryForm',
                    title: '添加自定义查询条件',
                    url: top.$.rootUrl + '/AM_SystemModule/CustmerQuery/QueryForm?moduleId=' + moduleId,
                    width: 500,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshData);
                    }
                });
            });

            // 公式提示
            $('#am-info').hover(function () { $('#am-message').show(); }, function () { $('#am-message').hide(); });
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_CustmerQueryId;
                $('#form').SetFormData(selectedRow);
                queryDataList = JSON.parse(selectedRow.F_QueryJson);
                loadData();
            }
        }
    };

    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        var postData = $('#form').GetFormData(keyValue);
        postData.F_QueryJson = JSON.stringify(queryDataList);
        $.SaveForm(top.$.rootUrl + '/AM_SystemModule/CustmerQuery/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    }

    page.init();
}


