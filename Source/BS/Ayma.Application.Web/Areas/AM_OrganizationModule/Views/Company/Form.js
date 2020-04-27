/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.18
 * 描 述：公司管理	
 */
var F_CompanyId = request('F_CompanyId');
var acceptClick;
var keyValue = '';
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 公司性质
            $('#F_Nature').DataItemSelect({ code: 'CompanyNature', maxHeight: 230 });
            // 上级公司
            $('#F_ParentId').CompanySelect({ maxHeight: 180 });
            // 省市区
            $('#area').AreaSelect({ maxHeight: 160 });
            $('#F_ParentId').selectSet(F_CompanyId)
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_CompanyId;
                $('#form').SetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        var postData = $('#form').GetFormData(keyValue);
        if (postData["F_ParentId"] == '' || postData["F_ParentId"] == '&nbsp;') {
            postData["F_ParentId"] = '0';
        } else if (postData["F_ParentId"] == keyValue) {
            ayma.alert.error('上级不能是自己本身');
            return false;
        }
        $.SaveForm(top.$.rootUrl + '/AM_OrganizationModule/Company/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}