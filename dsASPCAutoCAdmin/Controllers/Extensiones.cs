using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace dsASPCAutoCAdmin.Controllers
{
    public static class FileUtilityExtension
    {
        
        /// <summary>
        /// Write stream to a file
        /// </summary>
        /// <param name="inputStream">InputStream value</param>
        /// <param name="filePath">FilePath valye</param>
        /// <returns>true/false</returns>
        public static bool WriteStream(this Stream inputStream, string filePath)
        {
            try
            {
                using (FileStream outPutStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    byte[] buffer = new byte[1048575]; // 1MB buffer
                    while (true)
                    {
                        int read = inputStream.Read(buffer, 0, buffer.Length);
                        if (read > 0)
                            outPutStream.Write(buffer, 0, read);
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        

        /// <summary>
        /// Read stream value
        /// </summary>
        /// <param name="fileStream">FileStream to be read</param>
        /// <param name="filePath">FilePath value</param>
        /// <returns>Stream object</returns>
        public static Stream ReadStream(this FileInfo fileInfo)
        {
            int bufferSize = 1048575; // 1MB
            return new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize); ;
        }
    }
}
