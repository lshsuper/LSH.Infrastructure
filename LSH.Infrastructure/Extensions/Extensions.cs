using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace LSH.Infrastructure.Extensions
{
    public static class Extensions
    {

        /// <summary>
        /// 指定字符串转Unicode码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] ToUnicode(this string str)
        {
            string[] codeArr = new string[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                string coding = "";
                string curStr = str[i].ToString();
                for (int j = 0; j < curStr.Length; j++)
                {
                    byte[] bytes = Encoding.Unicode.GetBytes(curStr.Substring(j, 1));

                    //取出二进制编码内容  
                    string lowCode = Convert.ToString(bytes[0], 16);

                    //取出低字节编码内容（两位16进制）  
                    if (lowCode.Length == 1)
                    {
                        lowCode = "0" + lowCode;
                    }

                    string hightCode = Convert.ToString(bytes[1], 16);

                    //取出高字节编码内容（两位16进制）  
                    if (hightCode.Length == 1)
                    {
                        hightCode = "0" + hightCode;
                    }

                    coding += (hightCode + lowCode);

                }

                codeArr[i] = coding;
            }
            return codeArr;
        }

        /// <summary>
        /// 搜索当前指定文本下室友包含某些关键词
        /// </summary>
        /// <param name="input"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static bool Exist(this string input,params string[] keywords)
        {
            StringBuilder pattern = new StringBuilder($@"^.*");
            for (int i = 0; i < keywords.Length; i++)
            {
                if (i == keywords.Length - 1)
                {
                    pattern.AppendFormat("({0})", keywords[i]);
                }
                else
                {
                    pattern.AppendFormat("({0})|", keywords[i]);
                }
            }
            pattern.Append(".*$");
            return Regex.IsMatch(input, pattern.ToString());
        }
       
    }
}
