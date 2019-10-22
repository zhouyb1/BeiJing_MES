//var goodsCode = request('GoodsCode');
var orderNo = request('orderNo');
var orderDate = request('orderDate');
var qty;
var acceptClick;
var G_Code="";
var bootstrap = function ($, ayma) {
    "user strict";
    var page = {

        init: function () {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initGrid();
        },
        bind: function () {
            $("#P_OrderNo").val(orderNo);
            $('#B_ParentID').select();
            //绑定商品
            $('#P_GoodsCode').select({
                url: top.$.rootUrl + '/MesDev/Tools/GetOrderGoodsList?orderNo=' + orderNo,
                type: 'default',
                value: 'P_GoodsCode',
                text: "P_GoodsName",
                maxHeight: 225,
                param: {},

            }).on('change', function() {
                var goodsCode = $(this).selectGet();
                $.get(top.$.rootUrl + '/MesDev/Tools/GetOrderGoodEntity?goodsCode=' + goodsCode, function(res) {
                    qty = res.data.P_Qty;
                    $('#B_Qty').val(qty);
                }, 'json');

                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/GetCode',
                    data: { goodsCode: $('#P_GoodsCode').selectGet() },
                    dataType: "json",
                    success: function(res) {
                        G_Code = res.data == null ? "" : res.data.G_Code;
                        $('#B_ParentID').selectRefresh({
                            url: top.$.rootUrl + '/MesDev/Tools/GetBomList?goodsCode='+G_Code,
                            type: 'default',
                            value: 'ID',
                            text: "B_FormulaName",
                            maxHeight: 225,
                            param: {}
                        });
                    }
                });
            });
            
            $('#B_ParentID').on('change', function () {
                var parentId = $(this).selectGet();
                page.initData(parentId, qty);
            });

            //绑定仓库和班组
            //绑定原料仓
            $("#C_StockCode").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockListByParam',
                // 访问数据接口参数
                param: { strWhere: "S_Kind =1" }
            }).on('change', function() {
                var fromStockName = $(this).selectGetText();
                $("#C_StockName").val(fromStockName);
            });
            //绑定目标仓
            $("#C_StockToCode").select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockListByParam',
                // 访问数据接口参数
                param: { strWhere: "S_Kind =4" }
            }).on('change', function() {
                var stockToName = $(this).selectGetText();
                $("#C_StockToName").val(stockToName);
            });
            //绑定班组
            $("#C_TeamCode").select({
                type: 'default',
                value: 'T_Code',
                text: 'T_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetTeamList',
                // 访问数据接口参数
                param: {}
            });
        },
        initGrid: function () {
            $('#girdtable').jfGrid({
                headData: [
                { label: "配方名称", name: "B_FormulaName", width: 100, align: "left" },
                { label: "配方编码", name: "B_FormulaCode", width: 100, align: "left" },
                { label: "工艺代码", name: "B_RecordCode", width: 90, align: "left" },
                { label: "物料编码", name: "B_GoodsCode", width: 100, align: "left" },
                { label: "物料名称", name: "B_GoodsName", width: 100, align: "left" },
                { label: '单位', name: 'B_Unit', width: 60, align: 'left' },
                { label: '标准数量', name: 'B_Qty', width: 70, align: 'left' },
                { label: '订单物料统计', name: 'B_Total', width: 90, align: 'left' },
                { label: '餐食编码', name: 'B_ErpCode', width: 90, align: 'left', hidden: true },
                { label: '供应商编码', name: 'G_SupplyCode', width: 90, align: 'left', hidden: true },
                { label: '供应商', name: 'B_ErpCode', width: 90, align: 'left'},
                { label: '价格', name: 'G_Price', width: 90, align: 'left' }
                //{ label: '班组编码', name: 'G_TeamCode', width: 90, align: 'left',hidden:true },
                //{ label: '班组', name: 'G_TeamName', width: 90, align: 'left' }

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
        initData: function (parentId, qty) {
            $.SetForm(top.$.rootUrl + '/MesDev/ProductOrderManager/GetBomTreeList?parentId=' + parentId + '&qty=' + qty, function (data) {
                $('#girdtable').jfGridSet('refreshdata', { rowdatas: data });
            });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        //var strJsonBomList = JSON.stringify($('#girdtable').jfGridGet('rowdata')[0].jfGrid_ChildRows);
        var dataSelect = $('#girdtable').jfGridGet('rowdatas');
        if (dataSelect.length==0) {
            ayma.alert.error("请选择商品");
            return false;
        }
        if ($("#C_StockCode").selectGet() == $("#C_ToStockCode").selectGet()) {
            ayma.alert.warning("仓库相同");
            return false;
        }
        var str = [];
        //剔除父级数据
        for (var i = 0; i < dataSelect.length; i++) {

            if (str.indexOf(dataSelect[i].B_GoodsCode) == 1) {
                ayma.alert.error('同一编码的物料只选择一个！');
                return false;
            }
            str.push(dataSelect[i].B_GoodsCode);
            if (dataSelect[i].B_ParentID == "0") {
                dataSelect.splice(dataSelect.indexOf(dataSelect[i]), 1);
            }
        }
        //领料单所需数据
        var pickMaterData = JSON.stringify($('[data-table="Mes_CollarHead"]').GetFormData());
        $.SaveForm(top.$.rootUrl + '/MesDev/ProductOrderManager/SaveBomData', { strJsonBomList: JSON.stringify(dataSelect), orderNo: orderNo, orderDate: orderDate, strCollarHead: pickMaterData }, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();

}