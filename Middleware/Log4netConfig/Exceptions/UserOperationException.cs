/* ****************************************************************
 * Copyright (C) 2018
 * 文件名：
 * 文件功能描述：
 * 
 * 创建标识：时俊鹏-2019/3/2 8:40:49
 * 
 * 修改标识：
 * 修改描述：
 * ***************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Log4netConfig.Exceptions
{

    public class UserOperationException : Exception
    {
        public UserOperationException() { }
        public UserOperationException(string message) : base(message) { }
        public UserOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
