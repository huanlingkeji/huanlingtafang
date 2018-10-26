using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jineng5_quan : MonoBehaviour {

    public GameObject circle;
    public float max_radius = 10f;

    private TrollDrawLine circle_com;
    private TrollDrawLine circle_com2;
    private float keep_time = 1f;
    private float t1 = 0f;
    private float radius = 0f;

    // Use this for initialization
    void Start()
    {
        circle_com = (TrollDrawLine)GameObject.Instantiate(circle, transform.position, transform.rotation).GetComponent<TrollDrawLine>();
        circle_com2 = (TrollDrawLine)GameObject.Instantiate(circle, transform.position, transform.rotation).GetComponent<TrollDrawLine>();
        circle_com.transform.SetParent(transform);
        circle_com2.transform.SetParent(transform);
        //   this.gameObject.SetActive(false);
        StartCoroutine(destroy(keep_time));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        changeCircle();
    }

    private void changeCircle()
    {
        t1 += Time.deltaTime;
        if (t1 > keep_time)
            t1 = 0f;
        radius = max_radius * (1f - t1 / keep_time);

        circle_com.setRadius(max_radius);
        circle_com2.setRadius(radius);
    }
    IEnumerator destroy(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject[] diren = GameObject.FindGameObjectsWithTag("Enemy");
        int lenth = diren.Length;
        for (int i = 0; i < lenth; i++)
        {
            if (Vector3.Distance(diren[i].transform.position, transform.position) <= max_radius)
            {
                diren[i].GetComponent<Xiaobing_Controll>().xuanyun(5f);
            }
        }
        Destroy(this.gameObject);
    }
}
