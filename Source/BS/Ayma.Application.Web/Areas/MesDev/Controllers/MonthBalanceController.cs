using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 14:02
    /// 描 述：财务月结
    /// </summary>
    public partial class MonthBalanceController : MvcControllerBase
    {
        private MonthBalanceIBLL monthBalanceIBLL = new MonthBalanceBLL();

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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = monthBalanceIBLL.GetPageList(paginationobj, queryJson);
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
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var Mes_MonthBalanceData = monthBalanceIBLL.GetMes_MonthBalanceEntity( keyValue );
            var jsonData = new {
                Mes_MonthBalanceData = Mes_MonthBalanceData,
            };
            return Success(jsonData);
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
            string msg = "";
            monthBalanceIBLL.DeleteEntity(keyValue,out msg);
            if (string.IsNullOrEmpty(msg))
                return Success("删除成功！");
            else
            {
                return Fail(msg);
            }
           
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            string msg = "";
            Mes_MonthBalanceEntity entity = strEntity.ToObject<Mes_MonthBalanceEntity>();
            monthBalanceIBLL.SaveEntity(keyValue,entity,out  msg);

            if (string.IsNullOrEmpty(msg))
              return Success("保存成功！");
            else
            {
                return Fail(msg);
            }

        }

        /// <summary>
        /// 月结、反月结
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult PostOrCancel(string month, int type)
        {
            string msg = "";
            monthBalanceIBLL.PostOrCancel(month,type, out msg);
            if (string.IsNullOrEmpty(msg))
                return Success("操作成功！");
            else
            {
                return Fail(msg);
            }
        }
        
        #endregion

    }
}
