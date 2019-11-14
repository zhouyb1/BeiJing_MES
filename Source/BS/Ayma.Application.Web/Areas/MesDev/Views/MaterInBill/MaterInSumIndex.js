/* * 创建人：超级管理员
 * 日  期：2018-11-13 16:47
 * 描  述：原物料入库列表
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
                page.search();
            });
            $('#girdtable').jfGrid({});


            var postData = {};
            postData.queryJson = JSON.stringify({ code: "9527" });

            $.GetForm(top.$.rootUrl + '/MesDev/MaterInBill/GetPageTitle', postData, function (res) {

                $('#girdtable').jfGridSet("unload", {
                    url: top.$.rootUrl + '/MesDev/MaterInBill/GetMaterInDetail',
                    headData: res.data,
                    mainId: 'M_SupplyName',
                    reloadSelected: true,
                    isPage: false,
                    footerrow: false,
                    isStatistics: false
                });

                $('#girdtable').jfGridSet('reload', { param: postData });
            });
        },
        search: function () {

            
            //var postData = {};
            //postData.queryJson = JSON.stringify({code:"9527"});

            //$.GetForm(top.$.rootUrl + '/MesDev/MaterInBill/GetPageTitle', postData, function (res) {

            //    $('#girdtable').jfGridSet("unload", {
            //        url: top.$.rootUrl + '/MesDev/MaterInBill/GetMaterInDetail',
            //        headData: res.data,
            //        mainId: 'M_SupplyName',
            //        reloadSelected: true,
            //        isPage: false,
            //        footerrow: false,
            //        isStatistics: false
            //    });

            //    //$('#girdtable').jfGridSet('reload', { param: postData });
            //});
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
