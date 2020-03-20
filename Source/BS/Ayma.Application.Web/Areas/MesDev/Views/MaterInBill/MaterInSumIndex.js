/* * 创建人：超级管理员
 * 日  期：2018-11-13 16:47
 * 描  述：原物料入库列表
 */
var refreshGirdData;
var jsonquery = {};
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 320, 500);

            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
                //page.search();
            });
            // 快速打印
            $('#am_print').on('click', function () {
                var list = $('#girdtable').jfGridGet('rowdatas');
                if (list.length <= 0) {
                    ayma.alert.error("请先查出数据");
                    return;
                }
                ayma.layerForm({
                    id: 'GYSCHFLHZ',
                    title: '供应商存货分类汇总打印',
                    url: top.$.rootUrl + '/MesDev/MaterInBill/PrintReport3?report=GYSCHFLHZReport&data=GYSCHFLHZ&queryJson=' + encodeURIComponent(JSON.stringify(jsonquery)),
                    width: 1200,
                    height: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            $('#girdtable').jfGrid({});

           
            //var postData = {};
            //postData.queryJson = JSON.stringify({ code: "9527" });

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

            //    $('#girdtable').jfGridSet('reload', { param: postData });
            //});
        },
        search: function(param) {
            param = param || {};
            jsonquery = param;
            param["StartTime"] = param["StartTime"] + " 00:00:00";
            param["EndTime"] = param["EndTime"] + " 23:59:59";
            var postData = {};
            postData.queryJson = JSON.stringify(param);
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
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
