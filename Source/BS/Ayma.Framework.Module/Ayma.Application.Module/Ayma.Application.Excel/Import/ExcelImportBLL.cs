using Dapper;
using Ayma.Application.Base.SystemModule;
using Ayma.Cache.Base;
using Ayma.Cache.Factory;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Application.TwoDevelopment.Tools;
using Ayma.DataBase.Repository;


namespace Ayma.Application.Excel
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.04.01
    /// 描 述：Excel数据导入设置
    /// </summary>ImportTable
    public class ExcelImportBLL : ExcelImportIBLL
    {
        private ExcelImportService excelImportService = new ExcelImportService();
        private DatabaseTableIBLL databaseTableIBLL = new DatabaseTableBLL();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();

        private DataItemIBLL dataItemIBLL = new DataItemBLL();
        private DataSourceIBLL dataSourceIBLL = new DataSourceBLL();
        private ToolsIBLL toolsIbll = new ToolsBLL();

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "ayma_adms_excelError_";       // +公司主键
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件参数</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return excelImportService.GetPageList(pagination, queryJson);
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
        /// 获取导入配置列表根据模块ID
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportEntity> GetList(string moduleId)
        {
            try
            {
                return excelImportService.GetList(moduleId);
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
        /// 获取配置信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public ExcelImportEntity GetEntity(string keyValue)
        {
            try
            {
                return excelImportService.GetEntity(keyValue);
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
        /// 获取配置字段列表
        /// </summary>
        /// <param name="importId">配置信息主键</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportFieldEntity> GetFieldList(string importId)
        {
            try
            {
                return excelImportService.GetFieldList(importId);
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
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                excelImportService.DeleteEntity(keyValue);
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体数据</param>
        /// <param name="filedList">字段列表</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, ExcelImportEntity entity, List<ExcelImportFieldEntity> filedList)
        {
            try
            {
                excelImportService.SaveEntity(keyValue, entity, filedList);
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
        /// 更新配置主表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void UpdateEntity(string keyValue, ExcelImportEntity entity)
        {
            try
            {
                excelImportService.UpdateEntity(keyValue, entity);
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

        #region 扩展方法


        /// <summary>
        /// excel 数据导入（未导入数据写入缓存）
        /// </summary>
        /// <param name="templateId">导入模板主键</param>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <returns></returns>
        public string ImportTable(string templateId, string fileId, DataTable dt)
        {
            int snum = 0;
            int fnum = 0;
            try
            {
                if (dt.Rows.Count > 0)
                {
                    ExcelImportEntity entity = GetEntity(templateId);
                    List<ExcelImportFieldEntity> list = (List<ExcelImportFieldEntity>)GetFieldList(templateId);
                    if (entity != null && list.Count > 0)
                    {
                        UserInfo userInfo = LoginUserInfo.Get();
                        // 获取当前表的所有字段
                        IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(entity.F_DbId, entity.F_DbTable);
                        Dictionary<string, string> fieldMap = new Dictionary<string, string>();
                        foreach (var field in fieldList)// 遍历字段设置每个字段的数据类型
                        {
                            fieldMap.Add(field.f_column, field.f_datatype);
                        }
                        // 拼接导入sql语句
                        string sql = " INSERT INTO " + entity.F_DbTable + " (";
                        string sqlValue = "(";
                        bool isfirt = true;

                        foreach (var field in list)
                        {
                            if (!isfirt)
                            {
                                sql += ",";
                                sqlValue += ",";
                            }
                            sql += field.F_Name;
                            sqlValue += "@" + field.F_Name;
                            isfirt = false;
                        }
                        sql += " ) VALUES " + sqlValue + ")";
                        string sqlonly = " select * from " + entity.F_DbTable + " where ";

                        // 创建一个datatable容器用于保存导入失败的数据
                        DataTable failDt = new DataTable();
                        dt.Columns.Add("导入错误", typeof(string));
                        foreach (DataColumn dc in dt.Columns)
                        {
                            failDt.Columns.Add(dc.ColumnName, dc.DataType);
                        }

                        // 数据字典数据
                        Dictionary<string, List<DataItemDetailEntity>> dataItemMap = new Dictionary<string, List<DataItemDetailEntity>>();
                        // 循环遍历导入
                        foreach (DataRow dr in dt.Rows)
                        {

                            try
                            {
                                var dp = new DynamicParameters(new { });
                                foreach (var col in list)
                                {
                                    string paramName = "@" + col.F_Name;
                                    DbType dbType = FieldTypeHepler.ToDbType(fieldMap[col.F_Name]);

                                    switch (col.F_RelationType)
                                    {
                                        case 0://无关联
                                            dp.Add(col.F_Name, dr[col.F_ColName].ToString(), dbType);
                                            IsOnlyOne(col, sqlonly, dr[col.F_ColName].ToString(), entity.F_DbId, dbType);
                                            break;
                                        case 1://GUID
                                            dp.Add(col.F_Name, Guid.NewGuid().ToString(), dbType);
                                            break;
                                        case 2://数据字典
                                            string dataItemName = "";
                                            if (!dataItemMap.ContainsKey(col.F_DataItemCode))
                                            {
                                                List<DataItemDetailEntity> dataItemList = dataItemIBLL.GetDetailList(col.F_DataItemCode);
                                                dataItemMap.Add(col.F_DataItemCode, dataItemList);
                                            }
                                            dataItemName = FindDataItemValue(dataItemMap[col.F_DataItemCode], dr[col.F_ColName].ToString(), col.F_ColName);
                                            dp.Add(col.F_Name, dataItemName, dbType);
                                            IsOnlyOne(col, sqlonly, dataItemName, entity.F_DbId, dbType);
                                            break;
                                        case 3://数据表
                                            string v = "";
                                            try
                                            {
                                                string[] dataSources = col.F_DataSourceId.Split(',');
                                                string strWhere = " " + dataSources[1] + " =@" + dataSources[1];
                                                string queryJson = "{" + dataSources[1] + ":\"" + dr[col.F_ColName].ToString() + "\"}";
                                                DataTable sourceDt = dataSourceIBLL.GetDataTable(dataSources[0], strWhere, queryJson);
                                                v = sourceDt.Rows[0][0].ToString();
                                                dp.Add(col.F_Name, v, dbType);
                                            }
                                            catch (Exception)
                                            {
                                                throw (new Exception("【" + col.F_ColName + "】 找不到对应的数据"));
                                            }
                                            IsOnlyOne(col, sqlonly, v, entity.F_DbId, dbType);
                                            break;
                                        case 4://固定值
                                            dp.Add(col.F_Name, col.F_Value, dbType);
                                            break;
                                        case 5://操作人ID
                                            dp.Add(col.F_Name, userInfo.userId, dbType);
                                            break;
                                        case 6://操作人名字
                                            dp.Add(col.F_Name, userInfo.realName, dbType);
                                            break;
                                        case 7://操作时间
                                            dp.Add(col.F_Name, DateTime.Now, dbType);
                                            break;
                                    }
                                }
                                databaseLinkIBLL.ExecuteBySql(entity.F_DbId, sql, dp);
                                snum++;
                            }
                            catch (Exception ex)
                            {
                                fnum++;
                                if (entity.F_ErrorType == 0)// 如果错误机制是终止
                                {
                                    dr["导入错误"] = ex.Message + "【之后数据未被导入】";
                                    failDt.Rows.Add(dr.ItemArray);
                                    break;
                                }
                                else
                                {
                                    dr["导入错误"] = ex.Message;
                                    failDt.Rows.Add(dr.ItemArray);
                                }
                            }
                        }

                        // 写入缓存如果有未导入的数据
                        if (failDt.Rows.Count > 0)
                        {
                            string errordt = failDt.ToJson();

                            cache.Write<string>(cacheKey + fileId, errordt, CacheId.excel);
                        }
                    }
                }


                return snum + "|" + fnum;
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
        /// excel 数据导入（班次表导入）
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        public string ImportClassTable(string fileId, DataTable dt, ref List<Mes_ClassEntity> listData)
        {
            int snum = 0;
            int fnum = 0;
            try
            {
                List<Mes_ClassEntity> listModel = new List<Mes_ClassEntity>();//返回前端的数据
                if (dt.Rows.Count > 0)
                {
                    // 创建一个datatable容器用于保存导入失败的数据
                    DataTable failDt = new DataTable();
                    dt.Columns.Add("导入错误", typeof(string));
                    foreach (DataColumn dc in dt.Columns)
                    {
                        failDt.Columns.Add(dc.ColumnName, dc.DataType);
                    }

                    // 循环遍历导入
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            var code = dr["编码"].ToString();//编码
                            var name = dr["名称"].ToString();//名称
                            var isCode = toolsIbll.IsCode("Mes_Class", "C_Code", code, "");//查询编码重复性
                            var isName = toolsIbll.IsName("Mes_Class", "C_Name", name, "");//查询名称重复性
                            if (isCode)
                            {
                                fnum++;
                                dr["导入错误"] = "编码【" + code + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            if (isName)
                            {
                                fnum++;
                                dr["导入错误"] = "名称【" + name + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            var startTime = dr["开始时间（时:分:秒）"].ToString();//开始时间
                            var endTime = dr["结束时间（时:分:秒）"].ToString();//结束时间
                            var remark = dr["备注"].ToString();//备注

                            var model = new Mes_ClassEntity()
                            {
                                C_Code = code,
                                C_Name = name,
                                C_StartTime = startTime,
                                C_EndTime = endTime,
                                C_Remark = remark

                            };
                            listModel.Add(model);
                            model.Create();
                            new RepositoryFactory().BaseRepository().Insert(model);
                            snum++;
                        }
                        catch (Exception ex)
                        {
                            fnum++;
                            dr["导入错误"] = "格式有误";
                            failDt.Rows.Add(dr.ItemArray);

                        }
                    }

                    // 写入缓存如果有未导入的数据
                    if (failDt.Rows.Count > 0)
                    {
                        string errordt = failDt.ToJson();

                        cache.Write<string>(cacheKey + fileId, errordt, CacheId.excel);
                    }

                }
                listData = listModel;

                return snum + "|" + fnum;
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
        /// excel 数据导入（供应商导入）
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        public string ImportSupplyTable(string fileId, DataTable dt, ref List<Mes_SupplyEntity> listData)
        {
            int snum = 0;
            int fnum = 0;
            try
            {
                List<Mes_SupplyEntity> listModel = new List<Mes_SupplyEntity>();//返回前端的数据
                if (dt.Rows.Count > 0)
                {
                    // 创建一个datatable容器用于保存导入失败的数据
                    DataTable failDt = new DataTable();
                    dt.Columns.Add("导入错误", typeof(string));
                    foreach (DataColumn dc in dt.Columns)
                    {
                        failDt.Columns.Add(dc.ColumnName, dc.DataType);
                    }

                    // 循环遍历导入
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            var code = dr["供应商编码"].ToString();//编码
                            var name = dr["供应商名称"].ToString();//名称
                            var isCode = toolsIbll.IsCode("Mes_Supply", "S_Code", code, "");//查询编码重复性
                            var isName = toolsIbll.IsName("Mes_Supply", "S_Name", name, "");//查询名称重复性
                            if (isCode)
                            {
                                fnum++;
                                dr["导入错误"] = "供应商编码【" + code + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            if (isName)
                            {
                                fnum++;
                                dr["导入错误"] = "供应商名称【" + name + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            var effectTime = dr["资质期限（年-月-日）"].ToDate();//资质期限
                            var remark = dr["备注"].ToString();//备注

                            var model = new Mes_SupplyEntity()
                            {
                                S_Code = code,
                                S_Name = name,
                                S_EffectTime = effectTime,
                                S_Remark = remark
                            };
                            listModel.Add(model);
                            model.Create();
                            new RepositoryFactory().BaseRepository().Insert(model);
                            snum++;
                        }
                        catch (Exception ex)
                        {
                            fnum++;
                            dr["导入错误"] = "格式有误";
                            failDt.Rows.Add(dr.ItemArray);

                        }
                    }

                    // 写入缓存如果有未导入的数据
                    if (failDt.Rows.Count > 0)
                    {
                        string errordt = failDt.ToJson();

                        cache.Write<string>(cacheKey + fileId, errordt, CacheId.excel);
                    }

                }
                listData = listModel;

                return snum + "|" + fnum;
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
        /// excel 数据导入（仓库表导入）
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        public string ImportStockTable(string fileId, DataTable dt, ref List<Mes_StockEntity> listData)
        {
            int snum = 0;
            int fnum = 0;
            try
            {
                List<Mes_StockEntity> listModel = new List<Mes_StockEntity>();//返回前端的数据
                if (dt.Rows.Count > 0)
                {
                    // 创建一个datatable容器用于保存导入失败的数据
                    DataTable failDt = new DataTable();
                    dt.Columns.Add("导入错误", typeof(string));
                    foreach (DataColumn dc in dt.Columns)
                    {
                        failDt.Columns.Add(dc.ColumnName, dc.DataType);
                    }

                    // 循环遍历导入
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            var code = dr["仓库编码"].ToString();//编码
                            var name = dr["仓库名称"].ToString();//名称
                            var isCode = toolsIbll.IsCode("Mes_Stock", "S_Code", code, "");//查询编码重复性
                            var isName = toolsIbll.IsName("Mes_Stock", "S_Name", name, "");//查询名称重复性
                            if (isCode)
                            {
                                fnum++;
                                dr["导入错误"] = "仓库编码【" + code + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            if (isName)
                            {
                                fnum++;
                                dr["导入错误"] = "仓库名称【" + name + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            var type = dr["仓库类型"].ToString();//仓库类型
                            var dataItemList = dataItemIBLL.GetDetailList("StockType");
                            DataItemDetailEntity dataItem = dataItemList.Find(t => t.F_ItemName == type);
                            var stockType = "";
                            if (dataItem != null)
                            {
                                stockType= dataItem.F_ItemValue;
                            }
                            else
                            {
                                fnum++;
                                dr["导入错误"] = "仓库类型【" + type + "】不存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            var person = dr["仓库负责人"].ToString();//仓库负责人
                            var remark = dr["备注"].ToString();//备注

                            var model = new Mes_StockEntity()
                            {
                                S_Code = code,
                                S_Name = name,
                                S_Peson = person,
                                S_Kind = Convert.ToInt32(stockType),
                                S_Remark = remark
                            };
                            listModel.Add(model);
                            model.Create();
                            new RepositoryFactory().BaseRepository().Insert(model);
                            snum++;
                        }
                        catch (Exception ex)
                        {
                            fnum++;
                            dr["导入错误"] = "格式有误";
                            failDt.Rows.Add(dr.ItemArray);

                        }
                    }

                    // 写入缓存如果有未导入的数据
                    if (failDt.Rows.Count > 0)
                    {
                        string errordt = failDt.ToJson();

                        cache.Write<string>(cacheKey + fileId, errordt, CacheId.excel);
                    }

                }
                listData = listModel;

                return snum + "|" + fnum;
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
        /// 获取excel导入的错误数据
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <returns></returns>
        public DataTable GetImportError(string fileId)
        {
            try
            {
                string strdt = cache.Read<string>(cacheKey + fileId, CacheId.excel);
                DataTable dt = strdt.ToObject<DataTable>();
                cache.Remove(cacheKey + fileId, CacheId.excel);
                return dt;
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
        /// 数据字典查找Value
        /// </summary>
        /// <param name="dataItemList">数据字典数据</param>
        /// <param name="itemName">项目名</param>
        /// <param name="colName">列名</param>
        /// <returns></returns>
        private string FindDataItemValue(List<DataItemDetailEntity> dataItemList, string itemName, string colName)
        {
            DataItemDetailEntity dataItem = dataItemList.Find(t => t.F_ItemName == itemName);
            if (dataItem != null)
            {
                return dataItem.F_ItemValue;
            }
            else
            {
                throw (new Exception("【" + colName + "】数据字典找不到对应值"));
            }
        }

        /// <summary>
        /// 判断是否数据有重复
        /// </summary>
        /// <param name="col"></param>
        /// <param name="sqlonly"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        private void IsOnlyOne(ExcelImportFieldEntity col, string sqlonly, string value, string dbId, DbType dbType)
        {
            if (col.F_OnlyOne == 1)
            {
                var dp = new DynamicParameters(new { });
                sqlonly += col.F_Name + " = @" + col.F_Name;
                dp.Add(col.F_Name, value, dbType);
                var d = databaseLinkIBLL.FindTable(dbId, sqlonly, dp);
                if (d.Rows.Count > 0)
                {
                    throw new Exception("【" + col.F_ColName + "】此项数据不能重复");
                }
            }
        }
        #endregion
    }
}
