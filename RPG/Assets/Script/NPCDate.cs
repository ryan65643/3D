using UnityEngine;

public enum NPCState
{
    NMission,Missing,Finish
}
//ScriptableObject 腳本化物件:可儲存於專案的資料
[CreateAssetMenu(fileName ="NPC 資料",menuName ="R/ NPC 資料")]

public class NPCDate : ScriptableObject
{
    [Header("NPC 狀態")]
    public NPCState _NPCStste = NPCState.NMission;
    [Header("任務數量")]
    public int Count;
    [Header("對話")]
    public string[] dialongs = new string[3];
}
