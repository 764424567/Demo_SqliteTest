using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    public Transform m_PrefabParent;
    public GameObject m_WallPrefab;
    public Camera m_Camera;
    private bool m_IsFirstOnClick = true;

    //初始坐标
    Vector3 mouseOnClickFirstPosition;
    //当前点的位置坐标
    Vector3 mouseNowPointPosition;
    //当前点坐标和初始坐标的距离差
    float mouseDistance;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray_Control();
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
                    if (m_IsFirstOnClick)
                    {
                        //第一次点击的坐标
                        mouseOnClickFirstPosition = hit.point;
                        //生成预制体
                        Instantiate(m_WallPrefab, mouseOnClickFirstPosition, Quaternion.identity, m_PrefabParent);
                        m_IsFirstOnClick = false;
                    }
                    else
                    {
                        //记录当前点的位置坐标
                        mouseNowPointPosition = hit.point;
                        mouseDistance = Vector3.Distance(mouseOnClickFirstPosition, mouseNowPointPosition);
                        //当前点-初始点坐标>3 再生成一个预制体
                        //Debug.Log(mouseDistance);
                        int temp = (int)mouseDistance;
                        if (temp % 3 == 0)
                        {
                            //生成预制体
                            GameObject go = Instantiate(m_WallPrefab, mouseNowPointPosition, Quaternion.identity, m_PrefabParent);
                            go.transform.LookAt(mouseNowPointPosition);
                        }
                    }
                }
            }
        }
    }
}
