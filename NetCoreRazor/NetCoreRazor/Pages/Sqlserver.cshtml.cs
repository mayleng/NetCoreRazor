using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using NetCoreRazor.Models;
using Microsoft.Extensions.Options;

namespace NetCoreRazor.Pages
{
    //创建sql server数据库的连接
    public class SqlserverModel : PageModel
    {
        public readonly Connection _sqlserverconn;

        public SqlserverModel(IOptions<Connection> sqlserverconn)
        {
            _sqlserverconn = sqlserverconn.Value;
        }

        public string CreateMessege { get; private set; } = "CreateTable ";
        public string InsertMessege { get; private set; } = "Insert ";
        public string UpdateMessege { get; private set; } = " Update";
        public string SearchMessege { get; private set; } = "Search ";
        public string DeleteMessege { get; private set; } = "Delete ";
        public string DelTabMessege { get; private set; } = "Delete Table";

        public void OnGet()
        {

        // String sqlConnStr = "Server=192.168.1.247;User ID=sa;Password=may123;database=test";
        string sqlConnStr = _sqlserverconn.Sqlserverconn;
        SqlConnection con = new SqlConnection(sqlConnStr);
            con.Open();
        //创建表girls
        string Createcom = "create table  girls" + "(id int  primary key,"
                                                                    + "name varchar(30) not null,"
                                                               + "hobby varchar(20) not null,"
                                                                  + "age int)";
            SqlCommand cmd = new SqlCommand(Createcom, con);

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == -1)
                {
                    CreateMessege += "创建表成功！";
                }
            }
            catch (Exception ex)
            {
                CreateMessege += "创建表失败：" + ex.Message;
            }

            //插入数据
            string Insertcom = "insert into girls values(20,'moon','dance',2)";
            cmd = new SqlCommand(Insertcom,con);

            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    InsertMessege += "插入数据成功";
                }
               
            }
            catch(Exception ex)
            {
                InsertMessege += "插入数据失败："+ex.Message;
            }

            //修改数据
            string updatecom = "update girls set hobby='sing' where name='moon' ";
            cmd = new SqlCommand(updatecom, con);

            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    UpdateMessege += "修改数据成功";
                }
                
            }
            catch(Exception ex)
            {
                UpdateMessege += "修改数据失败 ："+ex.Message;
            }


            //查询数据
            string searchcom = "select * from girls where name='moon'";
            cmd = new SqlCommand(searchcom, con);

            try
            {
                SqlDataReader myreader = cmd.ExecuteReader();
                if (myreader.Read())
                {
                    SearchMessege += "查询数据成功";
                }
                myreader.Close();
            }
            catch (Exception ex)
            {
                SearchMessege += "查询数据失败："+ex.Message;
            }

            //删除数据
            string delecom = "delete from girls where id =20";
            cmd = new SqlCommand(delecom, con);
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    DeleteMessege += "删除数据成功";
                }
              
            }
            catch(Exception ex)
            {
                DeleteMessege += "删除数据失败："+ex.Message;
            }



            //删除数据表
          string deletabcom = "drop table girls";
          cmd = new SqlCommand(deletabcom, con);
           try
         {
           int t= cmd.ExecuteNonQuery();
               if (t == -1)
              {
                  DelTabMessege += "删除表成功" ;
             } 
           }
         catch (Exception ex)
          {
               DelTabMessege += "删除表失败：" + ex.Message;
           }







        }
    }
}