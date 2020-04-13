using LSH.Infrastructure;
using LSH.Infrastructure.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.UnitTest
{


    [TestClass]
    public class Unit_HtmlHelper
    {
        [TestMethod]
        public void CrearePDF()
        {
            HtmlHelper.ToPDF();
        }
    }
}
