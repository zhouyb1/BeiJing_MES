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
using System.Text;
using System.Web;
using Ayma.Util;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;
using NPOI.HSSF.Util;

namespace Ayma.Util
{
    public class NPOIExcel
    {


        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream ToExcel(DataTable table, string title, string sheetName)
        {
            IWorkbook workBook = new HSSFWorkbook();
            string _sheetName = sheetName.IsEmpty() ? "sheet1" : sheetName;
            ISheet sheet = workBook.CreateSheet(_sheetName);

            //处理表格标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(title);
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

            using (MemoryStream ms = new MemoryStream())
            {
                workBook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }

        }


        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream ToExcelMoreheader(DataTable table, string title, string sheetName, string StartTime, string EndTime)
            {
            IWorkbook workBook = new HSSFWorkbook();
            string _sheetName = sheetName.IsEmpty() ? "sheet1" : sheetName;
            ISheet sheet = workBook.CreateSheet(_sheetName);

            //处理表格标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(title);        
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
            row.Height = 500;
            //表格标题的样式
            ICellStyle cellStyle = workBook.CreateCellStyle();
            IFont font = workBook.CreateFont();
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 17;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;

            //合并的样式
            ICellStyle cellStyle1 = workBook.CreateCellStyle();
            IFont font1 = workBook.CreateFont();
            font1.FontName = "宋体";
            font1.FontHeightInPoints = 11;
            cellStyle1.SetFont(font1);
            cellStyle1.VerticalAlignment = VerticalAlignment.Center;
            cellStyle1.Alignment = HorizontalAlignment.Center;

            //日期的样式
            ICellStyle cellStyle2 = workBook.CreateCellStyle();
            IFont font2 = workBook.CreateFont();
            font2.FontName = "宋体";
            font2.FontHeightInPoints = 11;
            cellStyle2.SetFont(font2);
            cellStyle2.VerticalAlignment = VerticalAlignment.Center;
            cellStyle2.Alignment = HorizontalAlignment.Left;
            if (title != "物料出成率列表")
            {
                //空第一行出来加日期
                row = sheet.CreateRow(1);
                sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, table.Columns.Count - 1));
                row.CreateCell(0).SetCellValue("日期：" + StartTime + "至" + EndTime);
                row.Cells[0].CellStyle = cellStyle2;
            }
            //空一行出来合并
            if (title == "原物料出入库统计")
            {
                #region 原物料出入库统计
                row = sheet.CreateRow(2);
                //合并
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 0, 0));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 1, 1));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 2, 2));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 3, 3));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 4, 4));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 5, 5));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 6, 6));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 7, 7));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 8, 8));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 9, 10));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 11, 12));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 13, 13));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 14, 14));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 15, 17));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 18, 18));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 19, 19));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 20, 20));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 21, 21));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 22, 22));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 23, 23));
                //赋列名
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                    row.Cells[i].CellStyle = cellStyle1;
                }
   
                row.CreateCell(9).SetCellValue("期初库存");
                row.CreateCell(11).SetCellValue("期末库存");
                row.CreateCell(15).SetCellValue("原物料销售");
                row.Cells[9].CellStyle = cellStyle1;
                row.Cells[11].CellStyle = cellStyle1;
                row.Cells[15].CellStyle = cellStyle1;
                #endregion
            }
            else if (title == "库存明细统计")
            {
                #region 库存明细统计
                row = sheet.CreateRow(2);
                //合并
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 0, 0));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 1, 1));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 2, 2));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 3, 3));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 4, 4));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 5, 5));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 6, 6));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 7, 10));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 11, 13));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 14, 16));  
                //赋列名
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                    row.Cells[i].CellStyle = cellStyle1;
                }
                row.CreateCell(7).SetCellValue("收入");
                row.CreateCell(11).SetCellValue("发出");
                row.CreateCell(14).SetCellValue("结存");
                row.Cells[7].CellStyle = cellStyle1;
                row.Cells[11].CellStyle = cellStyle1;
                row.Cells[14].CellStyle = cellStyle1;
                   
                #endregion
            }
            else if (title == "出成率报表-按原物料")
            {
                #region 出成率报表-按原物料
                row = sheet.CreateRow(2);
                //合并
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 0, 0));
                sheet.AddMergedRegion(new CellRangeAddress(2, 3, 1, 1));  
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 2, 4));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 5, 9));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 10, 14));
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 15, 20));
                //赋列名
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                    row.Cells[i].CellStyle = cellStyle1;
                }
                row.CreateCell(2).SetCellValue("原物料");
                row.CreateCell(5).SetCellValue("前处理");
                row.CreateCell(10).SetCellValue("细加工");
                row.CreateCell(15).SetCellValue("包装");
                row.Cells[2].CellStyle = cellStyle1;
                row.Cells[5].CellStyle = cellStyle1;
                row.Cells[10].CellStyle = cellStyle1;
                row.Cells[14].CellStyle = cellStyle1;
                #endregion
            }
            else if (title == "物料出成率列表")
            {
                #region 出成率实时查询
                row = sheet.CreateRow(1);
                //合并
                sheet.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
                sheet.AddMergedRegion(new CellRangeAddress(1, 1, 1, 4));
                sheet.AddMergedRegion(new CellRangeAddress(1, 1, 5, 8));
                sheet.AddMergedRegion(new CellRangeAddress(1, 2, 9, 9));
                sheet.AddMergedRegion(new CellRangeAddress(1, 2, 10,10 ));
                sheet.AddMergedRegion(new CellRangeAddress(1, 2, 11, 11));
                sheet.AddMergedRegion(new CellRangeAddress(1, 2, 12, 12));
                sheet.AddMergedRegion(new CellRangeAddress(1, 2, 13, 13));
                sheet.AddMergedRegion(new CellRangeAddress(1, 2, 14, 14));
                sheet.AddMergedRegion(new CellRangeAddress(1, 2, 15, 15));
                //赋列名
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                    row.Cells[i].CellStyle = cellStyle1;
                }
                row.CreateCell(1).SetCellValue("转换前");
                row.CreateCell(5).SetCellValue("转换后");
                row.Cells[1].CellStyle = cellStyle1;
                row.Cells[5].CellStyle = cellStyle1;
                //处理表格列头
                row = sheet.CreateRow(2);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                    row.Cells[i].CellStyle = cellStyle1;
                    row.Height = 300;
                    sheet.AutoSizeColumn(i);
                }
                //起始行号，终止行号， 起始列号，终止列号
                //处理数据内容
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = sheet.CreateRow(3 + i);
                    row.Height = 250;
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(table.Rows[i][j].ToString());
                        sheet.SetColumnWidth(j, 256 * 15);
                    }

                    double cell = row.GetCell(14).ToDouble();
                    //字体的样式
                    ICellStyle cellStyle3 = workBook.CreateCellStyle();
                    IFont font3 = workBook.CreateFont();
                    if (cell > 0)
                    {
                        font3.Color = HSSFColor.Blue.Index;
                    }
                    else if (cell < 0)
                    {
                        font3.Color = HSSFColor.Red.Index;
                    }
                    else
                    {
                        font3.Color = HSSFColor.Green.Index;
                    }
                    cellStyle3.SetFont(font3);
                    cellStyle3.VerticalAlignment = VerticalAlignment.Center;
                    cellStyle3.Alignment = HorizontalAlignment.Left;
                    //设置颜色
                    row.Cells[14].CellStyle = cellStyle3;
              
                }
                #endregion
            }
            if (title != "物料出成率列表")
            {
                //处理表格列头
                row = sheet.CreateRow(3);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                    row.Cells[i].CellStyle = cellStyle1;
                    row.Height = 300;
                    sheet.AutoSizeColumn(i);
                }

                //起始行号，终止行号， 起始列号，终止列号
                //处理数据内容
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = sheet.CreateRow(4 + i);
                    row.Height = 250;
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(table.Rows[i][j].ToString());
                        sheet.SetColumnWidth(j, 256 * 15);
                    }
                }
            }

            //单独设置列宽
            if (title == "库存明细统计")
            {
                sheet.SetColumnWidth(0, 8 * 265);
                sheet.SetColumnWidth(1, 20 * 265);
                sheet.SetColumnWidth(2, 50 * 265);
                sheet.SetColumnWidth(6, 20 * 265);
                sheet.SetColumnWidth(8, 20 * 265);
                sheet.SetColumnWidth(12, 20 * 265);
                sheet.SetColumnWidth(15, 20 * 265);
            }
            if (title == "出成率报表-按原物料")
            {    
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j == 0)
                    {
                        sheet.SetColumnWidth(j, 8 * 265);
                    }
                    else
                    {
                        sheet.SetColumnWidth(j, 20 * 265);
                    }
                }
            }
            if (title == "出成率实时查询")
            {
                sheet.SetColumnWidth(0, 8 * 265);
                sheet.SetColumnWidth(9, 20 * 265);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workBook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }

        }

    }
}
