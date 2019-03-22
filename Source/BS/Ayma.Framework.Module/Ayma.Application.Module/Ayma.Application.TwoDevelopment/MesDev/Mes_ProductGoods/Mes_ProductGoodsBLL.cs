using Ayma.Application.TwoDevelopment.MesDev.Mes_ProductOrderHead;
using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 15:20
    /// 描 述：同步ERP成品商品资料
    /// </summary>
    public partial class Mes_ProductGoodsBLL : Mes_ProductGoodsIBLL
    {
        private Mes_ProductGoodsService mes_ProductGoodsService = new Mes_ProductGoodsService();

        #region 获取数据


      
        #endregion

        #region 提交数据


        #endregion


         /// <summary>
        /// 获取ERP的商品资料
        /// </summary>
        public List<ERPTgoodsListModel> GetErpTgoodsList()
        {
            try
            {
                return mes_ProductGoodsService.GetErpTgoodsList();
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

        ///// <summary>
        ///// 保存ERP同步过来的商品信息
        ///// </summary>
        ///// <param name="ERPTgoodsListEntity"></param>
        //public void SaveErpTgoods(List<ERPTgoodsListModel> ERPTgoodsListEntity)
        //{
        //    try
        //    {
        //        mes_ProductGoodsService.SaveErpTgoods(ERPTgoodsListEntity);
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
