using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Schedules
{
    First,
    Second,
    Third,
    Fourthly
}
public class CreateTest : MonoBehaviour
{
    public LineRenderer m_Arrow;
    public Material m_Material;
    public Camera m_Camera;
    private bool m_IsFirstOnClick = true;
    private Schedules m_Schedule = Schedules.First;

    //初始坐标
    Vector3 mouseOnClickFirstPosition;
    //当前点的位置坐标
    Vector3 mouseNowPointPosition;
    //临时点
    Vector3 mouseTempPointPosition;
    //当前点坐标和初始坐标的距离差
    float mouseDistance;

    void Update()
    {
        switch (m_Schedule)
        {
            case Schedules.First:
                Ray_Control();
                break;
            case Schedules.Second:
                Create_Wall();
                break;
            case Schedules.Third:
                break;
            case Schedules.Fourthly:
                break;
            default:
                break;
        }
    }

    public void Create_Wall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //第一次点击的坐标
            m_Arrow.SetPosition(0, mouseOnClickFirstPosition);
            mouseNowPointPosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            mouseTempPointPosition = mouseNowPointPosition;
            //记录当前点的位置坐标
            m_Arrow.SetPosition(1, mouseNowPointPosition);
            mouseDistance = Vector3.Distance(mouseOnClickFirstPosition, mouseNowPointPosition);
            //当前点-初始点坐标>3 再生成一个预制体
            //Debug.Log(mouseDistance);
            int temp = (int)mouseDistance;
            switch (temp)
            {
                case 1:
                    m_Material.mainTextureScale = new Vector2(5, 1);
                    break;
                case 2:
                    m_Material.mainTextureScale = new Vector2(10, 1);
                    break;
                case 3:
                    m_Material.mainTextureScale = new Vector2(15, 1);
                    break;
                default:
                    break;
            }
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    m_Arrow.SetPosition(2, mouseTempPointPosition);
        //    mouseNowPointPosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        //    //记录当前点的位置坐标
        //    m_Arrow.SetPosition(1, mouseNowPointPosition);
        //    mouseDistance = Vector3.Distance(mouseOnClickFirstPosition, mouseNowPointPosition);
        //    //当前点-初始点坐标>3 再生成一个预制体
        //    //Debug.Log(mouseDistance);
        //    int temp = (int)mouseDistance;
        //    switch (temp)
        //    {
        //        case 1:
        //            m_Material.mainTextureScale = new Vector2(5, 1);
        //            break;
        //        case 2:
        //            m_Material.mainTextureScale = new Vector2(10, 1);
        //            break;
        //        case 3:
        //            m_Material.mainTextureScale = new Vector2(15, 1);
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

    public void Ray_Control()
    {
        if (Input.GetMouseButton(0))
        {
            //Vector3 mouseVec2 = Input.mousePosition;
            //Vector3 mouseVec3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(mouseVec2 + "mouseVec3" + mouseVec3);
            RaycastHit hit;
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                GameObject gameobj = hit.collider.gameObject;
                //注意要将对象的tag设置成collider才能检测到
                if (gameobj.tag == "floor")
                {
                    Debug.Log("点击到地板了");
                    mouseOnClickFirstPosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
                    m_Schedule = Schedules.Fourthly;
                }
            }
        }
    }
}
