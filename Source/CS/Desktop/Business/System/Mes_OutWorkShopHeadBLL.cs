using DataAccess.SqlServer;
using Model;
using Model.System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    public partial class Mes_OutWorkShopHeadBLL
    {
        SqlHelper db = new SqlHelper();



        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_OutWorkShopHeadEntity> GetList_OutWorkShopHead(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT Top 1 * FROM Mes_OutWorkShopHead ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_OutWorkShopHeadEntity>(strSql.ToString(), paramList.ToArray());
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
        public int SaveEntity(string keyValue, Mes_OutWorkShopHeadEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_OutWorkShopHead(");
                    strSql.Append("ID,");
                    strSql.Append("O_OutNo,");
                    strSql.Append("O_StockCode,");
                    strSql.Append("O_StockName,");
                    strSql.Append("O_WorkShop,");
                    strSql.Append("O_OrderNo,");
                    strSql.Append("O_OrderDate,");
                    strSql.Append("O_Status,");
                    strSql.Append("O_CreateBy,");
                    strSql.Append("O_Kind,");
                    strSql.Append("O_CreateDate,");

                    strSql.Append("O_Remark");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@O_OutNo,");
                    strSql.Append("@O_StockCode,");
                    strSql.Append("@O_StockName,");
                    strSql.Append("@O_WorkShop,");
                    strSql.Append("@O_OrderNo,");
                    strSql.Append("@O_OrderDate,");
                    strSql.Append("@O_Status,");

                    strSql.Append("@O_CreateBy,");
                    strSql.Append("@O_Kind,");
                    strSql.Append("@O_CreateDate,");

                    strSql.Append("@O_Remark");
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

                paramList.Add(new SqlParameter("@O_OutNo", entity.O_OutNo));
                paramList.Add(new SqlParameter("@O_StockCode", entity.O_StockCode));
                paramList.Add(new SqlParameter("@O_StockName", entity.O_StockName));
                paramList.Add(new SqlParameter("@O_WorkShop", entity.O_WorkShop));
                paramList.Add(new SqlParameter("@O_OrderNo", entity.O_OrderNo));
                paramList.Add(new SqlParameter("@O_OrderDate", entity.O_OrderDate));
                paramList.Add(new SqlParameter("@O_Status", entity.O_Status));
                paramList.Add(new SqlParameter("@O_CreateBy", entity.O_CreateBy));
                paramList.Add(new SqlParameter("@O_Kind", entity.O_Kind));
                paramList.Add(new SqlParameter("@O_CreateDate", entity.O_CreateDate));

                paramList.Add(new SqlParameter("@O_Remark", entity.O_Remark));
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
