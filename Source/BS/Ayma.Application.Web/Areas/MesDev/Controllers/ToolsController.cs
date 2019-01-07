using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    //公用控制器，执行公用方法
    public class ToolsController : MvcControllerBase
    {
        private ToolsIBLL toosIBLL=new ToolsBLL();

        #region 获取数据
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsName(string tables, string names)
        {
            var isName = toosIBLL.IsName(tables,names);
            return Success(isName);
        }
        /// <summary>
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsOrderNo(string tables, string orderNo)
        {
            var isOrderNo = toosIBLL.IsOrderNo(tables, orderNo);
            return Success(isOrderNo);
        }
        /// <summary>
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsCode(string tables, string code)
        {
            var isCode = toosIBLL.IsCode(tables, code);
            return Success(isCode);
        }
        #endregion
	}
}