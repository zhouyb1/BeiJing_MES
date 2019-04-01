using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-02 15:05
    /// 描 述：生成订单制作
    /// </summary>
    public partial class ProductOrderMakeController : MvcControllerBase
    {
        private ProductOrderMakeIBLL productOrderMakeIBLL = new ProductOrderMakeBLL();

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
        /// 生产订单查询页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchIndex()
        {
             return View();
        } 
        /// <summary>
        /// 生产订单查询表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchForm()
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
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
        {
            return View();
        }
        /// <summary>
        /// 生产订单报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取商品列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">商品编码/商品名称搜索</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string queryJson, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = productOrderMakeIBLL.GetGoodsList(paginationobj, queryJson, keyword);
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = productOrderMakeIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_ProductOrderHeadData = productOrderMakeIBLL.GetMes_ProductOrderHeadEntity( keyValue );
            var Mes_ProductOrderDetailData = productOrderMakeIBLL.GetMes_ProductOrderDetailList( Mes_ProductOrderHeadData.P_OrderNo );
            var jsonData = new {
                Mes_ProductOrderHeadData = Mes_ProductOrderHeadData,
                Mes_ProductOrderDetailData = Mes_ProductOrderDetailData,
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
            productOrderMakeIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_ProductOrderDetailList)
        {
            Mes_ProductOrderHeadEntity entity = strEntity.ToObject<Mes_ProductOrderHeadEntity>();
            List<Mes_ProductOrderDetailEntity> mes_ProductOrderDetailList = strmes_ProductOrderDetailList.ToObject<List<Mes_ProductOrderDetailEntity>>();
            productOrderMakeIBLL.SaveEntity(keyValue,entity,mes_ProductOrderDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
