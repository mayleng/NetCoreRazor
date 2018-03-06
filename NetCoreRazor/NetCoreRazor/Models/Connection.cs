using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreRazor.Models
{
    //定义一个存放sql数据库连接字符串存放的类
    public class Connection
    {
        public string Mysqlconn { get; set; }
        public string Sqlserverconn { get; set; }
        public string Postgresqlconn { get; set; }
    


    }
}


//读取appseting.json文件时：
//先在startup.cs 中注册相关的配置字段，在定义一个字段相关类，将所有项设为字段，最后在文件中引入使用。