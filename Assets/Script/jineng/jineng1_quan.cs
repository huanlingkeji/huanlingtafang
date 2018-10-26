using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jineng1_quan : MonoBehaviour {

    public GameObject liuxing;
    private float create_time=0;
    private float CREATEINTERVAL = 0.2f;
    private int redius = 20;
    private int damage = 100;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 10f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;
        createLiuxing();
	}
    private void createLiuxing()
    {
        create_time += Time.deltaTime;
        if(create_time>CREATEINTERVAL)
        {
            create_time = 0;

            int x = Random.Range(0, 2*redius+1) - redius;
            int maxz = (int)Mathf.Sqrt(redius * redius - x * x);
            int z = Random.Range(0, 2*maxz + 1)-maxz;
            Vector3 hit_place = new Vector3(transform.position.x+ x, 1,transform.position.z+ z);
            GameObject obj = GameObject.Instantiate(liuxing, hit_place + new Vector3(-30, 30, 30), transform.rotation);
            obj.transform.LookAt(hit_place);
            obj.GetComponent<jineng1_liuxing>().target = hit_place;
            obj.GetComponent<jineng1_liuxing>().damage = damage;
        }
    }

    /*
     * 
     *n秒后消灭自己
     * 
     * 每过m秒创造一颗流星 流星随机砸在圈范围内  流星造成一定伤害
     * 
     * 
     */
}
