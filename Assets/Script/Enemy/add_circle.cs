using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_circle : MonoBehaviour {

    public GameObject obj;
    public float max_radius = 10f;

    private TrollDrawLine circle_com;
    private TrollDrawLine circle_com2;
    private float keep_time = 5f;
    private float t1 = 0f;
    private float radius = 0f;
    GameObject ogj;
    // Use this for initialization
    void Start () {
        Vector3 createPosition = new Vector3(transform.position.x, 0.11f, transform.position.z);
        circle_com = (TrollDrawLine)GameObject.Instantiate(obj, createPosition, transform.rotation).GetComponent<TrollDrawLine>();
        circle_com2 = (TrollDrawLine)GameObject.Instantiate(obj, createPosition, transform.rotation).GetComponent<TrollDrawLine>();
        circle_com.transform.SetParent(transform);
        circle_com2.transform.SetParent(transform);

        ogj = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update () {
        if (Time.timeScale == 0)
            return;
        t1 += Time.deltaTime;
        if (t1 > keep_time)
            t1 = 0f;
        radius = max_radius * (t1 / keep_time);

        circle_com.setRadius(max_radius);
        circle_com2.setRadius(radius);

        //if (Input.GetKeyDown(KeyCode.J))
        //    ogj.GetComponent<sMove>().changeSpeed(0, 0.8f, 3f);
        //if (Input.GetKeyDown(KeyCode.K))
        //    ogj.GetComponent<sMove>().changeSpeed(1, -2f, 3f);
        //if (Input.GetKeyDown(KeyCode.L))
        //    ogj.GetComponent<sMove>().changeSpeed(0, 1.2f, 3f);
    }
}
