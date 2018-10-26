using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_zidan : MonoBehaviour {

    public GameObject target;
    private float speed=30;
    public int damage ;
    public bool isRealDamage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (target == null)
        {
            Destroy(this.gameObject);
         //   target = null;
            return;
        }
        if (Vector3.Distance(transform.position, target.transform.position) < 0.4f)
        {
            Destroy(this.gameObject);
            if(!isRealDamage)
                target.GetComponent<Xiaobing_Controll>().TakeDamage(damage);
            else
            {
                target.GetComponent<Xiaobing_Controll>().TakeRealDamage(damage);
            }
            return;
        }
        transform.position+=(target.transform.position - transform.position).normalized * Time.deltaTime * speed;
    }
}
