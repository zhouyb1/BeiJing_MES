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
    /// 日 期：2019-10-09 16:16
    /// 描 述：保质期
    /// </summary>
    public partial class PeriodService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public PeriodService()
        {
            fieldSql= @"
               (select
                m.ID,
                h.M_StockName,
		        m.M_GoodsName,
		        m.M_Unit,
		        m.M_Batch,
		        (select g.G_Period from Mes_Goods g where g.G_Code=m.M_GoodsCode) as G_Period,
		         datediff(day,h.M_CreateDate,getdate()) as InventoryDay,
                    case when  datediff(day,h.M_CreateDate,getdate()) >(select g.G_Period from Mes_Goods g where g.G_Code=m.M_GoodsCode) then '产品已过期'
                    when  datediff(day,h.M_CreateDate,getdate())<(select g.G_Period from Mes_Goods g where g.G_Code=m.M_GoodsCode) then '正常'
                    when datediff(day,h.M_CreateDate,getdate())=(select g.G_Period from Mes_Goods g where g.G_Code=m.M_GoodsCode) then '明天过期'
                    else '无' end as GoodsState
                    from Mes_MaterInDetail m left join   Mes_MaterInHead h on m.M_MaterInNo=h.M_MaterInNo) c where c.ID in 
                    (select min(ID) from (select
                    m.ID,
		            m.M_GoodsName,
		            m.M_Batch	
             from Mes_MaterInDetail m ) c group by c.M_Batch,c.M_GoodsName)
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInDetailEntity> GetList( string queryJson )
        {
            try
            {
                //参考写法
                //var queryParam = queryJson.ToJObject();
                // 虚拟参数
                //var dp = new DynamicParameters(new { });
                //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                var strSql = new StringBuilder();
                strSql.Append("SELECT * from");
                strSql.Append(fieldSql);
                strSql.Append(" and 1=1");
                return this.BaseRepository().FindList<Mes_MaterInDetailEntity>(strSql.ToString());
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
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInDetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * from");
                strSql.Append(fieldSql);
                strSql.Append(" and 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND M_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["M_StockName"].IsEmpty())
                {
                    dp.Add("M_StockName", "%" + queryParam["M_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND M_StockName Like @M_StockName ");
                }
                return this.BaseRepository().FindList<Mes_MaterInDetailEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_MaterInDetailEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_MaterInDetailEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_MaterInDetailEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_MaterInDetailEntity entity)
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
