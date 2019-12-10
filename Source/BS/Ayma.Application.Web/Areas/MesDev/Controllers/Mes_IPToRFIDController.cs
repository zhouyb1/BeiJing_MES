using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.JScript;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-10 15:07
    /// 描 述：IP与RFID对应表
    /// </summary>
    public partial class Mes_IPToRFIDController : MvcControllerBase
    {
        private Mes_IPToRFIDIBLL mes_IPToRFIDIBLL = new Mes_IPToRFIDBLL();
        private ToolsIBLL Tools = new ToolsBLL();
        #region 视图功能

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
             return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList( string queryJson )
        {
            var data = mes_IPToRFIDIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = mes_IPToRFIDIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var data = mes_IPToRFIDIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            mes_IPToRFIDIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue,Mes_IPToRFIDEntity entity)
        {
            var IsCode = Tools.IsCode("MES_IPToRFID", "I_DoorCode", entity.I_DoorCode, "");
            if (IsCode == true)
            {
                return Fail("【" + entity.I_DoorName + "】已存在!");
            }
            var IsIP = Tools.IsCode("MES_IPToRFID", "I_IP", entity.I_IP, "");
            if (IsIP == true)
            {
                return Fail("【" + entity.I_IP + "】该IP地址已存在!");
            }
            mes_IPToRFIDIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        #endregion

    }
}
