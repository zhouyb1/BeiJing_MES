using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ayma.Application.TwoDevelopment.MesDev;

namespace Ayma.Application.TwoDevelopment.Tools
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2018-10-09 10:32
    /// 描 述：商家
    /// </summary>
    public partial class ToolsService : RepositoryFactory
    {

        #region 获取数据
        /// <summary>
        /// 根据物料编码获取物料实体信息
        /// </summary>
        /// <param name="code">物料编码</param>
        /// <returns></returns>
        public Mes_GoodsEntity ByCodeGetGoodsEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_GoodsEntity>(x=>x.G_Code==code);
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
        /// 获取物料列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetGoodsList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_GoodsEntity>();
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
        /// 根据门编码获取门实体信息
        /// </summary>
        /// <param name="code">门编码</param>
        /// <returns></returns>
        public Mes_DoorEntity ByCodeGetDoorEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_DoorEntity>(x=>x.D_Code==code);
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
        /// 获取门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_DoorEntity> GetDoorList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_DoorEntity>();
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
        /// 根据主键获取供应商实体信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_SupplyEntity ByIdGetSupplyEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_SupplyEntity>(x=>x.ID==keyValue);
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
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_SupplyEntity> GetSupplyList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_SupplyEntity>();
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
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        public bool IsName(string tables, string field, string names)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where "+field+"=@F_Name");
                var dp = new DynamicParameters(new { });
                dp.Add("F_Name", names, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
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
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        public bool IsOrderNo(string tables, string orderNo)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where F_OrderNo=@OrderNo");
                var dp = new DynamicParameters(new { });
                dp.Add("OrderNo", orderNo, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
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
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public bool IsCode(string tables,string field, string code)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where "+field+"=@Code");
                var dp = new DynamicParameters(new { });
                dp.Add("Code", code, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
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
