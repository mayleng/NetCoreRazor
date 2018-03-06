using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NetCoreRazor
{
    public class Program
    {
        public static void Main(string[] args)
        {

             BuildWebHost(args).Run();
           
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel()  //配置服务器
                .UseUrls("http://*:9001")  //配置URL和端口号
                .Build();




    }
}
