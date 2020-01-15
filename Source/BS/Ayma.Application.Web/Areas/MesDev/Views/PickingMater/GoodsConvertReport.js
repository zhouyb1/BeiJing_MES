/* * 创建人：超级管理员
 * 日  期：2019-03-11 19:22
 * 描  述：领料单
 */
var refreshGirdData;
var jsonquery;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
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
                    mainId: 'MClassName',
                    reloadSelected: true,
                    isPage: false,
                    sidx: "MClassName", //排序字段
                    sord: "ASC", //排序方式
                    footerrow: false,
                    isStatistics: false
                });

                //$('#girdtable').jfGridSet('reload', { param: postData });
            });
        },
    };

    refreshGirdData = function () {
        page.search();
    };

    page.init();
}

