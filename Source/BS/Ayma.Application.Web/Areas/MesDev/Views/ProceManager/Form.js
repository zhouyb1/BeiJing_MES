/* * 创建人：超级管理员
 * 日  期：2019-03-06 14:55
 * 描  述：工序管理
 */
var acceptClick;
var keyValue = request('keyValue');
var parentId = request('parentId');
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
        },
        bind: function () {
            // 上级
            $('#P_ParentId').select({
                url: top.$.rootUrl + '/MesDev/Tools/GetProceTreeList',
                type: 'tree',
                allowSearch: true,
                maxHeight: 225
            }).selectSet(parentId);
            /*检测重复项*/
            $('#P_RecordCode').on('blur', function () {
                $.ExistField(keyValue, 'P_RecordCode', top.$.rootUrl + '/MesDev/ProceManager/ExistRecordCode');
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/ProceManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/ProceManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
