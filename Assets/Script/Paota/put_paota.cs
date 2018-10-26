using Assets.Script.Model;
using Assets.Script.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Script.Tools;
//放置炮塔

//注意  button组件的物体不要被挡住  不然是无法触发事件的
public class put_paota : MonoBehaviour
{
    private int total_paota_num=12;
    private int initMaxpaota = 12;
    private int now_paota_num=0;
    private Text remain_paota_num;
    private static int MAXPAOTANUM = 10;
    private int now_num = 0;
    private GameObject[] paotafab = new GameObject[MAXPAOTANUM];
    private static string paotaPath = "prefab/paota/";
    private GameObject chooseObj = null;
    private bool ischoose = false;
    private ChangeColor changecolor;
    private Button[] paota = new Button[5];
    private GameObject[] paota_image = new GameObject[5];
    private Text introduction;
    private Text attack;
    private Text attdistance;
    private Text attackinterval;
    private Text cost;
    private Text tip;
    private int choose_num = -1;
    private jinengManager jineng_manager;
    private float gameTime = 0;

    //private float getMouse1DownTime = 0f;
    ////计数器
    //private int getDownCount = 0;
    //// Use this for initialization
    void Start()
    {
    //    Time.timeScale = 0;
        changecolor = GameObject.Find("points").GetComponent<ChangeColor>();
        LoadPaota();
        for (int i = 0; i < 5; i++)
        {
            paota[i] = (Button)GameTools.FindGameObjectByParent("GameUI/paotaUI/paota" + (i + 1) + "/Button/").GetComponent<Button>();
            paota_image[i] = (GameObject)GameTools.FindGameObjectByParent("GameUI/paotaUI/paota" + (i + 1) + "/Image/");
            paota_image[i].SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            int temp = i;
         //   Debug.Log(i);
            paota[i].onClick.AddListener(() => {
             //   Debug.Log("添加炮塔Button点击事件");
                showPaotaData(temp); });
        }

        introduction = (Text)GameTools.FindGameObjectByParent("GameUI/paotaUI/introduction/").GetComponent<Text>();
        attack = (Text)GameTools.FindGameObjectByParent("GameUI/paotaUI/attack/").GetComponent<Text>();
        attdistance = (Text)GameTools.FindGameObjectByParent("GameUI/paotaUI/attdistance/").GetComponent<Text>();
        attackinterval = (Text)GameTools.FindGameObjectByParent("GameUI/paotaUI/attackinterval/").GetComponent<Text>();
        cost = (Text)GameTools.FindGameObjectByParent("GameUI/paotaUI/cost/").GetComponent<Text>();
        jineng_manager = GameTools.FindGameObjectByParent("jinengManager/").GetComponent<jinengManager>();
        tip = (Text)GameTools.FindGameObjectByParent("GameUI/tip/").GetComponent<Text>();
        remain_paota_num = (Text)GameTools.FindGameObjectByParent("GameUI/remain_paota_num/").GetComponent<Text>();
        tip.text="";
    }
    //显示炮塔信息
    public void showPaotaData(int n)
    {
        if (Time.timeScale == 0)
            return;
        for (int i = 0; i < 5; i++)
        {
            if (n == i)
                paota_image[i].SetActive(true);
            else
                paota_image[i].SetActive(false);
        }
        if (n >= 0)
        {
            choose_num = n;
            ischoose = true;
            jineng_manager.Cancle();
            Mouse.state = Mouse.State.PAOTA;
            introduction.text = "炮塔介绍：" + paotaArr.introductions[n];
            attack.text = "炮塔攻击：" + paotaArr.attacks[n];
            attdistance.text = "射击距离：" + paotaArr.attdistances[n];
            attackinterval.text = "攻击间隔：" + paotaArr.attackintervals[n];
            cost.text = "建造花费：" + paotaArr.costs[n];
        }
        else
        {
            introduction.text = "炮塔介绍：";
            attack.text = "炮塔攻击：";
            attdistance.text = "射击距离：";
            attackinterval.text = "攻击间隔：";
            cost.text = "建造花费：";
        }
    }
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        put_Paota();
        showPaotaNum();
        solve_max_paota_num();
    }
    private void solve_max_paota_num()
    {
        gameTime += Time.deltaTime;
        total_paota_num = initMaxpaota + (int)gameTime / 60 * 2;

    }
    private void showPaotaNum()
    {
        remain_paota_num.text = "炮塔建造数：" + now_paota_num + "/" + total_paota_num;
    }
    //放置炮塔
    private void put_Paota()
    {
        //不是选择炮塔状态
        //   Debug.Log(Mouse.state);
        if (Mouse.state != Mouse.State.PAOTA)
            return;
        //双击右键
        changecolor.setShowPosition(GameTools.getMouseToPlaneRayPosition());
        if (GameTools.isDoubleHitMouse1() || Input.GetKeyDown(KeyCode.Escape))
        {
            //   Debug.Log("点击右键");
            Mouse.state = Mouse.State.NULL;
            ischoose = false;
            choose_num = -1;
            showPaotaData(choose_num);
            changecolor.resetShow();
        }
        //    Debug.Log("鼠标状态判断后");
        //放置炮塔
    //    Debug.Log("panduan");
        if (Input.GetMouseButtonUp(0) && ischoose)
        {
            //    ischoose = false;
            //如果点击到了UI
            if (GameTools.isPointUI())
            {
                return;
            }

            if (total_paota_num<=now_paota_num)
            {
                tip.text = "建造炮塔已达到上限";
                StartCoroutine(SetFalseTip(2));
                return;
            }
            Vector3 hitPoint = GameTools.getMouseToPlaneRayPosition();
            int h = (int)(hitPoint.x + 2.5) / 5;
            int l = (int)(hitPoint.z + 2.5) / 5;
            int x = h * 5;
            int z = l * 5;
            if (!Map.isMapBuild[h][l])
            {
                Map.isMapBuild[h][l] = true;
                GameObject obj = GameObject.Instantiate(paotafab[choose_num]);
                obj.transform.position = new Vector3(x, 0, z);
                now_paota_num++;
            }
            else
            {
                tip.text = "该位置已经建造有炮塔";
                StartCoroutine(SetFalseTip(2));
            }
        }
    }
    IEnumerator SetFalseTip(float time)
    {
        yield return new WaitForSeconds(time);
        tip.text = "";
    }
    //加载炮塔 多个
    private void LoadPaota()
    {
        LoadPaota(paotaPath + "tank");
        LoadPaota(paotaPath + "sanshepao");
        LoadPaota(paotaPath + "gaoshepao");
        LoadPaota(paotaPath + "sigongta");
        LoadPaota(paotaPath + "diyuta");
    }
    //加载炮塔 单个
    private void LoadPaota(string path)
    {
        if (now_num < MAXPAOTANUM)
        {
            paotafab[now_num] = Resources.Load(path) as GameObject;
            now_num++;
        }
    }
    private void addPaotaNum(int n)
    {
        total_paota_num += n;
    }
}
