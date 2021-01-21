using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Speed")]
    public float speed = 3f;
        
    private void LateUpdate()
    {
        Track();
    }
    private void Track()
    {
        Vector3 posCurr = transform.position;
        Vector3 posTarget = target.position;

        posTarget.z = -10;

        posCurr = Vector3.Lerp(posCurr, posTarget, speed * Time.deltaTime);
        transform.position = posCurr;
    }
}
