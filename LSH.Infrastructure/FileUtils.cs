using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LSH.Infrastructure
{
   public class FileUtils
    {

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="zipPath"></param>
        /// <returns></returns>
         public static  int Zip(List<string>filePaths,string zipPath)
        {
            int succCount = filePaths.Count;
            try
            {
                using (FileStream zipFile = System.IO.File.Create(zipPath))
                {
                    using (ZipOutputStream zipStream = new ZipOutputStream(zipFile))
                    {
                        foreach (var fileToZip in filePaths)
                        {
                            //如果文件没有找到，则报错
                            if (!File.Exists(fileToZip))
                            {
                                succCount--;
                                continue;
                            }

                            using (FileStream fs = System.IO.File.OpenRead(fileToZip))
                            {
                                byte[] buffer = new byte[fs.Length];
                                fs.Read(buffer, 0, buffer.Length);
                                fs.Close();

                                string fileName = fileToZip.Substring(fileToZip.LastIndexOf("\\") + 1);
                                ZipEntry zipEntry = new ZipEntry(fileName);
                                zipEntry.IsUnicodeText = true;
                                zipStream.PutNextEntry(zipEntry);
                                zipStream.SetLevel(5);
                                zipStream.Write(buffer, 0, buffer.Length);

                            }
                        }

                        zipStream.Finish();
                        zipStream.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                succCount = 0;
            }

            return succCount;


        }

    }
}
