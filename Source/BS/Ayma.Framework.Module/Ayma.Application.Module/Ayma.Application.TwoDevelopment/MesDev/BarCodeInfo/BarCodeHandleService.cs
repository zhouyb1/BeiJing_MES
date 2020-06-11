using System;
using System.Data;
using System.Linq;
using Ayma.Application.TwoDevelopment.MesDev.BarCodeInfo;
using Ayma.DataBase.Repository;
using Ayma.Util;
using Dapper;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    public class BarCodeHandleService : RepositoryFactory
    {
        public ProductsInfo GetGoodsInfoByCode(string code, string printfTime)
        {
            GoodsListService goodsService = new GoodsListService();

            var sql = " SELECT ID,S_Code,S_Name,S_Producer,S_Standard,S_Storage FROM dbo.Mes_ScanCode where S_Code =@barcode";
            var dp1 = new DynamicParameters(new {});
            dp1.Add("barcode",code,DbType.String);
            var productInfo = this.BaseRepository().FindEntity<ProductsInfo>(sql,dp1); //获取二维码信息

            var goodsEntity = goodsService.GetMes_GoodsEntity(code);//根据条码获取物料编码
            //获取原物料配料

            var sqlGetBom = @"WITH CTE AS ( SELECT ID ,
                                                B_ParentID ,
                                                B_GoodsCode ,
                                                B_GoodsName ,
                                                0 AS F_Level
                                       FROM     Mes_BomRecord
                                       WHERE    B_GoodsCode = @goodsCode
                                       UNION ALL
                                       SELECT   Bom1.ID ,
                                                Bom1.B_ParentID ,
                                                Bom1.B_GoodsCode ,
                                                Bom1.B_GoodsName ,
                                                ( Bom2.F_Level + 1 ) F_Level
                                       FROM     Mes_BomRecord Bom1 ,
                                                CTE Bom2
                                       WHERE    Bom1.B_ParentID = Bom2.ID
                                     )
                            SELECT DISTINCT G.G_Name GoodsName,c.F_Level
                                 
                            FROM    CTE C
                                    LEFT JOIN Mes_Goods G ON C.B_GoodsCode = G.G_Code
                            WHERE   g.G_Kind = 1
                            ORDER BY C.F_Level";

            if (productInfo != null)
            {
                //虚拟参数
                var dp = new DynamicParameters(new { });
                dp.Add("goodsCode", goodsEntity.G_Code);
                var list = this.BaseRepository().FindList<BomInfo>(sqlGetBom, dp).ToList();
                if (list.Any())
                {
                    //拼接配料
                    var bomStr = list.Aggregate("", (current, goodsname) => current + (""+goodsname.GoodsName + ","));
                    bomStr = bomStr.Substring(0, bomStr.Length - 1);
                    productInfo.S_MaterName = bomStr;
                }
                else
                {
                    productInfo.S_MaterName = "暂无配料";
                }
                var db = this.BaseRepository().BeginTrans();

                try
                {

                    var recordEntity = this.BaseRepository().FindEntity<Mes_ScanRecordEntity>(c => c.S_Barcode == code && c.S_PrintfTime == printfTime);
                    if (recordEntity != null)
                    {
                        var recordNo = recordEntity.S_ScanCount + 1;
                        recordEntity.S_ScanCount = recordNo;
                        recordEntity.S_ScanTime = DateTime.Now;
                        db.Update(recordEntity);
                    }
                    else
                    {
                        recordEntity=new Mes_ScanRecordEntity();
                        recordEntity.Create();
                        recordEntity.S_PrintfTime = printfTime;
                        recordEntity.S_Barcode = code;
                        db.Insert(recordEntity);
                    }
                    db.Commit();
                    productInfo.S_ScanRecord = recordEntity.S_ScanCount;
                    productInfo.S_ScanTime = recordEntity.S_ScanTime;
                    productInfo.S_ProductDate = "生产时间见覆膜喷码";
                    productInfo.S_Team = "热厨班组";
                    productInfo.S_Quality = "保质期见覆膜喷码";
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
            return productInfo;
        }
    }
}
