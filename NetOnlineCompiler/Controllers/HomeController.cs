using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Code = CodeContent();
            return View();
        }

        public ActionResult Compiler(string code)
        {
            StringBuilder sbcode = new StringBuilder();
            sbcode.Append("using System;");
            sbcode.Append(Environment.NewLine);
            sbcode.Append("namespace DynamicCodeGenerate");
            sbcode.Append(Environment.NewLine);
            sbcode.Append("{");
            sbcode.Append(code);
            sbcode.Append("}");

            CodeDomProvider codeProvider = new CSharpCodeProvider();  //编译器
            CompilerParameters parameters = new CompilerParameters();//编译器参数
            parameters.GenerateExecutable = false;//是否生成可执行文件
            parameters.GenerateInMemory = true;//是否在内存中输出

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, sbcode.ToString());
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in results.Errors)
                {
                    sb.Append(item.ToString());
                    sb.AppendLine();
                }
                return Json(new { error = sb.ToString() });
            }
            // CS-Script 这个类库还没试过，也是很简单的



            //通过反射调用实例
            Assembly assembly = results.CompiledAssembly;

            //var obj = assembly.CreateInstance("DynamicCodeGenerate.Test");
            //MethodInfo method = obj?.GetType().GetMethod("Test2");
            //Object[] objp = new Object[] { 1, 2 };

            //var retext = method?.Invoke(obj, objp);
            //return Json(retext);

            //通过动态调用
            dynamic dynObj = assembly.CreateInstance("DynamicCodeGenerate.Test");
            var re = dynObj?.Test2(1,2);
            return Json(re);
        }

        private string CodeContent()
        {
            StringBuilder sbcode = new StringBuilder();
            sbcode.Append("     public class Test");
            sbcode.Append(Environment.NewLine);
            sbcode.Append("     {");
            sbcode.Append(Environment.NewLine);
            sbcode.Append("         public int Test2(int a,int b)");
            sbcode.Append(Environment.NewLine);
            sbcode.Append("         {");
            sbcode.Append(Environment.NewLine);
            sbcode.Append("     //请在这里输入代码");
            sbcode.Append(Environment.NewLine);
            sbcode.Append(Environment.NewLine);
            sbcode.Append(Environment.NewLine);
            sbcode.Append(Environment.NewLine);
            sbcode.Append(Environment.NewLine);
            sbcode.Append("          }");
            sbcode.Append(Environment.NewLine);

            sbcode.Append("      }");
            return sbcode.ToString();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}