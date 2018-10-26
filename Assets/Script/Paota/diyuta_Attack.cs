using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class diyuta_Attack : MonoBehaviour {
    private LineRenderer line;
    public Transform fashekou;

    private int initDamage = 10;
    private int damage = 10;
    private int MAXDAMAGE = 100;
    private float initLine_width;

    private GameObject[] diren;
    private GameObject target;
    private float ATTACKINTERVAL = 0.2f;
    private float atttime = 0;
    private bool existTarget=false;
    private static int ATTACKDISTANCE = 30;

    private bool isbaofa = false;
    private int lv = 1;
    private float lvUpTime = 0;
    private float totalUpTime = 15;
    public Slider nengliang;

    void Start()
    {
        line = this.GetComponent<LineRenderer>();
        initLine_width = line.widthMultiplier;
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
                line.SetPositions(new Vector3[] { fashekou.transform.position, target.transform.position });
                existTarget = true;
                line.enabled = true;
            }
            //已经有目标
            else
            {
                //敌人死亡
                if (!target)
                {
                    existTarget = false;
                    line.enabled = false;
                    line.widthMultiplier = 10.0f * initDamage / MAXDAMAGE * initLine_width;
                    resetDamage();
                    return;
                }
                //敌人超出范围
                if (Vector3.Distance(target.transform.position, transform.position) > ATTACKDISTANCE)
                {
                    existTarget = false;
                    line.enabled = false;
                    line.widthMultiplier = 10.0f * initDamage / MAXDAMAGE * initLine_width;
                    resetDamage();
                    return;
                }
                //敌人没有死亡且在范围
                else
                {
                    line.SetPositions(new Vector3[] { fashekou.transform.position, target.transform.position });
                    line.widthMultiplier = 10.0f* damage / MAXDAMAGE *initLine_width;
                    target.GetComponent<Xiaobing_Controll>().TakeDamage(damage);
                    if (damage < MAXDAMAGE)
                        damage+=5;
                    //重置时间
                    atttime = 0;
                }
            }
        }
    }
    private void resetDamage()
    {
        if(isbaofa)
            damage = MAXDAMAGE/2;
        else
            damage = initDamage;
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
