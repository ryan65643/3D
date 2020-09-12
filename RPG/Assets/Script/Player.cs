﻿using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("移動速度"),Range(0,1000)]
    public float speed=1;

    [Header("攝影機速度"), Range(0, 1000)]
    public float turn = 1;

    [Header("攻擊")]
    private float ATK=10;

    [Header("血量")]
    private float HP=1000;

    private float maxhp = 1000;

    [Header("魔力")]
    private float MP=100;
    private float MAXMP=1000;

    [Header("經驗")]
    private float EXP;
    private float MAXEXP=100;

    [Header("等級")]
    private int LV= 1;

    [Header("流星雨")]
    public Transform Stone ;
    //隱藏
    [HideInInspector]
    public bool stop;

    [Header("傳送門")]
    public Transform[] doors;
    [HideInInspector]
    public float SA;
    public float STONCAST;
    private Rigidbody rig;
    private Animator ani;
    private Transform cam;
    [Header("介面區塊")]
    public Image barhp;
    public Image barmp;
    public Image barexp;
    private float REMP = 5;
    private Text textLv;
    public float[] exps = new float[999];

    private void FixedUpdate()
    {
        if (stop) return;
        Move();
        ATTK();
        Skill();
        REmp();
    }
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        cam = GameObject.Find("攝影機根物件").transform;

        for (int i = 0; i < exps.Length; i++)
        {
            exps[i] = 100 * (i + 1);
        }
        
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

    private void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (STONCAST<=MP)
            {
                MP -= STONCAST;
                barmp.fillAmount = MP / MAXMP;
            Vector3 pos = transform.forward * 3 + transform.up * 2;
            Instantiate(Stone, transform.position + pos, transform.rotation);
            }
        }
    }

    private void ATTK()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("攻擊觸發");
        }
    }

    private void Dead()
    {
        ani.SetBool("死亡開關", true);
        enabled = false;
    }

    public void atk(float damge,Transform dis)
    {
       
        HP -=  damge;
        ani.SetTrigger("受傷觸發");
        rig.AddForce(dis.forward * 100);
        print(HP);
        HP = Mathf.Clamp(HP, 0, 9999999);
        barhp.fillAmount = HP/maxhp;
        if (HP == 0) Dead();
    }



    public void exp(float getExp)
    {
        EXP += getExp;
        barexp.fillAmount = EXP / MAXEXP;
        while (EXP>=MAXEXP)
        {
            lv();
        }
    }

    private void lv()
    {
        LV++;
        maxhp += 10;
        MAXMP += 10;
        ATK += 10;

        HP = maxhp;
        MP = MAXMP;
        EXP -=MAXEXP;
        MAXEXP = exps[LV - 1];
        barexp.fillAmount =EXP/MAXEXP;
        barhp.fillAmount = 1;
        barmp.fillAmount = 1;
        textLv.text = "LV" + LV;
    }

    public void REmp()
    {
        MP += REMP*Time.deltaTime;
        MP = Mathf.Clamp(MP, 0, MAXMP);
        barmp.fillAmount = MP / MAXMP;
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
        if (other.name=="BOSS")
        {
            other.GetComponent<Enmey>().HIT(ATK, transform);
            print("123");
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
