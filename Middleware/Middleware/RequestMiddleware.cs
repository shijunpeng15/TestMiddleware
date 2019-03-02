/* ****************************************************************
 * Copyright (C) 2018
 * 文件名：
 * 文件功能描述：
 * 
 * 创建标识：时俊鹏-2019/3/2 9:06:08
 * 
 * 修改标识：
 * 修改描述：
 * ***************************************************************/
using Microsoft.AspNetCore.Http;
using Middleware.Log4netConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            LogHelper.RequestLog(context);
            await _next.Invoke(context);
        }
    }
}
