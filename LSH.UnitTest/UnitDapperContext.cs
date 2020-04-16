using LSH.Infrastructure.Dapper;
using LSH.Infrastructure.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LSH.UnitTest
{

    [TestClass]
    public class UnitDapperContext
    {


        [TestMethod]
        public void ImportArea()
        {


            //1.读取

            string path = AppContext.BaseDirectory + "\\AppData\\areas.json";
            var json = File.ReadAllText(path, Encoding.UTF8);


            var allAreas = json.ToObject<TData>();

            using (var _ctx = new DapperContext("", DatabaseType.Mysql))
            {

            }
        }

    }




    



    public class DbModel
    {


        public int RegionID { get; set; }

        public string RegionName { get; set; }

        public int ParentID { get; set; }

        public string ShortName { get; set; }

        public  int LevelType { get; set; }

        public  int CityCode { get; set; }

        public  int ZipCode { get; set; }

        public  string MergerName { get; set; }

        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }



    }

    public class TData
    {

        public int status { get; set; }
        public string message { get; set; }


        public  string data_version { get; set; }

        public List<List<TData_Result_Element>> result { get; set; }

    }

    public class TData_Result {
         
            
    
    }
    public class TData_Result_Element {
    
         public string id { get; set; }

         public string name { get; set; }

        public  string fullname { get; set; }

         public List<string> pinyin { get; set; }

     public  Dictionary<string,decimal> location { get; set; }
         
        public  List<int> cidx { get; set; }
    }
}
