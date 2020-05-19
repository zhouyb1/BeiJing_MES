/* * 创建人：超级管理员
 * 日  期：2019-08-06 10:54
 * 描  述：原物料入库价格表
 */
var refreshGirdData;
var $subgridTable;//子列表
var recordDel;//删除子列表
var recordEdit;//编辑子列表
var js_method;
var refreshSubGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            page.search();
        },
        bind: function () {          
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 250, 480);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/InPrice/Form',
                    width: 600,
                    height: 400,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            //物料名称
            $("#P_GoodsName").select({
                type: 'default',
                value: 'G_Name',
                text: 'G_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetMaterialGoodsList',
                // 访问数据接口参数
            });
            //供应商名称
            $("#P_SupplyName").select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetEffectSupplyList',
                // 访问数据接口参数
            });
            // 保存
            //$('#am_edit').on('click', function () {
            //    var keyValue = $('#girdtable').jfGridValue('ID');
            //    if (ayma.checkrow(keyValue)) {
            //        ayma.layerForm({
            //            id: 'form',
            //            title: '编辑',
            //            url: top.$.rootUrl + '/MesDev/InPrice/Form?keyValue=' + keyValue,
            //            width: 600,
            //            height: 400,
            //            maxmin: true,
            //            callBack: function (id) {
            //                return top[id].acceptClick(refreshGirdData);
            //            }
            //        });
            //    }
            //});

            $('#am_edit').on('click', function () {
                var arr = [];
                var rowdatas = $subgridTable.jfGridGet('rowdata');
               if (rowdatas==null || rowdatas.length==0) {
                   ayma.alert.warning('请勾选任意一行！');
                   return false;
               }
               else if (rowdatas.length == undefined) {
                    arr.push(rowdatas);
                } else {
                    arr = rowdatas;
                }
                var postData = {
                    strEntity: JSON.stringify(arr),
                    strEntity2: JSON.stringify(arr)
                };
                $.SaveIndex(top.$.rootUrl + '/MesDev/InPrice/Save', postData, function (res) {
                    // 保存成功后才回调
                    if (res.code == 200) {
                        refreshSubGirdData();
                    } else {
                        ayma.alert.error(res.info);
                        refreshSubGirdData();
                    }
                });
            });
            // 删除 多选删除子列表数据
            //$('#am_delete').on('click', function() {
            //    var dataList = [];
            //    var data = $subgridTable.jfGridGet('rowdata');
            //    if (data == null || data.length == 0) {
            //        ayma.alert.warning('请勾选任意一行！');
            //        return false;
            //    }
            //    else if (data.length == undefined) {
            //        dataList.push(data);
            //    }
            //    else {
            //        dataList = data;
            //    }
            //    ayma.layerConfirm('是否确认删除该项！', function(res) {
            //        if (res) {
            //            ayma.deleteForm(top.$.rootUrl + '/MesDev/InPrice/DeleteForm', { strEntity:  dataList }, function () {
            //                refreshGirdData();
            //            });
            //        }
            //    });

            //});
            // 删除 整个供应商
            $('#am_delete').on('click', function() {
                var dataFather = $("#girdtable").jfGridGet('rowdata');
                var keyValue = $("#girdtable").jfGridValue("P_SupplyCode");
                if (dataFather == null || dataFather.length == 0) {
                    ayma.alert.warning('请勾选任意一行！');
                    return false;
                }
                    ayma.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            ayma.deleteForm(top.$.rootUrl + '/MesDev/InPrice/DeleteEntity', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });

            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/InPrice/GetPageList',
                headData: [
                    { label: "供应商编码", name: "P_SupplyCode", width: 200, align: "left" },
                    { label: "供应商名称", name: "P_SupplyName", width: 200, align: "left" },
                    { label: "资质到期时间", name: "S_EffectTime", width: 200, align: "left" },
                ],
                mainId:'ID',
                reloadSelected: false,
                isPage: true,
                isSubGrid: true,
                subGridRowExpanded: function (subgridId, row) {
                    var P_SupplyCode = row.P_SupplyCode;
                    var subgridTableId = subgridId + "_t";
                    $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                    $subgridTable = $("#" + subgridTableId);
                    $subgridTable.jfGrid({
                        url: top.$.rootUrl + '/MesDev/InPrice/GetPriceBySupply?P_SupplyCode=' + P_SupplyCode + '&P_GoodsName=' + $("#P_GoodsName").selectGet(),
                        headData: [
                    { label: "ID", name: "ID", width: 160, align: "left", hidden: true },
                    { label: "供应商编码", name: "P_SupplyCode", width: 200, align: "left", hidden: true },
                    { label: "供应商名称", name: "P_SupplyName", width: 200, align: "left", hidden: true },
                    { label: "物料编码", name: "P_GoodsCode", width: 160, align: "left"},
                    { label: "物料名称", name: "P_GoodsName", width: 160, align: "left" },
                    { label: "开始时间", name: "P_StartDate", width: 160, align: "left", editType: 'dateinput' },
                    { label: "到期时间", name: "P_EndDate", width: 160, align: "left", editType: 'dateinput' },
                    {
                        label: "供应商价格(含税)", name: "P_TaxPrice", width: 160, align: "left" , editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.P_TaxPrice.toString().replace('.', ''))) { //验证只能为数字
                                    row.P_TaxPrice = 0;
                                }
                                if (row.P_TaxPrice == 0) { //验证只能为数字
                                    row.P_TaxPrice = "";
                                    ayma.alert.error("含税价格不能为0且只能为数字");
                                }
                            }
                        }
                    },
                    {
                        label: "购进税率(%)", name: "P_Itax", width: 160, align: "left"
                        , editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.P_Itax.toString().replace('.', ''))) { //验证只能为数字
                                    row.P_Itax =0;
                                }
                            }
                        }, formatter: function (value, row, dfop) {

                        }
                    },
                           {
                               label: "供应商价格(不含税)", name: "P_InPrice", width: 160, align: "left"
                                  , editType: 'label',
                               editOp: {
                                   callback: function (rownum, row) {
                                       if (/\D/.test(row.P_InPrice.toString().replace('.', ''))) { //验证只能为数字
                                           row.P_InPrice = 0;
                                       }
                                       if (row.P_InPrice ==0) { //验证只能为数字
                                           row.P_InPrice = "";
                                           ayma.alert.error("不含税价格不能为0且只能为数字");
                                       }
                                       if (row.P_InPrice > row.P_TaxPrice) { //验证只能为数字
                                           row.P_InPrice = row.P_TaxPrice;
                                           ayma.alert.error("不含税价格不能大于含税价格");
                                       }
                                   }
                               }
                           },
                       {
                           label: '操作', name: '', index: '', width: 120, align: 'left', frozen: true,
                           formatter: function (value, grid, rows) {
                               var result = "<a href=\"javascript:;\" style=\"color:#f60\" onclick=\"recordDel('" + grid.ID + "')\">删除</a>";
                               return result;
                           }
                       },
                         {
                             label: '操作', name: '', index: '', width: 120, align: 'left', frozen: true,hidden:true,
                             formatter: function (value, grid, rows) {
                                 var result = "<a href=\"javascript:;\" style=\"color:#f60\" onclick=\"recordEdit('" + grid.ID + "')\">编辑</a>";
                                 return result;
                             }
                         },
                        ],
                        onRenderComplete: function (rows) {
                            var lengh = rows.length;
                            var list = $('#girdtable').jfGridGet('rowdatas');
                            var data = [];
                            for (var j = 0; j < list.length; j++) {
                                for (var i = 0; i < lengh; i++) {
                                    $("[rownum='rownum_jfgrid_chlidgird_content_girdtable_rownum_girdtable_" + j + "_t_" + i + "'][colname='P_GoodsName']").html("<a href =# style=text-decoration:underline title='点击查询库存' onclick=js_method('" + rows[i].P_GoodsCode + "','9b04a0f2-28c0-4a58-973d-47bd51944a1c')>" + rows[i].P_GoodsName + "</ a>");
                                }
                            }       
                        },
                        mainId: 'ID',
                        isPage: true,
                        sidx: "P_GoodsCode",
                        sord: 'ASC',
                        isMultiselect:true,
                        reloadSelected: true,
                        inputCount:3
                    }).jfGridSet("reload");
                }
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    js_method = function (code, moduleId) {
        var module = top.ayma.clientdata.get(['modulesMap', moduleId]);
        module.F_UrlAddress = '/MesDev/InventorySeach/Index?goodsCode=' + encodeURIComponent(code);
        top.ayma.frameTab.openNew(module);

    }
    refreshGirdData = function () {
        page.search();
    };
    recordDel = function (keyValue) {
        ayma.layerConfirm('是否确认删除该项！', function (res) {
            if (res) {
                ayma.deleteForm(top.$.rootUrl + '/MesDev/InPrice/DeleteEntity', { keyValue: keyValue }, function () {
                    refreshGirdData();
                });
            }
        });
    }
    recordEdit = function (keyValue) {
        if (ayma.checkrow(keyValue)) {
            ayma.layerForm({
                id: 'form',
                title: '编辑原物料入库价格',
                url: top.$.rootUrl + '/MesDev/InPrice/Form?keyValue=' + keyValue,
                width: 700,
                height: 500,
                maxmin: true,
                callBack: function (id) {
                    return top[id].acceptClick(refreshSubGirdData);
                }
            });
        }

    }
    refreshSubGirdData = function () {
        $subgridTable.jfGridSet("reload");
    };
    page.init();
}
