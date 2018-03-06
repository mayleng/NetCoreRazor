using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using MySql.Data.MySqlClient;
using NetCoreRazor.Models;
using Microsoft.Extensions.Options;

namespace NetCoreRazor.Pages
{
    public class MysqlModel : PageModel
    {
        //对于MySQL数据库的操作采用的是Dapper操作MySQL

        public string CreateMessege { get; private set; } = "CreateTable ";
        public string InsertMessege { get; private set; } = "Insert ";
        public string UpdateMessege { get; private set; } = " Update";
        public string SearchMessege { get; private set; } = "Search ";
        public string DeleteMessege { get; private set; } = "Delete ";
        public string DelTabMessege { get; private set; } = "Delete Table";

        
        public readonly Connection _mysqlconn;

        public MysqlModel(IOptions<Connection> mysqlconn)
        {
            _mysqlconn = mysqlconn.Value;
        }
        //页面加载调用此方法
        public void OnGet()
        {
            
            string mysqlconstr =_mysqlconn.Mysqlconn;
            //连接数据库
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // MySqlConnection con = new MySqlConnection("server=122.11.58.20;database=test;uid=apm;pwd=apm");
            MySqlConnection con = new MySqlConnection(mysqlconstr);

           //打开连接
            con.Open();

            //创建表student
            string sql = "CREATE TABLE `students` ( `sid` int(4) NOT NULL AUTO_INCREMENT PRIMARY KEY,`sname` varchar(50) NOT NULL," +
                "`sex` char(1) NOT NULL);";

          
           MySqlCommand cmd = new MySqlCommand(sql, con);
            
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    CreateMessege += "创建表成功";
                }
            }
            catch(Exception ex)
            {
                CreateMessege += "创建表失败"+ex.Message;
            }


            //插入数据

            string insertsql = "insert into students(sname,sex) values('lihua',1)";
            cmd = new MySqlCommand(insertsql, con);
            try
            {
                 if (cmd.ExecuteNonQuery()>0)
                {
                    InsertMessege += "插入数据成功";
                }
            }
            catch (Exception ex)
            {
                InsertMessege += "插入数据失败" + ex.Message;
            }


            //查询数据
            string searchsql = "select * from students where sname='lihua';";
            cmd = new MySqlCommand(searchsql, con);
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    SearchMessege += "查询成功";
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                SearchMessege += "查询失败"+ex.Message;
            }
           

            //修改数据
            string updatesql = "update students set sex=0 where sname='lihua';";
            cmd = new MySqlCommand(updatesql, con);
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    UpdateMessege += "编辑成功";
                }
            }
            catch (Exception ex)
            {
                UpdateMessege += "编辑失败"+ex.Message;
            }


            //删除数据
            string deletesql = "delete from students where sname='lihua'";
            cmd = new MySqlCommand(deletesql, con);
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    DeleteMessege += "删除数据成功";
                }
            }
            catch (Exception ex)
            {
                DeleteMessege += "删除数据失败" + ex.Message;
            }

            //删除表
            string delTabsql = "drop table students";
            cmd = new MySqlCommand(delTabsql, con);
            try
            {
                if (cmd.ExecuteNonQuery() >= 0)
                {
                    DelTabMessege += "删除表students成功";
                }
            }
            catch (Exception ex)
            {
                DelTabMessege += "删除表students失败" + ex.Message;
            }






        }
    }
}