using Assets.Script.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Introduce_controll : MonoBehaviour {
    private int page = 0;
    private GameObject UI1;
    private GameObject UI2;
    private Button nextpage;
    private Button lastpage;
    private Button end;
    // Use this for initialization
    void Start () {
        UI1 = GameTools.FindGameObjectByParent("MainUI/UI1/");
        UI2 = GameTools.FindGameObjectByParent("MainUI/UI2/");
        nextpage = (Button)GameTools.FindGameObjectByParent("MainUI/UI1/nextpage/").GetComponent<Button>();
        lastpage = (Button)GameTools.FindGameObjectByParent("MainUI/UI2/lastpage/").GetComponent<Button>();
        end = (Button)GameTools.FindGameObjectByParent("MainUI/UI2/end/").GetComponent<Button>();
        UI2.SetActive(false);
        nextpage.onClick.AddListener(NextPage);
        lastpage.onClick.AddListener(LastPage);
        end.onClick.AddListener(End);
    }
    private void NextPage()
    {
        UI1.SetActive(false);
        UI2.SetActive(true);
    }
    private void LastPage()
    {
        UI1.SetActive(true);
        UI2.SetActive(false);
    }
    private void End()
    {
        SceneManager.LoadScene("shoujiemian");
    }
}
