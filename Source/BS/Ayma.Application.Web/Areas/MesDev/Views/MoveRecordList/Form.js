/* * 创建人：超级管理员
 * 日  期：2019-01-07 15:11
 * 描  述：人员走动记录列表
 */
var acceptClick;
var keyValue = request('keyValue');
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
            //门列表
            var dfop = {
                type: 'default',
                value: 'D_Code',
                text: 'D_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetDoorList',
                // 访问数据接口参数
                param: {}
            }
            $("#M_DoorCode").select(dfop).on("change",function() { //根据门编码带出门名称
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetDoorEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#M_DoorName").val(entity.D_Name);
                    }
                });
            });
            //编码重复验证
            $("#M_UserCode").on('keyup', function () {
                var code = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isCode" title="编码重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsCode',
                    data: { tables: "Mes_MoveRecord", field: "M_UserCode", code: code },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#M_UserCode").addClass("am-field-error");
                            $("#M_UserCode").parent().append(html);
                            ayma.alert.error("编码重复");
                        } else {
                            $("#M_UserCode").removeClass("am-field-error");
                            $("#isCode").remove();
                        }
                    }
                });
            });
            //名称重复验证
            $("#M_UserName").on('keyup', function () {
                var name = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isName" title="名称重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsName',
                    data: { tables: "Mes_MoveRecord", field: "M_UserName", names: name },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#M_UserName").addClass("am-field-error");
                            $("#M_UserName").parent().append(html);
                            ayma.alert.error("名称重复");
                        } else {
                            $("#M_UserName").removeClass("am-field-error");
                            $("#isName").remove();
                        }
                    }
                });
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/MoveRecordList/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                    $("#M_Date").val(data.Mes_MoveRecordData.M_Date);
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/MoveRecordList/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
