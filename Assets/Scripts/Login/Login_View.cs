using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//主要负责UI界面展示和动画表现的处理
public class Login_View : MonoBehaviour
{
    //业务处理类
    public Login_Control m_LoginControl;
    //进度条速度间隔
    private float m_RotateTime;
    //进度条
    public Slider m_Slider;
    //进度条显示文本
    public Text m_SliderNumTest;
    //Panel
    public GameObject[] m_Panel;
    //控制代码运行
    [HideInInspector]
    public bool m_isFrist = false;

    void Start()
    {
        for (int i = 0; i < m_Panel.Length; i++)
        {
            m_Panel[i].SetActive(false);
        }
        //初始化
        Control_Panel(true);
        m_isFrist = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFrist)
        {
            Control_Slider();
        }
    }

    //Slider组件控制
    public void Control_Slider()
    {
        Control_Panel(true);
        //设置进度条的值
        m_Slider.value = m_RotateTime / 2;
        m_RotateTime += Time.deltaTime;
        //设置进度条数值
        int num = (int)(m_Slider.value * 100);
        m_SliderNumTest.text = num.ToString() + "%";
        //当进度条的值不满的时候，设置为100
        if (m_Slider.value >= 0.9f)
        {
            m_LoginControl.Creat_DataBase();
            m_LoginControl.Creat_Table();
            m_SliderNumTest.text = "100%";
            Control_Panel(false);
            m_isFrist = false;
        }
    }

    //Panel控制
    public void Control_Panel(bool isFrist)
    {
        if (isFrist)
        {
            m_Panel[0].SetActive(true);
            m_Panel[1].SetActive(false);
        }
        else
        {
            m_Panel[0].SetActive(false);
            m_Panel[1].SetActive(true);
        }
    }
}
