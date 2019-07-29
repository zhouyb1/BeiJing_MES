using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 13:55
    /// 描 述：物料列表
    /// </summary>
    public partial class GoodsListService : RepositoryFactory
    {
        
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                      g.[ID]
                      ,g.[G_Code]
                      ,g.[G_Name]
                      ,g.[G_Kind]
                      ,g.[G_Period]
                      ,g.[G_Price]
                      ,g.[G_Unit]
                      ,g.[G_UnitWeight]
                      ,g.[G_Super]
                      ,g.[G_Lower]
                      ,g.[G_CreateBy]
                      ,g.[G_CreateDate]
                      ,g.[G_UpdateBy]
                      ,g.[G_UpdateDate]
                      ,g.[G_Remark]
                      ,g.[G_Erpcode]
                      ,g.[G_TKind]
                      ,g.[G_UnitQty]
                      ,g.[G_Unit2]
                      ,g.[G_Self]
                      ,g.[G_Online]
                      ,g.[G_Prepareday]
                      ,g.[G_Otax]
                      ,g.[G_Itax]
                      ,k.G_Name KindName
                ");
                strSql.Append("  FROM Mes_Goods g LEFT JOIN dbo.Mes_GoodKind k ON(g.G_TKind=k.G_Code)");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["G_Code"].IsEmpty())
                {
                    dp.Add("G_Code", "%" + queryParam["G_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND g.G_Code Like @G_Code ");
                }
                if (!queryParam["G_Name"].IsEmpty())
                {
                    dp.Add("G_Name", "%" + queryParam["G_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND g.G_Name Like @G_Name ");
                }
                if (!queryParam["G_Supply"].IsEmpty())
                {
                    dp.Add("G_Supply", "%" + queryParam["G_Supply"].ToString() + "%", DbType.String);
                    strSql.Append(" AND g.G_Supply Like @G_Supply ");
                }
                if (!queryParam["G_Kind"].IsEmpty())
                {
                    dp.Add("G_Kind", "%" + queryParam["G_Kind"].ToString() + "%", DbType.String);
                    strSql.Append(" AND g.G_Kind Like @G_Kind ");
                }
                return this.BaseRepository().FindTable(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Goods表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_GoodsEntity GetMes_GoodsEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_GoodsEntity>(keyValue);
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
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<Mes_GoodsEntity>(t=>t.ID == keyValue);
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
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_GoodsEntity entity)
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

    }
}
