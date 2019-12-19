/* * 创建人：超级管理员
 * 日  期：2019-11-06 16:30
 * 描  述：客户表
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
            $('#S_Effect1').Uploader();
            $('#S_Effect2').Uploader();
            $('#S_Effect3').Uploader();
            $('#S_Effect4').Uploader();
            $('#S_Effect5').Uploader();
            //客户名称验证
            //$("#C_Name").on('blur', function () {
            //    var code = $.trim($(this).val()); //去除空格
            //    var html = '<div class="am-field-error-info" id="isCode" title="该客户名称已存在！"><i class="fa fa-info-circle"></i></div>';
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/IsCode',
            //        data: { tables: "Mes_Customer", field: "C_Name", code: code, keyValue: keyValue },
            //        success: function (data) {
            //            var isOk = JSON.parse(data).data;
            //            if (isOk) {
            //                $("#C_Name").addClass("am-field-error");
            //                $("#C_Name").parent().append(html);
            //                ayma.alert.error("该客户名称已存在");
            //            } else {
            //                $("#C_Name").removeClass("am-field-error");
            //                $("#isCode").remove();
            //            }
            //        }
            //    });
            //});
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Custom/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                            $('#S_Effect1').UploaderSet(data[id].S_Effect1);
                            $('#S_Effect2').UploaderSet(data[id].S_Effect2);
                            $('#S_Effect3').UploaderSet(data[id].S_Effect3);
                            $('#S_Effect4').UploaderSet(data[id].S_Effect4);
                            $('#S_Effect5').UploaderSet(data[id].S_Effect5);
                        }
                    }
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        //电话号码验证
        var phone = $.trim($("#S_Telephone").val()); //去除空格
        if (phone != undefined && phone != "") {
            if (!/^(1[34578]\d{9}$)/.test(phone.toString()) && !/^(([0-9]{3,4}[-])?[0-9]{7,8}$)/.test(phone.toString())) {
                ayma.alert.error("请输入正确的手机号码或正确的座机号");
                return false;
            }
        }
        //纳税人识别号
        var tax = $.trim($("#S_TaxCode").val()); //去除空格  
        if (tax != undefined && tax != "") {
            if (checkTaxId(tax) == false) {
                ayma.alert.error("纳税人识别号格式错误");
                return false;
            }
        }
        var postData = $('#form').GetFormData();
        $.SaveForm(top.$.rootUrl + '/MesDev/Custom/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    function checkTaxId(taxId) {
        var regArr = [/^[\da-z]{10,15}$/i, /^\d{6}[\da-z]{10,12}$/i, /^[a-z]\d{6}[\da-z]{9,11}$/i, /^[a-z]{2}\d{6}[\da-z]{8,10}$/i, /^\d{14}[\dx][\da-z]{4,5}$/i, /^\d{17}[\dx][\da-z]{1,2}$/i, /^[a-z]\d{14}[\dx][\da-z]{3,4}$/i, /^[a-z]\d{17}[\dx][\da-z]{0,1}$/i, /^[\d]{6}[\da-z]{13,14}$/i],
            i, j = regArr.length;
        for (var i = 0; i < j; i++) {
            if (regArr[i].test(taxId)) {
                return true;
            }
        }
        //纳税人识别号格式错误
        return false;
    }
    page.init();
}
