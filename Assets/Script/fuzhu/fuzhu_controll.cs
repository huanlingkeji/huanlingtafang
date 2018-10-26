using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuzhu_controll : MonoBehaviour
{
    private GameObject[] paotas;                        //炮塔对象
    private float move_speed = 10f;                     //天使的移动速度
    private Vector3 init_scale;                         //初始的尺寸
    private GameObject move_target = null;              //移动的目标炮塔
    private float init_distance;                        //初始和炮塔的距离
    private Vector3 target_position;                    //移动点的位置
    private float shellray_time;                        //发射射线时间
    private float init_y;                               //天使的初始的y（高度）
    private STATE state;
    enum STATE
    {
        //待命    ，移动飞，   移动冲，  附体，状态数
        DAIMING, YIDONG_FEI, YIDONG_CHONG, FUTI, STATE_NUM
    };
    //待命        转   移动飞 冲
    //移动飞      转   冲 待命
    //冲          转   附体
    //附体        转   待命

    // Use this for initialization
    void Start()
    {
        init_scale = transform.localScale;
        target_position = transform.position;
        state = STATE.DAIMING;
        init_y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        //       find_move_to_target();
        //point_get_target_position();
        //move_to_target_position();
        solve_state();
     //   Debug.Log(state);
    }
    //找到附体的目标并向它移动
    private void find_target()
    {
        paotas = GameObject.FindGameObjectsWithTag("PaoTa");
        int lenth = paotas.Length;
        if (lenth > 0)
        {
            move_target = paotas[3];
            init_distance = Vector3.Distance(move_target.transform.position, transform.position);
        }
    }
    //朝目的炮塔飞行  靠近则激怒它
    private void move_to_target()
    {
        transform.position += (move_target.transform.position - transform.position).normalized * Time.deltaTime * move_speed;
        float dis = Vector3.Distance(move_target.transform.position, transform.position);
        //距离靠近时激怒目标
        if (dis < 0.1f)
        {
            if (move_target.GetComponent<gaoshepao_Attack>())
                move_target.GetComponent<gaoshepao_Attack>().jinu();
            if (move_target.GetComponent<sigongta_Attack>())
                move_target.GetComponent<sigongta_Attack>().jinu();
            state = STATE.FUTI;
        }
        transform.localScale = init_scale * dis / init_distance;
        transform.Rotate(new Vector3(0, 680 * Time.deltaTime, 0));
    }
    //点击发射射线获取目标点
    private void point_get_target_position()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float validTouchDistance = 200;
            string layerName = "Plane";
            //随鼠标点发射
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //屏幕中心点发射
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, validTouchDistance, LayerMask.GetMask(layerName)))
            {
                Vector3 hitPoint = hitInfo.point;
                target_position = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                transform.LookAt(target_position);
            }
        }
    }
    //在待命时自动转飞行
    private void camera_new_shellray()
    {
        shellray_time += Time.deltaTime;
        if (shellray_time > 0.2f)
        {
            shellray_time = 0;
            float validTouchDistance = 200;
            string layerName = "Plane";
            //随鼠标点发射
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //屏幕中心点发射
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, validTouchDistance, LayerMask.GetMask(layerName)))
            {
                Vector3 hitPoint = hitInfo.point;
                Vector3 temp= new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                if (Vector3.Distance(target_position, temp) > 4)
                {
                    target_position = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                    transform.LookAt(target_position);
                    state = STATE.YIDONG_FEI;
                }
            }
        }
    }
    //屏幕中心发射射线  在非自动模式中使用
    private void camera_shellray()
    {
        shellray_time += Time.deltaTime;
        if (shellray_time > 0.2f)
        {
            shellray_time = 0;
            float validTouchDistance = 200;
            string layerName = "Plane";
            //随鼠标点发射
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //屏幕中心点发射
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, validTouchDistance, LayerMask.GetMask(layerName)))
            {
                Vector3 hitPoint = hitInfo.point;
                target_position = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                transform.LookAt(target_position);
            }
        }
    }
    //朝目标地飞去
    private void move_to_target_position()
    {
        if (transform.position != target_position)
        {
            float dis = Vector3.Distance(transform.position, target_position);
            transform.position += (target_position - transform.position).normalized * (move_speed + dis) * Time.deltaTime;
            if (dis < 0.4f)
            {
                transform.position = target_position;
            }
        }
        else
        {
            state = STATE.DAIMING;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            state = STATE.YIDONG_CHONG;
            find_target();
        }
    }
    //待命部分逻辑
    private void daiMing()
    {
        transform.position += transform.forward * 5 * Time.deltaTime;
        transform.Rotate(new Vector3(0, 60 * Time.deltaTime, 0));
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            state = STATE.YIDONG_FEI;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            state = STATE.YIDONG_CHONG;
            find_target();
        }
    }
    //附体解除的处理
    private void futi_jiechu()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            //让小天使的高度恢复 尺寸恢复
            transform.position = new Vector3(transform.position.x, init_y, transform.position.z);
            transform.localScale = init_scale;
            //取消炮塔的激怒效果
            if (move_target.GetComponent<gaoshepao_Attack>())
                move_target.GetComponent<gaoshepao_Attack>().cancle_jinu();
            if (move_target.GetComponent<sigongta_Attack>())
                move_target.GetComponent<sigongta_Attack>().cancle_jinu();

            state = STATE.YIDONG_FEI;
        }
    }
    private void solve_state()
    {
        switch (state)
        {
            case STATE.DAIMING:
                camera_new_shellray();
                daiMing();
                break;
            case STATE.FUTI:
                //附体时解除附体
                futi_jiechu();
                break;
            case STATE.YIDONG_CHONG:
                move_to_target();
                break;
            case STATE.YIDONG_FEI:
                camera_shellray();
                move_to_target_position();
                break;
            case STATE.STATE_NUM:
                futi_jiechu();
                break;
        }
    }
}

/*
 * 
 * 
 * 
    public GameObject prefab;
	private float validTouchDistance; //200 射线长度
	private string layerName;         //"Ground"
	// Use this for initialization
	void Start () {
		validTouchDistance = 200;
		layerName = "Plane";	
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //摄像机需要设置MainCamera的Tag这里才能找到
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo , validTouchDistance , LayerMask.GetMask(layerName) ))
			{
				Debug.Log("Hit Plane" );
				GameObject gameObj = hitInfo.collider.gameObject;
				Vector3 hitPoint = hitInfo.point;
				GameObject obj = GameObject.Instantiate (prefab);
				obj.transform.position = hitInfo.point+new Vector3(0,1,0);
				Destroy (obj, 0.5f);
			}
//			if (Physics.Raycast (ray, out hitInfo)) {
//				if (hitInfo.collider.name == "Plane") {
//					GameObject obj = GameObject.Instantiate (prefab);
//					obj.transform.position = hitInfo.point+new Vector3(0,1,0);
//					Destroy (obj, 0.5f);
//				}
//			}
		}
	}
 * 
 */
