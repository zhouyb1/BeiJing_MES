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
    /// 日 期：2019-03-02 17:05
    /// 描 述：成品出库单制作
    /// </summary>
    public partial class ProOutMakeController : MvcControllerBase
    {
        private ProOutMakeIBLL proOutMakeIBLL = new ProOutMakeBLL();
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
        /// 出库单查询页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchIndex()
        {
             return View();
        }  
        /// <summary>
        /// 出库单查询表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchForm()
        {
             return View();
        } 
        /// <summary>
        /// 添加商品界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
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
            var data = proOutMakeIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_ProOutHeadData = proOutMakeIBLL.GetMes_ProOutHeadEntity( keyValue );
            var Mes_ProOutDetailData = proOutMakeIBLL.GetMes_ProOutDetailList( Mes_ProOutHeadData.P_ProOutNo );
            var jsonData = new {
                Mes_ProOutHeadData = Mes_ProOutHeadData,
                Mes_ProOutDetailData = Mes_ProOutDetailData,
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
            proOutMakeIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_ProOutDetailList)
        {
            Mes_ProOutHeadEntity entity = strEntity.ToObject<Mes_ProOutHeadEntity>();
            List<Mes_ProOutDetailEntity> mes_ProOutDetailList = strmes_ProOutDetailList.ToObject<List<Mes_ProOutDetailEntity>>();
            if (string.IsNullOrEmpty(keyValue))
            {
                var codeRulebll = new CodeRuleBLL();
                if (toolsIBLL.IsOrderNo("Mes_ProOutHead", "P_ProOutNo", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.ProOut).ToString())))
                {
                    //若重复 先占用再赋值
                    codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.ProOut).ToString()); //标志已使用
                    entity.P_ProOutNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.ProOut).ToString());
                }
                else
                {
                    entity.P_ProOutNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.ProOut).ToString());
                }
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.ProOut).ToString()); //标志已使用
            }
            proOutMakeIBLL.SaveEntity(keyValue,entity,mes_ProOutDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
