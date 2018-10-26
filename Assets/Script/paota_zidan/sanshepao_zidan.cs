using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sanshepao_zidan : MonoBehaviour {
    public GameObject target;
    private float speed = 20;
    public int damage;
    public float coefficient;
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
            target.GetComponent<Xiaobing_Controll>().TakeDamage(damage);
            target.GetComponent<Xiaobing_Controll>().changeSpeed(0, coefficient, 5);
            return;
        }
        transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * speed;
    }
}
