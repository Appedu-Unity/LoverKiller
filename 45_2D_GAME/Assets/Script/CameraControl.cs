using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("目標")]
    public Transform target;
    [Header("追蹤速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("攝像機下方與上方的限制")]
    public Vector2 limitY = new Vector2(-18, 82);

    [Header("攝像機左方與右方的限制")]
    public Vector2 limitX = new Vector2(-11, 192);


    private void Track()
    {
        Vector3 posA = transform.position;                               //取得攝影機座標
        Vector3 posB = target.position;                                  //取得目標座標

        posB.z = -10;                                                    //固定Z軸
        posA = Vector3.Lerp(posA, posB, speed * Time.deltaTime);         //插值
        posA.x = Mathf.Clamp(posA.x, limitX.x, limitX.y);
        posA.y = Mathf.Clamp(posA.y, limitY.x, limitY.y);

        transform.position = posA;                                       //回傳給攝影機
    }

    private void LateUpdate()
    {
        Track();
    }
}
