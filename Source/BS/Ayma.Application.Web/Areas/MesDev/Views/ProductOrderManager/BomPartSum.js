var parentId;
var goodsCode = request('GoodsCode');

var bootstrap = function ($, ayma) {
    "user strict";
    var page = {
       
        init: function () {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initGird();
        },
        bind: function () {
            $.post(top.$.rootUrl + '/MesDev/Tools/GetCode', { goodsCode: goodsCode }, function (res) {
                $('#B_ParentID').select({
                    url: top.$.rootUrl + '/MesDev/Tools/GetBomList',
                    type: 'default',
                    value: 'ID',
                    text: "B_FormulaName",
                    maxHeight: 225,
                    param: { goodsCode: res.data.G_Code }
                });

            }, 'json');
            $('#B_ParentID').on('change', function () {
                var parentId = $(this).selectGet();
                page.initGird();
                page.initData(parentId);

            });
        },
        
        initGird: function () {
            $('#girdtable').jfGrid({
                headData: [
                    { label: "工艺代码", name: "B_RecordCode", width: 160, align: "left" },
                    { label: "工序号", name: "B_ProNo", width: 160, align: "left" },
                    { label: "配方编码", name: "B_FormulaCode", width: 160, align: "left" },
                    { label: "配方名称", name: "B_FormulaName", width: 160, align: "left" },
                    { label: "物料编码", name: "B_GoodsCode", width: 160, align: "left" },
                    { label: "物料名称", name: "B_GoodsName", width: 160, align: "left" },
                    { label: '单位', name: 'B_Unit', width: 100, align: 'left' },
                    { label: '数量', name: 'B_Qty', width: 160, align: 'left' },
                ],
                isTree: true,
                mainId: 'ID',
                parentId: 'B_ParentID',
                reloadSelected: true,
                isShowNum: true
                //isAutoHeight: true,          // 自动适应表格高度
            });
        },
        initData: function (parentId) {
            $.SetForm(top.$.rootUrl + '/MesDev/ProductOrderManager/GetBomTreeList?parentId=' + parentId, function (data) {
                for (var id in data) {
                    debugger;
                    $('#girdtable').jfGridSet('refreshdata', { rowdatas: data[id]});

                }
            });
        },
        search: function (data) {
            data = data || {};
            $('#girdtable').jfGridSet('refreshdata', { rowdatas: data });
        }
        

    };
    page.init();
}