using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:55
    /// 描 述：工序管理
    /// </summary>
    public partial class ProceManagerBLL : ProceManagerIBLL
    {
        private ProceManagerService proceManagerService = new ProceManagerService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return proceManagerService.GetPageList(pagination, queryJson);
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
        /// 获取页面显示树形列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProceEntity> GetTreeList(string queryJson)
        {
            try
            {
                return proceManagerService.GetTreeList(queryJson);
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
        /// 获取Mes_Proce表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProceEntity GetMes_ProceEntity(string keyValue)
        {
            try
            {
                return proceManagerService.GetMes_ProceEntity(keyValue);
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
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                proceManagerService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, Mes_ProceEntity entity)
        {
            try
            {
                proceManagerService.SaveEntity(keyValue, entity);
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

        #region 验证数据
        /// <summary>
        /// 工艺代码不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="recordCode">工艺代码</param>
        public bool ExistRecordCode(string keyValue, string recordCode)
        {
            try
            {
                return proceManagerService.ExistRecordCode(keyValue, recordCode);
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
        /// 工艺名称不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="recordName">工艺名称</param>
        public bool ExistRecordName(string keyValue, string recordName)
        {
            try
            {
                return proceManagerService.ExistRecordName(keyValue, recordName);
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
        /// 工序名称不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="proName">工序名称</param>
        public bool ExistProName(string keyValue, string proName)
        {
            try
            {
                return proceManagerService.ExistProName(keyValue, proName);
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
        /// 工序号不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="proNo">工艺代码</param>
        public bool ExistProNo(string keyValue, string proNo)
        {
            try
            {
                return proceManagerService.ExistProNo(keyValue, proNo);
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
