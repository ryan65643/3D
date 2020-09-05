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
    private float ExP = 1000;

    [Header("冷卻時間"), Range(30f, 3000f)]
    private float CD = 2.5f;

    [Header("面向玩家速度"), Range(1f, 1000f)]
    public float turn = 1f;

    private NavMeshAgent nav;
    private Transform Player;
    private Animator ani;
    private float timer;    

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
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
    private void Att()
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
}
