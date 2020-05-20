using LSH.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LSH.UnitTest
{


    [TestClass]
    public class Unit_NPOIExcelProvider
    {
        [TestMethod]
        public void UnitCreateSheet()
        {

            List<WorkDetail> details = new List<WorkDetail>() {

                 new WorkDetail(){ One=120,Two=200 },
                  new WorkDetail(){ One=120,Two=200 },

            };

            //using (NPOIExcelProvider _proveder = new NPOIExcelProvider(NPOIExcelType.XLSX))
            //{


            //    var sheet = new NPOIExcelSheet()
            //    {
            //        SheetName = "lsh",
            //        Rows = new List<NPOIExcelRow>() {

            //            new NPOIExcelRow(){
            //                 EnableRegion=true,
            //                 Cells=new List<NPOIExcelCell>(){ new NPOIExcelCell() { IsAutoWidth=true, Value="工作量统计",
            //                            Style=_proveder.SimpleCellStyle(),Type=CellType.String
            //                 } },
            //                  Regions=new List<NPOIExcelMergeRegion>() {
            //                  new NPOIExcelMergeRegion(){
            //                      FirstCol=0, FirstRow=0, LastCol=2, LastRow=1
            //                  },

            //         }
            //            },
            //            new NPOIExcelRow(){

            //                      Cells=new List<NPOIExcelCell>(){
            //                            new NPOIExcelCell(){
            //                                  IsAutoWidth=true,
            //                                   Value="一月",Style=_proveder.SimpleCellStyle(),Type=CellType.String
            //                            },
            //                             new NPOIExcelCell(){
            //                                  IsAutoWidth=true,
            //                                   Value="二月",Style=_proveder.SimpleCellStyle(),Type=CellType.String

            //                            }
            //                      }
            //               }
            //         }

            //    };
            //    details.ForEach(ele =>
            //    {

            //        sheet.Rows.Add(new NPOIExcelRow()
            //        {
            //            Cells = new List<NPOIExcelCell>(){
            //                    new NPOIExcelCell(){ IsAutoWidth=true, Value=ele.One, Type=CellType.Numeric,Style=_proveder.SimpleCellStyle() },
            //                    new NPOIExcelCell(){ IsAutoWidth=true, Value=ele.Two, Type=CellType.Numeric,Style=_proveder.SimpleCellStyle() }
            //               }
            //        });

            //    });
            //    _proveder.CreteSheet(sheet);
            //    _proveder.CreteSheet(new NPOIExcelSheet() { SheetName = "lsh666" });
            //    _proveder.CreteSheet(new NPOIExcelSheet() { SheetName = "lsh6666" });

            //    _proveder.Save("lsh", "c:\\lsh");
            //}
        }

        [TestMethod]
        public void CreateChart()
        {
            using (NPOIExcelProvider _proveder = new NPOIExcelProvider(NPOIExcelType.XLS))
            {

                // var style = _proveder.CreateCellStyle();
                IFont font = _proveder.CreateFont();

                font.Color = HSSFColor.Blue.Index;

                IFont font02 = _proveder.CreateFont();

                font02.Color = HSSFColor.Red.Index;



                _proveder.CreteSheet(new NPOIExcelSheet()
                {
                    SheetName = "sheet01"

                    ,
                    Rows = new List<NPOIExcelRow>() {

                           new NPOIExcelRow(){ 
                               Cells=new List<NPOIExcelCell>(){

                                new NPOIExcelCell(){

                                      RichTextSettings=new List<NPOIExcelCellRichTextSetting>(){
                                         new  NPOIExcelCellRichTextSetting(){
                                              Start=0,
                                              End=2,
                                              Font=font
                                         },
                                         new NPOIExcelCellRichTextSetting(){
                                                Start=3,
                                                End=6,
                                                Font=font02,

                                         }
                                      },
                                       Value="dhsdhsdh\nsdhs\n\ndhdhd\nsdnnnueu"
                                },
                                
                           },
                               Regions=new List<NPOIExcelMergeRegion>(){
                                            
                                    new NPOIExcelMergeRegion(){
                                         StartCol=0,
                                          EndCol=2,
                                          RowCount=5
                                    },
                                    new NPOIExcelMergeRegion(){
                                         StartCol=3,
                                         RowCount=3,
                                         EndCol=6
                                    
                                    }
                               
                               }
                           
                           }

                    }
                });
                _proveder.Save("7", "d:\\lsh");
            }

        }

        [TestMethod]
        public void Test()
        {
            var str = @"[内部自我认知|(多元升学指导|自主招生|综合评价录取|三大专项)]";

        }

    }


    public class WorkDetail
    {

        public int One { get; set; }

        public int Two { get; set; }
    }
}
