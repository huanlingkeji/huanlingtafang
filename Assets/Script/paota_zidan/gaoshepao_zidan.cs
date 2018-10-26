using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//子弹移动
public class gaoshepao_zidan : MonoBehaviour
{
    public GameObject prefab;
    public GameObject target;
    public int damage;
    private GameObject[] diren;
    private static float MIN_Y = 1f;
    private float time = 0f;    //子弹创建时间
    private Vector3 hitposition;
    private bool arrive_height=false;
    private int HEIGHT = 50;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (transform.position.y > HEIGHT && !arrive_height)
        {
            this.GetComponent<Rigidbody>().velocity = transform.forward * -30;
            arrive_height = true;
            transform.position = hitposition;
        }
        if (arrive_height)
        {
            if (target != null)
            {
                transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            }
        }
        if (transform.position.y < MIN_Y)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.position = transform.position;
            HitEnemy();
            Destroy(obj, 0.5f);
            Destroy(this.gameObject);
        }

    }
    void HitEnemy()
    {
        diren = GameObject.FindGameObjectsWithTag("Enemy");
        int lenth = diren.Length;
        for (int i = 0; i < lenth; i++)
        {
            if (Vector3.Distance(diren[i].transform.position, transform.position) <= 5f)
            {
                diren[i].GetComponent<Xiaobing_Controll>().TakeDamage(damage);
            }
        }
    }
    public void setArr(GameObject obj,int dam )
    {
        target = obj;
        hitposition = new Vector3(target.transform.position.x, HEIGHT, target.transform.position.z);
        damage = dam;
    }
}
