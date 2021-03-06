﻿using LSH.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
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


                for (int k = 0; k < 1; k++)
                {
                    NPOIExcelSheet sheet = new NPOIExcelSheet() { SheetName = "lsh"+k };
                    for (int i = 0; i < 10000; i++)
                    {
                        NPOIExcelRow row = new NPOIExcelRow();
                        for (int j = 0; j < 20; j++)
                        {
                            row.Cells.Add(new NPOIExcelCell()
                            {
                                Value = Guid.NewGuid().ToString("N"),
                                Type = CellType.String,
                                Width = 0
                            });
                        }
                        sheet.Rows.Add(row);
                    }

                    _proveder.CreteSheet(sheet);
                }
            


                _proveder.Save("111", "c:\\lsh");
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
