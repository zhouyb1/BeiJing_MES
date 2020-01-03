using Ayma.Cache.Base;
using Ayma.Cache.Factory;
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
        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "ayma_bom_"; // +父级Id
        #endregion
        /// <summary>
        /// 获取配方列表数据
        /// </summary>
        /// <returns></returns>
        public List<Mes_BomRecordEntity> GetBomRecordTreeList(string parentId)
        {
            try
            {
                if (string.IsNullOrEmpty(parentId))
                {
                    parentId = "0";
                }
                List<Mes_BomRecordEntity> list = cache.Read<List<Mes_BomRecordEntity>>(cacheKey + parentId,CacheId.area);
                if (list == null)
                {
                    list = (List<Mes_BomRecordEntity>)bomHeadService.GetBomRecordTreeList(parentId);
                    cache.Write<List<Mes_BomRecordEntity>>(cacheKey + parentId, list, CacheId.area);
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
        /// 获取区域列表数据
        /// </summary>
        /// <param name="parentId">父节点主键（0表示顶层）</param>
        /// <param name="keyword">关键字查询（名称/编号）</param>
        /// <returns></returns>
        public List<Mes_BomRecordEntity> GetBomRecordTreeList(string parentId, string keyword)
        {
            try
            {
                List<Mes_BomRecordEntity> list = GetBomRecordTreeList(parentId);
                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.FindAll(t => t.B_GoodsCode.Contains(keyword) || t.B_GoodsName.Contains(keyword));
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
                throw;
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
        /// <summary>
        /// 获取区域数据树（某一级的）
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public List<TreeModel> GetTree(string parentId)
        {
            try
            {
                List<TreeModel> treeList = new List<TreeModel>();
                List<Mes_BomRecordEntity> list = GetBomRecordTreeList(parentId);

                foreach (var item in list)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.ID;
                    node.text = item.B_GoodsName;
                    node.value = item.B_GoodsCode;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.hasChildren = GetBomRecordTreeList(item.B_ParentID).Count > 0 ? true : false;
                    node.isexpand = false;
                    node.complete = false;
                    treeList.Add(node);
                }
                return treeList;
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
                throw;
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
