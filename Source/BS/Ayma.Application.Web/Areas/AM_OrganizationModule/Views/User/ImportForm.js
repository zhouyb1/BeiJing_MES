﻿/*
 * 
 * 
 * 创建人：前端开发组
 * 日 期：2017.04.11
 * 描 述：导入Excel	
 */
var id = "userimport";
var companyId = request('companyId');
var keyVaule = '';
var bootstrap = function ($, ayma) {
    "use strict";

    var fileInfo = {};
  
    // 触发合并文件碎片
    var mergeFileChunks = function (file) {
        var param = {};
        param['__RequestVerificationToken'] = $.amToken;
        param['fileId'] = fileInfo[file.id].fileGuid;
        param['chunks'] = fileInfo[file.id].chunks;
        param['ext'] = file.ext;
        param['templateId'] = id;
        param['companyId'] = companyId;
        ayma.httpAsyncPost(top.$.rootUrl + "/Tools/ExecuteImportUserExcel", param, function (res) {
            var $fileItem = $('#am_form_file_queue_list').find('#am_filequeue_' + file.id);
            $fileItem.find('.am-uploader-progress').remove();
            if (res.code == ayma.httpCode.success) {
                if (res.data.Success != '0') {
                    ayma.alert.success('导入成功' + res.data.Success + '条');
                }
                // 文件保存成功后
                $fileItem.append('<div class="am-msg2"><span>' + res.data.Success + '</span><span>/</span><span style="color:#b94a48;" >' + res.data.Fail + '</span></div>');
                // 如果有失败
                if (res.data.Fail != '0') {
                    ayma.download({ url: top.$.rootUrl + '/AM_SystemModule/ExcelImport/DownImportErrorFile', param: { fileId: fileInfo[file.id].fileGuid, fileName: fileInfo[file.id].name, __RequestVerificationToken: $.amToken }, method: 'POST' });
                }
            }
            else {
                $fileItem.append('<div class="am-msg"><i class="fa fa-exclamation-circle"></i></div>');
            }
        });
    }
    // 触发清楚文件碎片
    var reomveFileChunks = function (file) {
        var param = {};
        param['__RequestVerificationToken'] = $.amToken;
        param['fileGuid'] = fileInfo[file.id].fileGuid;
        param['chunks'] = fileInfo[file.id].chunks;
        ayma.httpAsyncPost(top.$.rootUrl + "/AM_SystemModule/Annexes/MergeAnnexesFile", param, function (res) { });
        var $fileItem = $('#am_form_file_queue_list').find('#am_filequeue_' + file.id);
        $fileItem.find('.am-uploader-progress').remove();
        $fileItem.append('<div class="am-msg"><i class="fa fa-exclamation-circle"></i></div>');
    }

    var page = {
        uploader: null,
        init: function () {

            /*模板下载*/
            $('#am_down_file_btn').on('click', function () {
                ayma.download({ url: top.$.rootUrl + '/AM_SystemModule/ExcelImport/DownSchemeFile', param: { keyValue: id, __RequestVerificationToken: $.amToken }, method: 'POST' });
            });


            if (!WebUploader.Uploader.support()) {
                alert('Web Uploader 不支持您的浏览器！如果你使用的是IE浏览器，请尝试升级 flash 播放器');
                throw new Error('WebUploader does not support the browser you are using.');
            }

            page.uploader = WebUploader.create({
                auto: true,
                swf: top.$.rootUrl + '/Content/webuploader/Uploader.swf',
                // 文件接收服务端。
                server: top.$.rootUrl + "/AM_SystemModule/Annexes/UploadAnnexesFileChunk",
                // 选择文件的按钮。可选。
                // 内部根据当前运行是创建，可能是input元素，也可能是flash.
                pick: '#am_add_file_btn',
                dnd: '#am_form_file_queue',
                paste: 'document.body',
                disableGlobalDnd: true,
                accept: {
                    extensions: "xls,xlsx"
                },
                multiple: true,
                // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
                resize: false,
                // 文件分片上传
                chunked: true,
                chunkRetry: 3,
                prepareNextFile: true,
                chunkSize: '1048576',
                // 上传参数
                formData: {
                    __RequestVerificationToken: $.amToken
                }
            });
            page.uploader.on('fileQueued', page.fileQueued);
            page.uploader.on('uploadStart', page.uploadStart);
            page.uploader.on('uploadBeforeSend', page.uploadBeforeSend);
            page.uploader.on('uploadProgress', page.uploadProgress);
            page.uploader.on('uploadSuccess', page.uploadSuccess);
            page.uploader.on('uploadError', page.uploadError);
            page.uploader.on('uploadComplete', page.uploadComplete);
            page.uploader.on('error', page.error);

            $('#am_form_file_queue').mCustomScrollbar({ // 优化滚动条
                theme: "minimal-dark"
            });

        },
        fileQueued: function (file) {// 文件加载到队列
            fileInfo[file.id] = { name: file.name };
            $('#am_form_file_queue .am-form-file-queue-bg').hide();
            // 添加一条文件记录
            var $item = $('<div class="am-form-file-queue-item" id="am_filequeue_' + file.id + '" ></div>');
            $item.append('<div class="am-file-image"><img src="' + top.$.rootUrl + '/Content/images/filetype/' + file.ext + '.png"></div>');
            $item.append('<span class="am-file-name">' + file.name + '(' + ayma.countFileSize(file.size) + ')</span>');

            $('#am_form_file_queue_list').append($item);
        },
        uploadStart: function (file) {
            var $fileItem = $('#am_form_file_queue_list').find('#am_filequeue_' + file.id);
            $fileItem.append('<div class="am-uploader-progress"><div class="am-uploader-progress-bar" style="width:0%;"></div></div>');
        },
        uploadBeforeSend: function (object, data, headers) {
            data.chunk = data.chunk || 0;
            data.chunks = data.chunks || 1;
            fileInfo[data.id].fileGuid = fileInfo[data.id].fileGuid || WebUploader.Base.guid();
            data.fileGuid = fileInfo[data.id].fileGuid;
            fileInfo[data.id].chunks = data.chunks;
        },
        uploadProgress: function (file, percentage) {
            var $fileItem = $('#am_form_file_queue_list').find('#am_filequeue_' + file.id);
            $fileItem.find('.am-uploader-progress-bar').css('width', (percentage * 100 + '%'));
        },
        uploadSuccess: function (file, res) {
            if (res.code == 200) {// 上传成功
                mergeFileChunks(file);
            }
            else {// 上传失败
                reomveFileChunks(file);
            }
        },
        uploadError: function (file, code) {
            reomveFileChunks(file);
        },
        uploadComplete: function (file) {
        },
        error: function (type) {
            switch (type) {
                case 'Q_TYPE_DENIED':
                    ayma.alert.error('当前文件类型不允许上传');
                    break;
            };
        }
    };
    page.init();

}