/* * 创建人：超级管理员
 * 日  期：2019-03-11 19:22
 * 描  述：领料单
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var jsonquery = {};
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {

            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 200, 350);

            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });

            $('#GoodsCode').select({
                type: 'default',
                value: 'G_Code',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址 
                url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
                // 访问数据接口参数
                param: {}
            });
            $('#am_export').on('click', function () {
                var list = $('#girdtable').jfGridGet('rowdatas');
                if (list.length<=0) {
                    ayma.alert.error("请先查出数据");
                    return;
                }
                var url = top.$.rootUrl + '/MesDev/PickingMater/Export?queryJson=' + JSON.stringify(jsonquery);
                    window.location.href = url;               
            });
            // 快速打印
            $('#am_print').on('click', function () {
                ayma.layerForm({
                    id: 'CCLBB',
                    title: '出成率报表-按原物料打印',
                    url: top.$.rootUrl + '/MesDev/PickingMater/PrintReport4?report=CCLBBReport&data=CCLBB&queryJson=' + encodeURIComponent(JSON.stringify(jsonquery)),
                    width: 1200,
                    height: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            $('#girdtable').jfGrid({});

        },

        search: function (param) {
            param = param || {};

            if (param.GoodsCode == "" || param.StartTime == "" || param.EndTime == "")
            {
                ayma.alert.error("请先选择条件");
                return;
            }
            jsonquery = param;

            var postData = {};
            postData.queryJson = JSON.stringify(param);

            

            $.GetForm(top.$.rootUrl + '/MesDev/PickingMater/GetReportTitle', postData, function (res) {

                $('#girdtable').jfGridSet("unload", {
                    url: top.$.rootUrl + '/MesDev/PickingMater/GetReportData',
                    headData: res.data,
                    mainId: 'F_CreateDate',
                    reloadSelected: true,
                    isPage: false,
                    //sidx: "MClassName", //排序字段
                    //sord: "ASC", //排序方式
                    footerrow: false,
                    isStatistics: false
                });

                $('#girdtable').jfGridSet('reload', { param: postData });
            });
        },
    };

    refreshGirdData = function () {
        page.search();
    };

    page.init();
}

