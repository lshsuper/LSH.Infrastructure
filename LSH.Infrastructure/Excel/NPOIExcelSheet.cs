using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.Infrastructure
{
    public class NPOIExcelSheet
    {
        public NPOIExcelSheet()
        {
            Rows = new List<NPOIExcelRow>();
            Chart = new List<NPOIExcelChart>();

        }
        /// <summary>
        /// 表格名称
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 行数据
        /// </summary>
        public List<NPOIExcelRow> Rows { get; set; }

        //图表
        public List<NPOIExcelChart> Chart { get; set; }

    }


    public class NPOIExcelRow
    {
        public NPOIExcelRow()
        {
            Regions = new List<NPOIExcelMergeRegion>();
            Cells = new List<NPOIExcelCell>();

        }
        public List<NPOIExcelCell> Cells { get; set; }
        /// <summary>
        /// 合并单元格策略
        /// </summary>
        public List<NPOIExcelMergeRegion> Regions { get; set; }

        public int Height { get; set; }
        /// <summary>
        /// 距离下一行距离
        /// </summary>
        public  int MaginButton { get; set; }

    }

    public class NPOIExcelCell
    {
        public NPOIExcelCell()
        {
            RichTextSettings = new List<NPOIExcelCellRichTextSetting>();
            Width = 0;
        }
        public dynamic Value { get; set; }

        public CellType Type { get; set; }
        /// <summary>
        /// 0->自适应,>0->具体值,<0->不设置
        /// </summary>
        public  int Width { get; set; }

        public ICellStyle Style { get; set; }

        public List<NPOIExcelCellRichTextSetting> RichTextSettings { get; set; }

        
    }

    /// <summary>
    /// 富文本设置
    /// </summary>
    public  class NPOIExcelCellRichTextSetting
    {
        /// <summary>
        /// 左边界(闭节点)
        /// </summary>
        public  int Start { get; set; }
        /// <summary>
        /// 右边界（开节点）
        /// </summary>
        public int End { get; set; }
        /// <summary>
        /// 指定字体
        /// </summary>
        public IFont Font { get; set; }

    }


    public class NPOIExcelMergeRegion
    {
        public int FirstRow { get; set; }
        public int LastRow { get; set; }
        public int FirstCol { get; set; }

        public int LastCol { get; set; }

    }

    public class NPOIExcelCellStyle
    {
        /// <summary>
        /// 
        /// </summary>
        public int Align { get; set; }

        public int VerticalAlign { get; set; }
    }

    public enum NPOIExcelType
    {

        XLS = 1,

        XLSX = 2



    }


    public class NPOIExcelChart
    {

        public List<string> Axis { get; set; }


        public Dictionary<string, List<double>> Data { get; set; }

        public string Title { get; set; }

        public int MarginBottom { get; set; }

        public NPOIExcelChartType ExcelChartType { get; set; }


    }

    public enum NPOIExcelChartType
    {

        Bar = 1,
        Line = 2

    }

}
