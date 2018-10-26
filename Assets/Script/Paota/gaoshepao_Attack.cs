using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gaoshepao_Attack : MonoBehaviour
{
    public Transform fashekou;
    public GameObject zidan_fab;
    public GameObject xuanzhuan;

    private int damage = 100;
    private GameObject[] diren;
    private GameObject target;
    private float ATTACKINTERVAL = 1f;
    private float atttime = 0;
    private bool existTarget = false;
    private static int ATTACKDISTANCE = 50;
    private static float VELOCITY_VALUE = 50f;
    private static int MID_DISTANCE = 20;
    private static float BAOFATIME = 7;
    private bool isbaofa = false;

    private int lv = 1;
    private float lvUpTime = 0;
    private float totalUpTime = 15;
    public Slider nengliang;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        Attack();
        //if (!isbaofa)
        //{
        //    if (Input.GetKeyDown(KeyCode.I))
        //    {
        //        isbaofa = true;
        //        StartCoroutine(closeBaofa(BAOFATIME));
        //    }
        //}
        solveBaofa();
    }
    private void solveBaofa()
    {
        if (!isbaofa)
        {
            lvUpTime += Time.deltaTime;
            if (lvUpTime > totalUpTime)
            {
                baofa();
            }
        }
        nengliang.value = Mathf.Min(lvUpTime / totalUpTime, 1);
        nengliang.transform.localScale = new Vector3(1, 1, 1) * Camera.main.fieldOfView / 50;
    }
    IEnumerator closeBaofa(float keep_time)
    {
        yield return new WaitForSeconds(keep_time);
        isbaofa = false;
    }

    private void Attack()
    {
        //可以进行攻击
        atttime += Time.deltaTime;
        if (atttime > ATTACKINTERVAL)
        {
            //没有攻击目标
            if (!existTarget)
            {
                //获取敌人
                diren = GameObject.FindGameObjectsWithTag("Enemy");
                int lenth = diren.Length;
                if (lenth == 0)
                    return;
                List<GameObject> linshi_diren = new List<GameObject>();
                //遍历敌人获取在攻击范围的第一个敌人
                for (int i = 0; i < lenth; i++)
                {
                    float dis = Vector3.Distance(diren[i].transform.position, transform.position);
                    if (dis <= ATTACKDISTANCE)
                    {
                        linshi_diren.Add(diren[i]);
                    }
                }
                if (linshi_diren.Count == 0)
                    return;
                int n = Random.Range(0, linshi_diren.Count);
                target = linshi_diren[n];
                existTarget = true;
            }
            //已经有目标
            else
            {
                //敌人死亡
                if (!target)
                {
                    existTarget = false;
                    return;
                }
                //敌人超出范围
                float dis = Vector3.Distance(target.transform.position, transform.position);
                if (dis > ATTACKDISTANCE)
                {
                    existTarget = false;
                }
                //敌人没有死亡且在范围
                else
                {
                    xuanzhuan.transform.LookAt(new Vector3(target.transform.position.x, xuanzhuan.transform.position.y, target.transform.position.z));
                    if (!isbaofa)
                    {
                        GameObject obj = GameObject.Instantiate(zidan_fab, fashekou.transform.position, fashekou.transform.rotation);
                        obj.GetComponent<Rigidbody>().velocity = fashekou.transform.forward * VELOCITY_VALUE;
                        obj.GetComponent<gaoshepao_zidan>().setArr(target, (int)damage);
                     //   StartCoroutine(baofaAttack(dis));
                    }
                    else
                    {
                        StartCoroutine(baofaAttack(dis));
                    }
                    //重置时间
                    atttime = 0;
                }
            }
        }
    }
    IEnumerator baofaAttack(float dis)
    {
        for (int i = 0; i < 5; i++)
        {
            if (!target)
            {
                break;
            }
            GameObject obj = GameObject.Instantiate(zidan_fab, fashekou.transform.position, fashekou.transform.rotation);
            obj.GetComponent<Rigidbody>().velocity = fashekou.transform.forward * VELOCITY_VALUE;
            obj.GetComponent<gaoshepao_zidan>().setArr(target, (int)damage);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void jinu()
    {
        if (!isbaofa)
        {
            isbaofa = true;
 //           StartCoroutine(closeBaofa(BAOFATIME));
        }
    }
    public void cancle_jinu()
    {
        isbaofa = false;
    }
    private void baofa()
    {
        isbaofa = true;
        ATTACKINTERVAL = 3;
        StartCoroutine(cancel_baofa());
    }
    IEnumerator cancel_baofa()
    {
        yield return new WaitForSeconds(10);
        isbaofa = false;
        lvUpTime = 0;
        ATTACKINTERVAL = 1;
    }
}
