/* * 创建人：超级管理员
 * 日  期：2019-12-10 15:07
 * 描  述：IP与RFID对应表
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            //编辑禁用
            if (keyValue)
            {
                $("#I_DoorName").attr('disabled', 'disabled');
            }
            //状态
            $("#I_Status").DataItemSelect({ code: 'IPToRFID' });
            //绑定门
            var dfop = {
                type: 'default',
                value: 'D_Name',
                text: 'D_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetDoorList',
                // 访问数据接口参数
                param: {}
            };
            //绑定门
            $('#I_DoorName').select(dfop).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetDoorEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#I_DoorCode").val(entity.D_Code);
                    }
                });
            });
            //IP
            //$("#I_IP").on('blur', function () {
            //    var IP = $.trim($(this).val()); //去除空格
            //    if (IP != undefined && IP != "") {
            //        if (!/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/.test(period.toString().replace('.', ''))) {
            //            ayma.alert.error("物料价格必须是非负数.");
            //            $("#P_InPrice").val(1);
            //        }
            //    }
            //});
        },
        initData: function () {
            if (!!selectedRow) {
                $('#form').SetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        //IP地址验证
        var IP = $.trim($("#I_IP").val()); //去除空格
        if (IP != undefined && IP != "") {
            if (!/(?=(\b|\D))(((\d{1,2})|(1\d{1,2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{1,2})|(2[0-4]\d)|(25[0-5]))(?=(\b|\D))/.test(IP.toString())) {
                ayma.alert.error("IP地址不正确.");
                return false;
            }
        }
        var postData = $('#form').GetFormData();
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_IPToRFID/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
