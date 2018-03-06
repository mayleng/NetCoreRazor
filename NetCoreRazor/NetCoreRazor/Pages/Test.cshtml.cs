using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace NetCoreRazor.Pages
{
    public class TestModel : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";
        public string UserName { get; private set; } = "";
        public string Passwd { get; private set; } = "";

        //页面初始化访问此方法（get请求默认调用此方法）
        public void OnGet()
        {
            Thread.Sleep(50);
            Message += $" Server time is { DateTime.Now }";

        }
     
      //post请求会自动调用这个方法
      public void Onpost()
        {
            UserName = Request.Form["account"];
            Passwd  = Request.Form["password"];
        }

      
    }
}