﻿using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace LSH.Infrastructure
{
    public class NPOIExcelProvider : IDisposable
    {
        private IWorkbook _book;

        public NPOIExcelType ExcelType { get; private set; }

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

            ExcelType = excelType;

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
            ExcelType = excelType;


        }

        #endregion

        public void CreteSheet(NPOIExcelSheet sheet)
        {

            ISheet curSheet = _book.CreateSheet(sheet.SheetName);

            //表格
            int rowIndex = 0;
            if (sheet.Rows != null && sheet.Rows.Count > 0)
            {
                foreach (var row in sheet.Rows)
                {
                    IRow curRow = curSheet.CreateRow(rowIndex);
                    int cellIndex = 0;
                    foreach (var cell in row.Cells)
                    {
                        ICell curCell = curRow.CreateCell(cellIndex, cell.Type);

                        //富文本设置
                        if (cell.RichTextSettings != null)
                        {
                            if (ExcelType == NPOIExcelType.XLSX)
                            {
                                HSSFRichTextString hSSF = new HSSFRichTextString(cell.Value);
                                cell.RichTextSettings.ForEach(setting => { hSSF.ApplyFont(setting.Start, setting.End, setting.Font); });
                                curCell.SetCellValue(hSSF);
                            }
                            else
                            {
                                XSSFRichTextString xSSF = new XSSFRichTextString(cell.Value);
                                cell.RichTextSettings.ForEach(setting => { xSSF.ApplyFont(setting.Start, setting.End, setting.Font); });
                                curCell.SetCellValue(xSSF);
                            }
                        }
                        else
                        {
                            curCell.SetCellValue(cell.Value);
                        }

                        //自动宽度
                        if (cell.Width >= 0)
                        {

                            if (cell.Width == 0)
                            {
                                AutoWidth(curSheet, cellIndex);
                            }
                            else
                            {
                                curSheet.SetColumnWidth(cellIndex, (int)cell.Width * 256);
                            }

                        }


                        if (cell.Style != null)
                        {
                            curCell.CellStyle = cell.Style;

                        }

                        cellIndex++;
                    }
                    curRow.Height = (short)Convert.ToInt32(row.Height * 20);

                    //合并单元格策略
                    if (row.Regions.Any())
                    {
                        int maxRow = 0;
                        foreach (var region in row.Regions)
                        {
                            if (maxRow < rowIndex + region.RowCount-1) {
                                maxRow = rowIndex + region.RowCount-1;
                            }
                            curSheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex+region.RowCount-1,region.StartCol, region.EndCol));
                           
                        }

                        rowIndex += maxRow + 1;


                    }
                    else
                    {

                        rowIndex++;
                    }

                    rowIndex += row.MaginButton;

                }
            }

            //图表
            if (sheet.Chart != null && sheet.Chart.Count > 0)
            {
                int startRow = rowIndex + 1;
                foreach (var curChart in sheet.Chart)
                {
                    int endRow = startRow + curChart.MarginBottom;
                    CreateChart(curChart, curSheet, startRow, endRow);
                    startRow = endRow + 1;
                }

            }

        }

        public ICellStyle SimpleCellStyle()
        {
            ICellStyle style = _book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            IFont font = _book.CreateFont();
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
                _book.Write(_fs);
            }
        }


        public DataTable SimpleReaderAt(int sheetIndex = 0)
        {
            DataTable dt = new DataTable();
            ISheet curSheet = _book.GetSheetAt(sheetIndex);
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
            int count = _book.NumberOfSheets; //获取所有SheetName
            int sheetIndex = 0;
            while (sheetIndex < count)
            {
                ds.Tables.Add(SimpleReaderAt(sheetIndex));
                sheetIndex++;
            }
            return ds;
        }

        public IFont CreateFont()
        {
            return _book.CreateFont();
        }

        public ICellStyle CreateCellStyle()
        {
            return _book.CreateCellStyle();
        }

        /// <summary>
        /// 创建一个图表实例
        /// </summary>
        /// <param name="excelChart"></param>
        /// <param name="sheet"></param>
        private void CreateChart(NPOIExcelChart excelChart, ISheet sheet, int startRow, int endRow)
        {
            if (ExcelType != NPOIExcelType.XLS) throw new NotImplementedException("只支持.xls文件作图");

            IDrawing drawing = sheet.CreateDrawingPatriarch();
            IClientAnchor anchor = drawing.CreateAnchor(0, 0, 0, 0, 0, startRow, excelChart.Axis.Count, endRow);
            XSSFChart chart = drawing.CreateChart(anchor) as XSSFChart;
            chart.SetTitle(excelChart.Title);

            IChartLegend legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.TopRight;

            //坐标轴
            var axis = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Bottom);
            axis.IsVisible = true;
            //值轴
            var data = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
            data.IsVisible = true;
            data.Crosses = (AxisCrosses.AutoZero);


            IChartDataSource<string> xs = DataSources.FromArray<string>(excelChart.Axis.ToArray());

            switch (excelChart.ExcelChartType)
            {
                case NPOIExcelChartType.Bar:
                    var chartBarData = chart.ChartDataFactory.CreateBarChartData<string, double>();
                    foreach (var item in excelChart.Data)
                    {
                        var curData = DataSources.FromArray<double>(item.Value.ToArray());
                        var curSeries = chartBarData.AddSeries(xs, curData);
                        curSeries.SetTitle(item.Key);
                    }
                    chart.Plot(chartBarData, axis, data);
                    return;
                case NPOIExcelChartType.Line:
                    var chartLineData = chart.ChartDataFactory.CreateLineChartData<string, double>();
                    foreach (var item in excelChart.Data)
                    {
                        var curData = DataSources.FromArray<double>(item.Value.ToArray());
                        var curSeries = chartLineData.AddSeries(xs, curData);
                        curSeries.SetTitle(item.Key);
                    }
                    chart.Plot(chartLineData, axis, data);
                    break;
                default:
                    break;
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

        public void Dispose()
        {

            if (_book != null)
            {
                _book.Close();
            }
        }


        public void GetColor()
        {

        }


    }
}
