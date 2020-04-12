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

        public bool EnableRegion { get; set; }

    }

    public class NPOIExcelCell
    {

        public NPOIExcelCell()
        {
            IsAutoWidth = true;
            
        }
        public dynamic Value { get; set; }

        public CellType Type { get; set; }

        public bool IsAutoWidth { get; set; }

        public ICellStyle Style { get; set; }
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

        XLS=1,

        XLSX=2



    }


    public class NPOIExcelChart {

           public  List<string> Axis { get; set; }


           public  Dictionary<string,List<double>> Data { get; set; }

           public string Title { get; set; }

           public  int MarginBottom { get; set; }

        public NPOIExcelChartType ExcelChartType { get; set; }


    }

    public enum NPOIExcelChartType {

           Bar=1,
           Line=2

    }

}
