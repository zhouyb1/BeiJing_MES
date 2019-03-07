/* * 创建人：超级管理员
 * 日  期：2019-03-06 17:41
 * 描  述：配方表
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#B_Avail').DataItemSelect({ code: 'YesOrNo' });
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
                param: {}
            }).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/GetProceEntityBy',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#B_ProNo").val(entity.P_ProNo);
                    }
                });
            });
            //$('#Mes_BomRecord').jfGrid({
            //    headData: [
            //        {
            //            label: 'ID', name: 'ID', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '配方编码', name: 'B_FormulaCode', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '物料编码', name: 'B_GoodsCode', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '物料名称', name: 'B_GoodsName', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '单位', name: 'B_Unit', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '数量', name: 'B_Qty', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '下级物料编码', name: 'B_SecGoodsCode', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '下级物料名称', name: 'B_SecGoodsName', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '下级物料数量', name: 'B_SecQty', width: 160, align: 'left',editType: 'input'
            //        },
            //        {
            //            label: '下级物料单位', name: 'B_SecUnit', width: 160, align: 'left',editType: 'input'
            //        },
            //    ],
            //    isAutoHeight: true,
            //    isEidt: true,
            //    footerrow: true,
            //    minheight: 400
            //});
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/BomHead/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            //$('#Mes_BomRecord').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            } else {
                $('#B_Avail').selectSet(1);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        //postData.strEntity = JSON.stringify($('[data-table="Mes_BomHead"]').GetFormData());
        //postData.strmes_BomRecordList = JSON.stringify($('#Mes_BomRecord').jfGridGet('rowdatas'));
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/BomHead/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
