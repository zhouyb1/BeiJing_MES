/* * 创建人：超级管理员
 * 日  期：2019-08-06 10:54
 * 描  述：原物料入库价格表
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
            // 时间搜索框
            $('#datesearch').amdate({
                dfdata: [
                    { name: '今天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return ayma.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return ayma.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
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
               var rowdatas= $('#girdtable').jfGridGet('rowdata');
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
                        page.search();
                    } else {
                        ayma.alert.error(res.info);
                        page.search();
                    }
                });
            });
            // 删除
            $('#am_delete').on('click', function() {
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
                ayma.layerConfirm('是否确认删除该项！', function(res) {
                    if (res) {
                        ayma.deleteForm(top.$.rootUrl + '/MesDev/InPrice/DeleteForm', { strEntity:  dataList }, function () {
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
                    { label: "供应商编码", name: "P_SupplyCode", width: 160, align: "left"},
                    { label: "供应商名称", name: "P_SupplyName", width: 160, align: "left"},
                    { label: "物料编码", name: "P_GoodsCode", width: 160, align: "left"},
                    { label: "物料名称", name: "P_GoodsName", width: 160, align: "left"},
                    {
                        label: "供应商价格(不含税)", name: "P_InPrice", width: 160, align: "left", editType: 'input', editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.P_InPrice.toString().replace('.', ''))) { //验证只能为数字
                                    row.P_InPrice = 0;
                                }

                            }
                        },formatter: function() {
                            
                        }
                    },
                    {
                        label: "税率", name: "P_Itax", width: 160, align: "left", editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.P_Itax.toString().replace('.', ''))) { //验证只能为数字
                                    row.P_Itax = 0;
                                }

                            }
                        }
                    },
                    //{ label: "起始批次", name: "P_StartBatch", width: 160, align: "left" },
                    //{ label: "终止批次", name: "P_EndBatch", width: 160, align: "left" },
                    //{ label: "添加人", name: "P_CreateBy", width: 160, align: "left"},
                    //{ label: "添加时间", name: "P_CreateDate", width: 160, align: "left"},
                ],
                mainId:'ID',
                reloadSelected: true,
                isPage: true,
                isMultiselect:true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };


    page.init();
}
