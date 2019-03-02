/* ****************************************************************
 * Copyright (C) 2018
 * 文件名：
 * 文件功能描述：
 * 
 * 创建标识：时俊鹏-2019/3/2 8:35:06
 * 
 * 修改标识：
 * 修改描述：
 * ***************************************************************/
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Middleware.Log4netConfig.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Log4netConfig.Exceptions
{
    public class GlobalExceptions : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        public GlobalExceptions(IHostingEnvironment _env)
        {
            this._env = _env;
        }
        public void OnException(ExceptionContext context)
        {
            var json = new JsonErrorResponse();
            //这里面是自定义的操作记录日志
            json.Message = context.Exception.Message;
            if (_env.IsDevelopment())
            {
                json.DevelopmentMessage = context.Exception.StackTrace;//堆栈信息
            }

            if (context.Exception.GetType() == typeof(UserOperationException))
            {
                context.Result = new BadRequestObjectResult(json);//返回异常数据
            }
            else
            {
                context.Result = new InternalServerErrorObjectResult(json);
            }
            //采用log4net 进行错误日志记录
            LogHelper.ErrorLog(json.Message, context.Exception);
        }
    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
