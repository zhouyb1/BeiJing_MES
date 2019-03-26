/* * 创建人：超级管理员
 * 日  期：2019-03-12 17:32
 * 描  述：排班记录
 */
var acceptClick;
var keyValue = request('keyValue');
var tmp = new Map();
var parentFormId = request('formId');//上一级formId
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
        },
        bind: function () {
            var dfop = {
                type: 'default',
                value: 'W_Code',
                text: 'W_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetWorkShopList',
                // 访问数据接口参数
                param: {}
            }
            $('#A_WorkShopCode').select(dfop).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetWorkShopEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#A_WorkShopName").val(entity.W_Name);
                    }
                });
            });
            //是否有效
            $('#A_Avail').DataItemSelect({ code: 'YesOrNo' });
            $('#A_ProCode').select();//工序号下拉初始化
            //工艺代码
            $('#A_Record').select({
                type: 'default',
                value: 'R_Record',
                text: 'R_Record',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetRecordList',
                // 访问数据接口参数
                param: {}
            }).on('change', function () {
                var code = $(this).selectGet();
                $("#A_ProCode").selectRefresh({
                    type: 'default',
                    value: 'P_ProNo',
                    text: 'P_ProNo',
                    // 展开最大高度
                    maxHeight: 200,
                    // 是否允许搜索
                    allowSearch: true,
                    // 访问数据接口地址
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetProceEntity',
                    // 访问数据接口参数
                    param: { code: code }
                });
            });

            //用户编码
            $('#A_F_EnCode').select({
                type: 'default',
                value: 'F_EnCode',
                text: 'F_EnCode',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetUserList',
                // 访问数据接口参数
                param: {}
            });
            //班次
            $('#A_ClassCode').select({
                type: 'default',
                value: 'C_Code',
                text: 'C_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetClassList',
                // 访问数据接口参数
                param: {}
            });
            //生产订单号
            $('#A_OrderNo').select({
                type: 'default',
                value: 'P_OrderNo',
                text: 'P_OrderNo',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderList',
                // 访问数据接口参数
                param: {}
            });
            if (!keyValue) {
                $('#Mes_Arrange').jfGrid({
                    headData: [
                        {
                            label: '主键',
                            name: 'ID',
                            width: 160,
                            align: 'left',
                            editType: 'label',
                            hidden: 'true'
                        },
                        {
                            label: '日期',
                            name: 'A_Date',
                            width: 100,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '时间',
                            name: 'A_DateTime',
                            width: 100,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '生产订单号',
                            name: 'A_OrderNo',
                            width: 160,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '车间编码',
                            name: 'A_WorkShopCode',
                            width: 100,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '车间名称',
                            name: 'A_WorkShopName',
                            width: 100,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '用户编码',
                            name: 'A_F_EnCode',
                            width: 80,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '工艺代码',
                            name: 'A_Record',
                            width: 80,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '工序号',
                            name: 'A_ProCode',
                            width: 80,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '班次',
                            name: 'A_ClassCode',
                            width: 80,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '是否有效',
                            name: 'A_Avail',
                            width: 60,
                            align: 'left',
                            editType: 'label'
                        },
                        {
                            label: '备注',
                            name: 'A_Remark',
                            width: 160,
                            align: 'left',
                            editType: 'label'
                        },
                    ],
                    isAutoHeight: true,
                    isEidt: true,
                    isMultiselect: true,
                    footerrow: true,
                    minheight: 400
                });
            } else {
                $("#am_confirm").hide(); //隐藏确定按钮
            }
            // 确定
            $('#am_confirm').on('click', function() {
                var strEntity = $('body').GetFormData();
                if (!$('body').Validform()) {
                    return false;
                }
                var rows = $('#Mes_Arrange').jfGridGet('rowdatas');
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i]["A_Date"] == strEntity.A_Date && rows[i]["A_F_EnCode"] == strEntity.A_F_EnCode && rows[i]["A_ClassCode"] == strEntity.A_ClassCode) {
                        ayma.alert.error("同一个用户编码不能在同一班次出现");
                        return false;
                    }
                }
                if (!tmp.get(strEntity)) {
                    tmp.set(strEntity, 1);
                    rows.push(strEntity);
                }
                //数组过滤
                var filterarray = $.grep(rows, function (item) {
                    return item["A_OrderNo"] != undefined;
                }); page.search(filterarray);
            });
            //生产订单号校验
            //$('#A_OrderNo').on('blur', function () {
            //    var orderNo = $.trim($(this).val()); //生产订单号
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/IsOrderNo',
            //        data: { tables: "Mes_ProductOrderHead", field: "P_OrderNo", orderNo: orderNo },
            //        success: function (data) {
            //            var isOk = JSON.parse(data).data;
            //            if (!isOk) {
            //                ayma.alert.error("生产订单不存在");
            //                return false;
            //            }
            //        }
            //    });
            //});
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_Arrange/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    } 
                    $('#A_DateTime').val(data.time);  //给时间框赋值
                });
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_Arrange').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        var postData;
        if (!!keyValue) {
            postData = {
                strEntity: JSON.stringify($('body').GetFormData())
            };
        } else {
            postData = {
                strEntity: JSON.stringify($('#Mes_Arrange').jfGridGet('rowdatas'))
            };
            if (postData.strEntity == "[{}]") {
                ayma.alert.error("请添加排班信息!");
                return false;
            }
        }
        console.log(JSON.stringify(postData.strEntity));
        
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_Arrange/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };

    page.init();
}
