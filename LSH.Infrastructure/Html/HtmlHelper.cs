using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using WkHtmlWrapper.Pdf.Converters;

namespace LSH.Infrastructure.Html
{
    public class HtmlHelper
    {

        public static void ToPDF()
        {
            //var html = "<html><body><h2>Hello World!</h2></body></html>";
            // new HtmlToPdfConverter().ConvertAsync(html, "c:\\lsh\\test.pdf").Wait();
            HttpClient client = new HttpClient();
           var res=client.GetAsync("https://www.youku.com/").Result;
            new HtmlToPdfConverter().ConvertAsync(res.Content.ReadAsStreamAsync().Result, "c:\\lsh\\test02.pdf");
        }

    }
}
