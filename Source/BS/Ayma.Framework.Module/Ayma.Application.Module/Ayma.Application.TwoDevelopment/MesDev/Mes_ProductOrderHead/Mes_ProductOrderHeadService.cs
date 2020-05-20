using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;
using Ayma.Application.TwoDevelopment.MesDev.Mes_ProductOrderHead;
using System.Security.Cryptography;
using Ayma.Util.Security;


namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 17:54
    /// 描 述：查询生成清单
    /// </summary>
    public partial class Mes_ProductOrderHeadService : RepositoryFactory
    {

        string URL_ERPFood = Config.GetValue("URL_ERPFood");//ERP接口地址

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.P_OrderNo,
                t.P_OrderStationID,
                t.P_OrderDate,
                t.P_OrderStationName,
                dbo.GetUserNameById(t.P_CreateBy) as P_CreateBy,
                t.P_CreateDate,
                t.P_UpdateBy,
                t.P_UpdateDate,
                t.P_UseDate,
                t1.P_GoodsCode,
                t1.P_GoodsName,
                t1.P_Unit,
                t1.P_Qty
                ");
                strSql.Append("  FROM Mes_ProductOrderHead t ");
                strSql.Append("  LEFT JOIN Mes_ProductOrderDetail t1 ON t1.P_OrderNo = t.P_OrderNo ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.P_UseDate >= @startTime AND t.P_UseDate <= @endTime ) ");
                }
                if (!queryParam["P_OrderDate"].IsEmpty())
                {
                    dp.Add("P_OrderDate",queryParam["P_OrderDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.P_OrderDate = @P_OrderDate ");
                }
                if (!queryParam["P_OrderNo"].IsEmpty())
                {
                    dp.Add("P_OrderNo", "%" + queryParam["P_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderNo Like @P_OrderNo ");
                }
                if (!queryParam["P_GoodsCode"].IsEmpty())
                {
                    dp.Add("P_GoodsCode", "%" + queryParam["P_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.P_GoodsCode Like @P_GoodsCode ");
                }
                if (!queryParam["P_GoodsName"].IsEmpty())
                {
                    dp.Add("P_GoodsName", "%" + queryParam["P_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.P_GoodsName Like @P_GoodsName ");
                }
                return this.BaseRepository().FindList<Mes_ProductOrderHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_ProductOrderHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProductOrderHeadEntity GetMes_ProductOrderHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProductOrderHeadEntity>(keyValue);
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
        /// 获取Mes_ProductOrderHead表实体数据
        /// </summary>
        /// <param name="orderNo">生产订单号</param>
        /// <returns></returns>
        public Mes_ProductOrderHeadEntity GetEntityByNo(string orderNo)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProductOrderHeadEntity>(c=>c.P_OrderNo==orderNo);
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
        /// 获取Mes_ProductOrderDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProductOrderDetailEntity GetMes_ProductOrderDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProductOrderDetailEntity>(t=>t.P_OrderNo == keyValue);
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
                var mes_ProductOrderHeadEntity = GetMes_ProductOrderHeadEntity(keyValue); 
                db.Delete<Mes_ProductOrderHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_ProductOrderDetailEntity>(t=>t.P_OrderNo == mes_ProductOrderHeadEntity.P_OrderNo);
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
        public void SaveEntity(string keyValue, Mes_ProductOrderHeadEntity entity,Mes_ProductOrderDetailEntity mes_ProductOrderDetailEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_ProductOrderHeadEntityTmp = GetMes_ProductOrderHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_ProductOrderDetailEntity>(t=>t.P_OrderNo == mes_ProductOrderHeadEntityTmp.P_OrderNo);
                    mes_ProductOrderDetailEntity.Create();
                    mes_ProductOrderDetailEntity.P_OrderNo = mes_ProductOrderHeadEntityTmp.P_OrderNo;
                    db.Insert(mes_ProductOrderDetailEntity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    mes_ProductOrderDetailEntity.Create();
                    mes_ProductOrderDetailEntity.P_OrderNo = entity.P_OrderNo;
                    db.Insert(mes_ProductOrderDetailEntity);
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

        #endregion


        /// <summary>
        /// 获取ERP的餐食计划清单
        /// </summary>
        /// <param name="useDate"></param>
        public List<ERPFoodListModel> GetErpFoodList(string useDate, string timeStamp)
        {
            
            List<ERPFoodListModel> list = new List<ERPFoodListModel>();
            try
            {
                URL_ERPFood = String.Format(URL_ERPFood + "GetFoodList?useDate={0}&timeStamp={1}", useDate, timeStamp);
                var result = HttpMethods.DoGet(URL_ERPFood);
                result=ERPDESEncrypt.Decrypt(result);
                JObject obj = JObject.Parse(result);
                if ("200" == obj["msgCode"].ToString())
                {
                    
                    var json=JObject.Parse( obj["data"].ToString());
                    var company = json["company"].ToString();
                    var com_name = json["com_name"].ToString();
                    var stock = json["stock"].ToString();
                    var name = json["name"].ToString();
                    //var wj = json["wj"].ToString();
                    var t_date = json["t_date"].ToString();
                    var use_date = json["use_date"].ToString();
                    var data = JArray.Parse(json["bodyList"].ToString());
                    foreach(var item in data)
                    {
                        ERPFoodListModel food = new ERPFoodListModel();
                        food.pratno = item["pratno"].ToString();
                        food.pname = item["pname"].ToString();
                        food.qty = item["qty"].ToString();
                        food.company = company;
                        food.com_name = com_name;
                        food.stock = stock;
                        food.name = name;
                        //food.wj = wj;
                        food.t_date = t_date;
                        food.use_date = use_date;
                        list.Add(food);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }


      


        /// <summary>
        /// 生产订单状态
        /// </summary>
        public enum  ProductState
        { 
            /// <summary>
            /// 待生产
            /// </summary>
            ProductTn=0,
            /// <summary>
            /// 生产中
            /// </summary>
            ProductIn = 1,

            /// <summary>
            /// 生产完成
            /// </summary>
            ProductOk = 2,

            /// <summary>
            /// 生产完成已入库
            /// </summary>
            ProductFinish = 3,

        };

        /// <summary>
        /// 保存ERP餐食清单
        /// </summary>
        /// <param name="foodEntity"></param>
        public void SaveERPFood(List<ERPFoodListModel> foodEntity,out int msgCode,out string msgInfo)
        {
            var db = this.BaseRepository();
            try
            {
                StringBuilder sb = new StringBuilder();
                var dp = new DynamicParameters(new { });
                sb.Append(" SELECT * FROM DBO.MES_PRODUCTORDERHEAD ");
                sb.Append(" WHERE CONVERT(varchar(10),P_UseDate,23)=@P_UseDate ");
                dp.Add("@P_UseDate",foodEntity[0].use_date);
                //根据生产日期判断生产清单是否已经存在
                var headEntity = this.BaseRepository().FindEntity<Mes_ProductOrderHeadEntity>(sb.ToString(), dp);
                var P_ORDERNO = string.Empty;
                if (headEntity == null)
                {
                    sb.Clear();
                    sb.Append(" INSERT INTO MES_PRODUCTORDERHEAD(ID, P_ORDERNO, P_ORDERDATE, P_ORDERSTATIONID, P_ORDERSTATIONNAME, P_CREATEBY, P_CREATEDATE, P_USEDATE, P_STATUS) ");
                    sb.Append(" VALUES(@ID, @P_ORDERNO, @P_ORDERDATE, @P_ORDERSTATIONID, @P_ORDERSTATIONNAME, @P_CREATEBY, @P_CREATEDATE,@P_USEDATE, @P_STATUS) ");

                    //生成订单号
                    Random rm = new Random();
                    P_ORDERNO = DateTime.Now.ToString("yyyyMMddHHmmss") + rm.Next(1000, 9999);

                    dp.Add("@ID", Guid.NewGuid().ToString());
                    dp.Add("@P_ORDERNO", P_ORDERNO);
                    dp.Add("@P_ORDERDATE", foodEntity[0].t_date);
                    dp.Add("@P_ORDERSTATIONID", foodEntity[0].stock);
                    dp.Add("@P_ORDERSTATIONNAME", foodEntity[0].name);
                    var userInfo = LoginUserInfo.Get();//获取登录用户
                    dp.Add("@P_CREATEBY", userInfo.realName);
                    dp.Add("@P_CREATEDATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dp.Add("@P_USEDATE", foodEntity[0].use_date);
                    dp.Add("@P_STATUS", ProductState.ProductTn);
                    db.ExecuteBySql(sb.ToString(), dp);

                }
                else { 
                    P_ORDERNO = headEntity.P_OrderNo;
                }

                sb.Clear();
                sb.Append(" INSERT INTO MES_PRODUCTORDERDETAIL(ID, P_ORDERNO, P_ORDERDATE, P_GOODSCODE, P_GOODSNAME, P_QTY) ");
                sb.Append(" VALUES(@ID, @P_ORDERNO, @P_ORDERDATE, @P_GOODSCODE, @P_GOODSNAME, @P_QTY) ");
                foreach (var item in foodEntity)
                {
                    var dp2 = new DynamicParameters(new { });
                    dp2.Add("@ID", Guid.NewGuid().ToString());
                    dp2.Add("@P_ORDERNO", P_ORDERNO);
                    dp2.Add("@P_ORDERDATE", item.t_date);
                    dp2.Add("@P_GOODSCODE", item.pratno);
                    dp2.Add("@P_GOODSNAME", item.pname);
                    dp2.Add("@P_QTY", item.qty);

                    var bodyEntity = this.BaseRepository().FindTable("SELECT P_ORDERDATE FROM DBO.MES_PRODUCTORDERDETAIL WHERE CONVERT(varchar(10),P_OrderDate,23)=@P_ORDERDATE AND P_GoodsCode=@P_GOODSCODE", dp2);
                    if (bodyEntity .Rows.Count<1)
                    {
                        db.ExecuteBySql(sb.ToString(), dp2);
                    }
                }
                //db.Commit();
                msgCode = 100;
                msgInfo = "保存成功";

            }
            catch (Exception ex)
            {
                db.Rollback();
                msgCode = 101;
                msgInfo = "保存异常";
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        
        }



    }
}
