using LSH.Infrastructure.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.UnitTest
{


    [TestClass]
   public class Unit_RegexExpression
    {

        [TestMethod]
        public  void GetUnicode()
        {


            //string msg =Encoding.Unicode.get(Encoding.UTF8.GetBytes("中国"));

           // string[] msg = "中国".ToUnicode();

            bool isHas = "A中国saaaa".Exist(new string[] { "中国s","abc"});

        }


    }
}
