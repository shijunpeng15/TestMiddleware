/* ****************************************************************
 * Copyright (C) 2018
 * 文件名：
 * 文件功能描述：
 * 
 * 创建标识：时俊鹏-2019/3/16 22:38:27
 * 
 * 修改标识：
 * 修改描述：
 * ***************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Scripting.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PythonCompiler.Controllers
{
    public class PythonController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PythonCompiler(string code)
        {


            //
            var guid = Guid.NewGuid().ToString();

            var filePathDir = Path.Combine(Environment.CurrentDirectory, "PythonTemp");
            if (!Directory.Exists(filePathDir))
            {
                Directory.CreateDirectory(filePathDir);
            }
            var filePath = $"{filePathDir}{Path.DirectorySeparatorChar}{guid}.py";
            //if (!System.IO.File.Exists(filePath))
            //{
            //    System.IO.File.Create(filePath);
            //}
            var fs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(code);
            sw.Flush();
            sw.Close();
            fs.Close();

            //第一种方法
            ScriptEngine engine = Python.CreateEngine();//创建Python解释器对象
                                                        //ScriptScope scope = engine.CreateScope();//构建一个执行上下文
                                                        //ScriptSource source = engine.CreateScriptSourceFromFile(filePath); //从文件
                                                        //source.Execute(scope);
                                                        ////var com = source.Compile(); //编译
                                                        ////com.Execute(scope); //执行
                                                        //var _func = scope.GetVariable("Test1"); //调用方法名
                                                        //var CustomerData = _func(1, 2);//方法传参，若无参则不传

            //第二种方法
            //dynamic py = engine.ExecuteFile(filePath); //对象直接读取文件
            //var re = py.Test2(); //调用方法，若无参则不传

            //第三种方法
            ScriptRuntime runtime = Python.CreateRuntime();
            dynamic obj = runtime.UseFile(filePath);
            var re2 = obj.Test1(1, 2);
            //第四种方法，需要在宿主机上安装python解释器，直接调用解释器编译
            //PS:没试成功

            return Json(re2);
        }

    }
}
