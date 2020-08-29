using UnityEngine;
using UnityEngine.UI;

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


    /// <summary>
    /// 對話系統
    /// </summary>
    public void Dialong()
    {
        Dialongs.SetActive(true);
        TextName.text=name;
        TextC.text = Date.dialongs[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "月") Dialong();
    }
}
