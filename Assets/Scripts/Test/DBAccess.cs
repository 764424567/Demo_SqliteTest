using UnityEngine;
using System;
using Mono.Data.Sqlite;

public class DBAccess
{
    //数据库连接对象
    private SqliteConnection dbConnection;
    //数据库命令执行对象
    private SqliteCommand dbCommand;
    //数据读取对象
    private SqliteDataReader reader;

    //带参构造函数
    public DBAccess(string connectionString)
    {
        //打开数据库
        OpenDB(connectionString);
    }

    //无参构造函数
    public DBAccess()
    {

    }

    //打开数据库
    public void OpenDB(string connectionString)
    {
        try
        {
            dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            Debug.Log("成功连接数据库");
        }
        catch (Exception e)
        {
            string temp1 = e.ToString();
            Debug.Log("数据库打开失败： "+temp1);
        }
    }

    //关闭数据库连接
    public void CloseSqlConnection()
    {
        //销毁命令对象
        if (dbCommand != null)
        {
            dbCommand.Dispose();
        }
        dbCommand = null;

        //销毁数据读取对象
        if (reader != null)
        {
            reader.Dispose();
        }
        reader = null;

        //关闭连接
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;

        Debug.Log("断开数据库");
    }

    //命令执行函数
    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        Debug.Log("SQL命令为： " + sqlQuery);
        //初始化命令执行对象
        dbCommand = dbConnection.CreateCommand();
        //添加SQL命令
        dbCommand.CommandText = sqlQuery;
        //执行SQL命令
        return dbCommand.ExecuteReader();
    }

    /// <summary>
    /// 创建表格
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="col">字段名</param>
    /// <param name="colType">字段类型</param>
    /// <returns></returns>
    public SqliteDataReader CreateTable(string tableName, string[] col, string[] colType)
    {
        //SQL = CREATE TABLE 表名(字段名称 字段类型,字段名称 字段类型);
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }
        string query = "CREATE TABLE " + tableName + " (" + col[0] + " " + colType[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 删除表格
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public SqliteDataReader DeleteTable(string tableName)
    {
        //SQL = DELETE FROM 表名;
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 查询数据，查询整张表的数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public SqliteDataReader SelectData(string tableName)
    {
        //SQL = SELECT * FROM 表名;
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query); 
    }

    /// <summary>
    /// 查询数据，不带条件查询
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="fields">字段名</param>
    /// <returns></returns>
    public SqliteDataReader SelectData(string tableName, string[] fields)
    {
        //SQL = SELECT 字段名,字段名 FROM 表名; 
        string query = "SELECT " + fields[0];
        for (int i = 1; i < fields.Length; i++)
        {
            query += "," + fields[i];
        }
        query += " FROM" + tableName;
        return ExecuteQuery(query);  
    }

    /// <summary>
    /// 查询数据，带条件查询
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items">列名称</param>
    /// <param name="col">列名称</param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public SqliteDataReader SelectData(string tableName, string[] items, string[] col, string[] values)
    {
        //SQL = SELECT 列名称,列名称 FROM 表名 WHERE 列名称 = '值' AND 列名称 = '值'; 
        //如果字段的个数不相等，则返回错误信息
        if (col.Length != values.Length)
        {
            throw new SqliteException("查询的字段个数不相等： col.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }
        query += " FROM " + tableName + " WHERE " + col[0] + " = " + "'" + values[0] + "' ";
        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col[i] + " = " + "'" + values[0] + "' ";
        }
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public SqliteDataReader InsertData(string tableName, string[] values)
    {
        //SQL = INSERT INTO 表名 VALUES (值,值,值)
        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">列名称</param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public SqliteDataReader InsertData(string tableName, string[] cols, string[] values)
    {
        //SQL = INSERT INTO 表名 ( 列名称,列名称,列名称) VALUES (值,值,值);  
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }
        string query = "INSERT INTO " + tableName + "(" + cols[0];
        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        query += ") VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">字段名</param>
    /// <param name="colsvalues">值</param>
    /// <param name="selectkey">条件字段名</param>
    /// <param name="selectvalue">条件值</param>
    /// <returns></returns>
    public SqliteDataReader UpdateData(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        //SQL = update 表名 set 字段名=值 where 条件
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += ", " + cols[i] + " =" + colsvalues[i];
        }

        query += " WHERE " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }

    /// <summary>
    /// 删除数据，多条数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">列名称</param>
    /// <param name="colsvalues">值</param>
    /// <returns></returns>
    public SqliteDataReader DeleteData(string tableName, string[] cols, string[] colsvalues)
    {
        //SQL = DELETE FROM 表名 WHERE 列名称 = 值 or 列名称 = 值;
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        return ExecuteQuery(query);
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">列名称</param>
    /// <param name="colsValues">值</param>
    /// <returns></returns>
    public SqliteDataReader DeleteData(string tableName,string cols,string colsValues)
    {
        //SQL = DELETE FROM 表名 WHERE 列名称 = 值
        string query = "DELETE FROM " + tableName + " WHERE " + cols + colsValues;
        return ExecuteQuery(query);
    }
}