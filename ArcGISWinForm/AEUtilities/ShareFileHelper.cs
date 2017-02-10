using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SeanShen.AEUtilities
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2017/02/09,周四 16:00:35
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  694e73d7-97f5-472d-8fe1-610a4d026dcb
    ** 描述： 共享文件夹帮助类
    *******************************/
    public class ShareFileHelper
    {
         private string ShareServer;
        private string SharePath;
        private string ShareUser;
        private string SharePW;
        private string ShareCurPath;

        /// <summary>
        /// 当前文件的路径
        /// </summary>
        public string ShareFilePath;

        private DirectoryInfo _DirInfo;
        public ShareFileHelper(string shareServer, string sharePath, string shareUser, string sharePW)
        {
            ShareServer = shareServer;
            SharePath = sharePath;
            ShareUser = shareUser;
            SharePW = sharePW;
            ShareCurPath = "\\" + shareServer + sharePath;
        }

        public void Connect()
        {
            if (ShareCurPath.Length > 1)
                ShareCurPath = ShareCurPath.Remove(ShareCurPath.Length - 1);
            _DirInfo = new DirectoryInfo(ShareCurPath);
        }

        private bool CanConnect(string Server, string User, string PW, out string error)
        {
            bool bConnceted = false;
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = "net.exe";
            string str = "";
            if (Server.Length > 1)
                str = Server.Remove(Server.Length - 1);
            string strArgu = string.Format(" use {0} {1} /USER:{2}", str, User, PW);
            myProcess.StartInfo.Arguments = strArgu;
            myProcess.StartInfo.UseShellExecute = false;        //关闭Shell的使用
            myProcess.StartInfo.RedirectStandardInput = true;   //重定向标准输入
            myProcess.StartInfo.RedirectStandardOutput = true;  //重定向标准输出
            myProcess.StartInfo.RedirectStandardError = true;   //重定向错误输出
            myProcess.StartInfo.CreateNoWindow = true;          //设置不显示窗口
            myProcess.Start();
            error = myProcess.StandardError.ReadToEnd();
            myProcess.WaitForExit();
            if (error.Length == 0)
                bConnceted = true;
            return bConnceted;
        }

        public void ChangeDir(string strPath)
        {
            ShareCurPath = "\\" + ShareServer + "/" + SharePath + "/" + strPath;
            try
            {
                ShareCurPath = Path.GetFullPath(ShareCurPath);
                _DirInfo = new DirectoryInfo(ShareCurPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        public bool Download(string filePath, string fileName, out string errorinfo)
        {
            errorinfo = "";
            bool bOK = false;
            try
            {
                FileInfo[] str = _DirInfo.GetFiles();
                FileInfo info = null;
                foreach (FileInfo s in str)
                {
                    if (string.Compare(s.Name, fileName, true) == 0)
                    {
                        info = s;
                        break;
                    }
                }
                int a = fileName.LastIndexOf(".");
                string strLocalePath = filePath + fileName;
                if (string.Compare(fileName.Substring(a + 1, fileName.Length - a - 1), "dwg", true) == 0)//dwg特殊处理
                {
                    if (IsContainsChs(info.FullName))
                    {
                        File.Copy(info.FullName, strLocalePath, true);
                        ShareFilePath = strLocalePath;
                    }
                }
                else
                    ShareFilePath = info.FullName;
                bOK = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                errorinfo = ex.Message;
            }
            return bOK;
        }

        public Dictionary<string, bool> GetDetailList()
        {
            Dictionary<string, bool> _dic = new Dictionary<string, bool>();
            FileInfo[] str = _DirInfo.GetFiles();
            DirectoryInfo[] dir = _DirInfo.GetDirectories();
            foreach (DirectoryInfo dinfo in dir)
            {
                _dic.Add(dinfo.Name, true);
            }
            foreach (FileInfo s in str)
            {
                _dic.Add(s.Name, false);
            }
            return _dic;
        }

        ///<summary>
        ///是否包含中文路径
        ///</summary>
        ///<param name="path"></param>
        ///<returns></returns>
        public static bool IsContainsChs(string path)
        {
            string TmmP;
            for (int i = 0; i < path.Length; i++)
            {
                TmmP = path.Substring(i, 1);
                byte[] sarr = System.Text.Encoding.GetEncoding("gb2312").GetBytes(TmmP);
                if (sarr.Length == 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
