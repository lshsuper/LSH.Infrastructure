using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.WebSockets;

namespace Console.XSocket
{
    class Program
    {
        static void Main(string[] args)
        {

            var a = Enum.GetValues(typeof(XYEduModuleEnum)) as int[];

            var res = GetCommbins(new int[] { 0, 1, 2, 4, 8 });
            System.Console.WriteLine(res.Count);
            foreach (var item in res)
            {
                System.Console.WriteLine(string.Join(",", item));
            }

    
            System.Console.WriteLine(res.Count);
            System.Console.ReadLine();
        }



        public static List<List<int>> GetCommbins(int[] arr)
        {
            List<List<int>> res = new List<List<int>>();

            List<int> lst;
            for (int i =1; i < Math.Pow(2, arr.Length); i++)
            {
                lst = new List<int>();
                for (int j = 0; j < arr.Length; j++)
                {
                    if ((i & (int)Math.Pow(2, j)) == Math.Pow(2, j))

                        lst.Add(arr[j]);
                }
                res.Add(lst);
            }

            return res;

        }



    }

    public enum XYEduModuleEnum : short
    {
        /// <summary>
        /// 基础模块
        /// </summary>
        [Description("基础模块")]
        Bims = 0,

        /// <summary>
        /// 数据驾驶舱
        /// </summary>
        [Description("数据驾驶舱")]
        Stat = 1,

        /// <summary>
        /// 生涯规划
        /// </summary>
        [Description("生涯规划")]
        Cap = 2,

        /// <summary>
        /// 学情分析
        /// </summary>
        [Description("学情分析")]
        Lsa = 4,

        /// <summary>
        /// 在线课程
        /// </summary>
        [Description("在线课程")]
        Oc = 8,
    }
}
