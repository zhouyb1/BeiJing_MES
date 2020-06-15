/* * 创建人：超级管理员
 * 日  期：2019-01-07 09:31
 * 描  述：供应商列表
 */
var acceptClick;
var keyValue = request('keyValue');
var type = request('type');
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

            if (type=="view") {
                $("#S_Name").attr('disabled', true);
                $("#S_EffectTime").attr('disabled', true);
                $("#S_Remark").attr('disabled', true);
            }
            $('#S_Effect1').Uploader({ func: "" });
            $('#S_Effect2').Uploader({ func: "" });
            $('#S_Effect3').Uploader({ func: "" });
            $('#S_Effect4').Uploader({ func: "" });
            $('#S_Effect5').Uploader({ func: "" });
            //编码重复验证
            $("#S_Code").on('blur', function () {
                var code = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isCode" title="编码重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/IsCode',
                    data: { tables: "Mes_Supply", field: "S_Code", code: code, keyValue: keyValue },
                    success: function (data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#S_Code").addClass("am-field-error");
                            $("#S_Code").parent().append(html);
                            ayma.alert.error("编码重复");
                        } else {
                            $("#S_Code").removeClass("am-field-error");
                            $("#isCode").remove();
                        }
                    }
                });
            });
            //名称重复验证
            $("#S_Name").on('blur', function () {
                var name = $.trim($(this).val()); //去除空格
                var html = '<div class="am-field-error-info" id="isName" title="名称重复！"><i class="fa fa-info-circle"></i></div>';
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl+'/MesDev/Tools/IsName',
                    data: { tables: "Mes_Supply", field: "S_Name", names: name, keyValue: keyValue },
                    success:function(data) {
                        var isOk = JSON.parse(data).data;
                        if (isOk) {
                            $("#S_Name").addClass("am-field-error");
                            $("#S_Name").parent().append(html);
                            ayma.alert.error("名称重复");
                        } else {
                            $("#S_Name").removeClass("am-field-error");
                            $("#isName").remove();
                        }
                    }
                });
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/SupplyList/GetFormData?keyValue=' + keyValue, function (data) {
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
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData())
        };
        //电话号码验证
        var phone = $.trim($("#S_Telephone").val()); //去除空格
        if (phone != undefined && phone != "") {
            if (!/^(1[34578]\d{9}$)/.test(phone.toString()) &&!/^(([0-9]{3,4}[-])?[0-9]{7,8}$)/.test(phone.toString())) {
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
        $.SaveForm(top.$.rootUrl + '/MesDev/SupplyList/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
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
}
