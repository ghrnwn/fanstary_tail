using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAndPause : MonoBehaviour
{
    enum SceneName { Title, GameStage = 2 };
    public static int saveProgress;
    string loadSceneName;
    public Transform[] savePoint;

    bool readyLoadScene = false;
    private void Start()
    {
        if (Player.p != null && savePoint.Length > saveProgress)
        {
            Player.p.transform.position = savePoint[saveProgress].position;
            Player.p.transform.localEulerAngles = savePoint[saveProgress].localEulerAngles;
        }
    }
    public void saveReset()
    {
        saveProgress = 0;
    }
    public void SceneLoadWithFade(string scene)
    {
        if (scene == "Title") 
            saveReset();
        if(scene == "GameStage")
            Player.score = 0;
        if (scene == "Game EndScene") Cursor.lockState = CursorLockMode.None;
            if (readyLoadScene == true)
        {
            Time.timeScale = 1;
            if (scene == "50")
            {
                Application.Quit();
                return;
            }
            else
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        else
        {
            loadSceneName = scene;
            StartCoroutine(FadeTime());
        }
    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSecondsRealtime(3);
        readyLoadScene = true;
        SceneLoadWithFade(loadSceneName);
        yield break;
    }
}
