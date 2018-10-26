using Assets.Script.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class jineng4 : base_jineng {
    public GameObject jineng4_yunshi;
    public GameObject circle;
    private float max_radius = 15f;
    private TrollDrawLine circle_com;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Create()
    {
        Vector3 createPosition = GameTools.getMouseToPlaneRayPosition();
        GameObject obj = GameObject.Instantiate(jineng4_yunshi, createPosition + new Vector3(0, 30, 0), transform.rotation);
        obj.transform.LookAt(createPosition);
        circle_com = (TrollDrawLine)GameObject.Instantiate(circle, createPosition + new Vector3(0, 0.2f, 0), transform.rotation).GetComponent<TrollDrawLine>();
        circle_com.setRadius(max_radius);
        Destroy(circle_com.gameObject, 2f);
    }
    public override void Prepare()
    {

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

    }
}