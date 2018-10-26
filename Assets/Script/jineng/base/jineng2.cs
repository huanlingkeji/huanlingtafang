using System.Collections;
using Assets.Script.Tools;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class jineng2 : base_jineng
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    public GameObject jineng2_guangbo;
    private GameObject circle;
    private bool isStart = false;
    private float hit_time = 0;
    private float redius = 30;
    private bool isShow = false;
    private LineRenderer renderer;
    private jinengManager jineng_manager;
    // Use this for initialization
    void Start()
    {
        circle = (GameObject)GameTools.FindGameObjectByParent("jinengManager/jineng2/circle/");
        circle.SetActive(false);
        renderer = GetComponent<LineRenderer>();
        renderer.enabled = false;
        renderer.positionCount = 2;
        jineng_manager = GameTools.FindGameObjectByParent("jinengManager/").GetComponent<jinengManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void ready()
    {
        isStart = true;
        hit_time = 0;
        startPosition = GameTools.getMouseToPlaneRayPosition() + new Vector3(0, 0.2f, 0);
        transform.position = startPosition;
        circle.SetActive(true);
        renderer.enabled = true;
        renderer.SetPosition(0, startPosition);
        jineng_manager.hid_circle();
     //   renderer.SetPosition(1, startPosition);
    }
    private void show_jineng()
    {
        endPosition = GameTools.getMouseToPlaneRayPosition() + new Vector3(0, 0.2f, 0);
        float dis = Vector3.Distance(endPosition, startPosition);
        if (dis < redius)
        {
            renderer.SetPosition(1, endPosition);
        }
        else
        {
            Vector3 newPosition = startPosition+(endPosition - startPosition) * (redius / dis);
            renderer.SetPosition(1, newPosition);
        }
        Vector3 lookPosition = endPosition + (endPosition - startPosition).normalized;
        jineng2_guangbo.transform.LookAt(lookPosition);
    }

    public override void Create()
    {
        endPosition = GameTools.getMouseToPlaneRayPosition() + new Vector3(0, 0.2f, 0);
        if (endPosition == startPosition)
            endPosition=startPosition+new Vector3(-1,0,0);
        //    Debug.Log("shifangjineng");
        Vector3 createPositon = startPosition - (endPosition - startPosition).normalized * redius;
        Vector3 toPosition = startPosition + (endPosition - startPosition).normalized * redius;
        GameObject obj = GameObject.Instantiate(jineng2_guangbo, createPositon, transform.rotation);
        obj.transform.LookAt(endPosition);
        obj.GetComponent<jineng2_guangbo>().target = toPosition;
    }
    public override void Prepare()
    {
        hit_time += Time.deltaTime;
        //按下左键  准备技能
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameTools.isPointUI())
            {
                ready();
            }
        }
        //左键按下中 如果够0.3秒则显示技能范围
        if (Input.GetMouseButton(0))
        {
            if (!isStart)
                return;
            show_jineng();
        }
    }
    public override bool IsReady()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (GameTools.isPointUI())
            {
                return false;
            }
            return true;
        }
        return false;
    }
    public override void Cancle()
    {
        renderer.enabled = false;
        isStart = false;
        circle.SetActive(false);
    }

}

/*  按下左键 
    选择起点  按下时间超过0.3秒则显示圈  如果抬起鼠标时超过0.3秒则释放技能


    */
