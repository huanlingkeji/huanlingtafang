using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jineng4_yunshi : MonoBehaviour {

    private int damage=500;
    private GameObject[] diren;
    private static float MIN_Y = 1f;
    private static int speed = 15;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        transform.position += new Vector3(0,-1,0) * speed * Time.deltaTime;
        if (transform.position.y < MIN_Y)
        {
            HitEnemy();
            Destroy(this.gameObject);
        }
    }
    void HitEnemy()
    {
        diren = GameObject.FindGameObjectsWithTag("Enemy");
        int lenth = diren.Length;
        for (int i = 0; i < lenth; i++)
        {
            float dis = Vector3.Distance(diren[i].transform.position, transform.position);
            if (dis <= 15f)
            {
                int dam =(int)( damage * dis / 30) + damage;
                diren[i].GetComponent<Xiaobing_Controll>().TakeDamage(dam);
            }
        }
    }
}
