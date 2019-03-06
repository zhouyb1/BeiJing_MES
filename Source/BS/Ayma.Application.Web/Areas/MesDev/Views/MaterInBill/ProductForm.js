﻿/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
 */
var acceptClick;
var refreshGirdData;
var RemoveGridData;
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
        },
        bind: function () {
            //添加商品
            $("#am_add").on("click", function () {
                var batch = $("#M_Batch").val();
                ayma.layerForm({
                    id: 'GoodsForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/MaterInBill/GoodsListIndex?formId=' + parentFormId + '&batch=' + batch,
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_MaterInDetail').jfGrid({
                headData: [
                    {
                        label: '物料编码', name: 'M_GoodsCode', width: 140, align: 'left',editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'M_GoodsName', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'M_Unit', width: 60, align: 'left', editType: 'label'
                    },
                    {
                        label: '数量', name: 'M_Qty', width: 60, align: 'left', editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.M_Qty.toString().replace('.', ''))) { //验证只能为数字
                                    row.M_Qty = 0;
                                }
                                if (row.M_Qty < 0 || ('' + row.M_Qty + '').substring(0, 1) == '0') {
                                    row.M_Qty = 0;
                                }
                            }
                        }
                    },
                    {
                        label: '批次', name: 'M_Batch', width: 100, align: 'left', editType: 'input'
                    },
                    {
                        label: '备注', name: 'M_Remark', width: 160, align: 'left', editType: 'input'
                    }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 3
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/MaterInBill/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_MaterInDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_MaterInDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var rowlist = $('#Mes_MaterInDetail').jfGridGet('rowdatas');
        if (rowlist[0]["ID"] == undefined) {
            ayma.alert.error("请选择商品");
            return false;
        }
        for (var i = 0; i < rowlist.length; i++) {
            if (rowlist[i]["M_Qty"] == "" || rowlist[i]["M_Qty"] == "0") {
                ayma.alert.error("数量不能为空");
                return false;
            }
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_MaterInHead"]').GetFormData());
        postData.strmes_MaterInDetailList = JSON.stringify($('#Mes_MaterInDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/MaterInBill/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_MaterInDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                rows.push(row);
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    rows.push(data[i]);
                }
            }
        }
        //数组过滤
        var filterarray = $.grep(rows, function (item) {
            return item["M_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_MaterInDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["M_GoodsCode"] == row["G_Code"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_MaterInDetail').jfGridGet('rowdatas');
    }
    page.init();
}