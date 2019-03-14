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
        private RecordIBLL recordIbll = new RecordBLL();
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
        /// 工序表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProceForm()
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetRecordList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = recordIbll.GetPageList(paginationobj, queryJson);
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
        /// 根据工艺代码获取工序列表
        /// </summary>
        /// <param name="record">工艺代码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProceListBy(string record)
        {
            var data = proceManagerIBLL.GetProceListBy(record);

            return Success(data);
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
            var Mes_ProceData = proceManagerIBLL.GetMes_ProceEntity(keyValue);
            var jsonData = new
            {
                Mes_ProceData = Mes_ProceData,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取工艺代码表单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetRecordFormData(string keyValue)
        {
            var Mes_RecordData = recordIbll.GetMes_RecordEntity(keyValue);
            var jsonData = new
            {
                Mes_RecordData = Mes_RecordData,
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
        /// <param name="record">工艺代码</param>
        /// <param name="strEntity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string record, string strEntity)
        {
            Mes_ProceEntity entity = strEntity.ToObject<Mes_ProceEntity>();
            if (!string.IsNullOrEmpty(record) && string.IsNullOrEmpty(keyValue))
            {
                entity.P_RecordCode = record;
            }
            var resProNo=proceManagerIBLL.ExistProNo(keyValue,entity.P_RecordCode, entity.P_ProNo);
            if (!resProNo)
            {
                return Fail("该工序号已存在！");
            }
            proceManagerIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存工艺代码表（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveRecordForm(string keyValue, string strEntity)
        {
            Mes_RecordEntity entity = strEntity.ToObject<Mes_RecordEntity>();
            var resCode = proceManagerIBLL.ExistRecordCode(keyValue, entity.R_Record);
            if (!resCode)
            {
                return Fail("该工艺代码已存在！");
            }

            recordIbll.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 工艺代码不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="R_Record">工艺代码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistRecordCode(string keyValue, string R_Record)
        {
            bool res = proceManagerIBLL.ExistRecordCode(keyValue, R_Record);
            return Success(res);
        }


        #endregion
    }
}
