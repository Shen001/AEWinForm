using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 23/11/2016 08:39 AM 周三
    Author: shenxin
    Description: 管理command集合
    Modify:
    */
	
    public interface ISeanCommandManager
    {
        ISeanCommand GetCommand(Guid uid);

        ISeanCommand GetCommand(string uid);
    }
}
