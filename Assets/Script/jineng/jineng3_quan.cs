using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jineng3_quan : MonoBehaviour {

    private float chufaTime=0;
    private GameObject[] diren;
    // Use this for initialization
    void Start () {
        Destroy(this.gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;
        chufaTime += Time.deltaTime;
        if (chufaTime > 0.05f)
        {
            chufaTime = 0;
            diren = GameObject.FindGameObjectsWithTag("Enemy");
            int lenth = diren.Length;
            if (lenth == 0)
                return;
            for (int i = 0; i < lenth; i++)
            {
                float dis = Vector3.Distance(diren[i].transform.position, transform.position);
                if (dis <= 15)
                {
                    Vector3 newPosition = new Vector3(transform.position.x, diren[i].transform.position.y, transform.position.z);
                    diren[i].transform.position += (newPosition - diren[i].transform.position).normalized *0.5f;
                    diren[i].GetComponent<Xiaobing_Controll>().TakeDamage((int)(10+dis/15*10));
                    if(Vector3.Distance(diren[i].transform.position, transform.position)<0.1f)
                        diren[i].transform.position = transform.position;
                }
            }
        }
	}
}
