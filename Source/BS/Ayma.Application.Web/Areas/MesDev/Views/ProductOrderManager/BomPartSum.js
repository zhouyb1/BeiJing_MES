
var goodsCode = request('GoodsCode');
var orderNo = request('orderNo');
var orderDate = request('orderDate');
var qty = request('qty');
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
            //绑定商品
            $('#P_GoodsCode').select({
                url: top.$.rootUrl + '/MesDev/Tools/GetOrderGoodsList?orderNo=' + orderNo,
                type: 'default',
                value: 'P_GoodsCode',
                text: "P_GoodsName",
                maxHeight: 225,
                param: {},

            });
            $('#B_Qty').val(qty == ""  ? 0 : parseInt(qty));
            $('#P_OrderNo').val(orderNo);
            $.post(top.$.rootUrl + '/MesDev/Tools/GetCode', { goodsCode: goodsCode }, function (res) {
                $('#B_ParentID').select({
                    url: top.$.rootUrl + '/MesDev/Tools/GetBomList',
                    type: 'default',
                    value: 'ID',
                    text: "B_FormulaName",
                    maxHeight: 225,
                    param: { goodsCode: res.data == null ? "" : res.data.G_Code }
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
                    { label: "配方名称", name: "B_FormulaName", width: 100, align: "left" },
                    { label: "配方编码", name: "B_FormulaCode", width: 100, align: "left" },
                    { label: "工艺代码", name: "B_RecordCode", width: 90, align: "left" },
                    { label: "物料编码", name: "B_GoodsCode", width: 100, align: "left" },
                    { label: "物料名称", name: "B_GoodsName", width: 100, align: "left" },
                    { label: '单位', name: 'B_Unit', width: 60, align: 'left' },
                    { label: '标准数量', name: 'B_Qty', width: 70, align: 'left' },
                    { label: '订单物料统计', name: 'B_Total', width: 90, align: 'left' }
                ],
                isTree: true,
                mainId: 'ID',
                parentId: 'B_ParentID',
                reloadSelected: true,
                isShowNum: true,
                footerrow: true,
                isMultiselect: true,
                height: 350
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
        //var strJsonBomList = JSON.stringify($('#girdtable').jfGridGet('rowdata')[0].jfGrid_ChildRows);
        var dataSelect = $('#girdtable').jfGridGet('rowdata');
        if (dataSelect==undefined) {
            ayma.alert.error("请选择商品");
            return false;
        }
        var str = [];
        //剔除父级数据
        for (var i = 0;i< dataSelect.length;  i++) {

            if (str.indexOf(dataSelect[i].B_GoodsCode)==1) {
                ayma.alert.error('同一编码的物料只选择一个！');
                return false;
            }
            str.push(dataSelect[i].B_GoodsCode);
            if (dataSelect[i].B_ParentID=="0") {
                dataSelect.splice(dataSelect.indexOf(dataSelect[i]), 1);
            }
        }
        $.SaveForm(top.$.rootUrl + '/MesDev/ProductOrderManager/SaveBomData', { strJsonBomList: JSON.stringify(dataSelect), orderNo: orderNo, orderDate: orderDate }, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
    
}