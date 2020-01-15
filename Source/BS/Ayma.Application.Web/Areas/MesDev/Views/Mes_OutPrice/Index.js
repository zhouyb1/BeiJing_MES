/* * 创建人：超级管理员
 * 日  期：2019-12-04 09:28
 * 描  述：原物料售卖价格表
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 250, 480);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //物料名称
            $("#O_GoodsName").select({
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
            // 新增
            $('#am_add').on('click', function () {
                selectedRow = null;
                ayma.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/MesDev/Mes_OutPrice/Form',
                    width: 700,
                    height: 400,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            $('#am_edit').on('click', function () {
                var arr = [];
                var rowdatas = $('#girdtable').jfGridGet('rowdata');
                if (rowdatas == null || rowdatas.length == 0) {
                    ayma.alert.warning('请勾选任意一行！');
                    return false;
                }
                else if (rowdatas.length == undefined) {
                    arr.push(rowdatas);
                } else {
                    arr = rowdatas;
                }
                var postData = {
                    strEntity: JSON.stringify(arr)
                };
                $.SaveIndex(top.$.rootUrl + '/MesDev/Mes_OutPrice/Save', postData, function (res) {
                    // 保存成功后才回调
                    if (res.code == 200) {
                        page.search();
                    } else {
                        ayma.alert.error(res.info);
                        page.search();
                    }
                });
            });
            // 删除
            $('#am_delete').on('click', function () {
                var dataList = [];
                var data = $('#girdtable').jfGridGet('rowdata');
                if (data == null || data.length == 0) {
                    ayma.alert.warning('请勾选任意一行！');
                    return false;
                }
                else if (data.length == undefined) {
                    dataList.push(data);
                }
                else {
                    dataList = data;
                }
                ayma.layerConfirm('是否确认删除该项！', function (res) {
                    if (res) {
                        ayma.deleteForm(top.$.rootUrl + '/MesDev/Mes_OutPrice/DeleteForm', { strEntity: dataList }, function () {
                            refreshGirdData();
                        });
                    }
                });
            });
        },
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_OutPrice/GetPageList',
                headData: [
                        { label: '物料编码', name: 'O_GoodsCode', width: 200, align: "left" },
                        { label: '物料名称', name: 'O_GoodsName', width: 200, align: "left" },
                        { label: '备注', name: 'O_Remark', width: 200, align: "left" },
                        {
                            label: '售卖价格', name: 'O_SalePrice', width: 200, align: "left",
                            editType: 'input', editOp: {
                                callback: function (rownum, row) {
                                    if (/\D/.test(row.O_SalePrice.toString().replace('.', ''))) { //验证只能为数字
                                        row.O_SalePrice = 0;
                                    }

                                }
                            }, formatter: function () {

                            }
                        },      
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                isMultiselect: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
