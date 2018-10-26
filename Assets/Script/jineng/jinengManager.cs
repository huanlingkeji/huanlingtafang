using Assets.Script.Game;
using Assets.Script.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class jinengManager : MonoBehaviour {

    private GameObject circle;                                   //显示技能范围的圈
    private GameObject wujiaoxing;                               //显示技能的五角星
    private Image[] iamge_time = new Image[5];                  //控制显示
    private ChangeColor changecolor;                            //改变地图颜色
    private Button[] jienng = new Button[5];                    //技能按钮
    private float[] lenque_time = new float[5];                  //技能冷却时间
    private float[] total_lenque_time = new float[5];           //最大冷却时间
    private base_jineng[] jinengCom = new base_jineng[5];       //五个技能的脚本组件
    private int choose_num = 0;                                 //没有选择技能
    private GameObject tips;                                    //技能未冷却的提示
    private int[] radius = { 20, 30, 15, 15, 10 };              //技能范围
    private put_paota putCom;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < 5; i++)
        {
            jienng[i] = (Button)GameTools.FindGameObjectByParent("GameUI/jinengUI/jineng" + (i + 1) + "/").GetComponent<Button>();
            iamge_time[i] = (Image)GameTools.FindGameObjectByParent("GameUI/jinengUI/jineng" + (i + 1) + "/").GetComponent<Image>();
            int temp = i;
         //   Debug.Log("添加响应事件" + temp);
            jienng[i].onClick.AddListener(() =>
            {
            //    Debug.Log("添加技能点击响应事件" + temp);
                onclickJineng(temp);
            });
            lenque_time[i] =20;
            total_lenque_time[i] = 20;
            jinengCom[i] = (base_jineng)GameTools.FindGameObjectByParent("jinengManager/jineng" + (i + 1) + "/").GetComponent<base_jineng>();
        }
        changecolor = GameObject.Find("points").GetComponent<ChangeColor>();
        circle = (GameObject)GameTools.FindGameObjectByParent("jinengManager/circle/");
        circle.GetComponent<TrollDrawLine>().setRadius(10);
        circle.SetActive(false);
        wujiaoxing = (GameObject)GameTools.FindGameObjectByParent("jinengManager/wujiaoxing/");
        wujiaoxing.GetComponent<wujiaoxing>().setRadius(10);
        wujiaoxing.SetActive(false);
        tips = (GameObject)GameTools.FindGameObjectByParent("GameUI/jineng_tips/");
        putCom = GameTools.FindGameObjectByParent("GameManager/").GetComponent<put_paota>();
        tips.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale == 0)
            return;
        //控制技能释放
        Controll();
        //空冷却时间
        ControllTime();
        //
        //if (Input.GetMouseButtonDown(0))
        //{
        // //   Debug.Log(GameTools.getMouseToPlaneRayPosition());
        //    jinengCom[4].Create();
        //}
    //    Debug.Log(Input.mousePosition);
    }
    private void ControllTime()
    {
        for (int i = 0; i < 5; i++)
        {
            lenque_time[i] += Time.deltaTime;
            if (lenque_time[i] >= total_lenque_time[i])
            {
                //   Debug.Log(i);
                iamge_time[i].fillAmount = 1;
            }
            else
            {
                iamge_time[i].fillAmount = lenque_time[i] / total_lenque_time[i];
            }
        }
    }
    private void Controll()
    {
        if (Mouse.state == Mouse.State.JINENG)
        {
            jinengCom[choose_num].Prepare();
            if (jinengCom[choose_num].IsReady())
            {
                jinengCom[choose_num].Create();
                lenque_time[choose_num] = 0;
                Cancle();
            }
            //    Debug.Log("执行位置 jineng manager");
            if (GameTools.isDoubleHitMouse1() || Input.GetKeyDown(KeyCode.Escape))
            {
                Cancle();
            }
            show_jineng_fanwei();
        }
    }
    private void show_jineng_fanwei()
    {
        putCom.showPaotaData(-1);
        Vector3 position = GameTools.getMouseToPlaneRayPosition()+ new Vector3(0,0.2f,0);
        circle.transform.position = position;
        wujiaoxing.transform.position = position;
    //    Debug.Log("show circle");
    }
    //Button的响应事件
    public void onclickJineng(int num)
    {
        if (Time.timeScale == 0)
            return;
        changecolor.resetShow();
    //    Debug.Log("点击的技能是" + num);
        if (lenque_time[num] < total_lenque_time[num])
        {
            tips.SetActive(true);
            tips.GetComponent<Text>().text = "技能尚未冷却完成";
            StartCoroutine(showTips());
            return;
        }
        Mouse.state = Mouse.State.JINENG;
        choose_num = num;
        circle.SetActive(true);
        circle.GetComponent<TrollDrawLine>().setRadius(radius[choose_num]);
        wujiaoxing.SetActive(true);
        wujiaoxing.GetComponent<wujiaoxing>().setRadius(radius[choose_num]);
    }
    public void Cancle()
    {
        Mouse.state = Mouse.State.NULL;
        circle.SetActive(false);
        wujiaoxing.SetActive(false);
        if (choose_num>=0)
            jinengCom[choose_num].Cancle();
    }
    public void hid_circle()
    {
        circle.SetActive(false);
        wujiaoxing.SetActive(false);
    }
    public void onClickButton()
    {
        Debug.Log("点击事件");
    }

    IEnumerator showTips()
    {
        yield return new WaitForSeconds(2);
        tips.SetActive(false);
    }
}
/*
 * 获取Button  添加事件
 * 获取slider  控制显示冷却
 * 获取技能实例预制体  用于生成技能
 * 记录是否选择技能  是：显示技能范围 （如果点击释放则释放，恢复到没有选择技能状态）  否：不能释放技能
 * 执行判断是否可以释放技能
 * 显示技能范围  五角星加圆形  半径  旋转速度  等设置
 * 让技能脚本继承一个技能基类  
 * 
 */