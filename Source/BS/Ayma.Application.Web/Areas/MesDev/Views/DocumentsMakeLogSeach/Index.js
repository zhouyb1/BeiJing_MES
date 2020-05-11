/* * 创建人：超级管理员
 * 日  期：2019-01-08 17:51
 * 描  述：操作记录查询
 */
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {

            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 450);
            $('#F_StockName').select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            });
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/DocumentsMakeLogSeach/GetPageList',
                headData: [
                    { label: "仓库编号", name: "F_StockCode", width: 160, align: "left"},
                    { label: "仓库名称", name: "F_StockName", width: 160, align: "left"},
                    { label: "单据类型", name: "F_BillType", width: 160, align: "left"},
                    { label: "操作类型", name: "F_OperationType", width: 160, align: "left"},
                    { label: "操作单号", name: "F_OrderNo", width: 160, align: "left"},
                    { label: "备注", name: "F_Remark", width: 160, align: "left"},
                    { label: "创建时间", name: "F_CreateDate", width: 160, align: "left"},
                    { label: "创建人", name: "F_CreateUserName", width: 160, align: "left"},
                ],
                mainId:'F_Id',
                reloadSelected: true,
                isPage: true,
                sidx: "F_CreateDate",
                sord: 'DESC',
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = $("#StartTime").val();
            param.EndTime = $("#EndTime").val();
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
