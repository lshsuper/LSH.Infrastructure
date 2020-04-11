using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace LSH.Infrastructure
{
    public class NPOIExcelProvider : IDisposable
    {

        private IWorkbook _book;
        private FileStream _fs;
        private NPOIExcelType _excelType;

        #region +初始化器

        public NPOIExcelProvider(NPOIExcelType excelType)
        {
            if (_book != null) return;


            switch (excelType)
            {
                case NPOIExcelType.XLS:
                    _book = new XSSFWorkbook();
                    break;
                case NPOIExcelType.XLSX:
                    _book = new HSSFWorkbook();
                    break;
                default:
                    throw new Exception("excel文件类型不正确");

            }

            _excelType = excelType;


        }
        public NPOIExcelProvider(Stream stream, NPOIExcelType excelType)
        {
            if (_book != null) return;


            switch (excelType)
            {
                case NPOIExcelType.XLS:
                    _book = new XSSFWorkbook(stream);
                    break;
                case NPOIExcelType.XLSX:
                    _book = new HSSFWorkbook();
                    break;
                default:
                    throw new Exception("excel文件类型不正确");

            }
            _excelType = excelType;


        } 

        #endregion

        public void CreteSheet(NPOIExcelSheet sheet)
        {

            ISheet curSheet = _book.CreateSheet(sheet.SheetName);
            int rowIndex = 0;

            foreach (var row in sheet.Rows)
            {
                IRow curRow = curSheet.CreateRow(rowIndex);
                int cellIndex = 0;
                foreach (var cell in row.Cells)
                {
                    ICell curCell = curRow.CreateCell(cellIndex,cell.Type);
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

                _book.CreateCellStyle();


            }

        }

        public ICellStyle GetSimpleCellStyle()
        {
            ICellStyle style = CreateCellStyle();

            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            IFont font = _book.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 12;
            style.SetFont(font);
            return style;
        }

        /// <summary>
        /// 创建一个单元格样式实例
        /// </summary>
        /// <returns></returns>
        public ICellStyle CreateCellStyle()
        {
            return _book.CreateCellStyle();
        }
        /// <summary>
        /// 生成文档
        /// </summary>
        public void Save(string fileName,string directory)
        {
            _fs = new FileStream($"{directory}\\{fileName}.{_excelType.ToString().ToLower()}", FileMode.Create, FileAccess.Write);
            _book.Write(_fs);
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

        public void Dispose()
        {
            if (_fs != null)
            {
                _fs.Dispose();
                _fs.Close();

            }


            if (_book != null)
            {
                _book.Close();
            }
        }

    }
}
