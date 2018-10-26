using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace  Assets.Script.Tools
{

    public class GameTools
    {

        private static float  getMouse1DownTime = 0f;
        //计数器
        private static int getDownCount = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public static GameObject FindChild(GameObject parent, string childName)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>();
            bool isFinded = false;
            Transform child = null;
            foreach (Transform t in children)
            {
                //   Debug.Log(t.name);
                if (t.name == childName)
                {
                    if (isFinded)
                    {
                        Debug.LogWarning("在游戏物体" + parent + "下存在不止一个子物体:" + childName);
                    }
                    isFinded = true;
                    child = t;
                }
            }
            if (isFinded)
                return child.gameObject;
            return null;
        }

        public static GameObject FindGameObjectByParent(string childName)
        {
            string[] childNames = childName.Split('/');
            //   Debug.Log(childNames);
            GameObject p = GameObject.Find(childNames[0]);

            for (int i = 1; i < childNames.Length - 1; i++)
            {
                //    Debug.Log(p.name);
                p = FindChild(p, childNames[i]);
                if (p == null)
                {
                    break;
                }
            }
            return p;
        }

        public static Vector3 getMouseToPlaneRayPosition()
        {
            float validTouchDistance = 200;
            string layerName = "Plane";
            //随鼠标点发射
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //屏幕中心点发射
            //    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, validTouchDistance, LayerMask.GetMask(layerName)))
            {
                return hitInfo.point;
            }
            return Vector3.zero;
        }
        public static bool isDoubleHitMouse1()
        {
            if (Input.GetMouseButtonUp(1))
            {
                getDownCount++;
            //    Debug.Log("鼠标点击次数"+ getDownCount);
                //当第一次点击鼠标，启动计时器
                if (getDownCount == 1)
                {
                    getMouse1DownTime = Time.time;
                }
                //当第二次点击鼠标，且时间间隔满足要求时双击鼠标
                if (2 == getDownCount && Time.time - getMouse1DownTime <= 0.5f)
                {
                 //   Debug.Log("双击键盘");
                    return true;
                }
            }
            if (Time.time - getMouse1DownTime > 0.5f)
            {
                //   Debug.Log(Time.time - getMouse1DownTime);
                // time = 0f;
                getDownCount = 0;
            }
            return false;
        }
        public static bool isPointUI()
        {
            //   Debug.Log("z:"+Input.mousePosition.z + "  x:" + Input.mousePosition.x);
            float y = Input.mousePosition.y;
            float x = Input.mousePosition.x;
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (y < 175)
                {
                 //   Debug.Log("y=" + y);
                    return true;
                }
                if (x > Screen.width - 110)
                {
                 //   Debug.Log("x=" + x);
                    return true;
                }
            }
            return false;
        }
    }
}