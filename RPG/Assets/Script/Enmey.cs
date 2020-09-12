using UnityEngine;
using UnityEngine.AI;

public class Enmey : MonoBehaviour
{
    [Header("移動速度"), Range(1f, 1000f)]
    public float speed = 1f;

    [Header("攻擊"), Range(30f, 3000f)]
    private float ATK = 300f;

    [Header("血量"), Range(30f, 3000f)]
    private float Hp = 200f;

    [Header("攻擊停止距離"), Range(30f, 3000f)]
    private float Disdant = 800f;

    [Header("經驗")]
    private float ExP = 100;

    [Header("冷卻時間"), Range(30f, 3000f)]
    private float CD = 2.5f;

    [Header("面向玩家速度"), Range(1f, 1000f)]
    public float turn = 1f;

    private NavMeshAgent nav;
    private Transform Player;
    private Animator ani;
    private float timer;
    private Rigidbody rig;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        nav.speed = speed;
        nav.stoppingDistance = Disdant;
        Player = GameObject.Find("月").transform;
        nav.SetDestination(Player.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.35f);
        Gizmos.DrawSphere(transform.position, Disdant);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name=="月")
        {
            float range = Random.Range(10f, -5f);
            other.GetComponent<Player>().atk(ATK+range,transform);
        }
    }
    public void Move()
    {
        nav.SetDestination(Player.position);
        ani.SetFloat("移動", nav.velocity.magnitude);
        if (nav.remainingDistance < Disdant) Att();

    }
    private void Update()
    {
        Move();
        
    }
    public void Att()
    {
        Quaternion look = Quaternion.LookRotation(Player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * turn);

        timer += Time.deltaTime;
        if (timer>=CD)
        {
        timer = 0;
        ani.SetTrigger("攻擊觸發");
        }
    }
    public void Dead()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        ani.SetBool("死亡開關", true);
        enabled = false;
        nav.isStopped = true;
        Player.GetComponent<Player>().exp(ExP);

    }
    public void HIT(float damage,Transform direction)
    {
        Hp -= damage;
        ani.SetTrigger("受傷觸發");
        rig.AddForce(direction.forward * 100+direction.up*150);
        Hp = Mathf.Clamp(Hp, 0, 9999);

        if (Hp == 0) Dead();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.name=="碎石")
        {
            HIT(Player.GetComponent<Player>().SA, Player.transform);
        }
    }
}
