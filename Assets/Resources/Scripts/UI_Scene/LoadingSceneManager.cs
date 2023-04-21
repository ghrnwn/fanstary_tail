using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingSceneManager : MonoBehaviour
{
    public GameObject anyKeyNext;
    public static string nextScene;
    [SerializeField]
    Scrollbar Scroll;
    AsyncOperation op;
    private float progressing;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    private void Update()
    {
        if (anyKeyNext && anyKeyNext.activeSelf == true && Input.anyKeyDown)
        {
            op.allowSceneActivation = true;
        }
    }
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
      //  SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressing = Mathf.Lerp(Scroll.size, op.progress, timer);
                
                if (Scroll.size >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                Scroll.size = Mathf.Lerp(Scroll.size, 1f, timer);
                if (Scroll.size == 1.0f)
                {
                    anyKeyNext.SetActive(true);
                    yield break;
                }
            }
        }
    }
}