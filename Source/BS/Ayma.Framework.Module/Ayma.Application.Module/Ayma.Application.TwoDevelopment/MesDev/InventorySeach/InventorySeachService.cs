using System.Linq;
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
    /// 日 期：2019-01-08 17:17
    /// 描 述：库存查询
    /// </summary>
    public partial class InventorySeachService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.I_StockCode,
                t.I_StockName,
                t.I_GoodsCode,
                t.I_GoodsName,
                t.I_Unit,
                t.I_Qty,
                t.I_Batch,
                t.I_Remark
                ");
                strSql.Append("  FROM Mes_Inventory t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["I_StockCode"].IsEmpty())
                {
                    dp.Add("I_StockCode", "%" + queryParam["I_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_StockCode Like @I_StockCode ");
                }
                if (!queryParam["I_StockName"].IsEmpty())
                {
                    dp.Add("I_StockName", "%" + queryParam["I_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_StockName Like @I_StockName ");
                }
                if (!queryParam["I_GoodsCode"].IsEmpty())
                {
                    dp.Add("I_GoodsCode", "%" + queryParam["I_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_GoodsCode Like @I_GoodsCode ");
                }
                if (!queryParam["I_GoodsName"].IsEmpty())
                {
                    dp.Add("I_GoodsName", "%" + queryParam["I_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_GoodsName Like @I_GoodsName ");
                }
                if (!queryParam["I_Batch"].IsEmpty())
                {
                    dp.Add("I_Batch", "%" + queryParam["I_Batch"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_Batch Like @I_Batch ");
                }
                return this.BaseRepository().FindList<Mes_InventoryEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Inventory表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InventoryEntity GetMes_InventoryEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_InventoryEntity>(keyValue);
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
        /// 获取Mes_Inventory表实体数据 根据商品编码和仓库编码以及批次
        /// </summary>
        /// <param name="goodsCode">商品编码</param>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="batch">批次</param>
        /// <returns></returns>
        public Mes_InventoryEntity GetEntityBy(string goodsCode, string stockCode, string batch)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_InventoryEntity>(c=>c.I_GoodsCode==goodsCode&&c.I_StockCode==stockCode&&c.I_Batch==batch);
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
        ///根据goodsCode、批次获取Entity
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        public Mes_InventoryEntity GetListByParams(string goodsCode,string batch)
        {
            try
            {
                return
                    this.BaseRepository()
                        .FindEntity<Mes_InventoryEntity>(c => c.I_GoodsCode == goodsCode && c.I_Batch == batch);
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
                this.BaseRepository().Delete<Mes_InventoryEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_InventoryEntity entity)
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
