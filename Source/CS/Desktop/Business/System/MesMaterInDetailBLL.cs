using System;
using System.Collections.Generic;
using System.Linq;
using Model.Dto;
using Model;
using DataAccess.SqlServer;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Business.System
{
    /// <summary>
    /// 描述: 业务层 -- Mes_MaterInDetail
    /// </summary>
    public partial class MesMaterInDetailBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesMaterInDetailEntity> GetList(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT [ID]
      ,[M_MaterInNo]
      ,[M_OrderNo]
      ,[M_GoodsCode]
      ,(CASE M_Kind WHEN 2 THEN '成品' 
                  
                    ELSE '非成品' END) M_Kind
      ,[M_GoodsName]
      ,[M_Unit]
      ,[M_Qty]
      ,[M_Batch]
      ,[M_Remark]
      ,[M_Price] FROM Mes_MaterInDetail");
                strSql.Append(" WHERE M_MaterInNo = @M_MaterInNo");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@M_MaterInNo", string.Format("{0}", keyValue)));
                var rows = db.ExecuteObjects<MesMaterInDetailEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过物料编码获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesMaterInDetailEntity> GetList_GoodsCode(string GoodsCode, string MaterInNo)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT [ID]
      ,[M_MaterInNo]
      ,[M_OrderNo]
      ,[M_GoodsCode]
      ,(CASE M_Kind WHEN 2 THEN '成品' 

                    ELSE '非成品' END) M_Kind
      ,[M_GoodsName]
      ,[M_Unit]
      ,[M_Qty]
      ,[M_Batch]
,[M_Price]
      ,[M_Remark] FROM Mes_MaterInDetail");
                strSql.Append(" WHERE M_GoodsCode = @M_GoodsCode and M_MaterInNo=@M_MaterInNo");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@M_GoodsCode",  GoodsCode));
                paramList.Add(new SqlParameter("@M_MaterInNo", MaterInNo));


                var rows = db.ExecuteObjects<MesMaterInDetailEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 通过主键获取实体
        /// </summary>
		/// <param name="keyValue">主键</param>
        /// <returns>MesMaterInDetail</returns>
		public MesMaterInDetailEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append(@"SELECT [ID]
      ,[M_MaterInNo]
      ,[M_OrderNo]
      ,[M_GoodsCode]
      ,[M_Kind] 
      ,[M_GoodsName]
      ,[M_Unit]
      ,[M_Qty]
      ,[M_Batch]
,[M_Price]
      ,[M_Remark] FROM Mes_MaterInDetail");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesMaterInDetailEntity>(strSql.ToString(),paramList.ToArray());
                return rowData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>返回值大于0:删除成功</returns>
        public int DeleteEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("DELETE Mes_MaterInDetail");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var result = db.ExecuteNonQuery(strSql.ToString(),paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }			
		}
        
        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
		/// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue,MesMaterInDetailEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_MaterInDetail(");
                     strSql.Append("ID,");
                     strSql.Append("M_MaterInNo,");
                     strSql.Append("M_GoodsCode,");
                     strSql.Append("M_Kind,");
                     strSql.Append("M_GoodsName,");
                     strSql.Append("M_Unit,");
                     strSql.Append("M_Qty,");
                     strSql.Append("M_Batch,");
                     strSql.Append("M_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@M_MaterInNo,");
                     strSql.Append("@M_GoodsCode,");
                     strSql.Append("@M_Kind,");
                     strSql.Append("@M_GoodsName,");
                     strSql.Append("@M_Unit,");
                     strSql.Append("@M_Qty,");
                     strSql.Append("@M_Batch,");
                     strSql.Append("@M_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_MaterInDetail SET ");
                     strSql.Append("M_MaterInNo=@M_MaterInNo,");
                     strSql.Append("M_GoodsCode=@M_GoodsCode,");
                     strSql.Append("M_Kind=@M_Kind,");
                     strSql.Append("M_GoodsName=@M_GoodsName,");
                     strSql.Append("M_Unit=@M_Unit,");
                     strSql.Append("M_Qty=@M_Qty,");
                     strSql.Append("M_Batch=@M_Batch,");
                     strSql.Append("M_Remark=@M_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@M_MaterInNo",entity.M_MaterInNo));
                paramList.Add(new SqlParameter("@M_GoodsCode",entity.M_GoodsCode));
                paramList.Add(new SqlParameter("@M_Kind", entity.M_Kind));
                paramList.Add(new SqlParameter("@M_GoodsName",entity.M_GoodsName));
                paramList.Add(new SqlParameter("@M_Unit",entity.M_Unit));
                paramList.Add(new SqlParameter("@M_Qty",entity.M_Qty));
                paramList.Add(new SqlParameter("@M_Batch",entity.M_Batch));
                paramList.Add(new SqlParameter("@M_Remark",entity.M_Remark));
				var result = db.ExecuteNonQuery(strSql.ToString(),paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }			
		}

        /// <summary>
        /// 保存实体数据 事务
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntityTrans(string keyValue, MesMaterInDetailEntity entity, string WeightRecord_keyValue, MesWeightRecordEntity WeightRecord_entity)
        {
            SqlTrans trans = new SqlTrans();
            SqlConnection con = trans.TranConn(); ;
            SqlTransaction tran = trans.TransBegin(con);
            string role = "";
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                var WeightRecord_strSql = new StringBuilder();
                var WeightRecord_paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_MaterInDetail(");
                    strSql.Append("ID,");
                    strSql.Append("M_MaterInNo,");
                    strSql.Append("M_OrderNo,");
                    strSql.Append("M_GoodsCode,");
                    strSql.Append("M_Kind,");
                    strSql.Append("M_GoodsName,");
                    strSql.Append("M_Unit,");
                    strSql.Append("M_Qty,");
                    strSql.Append("M_Batch,");
                    strSql.Append("M_Price,");
                    strSql.Append("M_Remark");
                    strSql.Append(")");
                    strSql.Append("VALUES(");
                    strSql.Append("@ID,");
                    strSql.Append("@M_MaterInNo,");
                    strSql.Append("@M_OrderNo,");
                    strSql.Append("@M_GoodsCode,");
                    strSql.Append("@M_Kind,");
                    strSql.Append("@M_GoodsName,");
                    strSql.Append("@M_Unit,");
                    strSql.Append("@M_Qty,");
                    strSql.Append("@M_Batch,");
                    strSql.Append("@M_Price,");
                    strSql.Append("@M_Remark");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                    strSql.Append("UPDATE Mes_MaterInDetail SET ");
                    strSql.Append("M_MaterInNo=@M_MaterInNo,");
                    strSql.Append("M_OrderNo=@M_OrderNo,");
                    strSql.Append("M_GoodsCode=@M_GoodsCode,");
                    strSql.Append("M_Kind=@M_Kind,");
                    strSql.Append("M_GoodsName=@M_GoodsName,");
                    strSql.Append("M_Unit=@M_Unit,");
                    strSql.Append("M_Qty=@M_Qty,");
                    strSql.Append("M_Price=@M_Price,");
                    strSql.Append("M_Batch=@M_Batch,");
                    strSql.Append("M_Remark=@M_Remark ");

                    strSql.Append(" WHERE ID=@ID");
                    paramList.Add(new SqlParameter("@ID", keyValue));
                }
                paramList.Add(new SqlParameter("@M_MaterInNo", entity.M_MaterInNo));
                paramList.Add(new SqlParameter("@M_OrderNo", entity.M_OrderNo));
                paramList.Add(new SqlParameter("@M_GoodsCode", entity.M_GoodsCode));
                paramList.Add(new SqlParameter("@M_Kind", entity.M_Kind));
                paramList.Add(new SqlParameter("@M_GoodsName", entity.M_GoodsName));
                paramList.Add(new SqlParameter("@M_Unit", entity.M_Unit));
                paramList.Add(new SqlParameter("@M_Qty", entity.M_Qty));
                paramList.Add(new SqlParameter("@M_Batch", entity.M_Batch));
                paramList.Add(new SqlParameter("@M_Price", entity.M_Price));
                paramList.Add(new SqlParameter("@M_Remark", entity.M_Remark)); 

                SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), paramList.ToArray());

                WeightRecord_strSql.Append("INSERT INTO Mes_WeightRecord(");
                WeightRecord_strSql.Append("ID,");
                WeightRecord_strSql.Append("P_OrderNo,");
                WeightRecord_strSql.Append("W_Kind,");
                WeightRecord_strSql.Append("W_Date,");
                WeightRecord_strSql.Append("W_GoodsCode,");
                WeightRecord_strSql.Append("W_GoodsName,");
                WeightRecord_strSql.Append("W_Unit,");
                WeightRecord_strSql.Append("W_Qty,");
                WeightRecord_strSql.Append("W_Batch");
                WeightRecord_strSql.Append(")");
                WeightRecord_strSql.Append("VALUES(");
                WeightRecord_strSql.Append("@ID,");
                WeightRecord_strSql.Append("@P_OrderNo,");
                WeightRecord_strSql.Append("@W_Kind,");
                WeightRecord_strSql.Append("@W_Date,");
                WeightRecord_strSql.Append("@W_GoodsCode,");
                WeightRecord_strSql.Append("@W_GoodsName,");
                WeightRecord_strSql.Append("@W_Unit,");
                WeightRecord_strSql.Append("@W_Qty,");
                WeightRecord_strSql.Append("@W_Batch");
                WeightRecord_strSql.Append(")");
                WeightRecord_paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));

               WeightRecord_paramList.Add(new SqlParameter("@P_OrderNo", WeightRecord_entity.P_OrderNo));
               WeightRecord_paramList.Add(new SqlParameter("@W_Kind", WeightRecord_entity.W_Kind));
               WeightRecord_paramList.Add(new SqlParameter("@W_Date", WeightRecord_entity.W_Date));
               WeightRecord_paramList.Add(new SqlParameter("@W_GoodsCode", WeightRecord_entity.W_GoodsCode));
               WeightRecord_paramList.Add(new SqlParameter("@W_GoodsName", WeightRecord_entity.W_GoodsName));
               WeightRecord_paramList.Add(new SqlParameter("@W_Unit", WeightRecord_entity.W_Unit));
               WeightRecord_paramList.Add(new SqlParameter("@W_Qty", WeightRecord_entity.W_Qty));
               WeightRecord_paramList.Add(new SqlParameter("@W_Batch", WeightRecord_entity.W_Batch));

               SqlHelper.ExecuteNonQuery(tran, CommandType.Text, WeightRecord_strSql.ToString(), WeightRecord_paramList.ToArray());

                trans.TransCommit(tran);
                trans.TransEnd(con);

                return 1;
            }
            catch (Exception)
            {
                trans.TransRollback(tran);
                trans.TransEnd(con);

                throw;
            }
        }
        
    }
}
