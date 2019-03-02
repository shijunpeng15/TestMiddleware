/* ****************************************************************
 * Copyright (C) 2018
 * 文件名：
 * 文件功能描述：
 * 
 * 创建标识：时俊鹏-2019/3/2 8:14:28
 * 
 * 修改标识：
 * 修改描述：
 * ***************************************************************/
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Http;
using Middleware.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Log4netConfig
{
    public class LogInitRepository
    {
        public static ILoggerRepository loggerRepository { get; private set; }
        public static void Init(string repository)
        {
            loggerRepository = LogManager.CreateRepository(repository);
            XmlConfigurator.Configure(loggerRepository, new FileInfo(GlobalObjectProvider.logConfig));

        }
    }
    public class LogHelper
    {
        //全局异常错误记录持久化
        private static readonly ILog logerror = LogManager.GetLogger(LogInitRepository.loggerRepository.Name, "logerror");
        //自定义操作记录
        private static readonly ILog loginfo = LogManager.GetLogger(LogInitRepository.loggerRepository.Name, "loginfo");
        //请求记录
        private static readonly ILog loghttp = LogManager.GetLogger(LogInitRepository.loggerRepository.Name, "httpRequest");

        #region 全局异常记录
        public static void ErrorLog(string throwMsg, Exception ex)
        {
            string msg = $"【抛出信息】:{throwMsg} <br> 【异常类型】:{ex.GetType().Name} <br>  【异常信息】:{ex.Message} <br>  【调用堆栈】:{ex.StackTrace}";
            msg = msg.Replace("\r\n", "<br>");
            msg = msg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            logerror.Error(msg);
        }
        #endregion

        #region 自定义操作记录
        public static void InfoLog(string throwMsg, Exception ex)
        {
            string msg = $"【抛出信息】:{throwMsg} <br> 【异常类型】:{ex.GetType().Name} <br>  【异常信息】:{ex.Message} <br>  【调用堆栈】:{ex.StackTrace}";
            msg = msg.Replace("\r\n", "<br>");
            msg = msg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            loginfo.Info(msg);
        }

        #endregion


        #region 请求记录
        /// <summary>
        /// 不允许调用,仅
        /// 供中间件调用
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        public static void RequestLog(HttpContext context)
        {
            string msg = $"【URL】:{context.Request.Scheme}:{context.Request.Host}{context.Request.Path}{context.Request.QueryString}<br>" +
                         $"【方式】:{context.Request.Method}<br>" +
                         $"【IP】:{context.Connection.RemoteIpAddress}<br>" +
                         $"【协议】:{context.Request.Protocol}";

            msg = msg.Replace("\r\n", "<br>");
            msg = msg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            loghttp.Info(msg);
        }
        #endregion
    }
}
