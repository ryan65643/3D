using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class meauman : MonoBehaviour
{
    [Header("載入畫面")]
    public GameObject Loading;
    [Header("文字")]
    public Text Text;
    [Header("進度條")]
    public Image Ima;
    [Header("載入場景")]
    public string namescenc = "遊戲關卡";
    [Header("提示")]
    public GameObject tip;

    public void Quit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        StartCoroutine(Loaading());
    }
    private IEnumerator Loaading()
    {
        Loading.SetActive(true);
       AsyncOperation ao= SceneManager.LoadSceneAsync(namescenc);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            Text.text = (ao.progress/0.9f * 100).ToString("F2") + "%";
            Ima.fillAmount = ao.progress/0.9f;
            yield return null;
        if (ao.progress==0.9f)
        {
            tip.SetActive(true);
            if(Input.anyKey) ao.allowSceneActivation = true;
        }
        }

    }
}
