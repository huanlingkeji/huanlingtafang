using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//攻击函数
public class Tank_Attack : MonoBehaviour
{
    public GameObject zidan;
    public GameObject xuanzhuan;
    public Transform fashekou;


    private int damage=30;
    private GameObject[] diren;
    private GameObject target;
    private float ATTACKINTERVAL=0.1f;
    private float atttime=0;
    private bool existTarget;
    private static int ATTACKDISTANCE = 30;

    private bool isbaofa = false;
    private int lv = 1;
    private float lvUpTime = 0;
    private float totalUpTime = 15;
    public Slider nengliang;
    void Start()
    {
        existTarget = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        //可以攻击  判断是否有敌人  有则判断是否还在范围  不在置空
        //没有则选择  并攻击
        //
        Attack();
        //   paokou.transform.RotateAround(xuanzhuandian.position, Vector3.up, 180 * Time.deltaTime);
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
    private void Attack()
    {
        if (Time.timeScale == 0)
            return;
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
                List<GameObject> linshi_diren = new List<GameObject>();
                //遍历敌人获取在攻击范围的第一个敌人
                for (int i = 0; i < lenth; i++)
                {
                    if (Vector3.Distance(diren[i].transform.position, transform.position) <= ATTACKDISTANCE)
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
                if (Vector3.Distance(target.transform.position, transform.position) > ATTACKDISTANCE)
                {
                    existTarget = false;
                }
                //敌人没有死亡且在范围
                else
                {
                    //炮口朝向目标
                    xuanzhuan.transform.LookAt(new Vector3(target.transform.position.x, xuanzhuan.transform.position.y, target.transform.position.z));
                    GameObject obj = GameObject.Instantiate(zidan, fashekou.position, fashekou.rotation);
                    obj.GetComponent<Tank_zidan>().target = target;
                    obj.GetComponent<Tank_zidan>().damage = (int)damage;   
                    if(isbaofa)
                        obj.GetComponent<Tank_zidan>().isRealDamage = true;
                    else
                        obj.GetComponent<Tank_zidan>().isRealDamage = false;
                    //重置时间
                    atttime = 0;
                }
            }
        }
    }
    float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;

        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }
    private void baofa()
    {
        isbaofa = true;
        ATTACKINTERVAL = 0.05f;
        StartCoroutine(cancel_baofa());
    }
    IEnumerator cancel_baofa()
    {
        yield return new WaitForSeconds(10);
        isbaofa = false;
        ATTACKINTERVAL = 0.1f;
        lvUpTime = 0;
    }
}
