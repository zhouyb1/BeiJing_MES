/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.18
 * 描 述：账号添加	
 */
var companyId = request('companyId');


var acceptClick;
var keyValue = '';
var getImgName;
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            //状态
            $("#F_EnabledMark").select();
            // 部门
            $('#F_DepartmentId').DepartmentSelect({ companyId: companyId });
            //用户类型
            $('#F_Kind').DataItemSelect({ code: 'EmployeeKind' });
            //在职状态
            $('#F_Status').DataItemSelect({ code: 'JobStatus' });
            // 性别
            $('#F_Gender').select();
            ////照片
            $('#F_Picture1').Uploader({ func: getImgName });
            /*检测重复项*/
            $('#F_Account').on('blur', function () {
                $.ExistField(keyValue, 'F_Account', top.$.rootUrl + '/AM_OrganizationModule/User/ExistAccount');
            });
            ////角色的分类
            $("#R_Code").select({
                type: 'default',
                value: 'F_EnCode',
                text: 'F_FullName',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetRoleList',
                // 访问数据接口参数
                param: {}
            });
        },
        initData: function () {
            if (!!selectedRow) {
                console.log(selectedRow)
                keyValue = selectedRow.F_UserId;
                selectedRow.F_Password = "******";
                $('#form').SetFormData(selectedRow);
                $('#F_Account').attr('readonly', 'readonly');
            }
            else {
                $('#F_CompanyId').val(companyId);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').Validform()) {
            return false;
        }
        var postData = $('#form').GetFormData(keyValue);
        if (!keyValue) {
            postData.F_Password = $.md5(postData.F_Password);
        }
        $.SaveForm(top.$.rootUrl + '/AM_OrganizationModule/User/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    getImgName = function () {
        ayma.fileName = $("#F_Account").val();
    }
    page.init();
}