using UnityEngine;
using UnityEngine.SceneManagement;  //引用 場景管理 API
using UnityEngine.UI;

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
    private void SetCollision()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("player_magic"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemy"), LayerMask.NameToLayer("enemy_magic"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemy_magic"), LayerMask.NameToLayer("player_magic"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemy_magic"), LayerMask.NameToLayer("enemy_magic"));
    }

    public void retBtn()
    {
        SceneManager.LoadScene(0);
    }

}
