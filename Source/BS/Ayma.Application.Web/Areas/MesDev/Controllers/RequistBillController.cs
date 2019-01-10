using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:20
    /// 描 述：调拨单制作
    /// </summary>
    public partial class RequistBillController : MvcControllerBase
    {
        private RequistBillIBLL requistBillIBLL = new RequistBillBLL();
        private ToolsIBLL toolsIBLL=new ToolsBLL();
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
            if (Request["keyValue"] == null)
            {
                ViewBag.OrderNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString());//自动获取主编码
            }
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
        /// 单据查询页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostIndex()
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
        /// <param name="stockCode">仓库编码</param>
        /// <param name="keyword">商品编码/商品名称搜索</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList(string pagination, string queryJson, string stockCode,string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = requistBillIBLL.GetList(paginationobj, queryJson,stockCode,keyword);
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
        /// 获取已提交单据列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPostPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = requistBillIBLL.GetPostPageList(paginationobj, queryJson);
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
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = requistBillIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_RequistHeadData = requistBillIBLL.GetMes_RequistHeadEntity(keyValue);
            var Mes_RequistDetailData = requistBillIBLL.GetMes_RequistDetailList(Mes_RequistHeadData.R_RequistNo);
            var jsonData = new
            {
                Mes_RequistHeadData = Mes_RequistHeadData,
                Mes_RequistDetailData = Mes_RequistDetailData,
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
            requistBillIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_RequistDetailList)
        {
            Mes_RequistHeadEntity entity = strEntity.ToObject<Mes_RequistHeadEntity>();
            List<Mes_RequistDetailEntity> mes_RequistDetailList = strmes_RequistDetailList.ToObject<List<Mes_RequistDetailEntity>>();
            if (string.IsNullOrEmpty(keyValue))
            {
                var codeRulebll = new CodeRuleBLL();
                if (toolsIBLL.IsOrderNo("Mes_RequistHead", "R_RequistNo", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString())))
                {
                    //若重复 先占用再赋值
                    codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString()); //标志已使用
                    entity.R_RequistNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString());
                }
                else
                {
                    entity.R_RequistNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString());
                }
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString()); //标志已使用
            }
            requistBillIBLL.SaveEntity(keyValue, entity, mes_RequistDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
