using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊戲管理器
/// </summary>
public class Gamemanager : MonoBehaviour
{

    //陣列
    public GameObject[] lives;
    private static int live = 3;

    private void Awake()
    {
        Setlive();
    }
    public void PlayDead()
    {
        live--;

        Setlive();
    }

    private void Setlive()
    {
        //  陣列欄位[編號] 的 方法
        //  lives[2].SetActive(false);

        for (int i = 0; i < lives.Length; i++)
        {
            if(i >=live) lives[i].SetActive(false);
        }
    }
    private void setCollision()
    { 
   // Physics2D.IgnoreCollision(LayerMask.NameToLayer("敵人"),LayerMask.NameToLayer("敵人"))
    }
}
