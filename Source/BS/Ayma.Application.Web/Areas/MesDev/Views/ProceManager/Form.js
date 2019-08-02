/* * 创建人：超级管理员
 * 日  期：2019-03-06 14:55
 * 描  述：工序管理
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initData();
        },
        bind: function () {
            //物料名称
            $("#B_GoodsName").select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProjGoodsList',
                // 访问数据接口参数
            }).bind("change", function () {
                var name = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByNameGetGoodsEntity',
                    data: { name: name },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#R_GoodsCode").val(entity.G_Code);
                    }
                });
            });
            /*检测重复项 工艺代码*/
            $('#R_Record').on('blur', function () {
                $.ExistField(keyValue, 'R_Record', top.$.rootUrl + '/MesDev/ProceManager/ExistRecordCode');
            });
           
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/ProceManager/GetRecordFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                            $.ajax({
                                type: "get",
                                url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetGoodsEntity',
                                data: { code: data[id].R_GoodsCode },
                                success: function (data) {
                                    var entity = JSON.parse(data).data;
                                    $("#B_GoodsName").selectSet(entity.G_Name);
                                }
                            });
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
        $.SaveForm(top.$.rootUrl + '/MesDev/ProceManager/SaveRecordForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
