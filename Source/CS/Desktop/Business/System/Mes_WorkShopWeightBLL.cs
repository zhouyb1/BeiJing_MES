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
    public partial class Mes_WorkShopWeightBLL
    {
        SqlHelper db = new SqlHelper();



        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_WorkShopWeightEntity> GetList_WorkShopWeight(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT  * FROM Mes_WorkShopWeight ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_WorkShopWeightEntity>(strSql.ToString(), paramList.ToArray());
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
        public int SaveEntity(string keyValue, Mes_WorkShopWeightEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_WorkShopWeight(");
                    strSql.Append("ID,");
                    strSql.Append("W_RecordCode,");
                    strSql.Append("W_RecordName,");
                    strSql.Append("W_ProceCode,");
                    strSql.Append("W_ProceName,");
                    strSql.Append("W_WorkShopCode,");
                    strSql.Append("W_WorkShopName,");
                    strSql.Append("W_OrderNo,");
                    strSql.Append("W_Status,");
                    strSql.Append("W_CreateBy,");
                    strSql.Append("W_CreateDate,");
                    strSql.Append("W_SecGoodsCode,");
                    strSql.Append("W_SecGoodsName,");
                    strSql.Append("W_SecUnit,");
                    strSql.Append("W_SecQty,");
                    strSql.Append("W_SecBatch,");
                    strSql.Append("W_Remark");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@W_RecordCode,");
                    strSql.Append("@W_RecordName,");
                    strSql.Append("@W_ProceCode,");
                    strSql.Append("@W_ProceName,");
                    strSql.Append("@W_WorkShopCode,");
                    strSql.Append("@W_WorkShopName,");
                    strSql.Append("@W_OrderNo,");
                    strSql.Append("@W_Status,");
                    strSql.Append("@W_CreateBy,");
                    strSql.Append("@W_CreateDate,");
                    strSql.Append("@W_SecGoodsCode,");
                    strSql.Append("@W_SecGoodsName,");
                    strSql.Append("@W_SecUnit,");
                    strSql.Append("@W_SecQty,");
                    strSql.Append("@W_SecBatch,");
                    strSql.Append("@W_Remark");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {

                }
                paramList.Add(new SqlParameter("@W_RecordCode", entity.W_RecordCode));
                paramList.Add(new SqlParameter("@W_RecordName", entity.W_RecordName));
                paramList.Add(new SqlParameter("@W_ProceCode", entity.W_ProceCode));
                paramList.Add(new SqlParameter("@W_ProceName", entity.W_ProceName));
                paramList.Add(new SqlParameter("@W_WorkShopCode", entity.W_WorkShopCode));
                paramList.Add(new SqlParameter("@W_WorkShopName", entity.W_WorkShopName));
                paramList.Add(new SqlParameter("@W_OrderNo", entity.W_OrderNo));
                paramList.Add(new SqlParameter("@W_Status", entity.W_Status));
                paramList.Add(new SqlParameter("@W_CreateBy", entity.W_CreateBy));
                paramList.Add(new SqlParameter("@W_CreateDate", entity.W_CreateDate));
                paramList.Add(new SqlParameter("@W_SecGoodsCode", entity.W_SecGoodsCode));
                paramList.Add(new SqlParameter("@W_SecGoodsName", entity.W_SecGoodsName));
                paramList.Add(new SqlParameter("@W_SecUnit", entity.W_SecUnit));
                paramList.Add(new SqlParameter("@W_SecQty", entity.W_SecQty));
                paramList.Add(new SqlParameter("@W_SecBatch", entity.W_SecBatch));
                paramList.Add(new SqlParameter("@W_Remark", entity.W_Remark));
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
