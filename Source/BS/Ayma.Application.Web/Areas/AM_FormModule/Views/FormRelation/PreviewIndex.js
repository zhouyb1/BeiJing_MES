﻿/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.17
 * 描 述：自定义表单发布功能	
 */
var id = request('id');

var refreshGirdData; // 更新数据

var formScheme;
var settingJson;
var relation;

var mainTablePk = "";
var mainTable = "";
var mainCompontId = "";

var bootstrap = function ($, ayma) {
    "use strict";

    var queryJson = {};

    var page = {
        init: function () {
            // 获取自定义表单设置内容
            ayma.httpAsync('GET', top.$.rootUrl + '/AM_FormModule/FormRelation/GetCustmerFormData', { keyValue: id }, function (data) {
                relation = data.relation;
                settingJson = JSON.parse(data.relation.F_SettingJson);
                formScheme = JSON.parse(data.scheme.F_Scheme);

                for (var i = 0, l = formScheme.dbTable.length; i < l; i++)
                {
                    var tabledata = formScheme.dbTable[i];
                    if (tabledata.relationName == "")
                    {
                        mainTable = tabledata.name;
                        mainTablePk = tabledata.field;
                    }
                }


                // 条件项设置
                if (settingJson.query.isDate == '1' && !!settingJson.query.DateField) {// 时间搜索框
                    $('#datesearch').amdate({
                        dfdata: [
                            { name: '今天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                            { name: '近7天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                            { name: '近1个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                            { name: '近3个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
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
                            queryJson.lrbegin = begin;
                            queryJson.lrend = end;
                            queryJson.amdateField = settingJson.query.DateField;

                            page.search();
                        }
                    });
                }

                // 复合条件查询
                if (!!settingJson.query.fields && settingJson.query.fields.length > 0) {
                    $('#multiple_condition_query_item').html('<div id="multiple_condition_query"><div class="am-query-formcontent"></div></div>');
                    $('#multiple_condition_query').MultipleQuery(function (_queryJson) {
                        queryJson = _queryJson;
                        page.search();
                    }, 220);
                    var $content = $('#multiple_condition_query .am-query-content');

                    var queryFieldMap = {};
                    $.each(settingJson.query.fields, function (id, item) {
                        queryFieldMap[item.fieldId] = item;
                    });
                    for (var i = 0, l = formScheme.data.length; i < l; i++) {
                        var componts = formScheme.data[i].componts;
                        for (var j = 0, jl = componts.length; j < jl; j++) {
                            var item = componts[j];
                            var queryItem = queryFieldMap[item.id];
                            if (!!queryItem) {
                                queryItem.compont = item;
                            }
                        }
                    }
                    $.each(queryFieldMap, function (id, item) {
                        if (!!item.compont) {
                            var $row = $('<div class="col-xs-' + (12 / parseInt(item.portion)) + ' am-form-item" ></div>');
                            var $title = $(' <div class="am-form-item-title">' + item.fieldName + '</div>');
                            $row.append($title);
                            $content.append($row);
                            $.FormComponents[item.compont.type].renderQuery(item.compont, $row)[0].compont = item.compont;
                        }
                    });
                }
                // 列表设置
                page.initGird();
                // 按钮绑定事件
                page.bind();
            });
        },
        bind: function () {
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
                if (settingJson.layer.opentype == '1') {// 窗口弹层页
                    ayma.layerForm({
                        id: 'Form',
                        title: '新增',
                        url: top.$.rootUrl + '/AM_FormModule/Custmerform/LayerInstanceForm?id=' + relation.F_FormId,
                        width: settingJson.layer.width,
                        height: settingJson.layer.height,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
                else {// 窗口页
                    ayma.frameTab.open({ F_ModuleId: id, F_Icon: 'fa fa-pencil-square-o', F_FullName: '新增', F_UrlAddress: '/AM_FormModule/Custmerform/TabInstanceForm?id=' + relation.F_FormId });
                }
            });
            // 编辑
            $('#am_edit').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue(mainCompontId.toLowerCase());


                if (ayma.checkrow(keyValue)) {
                    if (settingJson.layer.opentype == '1') {// 窗口弹层页
                        ayma.layerForm({
                            id: 'Form',
                            title: '编辑',
                            url: top.$.rootUrl + '/AM_FormModule/Custmerform/LayerInstanceForm?id=' + relation.F_FormId + "&keyValue=" + keyValue,
                            width: settingJson.layer.width,
                            height: settingJson.layer.height,
                            callBack: function (id) {
                                return top[id].acceptClick(refreshGirdData);
                            }
                        });
                    }
                    else {// 窗口页
                        ayma.frameTab.open({ F_ModuleId: id, F_Icon: 'fa fa-pencil-square-o', F_FullName: '编辑', F_UrlAddress: '/AM_FormModule/Custmerform/TabInstanceForm?id=' + relation.F_FormId + "&keyValue=" + keyValue });
                    }
                }
            });
            // 删除
            $('#am_delete').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue(mainCompontId.toLowerCase());
                if (ayma.checkrow(keyValue)) {
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/AM_FormModule/Custmerform/DeleteInstanceForm', { keyValue: keyValue, schemeInfoId: relation.F_FormId }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            var colFieldMap = {};
            for (var i = 0, l = settingJson.col.fields.length; i < l; i++) {
                colFieldMap[settingJson.col.fields[i].compontId] = settingJson.col.fields[i];
            }
            var headData = [];

            var tableIndex = 0;
            var tableMap = {};

            for (var i = 0, l = formScheme.data.length; i < l; i++) {
                var componts = formScheme.data[i].componts;
                for (var j = 0, jl = componts.length; j < jl; j++) {
                    var item = componts[j];
                    if (!!item.table && tableMap[item.table] == undefined) {
                        tableMap[item.table] = tableIndex;
                        tableIndex++;
                    }


                    if (mainTable == item.table && mainTablePk == item.field) {
                        mainCompontId = item.field + tableMap[item.table];
                    }

                    var colItem = colFieldMap[item.id];
                    if (!!colItem) {
                        colItem.compont = item;
                    }
                }
            }
            $.each(colFieldMap, function (id, item) {
                if (!!item.compont)
                {
                    var point = { label: item.fieldName, name: (item.compont.field + tableMap[item.compont.table]).toLowerCase(), width: parseInt(item.width), align: item.align };
                    switch (item.compont.type) {
                        case 'radio':
                        case 'checkbox':
                        case 'select':
                            if (item.compont.dataSource == "0") {
                                point.formatterAsync = function (callback, value, row) {
                                    ayma.clientdata.getAsync('dataItem', {
                                        key: value,
                                        code: item.compont.itemCode,
                                        callback: function (_data) {
                                            callback(_data.text);
                                        }
                                    });
                                }
                            }
                            else {
                                var vlist = item.compont.dataSourceId.split(',');
                                point.formatterAsync = function (callback, value, row) {
                                    ayma.clientdata.getAsync('sourceData', {
                                        key: value,
                                        keyId: vlist[2],
                                        code: vlist[0],
                                        callback: function (_data) {
                                            callback(_data[vlist[1]]);
                                        }
                                    });
                                }
                            }
                            break;

                        case 'organize':
                        case 'currentInfo':
                            if (item.compont.dataType == 'user') {

                                point.formatterAsync = function (callback, value, row) {
                                    ayma.clientdata.getAsync('user', {
                                        key: value,
                                        callback: function (item) {
                                            callback(item.name);
                                        }
                                    });
                                }

                            }
                            else if (item.compont.dataType == 'company')
                            {
                                point.formatterAsync = function (callback, value, row) {
                                    ayma.clientdata.getAsync('company', {
                                        key: value,
                                        callback: function (_data) {
                                            callback(_data.name);
                                        }
                                    });
                                }
                            }
                            else if (item.compont.dataType == 'department') {
                                point.formatterAsync = function (callback, value, row) {
                                    ayma.clientdata.getAsync('department', {
                                        key: value,
                                        callback: function (item) {
                                            callback(item.name);
                                        }
                                    });
                                }
                            }
                            break;
                    }
                    headData.push(point);
                }
               
            });

            var girdurl = "";
            if (settingJson.col.isPage == "1") {
                girdurl = top.$.rootUrl + '/AM_FormModule/FormRelation/GetPreviewPageList?keyValue=' + id;
            }
            else {
                girdurl = top.$.rootUrl + '/AM_FormModule/FormRelation/GetPreviewList?keyValue=' + id;
            }

            $('#girdtable').AuthorizeJfGrid({
                url: girdurl,
                headData: headData,
                reloadSelected: true,
                isPage: (settingJson.col.isPage == "1" ? true : false),
                mainId: mainCompontId.toLowerCase()
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.queryJson = JSON.stringify(queryJson);
            $('#girdtable').jfGridSet('reload', { param: param });
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        page.search();
    }

    page.init();
}


