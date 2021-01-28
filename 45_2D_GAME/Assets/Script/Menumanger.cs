using UnityEngine;
using UnityEngine.SceneManagement;  //引用 場景管理 API

public class Menumanger : MonoBehaviour
{
    /// <summary>
    /// 開始遊戲
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("關卡一");
    }
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

}
