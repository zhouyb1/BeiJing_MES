﻿/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.18
 * 描 述：工作委托
 */
var acceptClick;
var bootstrap = function ($, ayma) {
    "use strict";

    var selectedRow = ayma.frameTab.currentIframe().selectedRow;

    var keyValue = '';
    var keyword = '';
    var schemeList = [];
    var schemeListSelected = {};

    var render = function () {
        var $warp = $('<div></div>');
        for (var i = 0, l = schemeList.length; i < l; i++) {
            var item = schemeList[i];
            var ponit = item;

            if (!!ponit) {
                if (keyword != '') {
                    if (ponit.F_Name.indexOf(keyword) == -1 && ponit.F_Code.indexOf(keyword) == -1) {
                        ponit = null;
                    }
                }
            }

            if (!!ponit) {// 刷新流程模板数据
                var _cardbox = "";

                var _active = "";
                if (!!schemeListSelected[item.F_Id]) {
                    _active = "active";
                }
                _cardbox += '<div class="card-box ' + _active + ' "  data-value="' + item.F_Id + '" >';
                _cardbox += '    <div class="card-box-img">';
                _cardbox += '        <img src="' + top.$.rootUrl + '/Content/images/filetype/Scheme.png" />';
                _cardbox += '    </div>';
                _cardbox += '    <div class="card-box-content">';
                _cardbox += '        <p>名称：' + item.F_Name + '</p>';
                _cardbox += '        <p>编号：' + item.F_Code + '</p>';
                _cardbox += '    </div>';
                _cardbox += '</div>';
                var $cardbox = $(_cardbox);
                $cardbox[0].shceme = item;
                $warp.append($cardbox);
            }
        }
        $warp.find('.card-box').on('click', function () {
            var $this = $(this);
            var value = $this.attr('data-value');
            if ($this.hasClass('active')) {
                $this.removeClass('active');
                delete schemeListSelected[value];
            }
            else {
                schemeListSelected[value] = $this[0].shceme;
                $this.addClass('active');
            }
        });

        $('#main_list').html($warp);
    }
    var isLoaded = false;

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 加载自定义流程列表
            ayma.httpAsync('GET', top.$.rootUrl + '/AM_WorkFlowModule/WfScheme/GetCustmerSchemeInfoList', {}, function (data) {
                schemeList = data;
                render();
                isLoaded = true;
            });

            $('#F_ToUserId').UserSelect(0);

            $("#txt_keyword").keydown(function (event) {
                if (event.keyCode == 13) {
                    keyword = $(this).val();
                    render();
                }
            });
            // 滚动条
            $('#main_list_warp').mCustomScrollbar({ // 优化滚动条
                theme: "minimal-dark"
            });
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_Id;
                $('.form-warp-top').SetFormData(selectedRow);
                ayma.httpAsync('GET', top.$.rootUrl + '/AM_WorkFlowModule/WfDelegateRule/GetRelationList', { keyValue: keyValue }, function (data) {
                    page.setSchemeData(data);
                });
            }
        },
        setSchemeData: function (_data) {
            if (isLoaded) {
                $.each(_data, function (id, item) {
                    $('.card-box[data-value="' + item.F_SchemeInfoId + '"]').trigger('click');
                });
            }
            else {
                setTimeout(function () {
                    page.setSchemeData(_data);
                }, 100);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('.form-warp-top').Validform()) {
            return false;
        }
        var formData = $('.form-warp-top').GetFormData(keyValue);
        formData.F_ToUserName = $('#F_ToUserId span').text();


        var schemeInfoList = [];
        for (var id in schemeListSelected) {
            schemeInfoList.push(id);
        }
        if (schemeInfoList.length == 0) {
            ayma.alert.warning('至少选择一个流程模板');
            return false;
        }
        $.SaveForm(top.$.rootUrl + '/AM_WorkFlowModule/WfDelegateRule/SaveForm?keyValue=' + keyValue, { 'strEntity': JSON.stringify(formData), 'strSchemeInfo': String(schemeInfoList) }, function (res) {
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}