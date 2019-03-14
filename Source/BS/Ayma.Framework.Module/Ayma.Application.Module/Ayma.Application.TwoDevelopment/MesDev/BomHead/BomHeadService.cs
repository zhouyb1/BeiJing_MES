using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public partial class BomHeadService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_BomHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.B_RecordCode,
                t.B_ProNo,
                t.B_FormulaCode,
                t.B_FormulaName,
                t.B_GoodsCode,
                t.B_GoodsName,
                t.B_Avail,
                t.B_StartTime,
                t.B_EndTime,
                t.B_Unit
                ");
                strSql.Append("  FROM Mes_BomHead t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["B_FormulaCode"].IsEmpty())
                {
                    dp.Add("B_FormulaCode", "%" + queryParam["B_FormulaCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_FormulaCode Like @B_FormulaCode ");
                }
                if (!queryParam["B_GoodsCode"].IsEmpty())
                {
                    dp.Add("B_GoodsCode", "%" + queryParam["B_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_GoodsCode Like @B_GoodsCode ");
                }
                if (!queryParam["B_GoodsName"].IsEmpty())
                {
                    dp.Add("B_GoodsName", "%" + queryParam["B_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_GoodsName Like @B_GoodsName ");
                }
                if (!queryParam["B_FormulaName"].IsEmpty())
                {
                    dp.Add("B_FormulaName", "%" + queryParam["B_FormulaName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_FormulaName Like @B_FormulaName ");
                }
                return this.BaseRepository().FindList<Mes_BomHeadEntity>(strSql.ToString(),dp, pagination);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据配方编码获取配方列表数据
        /// </summary>
        /// <param name="formulaCode">配方编码</param>
        /// <returns></returns>
        public IEnumerable<Mes_BomRecordEntity> GetBomRecordListBy(string formulaCode)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_BomRecordEntity>(t => t.B_FormulaCode == formulaCode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据主键获取配方表实体
        /// </summary>
        /// <param name="keyValue">配方表主键</param>
        /// <returns></returns>
        public Mes_BomRecordEntity GetBomRecordEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BomRecordEntity>(t => t.ID == keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取配方列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_BomRecordEntity> GetBomRecordTreeList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                           t.[ID]
                          ,t.[B_RecordCode]
                          ,t.[B_FormulaCode]
                          ,t.[B_FormulaName]
                          ,t.[B_GoodsCode]
                          ,t.[B_GoodsName]
                          ,t.[B_Unit]
                          ,t.[B_Qty]
                          ,t.[B_ParentID]
                          ,t.[B_CreateBy]
                          ,t.[B_CreateDate]
                          ,t.[B_UpdateBy]
                          ,t.[B_UpdateDate]
                          ,t.[B_Avail]
                          ,t.[B_StartTime]
                          ,t.[B_EndTime]
                          ,t.[B_Remark]
                        ");
                strSql.Append("  FROM [dbo].[Mes_BomRecord] t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["B_RecordCode"].IsEmpty())
                {
                    dp.Add("B_RecordCode", queryParam["B_RecordCode"].ToString(), DbType.String);
                    strSql.Append(" AND t.B_RecordCode = @B_RecordCode ");
                }
                if (!queryParam["B_FormulaCode"].IsEmpty())
                {
                    dp.Add("B_FormulaCode", "%" + queryParam["B_FormulaCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_FormulaCode Like @B_FormulaCode ");
                }
                if (!queryParam["B_GoodsCode"].IsEmpty())
                {
                    dp.Add("B_GoodsCode", "%" + queryParam["B_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_GoodsCode Like @B_GoodsCode ");
                }
                if (!queryParam["B_GoodsName"].IsEmpty())
                {
                    dp.Add("B_GoodsName", "%" + queryParam["B_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_GoodsName Like @B_GoodsName ");
                }
                if (!queryParam["B_FormulaName"].IsEmpty())
                {
                    dp.Add("B_FormulaName", "%" + queryParam["B_FormulaName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_FormulaName Like @B_FormulaName ");
                }
                return this.BaseRepository().FindList<Mes_BomRecordEntity>(strSql.ToString(), dp);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取Mes_BomRecord表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_BomRecordEntity> GetMes_BomRecordList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_BomRecordEntity>(t=>t.B_FormulaCode == keyValue );
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取Mes_BomHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BomHeadEntity GetMes_BomHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BomHeadEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取Mes_BomRecord表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BomRecordEntity GetMes_BomRecordEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BomRecordEntity>(t=>t.B_FormulaCode == keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除配方表数据以及子级的数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteBomRecordForm(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<Mes_BomRecordEntity>(t => t.ID == keyValue||t.B_ParentID==keyValue);

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        } 
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                var mes_BomHeadEntity = GetMes_BomHeadEntity(keyValue); 
                db.Delete<Mes_BomHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_BomRecordEntity>(t=>t.B_FormulaCode == mes_BomHeadEntity.B_FormulaCode);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_BomHeadEntity entity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //var mes_BomHeadEntityTmp = GetMes_BomHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //db.Delete<Mes_BomRecordEntity>(t=>t.B_FormulaCode == mes_BomHeadEntityTmp.B_FormulaCode);
                    //foreach (Mes_BomRecordEntity item in mes_BomRecordList)
                    //{
                    //    item.Create();
                    //    item.B_FormulaCode = mes_BomHeadEntityTmp.B_FormulaCode;
                    //    db.Insert(item);
                    //}
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    var bomRecord=new Mes_BomRecordEntity()
                    {
                        B_FormulaCode = entity.B_FormulaCode,
                        
                        B_GoodsCode = entity.B_GoodsCode,
                        B_GoodsName = entity.B_GoodsName
                    };
                    bomRecord.Create();
                    db.Insert(bomRecord);
                    //foreach (Mes_BomRecordEntity item in mes_BomRecordList)
                    //{
                    //    item.Create();
                    //    item.B_FormulaCode = entity.B_FormulaCode;
                    //    db.Insert(item);
                    //}
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 保存配方表数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveBomRecordForm(string keyValue, Mes_BomRecordEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

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
        public bool ExistCode(string keyValue, string parentId, string recordCode, string formulaCode, string goodsCode)
        {
            try
            {
                var expression = LinqExtensions.True<Mes_BomRecordEntity>();
                expression = expression.And(t => t.B_FormulaCode.Trim().ToUpper() == formulaCode.Trim().ToUpper() && t.B_GoodsCode.Trim().ToUpper() == goodsCode.Trim().ToUpper() && t.B_RecordCode.Trim().ToUpper() == recordCode.Trim().ToUpper() && t.B_ParentID == parentId);

                if (!string.IsNullOrEmpty(keyValue))
                {
                    expression = expression.And(t => t.ID != keyValue);
                }
                return !this.BaseRepository().IQueryable(expression).Any();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        #endregion
    }
}
