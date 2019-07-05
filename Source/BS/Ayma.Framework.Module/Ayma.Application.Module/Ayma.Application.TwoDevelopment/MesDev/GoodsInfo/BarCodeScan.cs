using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ayma.DataBase.Repository;

namespace Ayma.Application.TwoDevelopment.MesDev.GoodsInfo
{
    public class BarCodeScanService : RepositoryFactory
    {
        public Mes_ScanCodeEntity ScanCodeHandle(string code, string printfTime, ref string msg)
        {
            var db = this.BaseRepository();
            var Mes_ScanCodeEntity = db.FindEntity<Mes_ScanCodeEntity>(c => c.S_Code == code);
            if (Mes_ScanCodeEntity != null)
            {
                SqlParameter[] para11 =
                {
                    new SqlParameter("@S_Code", code),
                    new SqlParameter("@S_PrintfTime", printfTime)
                };
                var sql =
                    "select S_ScanCount from Mes_ScanRecord where S_Barcode =@S_Code and S_PrintfTime =@S_PrintfTime ";
                var recordDt = db.FindTable(sql, para11);
                if (recordDt != null && recordDt.Rows.Count > 0)
                {
                    var record = int.Parse(recordDt.Rows[0]["S_ScanCount"].ToString()) + 1;
                    var updateSql =
                        "update Mes_ScanRecord set S_ScanCount=@S_ScanCount,S_ScanTime=@S_ScanTime where S_Barcode =@S_Barcode and S_PrintfTime =@S_PrintfTime ";
                    IDbDataParameter[] psParameters =
                    {
                        new SqlParameter("@S_Barcode", code),
                        new SqlParameter("@S_PrintfTime", printfTime),
                        new SqlParameter("@S_ScanCount", record),
                        new SqlParameter("@S_ScanTime", DateTime.Now),
                    };
                    if (db.ExecuteBySql(updateSql, psParameters) > 0)
                    {
                        msg = "数据执行成功";
                        Mes_ScanCodeEntity.S_ScanRecord = record;
                        Mes_ScanCodeEntity.S_ScanTime = Convert.ToDateTime(psParameters[3].Value);
                        return Mes_ScanCodeEntity;
                    }
                    msg = "数据执行失败";
                    return null;
                }
                try
                {
                    db.BeginTrans();
                    var insertSql = @"insert into Mes_ScanRecord 
                                      (
                                       S_Barcode,
                                       S_PrintfTime,
                                       ID,
                                       S_ScanCount,
                                       S_ScanTime )
                                 values 
                                      (
                                       @S_Barcode,
                                       @S_PrintfTime,
                                       @ID,
                                       @S_ScanCount,
                                       @S_ScanTime )";
                    IDbDataParameter[] parac =
                    {
                        new SqlParameter("@S_Barcode", SqlDbType.VarChar),
                        new SqlParameter("@S_PrintfTime", SqlDbType.NVarChar),
                        new SqlParameter("@ID", SqlDbType.VarChar),
                        new SqlParameter("@S_ScanCount", SqlDbType.Int),
                        new SqlParameter("@S_ScanTime", SqlDbType.DateTime),
                    };
                    parac[0].Value = code;
                    parac[1].Value = printfTime;
                    parac[2].Value = Guid.NewGuid().ToString();
                    parac[3].Value = 1;
                    parac[4].Value = DateTime.Now;
                    var result = db.ExecuteBySql(insertSql, parac);
                    if (result > 0)
                    {
                        msg = "数据处理成功";
                        db.Commit();
                        Mes_ScanCodeEntity.S_ScanRecord = 1;
                        Mes_ScanCodeEntity.S_ScanTime = Convert.ToDateTime(parac[4].Value);
                        return Mes_ScanCodeEntity;
                    }
                    msg = "数据处理失败";
                    db.Rollback();
                    return null;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    msg = "数据处理执行异常";
                    throw;
                }
            }
            msg = "没有数据";
            return null;
        }
    }
}

