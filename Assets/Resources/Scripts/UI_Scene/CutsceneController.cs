//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public string nextScene;
    public float waitTime;
    float curTime = 0;


    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > waitTime) SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
}
