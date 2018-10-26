using Assets.Script.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//修改地板的颜色
public class ChangeColor : MonoBehaviour {

    public int _hang;
    public int _lie;
    private Map gameMap ;
    private Transform[] child_positions ;
    private MeshRenderer[] child_meshRenderers ;
    private bool is_show_put_paota_position=false;
    private Vector3 show_position;
    private float show_time=0;
    public Color[] color;
    // Use this for initialization
    void Start () {

        gameMap = new Map();
        child_positions = this.GetComponentsInChildren<Transform>();
   //     Debug.Log(child_positions[0].name);
        child_meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
  //      Debug.Log(child_meshRenderers[0].name);
        int len = child_positions.Length - 1;
        for (int i = 1; i < len; i++)
        {
            child_positions[i].position=new Vector3((i-1)/48*5,0,(i-1)%48*5);
        }
        for (int i = 1; i < len; i++)
        {
            int hang = (i - 1) / 48;
            int lie = (i - 1) % 48;
            if (gameMap.map[hang][lie] == 0)
            {
                child_meshRenderers[i - 1].material.color = color[0];
            }
            else if(gameMap.map[hang][lie] == 1)
            {
                //   Debug.Log(hang + "  " + lie);
                child_meshRenderers[i - 1].material.color = color[1];
            }
            else
            {
                child_meshRenderers[i - 1].material.color = color[2];
            }
        }

    }

    // Update is called once per frame
    void Update () {
     //   showPosition();
    }

    public void resetShow()
    {
        int hang = (int)(show_position.x + 2.5) / 5;
        int lie = (int)(show_position.z + 2.5) / 5;
        if (gameMap.map[hang][lie] == 0)
        {
            child_meshRenderers[hang * 48 + lie].material.color = color[0];
        }
        else if (gameMap.map[hang][lie] == 1)
        {
            child_meshRenderers[hang * 48 + lie].material.color = color[1];
        }
        else 
        {
            child_meshRenderers[hang * 48 + lie].material.color = color[2];
        }
    }
    public void setShowPosition(Vector3 pos)
    {
        resetShow();
        show_position = pos;
        display();
    }
    private void display()
    {
        int hang = (int)(show_position.x + 2.5) / 5;
        int lie = (int)(show_position.z + 2.5) / 5;
        child_meshRenderers[hang * 48 + lie ].material.color = Color.red;
    }
}
