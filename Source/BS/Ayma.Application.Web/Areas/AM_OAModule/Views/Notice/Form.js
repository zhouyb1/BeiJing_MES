﻿/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.11.11
 * 描 述：公告通知
 */
var acceptClick;
var keyValue = '';
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var ue;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            //公告类别
            $('#F_CategoryId').DataItemSelect({ code: 'NoticeCategory', maxHeight: 230 });
            //内容编辑器
            ue = UE.getEditor('editor');
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_NewsId;
                $('#form').SetFormData(selectedRow);
                $("#F_ReleaseTime").val(ayma.formatDate(selectedRow.F_ReleaseTime, 'yyyy/MM/dd hh:mm'));
                $.SetForm(top.$.rootUrl + '/AM_OAModule/Notice/GetEntity?keyValue=' + keyValue, function (data) {
                    setTimeout(function () {
                        ue.setContent(data.F_NewsContent);
                    }, 100);
                });
            }
        }
    };
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        var postData = $('#form').GetFormData(keyValue);
        postData["F_NewsContent"] = ue.getContent(null, null, true);
        $.SaveForm(top.$.rootUrl + '/AM_OAModule/Notice/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    }

    page.init();
}

