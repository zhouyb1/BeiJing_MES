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
        },
        search: function (param) {


            param = param || {};
            //param.StartTime = startTime;
            //param.EndTime = endTime;
            //jsonquery = param;

            var postData = {code:"鸭脖"};
            postData.queryJson = JSON.stringify(param);



            $.GetForm(top.$.rootUrl + '/MesDev/MaterInBill/GetPageTitle', postData, function (res) {

                $('#girdtable').jfGridSet("unload", {
                    url: top.$.rootUrl + '/MesDev/MaterInBill/GetMaterInDetail',
                    headData: res.data,
                    mainId: 'm_supplyname',
                    reloadSelected: true,
                    isPage: false,
                    footerrow: false,
                    isStatistics: false
                });

                $('#girdtable').jfGridSet('reload', { param: postData });
            });

            //alert('test');
            //$.GetForm(top.$.rootUrl + '/MesDev/MaterInBill/GetPageTitle', "{'name':'xiaom'}", function (res) {

            //    $('#girdtable').jfGridSet("unload", {
            //        url: top.$.rootUrl + '/MesDev/MaterInBill/GetMaterInDetail',
            //        headData: res.data,
            //        mainId: 'm_supplyname',
            //        reloadSelected: true,
            //        isPage: false,
            //        footerrow: false,
            //        isStatistics: false
            //    });

                //var s = "12";
                //$('#girdtable').jfGridSet('reload', { param: param });
            //});
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
