using NPOI.HSSF.UserModel;
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

        public IWorkbook _book { get; private set; }

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

        /// <summary>
        /// 创建一个图表实例
        /// </summary>
        /// <param name="excelChart"></param>
        /// <param name="sheet"></param>
        private void CreateChart(NPOIExcelChart excelChart, ISheet sheet, int startRow, int endRow)
        {


            IDrawing drawing = sheet.CreateDrawingPatriarch();
            IClientAnchor anchor = drawing.CreateAnchor(0, 0, 0, 0, 0, startRow, excelChart.Axis.Count, endRow);
            XSSFChart chart = drawing.CreateChart(anchor) as XSSFChart;
            chart.SetTitle(excelChart.Title);
          
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


    }
}
