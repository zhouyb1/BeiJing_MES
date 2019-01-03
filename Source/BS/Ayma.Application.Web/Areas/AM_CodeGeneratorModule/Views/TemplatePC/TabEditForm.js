﻿/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.11
 * 描 述：选项卡添加	
 */
var acceptClick;
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = top.layer_TabEditIndex.selectedRow;

    var page = {
        init: function () {
            page.initData();
        },
        initData: function () {
            if (!!selectedRow) {
                $('#form').SetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        var formData = $('#form').GetFormData();
        formData.id = formData.id || ayma.newGuid();
        formData.value = formData.id;
        formData.isexpand = false;
        formData.complete = true;

        if (!!selectedRow) {
            formData.componts = selectedRow.componts;
        }
        else {
            formData.componts = [];
        }
        

        callBack(formData);
        return true;
    };
    page.init();
}