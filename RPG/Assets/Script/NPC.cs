using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCDate Date;
    [Header("名稱")]
    public Text TextName;
    [Header("對話區塊")]
    public GameObject Dialongs;
    [Header("內容")]
    public Text TextC;
    [Header("音效")]
    public AudioSource Aud;
    public Player Player;
    public int C;
    [Header("任務區塊")]
    public RectTransform Panel;
    /// <summary>
    /// 對話系統
    /// </summary>
    public void Dialong()
    {
        Dialongs.SetActive(true);
        TextName.text=name;
        StartCoroutine(Print());
     }
    private void CloseDialong()
    {
        Dialongs.SetActive(false);

    }
    /// <summary>
    /// 打字系統
    /// </summary>
    private IEnumerator Print()
    {
        Mission();
        Player.stop = true;
        string diailng = Date.dialongs[(int)Date._NPCStste]; //對話=NPC資料.對話第一段
        TextC.text = "";                  //清空
        TextName.text = "精靈王(木)";
        for (int i = 0; i < diailng.Length; i++) //跑對話第一個字到最後一個字
        {
            TextC.text += diailng[i];   //對話內容+=對話[]
            yield return new WaitForSeconds(0.1F) ;
        }
        Player.stop = false;
        NOMission();
     
    }


    private void NOMission()
    {
        if (Date._NPCStste == NPCState.NMission)
        {
            Date._NPCStste = NPCState.Missing;
            StartCoroutine(MIssionMove());
        }
    }

    private void Mission()
    {
        if (C>=Date.Count) Date._NPCStste = NPCState.Finish;
    }

    private IEnumerator MIssionMove()
    {
        while (Panel.anchoredPosition.x>100)
        {
            Panel.anchoredPosition = Vector3.Lerp(Panel.anchoredPosition, new Vector3(375, 407, 0), 10 * Time.deltaTime);
            yield return null;
        }
    }
    private void Awake()
    {
        Date._NPCStste = NPCState.NMission;
        Aud = GetComponent<AudioSource>();
        Player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "月") Dialong();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "月") CloseDialong();
    }
}
