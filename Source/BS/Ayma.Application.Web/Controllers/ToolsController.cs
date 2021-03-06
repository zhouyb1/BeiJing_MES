﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.Excel;
using Ayma.Application.Organization;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Application.TwoDevelopment.Tools;
using Ayma.Util;

namespace Ayma.Application.Web.Controllers
{
    public class ToolsController : MvcControllerBase
    {
        private ExcelImportIBLL excelImportIBLL = new ExcelImportBLL();
        private AnnexesFileIBLL annexesFileIBLL = new AnnexesFileBLL();
        private ToolsIBLL toosIBLL = new ToolsBLL();
        #region 导入
        #region excel文件导入（班次表）
        /// <summary>
        /// excel文件导入（班次表）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>    
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteImportClassExcel(string templateId, string fileId, int chunks, string ext)
        {
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);

                List<Mes_ClassEntity> listDataPost = null;//返回前端的商品调价表体数据
                var res = excelImportIBLL.ImportClassTable(fileId, dt, ref listDataPost);
                var data = new
                {
                    Success = res.Split('|')[0],
                    Fail = Convert.ToInt32(res.Split('|')[1]),
                    Data = listDataPost
                };
                return Success(data);
            }
            else
            {
                return Fail("导入数据失败!");
            }
        }  
        #endregion

        #region excel文件导入（供应商表）
        /// <summary>
        /// excel文件导入（供应商表）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>    
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteImportSupplyExcel(string templateId, string fileId, int chunks, string ext)
        {
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);

                List<Mes_SupplyEntity> listDataPost = null;//返回前端的商品调价表体数据
                var res = excelImportIBLL.ImportSupplyTable(fileId, dt, ref listDataPost);
                var data = new
                {
                    Success = res.Split('|')[0],
                    Fail = Convert.ToInt32(res.Split('|')[1]),
                    Data = listDataPost
                };
                return Success(data);
            }
            else
            {
                return Fail("导入数据失败!");
            }
        } 
        #endregion

        #region excel文件导入（仓库表）
        /// <summary>
        /// excel文件导入（仓库表）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>    
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteImportStockExcel(string templateId, string fileId, int chunks, string ext)
        {
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);

                List<Mes_StockEntity> listDataPost = null;//返回前端的商品调价表体数据
                var res = excelImportIBLL.ImportStockTable(fileId, dt, ref listDataPost);
                var data = new
                {
                    Success = res.Split('|')[0],
                    Fail = Convert.ToInt32(res.Split('|')[1]),
                    Data = listDataPost
                };
                return Success(data);
            }
            else
            {
                return Fail("导入数据失败!");
            }
        }  
        #endregion

        #region excel文件导入（用户表）

        /// <summary>
        /// excel文件导入（用户表导入）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>    
        /// <param name="companyId">公司Id</param>    
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteImportUserExcel(string templateId, string fileId, int chunks, string ext, string companyId)
        {
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);

                List<UserEntity> listDataPost = null;//返回前端的商品调价表体数据
                var res = excelImportIBLL.ImportUserTable(fileId, dt, ref listDataPost, companyId);
                var data = new
                {
                    Success = res.Split('|')[0],
                    Fail = Convert.ToInt32(res.Split('|')[1]),
                    Data = listDataPost
                };
                return Success(data);
            }
            else
            {
                return Fail("导入数据失败!");
            }
        }    
        #endregion

        #region excel文件导入（物料表导入）
        /// <summary>
        /// excel文件导入（物料表导入）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>      
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteImportGoodsExcel(string templateId, string fileId, int chunks, string ext)
        {
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);

                List<Mes_GoodsEntity> listDataPost = null;//返回前端的商品调价表体数据
                var res = excelImportIBLL.ImportGoodsTable(fileId, dt, ref listDataPost);
                var data = new
                {
                    Success = res.Split('|')[0],
                    Fail = Convert.ToInt32(res.Split('|')[1]),
                    Data = listDataPost
                };
                return Success(data);
            }
            else
            {
                return Fail("导入数据失败!");
            }
        }    
        #endregion

        /// <summary>
        /// excel文件导入（配方表）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>      
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteImportBomHeadExcel(string templateId, string fileId, int chunks, string ext)
        {
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);

                List<Mes_BomRecordEntity> listDataPost = null;//返回前端的商品调价表体数据
                var res = excelImportIBLL.ImportBomRecordTable(fileId, dt, ref listDataPost);
                var data = new
                {
                    Success = res.Split('|')[0],
                    Fail = Convert.ToInt32(res.Split('|')[1]),
                    Data = listDataPost
                };
                return Success(data);
            }
            else
            {
                return Fail("导入数据失败!");
            }
        }    

        #endregion

        #region 通用方法 获取数据
        #endregion
    }
}