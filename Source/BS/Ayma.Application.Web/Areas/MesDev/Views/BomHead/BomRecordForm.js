/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.05
 * 描 述：配方管理	
 */
var parentId = request('parentId');
var selectedRow = top.BomRecordIndexSelectedRow();

var keyValue = '';
var acceptClick;
var bootstrap = function ($, ayma) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            //是否有效
            $('#B_Avail').DataItemSelect({ code: 'YesOrNo' });
            // 上级
            $('#B_ParentID').select({
                url: top.$.rootUrl + '/MesDev/Tools/GetBomRecordTree',
                type: 'tree',
                allowSearch: true,
                maxHeight: 225
            }).selectSet(parentId);
            $('#B_ProNo').select();//下拉初始化
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
            }).on('change', function () {
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
                //$.ajax({
                //    type: "get",
                //    url: top.$.rootUrl + '/MesDev/Tools/GetProceEntityBy',
                //    data: { code: code },
                //    success: function (data) {
                //        var entity = JSON.parse(data).data;
                //        $("#B_ProNo").val(entity.P_ProNo);
                //    }
                //});
            });
            //$('#F_ParentId').select({
            //    url: top.$.rootUrl + '/AM_SystemModule/DataItem/GetClassifyTree',
            //    type: 'tree',
            //    allowSearch: true,
            //    maxHeight:225
            //}).selectSet(goodsCode);
            /*检测重复项*/
            //$('#F_ItemName').on('blur', function () {
            //    $.ExistField(keyValue, 'F_ItemName', top.$.rootUrl + '/AM_SystemModule/DataItem/ExistItemName');
            //});
            //$('#F_ItemCode').on('blur', function () {
            //    $.ExistField(keyValue, 'F_ItemCode', top.$.rootUrl + '/AM_SystemModule/DataItem/ExistItemCode');
            //});
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.ID || '';
                $('#form').SetFormData(selectedRow);

            } else {
                $('#B_Avail').selectSet(1);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        var postData = $('#form').GetFormData(keyValue);
        if (postData["B_ParentID"] == '' || postData["B_ParentID"] == '&nbsp;') {
            postData["B_ParentID"] = '0';
        }
        else if (postData["B_ParentID"] == keyValue) {
            ayma.alert.error('上级不能是自己本身');
            return false;
        }
        $.SaveForm(top.$.rootUrl + '/MesDev/BomHead/SaveBomRecordForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };

    page.init();
}