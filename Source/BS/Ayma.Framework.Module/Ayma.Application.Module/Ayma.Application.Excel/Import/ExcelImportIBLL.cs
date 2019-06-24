using System;
using Ayma.Util;
using System.Collections.Generic;
using System.Data;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Application.Organization;

namespace Ayma.Application.Excel
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.04.01
    /// 描 述：Excel数据导入设置
    /// </summary>
    public interface ExcelImportIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件参数</param>
        /// <returns></returns>
        IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取导入配置列表根据模块ID
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        IEnumerable<ExcelImportEntity> GetList(string moduleId);
        /// <summary>
        /// 获取配置信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        ExcelImportEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取配置字段列表
        /// </summary>
        /// <param name="importId">配置信息主键</param>
        /// <returns></returns>
        IEnumerable<ExcelImportFieldEntity> GetFieldList(string importId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体数据</param>
        /// <param name="filedList">字段列表</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, ExcelImportEntity entity, List<ExcelImportFieldEntity> filedList);
        /// <summary>
        /// 更新配置主表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void UpdateEntity(string keyValue, ExcelImportEntity entity);
        #endregion

        #region

        /// <summary>
        /// excel 数据导入（未导入数据写入缓存）
        /// </summary>
        /// <param name="templateId">导入模板主键</param>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <returns></returns>
        string ImportTable(string templateId, string fileId, DataTable dt);
        /// <summary>
        /// 数据导入(班次表导入)
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        string ImportClassTable(string fileId, DataTable dt, ref List<Mes_ClassEntity> listData);
        /// <summary>
        /// 数据导入(供应商导入)
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        string ImportSupplyTable(string fileId, DataTable dt, ref List<Mes_SupplyEntity> listData); 
        /// <summary>
        /// 数据导入(仓库表)
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        string ImportStockTable(string fileId, DataTable dt, ref List<Mes_StockEntity> listData); 
        /// <summary>
        /// 数据导入(用户表)
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <param name="companyId">公司id</param>
        /// <returns></returns>
        string ImportUserTable(string fileId, DataTable dt, ref List<UserEntity> listData, string companyId);
        /// <summary>
        /// 数据导入(物料列表导入)
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        string ImportGoodsTable(string fileId, DataTable dt, ref List<Mes_GoodsEntity> listData);
        /// <summary>
        /// 数据导入(配方表导入)
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        string ImportBomRecordTable(string fileId, DataTable dt, ref List<Mes_BomRecordEntity> listData);

        /// <summary>
        /// 获取excel导入的错误数据
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <returns></returns>
        DataTable GetImportError(string fileId);
        #endregion
    }
}
