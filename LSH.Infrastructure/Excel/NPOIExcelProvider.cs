using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LSH.Infrastructure
{

    /// <summary>
    /// NPOI-Provider
    /// </summary>
    public class NPOIExcelProvider : IDisposable
    {

        #region +Private Attr
        private IWorkbook _book;

        private NPOIExcelType _excelType;

      
        #endregion

        #region +Construct

        public NPOIExcelProvider(NPOIExcelType excelType,bool isBigGrid=false)
        {
            if (_book != null) return;

            switch (excelType)
            {
                case NPOIExcelType.XLS:
                    _book = new HSSFWorkbook();
                    break;
                case NPOIExcelType.XLSX:
                    if (isBigGrid) {
                        _book = new SXSSFWorkbook();
                    }
                    else
                    {
                        _book = new XSSFWorkbook();
                    }
                   
                    break;
                default:
                    throw new Exception("excel文件类型不正确");

            }
            
            _excelType = excelType;

         
           
        }
        public NPOIExcelProvider(Stream stream, NPOIExcelType excelType,bool isBigGrid = false)
        {
            if (_book != null) return;


            switch (excelType)
            {
                case NPOIExcelType.XLS:
                    _book = new XSSFWorkbook(stream);
                    break;
                case NPOIExcelType.XLSX:
                    if (isBigGrid)
                    {
                        _book = new SXSSFWorkbook();
                    }
                    else
                    {
                        _book = new XSSFWorkbook();
                    }
                    break;
                default:
                    throw new Exception("excel文件类型不正确");

            }
            _excelType = excelType;

        }

        #endregion

        #region +Rich Utils

        /// <summary>
        /// 构建一个Sheet
        /// </summary>
        /// <param name="sheet"></param>
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
                        if (cell.RichTextSettings != null && cell.RichTextSettings.Any())
                        {
                            if (_excelType == NPOIExcelType.XLSX)
                            {
                                XSSFRichTextString xSSF = new XSSFRichTextString(cell.Value);
                                cell.RichTextSettings.ForEach(setting => { xSSF.ApplyFont(setting.Start, setting.End, setting.Font); });
                                curCell.SetCellValue(xSSF);
                            }
                            else
                            {
                                HSSFRichTextString hSSF = new HSSFRichTextString(cell.Value);
                                cell.RichTextSettings.ForEach(setting => { hSSF.ApplyFont(setting.Start, setting.End, setting.Font); });
                                curCell.SetCellValue(hSSF);

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
                            if (maxRow < rowIndex + region.RowCount - 1)
                            {
                                maxRow = rowIndex + region.RowCount - 1;
                            }
                            curSheet.AddMergedRegion(new CellRangeAddress(rowIndex, maxRow, region.StartCol, region.EndCol));

                        }

                        rowIndex += maxRow > 0 ? maxRow : 1;


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


        #endregion

        #region +Simple Instance
        public IFont CreateFont()
        {
            return _book.CreateFont();
        }

        public ICellStyle CreateCellStyle()
        {
            return _book.CreateCellStyle();
        }
        #endregion

        #region +Simple  Style




        public ICellStyle SimpleStyle(bool isBold, bool isShowBorder, int fontSize, short fontColor = -1)
        {
            ICellStyle style = _book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            if (isShowBorder)
            {
                style.BorderBottom = BorderStyle.Thin;
                style.BorderTop = BorderStyle.Thin;
                style.BorderLeft = BorderStyle.Thin;
                style.BorderRight = BorderStyle.Thin;
            }
            else
            {
                style.BorderBottom = BorderStyle.None;
                style.BorderTop = BorderStyle.None;
                style.BorderLeft = BorderStyle.None;
                style.BorderRight = BorderStyle.None;
            }
            style.WrapText = true;
            IFont font = _book.CreateFont();

            font.FontName = "微软雅黑";
            font.FontHeightInPoints = fontSize;
            font.IsBold = isBold;
            if (fontColor > 0)
            {
                font.Color = fontColor;
            }
            style.SetFont(font);
            return style;
        }



        #endregion

        #region +Simple Reader

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


        #endregion

        #region +Simple Writer

        /// <summary>
        /// 简单写Excel
        /// </summary>
        /// <param name="dt"></param>
        public void SimpleWriter(DataTable dt)
        {

            

            var sheet = _book.CreateSheet(dt.TableName);
            int rowIndex = 0;
            //填充Header
            var header = sheet.CreateRow(0);
            header.Height = 20 * 20;
            //var titleStyle = SimpleStyle(true,false,20);
            var headerStyle = SimpleStyle(true, false, 10);
            var contentStyle = SimpleStyle(false, false, 10);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
                cell.CellStyle = headerStyle;
                AutoWidth(sheet, i);
            }
            rowIndex++;

            foreach (DataRow row in dt.Rows)
            {
                var curRow = sheet.CreateRow(rowIndex);
                curRow.Height = 20* 20;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var curHeader = dt.Columns[i];
                    var cell = curRow.CreateCell(i);
                    cell.CellStyle = contentStyle;
                    var val = row[curHeader.ColumnName].ToString();
                    switch (curHeader.DataType.FullName)
                    {
                        case "System.String": //字符串类型
                            cell.SetCellValue(val);
                            break;
                        case "System.DateTime": //日期类型
                            DateTime.TryParse(val, out var dateV);
                            cell.SetCellValue(dateV);
                            break;
                        case "System.Boolean": //布尔型
                            bool.TryParse(val, out var boolV);
                            cell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":

                            int.TryParse(val, out int intV);
                            cell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //浮点型
                        case "System.Double":

                            double.TryParse(val, out double doubV);
                            cell.SetCellValue(doubV);
                            break;
                        case "System.DBNull": //空值处理
                            cell.SetCellValue("");
                            break;
                        default:
                            cell.SetCellValue("");
                            break;
                    }


                }

                rowIndex++;
            }



        }
        /// <summary>
        /// 简单写Excel
        /// </summary>
        /// <param name="ds"></param>
        public void SimpleWriter(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                SimpleWriter(dt);
            }
        }

        #endregion

        #region +Utils

        /// <summary>
        /// List To DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataTable ListToDataTable<T>(List<T> list) where T : class, new()
        {

            DataTable dt = new DataTable();

            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                if (!prop.IsDefined(typeof(NPOIExcelHeaderAttribute), true))
                    continue;

                var curAttr = prop.GetCustomAttribute<NPOIExcelHeaderAttribute>();
                dt.Columns.Add(new DataColumn(curAttr.Name, curAttr.Type));

            }

            foreach (T o in list)
            {
                var row = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var curColumn = dt.Columns[i];

                    var val = typeof(T).GetProperty(curColumn.ColumnName).GetValue(o);

                    row[curColumn.ColumnName] = val;
                }
                dt.Rows.Add(row);
            }

            return dt;


        }

        #endregion

        /// <summary>
        /// 创建一个图表实例
        /// </summary>
        /// <param name="excelChart"></param>
        /// <param name="sheet"></param>
        private void CreateChart(NPOIExcelChart excelChart, ISheet sheet, int startRow, int endRow)
        {
            if (_excelType != NPOIExcelType.XLS) throw new NotImplementedException("只支持.xls文件作图");

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
        /// <summary>
        /// 对象回收
        /// </summary>
       
        
        public void Dispose()
        {
            try
            {
                if (_book != null)
                {
                    _book.Close();
                }

               
            }
            catch (Exception)
            {

               
            }

        }

        /// <summary>
        /// 生成文档
        /// </summary>
        public string Save(string dir,string fileName)
        {


            string path = $"{dir}\\{fileName}.{_excelType.ToString().ToLower()}";

            using (var _fs= new FileStream(path,FileMode.Create, FileAccess.Write))
            {
                _book.Write(_fs);
            }


            return path;
            
        }
    }




}
