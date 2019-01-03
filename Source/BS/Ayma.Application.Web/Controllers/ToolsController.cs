using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.Excel;
using Ayma.Application.TwoDevelopment.Tools;
using Ayma.Util;

namespace Ayma.Application.Web.Controllers
{
    public class ToolsController : MvcControllerBase
    {
        private ExcelImportIBLL excelImportIBLL = new ExcelImportBLL();
        private AnnexesFileIBLL annexesFileIBLL = new AnnexesFileBLL();
        private ToolsIBLL toolsIBLL=new ToolsBLL();

        #region 导入

       
        #endregion

        #region 通用方法
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IsName(string tables,string names)
        {
            var isOk=toolsIBLL.IsName(tables, names);
            if (isOk)
            {
                return Fail("名称重复");
            }
            return Success(isOk);
        }
        #endregion
    }
}