using UnityEngine;
using System.Collections.Generic;
using System.Collections;
//画圆
public class wujiaoxing : MonoBehaviour
{

    private static int pointCount = 5;
    public float radius = 0f;
    private float angle;
    private Vector3[] points = new Vector3[pointCount + 1];
    private LineRenderer renderer;
    private float showTime = 0f;
    private int[] shunxu = { 0, 3, 1, 4, 2, 0 };
    void Start()
    {
        angle = 360f / pointCount;
        renderer = GetComponent<LineRenderer>();
        if (!renderer)
        {
            Debug.LogError("LineRender is NULL!");
        }
        renderer.positionCount = pointCount + 1;  ///这里是设置圆的点数，加1是因为加了一个终点（起点）
	}

    void PaintPoints(float rad)
    {
        Vector3 v = transform.position + transform.forward * rad;
        points[0] = v;
        Quaternion r = transform.rotation;
        for (int i = 1; i < pointCount; i++)
        {
            Quaternion q = Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y - (angle * i), r.eulerAngles.z);
            v = transform.position + (q * Vector3.forward) * rad;
            points[i] = v;
        }
        for (int i = 0; i < pointCount+1; i++)
        {
            //  Debug.DrawLine(transform.position, points[i], Color.green);
            renderer.SetPosition(i, points[shunxu[i]]);  //把所有点添加到positions里
        }
    }
    // Update is called once per frame
    void Update()
    {
        showTime += Time.deltaTime;
        if (showTime > 0.04f)
        {
            PaintPoints(radius);
            showTime = 0f;
        }
        transform.Rotate(new Vector3(0, 180*Time.deltaTime, 0));
    }
    public void setRadius(float r)
    {
        radius = r;
    }
    public void setEnable(bool e)
    {
        renderer.enabled = e;
    }
}
