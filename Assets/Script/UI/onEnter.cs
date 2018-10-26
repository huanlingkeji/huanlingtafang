using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerEnter call by " + name);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerExit call by" + name);
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("鼠标进入");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("鼠标离开");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("鼠标点下");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        print("鼠标抬起");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("鼠标点击");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("开始拖拽");


    }
    void OnMouseEnter()
    {
        print("鼠标进入");
    }
}
