
var goodsCode = request('GoodsCode');
var orderNo = request('orderNo');
var orderDate = request('orderDate');
var qty = parseInt(request('qty'));
var acceptClick;

var bootstrap = function ($, ayma) {
    "user strict";
    var page = {
       
        init: function () {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initGrid();
        },
        bind: function () {
            $('#B_Qty').val(qty);
            $('#orderNo').val();
            $('#orderDate').val();
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
                page.initData(parentId,qty);

            });
           
        },
        initGrid: function() {
            $('#girdtable').jfGrid({
                headData: [
                    { label: "配方名称", name: "B_FormulaName", width: 160, align: "left" },
                    { label: "配方编码", name: "B_FormulaCode", width: 160, align: "left" },
                    { label: "工艺代码", name: "B_RecordCode", width: 160, align: "left" },
                    { label: "物料编码", name: "B_GoodsCode", width: 160, align: "left" },
                    { label: "物料名称", name: "B_GoodsName", width: 160, align: "left" },
                    { label: "工序号", name: "B_ProNo", width: 160, align: "left" },
                    { label: '单位', name: 'B_Unit', width: 100, align: 'left' },
                    { label: '数量', name: 'B_Qty', width: 160, align: 'left' },
                    { label: '物料统计', name: 'B_Total', width: 160, align: 'left' }
                ],
                isTree: true,
                mainId: 'ID',
                parentId: 'B_ParentID',
                reloadSelected: true,
                isShowNum: true,
                footerrow: true,
                height: 300
            });
        },
        initData: function (parentId,qty) {
            $.SetForm(top.$.rootUrl + '/MesDev/ProductOrderManager/GetBomTreeList?parentId=' + parentId+'&qty='+qty, function (data) {
                $('#girdtable').jfGridSet('refreshdata', { rowdatas: data});
                
            });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var strJsonBomList = JSON.stringify($('#girdtable').jfGridGet('rowdatas')[0].jfGrid_ChildRows);
        $.SaveForm(top.$.rootUrl + '/MesDev/ProductOrderManager/SaveBomData', { strJsonBomList: strJsonBomList, orderNo: orderNo, orderDate: orderDate }, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
    
}