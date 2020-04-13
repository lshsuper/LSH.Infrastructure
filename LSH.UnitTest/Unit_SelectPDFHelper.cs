using LSH.Infrastructure;
using LSH.Infrastructure.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.UnitTest
{


    [TestClass]
    public class Unit_SelectPDFHelper
    {
        [TestMethod]
        public void CrearePDF()
        {
            SelectPDFHelper.CreatePdfByUrl("c:\\lsh\\1.pdf","http://www.baidu.com");
        }
    }
}
