using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    public Login_Control m_LoginControl;
    private Permissions m_Permissions;
    public Text m_TextName;
    public Text m_TextPwd;
    public Dropdown m_Dropdown;
    public Text m_HintText;

    // Use this for initialization
    void Start()
    {
        //m_LoginControl.Creat_DataBase();
        //m_LoginControl.User_Register("12","12",Permissions.Default);
        //m_LoginControl.CloseSqlConnection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button_Register()
    {
        //获取到权限信息
        string m_textDrop = m_Dropdown.captionText.text;
        switch (m_textDrop)
        {
            case "游客":
                m_Permissions = Permissions.Sightseer;
                break;
            case "普通用户":
                m_Permissions = Permissions.Default;
                break;
            case "管理员":
                m_Permissions = Permissions.Administrator;
                break;
            default:
                break;
        }

        //打开数据库
        m_LoginControl.Creat_DataBase();
        //判断用户名是否存在
        if (m_LoginControl.User_Exist(m_TextName.text))
        {
            m_HintText.text = "用户名已经存在，请重新输入";
        }
        else
        {
            m_LoginControl.User_Register(m_TextName.text, m_TextPwd.text, m_Permissions);
            m_HintText.text = "注册成功，请登录";
        }
        //关闭数据库
        m_LoginControl.CloseSqlConnection();
    }

    public void Button_Login()
    {
        //打开数据库
        m_LoginControl.Creat_DataBase();
        //判断用户名和密码是否存在
        if (m_LoginControl.User_Login(m_TextName.text,m_TextPwd.text))
        {
            m_HintText.text = "登录成功";
        }
        else
        {
            m_HintText.text = "帐号或者密码错误";
        }
        //关闭数据库
        m_LoginControl.CloseSqlConnection();
    }
}
