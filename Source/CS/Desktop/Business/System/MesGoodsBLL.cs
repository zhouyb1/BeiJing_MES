using System;
using System.Collections.Generic;
using System.Linq;
using Model.Dto;
using Model;
using DataAccess.SqlServer;
using System.Text;
using System.Data.SqlClient;

namespace Business.System
{
    /// <summary>
    /// 描述: 业务层 -- Mes_Goods
    /// </summary>
    public partial class MesGoodsBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        //public List<MesGoodsEntity> GetList()
        //{
        //    try
        //    {
        //        var strSql = new StringBuilder();
        //        strSql.Append("SELECT * FROM Mes_Goods");
        //        var rows = db.ExecuteObjects<MesGoodsEntity>(strSql.ToString());
        //        return rows;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// 输入物料编码或者物料名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesGoodsEntity> GetList(string G_Code, string G_Name)
        {
            try
            {
                var strSql = new StringBuilder();

                if (string.IsNullOrEmpty(G_Code) && string.IsNullOrEmpty(G_Name))
                {
                    strSql.Append("SELECT * FROM Mes_Goods where 1 = 1 ");
                    var rows = db.ExecuteObjects<MesGoodsEntity>(strSql.ToString());
                    return rows;
                }
                else
                {
                    var paramList = new List<SqlParameter>();
                    strSql.Append("SELECT * FROM Mes_Goods");
                    strSql.Append(" WHERE 1 = 1");
                    if (!string.IsNullOrEmpty(G_Code))
                    {
                        strSql.Append(" and G_Code = @G_Code");
                        paramList.Add(new SqlParameter("@G_Code", string.Format("{0}", G_Code)));
                    }
                    if (!string.IsNullOrEmpty(G_Name))
                    {
                        strSql.Append(" and G_Name = @G_Name");
                        paramList.Add(new SqlParameter("@G_Name", string.Format("{0}", G_Name)));
                    }
                    var rows = db.ExecuteObjects<MesGoodsEntity>(strSql.ToString(), paramList.ToArray());
                    return rows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 输入物料编码或者物料名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesGoodsEntity> GetData(string Condit)
        {
            try
            {
                var strSql = new StringBuilder();


                    var paramList = new List<SqlParameter>();
                    strSql.Append("SELECT * FROM Mes_Goods");
                    strSql.Append(" WHERE 1 = 1");
                    
                        strSql.Append(Condit);
                       
                    var rows = db.ExecuteObjects<MesGoodsEntity>(strSql.ToString(), paramList.ToArray());
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
        /// <returns>MesGoods</returns>
		public MesGoodsEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Goods");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesGoodsEntity>(strSql.ToString(),paramList.ToArray());
                return rowData;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 通过条件获取实体
        /// </summary>
        /// <param name="keyValue">条件</param>
        /// <returns>MesGoods</returns>
        public List<MesGoodsEntity> GetListCondit(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_Goods ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@ID", keyValue));
                //var rowData = db.ExecuteObject<MesGoodsEntity>(strSql.ToString(), paramList.ToArray());
                var rowData = db.ExecuteObjects<MesGoodsEntity>(strSql.ToString(), paramList.ToArray());
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
				strSql.Append("DELETE Mes_Goods");
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
        /// 修改实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>返回值大于0:修改成功</returns>
        public int UpdateEntity(string GoodsCode, decimal G_Price)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("Update Mes_Goods set G_Price = @G_Price");
                strSql.Append(" WHERE G_Code=@G_Code");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@G_Price", G_Price));
                paramList.Add(new SqlParameter("@G_Code", GoodsCode));
                var result = db.ExecuteNonQuery(strSql.ToString(), paramList.ToArray());
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
        public int SaveEntity(string keyValue,MesGoodsEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_Goods(");
                     strSql.Append("ID,");
                     strSql.Append("G_Code,");
                     strSql.Append("G_Name,");
                     strSql.Append("G_Kind,");
                     strSql.Append("G_Period,");
                     strSql.Append("G_Price,");
                     strSql.Append("G_Unit,");
                     strSql.Append("G_SupplyCode,");
                     strSql.Append("G_SupplyName,");
                     strSql.Append("G_Qty,");
                     strSql.Append("G_Super,");
                     strSql.Append("G_Lower,");
                     strSql.Append("G_CreateBy,");
                     strSql.Append("G_CreateDate,");
                     strSql.Append("G_UpdateBy,");
                     strSql.Append("G_UpdateDate,");
                     strSql.Append("G_UpdateDate,");
                     strSql.Append("G_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@G_Code,");
                     strSql.Append("@G_Name,");
                     strSql.Append("@G_Kind,");
                     strSql.Append("@G_Period,");
                     strSql.Append("@G_Price,");
                     strSql.Append("@G_Unit,");
                     strSql.Append("@G_SupplyCode,");
                     strSql.Append("@G_SupplyName,");
                     strSql.Append("@G_Qty,");
                     strSql.Append("@G_Super,");
                     strSql.Append("@G_Lower,");
                     strSql.Append("@G_CreateBy,");
                     strSql.Append("@G_CreateDate,");
                     strSql.Append("@G_UpdateBy,");
                     strSql.Append("@G_UpdateDate,");
                     strSql.Append("@G_StockCode,");
                     strSql.Append("@G_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_Goods SET ");
                     strSql.Append("G_Code=@G_Code,");
                     strSql.Append("G_Name=@G_Name,");
                     strSql.Append("G_Kind=@G_Kind,");
                     strSql.Append("G_Period=@G_Period,");
                     strSql.Append("G_Price=@G_Price,");
                     strSql.Append("G_Unit=@G_Unit,");
                     strSql.Append("G_SupplyCode=@G_SupplyCode,");
                     strSql.Append("G_SupplyName=@G_SupplyName,");
                     strSql.Append("G_Qty=@G_Qty,");
                     strSql.Append("G_Super=@G_Super,");
                     strSql.Append("G_Lower=@G_Lower,");
                     strSql.Append("G_CreateBy=@G_CreateBy,");
                     strSql.Append("G_CreateDate=@G_CreateDate,");
                     strSql.Append("G_UpdateBy=@G_UpdateBy,");
                     strSql.Append("G_UpdateDate=@G_UpdateDate,");
                     strSql.Append("G_UpdateDate=@G_StockCode,");
                     strSql.Append("G_Remark=@G_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@G_Code",entity.G_Code));
                paramList.Add(new SqlParameter("@G_Name",entity.G_Name));
                paramList.Add(new SqlParameter("@G_Kind",entity.G_Kind));
                paramList.Add(new SqlParameter("@G_Period",entity.G_Period));
                paramList.Add(new SqlParameter("@G_Price",entity.G_Price));
                paramList.Add(new SqlParameter("@G_Unit",entity.G_Unit));
                paramList.Add(new SqlParameter("@G_SupplyCode",entity.G_SupplyCode));
                paramList.Add(new SqlParameter("@G_Supply",entity.G_SupplyName));
                paramList.Add(new SqlParameter("@G_Qty",entity.G_Qty));
                paramList.Add(new SqlParameter("@G_Super",entity.G_Super));
                paramList.Add(new SqlParameter("@G_Lower",entity.G_Lower));
                paramList.Add(new SqlParameter("@G_CreateBy",entity.G_CreateBy));
                paramList.Add(new SqlParameter("@G_CreateDate",entity.G_CreateDate));
                paramList.Add(new SqlParameter("@G_UpdateBy",entity.G_UpdateBy));
                paramList.Add(new SqlParameter("@G_UpdateDate",entity.G_UpdateDate));
                paramList.Add(new SqlParameter("@G_StockCode", entity.G_StockCode));
                paramList.Add(new SqlParameter("@G_Remark",entity.G_Remark));
				var result = db.ExecuteNonQuery(strSql.ToString(),paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }			
		}
        
    }
}
