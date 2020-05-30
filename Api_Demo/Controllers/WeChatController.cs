using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LSH.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;

namespace Api_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "DEFAULT_SCHEME_NAME")]
    public class WeChatController : ControllerBase
    {
        public UserService _userService { get; set; }

        [HttpGet]
        public IActionResult Get()
        {

            using (NPOIExcelProvider _proveder = new NPOIExcelProvider(NPOIExcelType.XLSX,true))
            {

                //var contentStyle = _proveder.SimpleContentStyle();
                //var titleStyle = _proveder.SimpleTitleStyle();
                //var timeStyle = _proveder.SimpleReportTimeStyle();
                //var headerStyle = _proveder.SimpleHeaderStyle();

                DataSet ds = new DataSet();
                for (int k = 0; k < 10; k++)
                {
                    DataTable dt = new DataTable("lsh" + k);

                    for (int i = 0; i < 10; i++)
                    {
                        dt.Columns.Add(new DataColumn("表头" + i, typeof(string)));
                    }
                    #region A
                    //    NPOIExcelSheet sheet = new NPOIExcelSheet() { SheetName = "lsh" + k };

                    //    #region MyRegion
                    //    NPOIExcelRow title = new NPOIExcelRow() { Height = 80 };
                    //    title.Cells.Add(new NPOIExcelCell()
                    //    {
                    //        Type = CellType.String,
                    //        Value = "个人工作汇报",
                    //        Style = titleStyle
                    //    });
                    //    title.Regions.Add(new NPOIExcelMergeRegion()
                    //    {
                    //        RowCount = 1,
                    //        StartCol = 0,
                    //        EndCol = 9
                    //    });
                    //    sheet.Rows.Add(title);


                    //    NPOIExcelRow time = new NPOIExcelRow() { Height = 20 };

                    //    time.Cells.Add(new NPOIExcelCell()
                    //    {
                    //        Type = CellType.String,
                    //        Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    //        Style = timeStyle

                    //    });
                    //    time.Regions.Add(new NPOIExcelMergeRegion()
                    //    {
                    //        RowCount = 1,
                    //        StartCol = 0,
                    //        EndCol = 9
                    //    });
                    //    sheet.Rows.Add(time);

                    //    NPOIExcelRow head = new NPOIExcelRow() { Height = 20 };
                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        head.Cells.Add(new NPOIExcelCell() {
                    //               Width=0,
                    //                Style= headerStyle,
                    //                 Type=CellType.String,
                    //                  Value="这是一个标题"+i
                    //        });

                    //    }

                    //    sheet.Rows.Add(head);
                    //    #endregion


                    //    for (int i = 0; i < 50000; i++)
                    //    {
                    //        NPOIExcelRow row = new NPOIExcelRow() { Height=50};
                    //        for (int j = 0; j < 10; j++)
                    //        {
                    //            row.Cells.Add(new NPOIExcelCell()
                    //            {
                    //                Value = Guid.NewGuid().ToString("N"),
                    //                Type = CellType.String,
                    //                Width = 80,
                    //                Style= contentStyle
                    //            });
                    //        }
                    //        sheet.Rows.Add(row);
                    //    }

                    //    _proveder.CreteSheet(sheet);
                    //} 
                    #endregion

                    for (int i = 0; i < 50000; i++)
                    {

                        var row = dt.NewRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row[dt.Columns[j].ColumnName] = Guid.NewGuid().ToString("N");
                        }

                        dt.Rows.Add(row);
                    }

                    ds.Tables.Add(dt);

                }

                _proveder.SimpleWriter(ds);
                _proveder.Save("c:\\lsh", "111");
            }

            return File(System.IO.File.ReadAllBytes("c://lsh/111.xlsx"), "application/octet-stream", "1.XLSX");

        }

    }
}