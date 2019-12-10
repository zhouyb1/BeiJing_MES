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
    /// 日 期：2019-12-10 15:07
    /// 描 述：IP与RFID对应表
    /// </summary>
    public partial class Mes_IPToRFIDService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public Mes_IPToRFIDService()
        {
            fieldSql=@"
                t.ID,
                t.I_IP,
                t.I_RFIDCode,
                t.I_DoorCode,
                t.I_DoorName,
                t.I_Status,
                t.I_Remark
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_IPToRFIDEntity> GetList( string queryJson )
        {
            try
            {
                //参考写法
                //var queryParam = queryJson.ToJObject();
                // 虚拟参数
                //var dp = new DynamicParameters(new { });
                //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM Mes_IPToRFID t ");
                return this.BaseRepository().FindList<Mes_IPToRFIDEntity>(strSql.ToString());
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
        public IEnumerable<Mes_IPToRFIDEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM Mes_IPToRFID t where 1=1");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["I_IP"].IsEmpty())
                {
                    dp.Add("I_IP", "%" + queryParam["I_IP"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_IP Like @I_IP ");
                }
                if (!queryParam["I_DoorName"].IsEmpty())
                {
                    dp.Add("I_DoorName", "%" + queryParam["I_DoorName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_DoorName Like @I_DoorName ");
                }
                if (!queryParam["I_Status"].IsEmpty())
                {
                    dp.Add("I_Status", "%" + queryParam["I_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_Status Like @I_Status ");
                }
                return this.BaseRepository().FindList<Mes_IPToRFIDEntity>(strSql.ToString(), dp, pagination);
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
        public Mes_IPToRFIDEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_IPToRFIDEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_IPToRFIDEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_IPToRFIDEntity entity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@codeType", "RFID");
                    dp.Add("@code", "", DbType.String, ParameterDirection.Output);
                    dp.Add("@goodsSecNo", "");
                    dp.Add("@stockType", "");
                    db.ExecuteByProc("sp_GetCode", dp);
                    var I_RFIDCode = dp.Get<string>("@code"); //存储过程返回编号
                    entity.I_RFIDCode = I_RFIDCode;
                    entity.Create();
                    db.Insert(entity);
                }
                db.Commit();
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
