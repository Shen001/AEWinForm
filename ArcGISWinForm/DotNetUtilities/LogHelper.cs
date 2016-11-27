using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.DotNetUtilities
{
    public class LogHelper
    {
        static string logFileName = AppDomain.CurrentDomain.BaseDirectory + @"Log/" + DateTime.Now.ToLongDateString()+".txt";
        public static void WriteLog(string content)
        {
            try
            {
                if (!System.IO.File.Exists(logFileName))
                {
                    System.IO.File.CreateText(logFileName).Close();
                }
                System.IO.StreamWriter streamWriter = System.IO.File.AppendText(logFileName+"\r\n");
                streamWriter.Write(content);
                streamWriter.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
