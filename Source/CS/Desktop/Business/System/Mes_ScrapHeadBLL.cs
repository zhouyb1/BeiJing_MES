using DataAccess.SqlServer;
using System;
using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    public partial class Mes_ScrapHeadBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue, Mes_ScrapHeadEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    
                    strSql.Append("INSERT INTO Mes_ScrapHead(");
                    strSql.Append("ID,");
                    strSql.Append("S_ScrapNo,");
                    strSql.Append("S_StockCode,");
                    strSql.Append("S_StockName,");
                    strSql.Append("S_OrderDate,");
                    strSql.Append("S_Status,");
                    strSql.Append("S_CreateBy,");
                    strSql.Append("S_CreateDate,");
                    strSql.Append("S_UpdateBy,");
                    strSql.Append("S_UpdateDate,");
                    strSql.Append("S_DeleteBy,");
                    strSql.Append("S_DeleteDate,");
                    strSql.Append("S_UploadBy,");
                    strSql.Append("S_UploadDate,");
                    strSql.Append("S_Remark");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@S_ScrapNo,");
                    strSql.Append("@S_StockCode,");
                    strSql.Append("@S_StockName,");
                    strSql.Append("@S_OrderDate,");
                    strSql.Append("@S_Status,");
                    strSql.Append("@S_CreateBy,");
                    strSql.Append("@S_CreateDate,");
                    strSql.Append("@S_UpdateBy,");
                    strSql.Append("@S_UpdateDate,");
                    strSql.Append("@S_DeleteBy,");
                    strSql.Append("@S_DeleteDate,");
                    strSql.Append("@S_UploadBy,");
                    strSql.Append("@S_UploadDate,");
                    strSql.Append("@S_Remark");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {   
                }
                paramList.Add(new SqlParameter("@S_ScrapNo", entity.S_ScrapNo));
                paramList.Add(new SqlParameter("@S_StockCode", entity.S_StockCode));
                paramList.Add(new SqlParameter("@S_StockName", entity.S_StockName));
                paramList.Add(new SqlParameter("@S_OrderDate", entity.S_OrderDate));
                paramList.Add(new SqlParameter("@S_Status", entity.S_Status));
                paramList.Add(new SqlParameter("@S_CreateBy", entity.S_CreateBy));
                paramList.Add(new SqlParameter("@S_CreateDate", entity.S_CreateDate));
                paramList.Add(new SqlParameter("@S_UpdateBy", entity.S_UploadBy));
                paramList.Add(new SqlParameter("@S_UpdateDate", entity.S_UploadBy));
                paramList.Add(new SqlParameter("@S_DeleteBy", entity.S_UploadBy));
                paramList.Add(new SqlParameter("@S_DeleteDate", entity.S_UploadBy));
                paramList.Add(new SqlParameter("@S_UploadBy", entity.S_UploadBy));
                paramList.Add(new SqlParameter("@S_UploadDate", entity.S_UploadBy));
                paramList.Add(new SqlParameter("@S_Remark", entity.S_UploadBy));
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
