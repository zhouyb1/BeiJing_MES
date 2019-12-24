using DataAccess.SqlServer;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    public partial class Mes_OrgResHeadBLL
    {

        SqlHelper db = new SqlHelper();



        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_OrgResHeadEntity> GetList_OrgResHead(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT Top 1 * FROM Mes_OrgResHead ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_OrgResHeadEntity>(strSql.ToString(), paramList.ToArray());
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
        public int SH(string strDH)
        {
            var strSql = new StringBuilder();
            strSql.Append("update Mes_OrgResHead set O_Status = '2' where O_OrgResNo = '" + strDH + "'");
            var result = db.ExecuteNonQuery(strSql.ToString());
            return result;
        }

        /// <summary>
        /// 调用存储过程，提交单据
        /// </summary>
        /// <param name="strDJLX"></param>
        /// <returns></returns>
        public string UPLOAD(string strDH, string strName)
        {
            string strReturn = "";
            SqlParameter[] parameters = {
                        new SqlParameter("@OrderNo", SqlDbType.NVarChar,50), //单据
                        new SqlParameter("@UserName", SqlDbType.NVarChar,50), //用户
                        new SqlParameter("@errcode", SqlDbType.Int,40), 
                        new SqlParameter("@errtxt", SqlDbType.NVarChar,128) 
                    };
            parameters[0].Value = strDH;
            parameters[1].Value = strName;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.Output;
            CommandType type = CommandType.StoredProcedure;
            db.ExecuteNonQuery(type, "sp_OrgRes_Post", parameters);
            strReturn = parameters[3].Value.ToString();


            return strReturn;

        }

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue, Mes_OrgResHeadEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_OrgResHead(");
                    strSql.Append("ID,");
                    strSql.Append("O_OrgResNo,");
                    strSql.Append("O_Record,");
                    strSql.Append("O_ProCode,");
                    strSql.Append("O_WorkShopCode,");
                    strSql.Append("O_WorkShopName,");
                    strSql.Append("O_OrderNo,");
                    strSql.Append("O_OrderDate,");
                    strSql.Append("O_Status,");
                    strSql.Append("O_CreateBy,");
                    strSql.Append("O_CreateDate,");
                    strSql.Append("O_TeamCode,");
                    strSql.Append("O_TeamName,");
                    strSql.Append("O_Remark");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@O_OrgResNo,");
                    strSql.Append("@O_Record,");
                    strSql.Append("@O_ProCode,");
                    strSql.Append("@O_WorkShopCode,");
                    strSql.Append("@O_WorkShopName,");
                    strSql.Append("@O_OrderNo,");
                    strSql.Append("@O_OrderDate,");
                    strSql.Append("@O_Status,");

                    strSql.Append("@O_CreateBy,");
                    strSql.Append("@O_CreateDate,");
                    strSql.Append("@O_TeamCode,");
                    strSql.Append("@O_TeamName,");
                    strSql.Append("@O_Remark");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                }

                paramList.Add(new SqlParameter("@O_OrgResNo", entity.O_OrgResNo));
                paramList.Add(new SqlParameter("@O_Record", entity.O_Record));
                paramList.Add(new SqlParameter("@O_ProCode", entity.O_ProCode));
                paramList.Add(new SqlParameter("@O_WorkShopCode", entity.O_WorkShopCode));
                paramList.Add(new SqlParameter("@O_WorkShopName", entity.O_WorkShopName));
                paramList.Add(new SqlParameter("@O_OrderNo", entity.O_OrderNo));
                paramList.Add(new SqlParameter("@O_OrderDate", entity.O_OrderDate));
                paramList.Add(new SqlParameter("@O_Status", entity.O_Status));
                paramList.Add(new SqlParameter("@O_CreateBy", entity.O_CreateBy));
                paramList.Add(new SqlParameter("@O_CreateDate", entity.O_CreateDate));
                paramList.Add(new SqlParameter("@O_TeamCode", entity.O_TeamCode));
                paramList.Add(new SqlParameter("@O_TeamName", entity.O_TeamName));
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
