using Mono.Data.Sqlite;
using UnityEngine;


public enum Permissions
{
    Sightseer,//游客
    Default,//默认权限
    Administrator//管理员权限
}
//负责主要的业务逻辑处理
public class Login_Control : MonoBehaviour
{
    private string m_DbPath = null;
    private Login_Model db;

    //创建数据库
    public void Creat_DataBase()
    {
        m_DbPath = Application.dataPath + "/DataBase/MyDB.db";
        db = new Login_Model(@"Data Source=" + m_DbPath);
    }

    //创建表格
    public void Creat_Table()
    {
        //用户信息表
        db.CreateTable("User", new string[] { "UserName", "Password", "Permissions" }, new string[] { "text", "text", "text" });
        //设备信息表
        db.CreateTable("FacilityInfo", new string[] {
            "FacilityID",
            "FacilityMenu",
            "FacilitySort",
            "ProductUse",
            "FacilityName",
            "FacilityUse",
            "FacilityType",
            "FacilityManufacturer",
            "FacilityDescription",
            "FacilityDimensions",
            "FacilityInterfacePhysicalParameters",
            "FacilityInterfaceCapacityParameter",
            "FacilityTechnologicalParameter",
        }, new string[] { "text", "text", "text", "text", "text", "text", "text", "text", "text", "text", "text", "text", "text" });
    }

    //用户注册
    public void User_Register(string UserName, string Password,Permissions permissions)
    {
        if (User_Exist(UserName)==false)
        {
            db.InsertData("User", new string[] { "UserName", "Password", "Permissions" }, new string[] { UserName, Password, permissions.GetHashCode().ToString() });
        }
    }


    //判断用户名是否存在
    public bool User_Exist(string UserName)
    {
        //判断用户名是否存在
        SqliteDataReader sqReader = db.SelectData("User", UserName);
        if (sqReader.Read())
        {
            Debug.Log("读取到值");
            sqReader.Close();
            return true;
        }
        else
        {
            Debug.Log("未读取到值");
            sqReader.Close();
            return false;
        }    
    }

    //关闭数据库连接
    public void CloseSqlConnection()
    {
        db.CloseSqlConnection();
    }

    //登录
    public bool User_Login(string UserName,string UserPwd)
    {
        //判断帐号密码是否存在
        SqliteDataReader sqReader = db.SelectData("User", new string[] { "UserName", "Password" }, new string[] { "UserName", "Password" }, new string[] { UserName, UserPwd });
        if (sqReader.Read())
        {
            Debug.Log("读取到值，帐号密码正确");
            sqReader.Close();
            return true;
        }
        else
        {
            Debug.Log("未读取到值，帐号密码不正确");
            sqReader.Close();
            return false;
        }
    }
}
