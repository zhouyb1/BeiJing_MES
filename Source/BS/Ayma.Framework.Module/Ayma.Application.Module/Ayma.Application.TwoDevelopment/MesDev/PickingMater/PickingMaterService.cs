using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Ayma.Application.TwoDevelopment.MesDev.ExtensionModel;
using Microsoft.Win32.SafeHandles;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public partial class PickingMaterService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CollarHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.P_Status,
                t.C_CollarNo,
                t.C_StockCode,
                t.C_StockName,
                t.C_StockToCode,
                t.C_StockToName,
                t.P_OrderNo,
                t.P_OrderDate,
                t.C_Remark,
                dbo.GetUserNameById(t.C_CreateBy) C_CreateBy,
                t.C_CreateDate
                ");
                strSql.Append("  FROM Mes_CollarHead t ");
                strSql.Append("  WHERE t.P_Status in (1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.C_CreateDate >= @startTime AND t.C_CreateDate <= @endTime ) ");
                }
                if (!queryParam["C_CollarNo"].IsEmpty())
                {
                    dp.Add("C_CollarNo", "%" + queryParam["C_CollarNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_CollarNo Like @C_CollarNo ");
                }
                if (!queryParam["P_OrderNo"].IsEmpty())
                {
                    dp.Add("P_OrderNo", "%" + queryParam["P_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderNo Like @P_OrderNo ");
                }
                if (!queryParam["C_StockName"].IsEmpty())
                {
                    dp.Add("C_StockName", "%" + queryParam["C_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockName Like @C_StockName ");
                }
                if (!queryParam["C_StockToName"].IsEmpty())
                {
                    dp.Add("C_StockToName", "%" + queryParam["C_StockToName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockToName Like @C_StockToName ");
                }
                if (!queryParam["C_StockCode"].IsEmpty())
                {
                    dp.Add("C_StockCode", "%" + queryParam["C_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockCode Like @C_StockCode ");
                }
                if (!queryParam["C_StockToCode"].IsEmpty())
                {
                    dp.Add("C_StockToCode", "%" + queryParam["C_StockToCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockToCode Like @C_StockToCode ");
                }
                if (!queryParam["P_Status"].IsEmpty())
                {
                    dp.Add("P_Status", "%" + queryParam["P_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_Status Like @P_Status ");
                }
                return this.BaseRepository().FindList<Mes_CollarHeadEntity>(strSql.ToString(),dp, pagination);
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 获取Mes_CollarHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CollarHeadEntity GetMes_CollarHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_CollarHeadEntity>(keyValue);
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 获取Mes_CollarDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable< Mes_CollarDetailEntity >GetMes_CollarDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_CollarDetailEntity>(t=>t.C_CollarNo == keyValue);
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 获取库存物料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetMaterList(Pagination pagination, string queryJson, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT  S.ID ,
                                        S.I_StockCode ,
                                        S.I_StockName ,
                                        S.I_Kind ,
                                        S.I_GoodsCode ,
                                        S.I_GoodsName ,
                                        S.I_Unit ,
                                        S.I_Qty ,                              
                                        S.I_Batch ,
									    G.G_Price I_Price,
										G.G_UnitQty,
										G.G_Unit2,
										G.G_Unit,
                                        G.G_StockCode as C_StockCode, 
                                        (select S_Name from Mes_Stock where S_Code=G.G_StockCode) as C_StockName
                                   FROM    dbo.Mes_Inventory S   left join Mes_Goods G on (S.I_GoodsCode=G.G_Code) where  S.I_Kind=1 and  S.I_Qty <> 0 ");

                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!keyword .IsEmpty())
                {
                    dp.Add("keyword", "%"+keyword+"%", DbType.String);
                    strSql.Append(" AND  S.I_GoodsCode + S.I_GoodsName like @keyword ");
                }
                if (!queryParam["stockCode"].IsEmpty())
                {
                    dp.Add("stockCode", "%" + queryParam["stockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND  S.I_StockCode like @stockCode ");
                }
                return this.BaseRepository().FindList<Mes_InventoryEntity>(strSql.ToString(), dp, pagination);
            }
            catch (Exception ex) 
            {
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

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                var mes_CollarHeadEntity = GetMes_CollarHeadEntity(keyValue); 
                db.Delete<Mes_CollarHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_CollarDetailEntity>(t=>t.C_CollarNo == mes_CollarHeadEntity.C_CollarNo);
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

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_CollarHeadEntity entity,List<Mes_CollarDetailEntity> mes_CollarDetailEntityList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_CollarHeadEntityTmp = GetMes_CollarHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_CollarDetailEntity>(t=>t.C_CollarNo == mes_CollarHeadEntityTmp.C_CollarNo);
                    foreach (var item in mes_CollarDetailEntityList)
                    {
                        item.Create();
                        item.C_CollarNo = mes_CollarHeadEntityTmp.C_CollarNo;
                    }

                    db.Insert(mes_CollarDetailEntityList);
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "领料单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.C_CollarNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_CollarDetailEntityList)
                    {
                        item.Create();
                        item.C_CollarNo = entity.C_CollarNo;
                        item.C_OrderNo = null;
                    }
                    //mes_CollarDetailEntity.Create();
                    //mes_CollarDetailEntity.C_CollarNo = entity.C_CollarNo;
                    db.Insert(mes_CollarDetailEntityList);
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
        /// <summary>
        /// 新增实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(List<Mes_CollarHeadEntity> headList, List<Mes_CollarDetailEntity> mes_CollarDetailEntityList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                foreach (var entity in headList)
                {
                    entity.Create();
                }
                db.Insert(headList);
                foreach (var item in mes_CollarDetailEntityList)
                {
                    item.Create();
                }
                db.Insert(mes_CollarDetailEntityList);
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

        /// <summary>
        /// 自动生成领料单
        /// </summary>
        /// <param name="date"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AutoCreateOrder(string date, out string message)
        {
            bool success = true;
            var db = this.BaseRepository().BeginTrans();
            try
            {
                List< Product > products=new List<Product>();
                Dictionary<string, List<ProductBom>> boms=new Dictionary<string, List<ProductBom>>();
                List<GoodsInventory> inventorys=new List<GoodsInventory>();
                List<GoodsOut> goodsouts=new List<GoodsOut>();

                #region 获取生产订单
                if (success)
                {
                    string strGetProduct = @"--获取生产计划单
SELECT G.G_Code F_GoodsCode,SUM(D.P_Qty) F_Qty FROM Mes_ProductOrderHead H
LEFT JOIN Mes_ProductOrderDetail D ON D.P_OrderNo = H.P_OrderNo
LEFT JOIN Mes_Goods G ON G.G_Erpcode=D.P_GoodsCode AND G.G_Kind=3
WHERE CONVERT(VARCHAR(10),H.P_UseDate,120) =@date AND H.P_Status=2
GROUP BY G.G_Code 
HAVING(G.G_Code  IS NOT NULL)";

                    // 虚拟参数
                    var dp = new DynamicParameters(new { });
                    if (!date.IsEmpty())
                    {
                        dp.Add("date", date, DbType.String);
                    }

                    var rows = new RepositoryFactory().BaseRepository().FindList<Product>(strGetProduct, dp);
                    if (rows.Count() > 0)
                    {
                        products = rows.ToList();
                    }
                    else
                    {
                        success = false;
                        message = string.Format("生产日期{0}没有待生产的计划订单",date);
                    }
                }
                #endregion

                #region  获取产品配方

                if (success)
                {
                    string strGetBom = @"--递归获取子节点
WITH CTE
AS (SELECT ID,
           B_ParentID,
           B_GoodsCode,
           B_GoodsName,
           B_StockCode,
           B_Qty,
           0 AS F_Level
    FROM Mes_BomRecord
    WHERE B_GoodsCode = @F_GoodsCode
    UNION ALL
    SELECT Bom1.ID,
           Bom1.B_ParentID,
           Bom1.B_GoodsCode,
           Bom1.B_GoodsName,
           Bom1.B_StockCode,
           Bom1.B_Qty,
           (Bom2.F_Level + 1) F_Level
    FROM Mes_BomRecord Bom1,
         CTE Bom2
    WHERE Bom1.B_ParentID = Bom2.ID)
SELECT C.ID F_ID,
       C.B_ParentID F_ParentID,
       C.B_GoodsCode F_GoodsCode,
       G.G_Name F_GoodsName,
       G.G_Kind F_Kind,
       G.G_StockCode F_OutStockCode,--原料仓库仓库
       C.B_StockCode F_InStockCode,--车班入库仓库
       C.B_Qty F_PlanQty,
       C.B_Qty F_ProposeQty,
       C.F_Level
FROM CTE C
    LEFT JOIN Mes_Goods G
        ON C.B_GoodsCode = G.G_Code
ORDER BY C.F_Level";

                    foreach (var product in products)
                    {
                        var dp = new DynamicParameters(new { });
                        dp.Add("F_GoodsCode", product.F_GoodsCode, DbType.String);

                        var rows = new RepositoryFactory().BaseRepository().FindList<ProductBom>(strGetBom, dp);
                        if (rows.Count() > 0)
                        {
                            boms.Add(product.F_GoodsCode, rows.ToList());
                        }
                        else
                        {
                            success = false;
                            message = string.Format("产品编码{0}没有相应的配合", product.F_GoodsCode);
                            break;
                        }
                    }
                }

                #endregion

                #region 获取车间库存

                if (success)
                {
                    string strInventory = @"--加载车间库存
                    SELECT N.I_GoodsCode F_GoodsCode, SUM(N.I_Qty) F_Qty FROM Mes_Inventory N
                        LEFT JOIN Mes_Goods G ON G.G_Code = N.I_GoodsCode
                    WHERE G.G_Kind = 2
                    GROUP BY I_GoodsCode";

                    var rows = new RepositoryFactory().BaseRepository().FindList<GoodsInventory>(strInventory);
                    if (rows.Count() > 0)
                    {
                        inventorys = rows.ToList();
                    }
                }

                #endregion

                #region 加工数据

                foreach (var product in products)
                {
                    float? productQty = product.F_Qty;//生产数量
                    var productBom = boms[product.F_GoodsCode];//配合
                    var parent=productBom.Find(r => r.F_Level == 0);//根

                    SumQty(parent.F_ID, productBom, productQty.Value);//计算所需数量
                    CalculateQty(parent.F_ID, productBom, inventorys, goodsouts);//推算每个环节
                }


                #endregion


                if (success)
                {
                    db.Commit();
                }
                else
                {
                    db.Rollback();
                }
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

            message = "";
            return false;
        }


        /// <summary>
        ///计算某个产品每个环节所需总数量(其中包含g=>kg转换)
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="boms"></param>
        private void SumQty(string parentid, List<ProductBom> boms,float quantity)
        {
            var rows = boms.Where(r => r.F_ParentID == parentid); //子级
            if (rows == null || rows.Count() < 1)
            {
                return;
            }
            else
            {
                foreach (var row in rows)
                {
                    row.F_ProposeQty = (row.F_ProposeQty * quantity)/1000;
                    row.F_PlanQty = (row.F_PlanQty * quantity)/1000;

                    SumQty(row.F_ID,boms,quantity);
                }
            }
        }

        /// <summary>
        /// 循环计算每个环节所需数据
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="boms"></param>
        /// <param name="inventorys"></param>
        /// <param name="goodsouts"></param>
        /// <returns></returns>
        private List<GoodsOut> CalculateQty(string parentid ,List<ProductBom> boms, List<GoodsInventory> inventorys, List<GoodsOut> goodsouts)
        {

            var rows=boms.Where(r => r.F_ParentID == parentid);//子级

            if (rows == null || rows.Count() < 1) 
            {
                return goodsouts;
            }
            else
            {
                foreach (var row in rows)
                {
                    if(row.F_ProposeQty<=0)
                        continue;


                    //存在车间库存情况
                    GoodsOut gs = new GoodsOut();
                    gs.F_GoodsCode = row.F_GoodsCode;//物料编码
                    gs.F_GoodsName = row.F_GoodsName;//物料名称
                    gs.F_InStockCode = row.F_InStockCode;//车间入库仓库
                    gs.F_OutStockCode = row.F_OutStockCode;//原料出库仓库
                    gs.F_Kind = row.F_Kind;//物料类型
                    gs.F_PlanQty = row.F_PlanQty;//计划数量

                    var inventory=inventorys.Find(r => r.F_GoodsCode == row.F_GoodsCode);//查找车间库存
                    if (inventory != null && inventory.F_Qty>0)
                    {
                        gs.F_InventoryQty = inventory.F_Qty;//库存数量
                        var childrens = boms.Where(r => r.F_ParentID == row.F_ID);//查找子级

                        if (inventory.F_Qty > row.F_ProposeQty)
                        {
                            //车间库存充足
                            inventory.F_Qty -= row.F_ProposeQty;

                            row.F_ProposeQty = 0;
                            foreach (var children in childrens)
                            {
                                children.F_ProposeQty = 0;
                            }
                        }
                        else
                        {
                            //车间库存不足
                            inventory.F_Qty = 0;
                            gs.F_ProposeQty = row.F_ProposeQty- - inventory.F_Qty;

                            var surplus = gs.F_ProposeQty / gs.F_PlanQty;
                            foreach (var children in childrens)
                            {
                                children.F_ProposeQty = children.F_PlanQty* surplus;
                            }
                        }
                        goodsouts.Add(gs);
                    }
                    else
                    {
                        gs.F_InventoryQty = 0;//库存数量
                        gs.F_ProposeQty = row.F_ProposeQty;//建议数量
                    }
                    goodsouts.Add(gs);

                    CalculateQty(row.F_ID,boms,inventorys,goodsouts);
                }

                return goodsouts;
            }

        }

        #endregion
    }
}
