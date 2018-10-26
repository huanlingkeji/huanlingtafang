using Assets.Script.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour {

    public Slider gameHealth_slider;
    public Text gameHealth_text;

    private static int totalGameHealth = 100;
    private int gameHealth = totalGameHealth;

    private GameObject gameover;
    private Button returnMenu;
    private Button continueGame;

    private GameObject pause;
    private Button preturnMenu;
    private Button pcontinueGame;

    private Text victory;
    private float gameTime;         //坚守500秒
    private Text remainTime;
    private GameObject gameTaskTips;

    private string tasks = "任务提示：在时间之内防止敌军突破防线";
    // Use this for initialization
    void Start()
    {
        gameTime = 500;
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        gameHealth_text.text = "" + gameHealth;

        gameover = GameTools.FindGameObjectByParent("GameUI/GameOver/");
        returnMenu = (Button)GameTools.FindGameObjectByParent("GameUI/GameOver/returnMenu/").GetComponent<Button>();
        continueGame = (Button)GameTools.FindGameObjectByParent("GameUI/GameOver/continueGame/").GetComponent<Button>();
        returnMenu.onClick.AddListener(ReturnManu);
        continueGame.onClick.AddListener(ContionueGame);

        victory = (Text)GameTools.FindGameObjectByParent("GameUI/GameOver/victory/").GetComponent<Text>();
        remainTime = (Text)GameTools.FindGameObjectByParent("GameUI/remainTime/").GetComponent<Text>();
        gameTaskTips = GameTools.FindGameObjectByParent("GameUI/task_tips/");

        pause = GameTools.FindGameObjectByParent("GameUI/GamePause/");
        preturnMenu = (Button)GameTools.FindGameObjectByParent("GameUI/GamePause/returnMenu/").GetComponent<Button>();
        pcontinueGame = (Button)GameTools.FindGameObjectByParent("GameUI/GamePause/continueGame/").GetComponent<Button>();
        preturnMenu.onClick.AddListener(ReturnManu);
        pcontinueGame.onClick.AddListener(PContionueGame);

        pause.SetActive(false);
        gameover.SetActive(false);
        gameTaskTips.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        setSlider();
        solve_pause();
        solve_victory();
        solve_tasks_tip();
    }
    private void solve_tasks_tip()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            gameTaskTips.GetComponent<Text>().text = tasks;
            gameTaskTips.SetActive(true);
        }
        else
        {
            gameTaskTips.SetActive(false);
        }
    }
    private void solve_victory()
    {
        gameTime -= Time.deltaTime;
        remainTime.text = "防守剩余时间 " + (int)(gameTime / 60) + ":" + (int)gameTime % 60;
        if(gameTime <= 0)
        {
            victory.text = "游戏胜利，是否继续";
            GameOver();
        }
    }
    private void solve_pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pause.SetActive(true);
        }
    }
    private void PContionueGame()
    {
    //    StartCoroutine(jixuyouxi());
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    IEnumerator jixuyouxi()
    {
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    private void setSlider()
    {
        gameHealth_slider.value = 1.0f * gameHealth / totalGameHealth;
    }
    public void subGameHealth(int damage)
    {
        gameHealth -= damage;
        setSlider();
        gameHealth_text.text = "" + gameHealth;
        if (gameHealth <= 0)
            GameOver();//gameover
    }
    private void GameOver()
    {
        gameover.SetActive(true);
        Time.timeScale = 0;
     //   Debug.Log("执行timescale=0  "+Time.time);
    }
    private void ReturnManu()
    {
        SceneManager.LoadScene("shoujiemian");
        Time.timeScale = 1;
    }
    private void ContionueGame()
    {
    //    Debug.Log(Time.timeScale);
        SceneManager.LoadScene("youxijiemian");
        Time.timeScale = 1;
    //    Debug.Log("执行timescale=1  " + Time.time);
    }
}

