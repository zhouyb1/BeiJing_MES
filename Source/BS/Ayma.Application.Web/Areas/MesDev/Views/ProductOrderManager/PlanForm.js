/* * 创建人：何鹏
 * 日  期：2020-1-02 10:48
 * 描  述：领料计划
 */
var acceptClick;
var bootstrap = function ($, ayma) {
    "use strict";
    $("#date").val(top.ayma.formatDate(new Date(), "yyyy-MM-dd"));//价格有效日期 当天00:00:00

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {         
        },
        initData: function () {
        },
        search: function (data) {
        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        var postData = {};
        postData.date = $("#date").val();     
        $.SaveForm(top.$.rootUrl + '/MesDev/PickingMater/AutoCreateOrder', postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();




}
