/*******************************************************************************
 * Copyright © 2018 Sfliao.Framework 版权所有
 * Author: Sfliao
 * Description: 桌面应用快速开发
 
*********************************************************************************/

using System;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using System.IO;
using System.Windows.Forms;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;

namespace Tools
{
    public class NPOIExcel
    {
        private string _title;
        private string _sheetName;
        private string _filePath;

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool ToExcel(DataTable table)
        {
            FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            IWorkbook workBook = new HSSFWorkbook();
            this._sheetName = this._sheetName.IsEmpty() ? "sheet1" : this._sheetName;
            ISheet sheet = workBook.CreateSheet(this._sheetName);

            //处理表格标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(this._title);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
            row.Height = 500;

            ICellStyle cellStyle = workBook.CreateCellStyle();
            IFont font = workBook.CreateFont();
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 17;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;

            //处理表格列头
            row = sheet.CreateRow(1);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                row.Height = 350;
                sheet.AutoSizeColumn(i);
            }

            //处理数据内容
            for (int i = 0; i < table.Rows.Count; i++)
            {
                row = sheet.CreateRow(2 + i);
                row.Height = 250;
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(table.Rows[i][j].ToString());
                    sheet.SetColumnWidth(j, 256 * 15);
                }
            }

            //写入数据流
            workBook.Write(fs);
            fs.Flush();
            fs.Close();

            return true;
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <param name="title"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public bool ToExcel(DataTable table, string title, string sheetName, string filePath)
        {
            this._title = title;
            this._sheetName = sheetName;
            this._filePath = filePath;
            return ToExcel(table);
        }


        //------------【函数：将表格控件保存至Excel文件(添加/新建)】------------    
        //filePath要保存的目标Excel文件路径名
        //datagGridView要保存至Excel的表格控件
        //------------------------------------------------
        public  bool SaveToExcelAdd(string filePath, DataGridView dataGridView)
        {
            bool result = true;

            FileStream fs = null;//创建一个新的文件流
            HSSFWorkbook workbook = null;//创建一个新的Excel文件
            ISheet sheet = null;//为Excel创建一张工作表

            //定义行数、列数、与当前Excel已有行数
            int rowCount = dataGridView.RowCount;//记录表格中的行数
            int colCount = dataGridView.ColumnCount;//记录表格中的列数
            int numCount = 0;//Excell最后一行序号

            //为了防止出错这里应该判断文件夹是否存在

            //判断文件是否存在
            if (!File.Exists(filePath))
            {
                try
                {
                    fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet1");
                    IRow row = sheet.CreateRow(0);
                    for (int j = 0; j < colCount; j++)  //列循环
                    {
                        if (dataGridView.Columns[j].Visible && dataGridView.Rows[0].Cells[j].Value != null)
                        {
                            ICell cell = row.CreateCell(j);//创建列
                            cell.SetCellValue(dataGridView.Columns[j].HeaderText.ToString());//更改单元格值                  
                        }
                    }
                    workbook.Write(fs);
                }
                catch
                {
                    result = false;
                    return result;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                    workbook = null;
                }
            }
            //创建指向文件的工作表
            try
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                workbook = new HSSFWorkbook(fs);//.xls
                sheet = workbook.GetSheetAt(0);
                if (sheet == null)
                {
                    result = false;
                    return result;
                }
                numCount = sheet.LastRowNum + 1;
            }
            catch
            {
                result = false;
                return result;
            }

            for (int i = 0; i < rowCount; i++)      //行循环
            {
                //防止行数超过Excel限制
                if (numCount + i >= 65536)
                {
                    result = false;
                    break;
                }
                IRow row = sheet.CreateRow(numCount + i);  //创建行
                for (int j = 0; j < colCount; j++)  //列循环
                {
                    if (dataGridView.Columns[j].Visible && dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        ICell cell = row.CreateCell(j);//创建列
                        cell.SetCellValue(dataGridView.Rows[i].Cells[j].Value.ToString());//更改单元格值                  
                    }
                }
            }
            try
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                workbook.Write(fs);
            }
            catch
            {
                result = false;
                return result;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                workbook = null;
            }
            return result;
        }


        //------------【函数：将表格控件保存至Excel文件(新建/替换)】------------    

        //filePath要保存的目标Excel文件路径名
        //datagGridView要保存至Excel的表格控件
        //------------------------------------------------------------------------
        public bool SaveToExcelNew(string filePath, DataGridView dataGridView)
        {
            bool result = true;

            FileStream fs = null;//创建一个新的文件流
            HSSFWorkbook workbook = null;//创建一个新的Excel文件
            ISheet sheet = null;//为Excel创建一张工作表

            //定义行数、列数、与当前Excel已有行数
            int rowCount = dataGridView.RowCount;//记录表格中的行数
            int colCount = dataGridView.ColumnCount;//记录表格中的列数

            //为了防止出错，这里应该判定一下文件与文件是否存在

            //创建工作表
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                workbook = new HSSFWorkbook();
                sheet = workbook.CreateSheet("Sheet1");
                IRow row = sheet.CreateRow(0);
                for (int j = 0; j < colCount; j++)  //列循环
                {
                    if (dataGridView.Columns[j].Visible && dataGridView.Rows[0].Cells[j].Value != null)
                    {
                        ICell cell = row.CreateCell(j);//创建列
                        cell.SetCellValue(dataGridView.Columns[j].HeaderText.ToString());//更改单元格值                  
                    }
                }
            }
            catch
            {
                result = false;
                return result;
            }

            for (int i = 0; i < rowCount; i++)      //行循环
            {
                //防止行数超过Excel限制
                if (i >= 65536)
                {
                    result = false;
                    break;
                }
                IRow row = sheet.CreateRow(1 + i);  //创建行
                for (int j = 0; j < colCount; j++)  //列循环
                {
                    if (dataGridView.Columns[j].Visible && dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        ICell cell = row.CreateCell(j);//创建列
                        cell.SetCellValue(dataGridView.Rows[i].Cells[j].Value.ToString());//更改单元格值                  
                    }
                }
            }
            try
            {
                workbook.Write(fs);
            }
            catch
            {
                result = false;
                return result;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                workbook = null;
            }
            return result;
        }
    }
}
