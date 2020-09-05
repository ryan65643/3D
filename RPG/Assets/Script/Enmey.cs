using UnityEngine;
using UnityEngine.AI;

public class Enmey : MonoBehaviour
{
    [Header("移動速度"), Range(1f, 1000f)]
    public float speed = 1f;

    [Header("攻擊"), Range(30f, 3000f)]
    private float ATK = 300f;

    [Header("血量"), Range(30f, 3000f)]
    private float HP = 200f;

    [Header("攻擊停止距離"), Range(30f, 3000f)]
    private float Disdant = 800f;

    [Header("經驗")]
    private float EXP = 1000;

    private NavMeshAgent nav;
    private Transform Player;
    private Animator ani;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        nav.speed = speed;
        nav.stoppingDistance = Disdant;
        Player = GameObject.Find("月").transform;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.35f);
        Gizmos.DrawSphere(transform.position, Disdant);
    }

    public void Move()
    {
        nav.SetDestination(Player.position);
        ani.SetFloat("移動", nav.velocity.magnitude);
        if (nav.remainingDistance < Disdant) Att();
        {

        }
    }
    private void Update()
    {
        Move();
    }
    private void Att()
    {
        ani.SetTrigger("攻擊觸發");
    }
}
