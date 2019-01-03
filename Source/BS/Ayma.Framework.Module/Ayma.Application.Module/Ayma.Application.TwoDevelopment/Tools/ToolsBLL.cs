using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.Tools
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2018-10-09 10:32
    /// 描 述：商家
    /// </summary>
    public partial class ToolsBLL : ToolsIBLL
    {
        private  ToolsService toolsService=new ToolsService();

        #region 获取数据
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        public bool IsName(string tables, string names)
        {
            try
            {
                return toolsService.IsName(tables, names);
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
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        public bool IsOrderNo(string tables, string orderNo)
        {
            try
            {
                return toolsService.IsOrderNo(tables, orderNo);
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
        #endregion
        
    }
}
