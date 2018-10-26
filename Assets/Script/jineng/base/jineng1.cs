using Assets.Script.Game;
using Assets.Script.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class jineng1 : base_jineng
{
    public GameObject jineng1_quan;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Create()
    {
        GameObject obj = GameObject.Instantiate(jineng1_quan, GameTools.getMouseToPlaneRayPosition() + new Vector3(0, 0.2f, 0), transform.rotation);
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
