/*
Time: 16/11/2016 15:49 PM 周三
Author: shenxin
Description: 与使用python有关的帮助类的静态方法
Modify:
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
namespace SeanShen.DotNetUtilities
{
    public class PythonHelper
    {

        /// <summary>
        /// 调用python执行GP(python的执行代码可以在Esri的官方的帮助文档上找到，每一个GP都有)
        /// </summary>
        /// <param name="filePath">.py文件的全路径</param>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public static bool RunPython(string filePath, out string errorMessage, params string[] paramList)
        {
            try
            {
                string paramStr = "/c " + filePath;//cmd中执行的字符串
                if (paramList.Length > 0)
                {
                    foreach (var item in paramList)
                    {
                        paramStr += " " + item;
                    }
                }

                ProcessStartInfo pProcessInfo = new ProcessStartInfo("cmd", paramStr);
                pProcessInfo.RedirectStandardOutput = true;
                pProcessInfo.RedirectStandardError = true;
                pProcessInfo.RedirectStandardInput = true;
                pProcessInfo.UseShellExecute = false;
                pProcessInfo.CreateNoWindow = true;

                string output = string.Empty;
                string outMsg = string.Empty;

                using (Process p = new Process())
                {
                    p.StartInfo = pProcessInfo;

                    p.Start();

                    while (!p.HasExited)
                    {

                    }

                    output = p.StandardError.ReadToEnd();
                    outMsg = p.StandardOutput.ReadToEnd();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (!(output.Length == 0 && outMsg.Length == 0))
                {
                    errorMessage = string.Format("错误信息：{0}/r/n标准输出信息：{1}", output, outMsg);
                }
                else
                {
                    errorMessage = string.Empty;
                }

                return (output.Length == 0 && outMsg.Length == 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 调用GP执行具体的tool
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        private static bool RunGPTool(IGPProcess process)
        {
            try
            {
                Geoprocessor GP = new Geoprocessor();
                GP.OverwriteOutput = true;

                IGeoProcessorResult result = (IGeoProcessorResult)GP.ExecuteAsync(process);
                if (result != null && result.Status == ESRI.ArcGIS.esriSystem.esriJobStatus.esriJobSucceeded)
                    return true;
                return false;
            }
            catch
            {
                throw;
            }
        }
        //返回执行完的GP信息
        private string ReturnMessages(Geoprocessor gp)
        {
            StringBuilder sb = new StringBuilder();
            if (gp.MessageCount > 0)
            {
                for (int Count = 0; Count <= gp.MessageCount - 1; Count++)
                {
                    System.Diagnostics.Trace.WriteLine(gp.GetMessage(Count));
                    sb.AppendFormat("{0}\n", gp.GetMessage(Count));
                }
            }
            return sb.ToString();
        }


    }
}
