using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public interface BomHeadIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_BomHeadEntity> GetPageList(Pagination pagination, string queryJson); 
        /// <summary>
        /// 根据配方编码获取配方列表数据
        /// </summary>
        /// <param name="formulaCode">配方编码</param>
        /// <returns></returns>
        IEnumerable<Mes_BomRecordEntity> GetBomRecordListBy(string formulaCode);
        /// <summary>
        /// 根据主键获取配方表实体
        /// </summary>
        /// <param name="keyValue">配方表主键</param>
        /// <returns></returns>
        Mes_BomRecordEntity GetBomRecordEntity(string keyValue);        
        /// <summary>
        /// 获取配方列表数据
        /// </summary>
        /// <param name="queryJson">工艺代码、配方编码、配方名称、物料编码、物料名称</param>
        /// <returns></returns>
        IEnumerable<Mes_BomRecordEntity> GetBomRecordTreeList( string queryJson);
        /// <summary>
        /// 获取Mes_BomRecord表数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IEnumerable<Mes_BomRecordEntity> GetMes_BomRecordList(string keyValue);
        /// <summary>
        /// 获取Mes_BomHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BomHeadEntity GetMes_BomHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_BomRecord表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BomRecordEntity GetMes_BomRecordEntity(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除配方表数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void DeleteBomRecordForm(string keyValue); 
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, Mes_BomHeadEntity entity); 
        /// <summary>
        /// 保存配方表数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void SaveBomRecordForm(string keyValue, Mes_BomRecordEntity entity);
        #endregion

        #region 验证重复
        /// <summary>
        /// 根据父Id、工艺代码、配方编码、物料编码判断是否重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="parentId">父Id</param>
        /// <param name="recordCode">工艺代码</param>
        /// <param name="formulaCode">配方编码</param>
        /// <param name="goodsCode">物料编码</param>
        /// <returns></returns>
        bool ExistCode(string keyValue, string parentId,string recordCode, string formulaCode, string goodsCode); 
        #endregion
    }
}
