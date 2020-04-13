using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.Infrastructure.PDF
{
    public class PDFSharpProvider
    {

        public static string SimpleReader(string filePath)
        {
            PdfDocument pdf = PdfReader.Open(filePath);
            StringBuilder sb = new StringBuilder();
            foreach (var page in pdf.Pages)
            {

                PdfString s = new PdfString(page.Contents.ToString(),PdfStringEncoding.PDFDocEncoding);
                sb.Append(s.ToStringFromPdfDocEncoded());
            }

            return sb.ToString();
        }

    }
}
