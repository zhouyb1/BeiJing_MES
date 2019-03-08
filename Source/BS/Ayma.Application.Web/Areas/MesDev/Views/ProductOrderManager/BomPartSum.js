var b_parentId = request('');
var g_code = requestStart('');
//var selectedRow = top.BomRecordIndexSelectedRow();

var keyValue = '';
var bootstrap = function ($, ayma) {
    "user strict";
    var page = {
        init: function() {
            page.bind();
            page.initGird();
        },
        bind: function() {
            $('#B_Avail').DataItemSelect({ code: 'YesOrNo' });
            $('#B_ParentID').select({
                url: top.$.rootUrl + '/MesDev/Tools/GetBomRecordTree',
                type: 'tree',
                allowSearch: true,
                maxHeight: 225
            }).selectSet(b_parentId);

            $('#B_ProNo').select(); //下拉初始化
            //工艺代码
            $("#B_RecordCode").select({
                type: 'default',
                value: 'P_RecordCode',
                text: 'P_RecordCode',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProceList',
                // 访问数据接口参数
                param: { parentId: "0" }
            }).on('change', function() {
                var code = $(this).selectGet();
                $("#B_ProNo").selectRefresh({
                    type: 'default',
                    value: 'P_ProNo',
                    text: 'P_ProNo',
                    // 展开最大高度
                    maxHeight: 200,
                    // 是否允许搜索
                    allowSearch: true,
                    // 访问数据接口地址
                    url: top.$.rootUrl + '/MesDev/Tools/GetProceListBy',
                    // 访问数据接口参数
                    param: { code: code }
                });
            });
        },
        //initData: function() {
        //    if (!!selectedRow) {
        //        keyValue = selectedRow.ID || '';
        //        $('#form').SetFormData(selectedRow);

        //    } else {
        //        $('#B_Avail').selectSet(1);
        //    }
        //},

        initGird: function() {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/ProductOrderManager/GetBomRecordTreeList'
            });
        }
    };
    //top.BomRecordIndexSelectedRow = function () {
    //    return selectedRow;
    //}
}