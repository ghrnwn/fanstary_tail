using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIGameStart : MonoBehaviour
{
    void Start()
    {
        LoadingSceneManager.LoadScene("GameStage");
    }
}
