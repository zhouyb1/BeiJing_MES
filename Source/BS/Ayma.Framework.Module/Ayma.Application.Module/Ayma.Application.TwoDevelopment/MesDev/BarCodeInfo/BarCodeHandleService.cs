using System;
using System.Data;
using Ayma.Application.TwoDevelopment.MesDev.BarCodeInfo;
using Ayma.DataBase.Repository;
using Ayma.Util;
using Dapper;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    public class BarCodeHandleService : RepositoryFactory
    {
        public Mes_ScanCodeEntity GetGoodsInfoByCode(string code, string printfTime)
        {
            var db = this.BaseRepository().BeginTrans();

            var barcodeEntity = db.FindEntity<Mes_ScanCodeEntity>(d=>d.S_Code==code); //获取二维码信息
            if (barcodeEntity != null)
            {
                try
                {
                    var recordEntity = db.FindEntity<Mes_ScanRecordEntity>(c => c.S_Barcode == barcodeEntity.S_Code && c.S_PrintfTime == printfTime);
                    if (recordEntity != null)
                    {
                        //update
                        var recordNo = recordEntity.S_ScanCount + 1;
                        recordEntity.S_ScanCount = recordNo;
                        recordEntity.S_ScanTime = DateTime.Now;
                        db.Update(recordEntity);
                    }
                    else
                    {
                        var entity = new Mes_ScanRecordEntity { S_Barcode = code, S_PrintfTime = printfTime };
                        entity.Create();
                        db.Insert(entity);
                    }
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
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
            return barcodeEntity;
        }
    }
}
