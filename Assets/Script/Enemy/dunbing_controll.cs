using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//盾兵的生命值
public class dunbing_controll : Xiaobing_Controll
{
    private int max_per_second_damage;
    private float damage_time=0;
    private int suffer_damage = 0;
    void Start()
    {
        base.Start();
        max_per_second_damage = (int)(totalhp * 0.1f);
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;
        base.Update();
        damage_time += Time.deltaTime;
        if(damage_time>1f)
        {
            suffer_damage = 0;
            damage_time = 0;
        }
    }

    public override void TakeDamage(int damage)
    {
        if (suffer_damage + damage > max_per_second_damage)
        {
            hp -= max_per_second_damage - suffer_damage;
            suffer_damage = max_per_second_damage;
        }
        else
        {
            hp -= damage;
            suffer_damage += damage;
        }
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
