using Ayma.Util;
using System.Collections.Generic;
using System.Data;
using Ayma.Application.TwoDevelopment.MesDev;

namespace Ayma.Application.TwoDevelopment.Tools
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2018-10-09 10:32
    /// 描 述：商家
    /// </summary>
    public interface ToolsIBLL
    {
        #region 获取数据
        /// <summary>
        /// 根据物料编码获取物料实体信息
        /// <param name="code">物料编码</param>
        /// </summary>
        /// <returns></returns>
        Mes_GoodsEntity ByCodeGetGoodsEntity(string code);
        /// <summary>
        /// 获取物料
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetGoodsList();
        /// <summary>
        /// 根据门编码获取门实体信息
        /// </summary>
        /// <param name="code">门编码</param>
        /// <returns></returns>
        Mes_DoorEntity ByCodeGetDoorEntity(string code);
        /// <summary>
        /// 获取门列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_DoorEntity> GetDoorList();
        /// <summary>
        /// 根据主键获取供应商实体信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_SupplyEntity ByIdGetSupplyEntity(string keyValue);
        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_SupplyEntity> GetSupplyList();
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        bool IsName(string tables,string field, string names);
        /// <summary>
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        bool IsOrderNo(string tables, string orderNo);
        /// <summary>
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        bool IsCode(string tables,string field,string code);
        #endregion

    }
}
