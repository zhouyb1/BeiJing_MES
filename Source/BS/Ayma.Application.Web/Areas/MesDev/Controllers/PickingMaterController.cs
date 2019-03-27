using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public partial class PickingMaterController : MvcControllerBase
    {
        private PickingMaterIBLL pickingMaterIBLL = new PickingMaterBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();


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
        /// 订单原物料需求列表
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderMaterList()
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
                ViewBag.RequireNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString());//自动获取主编码
            }
            return View();
        }

        /// <summary>
        /// 报表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintViewReport()
        {
            return View();
        }

        /// <summary>
        /// 报表页
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = pickingMaterIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_CollarHeadData = pickingMaterIBLL.GetMes_CollarHeadEntity( keyValue );
            var Mes_CollarDetailData = pickingMaterIBLL.GetMes_CollarDetailEntityList( Mes_CollarHeadData.C_CollarNo );
            var jsonData = new {
                Mes_CollarHeadData = Mes_CollarHeadData,
                Mes_CollarDetailData = Mes_CollarDetailData,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取订单原物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetOrderMaterList(string pagination, string queryJson, string keyword)
        {

            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =  pickingMaterIBLL.GetOrderMaterList(paginationobj,queryJson,keyword).ToList();
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
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
            pickingMaterIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_CollarDetailEntity)
        {
            Mes_CollarHeadEntity entity = strEntity.ToObject<Mes_CollarHeadEntity>();
            var mes_CollarDetailEntityList = strmes_CollarDetailEntity.ToObject<List<Mes_CollarDetailEntity>>();
            if (mes_CollarDetailEntityList.Any(c=>c.C_Qty<=0))
            {
                return Fail("数量只能是大于0的实数");
            }
            if (string.IsNullOrEmpty(keyValue))
            {
                var codeRulebll = new CodeRuleBLL();
                if (toolsIBLL.IsOrderNo("Mes_CollarHead", "C_CollarNo", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString())))
                {
                    //若重复 先占用再赋值
                    codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.MaterIn).ToString()); //标志已使用
                    entity.C_CollarNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString());
                }
                else
                {
                    entity.C_CollarNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString());
                }
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString()); //标志已使用
            }
            pickingMaterIBLL.SaveEntity(keyValue, entity, mes_CollarDetailEntityList);
            return Success("保存成功！");
        }
        #endregion

    }
}
