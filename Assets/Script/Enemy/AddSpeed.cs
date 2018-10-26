using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//加速
public class AddSpeed : MonoBehaviour {
    private float ACTUSTINGDISTANCE = 15f;
    private float ADDINTERVAL = 0.2f;
    private float time = 0;
    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<add_circle>().max_radius = ACTUSTINGDISTANCE;
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        add_speed_for_enemy();
    }
    void add_speed_for_enemy()
    {
        time += Time.deltaTime;
        if (time > ADDINTERVAL)
        {
            time = 0;
            GameObject[] diren = GameObject.FindGameObjectsWithTag("Enemy");
            int lenth = diren.Length;
            //遍历敌人获取在攻击范围的第一个敌人
            for (int i = 0; i < lenth; i++)
            {
                if (Vector3.Distance(diren[i].transform.position, transform.position) <= ACTUSTINGDISTANCE)
                {
                    diren[i].GetComponent<Xiaobing_Controll>().changeSpeed(0, 2, 0.2f);
                }
            }
        }
    }
    void OnDestroy()
    {
        GameObject[] diren = GameObject.FindGameObjectsWithTag("Enemy");
        int lenth = diren.Length;
        //遍历敌人获取在攻击范围的第一个敌人
        for (int i = 0; i < lenth; i++)
        {
            if (Vector3.Distance(diren[i].transform.position, transform.position) <= ACTUSTINGDISTANCE)
            {
                diren[i].GetComponent<Xiaobing_Controll>().changeSpeed(0, 4, 1.5f);
            }
        }
    }
}
