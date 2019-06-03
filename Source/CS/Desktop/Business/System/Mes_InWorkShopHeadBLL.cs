using DataAccess.SqlServer;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    public partial class Mes_InWorkShopHeadBLL
    {
        SqlHelper db = new SqlHelper();



        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_InWorkShopHeadEntity> GetList_InWorkShopHead(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT Top 1 * FROM Mes_InWorkShopHead ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_InWorkShopHeadEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
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
        public int SaveEntity(string keyValue, Mes_InWorkShopHeadEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_InWorkShopHead(");
                    strSql.Append("ID,");
                    strSql.Append("I_InNo,");
                    strSql.Append("I_StockCode,");
                    strSql.Append("I_StockName,");
                    strSql.Append("I_WorkShop,");
                    strSql.Append("I_OrderNo,");
                    strSql.Append("I_OrderDate,");
                    strSql.Append("I_Status,");
                    strSql.Append("I_CreateBy,");
                    strSql.Append("I_CreateDate,");
                    
                    strSql.Append("I_Remark");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@I_InNo,");
                    strSql.Append("@I_StockCode,");
                    strSql.Append("@I_StockName,");
                    strSql.Append("@I_WorkShop,");
                    strSql.Append("@I_OrderNo,");
                    strSql.Append("@I_OrderDate,");
                    strSql.Append("@I_Status,");

                    strSql.Append("@I_CreateBy,");
                    strSql.Append("@I_CreateDate,");

                    strSql.Append("@I_Remark");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                    //strSql.Append("UPDATE Mes_InWorkShopHead SET ");
                    //strSql.Append("B_BasketCode=@B_BasketCode,");
                    //strSql.Append("B_BasketName=@B_BasketName,");
                    //strSql.Append("M_Weight=@M_Weight ");
                    //strSql.Append(" WHERE ID=@ID");
                    //paramList.Add(new SqlParameter("@ID", keyValue));
                }

                paramList.Add(new SqlParameter("@I_InNo", entity.I_InNo));
                paramList.Add(new SqlParameter("@I_StockCode", entity.I_StockCode));
                paramList.Add(new SqlParameter("@I_StockName", entity.I_StockName));
                paramList.Add(new SqlParameter("@I_WorkShop", entity.I_WorkShop));
                paramList.Add(new SqlParameter("@I_OrderNo", entity.I_OrderNo));
                paramList.Add(new SqlParameter("@I_OrderDate", entity.I_OrderDate));
                paramList.Add(new SqlParameter("@I_Status", entity.I_Status));
                paramList.Add(new SqlParameter("@I_CreateBy", entity.I_CreateBy));
                paramList.Add(new SqlParameter("@I_CreateDate", entity.I_CreateDate));
                
                paramList.Add(new SqlParameter("@I_Remark", entity.I_Remark));
                var result = db.ExecuteNonQuery(strSql.ToString(), paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
