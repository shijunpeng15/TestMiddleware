/* ****************************************************************
 * Copyright (C) 2018
 * 文件名：
 * 文件功能描述：
 * 
 * 创建标识：时俊鹏-2019/3/2 8:48:28
 * 
 * 修改标识：
 * 修改描述：
 * ***************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Provider
{
    public class GlobalObjectProvider
    {
        public static string path = Directory.GetCurrentDirectory();
        public static string logConfig = Path.Combine(path,"Log4netConfig", "log4net.config");


    }
}
