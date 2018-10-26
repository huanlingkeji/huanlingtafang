using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//首界面的按键控制
public class ONCLICK : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BeginGame()
    {
        SceneManager.LoadScene("youxijiemian");
    }

    public void GameIntroduce()
    {
        SceneManager.LoadScene("jieshaojiemian");
    }
    
    public void ExistGame()
    {
        Application.Quit();
    }
}
