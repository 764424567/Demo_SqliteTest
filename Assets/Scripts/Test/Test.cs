using UnityEngine;
using Mono.Data.Sqlite;

public class Test : MonoBehaviour
{
    private string appDBPath = null;
    public string dbName = "";

    void Start()
    {
        appDBPath = Application.dataPath + "/" + dbName + ".db";

        DBAccess db = new DBAccess(@"Data Source=" + appDBPath);
        //创建表格
        db.CreateTable("table_1", new string[] { "name", "qq", "email", "blog" }, new string[] { "text", "text", "text", "text" });
        //我在数据库中连续插入三条数据
        db.InsertData("table_1", new string[] { "'张三'", "'289187120'", "'zhangsan@gmail.com'", "'www.zhangsan.com'" });
        db.InsertData("table_1", new string[] { "'李四'", "'289187120'", "'000@gmail.com'", "'www.lisi.com'" });
        db.InsertData("table_1", new string[] { "'王五'", "'289187120'", "'111@gmail.com'", "'www.wangwu.com'" });
        //删掉两条数据
        //db.DeleteData("table_1", new string[] { "email", "email" }, new string[] { "'zhangsan@gmail.com'", "'000@gmail.com'" });
        //查询数据
        using (SqliteDataReader sqReader = db.SelectData("table_1", new string[] { "name", "email" }, new string[] { "qq" }, new string[] { "289187120" }))
        {
            while (sqReader.Read())
            {
                Debug.Log(sqReader.GetString(sqReader.GetOrdinal("name")));
                Debug.Log(sqReader.GetString(sqReader.GetOrdinal("email")));
            }
            sqReader.Close();
        }
        db.CloseSqlConnection();
    }

    void OnGUI()
    {
        if (appDBPath != null)
        {
            GUILayout.Label("数据库的路径" + appDBPath);
        }
    }
}