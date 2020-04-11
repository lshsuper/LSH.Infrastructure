using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;

namespace LSH.Infrastructure
{
    public class NPOIExcelProvider : IDisposable
    {

        public IWorkbook Book { get; private set; }
       
        public NPOIExcelType ExcelType { get; private set; }

        #region +初始化器

        public NPOIExcelProvider(NPOIExcelType excelType)
        {
            if (Book != null) return;


            switch (excelType)
            {
                case NPOIExcelType.XLS:
                    Book = new XSSFWorkbook();
                    break;
                case NPOIExcelType.XLSX:
                    Book = new HSSFWorkbook();
                    break;
                default:
                    throw new Exception("excel文件类型不正确");

            }

            ExcelType = excelType;


        }
        public NPOIExcelProvider(Stream stream, NPOIExcelType excelType)
        {
            if (Book != null) return;


            switch (excelType)
            {
                case NPOIExcelType.XLS:
                    Book = new XSSFWorkbook(stream);
                    break;
                case NPOIExcelType.XLSX:
                    Book = new HSSFWorkbook();
                    break;
                default:
                    throw new Exception("excel文件类型不正确");

            }
            ExcelType = excelType;


        }

        #endregion

        public void CreteSheet(NPOIExcelSheet sheet)
        {

            ISheet curSheet = Book.CreateSheet(sheet.SheetName);
            int rowIndex = 0;

            foreach (var row in sheet.Rows)
            {
                IRow curRow = curSheet.CreateRow(rowIndex);
                int cellIndex = 0;
                foreach (var cell in row.Cells)
                {
                    ICell curCell = curRow.CreateCell(cellIndex, cell.Type);
                    curCell.SetCellValue(cell.Value);
                    if (cell.IsAutoWidth)
                    {
                        AutoWidth(curSheet, cellIndex);
                    }
                    if (cell.Style != null)
                    {
                        curCell.CellStyle = cell.Style;
                    }
                    cellIndex++;
                }

                //合并单元格策略
                if (row.EnableRegion)
                {
                    int maxRow = 0;
                    foreach (var region in row.Regions)
                    {
                        if (region.LastRow > maxRow)
                        {
                            maxRow = region.LastRow;
                        }
                        curSheet.AddMergedRegion(new CellRangeAddress(region.FirstRow, region.LastRow, region.FirstCol, region.LastCol));

                    }
                    rowIndex += maxRow + 1;

                }
                else
                {
                    rowIndex++;
                }

                Book.CreateCellStyle();


            }

        }

        public ICellStyle SimpleCellStyle()
        {
            ICellStyle style = Book.CreateCellStyle();

            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            IFont font = Book.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 12;
            style.SetFont(font);
            return style;
        }

        /// <summary>
        /// 生成文档
        /// </summary>
        public void Save(string fileName, string directory)
        {
            using (FileStream _fs = new FileStream($"{directory}\\{fileName}.{ExcelType.ToString().ToLower()}", FileMode.Create, FileAccess.Write))
            {
                Book.Write(_fs);
            }
        }
        /// <summary>
        /// 自适应宽度
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cellIndex"></param>
        private void AutoWidth(ISheet sheet, int cellIndex)
        {
            int width = sheet.GetColumnWidth(cellIndex);
            int length = Encoding.UTF8.GetBytes(sheet.ToString()).Length;
            if (width < length + 1)
            {
                sheet.SetColumnWidth(cellIndex, (length + 1) * 256);
            }
        }

        public DataTable SimpleReaderAt(int sheetIndex=0)
        {
            DataTable dt = new DataTable();
            ISheet curSheet = Book.GetSheetAt(sheetIndex);
            dt.TableName = curSheet.SheetName;

            if (curSheet.LastRowNum <= 1) return dt;

            IRow header = curSheet.GetRow(0);
            foreach (var cell in header.Cells)
            {
                dt.Columns.Add(cell.StringCellValue);
            }

            int rowIndex = 1;
            while (rowIndex > curSheet.LastRowNum)
            {
                var curRow = curSheet.GetRow(rowIndex);
                DataRow row = dt.NewRow();
                int cellIndex = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    row[column.ColumnName] = curRow.Cells[cellIndex].StringCellValue;
                }
                rowIndex++;
            }
            return dt;
        }

        public DataSet SimpleReader()
        {
            DataSet ds = new DataSet();
            int count = Book.NumberOfSheets; //获取所有SheetName
            int sheetIndex = 0;
            while (sheetIndex<count)
            {
                ds.Tables.Add(SimpleReaderAt(sheetIndex));
                sheetIndex++;
            }
            return ds;
        }


        public void Dispose()
        {
           
            if (Book != null)
            {
                Book.Close();
            }
        }
       
     
    }
}
