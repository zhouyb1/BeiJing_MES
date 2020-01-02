using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using Ayma.Application.TwoDevelopment.MesDev.Mes_ProductOrderHead;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 17:54
    /// 描 述：查询生成清单
    /// </summary>
    public partial class Mes_ProductOrderHeadController : MvcControllerBase
    {
        private PickingMaterIBLL pickingMaterIBLL = new PickingMaterBLL();
        private Mes_ProductOrderHeadIBLL mes_ProductOrderHeadIBLL = new Mes_ProductOrderHeadBLL();

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

        /// <summary>
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FoodList()
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
            var data = mes_ProductOrderHeadIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_ProductOrderHeadData = mes_ProductOrderHeadIBLL.GetMes_ProductOrderHeadEntity( keyValue );
            var Mes_ProductOrderDetailData = mes_ProductOrderHeadIBLL.GetMes_ProductOrderDetailEntity( Mes_ProductOrderHeadData.P_OrderNo );
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
            mes_ProductOrderHeadIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_ProductOrderDetailEntity)
        {
            Mes_ProductOrderHeadEntity entity = strEntity.ToObject<Mes_ProductOrderHeadEntity>();
            Mes_ProductOrderDetailEntity mes_ProductOrderDetailEntity = strmes_ProductOrderDetailEntity.ToObject<Mes_ProductOrderDetailEntity>();
            mes_ProductOrderHeadIBLL.SaveEntity(keyValue,entity,mes_ProductOrderDetailEntity);
            return Success("保存成功！");
        }
        #endregion


        /// <summary>
        /// 获取ERP餐料清单
        /// </summary>
        /// <param name="useDate"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]

        public ActionResult GetErpFoodList(string useDate)
        {
            var timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var result= mes_ProductOrderHeadIBLL.GetErpFoodList( useDate,  timeStamp);
            var jsonData = new
            {
                ERPFoodList = result
            };
            return Success(jsonData); 
        }

        /// <summary>
        /// 保存ERP餐食清单
        /// </summary>
        /// <param name="ERPFoodListEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveERPFoodList(string ERPFoodListEntity)
        {
           List< ERPFoodListModel> foodEntity=  ERPFoodListEntity.ToObject<List<ERPFoodListModel>>();

            int msgCode=0;
            string msgInfo=string.Empty;
            mes_ProductOrderHeadIBLL.SaveERPFood(foodEntity, out msgCode, out msgInfo);
            if (100 == msgCode)
            {
             return   Success("保存成功!");
            }
            else {
              return  Fail(msgInfo);
            }
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult AutoCreateOrder(string date)
        {
            string message = "";
            bool datas = pickingMaterIBLL.AutoCreateOrder(date, out message);
            if(datas)
            {
                return Success("生成成功");
            }
            else
            {
                return Fail(message);
            }

             
        }
    }
}
