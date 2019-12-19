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
using Ayma.Application.Organization;
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
        private DepartmentService departmentService = new DepartmentService();
        private UserIBLL userIbll = new UserBLL();
        private ToolsService toolsService = new ToolsService();
        private BomHeadIBLL bomHeadIBLL = new BomHeadBLL();

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
                            DateTime? effectTime = null;

                            if (!dr["资质期限（年-月-日）"].ToString().IsEmpty())
                            {
                                effectTime = Convert.ToDateTime(dr["资质期限（年-月-日）"]);//资质期限
                            }

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
                                stockType = dataItem.F_ItemValue;
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
        /// excel 数据导入（用户表导入）
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <param name="companyId">公司IdS</param>
        /// <returns></returns>
        public string ImportUserTable(string fileId, DataTable dt, ref List<UserEntity> listData, string companyId)
        {
            int snum = 0;
            int fnum = 0;
            try
            {
                List<UserEntity> listModel = new List<UserEntity>();//返回前端的数据
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
                            var enCode = dr["工号"].ToString();//工号
                            var account = dr["登录账户"].ToString();//登录账户
                            var isCode = userIbll.GetEntityBy(enCode);//查询工号重复性
                            var isName = userIbll.GetEntityBy(account);//查询账号重复性
                            if (isCode != null)
                            {
                                fnum++;
                                dr["导入错误"] = "工号【" + enCode + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            if (isName != null)
                            {
                                fnum++;
                                dr["导入错误"] = "登录账户【" + account + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            var name = dr["真实姓名"].ToString();//真实姓名
                            var genderType = dr["性别"].ToString();//性别
                            var gender = 0;
                            if (genderType == "男")
                            {
                                gender = 1;
                            }

                            DateTime? birthday = null;
                            if (!dr["生日"].ToString().IsEmpty())
                            {
                                birthday = Convert.ToDateTime(dr["生日"]);//生日
                            }
                            var mobile = dr["手机"].ToString();//手机
                            var telephone = dr["电话"].ToString();//电话
                            var email = dr["电子邮件"].ToString();//电子邮件
                            var description = dr["备注"].ToString();//备注
                            var address = dr["地址"].ToString();//地址
                            var department = dr["所属部门"].ToString();//所属部门
                            var list = (List<DepartmentEntity>)departmentService.GetList(companyId);
                            var departmentEntity = list.Where(c => c.F_FullName == department).FirstOrDefault();
                            var departmentId = "";
                            if (departmentEntity != null)
                            {
                                departmentId = departmentEntity.F_DepartmentId;
                            }
                            else
                            {
                                fnum++;
                                dr["导入错误"] = "部门【" + department + "】不存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;

                            }
                            var kind = dr["员工类型(正式工,临时工,劳务工)"].ToString();//员工类型
                            var dataItemList = dataItemIBLL.GetDetailList("EmployeeKind");
                            DataItemDetailEntity dataItem = dataItemList.Find(t => t.F_ItemName == kind);
                            var kindcode = 0;
                            if (dataItem != null)
                            {
                                kindcode = Convert.ToInt32(dataItem.F_ItemValue);
                            }
                            else
                            {
                                fnum++;
                                dr["导入错误"] = "员工类型【" + kind + "】不存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            var rfidCode = dr["RFID编码"].ToString();//RFID编码
                            DateTime? indate = null;
                            if (!dr["入职日期"].ToString().IsEmpty())
                            {
                                indate = Convert.ToDateTime(dr["入职日期"]);//入职日期
                            }
                            DateTime? outdate = null;
                            if (!dr["离职日期"].ToString().IsEmpty())
                            {
                                outdate = Convert.ToDateTime(dr["离职日期"]);//离职日期
                            }

                            var cert = dr["身份证"].ToString();//身份证
                            var nation = dr["民族"].ToString();//民族
                            var record = dr["学历"].ToString();//学历
                            var origin = dr["籍贯"].ToString();//籍贯
                            var status = dr["在职状态(待入职,在职,待离职,离职)"].ToString();//在职状态
                            var dataItemListStatus = dataItemIBLL.GetDetailList("JobStatus");
                            DataItemDetailEntity dataItemStatus = dataItemListStatus.Find(t => t.F_ItemName == status);
                            var statuscode = 0;
                            if (dataItemStatus != null)
                            {
                                statuscode = Convert.ToInt32(dataItemStatus.F_ItemValue);
                            }
                            else
                            {
                                fnum++;
                                dr["导入错误"] = "在职状态【" + status + "】不存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            DateTime? contract = null;
                            if (!dr["合同到期时间"].ToString().IsEmpty())
                            {
                                contract = Convert.ToDateTime(dr["合同到期时间"]);//合同到期时间
                            }



                            var model = new UserEntity()
                            {
                                F_EnCode = enCode,
                                F_Account = account,
                                F_RealName = name,
                                F_Gender = gender,
                                F_Birthday = birthday,
                                F_Mobile = mobile,
                                F_Telephone = telephone,
                                F_Email = email,
                                F_Description = description,
                                U_Address = address,
                                F_DepartmentId = departmentId,
                                F_Kind = kindcode,
                                F_RFIDCode = rfidCode,
                                F_Indate = indate,
                                F_Outdate = outdate,
                                F_Cert = cert,
                                F_Nation = nation,
                                F_Record = record,
                                F_Origin = origin,
                                F_Status = statuscode,
                                F_Contract = contract,
                                F_CompanyId = companyId
                            };
                            listModel.Add(model);
                            model.Create();
                            model.F_Secretkey = Md5Helper.Encrypt(CommonHelper.CreateNo(), 16).ToLower();
                            //密码默认：0000
                            model.F_Password = Md5Helper.Encrypt(DESEncrypt.Encrypt("4a7d1ed414474e4033ac29ccb8653d9b", model.F_Secretkey).ToLower(), 32).ToLower();
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
        /// excel 数据导入（物料表导入）
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        public string ImportGoodsTable(string fileId, DataTable dt, ref List<Mes_GoodsEntity> listData)
        {
            int snum = 0;
            int fnum = 0;
            try
            {
                List<Mes_GoodsEntity> listModel = new List<Mes_GoodsEntity>();//返回前端的数据
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
                            var code = dr["商品编码"].ToString();//商品编码
                            var name = dr["商品名称"].ToString();//商品名称
                            var isCode = toolsIbll.IsCode("Mes_Goods", "G_Code", code, "");//查询编码重复性
                            var isName = toolsIbll.IsName("Mes_Goods", "G_Name", name, "");//查询名称重复性
                            if (isCode)
                            {
                                fnum++;
                                dr["导入错误"] = "商品编码【" + code + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            if (isName)
                            {
                                fnum++;
                                dr["导入错误"] = "商品名称【" + name + "】已存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }

                            var kind = dr["商品类型(原料,半成品,成品)"].ToString();//商品类型

                            var dataItemList = dataItemIBLL.GetDetailList("GoodsType");
                            DataItemDetailEntity dataItem = dataItemList.Find(t => t.F_ItemName == kind);
                            var kindcode = 1;
                            if (dataItem != null)
                            {
                                kindcode = Convert.ToInt32(dataItem.F_ItemValue);
                            }
                            else
                            {
                                fnum++;
                                dr["导入错误"] = "商品类型【" + kind + "】不存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            int? period = null;
                            if (!dr["保质时间(天)"].ToString().IsEmpty())
                            {
                                period = Convert.ToInt32(dr["保质时间(天)"]);//保质时间(天)
                            }
                            decimal? price = 0;//价格
                            if (!dr["价格"].ToString().IsEmpty())
                            {
                                price = Convert.ToDecimal(dr["价格"]);
                            }


                            decimal? unitWeight = 0;//单位重量
                            if (!dr["单位重量"].ToString().IsEmpty())
                            {
                                unitWeight = Convert.ToDecimal(dr["单位重量"]);//单位重量
                            }
                            decimal? super = 0;//上限预警单位数量
                            if (!dr["上限预警单位数量"].ToString().IsEmpty())
                            {
                                super = Convert.ToDecimal(dr["上限预警单位数量"]);//上限预警单位数量
                            }
                            decimal? lower = 0;//下限预警单位数量
                            if (!dr["下限预警单位数量"].ToString().IsEmpty())
                            {
                                lower = Convert.ToDecimal(dr["下限预警单位数量"]);//下限预警单位数量
                            }

                            var tKind = dr["二级分类(肉食,蔬菜,调料,冷链,面条,糕点)"].ToString();//二级分类
                            var dataItemtKindList = dataItemIBLL.GetDetailList("GoodsTypeT");
                            DataItemDetailEntity dataItemtKind = dataItemtKindList.Find(t => t.F_ItemName == tKind);
                            string tkindcode = null;
                            if (dataItemtKind != null)
                            {
                                tkindcode = dataItemtKind.F_ItemValue.ToString();
                            }
                            else
                            {
                                fnum++;
                                dr["导入错误"] = "二级分类【" + kind + "】不存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }

                            var self = dr["是否自制"].ToString();//是否自制
                            int? selfcode = 0;
                            if (self == "是")
                            {
                                selfcode = 1;
                            }
                            else
                            {
                                selfcode = 0;
                            }
                            var online = dr["是否在用"].ToString();//是否在用
                            int? onlinecode = 0;
                            if (online == "是")
                            {
                                onlinecode = 1;
                            }
                            else
                            {
                                onlinecode = 0;
                            }

                            int? prepareday = null;
                            if (!dr["备料天数"].ToString().IsEmpty())
                            {
                                prepareday = Convert.ToInt32(dr["备料天数"]);//备料天数
                            }
                            decimal? otax = null;
                            if (!dr["销售税率"].ToString().IsEmpty())
                            {
                                otax = Convert.ToDecimal(dr["销售税率"]);//销售税率
                            }
                            decimal? itax = null;
                            if (!dr["购进税率"].ToString().IsEmpty())
                            {
                                itax = Convert.ToDecimal(dr["购进税率"]);//购进税率
                            }
                            var erpcode = dr["ERP中的编码(成品必填)"].ToString();//ERP中的编码(成品必填)
                            var unitQty = dr["包装规格"].ToDecimal();//包装规格
                            var unit = dr["单位"].ToString();//单位
                            var remark = dr["备注"].ToString();//备注
                            var model = new Mes_GoodsEntity()
                            {
                                G_Code = code,
                                G_Name = name,
                                G_Kind = (ErpEnums.GkindEnum)kindcode,
                                G_Period = period,
                                G_Price = price,
                                G_UnitWeight = unitWeight,
                                G_Super = super,
                                G_Lower = lower,
                                G_TKind = tkindcode,
                                G_UnitQty = unitQty,
                                G_Self = (ErpEnums.YesOrNoEnum)selfcode,
                                G_Online = (ErpEnums.YesOrNoEnum)onlinecode,
                                G_Prepareday = prepareday,
                                G_Otax = otax,
                                G_Itax = itax,
                                G_Erpcode = erpcode,
                                G_Unit = unit,
                                G_Remark = remark
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
        /// excel 数据导入（配方表导入）
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <param name="listData">返回前端的数据</param>
        /// <returns></returns>
        public string ImportBomRecordTable(string fileId, DataTable dt, ref List<Mes_BomRecordEntity> listData)
        {
            int snum = 0;
            int fnum = 0;
            try
            {
                List<Mes_BomRecordEntity> listModel = new List<Mes_BomRecordEntity>();//返回前端的数据
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
                            var formulaCode = dr["配方编码"].ToString();//配方编码
                            var formulaName = dr["配方名称"].ToString();//配方名称
                            #region 物料编码
                            var goodsCodeTemp = dr["物料编码(见物料表)"].ToString();//物料编码(见物料表)
                            var goodsList = toolsIbll.GetGoodsList();
                            var goodsEntity = goodsList.Where(c => c.G_Code == goodsCodeTemp).FirstOrDefault();
                            string goodsCode = null;
                            string goodsName = null;
                            if (goodsEntity != null)
                            {
                                goodsCode = goodsEntity.G_Code;
                                goodsName = toolsIbll.ByCodeGetGoodsEntity(goodsEntity.G_Code).G_Name;
                            }
                            else
                            {
                                fnum++;
                                dr["导入错误"] = "物料编码【" + goodsCodeTemp + "】不存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            } 
                            #endregion

                            #region 工艺代码
                            var recordCodeTemp = dr["工艺代码(见工序表)"].ToString();//工艺代码
                            var recordList = toolsIbll.GetRecordList();
                            var listCode = recordList.Where(c => c.R_Record == recordCodeTemp).FirstOrDefault();

                            string recordCode = null;
                            if (listCode != null)
                            {
                                recordCode = listCode.R_Record;//工艺代码
                            }
                            else
                            {
                                fnum++;
                                dr["导入错误"] = "工艺代码【" + recordCodeTemp + "】不存在";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            } 
                            #endregion

                            #region 上级
                            var parentGoodsCode = dr["上级物料编码"].ToString();//上级 物料编码
                            string parentId = "0";
                            if (!parentGoodsCode.IsEmpty())
                            {
                                if (parentGoodsCode == goodsCodeTemp)//上级与本级不能相同
                                {
                                    fnum++;
                                    dr["导入错误"] = "上级和本级的物料不能相同！";
                                    failDt.Rows.Add(dr.ItemArray);
                                    continue;
                                }
                                var bomRecordList = toolsService.GetBomRecordList();//配方列表数据

                                var bomRecord = bomRecordList.Where(c => c.B_GoodsCode == parentGoodsCode).FirstOrDefault();

                                if (bomRecord != null)
                                {
                                    parentId = bomRecord.ID;
                                }
                                else
                                {
                                    fnum++;
                                    dr["导入错误"] = "上级物料编码【" + parentGoodsCode + "】不存在";
                                    failDt.Rows.Add(dr.ItemArray);
                                    continue;
                                }
                            } 
                            #endregion

                            var isExistCode=bomHeadIBLL.ExistCode("", parentId, recordCode, formulaCode, goodsCode);
                            if (!isExistCode)
                            {
                                fnum++;
                                dr["导入错误"] = "该配方已存在!";
                                failDt.Rows.Add(dr.ItemArray);
                                continue;
                            }
                            
                            var qtyTemp = dr["数量"].ToString();//数量
                            decimal? qty= 0;
                            if (!qtyTemp.IsEmpty())
                            {
                                qty = Convert.ToDecimal(qtyTemp);
                            }


                            var availTemp = dr["是否启用"].ToString();//是否启用
                            ErpEnums.YesOrNoEnum? avail = null;
                            if (availTemp=="是")
                            {
                                avail = ErpEnums.YesOrNoEnum.Yes;
                            }
                            else
                            {
                                avail = ErpEnums.YesOrNoEnum.No;
                                
                            }
                            DateTime? startTime = null;

                            if (!dr["开始时间"].ToString().IsEmpty())
                            {
                                startTime = Convert.ToDateTime(dr["开始时间"]);//开始时间
                            }
                            DateTime? endTime = null;

                            if (!dr["结束时间"].ToString().IsEmpty())
                            {
                                endTime = Convert.ToDateTime(dr["结束时间"]);//结束时间
                            }
                            var unit = dr["单位"].ToString();//单位
                            var remark = dr["备注"].ToString();//备注
                            var model = new Mes_BomRecordEntity()
                            {
                                B_RecordCode = recordCode,
                                B_FormulaCode = formulaCode,
                                B_FormulaName = formulaName,
                                B_GoodsCode = goodsCode,
                                B_GoodsName = goodsName,
                                B_Unit=unit,
                                B_Qty=qty,
                                B_ParentID = parentId,
                                B_Avail = avail,
                                B_StartTime = startTime,
                                B_EndTime = endTime,
                                B_Remark = remark
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
