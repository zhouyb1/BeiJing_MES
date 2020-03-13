using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 13:40
    /// 描 述：其它出库单
    /// </summary>
    public partial class Mes_OtherOutHeadController : MvcControllerBase
    {
        private Mes_OtherOutHeadIBLL mes_OtherOutHeadIBLL = new Mes_OtherOutHeadBLL();

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
        /// 打印页
        /// </summary>
        /// <returns></returns>
            [HttpGet]
        public ActionResult PrintReport()
        {
             return View();
        }
            /// <summary>
            /// 查询页
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            public ActionResult PostIndex()
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
        /// 物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
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
            var data = mes_OtherOutHeadIBLL.GetList(queryJson);
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
            var data = mes_OtherOutHeadIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取查询列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPostPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = mes_OtherOutHeadIBLL.GetPostPageList(paginationobj, queryJson);
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
        public ActionResult GetFormData(string keyValue,string state)
        {
            var mes_OtherOutHead = mes_OtherOutHeadIBLL.GetEntity(keyValue);
            var Mes_OtherOutDetailData = mes_OtherOutHeadIBLL.GetOtherOutDetailEntity(mes_OtherOutHead.O_OtherOutNo,state);
            var jsonData = new
            {
                mes_OtherOutHead = mes_OtherOutHead,
                Mes_OtherOutDetailData = Mes_OtherOutDetailData
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
            mes_OtherOutHeadIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_OtherOutDetailList)
        {
               if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = mes_OtherOutHeadIBLL.GetEntity(keyValue);
                if (entityTemp.O_Status == ErpEnums.OtherInStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
               Mes_OtherOutHeadEntity entity = strEntity.ToObject<Mes_OtherOutHeadEntity>();
               List<Mes_OtherOutDetailEntity> mes_OtherOutDetailList = strmes_OtherOutDetailList.ToObject<List<Mes_OtherOutDetailEntity>>();
               foreach (var item in mes_OtherOutDetailList)
               {
                   if (string.IsNullOrEmpty(item.O_Qty.ToString()) || item.O_Qty == 0)
                   {
                       return Fail("物料【" + item.O_GoodsName + "】入库数量不能为空!");
                   }
               }
              mes_OtherOutHeadIBLL.SaveEntity(keyValue, entity, mes_OtherOutDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
