using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ayma.Application.TwoDevelopment.MesDev.Mes_ProductOrderHead;
using Ayma.Util.Security;
using Newtonsoft.Json.Linq;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 15:20
    /// 描 述：同步ERP成品商品资料
    /// </summary>
    public partial class Mes_ProductGoodsService : RepositoryFactory
    {
        string URL_ERPFood = Config.GetValue("URL_ERPFood");//ERP接口地址

        #region 获取数据


        #endregion

        #region 提交数据

     
        #endregion


        /// <summary>
        /// 获取ERP的商品资料
        /// </summary>
        public List<ERPTgoodsListModel> GetErpTgoodsList()
        {
            List<ERPTgoodsListModel> list = new List<ERPTgoodsListModel>();
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            URL_ERPFood = String.Format(URL_ERPFood + "GetGoodList?timeStamp={0}", timeStamp);
            try
            {
                var result = HttpMethods.DoGet(URL_ERPFood);
                result = ERPDESEncrypt.Decrypt(result);//解密数据
                JObject obj = JObject.Parse(result);
                if ("200" == obj["msgCode"].ToString())
                {
                    list = obj["data"].ToString().ToObject<List<ERPTgoodsListModel>>();
                }
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
            return list;
        }



        ///// <summary>
        ///// 保存ERP同步过来的商品信息
        ///// </summary>
        ///// <param name="ERPTgoodsListEntity"></param>
        //public void SaveErpTgoods(List<ERPTgoodsListModel> ERPTgoodsListEntity)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append(" SELECT * FROM DBO.MES_GOODS");
        //        sb.Append(" WHERE G_CODE=@G_CODE ");
        //         var userInfo = LoginUserInfo.Get();//获取登录用户
        //        foreach(var item in ERPTgoodsListEntity)
        //        {
        //            var dp = new DynamicParameters(new { });
        //            dp.Add("@G_CODE",item.partno);
        //            var table= this.BaseRepository().FindTable(sb.ToString(),dp);
        //            if(table.Rows.Count<1)
        //            {
        //                Mes_GoodsEntity product = new Mes_GoodsEntity();
        //                product.ID = Guid.NewGuid().ToString();
        //                product.G_Code = item.partno;
        //                product.G_Name = item.pname;
        //                product.G_Price = item.price;
        //                product.G_Unit = item.pack_uom;
        //                product.G_CreateBy = userInfo.realName;
        //                product.G_CreateDate = DateTime.Now;
        //                this.BaseRepository().Insert<Mes_GoodsEntity>(product);
        //            }
                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is ExceptionEx)
        //        {
        //            throw;
        //        }
        //        else
        //        {
        //            throw ExceptionEx.ThrowBusinessException(ex);
        //        }
                
        //    }

        
        //}

    }
}
