using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:55
    /// 描 述：工序管理
    /// </summary>
    public partial class ProceManagerController : MvcControllerBase
    {
        private ProceManagerIBLL proceManagerIBLL = new ProceManagerBLL();

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
            var data = proceManagerIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取页面显示树形列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTreeList(string queryJson)
        {
            var data = proceManagerIBLL.GetTreeList(queryJson);
            
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
            var Mes_ProceData = proceManagerIBLL.GetMes_ProceEntity( keyValue );
            var jsonData = new {
                Mes_ProceData = Mes_ProceData,
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
            proceManagerIBLL.DeleteEntity(keyValue);
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
            Mes_ProceEntity entity = strEntity.ToObject<Mes_ProceEntity>();
            var resCode=proceManagerIBLL.ExistRecordCode(keyValue, entity.P_RecordCode);
            if (!resCode)
            {
                return Fail("该工艺代码已存在！");
            }
            if (!string.IsNullOrEmpty(entity.P_ParentId))
            {
                var resProNo = proceManagerIBLL.ExistProNo(keyValue, entity.P_ParentId, entity.P_ProNo);
                if (!resProNo)
                {
                    return Fail("该工艺号已存在！");
                }
            }
            proceManagerIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 工艺代码不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="P_RecordCode">工艺代码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistRecordCode(string keyValue, string P_RecordCode)
        {
            bool res = proceManagerIBLL.ExistRecordCode(keyValue, P_RecordCode);
            return Success(res);
        }
        
        #endregion
    }
}
