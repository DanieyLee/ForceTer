using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;

namespace ForceTer
{
    class Sqlite
    {
        private static SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=databases.db");
        public static Boolean SelectMenuName(string name)//查询名字是否已经存在
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "select id from menu where name = '" + name + "'";
            SQLiteDataReader reader = command.ExecuteReader();
            try
            {
                reader.Read();
                string id = reader["id"].ToString();
                return id == "" || id == null ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                reader.Close();
                m_dbConnection.Close();
            }
        }
        public static Image GetBackgroundImage()//查询背景
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "select texts from setting where name = '背景'";
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            String address = reader["texts"].ToString();
            reader.Close();
            m_dbConnection.Close();
            if (address == "\\localhost")
            {
                return Properties.Resources.background;
            }
            else
            {
                return Files.ReadImageFile("image\\" + address);
            }
        }
        public static void SetBackgroundImage(string name)//修改背景
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update setting set texts = '" + name + "' where name = '背景'";
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static int GetMenuAll()//查询所有存在的图标
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            int i = 0;
            string  sql = "select count(id) from menu";
            command.CommandText = sql;
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            i = int.Parse(reader["count(id)"].ToString());
            reader.Close();
            m_dbConnection.Close();
            return i;
        }
        public static List<Menu> GetMenu(string name)//查询所有图标
        {
            List<Menu> list = new List<Menu>();
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            string sql = "";
            switch (name)
            {
                case "热门应用":
                    sql = "select * from menu where hot > 0 order by hot desc";
                    break;
                case "娱乐游戏":
                    sql = "select * from menu where type = 1 order by hot desc";
                    break;
                case "社交聊天":
                    sql = "select * from menu where type = 2 order by hot desc";
                    break;
                case "影视音乐":
                    sql = "select * from menu where type = 3 order by hot desc";
                    break;
                case "工具软件":
                    sql = "select * from menu where type = 4 order by hot desc";
                    break;
                case "目录位置":
                    sql = "select * from menu where type = 5 order by hot desc";
                    break;
            }
            command.CommandText = sql;
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                list.Add(new Menu(reader["id"].ToString(),
                    reader["name"].ToString(),
                    reader["filetype"].ToString(),
                    reader["address"].ToString(),
                    reader["icon"].ToString(),
                    int.Parse(reader["type"].ToString()),
                    int.Parse(reader["hot"].ToString())));
            reader.Close();
            m_dbConnection.Close();
            return list;
        }
        public static List<Menu> FindByName(string name)//查询指定名称的图标
        {
            List<Menu> list = new List<Menu>();
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            string sql = "select * from menu where name like '%"+name+"%' order by hot desc";
            command.CommandText = sql;
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                list.Add(new Menu(reader["id"].ToString(),
                    reader["name"].ToString(),
                    reader["filetype"].ToString(),
                    reader["address"].ToString(),
                    reader["icon"].ToString(),
                    int.Parse(reader["type"].ToString()),
                    int.Parse(reader["hot"].ToString())));
            reader.Close();
            m_dbConnection.Close();
            return list;
        }
        public static void AddMenu(Menu menu)//添加图标
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "insert into menu values (" + menu.ToString() + ")";
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static void DelMenu(string id)//删除图标
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "delete from menu where id = '" + id.ToString() + "'";
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static void DelMenuAll()//删除所有图标
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "delete from menu";
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static void RemMenuName(string id, string name)//修改图标名字
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update menu set name = '" + name + "' where id = '" + id + "'";
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static void RemMenuHot(int hot, string id)//修改图标热度
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update menu set hot = " + hot + " where id = '" + id + "'";
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static void RemMenuType(int type, string id)//修改图标分类
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update menu set type = " + type + " where id = '" + id + "'";
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static void RemMenuHotPlus(string id)//图标热度+1
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "update menu set hot = hot + 1 where id = '" + id + "'";
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static Setting GetSetting()//查询设置项
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "select texts from setting where name = '设置' order by id";
            SQLiteDataReader reader = command.ExecuteReader();
            int i = 0;
            string[] str = new string[6];
            while (reader.Read())
                str[i++] = reader["texts"].ToString();
            reader.Close();
            m_dbConnection.Close();
            return new Setting(int.Parse(str[0]) == 0 ? false:true,
                int.Parse(str[1]) == 0 ? false : true,
                int.Parse(str[2]) == 0 ? false : true,
                int.Parse(str[3]) == 0 ? false : true,
                int.Parse(str[4]) == 0 ? false : true,
                str[5],int.Parse(str[5].Substring(0,str[5].LastIndexOf("×"))),
                int.Parse(str[5].Substring(str[5].LastIndexOf("×") + 1,
                (str[5].Length - str[5].LastIndexOf("×"))>5?3:
                (str[5].Length - str[5].LastIndexOf("×"))-1
                )));
        }
        public static void SetSetting(Setting setting)//修改设置项目
        {
            m_dbConnection.Open();
            SQLiteCommand command = m_dbConnection.CreateCommand();
            command.CommandText = "replace into setting (rowid, id, name, texts) values " + setting.ToString();
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
    }
}