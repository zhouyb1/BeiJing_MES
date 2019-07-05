using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    public class GoodsController : Controller
    {
        /// <summary>
        /// 扫码获取商品的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsInfo()
        {
            return View();
        }
    }
}