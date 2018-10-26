using UnityEngine;
using System.Collections.Generic;
using System.Collections;
//画圆
public class TrollDrawLine : MonoBehaviour {

	private static int pointCount = 100;
	public float radius = 0f;
	private float angle;
    private Vector3[] points = new Vector3[pointCount+1];
	private LineRenderer renderer;
    private float t2=0f;
    public Color Color;
	void Start () {
        angle = 360f / pointCount;
		renderer = GetComponent<LineRenderer>();
		if(!renderer)
		{
			Debug.LogError("LineRender is NULL!");
		}
		renderer.positionCount=pointCount + 1;  ///这里是设置圆的点数，加1是因为加了一个终点（起点）
	}

	void PaintPoints(float rad)
	{
		Vector3 v=transform.position+transform.forward*rad;
		points[0]=v;
		Quaternion r = transform.rotation;
		for(int i=1;i<pointCount;i++)
		{
			Quaternion q = Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y - (angle * i), r.eulerAngles.z);
			v = transform.position + (q * Vector3.forward) * rad;
			points[i]=v;
		}
		for(int i=0;i< pointCount; i++)
		{
			//  Debug.DrawLine(transform.position, points[i], Color.green);
			renderer.SetPosition(i, points[i]);  //把所有点添加到positions里
		}
		if (pointCount > 0)   //这里要说明一下，因为圆是闭合的曲线，最后的终点也就是起点，
			renderer.SetPosition(pointCount, points[0]);
	}
	// Update is called once per frame
	void Update () {
        t2 += Time.deltaTime;
        if (t2 > 0.04f)
        {
            PaintPoints(radius);
            t2 = 0f;
        }
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
