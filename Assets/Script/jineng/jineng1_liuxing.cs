using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jineng1_liuxing : MonoBehaviour {

    public GameObject prefab;
    public Vector3 target;
    public int damage;
    private GameObject[] diren;
    private static float MIN_Y = 1f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        transform.position += (target - transform.position).normalized * 20f * Time.deltaTime;
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
            if (Vector3.Distance(diren[i].transform.position, transform.position) <= 10f)
            {
                diren[i].GetComponent<Xiaobing_Controll>().TakeDamage(damage);
            }
        }
    }
}
