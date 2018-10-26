using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jineng2_guangbo : MonoBehaviour {
    public Vector3 target;
    private float speed = 20;
    public int damage=100;
    private float chufaTime = 0;
    private GameObject[] diren;
    private float redius = 5;
    // Use this for initialization
    void Start () {
		
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
                if (dis <= redius)
                {
                    diren[i].GetComponent<Xiaobing_Controll>().TakeDamage(damage);
                }
            }
        }
        transform.position += (target - transform.position).normalized * Time.deltaTime * speed;
        //到达目标消灭自身
        if (Vector3.Distance(transform.position, target) < 0.4f)
        {
            this.gameObject.SetActive(false);
         //   Destroy(this.gameObject);
            return;
        }

    }
}
