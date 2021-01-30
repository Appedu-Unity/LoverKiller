using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 遊戲管理器
/// </summary>
public class Gamemanager : MonoBehaviour
{
    [Header("生命數量"), Range(0, 10)]
    public static int live = 3;
    public Text textKeyValue;
    public GameObject final;

    private static int currentLive = 3;
    private GameObject[] imgLives;
    private int numOfKeys = 0;

    private void Awake()
    {
        currentLive = live;
        imgLives = new GameObject[live];
        for (int i = 0; i < imgLives.Length; i++)
        {
            imgLives[i] = GameObject.Find("生" + i);
            imgLives[i].SetActive(true);
        }
        numOfKeys = 0;
        textKeyValue.text = numOfKeys.ToString();
        setCollision();
    }

    private void setCollision()
    {
        // Physics2D.IgnoreCollision(LayerMask.NameToLayer("敵人"),LayerMask.NameToLayer("敵人"))
    }

    public void addLive()
    {
        if (currentLive < live)
        {
            imgLives[currentLive].SetActive(true);
            currentLive++;
        }
    }


    public bool HurtAndCheckDead()
    {
        if (currentLive <= 0)
        {
            return false;
        }
        else
        {
            currentLive--;
            imgLives[currentLive].SetActive(false);
            if (currentLive == 0) final.SetActive(true);
            return false;

        }
    }

    public int GetKeyNumbers()
    {
        return numOfKeys;
    }
    public void addKey()
    {
        numOfKeys++;
        textKeyValue.text = numOfKeys.ToString();
    }

    public void UseKey()
    {
        numOfKeys -= 1;
        textKeyValue.text = numOfKeys.ToString();
    }

    private void Setlive()
    {
        //  陣列欄位[編號] 的 方法
        //  lives[2].SetActive(false);

        for (int i = 0; i < imgLives.Length; i++)
        {
            if (i >= live) imgLives[i].SetActive(false);
        }
    }
    private void Update()
    {
        BakeTomenu();
        QuitGame();
    }
    private void BakeTomenu()
    {
        if (currentLive == 0 && Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("選單");
    }
    private void QuitGame()
    {
        if (currentLive == 0 && Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
