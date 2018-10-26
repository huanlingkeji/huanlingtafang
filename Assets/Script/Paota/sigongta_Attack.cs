using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class sigongta_Attack : MonoBehaviour
{
    public GameObject tank_zidan;
    public GameObject jiguangfab;
    public Transform[] fashekou=new Transform[4];
    public GameObject xuanzhuan;

    private int damage = 150;
    private GameObject[] diren;
    private GameObject target;
    private float ATTACKINTERVAL = 1f;
    private float BAOFAATTACKINTERVAL = 0.8f;
    private float atttime=0;
    private static float LIGHTKEEPTIME = 0.2f;
    private bool existTarget=false;
    private static int ATTACKDISTANCE = 35;
    private static float VELOCITY_VALUE = 0.7f;
    private static float BAOFATIME = 5;
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

        //if(isbaofa)
        //    xuanzhuan.transform.Rotate(0, 480 * Time.deltaTime, 0);
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.I))
        //    {
        //        isbaofa = true;
        //        ATTACKINTERVAL = BAOFAATTACKINTERVAL;
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
        ATTACKINTERVAL = 1;
    }

    private void Attack()
    {
        //可以进行攻击
        atttime += Time.deltaTime;
        if (atttime > ATTACKINTERVAL)
        {
            //爆发状态
            if (isbaofa)
            {
                diren = GameObject.FindGameObjectsWithTag("Enemy");
                int lenth = diren.Length;
                //没有敌人
                if (lenth == 0)
                    return;
                //遍历敌人获取在攻击范围的第一个敌人
                List<GameObject> linshi_diren = new List<GameObject>();
                for (int i = 0; i < lenth; i++)
                {
                    if (Vector3.Distance(diren[i].transform.position, transform.position) <= ATTACKDISTANCE)
                    {
                        linshi_diren.Add(diren[i]);
                    }
                }
                //没有敌人进入攻击范围
                if (linshi_diren.Count == 0)
                    return;
                for (int i = 0; i < 4; i++)
                {
                    GameObject obj = GameObject.Instantiate(tank_zidan, fashekou[i].position+fashekou[i].forward*2, fashekou[i].rotation);

                    int n = Random.Range(0, linshi_diren.Count);
                    if(n==linshi_diren.Count)
                        Debug.Log(linshi_diren.Count + "   " + n);
                    obj.GetComponent<Tank_zidan>().target = linshi_diren[n];
                    obj.GetComponent<Tank_zidan>().damage = (int)damage;

                    //重置时间
                    atttime = 0;
                }
                return;
            }
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

                    GameObject jiguang = (GameObject)GameObject.Instantiate(jiguangfab, fashekou[0].transform.position, fashekou[0].transform.rotation);
                    LineRenderer line = jiguang.GetComponent<LineRenderer>();
                    line.SetPositions(new Vector3[] { fashekou[0].transform.position, target.transform.position });
                    if (Random.Range(0, 100) < 23)
                    {
                        target.GetComponent<Xiaobing_Controll>().xuanyun(0.5f);
                    }
                    target.GetComponent<Xiaobing_Controll>().TakeDamage(damage);
                    Destroy(jiguang, LIGHTKEEPTIME);
           
                    //重置时间
                    atttime = 0;
                }
            }
        }
    }

    public void jinu()
    {
        if (!isbaofa)
        {
            isbaofa = true;
            ATTACKINTERVAL = BAOFAATTACKINTERVAL;
 //           StartCoroutine(closeBaofa(BAOFATIME));          
        }
    }
    public void cancle_jinu()
    {
        isbaofa = false;
        ATTACKINTERVAL = 1;
    }

    public GameObject getTarget_MaxHealth()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        int lenth = diren.Length;
        //没有敌人
        if (lenth == 0)
            return null;
        //获取在攻击范围的敌人
        List<GameObject> linshi_diren = new List<GameObject>();
        for (int i = 0; i < lenth; i++)
        {
            if (Vector3.Distance(diren[i].transform.position, transform.position) <= ATTACKDISTANCE)
            {
                linshi_diren.Add(diren[i]);
            }
        }
        int count = linshi_diren.Count;
        //没有敌人进入攻击范围
        if (count == 0)
            return null;
        int max_health = 0;
        GameObject mubiao = null;
        for (int i = 0; i < count; i++)
        {
            int hp = linshi_diren[i].GetComponent<Xiaobing_Controll>().hp;
            if (hp>max_health)
            {
                max_health = hp;
                mubiao = linshi_diren[i];
            }
        }
        return mubiao;
    }
    private void baofa()
    {
        isbaofa = true;
        ATTACKINTERVAL = BAOFAATTACKINTERVAL;
        StartCoroutine(cancel_baofa());
    }
    IEnumerator cancel_baofa()
    {
        yield return new WaitForSeconds(10);
        isbaofa = false;
        ATTACKINTERVAL = 1;
        lvUpTime = 0;
    }
}
