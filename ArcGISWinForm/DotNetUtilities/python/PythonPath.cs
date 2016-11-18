/*
Time: 16/11/2016 16:06 PM 周三
Author: shenxin
Description:与使用Pyhton有关的路径帮助静态类
Modify:
*/
			
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.DotNetUtilities
{
     public static class PythonPath
    {
        //Python脚本的默认文件夹路径
        public static readonly string baseScriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "script");

        public static readonly string pathname = "buffer.py";
    }
}
