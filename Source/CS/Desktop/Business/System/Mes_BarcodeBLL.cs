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
    
    public partial class Mes_BarcodeBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_BarcodeEntity> GetList_Mes_Barcode(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                //strSql.Append("SELECT * FROM Mes_Barcode ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_BarcodeEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        public int Update(string strSql2)
        {
            var strSql = new StringBuilder();
            strSql.Append(strSql2);
            var result = db.ExecuteNonQuery(strSql.ToString());
            return result;
        }

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue, Mes_BarcodeEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {


                    strSql.Append("INSERT INTO Mes_Barcode(");
                    strSql.Append("ID,");
                    strSql.Append("B_Barcode,");
                    strSql.Append("B_Code,");
                    strSql.Append("B_Name,");
                    strSql.Append("B_Qty,");
                    strSql.Append("B_WorkShopCode,");
                    strSql.Append("B_Status,");
                    strSql.Append("B_Ptime,");
                    strSql.Append("B_Itime,");
                    strSql.Append("B_Otime,");
                    strSql.Append("B_Utime,");
                    strSql.Append("B_Remark");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@B_Barcode,");
                    strSql.Append("@B_Code,");
                    strSql.Append("@B_Name,");
                    strSql.Append("@B_Qty,");
                    strSql.Append("@B_WorkShopCode,");
                    strSql.Append("@B_Status,");
                    strSql.Append("@B_Ptime,");
                    strSql.Append("@B_Itime,");
                    strSql.Append("@B_Otime,");
                    strSql.Append("@B_Utime,");

                    strSql.Append("@B_Remark");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                }

                paramList.Add(new SqlParameter("@B_Barcode", entity.B_Barcode));
                paramList.Add(new SqlParameter("@B_Code", entity.B_Code));
                paramList.Add(new SqlParameter("@B_Name", entity.B_Name));
                paramList.Add(new SqlParameter("@B_Qty", entity.B_Qty));
                paramList.Add(new SqlParameter("@B_WorkShopCode", entity.B_WorkShopCode));
                paramList.Add(new SqlParameter("@B_Status", entity.B_Status));
                paramList.Add(new SqlParameter("@B_Ptime", entity.B_Ptime));
                paramList.Add(new SqlParameter("@B_Itime", entity.B_Itime));
                paramList.Add(new SqlParameter("@B_Otime", entity.B_Otime));
                paramList.Add(new SqlParameter("@B_Utime", entity.B_Utime));
                paramList.Add(new SqlParameter("@B_Remark", entity.B_Remark));
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
