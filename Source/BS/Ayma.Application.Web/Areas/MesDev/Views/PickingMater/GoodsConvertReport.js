/* * 创建人：廖水法
 * 日  期：2020-01-15
 * 描  述：出成率报表
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;

    var companyId;
    var convoysId;
    var trainsId;
    var teamId;
    var jsonquery = {};

    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
       
         
            var dfop = {
                type: 'tree',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + 'Mesdev/Tools/GetMaterialGoodsList',
                // 访问数据接口参数
                //param: { queryJson: JSON.stringify({ F_CompanyId: CurrentCompanyId }) },
            }
            $('#F_GoodsCode').select(dfop);
       

            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
                page.search(queryJson);
            });

            //// 导出
            //$('#am_export').on('click', function () {
            //    var url = top.$.rootUrl + '/ErpDev/SaleTrainnoRep/Export?queryJson=' + JSON.stringify(jsonquery);

            //    window.location.href = url;
            //});

            $('#girdtable').jfGrid({});
        },
        search: function (param) {
            param = param || {};
            jsonquery = param;

            var postData = {};
            postData.queryJson = JSON.stringify(param);



            $.GetForm(top.$.rootUrl + '/ErpDev/PickingMater/GetReportTitle', postData, function (res) {

                $('#girdtable').jfGridSet("unload", {
                    url: top.$.rootUrl + '/ErpDev/PickingMater/GetReportData',
                    headData: res.data,
                    mainId: 'MClassName',
                    reloadSelected: true,
                    isPage: false,
                    sidx: "MClassName", //排序字段
                    sord: "ASC", //排序方式
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
