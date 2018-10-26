using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//小兵的生命值
public class Xiaobing_Controll : MonoBehaviour
{
    public Slider slider;                                           //血条
    public GameObject HPtext;                                       //血量值文本
    public int hp;                                                  //当前血量
    public GameObject canvas;                                       //画布
    public Color enemyColor;                                        //敌人用于区分的颜色
    protected int totalhp;                                            //总血量
    protected Text HPtextCom;                                         //血量值组件文本
    protected Vector3 initTransform;                                  //初始位置
    protected Camera main_camera;                                     //主相机
    protected static int INIT_FIELDOFVIEW = 50;                       //初始相机的视野度
    protected Vector3 init_scale ;                                    //设置scale变量
    protected int tupo_shanghai = 10;                                 //突破_伤害（对玩家）
    protected float speed;                                            //移动速度
    public float initspeed;                                           //初始移动速度
    protected List<float> multiple = new List<float>();               //速度乘法系数
    protected List<float> plus = new List<float>();                   //速度加法系统
    private MeshRenderer[] child_meshRenderers;                       //子的渲染器  
    private bool canmove = true;                                      //无法移动
    private float canmoveTime=0;                                      //无法移动时间
    protected void Start()
    {
        totalhp = 0;
        totalhp = hp;
        HPtextCom = (Text)HPtext.GetComponent<Text>();
        initTransform = transform.position;
        main_camera = Camera.main.GetComponent<Camera>();
        init_scale = canvas.transform.localScale;
        speed = initspeed;
        child_meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
        //改变外观
        int len = child_meshRenderers.Length;
        for (int i = 1; i < len; i++)
        {
            child_meshRenderers[i].material.color = enemyColor;
        }
    }
    public void addMaxHP(int ahp)
    {
        hp += ahp;
        totalhp = hp;
    }
    protected void FixedUpdate()
    {
        if (Time.timeScale == 0)
            return;
        if (canmove)
            move();
    }

    protected void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (totalhp != 0)
            slider.value = (float)hp / totalhp;
        else
            slider.value = 0;       

        HPtextCom.text = "" + hp;
        //    transform.position = initTransform + new Vector3(Random.Range(0, 2), 0, Random.Range(0, 2));
        //   transform.position = new Vector3(Mathf.PingPong(Time.time * 10, 10), transform.position.y, transform.position.z);
        //float dis = Vector3.Distance(transform.position, Camera.main.transform.position)-40f;
        //float n=dis>0? 0:dis;
        canvas.transform.localScale =init_scale*main_camera.fieldOfView / INIT_FIELDOFVIEW;
        solve_is_canmove();
    }
    private void solve_is_canmove()
    {
        canmoveTime -= Time.deltaTime;
        if(canmoveTime<0)
        {
            canmove = true;
        }
        else
        {
            canmove = false;
        }
    }


    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void TakeRealDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void move()
    {
        //朝右边走动
        transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        if(transform.position.z>200)
        {
            Destroy(this.gameObject);
            GameObject.Find("PlayerUIManager").GetComponent<PlayerManager>().subGameHealth(tupo_shanghai);
        }
    }

    public void setSpeed()
    {
        speed = initspeed;
        for (int i = 0; i < plus.Count; i++)
            speed += plus[i];
        for (int i = 0; i < multiple.Count; i++)
            speed *= multiple[i];
        if (speed < initspeed / 2)
            speed = 0.5f * initspeed;
    }
    public virtual void changeSpeed(int add_speed_type, float value, float keep_time)
    {
        if (add_speed_type == 0)
            multiple.Add(value);
        else
            plus.Add(value);
        setSpeed();
        StartCoroutine(subSpeed(add_speed_type, value, keep_time));
    }
    IEnumerator subSpeed(int type, float value, float keep_time)
    {
        yield return new WaitForSeconds(keep_time);
        if (type == 0)
            multiple.Remove(value);
        else
            plus.Remove(value);
        setSpeed();
    }
    IEnumerator jiedong(float time)
    {
        yield return new WaitForSeconds(time);
        canmove = true;
    }
    public virtual void xuanyun(float time)
    {
        if (canmoveTime > 0)
            canmoveTime += time;
        else
        {
            canmoveTime = time;
        }
    }
        //void OnMouseEnter()
        //{
        //    print("鼠标进入");
        //}
    public void addHp(int ahp)
    {
        hp = Mathf.Min(hp+ahp,totalhp);
    }
}
