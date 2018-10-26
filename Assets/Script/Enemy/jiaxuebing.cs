using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiaxuebing : Xiaobing_Controll {

    private float ACTUSTINGDISTANCE = 15f;
    private float ADDINTERVAL = 1f;
    private float time = 0;
    // Use this for initialization
    void Start()
    {
        base.Start();
        this.gameObject.GetComponent<add_circle>().max_radius = ACTUSTINGDISTANCE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        base.Update();
        add_health_for_enemy();
    }
    void add_health_for_enemy()
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
                    addHealth(diren[i], 100);
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
                addHealth(diren[i], 500);
            }
        }
    }
    private void addHealth(GameObject obj ,int ahp)
    {
        obj.GetComponent<Xiaobing_Controll>().addHp(ahp);
    }
}
