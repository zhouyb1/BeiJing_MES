using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public partial class BomHeadBLL : BomHeadIBLL
    {
        private BomHeadService bomHeadService = new BomHeadService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_BomHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return bomHeadService.GetPageList(pagination, queryJson);
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
        /// 根据配方编码获取配方列表数据
        /// </summary>
        /// <param name="formulaCode">配方编码</param>
        /// <returns></returns>
        public IEnumerable<Mes_BomRecordEntity> GetBomRecordListBy(string formulaCode)
        {
            try
            {
                return bomHeadService.GetBomRecordListBy(formulaCode);
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
        /// 根据主键获取配方表实体
        /// </summary>
        /// <param name="keyValue">配方表主键</param>
        /// <returns></returns>
        public Mes_BomRecordEntity GetBomRecordEntity(string keyValue)
        {
            try
            {
                return bomHeadService.GetBomRecordEntity(keyValue);
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
        /// 获取配方列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_BomRecordEntity> GetBomRecordTreeList(string queryJson)
        {
            try
            {
                return bomHeadService.GetBomRecordTreeList(queryJson);
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
        /// 获取Mes_BomRecord表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<Mes_BomRecordEntity> GetMes_BomRecordList(string keyValue)
        {
            try
            {
                return bomHeadService.GetMes_BomRecordList(keyValue);
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
        /// 获取Mes_BomHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BomHeadEntity GetMes_BomHeadEntity(string keyValue)
        {
            try
            {
                return bomHeadService.GetMes_BomHeadEntity(keyValue);
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
        /// 获取Mes_BomRecord表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BomRecordEntity GetMes_BomRecordEntity(string keyValue)
        {
            try
            {
                return bomHeadService.GetMes_BomRecordEntity(keyValue);
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

        #region 提交数据

        /// <summary>
        /// 删除配方表数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteBomRecordForm(string keyValue)
        {
            try
            {
                bomHeadService.DeleteBomRecordForm(keyValue);
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
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                bomHeadService.DeleteEntity(keyValue);
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
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_BomHeadEntity entity)
        {
            try
            {
                bomHeadService.SaveEntity(keyValue, entity);
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
        /// 保存配方表数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveBomRecordForm(string keyValue, Mes_BomRecordEntity entity)
        {
            try
            {
                bomHeadService.SaveBomRecordForm(keyValue, entity);
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
        #region 验证重复

        /// <summary>
        /// 根据父Id、工艺代码、配方编码、物料编码判断是否重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="parentId">父Id</param>
        /// <param name="recordCode">工艺代码</param>
        /// <param name="formulaCode">配方编码</param>
        /// <param name="goodsCode">物料编码</param>
        /// <returns></returns>
        public bool ExistCode(string keyValue, string parentId,string recordCode, string formulaCode, string goodsCode)
        {
            try
            {
                return bomHeadService.ExistCode(keyValue, parentId,recordCode, formulaCode, goodsCode);
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
