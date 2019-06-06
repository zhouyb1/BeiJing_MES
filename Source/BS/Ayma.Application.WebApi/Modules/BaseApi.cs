﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Cache.Base;
using Ayma.Loger;
using Ayma.Util;
using Ayma.Util.Operat;
using Nancy;
using System;
using Nancy.ModelBinding;
using Ayma.Cache.Factory;
using Senparc.Weixin.MP.TenPayLib;

namespace Ayma.Application.WebApi
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.05.12
    /// 描 述：Nancy-Api基础模块
    /// </summary>
    public class BaseApi : NancyModule
    {
        
        protected ICache redisCache = CacheFactory.CaChe();
        protected string cacheKeyOperator = "ayma_mes_operator_";// +登录者token
        protected string cacheKeyToken = "ayma_mes_token_";// +登录者token
        protected string cacheKeyError = "ayma_mes_error_";// + Mark

        #region 构造函数
        public BaseApi()
            : base()
        {
            Before += BeforeRequest;
            OnError += OnErroe;
        }
        public BaseApi(string baseUrl)
            : base(baseUrl)
        {
            Before += BeforeRequest;
            OnError += OnErroe;
        }
        #endregion

        #region 获取请求数据
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetReqData<T>() where T : class
        {
            try
            {
                ReqParameter<string> req = this.Bind<ReqParameter<string>>();
                return req.data.ToObject<T>();
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <returns></returns>
        public string GetReqData()
        {
            try
            {
                ReqParameter<string> req = this.Bind<ReqParameter<string>>();
                return req.data;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetReq<T>() where T : class
        {
            try
            {
                T req = this.Bind<T>();
                return req;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 响应接口
        /// <summary>
        /// 成功响应数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Response Success(string info)
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = info, data = new object { } };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        public Response Success(string info,object data)
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = info, data = data };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        /// <summary>
        /// 成功响应数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Response Success(object data)
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = "响应成功", data = data };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        /// <summary>
        /// 成功响应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public Response Success<T>(T data) where T : class
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = "响应成功", data = data };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        /// <summary>
        /// 成功响应数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Response SuccessString(string data)
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = "响应成功", data = data };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        /// <summary>
        /// 接口响应失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Response Fail(string info)
        {
            ResParameter res = new ResParameter { code = ResponseCode.fail, info = info, data = new object { } };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }

        /// <summary>
        /// 针对人脸识别
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Response SendSuccess(object data)
        {
            return Response.AsText(data.ToJson()).WithContentType("application/json");
        }

        #endregion
        
        #region 权限验证
        protected OperatorResult currentOper;
        public UserInfo userInfo;
        public string loginMark;
        public string token = "";
        /// <summary>
        /// 前置拦截器
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private Response BeforeRequest(NancyContext ctx)
        {
            string path = ctx.ResolvedRoute.Description.Path;
            //验证登录状态
            ReqParameter<string> req = this.Bind<ReqParameter<string>>();
            loginMark = req.loginMark;
            token = req.token;
            var dicParam = new Dictionary<string, string>();
            var ignoreUrl = new List<string>()
            {
                "/ayma/adms/user/login",
                "/",
                "/bgimg",
                "/webapi/openoauth/onlogin",
                "/webapi/openoauth/registeruserinfo",
                "/webapi/openoauth/verificode",
                "/webapi/openoauth/sendphonecode",
                "/webapi/orders/cancelorder",
               "/webapi/test/getca",
               "/ayma/api/productorder/syncorder",
               "/api/facerecording/getuserinfo"
            };

            if (ignoreUrl.Contains(path.ToLower()))// 登录接口，默认页面接口不做权限验证处理
            {
                WriterInfaceLog(ctx, req);
                return null;
            }
            if (string.IsNullOrEmpty(req.token))
            {
                return Fail("缺少token");
            }
            if (string.IsNullOrEmpty(req.loginMark))
            {
                return Fail("缺少loginMark");
            }
        
            WriterInfaceLog(ctx, req);
            switch (currentOper.stateCode)
            {
                case -1:
                    return this.Fail("未找到登录信息");
                case 0:
                    return this.Fail("登录信息已过期");
                default:
                    break;
            }
            return null;
        }
        #endregion

        #region 异常抓取
        /// <summary>
        /// 日志对象实体
        /// </summary>
        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }
        /// <summary>
        /// 监听接口异常
        /// </summary>
        /// <param name="ctx">连接上下信息</param>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        private Response OnErroe(NancyContext ctx, Exception ex)
        {
            try
            {
                this.WriteLog(ctx, ex);
            }
            catch (Exception)
            {
            }
            string msg = "异常信息：" + ex.Message;
            return Response.AsText(new ResParameter { code = ResponseCode.exception, info = msg }.ToJson()).WithContentType("application/json").WithStatusCode(HttpStatusCode.OK);
        }
        /// <summary>
        /// 写入日志（log4net）
        /// </summary>
        /// <param name="context">提供使用</param>
        public void WriteLog(NancyContext context, Exception ex)
        {
            if (context == null)
                return;
            string path = context.ResolvedRoute.Description.Path;
            var log = LogFactory.GetLogger("webapi");
            Exception Error = ex;
            LogMessage logMessage = new LogMessage();
            logMessage.OperationTime = DateTime.Now;
            logMessage.Url = path;
            logMessage.Class = "aymawebapi";
            logMessage.Ip = Net.Ip;
            logMessage.Host = Net.Host;
            logMessage.Browser = Net.Browser;
            if (userInfo != null)
            {
                logMessage.UserName = userInfo.realName;
            }

            if (Error.InnerException == null)
            {
                logMessage.ExceptionInfo = Error.Message;
            }
            else
            {
                logMessage.ExceptionInfo = Error.InnerException.Message;
            }
            logMessage.ExceptionSource = Error.Source;
            logMessage.ExceptionRemark = Error.StackTrace;
            string strMessage = new LogFormat().ExceptionFormat(logMessage);
            log.Error(strMessage);

            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 4;
            logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
            logEntity.F_OperateAccount = logMessage.UserName;
            if (userInfo != null)
            {
                logEntity.F_OperateUserId = userInfo.userId;
            }
            logEntity.F_ExecuteResult = -1;
            logEntity.F_ExecuteResultJson = strMessage;
            logEntity.WriteLog();
            //SendMail(strMessage);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="body">消息</param>
        private void SendMail(string body)
        {
            bool ErrorToMail = Config.GetValue("ErrorToMail").ToBool();
            if (ErrorToMail)
            {
                string SystemName = Config.GetValue("SystemName");//系统名称
                string recMail = Config.GetValue("RereceiveErrorMail");//接收错误信息邮箱
                MailHelper.Send("receivebug@ayma.cn", SystemName + " - 发生异常", body.Replace("-", ""));
            }
        }
        #endregion

        /// <summary>
        /// 添加接口访问记录
        /// </summary>
        public void WriterInfaceLog(NancyContext context ,ReqParameter<string>req)
        {
            try
            {
                var logMessage = new LogMessage() { OperationTime = DateTime.Now };
                logMessage.Url = context.Request.Url.Path;
                logMessage.Class = context.NegotiationContext.ModuleName;
                logMessage.Ip = Net.Ip;
                logMessage.Content = req.ToJson();
                var message = new LogFormat().InfoFormat(logMessage);
                Logger.Info(message);
            }
            catch (Exception ex)
            {
                WriteLog(context,ex);
            }
        }
        /// <summary>
        /// 添加接口访问记录
        /// </summary>
        public void WriterInfaceLog(NancyContext context,object req)
        {
            try
            {
                var logMessage = new LogMessage() { OperationTime = DateTime.Now };
                logMessage.Url = context.Request.Url.Path;
                logMessage.Class = context.NegotiationContext.ModuleName;
                logMessage.Ip = Net.Ip;
                logMessage.Content = req.ToJson();
                var message = new LogFormat().InfoFormat(logMessage);
                Logger.Info(message);
            }
            catch (Exception ex)
            {
                WriteLog(context, ex);
            }
        }
    }
}