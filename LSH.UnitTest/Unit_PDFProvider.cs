using LSH.Infrastructure.PDF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.UnitTest
{


    [TestClass]
  public  class Unit_PDFProvider
    {
        [TestMethod]
          public  void UnitSimpleReader()
        {


           var str= PDFSharpProvider.SimpleReader("c:\\lsh\\瑞昌二中生涯可行性分析报告(2).pdf");



        }

    }
}
