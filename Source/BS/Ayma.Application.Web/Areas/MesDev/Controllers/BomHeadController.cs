using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public partial class BomHeadController : MvcControllerBase
    {
        private BomHeadIBLL bomHeadIBLL = new BomHeadBLL();

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
        /// 配方详情列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BomRecordIndex()
        {
            return View();
        }
        /// <summary>
        /// 配方详情表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BomRecordForm()
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
            var data = bomHeadIBLL.GetPageList(paginationobj, queryJson);
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
        /// 根据配方编码获取配方列表数据
        /// </summary>
        /// <param name="formulaCode">配方编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBomRecordListBy(string formulaCode)
        {
            var data = bomHeadIBLL.GetBomRecordListBy(formulaCode);
            return Success(data);
        }
        /// <summary>
        /// 配方树形列表
        /// </summary>
        /// <param name="queryJson">工艺代码、配方编码、配方名称、物料编码、物料名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBomRecordTreeList(string queryJson)
        {
            var data = bomHeadIBLL.GetBomRecordTreeList(queryJson);

            return Success(data);
        }
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var Mes_BomHeadData = bomHeadIBLL.GetMes_BomHeadEntity(keyValue);
            var Mes_BomRecordData = bomHeadIBLL.GetMes_BomRecordList(Mes_BomHeadData.B_FormulaCode);
            var jsonData = new
            {
                Mes_BomHeadData = Mes_BomHeadData,
                Mes_BomRecordData = Mes_BomRecordData,
            };
            return Success(jsonData);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除配方表数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteBomRecordForm(string keyValue)
        {
            bomHeadIBLL.DeleteBomRecordForm(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            bomHeadIBLL.DeleteEntity(keyValue);
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
            Mes_BomHeadEntity entity = strEntity.ToObject<Mes_BomHeadEntity>();
            //List<Mes_BomRecordEntity> mes_BomRecordList = strmes_BomRecordList.ToObject<List<Mes_BomRecordEntity>>();
            bomHeadIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存配方表数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveBomRecordForm(string keyValue, Mes_BomRecordEntity entity)
        {
            bomHeadIBLL.SaveBomRecordForm(keyValue, entity);
            if (!string.IsNullOrEmpty(entity.B_ParentID))
            {
                if (entity.B_ParentID != "0")
                {
                    var resCode = bomHeadIBLL.ExistCode(keyValue, entity.B_ParentID, entity.B_FormulaCode, entity.B_GoodsCode);
                    if (!resCode)
                    {
                        return Fail("该配方已存在！");
                    }
                }
            }

            return Success("保存成功！");
        }
        #endregion

    }
}
