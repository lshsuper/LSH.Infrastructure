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


            List<DbModel> models = new List<DbModel>() {
                           new DbModel(){
                              LevelType=0,
                               MergerName="中国",
                                ParentID=0,
                                 RegionID=100000,
                                  RegionName="中国",
                                   ShortName="中国",
                                    Pinyin="ZhongGuo",
                                   
                                    
                           }
            };

            //第一层（省）
            foreach (var pro in allAreas.result[0])
            {

                string proFullName = pro.fullname;
                models.Add(new DbModel() {
                   Latitude=pro.location["lat"],
                    LevelType=1,
                     Longitude=pro.location["lng"],
                      MergerName="中国,"+proFullName,
                       ParentID= 100000,
                        Pinyin=string.Join("",pro.pinyin),
                         ShortName=pro.name,
                          RegionName=pro.fullname,
                           RegionID=int.Parse(pro.id),
                          
                       
                });

               
                //第二层市
                if (pro.cidx == null || !pro.cidx.Any()) continue;

                foreach (var city in allAreas.result[1].GetRange(pro.cidx[0],pro.cidx[1]-pro.cidx[0]+1))
                {

                    string cityFullName = city.fullname;

                    models.Add(new DbModel() {
                        Latitude = city.location["lat"],
                        LevelType = 2,
                        Longitude = city.location["lng"],
                        MergerName = "中国,"+proFullName+","+cityFullName,
                        ParentID = 100000,
                        Pinyin = string.Join("", city.pinyin),
                        ShortName = city.name,
                        RegionName = city.fullname,
                        RegionID = int.Parse(city.id),
                    });

                    //第三层县
                    
                    if (city.cidx == null || !city.cidx.Any()) continue;

                    foreach (var area in allAreas.result[2].GetRange(city.cidx[0], city.cidx[1] - city.cidx[0]+1))
                    {

                        string areaFullName = area.fullname;
                        models.Add(new DbModel()
                        {
                            Latitude = area.location["lat"],
                            LevelType = 3,
                            Longitude = area.location["lng"],
                            MergerName = "中国," + proFullName + "," + cityFullName+","+areaFullName,
                            ParentID = 100000,
                            Pinyin = area.pinyin==null?string.Empty:string.Join("", area.pinyin),
                            ShortName = area.name==null?area.fullname:area.name,
                            RegionName = area.fullname,
                            RegionID = int.Parse(area.id),
                        });

                    }

                }



            }


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

        public  string Pinyin { get; set; }

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
