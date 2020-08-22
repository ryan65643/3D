using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("目標")]
    public Transform tra;
    [Header("速度"), Range(0, 1000)]
    public float speed = 1;
    [Header("速度"), Range(0, 1000)]
    public float turn = 1;
    [Header("角度限制")]
    private Vector2 v2 = new Vector2(-30, 18);

    private Quaternion qua;

    private void Track()
    {
        Vector3 posA = transform.position;
        Vector3 posB = tra.position;
        posA = Vector3.Lerp(posA, posB, Time.deltaTime * speed);
        transform.position = posA;

        qua.x += Input.GetAxis("Mouse Y") * turn;
        qua.y += Input.GetAxis("Mouse X") * turn;
        transform.rotation= Quaternion.Euler(qua.x, qua.y, qua.z);
        qua.x = Mathf.Clamp(qua.x, v2.x, v2.y);
    }
    private void Awake()
    {
        Cursor.visible = false;
    }
    private void LateUpdate()
    {
        Track();
    }
}
    