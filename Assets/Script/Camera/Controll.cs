using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//控制相机的移动  伸缩放
public class Controll : MonoBehaviour
{
    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势  
    Vector2 oldPosition1 = new Vector2(0, 0);
    Vector2 oldPosition2 = new Vector2(0, 0);

    private static int MAX_WIDTH = 95;//20个
    private static int MAX_LENTH = 235;//48个；

    private static int MAX_Z = 235;
    private static int MIN_Z = 0;
    private static int MAX_X = 105;
    private static int MIN_X = 10;

    private static int MAX_FIELDOFVIEW = 100;
    private static int MIN_FIELDOFVIEW = 25;
    private static int PER_ADD = 5;

    private static int DX = 1;

    private int SCALE = 10;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Controll_Camera_FieldOfView();
        Controll_Camera_Move();
        touchControll();
    }
    //控制相机视野
    private void Controll_Camera_FieldOfView()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (GetComponent<Camera>().fieldOfView > MIN_FIELDOFVIEW)
                GetComponent<Camera>().fieldOfView-= PER_ADD;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (GetComponent<Camera>().fieldOfView < MAX_FIELDOFVIEW)
                GetComponent<Camera>().fieldOfView+= PER_ADD;
        }
    }
    //键盘鼠标控制相机的位置
    private void Controll_Camera_Move()
    {
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(0, 0, DX);
        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(0, 0, -DX);
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(DX, 0, 0);
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(-DX, 0, 0);

        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            float norx = Input.GetAxis("Mouse X");//获取鼠标的偏移量
            float nory = Input.GetAxis("Mouse Y");//获取鼠标的偏移量
            float value = 1.0f * GetComponent<Camera>().fieldOfView / MAX_FIELDOFVIEW;
            transform.position += new Vector3(nory * 5 * value, 0, -norx * 5 * value);
        }
        limitCameraPosition();
    }
    private void touchControll()
    {
        //判断触摸数量为单点触摸  
        if (Input.touchCount == 1)
        {
            //触摸类型为移动触摸
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //根据触摸点计算X与Y位置
                float norx = Input.GetAxis("Mouse X");//获取鼠标的偏移量
                float nory = Input.GetAxis("Mouse Y");//获取鼠标的偏移量
                float value = 1.0f * GetComponent<Camera>().fieldOfView / MAX_FIELDOFVIEW;
                transform.position += new Vector3(nory * 2 * value, 0, -norx * 2 * value);
            }
            limitCameraPosition();
        }
        //判断触摸数量为多点触摸  
        if (Input.touchCount > 1)
        {
            //前两只手指触摸类型都为移动触摸  
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {  //Unity3d教程.html" target="_blank" class="relatedlink">Unity3d教程手册：www.manew.com
               //计算出当前两点触摸点的位置  
                Vector2 tempPosition1 = Input.GetTouch(0).position;
                Vector2 tempPosition2 = Input.GetTouch(1).position;
                float value = getValue(oldPosition1, oldPosition2, tempPosition1, tempPosition2);
                GetComponent<Camera>().fieldOfView *= value;
                if (GetComponent<Camera>().fieldOfView > MIN_FIELDOFVIEW)
                    GetComponent<Camera>().fieldOfView = MIN_FIELDOFVIEW;
                if (GetComponent<Camera>().fieldOfView < MAX_FIELDOFVIEW)
                    GetComponent<Camera>().fieldOfView = MAX_FIELDOFVIEW;
                //函数返回真为放大，返回假为缩小  
                //if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                //{
                //    if (GetComponent<Camera>().fieldOfView > MIN_FIELDOFVIEW)
                //        GetComponent<Camera>().fieldOfView--;
                //}
                //else
                //{
                //    if (GetComponent<Camera>().fieldOfView < MAX_FIELDOFVIEW)
                //        GetComponent<Camera>().fieldOfView++;
                //}
                ////备份上一次触摸点的位置，用于对比          
                //oldPosition1 = tempPosition1;
                //oldPosition2 = tempPosition2;
            }
            else
            {
                oldPosition1 = Input.GetTouch(0).position;
                oldPosition2 = Input.GetTouch(1).position;
            }
        }
    }
    //函数返回真为放大，返回假为缩小  
    private bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势  
        var leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        var leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (leng1 < leng2)
        {
            //放大手势  
            return true;
        }
        else
        {
            //缩小手势
            return false;
        }
    }
    private float getValue(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        return leng2/leng1;
    }
    private void limitCameraPosition()
    {    
        if (transform.position.z > MAX_Z)
            transform.position = new Vector3(transform.position.x, transform.position.y, MAX_Z);
        if (transform.position.z<MIN_Z)
            transform.position = new Vector3(transform.position.x, transform.position.y, MIN_Z);
        if (transform.position.x > MAX_X)
            transform.position = new Vector3(MAX_X, transform.position.y, transform.position.z);
        if (transform.position.x<MIN_X)
            transform.position = new Vector3(MIN_X, transform.position.y, transform.position.z);
    }
}
