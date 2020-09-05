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
    //隱藏
    [HideInInspector]
    public bool stop;

    [Header("傳送門")]
    public Transform[] doors;

    private Rigidbody rig;
    private Animator ani;
    private Transform cam;

    private void FixedUpdate()
    {
        if (stop) return;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "傳送門")
        {
            transform.position = doors[1].position;
            doors[1].GetComponent<CapsuleCollider>().enabled = false;
            Invoke("OPENDOORN", 3);
        }
        if (other.name == "傳送門  (1)")
        {
            transform.position = doors[0].position;
            doors[0].GetComponent<CapsuleCollider>().enabled = false;
            Invoke("OPENDOORP", 3);
        }
    }

    private void OPENDOORN()
    {
        doors[1].GetComponent<CapsuleCollider>().enabled = true;
    }

    private void OPENDOORP()
    {
        doors[0].GetComponent<CapsuleCollider>().enabled = true;
    }
}
