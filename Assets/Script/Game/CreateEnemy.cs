using Assets.Script.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateEnemy : MonoBehaviour {

    private static int MAXENEMYNUM = 10;
    private int now_num = 0;
    private GameObject[] enemyfab=new GameObject[MAXENEMYNUM];
    public Transform ceate_place;

    private static string enemyPath = "prefab/enemy/";
    private static int CREATEINTERVAL = 15;
    private float create_time=30f;
    private Text attakcTime;

    private float gameTime = 0;
    // Use this for initialization
    void Start()
    {
        LoadEnemy();
        attakcTime = (Text)GameTools.FindGameObjectByParent("GameUI/attackTime/").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;
        createEnemy();
        show_attack_time();
        gameTime += Time.deltaTime;
    }
    private void show_attack_time()
    {
        attakcTime.text = "距离下一波进攻时间剩余：" + (int)create_time + "秒";
    }
    private void createEnemy()
    {
        create_time -= Time.deltaTime;
        if (create_time < 0)
        {
            create_time = CREATEINTERVAL;
            int addnum = (int)gameTime / 60;
            for (int i = 0; i < 10+addnum; i++)
            {
                int x = Random.Range(10, 80);
                int z = Random.Range(5, 40);
                Vector3 position = new Vector3(x, 1.5f, z);
                int n = Random.Range(0, 3);
                GameObject obj = GameObject.Instantiate(enemyfab[n], position, ceate_place.transform.rotation);
                obj.GetComponent<Xiaobing_Controll>().addMaxHP((int)gameTime / 30 * 200);
            }
            for(int i=0;i<2;i++)
            {
                int x = Random.Range(10, 80);
                int z = Random.Range(5, 40);
                Vector3 position = new Vector3(x, 1.5f, z);
                int n = Random.Range(3, now_num);
                GameObject obj = GameObject.Instantiate(enemyfab[n], position, ceate_place.transform.rotation);
                obj.GetComponent<Xiaobing_Controll>().addMaxHP((int)gameTime / 30 * 100);
            }
        }
    }
    private void LoadEnemy()
    {
        LoadEnemy(enemyPath + "xiaobing");
        LoadEnemy(enemyPath + "dunbing");
        LoadEnemy(enemyPath + "tezhongbing");
        LoadEnemy(enemyPath + "jiasubing");
        LoadEnemy(enemyPath + "jiaxuebing");
    }
    private void LoadEnemy(string path)
    {
        if (now_num < MAXENEMYNUM)
        {
            enemyfab[now_num]= Resources.Load(path) as GameObject;
            now_num++;
        }
    }
}
