using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MatchmanRUN
{
    class DalUsers //该类多用于连接数据库时便于复制
    {
        private string connStr = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "playerSQL.db;";  //取得当前工作目录的绝对地址
        
        public DataTable GetAllUsers()  //连接数据库并返回一个datatable
        {
            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                 conn.Open();
                 using (SQLiteCommand cmd = conn.CreateCommand())
                 {
                     cmd.CommandText = "select * from rank order by score desc";
                     SQLiteDataReader reader = cmd.ExecuteReader();
                     dt.Load(reader);
                     reader.Close();
                     return dt;
                }
            }
         }
        public void InsertScore(String user1,int score1)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using(SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into rank(user,score) values('"+user1+"',"+score1+")";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
