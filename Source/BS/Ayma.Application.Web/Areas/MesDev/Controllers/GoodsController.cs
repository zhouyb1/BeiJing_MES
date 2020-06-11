using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayma.Util;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    [HandlerLogin(FilterMode.Ignore)]
    public class GoodsController : MvcControllerBase
    {
        /// <summary>
        /// 扫码获取商品的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsInfoIndex()
        {
            return View();
        }
    }
}