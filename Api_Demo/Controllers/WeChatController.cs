using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSH.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;

namespace Api_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = "DEFAULT_SCHEME_NAME")]
    public class WeChatController : ControllerBase
    {
        public  UserService _userService { get; set; }
       
        [HttpGet]
        public  IActionResult Get()
        {

            using (NPOIExcelProvider _proveder = new NPOIExcelProvider(NPOIExcelType.XLS))
            {

                var contentStyle = _proveder.SimpleContentStyle();
                var titleStyle = _proveder.SimpleTitleStyle();
                var timeStyle = _proveder.SimpleReportTimeStyle();
                var headerStyle = _proveder.SimpleHeaderStyle();
                for (int k = 0; k < 1; k++)
                {
                    NPOIExcelSheet sheet = new NPOIExcelSheet() { SheetName = "lsh" + k };

                    #region MyRegion
                    NPOIExcelRow title = new NPOIExcelRow() { Height = 80 };
                    title.Cells.Add(new NPOIExcelCell()
                    {
                        Type = CellType.String,
                        Value = "个人工作汇报",
                        Style = titleStyle
                    });
                    title.Regions.Add(new NPOIExcelMergeRegion()
                    {
                        RowCount = 1,
                        StartCol = 0,
                        EndCol = 19
                    });
                    sheet.Rows.Add(title);


                    NPOIExcelRow time = new NPOIExcelRow() { Height = 20 };
                    
                    time.Cells.Add(new NPOIExcelCell()
                    {
                        Type = CellType.String,
                        Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                        Style = timeStyle

                    });
                    time.Regions.Add(new NPOIExcelMergeRegion()
                    {
                        RowCount = 1,
                        StartCol = 0,
                        EndCol = 19
                    });
                    sheet.Rows.Add(time);

                    NPOIExcelRow head = new NPOIExcelRow() { Height = 20 };
                    for (int i = 0; i < 20; i++)
                    {
                        head.Cells.Add(new NPOIExcelCell() {
                               Width=0,
                                Style= headerStyle,
                                 Type=CellType.String,
                                  Value="这是一个表头"+i
                        });

                    }
                   
                    sheet.Rows.Add(head);
                    #endregion


                    for (int i = 0; i < 10000; i++)
                    {
                        NPOIExcelRow row = new NPOIExcelRow() { Height=50};
                        for (int j = 0; j < 20; j++)
                        {
                            row.Cells.Add(new NPOIExcelCell()
                            {
                                Value = Guid.NewGuid().ToString("N"),
                                Type = CellType.String,
                                Width = 80,
                                Style= contentStyle
                            });
                        }
                        sheet.Rows.Add(row);
                    }

                    _proveder.CreteSheet(sheet);
                }



                _proveder.Save("111", "c:\\lsh");
            }

            return File(System.IO.File.ReadAllBytes("c://lsh/111.xls"), "application/octet-stream","1.xls");

        }

    }
}