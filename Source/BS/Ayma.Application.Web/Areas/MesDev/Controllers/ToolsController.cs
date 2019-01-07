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
        private ToolsIBLL toosIBLL = new ToolsBLL();

        #region 获取数据
        /// <summary>
        /// 根据主键获取供应商实体信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByIdGetSupplyEntity(string keyValue)
        {
            var supplyList = toosIBLL.ByIdGetSupplyEntity(keyValue);
            return Success(supplyList);
        }
        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSupplyList()
        {
            var supplyList = toosIBLL.GetSupplyList();
            return Success(supplyList);
        }
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsName(string tables,string field, string names)
        {
            var isName = toosIBLL.IsName(tables,field,names);
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
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsCode(string tables,string field, string code)
        {
            var isCode = toosIBLL.IsCode(tables,field, code);
            return Success(isCode);
        }
        #endregion
	}
}