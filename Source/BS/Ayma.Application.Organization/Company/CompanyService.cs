﻿using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ayma.Application.Organization
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.04.17
    /// 描 述：公司管理
    /// </summary>
    public class CompanyService : RepositoryFactory
    {
        #region 构造函数和属性
        private string fieldSql;
        public CompanyService()
        {
            fieldSql = @"
                    t.F_CompanyId,
                    t.F_Category,
                    t.F_ParentId,
                    t.F_EnCode,
                    t.F_ShortName,
                    t.F_FullName,
                    t.F_Nature,
                    t.F_OuterPhone,
                    t.F_InnerPhone,
                    t.F_Fax,
                    t.F_Postalcode,
                    t.F_Email,
                    t.F_Manager,
                    t.F_ProvinceId,
                    t.F_CityId,
                    t.F_CountyId,
                    t.F_Address,
                    t.F_WebAddress,
                    t.F_FoundedTime,
                    t.F_BusinessScope,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
                    t.F_Description,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName
                     ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取可管理的公司
        /// </summary>
        /// <returns></returns>
        public List<CompanyEntity> GetUserPowerTree()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM AM_Base_Company t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0  ");
                var userinfo = LoginUserInfo.Get();
                if (userinfo != null)
                {
                    if (!userinfo.isSystem && userinfo.account != "admin")
                    {
                        List<string> userPowers_CompanyIds = userinfo.userPowers_CompanyIds;
                        if (userPowers_CompanyIds == null || userPowers_CompanyIds.Count == 0)
                        {
                            strSql.Append(" and t.F_CompanyId=''");
                        }
                        else
                        {
                            string ids = Str.StrArray2Strin(userPowers_CompanyIds);
                            strSql.Append(" and t.F_CompanyId in(" + ids + ") ");
                        }
                    }
                }
                strSql.Append(" ORDER BY t.F_ParentId,t.F_FullName ");
                return (List<CompanyEntity>)this.BaseRepository().FindList<CompanyEntity>(strSql.ToString());
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
        /// 获取公司列表信息（全部）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM AM_Base_Company t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0  ORDER BY t.F_CreateDate desc");
                return this.BaseRepository().FindList<CompanyEntity>(strSql.ToString());
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
        /// 虚拟删除公司
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                CompanyEntity entity = new CompanyEntity()
                {
                    F_CompanyId = keyValue,
                    F_DeleteMark = 1
                };
                this.BaseRepository().Update(entity);
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
        /// 保存公司表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="companyEntity">公司实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CompanyEntity companyEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    companyEntity.Modify(keyValue);
                    this.BaseRepository().Update(companyEntity);
                }
                else
                {
                    companyEntity.Create();
                    this.BaseRepository().Insert(companyEntity);
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
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }

        } 
        #endregion
    }
}
