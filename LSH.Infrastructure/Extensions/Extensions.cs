using Microsoft.International.Converters.PinYinConverter;
using Newtonsoft.Json;
using NPinyin;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;
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
       

        public static IList<T> ToList<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<IList<T>>(json);
            }
            catch (Exception ex)
            {
                return default(List<T>);
            }
            
        }


        public  static  T ToObject<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                return default(T); ;
            }

        }


        public static string ToPinyin(this string words)
        {
            try
            {
                if (words.Length != 0)
                {
                    StringBuilder fullSpell = new StringBuilder();
                    for (int i = 0; i < words.Length; i++)
                    {
                        var chr = words[i];
                        fullSpell.Append(GetSpell(chr));
                    }

                    return fullSpell.ToString().ToUpper();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("全拼转化出错！" + e.Message);
            }

            return string.Empty;
        }

        private static string GetSpell(char chr)
        {
            var coverchr = Pinyin.GetPinyin(chr);

            bool isChineses = ChineseChar.IsValidChar(coverchr[0]);
            if (isChineses)
            {
                ChineseChar chineseChar = new ChineseChar(coverchr[0]);
                foreach (string value in chineseChar.Pinyins)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value.Remove(value.Length - 1, 1);
                    }
                }
            }

            return coverchr;

        }


        public static string ToFirstUpper(this string pinyin)
        {
            return pinyin.Substring(0, 1).ToUpper() + pinyin.Substring(1); 
        }

       

    }
}
