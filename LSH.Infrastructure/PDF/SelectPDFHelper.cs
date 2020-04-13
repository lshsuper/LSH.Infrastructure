using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.Infrastructure.PDF
{
   public class SelectPDFHelper
    {
        /// <summary>
        /// 网页生成PDF
        /// </summary>
        /// <param name="dirPath">保存路径</param>
        public static string CreatePdfByUrl(string filePath,string url)
        {


          
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertUrl(url);
            doc.Save(filePath);
            doc.Close();
            return filePath;
        }
    }
}
