using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sanshepao_Attack : MonoBehaviour
{
    private int damage = 50;
    private static int TARGETNUM = 3;
    private float ATTACKINTERVAL = 1f;
    private static int ATTACKDISTANCE = 35;

    public Transform[] fashekou = new Transform[TARGETNUM];
    public GameObject sanshepao_zidan;
    public GameObject[] xuanzhuan = new GameObject[TARGETNUM];


    private GameObject[] diren;
    private GameObject[] target = new GameObject[TARGETNUM];
    private float atttime = 0;
    private bool existTarget = false;

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
        solveBaofa();
    }
    private void solveBaofa()
    {
        if(!isbaofa)
        {
            lvUpTime += Time.deltaTime;
            if(lvUpTime> totalUpTime)
            {
                baofa();
            }
        }
        nengliang.value =Mathf.Min( lvUpTime / totalUpTime,1);
        nengliang.transform.localScale = new Vector3(1,1,1) * Camera.main.fieldOfView / 50;
    }

    private void Attack()
    {
        //可以进行攻击
        atttime += Time.deltaTime;
        if (atttime > ATTACKINTERVAL)
        {
            //没有满攻击目标   则添加攻击目标
            if (!isFullTarget(target))
            {
                //获取敌人
                diren = GameObject.FindGameObjectsWithTag("Enemy");
                int lenth = diren.Length;
                for (int i = 0; i < lenth; i++)
                {
                    if (Vector3.Distance(diren[i].transform.position, transform.position) <= ATTACKDISTANCE)
                    {
                        //判断敌人是否已经为攻击目标
                        if (!isAlreadyTarget(target,diren[i]))
                        {
                            //获取空目标索引填充目标
                            int index = getNullTargetIndex(target);
                            //如果满目标则退出循环
                            if (index == -1)
                                break;
                            target[index] = diren[i];
                        }
                    }
                }
            }
            //已经有目标 则攻击
            if (getNotNullTargetIndex(target) != 1)
            {
                for (int i = 0; i < TARGETNUM; i++)
                {
                    GameObject target1 = target[i];
                    //敌人死亡
                    if (!target1)
                    {
                        continue;
                    }
                    //敌人超出范围
                    if (Vector3.Distance(target1.transform.position, transform.position) > ATTACKDISTANCE)
                    {
                        target[i] = null;
                        continue;
                    }
                    //敌人没有死亡且在范围
                    else
                    {
                        xuanzhuan[i].transform.LookAt(new Vector3(target[i].transform.position.x,
                           xuanzhuan[i].transform.position.y, target[i].transform.position.z));
                        GameObject obj = GameObject.Instantiate(sanshepao_zidan, fashekou[i].position, fashekou[i].rotation);
                        obj.GetComponent<sanshepao_zidan>().target = target1;
                        obj.GetComponent<sanshepao_zidan>().damage = (int)damage;
                        if(isbaofa)
                            obj.GetComponent<sanshepao_zidan>().coefficient = 0.1f;
                        if (!isbaofa)
                            obj.GetComponent<sanshepao_zidan>().coefficient = 0.9f;
                        atttime = 0;
                    }
                }
            }
        }
    }
    //判断是否已经满目标
    public bool isFullTarget(GameObject[] target)
    {
        for (int i = 0; i < TARGETNUM; i++)
        {
            if (target[i] == null)
                return false;
        }
        return true;
    }
    //判断敌人是否已经为目标
    public bool isAlreadyTarget(GameObject[] target, GameObject obj)
    {
        for (int i = 0; i < TARGETNUM; i++)
        {
            if (target[i] == obj)
                return true;
        }
        return false;
    }
    //判断第一个空目标索引   满目标返回-1
    public int getNullTargetIndex(GameObject[] target)
    {
        for (int i = 0; i < TARGETNUM; i++)
        {
            if (target[i] == null)
                return i;
        }
        return -1;
    }
    //判断第一个非空目标  无目标返回-1
    public int getNotNullTargetIndex(GameObject[] target)
    {
        for (int i = 0; i < TARGETNUM; i++)
        {
            if (target[i] != null)
                return i;
        }
        return -1;
    }
    private void baofa()
    {
        isbaofa = true;
        StartCoroutine(cancel_baofa());
    }
    IEnumerator cancel_baofa()
    {
        yield return new WaitForSeconds(10);
        isbaofa = false;
        lvUpTime = 0;
    }
}
