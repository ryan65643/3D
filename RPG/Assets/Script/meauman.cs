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
            Text.text = ao.progress * 100 + "%";
            Ima.fillAmount = ao.progress;
            yield return null;
        }

    }
}
