using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 13:55
    /// 描 述：物料列表
    /// </summary>
    public partial class GoodsListController : MvcControllerBase
    {
        private GoodsListIBLL goodsListIBLL = new GoodsListBLL();
        private ToolsIBLL toosIBLL = new ToolsBLL();

        #region 视图功能

        /// <summary>
        /// 毛到净出成率
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport()
        {
             return View();
        }  
        /// <summary>
        /// 生到熟出成率
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LivingToCookIndex()
        {
             return View();
        }  
        /// <summary>
        /// 包装偏差率
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PackingRateIndex()
        {
             return View();
        } 
        /// <summary>
        /// 物料列表导入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImportForm()
        {
             return View();
        }
        /// <summary>
        /// 毛到净出成率
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult YieldRateIndex()
        {
             return View();
        }
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
        /// 获取毛到净出成率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetYieldRatePageList(string pagination, string queryJson)
        {
            var data = goodsListIBLL.GetYieldRatePageList(queryJson);
           
            return Success(data);
        } 
        /// <summary>
        /// 获取生到熟出成率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetLivingToCookPageList(string pagination, string queryJson)
        {
            var data = goodsListIBLL.GetLivingToCookPageList(queryJson);
           
            return Success(data);
        } 
        /// <summary>
        /// 获取包装偏差率出成率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPackingRatePageList(string pagination, string queryJson)
        {
            var data = goodsListIBLL.GetPackingRatePageList(queryJson);
           
            return Success(data);
        } 
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
            var data = goodsListIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_GoodsData = goodsListIBLL.GetMes_GoodsEntity( keyValue );
            var jsonData = new {
                Mes_GoodsData = Mes_GoodsData,
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
            goodsListIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            Mes_GoodsEntity entity = strEntity.ToObject<Mes_GoodsEntity>();
            var resCode = toosIBLL.IsCode("Mes_Goods", "G_Code", entity.G_Code, keyValue);
            if (resCode)
            {
                return Fail("该编码已存在！");
            }
            var resName = toosIBLL.IsName("Mes_Goods", "G_Name", entity.G_Name, keyValue);
            if (resName)
            {
                return Fail("该名称已存在！");
            }
            if (!string.IsNullOrEmpty(entity.G_Period.ToString()))
            {
                var reg = Regex.IsMatch(entity.G_Period.ToString(), @"^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$");
                if (!reg)
                {
                    return Fail("保质时间必须是非负数.");
                }
            }
            if (!string.IsNullOrEmpty(entity.G_Price.ToString()))
            {
                var reg = Regex.IsMatch(entity.G_Price.ToString(), @"^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$");
                if (!reg)
                {
                    return Fail("价格必须是非负数.");
                }
            }
            if (!string.IsNullOrEmpty(entity.G_Prepareday.ToString()))
            {
                var reg = Regex.IsMatch(entity.G_Prepareday.ToString(), @"^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$");
                if (!reg)
                {
                    return Fail("备用天数必须是非负数.");
                }
            }
            if (!string.IsNullOrEmpty(entity.G_Super.ToString()))
            {
                var reg = Regex.IsMatch(entity.G_Super.ToString(), @"^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$");
                if (!reg)
                {
                    return Fail("上限预警比例必须是非负数.");
                }
            }
            if (!string.IsNullOrEmpty(entity.G_Lower.ToString()))
            {
                var reg = Regex.IsMatch(entity.G_Lower.ToString(), @"^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$");
                if (!reg)
                {
                    return Fail("下限预警比例必须是非负数.");
                }
            }
            if (!string.IsNullOrEmpty(entity.G_UnitWeight.ToString()))
            {
                var reg = Regex.IsMatch(entity.G_UnitWeight.ToString(), @"^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$");
                if (!reg)
                {
                    return Fail("单位重量必须是非负数.");
                }
            }
            if (!string.IsNullOrEmpty(entity.G_Otax.ToString()))
            {
                var reg = Regex.IsMatch(entity.G_Otax.ToString(), @"^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$");
                if (!reg)
                {
                    return Fail("销售税率必须是非负数.");
                }
            }
            if (!string.IsNullOrEmpty(entity.G_Itax.ToString()))
            {
                var reg = Regex.IsMatch(entity.G_Itax.ToString(), @"^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$");
                if (!reg)
                {
                    return Fail("购进税率必须是非负数.");
                }
            }
            goodsListIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        #endregion

    }
}
