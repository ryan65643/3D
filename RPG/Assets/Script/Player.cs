using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度"),Range(0,1000)]
    public float speed=1;

    [Header("攝影機速度"), Range(0, 1000)]
    public float turn = 1;

    [Header("攻擊")]
    private float ATK;

    [Header("血量")]
    private float HP;

    [Header("魔力")]
    private float MP;

    [Header("經驗")]
    private float EXP;

    [Header("等級")]
    private float LV;

    private Rigidbody rig;
    private Animator ani;
    private Transform cam;

    private void FixedUpdate()
    {
        Move();   
    }
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        cam = GameObject.Find("攝影機根物件").transform;
    }

    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 pos = cam.forward * v + cam.right * h;
        rig.MovePosition(transform.position + pos * speed);
        ani.SetFloat("移動", Mathf.Abs(v) + Mathf.Abs(h));
        if (v!=0||h!=0)
        {
            pos.y = 0;
            Quaternion ngle = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, ngle, turn);
        }
    }

    private void Speed()
    {

    }

    private void atk()
    {

    }

    private void hp()
    {

    }

    private void mp()
    {

    }

    private void exp()
    {

    }

    private void lv()
    {

    }
}
