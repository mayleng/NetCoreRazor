using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCoreRazor.Models;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace NetCoreRazor.Pages
{
    //postgresql 数据库测试
    public class PostgresqlModel : PageModel
    {
        public readonly Connection _postgreconn;

        public PostgresqlModel(IOptions<Connection> postgreconn)
        {
            _postgreconn = postgreconn.Value;
        }

        public string CreateMessege { get; private set; } = "CreateTable ";
        public string InsertMessege { get; private set; } = "Insert ";
        public string UpdateMessege { get; private set; } = " Update";
        public string SearchMessege { get; private set; } = "Search ";
        public string DeleteMessege { get; private set; } = "Delete ";
        public string DelTabMessege { get; private set; } = "Delete Table";

        public void OnGet()
        {
            string postgreStr = _postgreconn.Postgresqlconn;

            NpgsqlConnection conn = new NpgsqlConnection(postgreStr);
            if (conn ==null)
            {
                return;
            }

            try
            {
                conn.Open();
            }
            catch 
            {
                conn.Close();
                conn.Dispose();
                return;
            }
            //创建表classroom
            string createsql = "create table classroom (id int primary key,name int)";

            NpgsqlCommand cmd = new NpgsqlCommand(createsql, conn);
            try
            {
                int r = cmd.ExecuteNonQuery();
                if (r == -1)
                {
                    CreateMessege += "创建表成功 ";
                }

            }
            catch (Exception ex)
            {
                CreateMessege += "创建表失败 " + ex.Message;
            }

            //插入数据
            string insertsql = "insert into classroom values(20,1)";
            cmd = new NpgsqlCommand(insertsql, conn);

            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    InsertMessege += "添加成功！";
                }
               
            }
            catch (Exception ex)
            {
                InsertMessege += "添加失败！ " + ex.Message;
            }

            //查询数据
            string searcsql = "select from classroom where id=20";
            cmd = new NpgsqlCommand(searcsql, conn);
            try
            {
                NpgsqlDataReader myreader = cmd.ExecuteReader();
               
                while (myreader.Read())
                {
                    SearchMessege += "查询成功！";
                   
                }
                myreader.Close();

            }
            catch (Exception ex)
            {
                SearchMessege += "查询失败！" + ex.Message;
            }

            //修改数据
            string upsql = "update classroom set name=5 where id=20; ";
            cmd = new NpgsqlCommand(upsql, conn);

            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    UpdateMessege += "修改成功！";
                    
                }
            }
            catch (Exception ex)
            {
                UpdateMessege += "修改失败！" + ex.Message;
            }

            //删除数据
            string deldatasql = "delete from classroom where id = 20";
            cmd = new NpgsqlCommand(deldatasql, conn);

            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    DeleteMessege += "删除成功！";
                }
            }
            catch (Exception ex)
            {
                DeleteMessege += "删除失败！" + ex.Message;
            }

            //删除表
            string deltabsql = "drop table classroom";
            cmd = new NpgsqlCommand(deltabsql, conn);

            try
            {
               int n = cmd.ExecuteNonQuery();
                if (n == -1)
                {
                    DelTabMessege += "删除成功！";
                }              
            }
            catch (Exception ex)
            {
                DelTabMessege += "删除失败！" + ex.Message;
            }




        }
    }
}