using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public bool gameIsStart;
    public bool field;
    public Entity e;
    public Image fade;
    public Image hpBar;

    public Text score;
    public Text timer;
    int second01, second02;

    public RandomSpawner spawner;
   
    public GameObject pause;
    public GameObject gameover;
    public GameObject gameclear;

    public MenuAndPause mP;

    private void Awake()
    {
        if(fade)
            FadeIn();
    }
    private void Update()
    {
        if (score) score.text = "Score : " + Player.score; 
        if (gameclear && !gameclear.activeSelf && fade && fade.color.a < 0.3f) gameIsStart = true;

        if (timer && spawner)
        {
            if (spawner.enabled == true)
            {
                timer.transform.parent.gameObject.SetActive(true);
            }
            second01 = (int)spawner.timer / 60;
            second02 = ((int)spawner.timer - (second01 * 60));

            if (second02 >= 10) 
            { 
                timer.text = "0" + second01 + " : " + second02;
            }
            else
            {
                if (second01 == 0)
                    timer.text = "<color=#ff0000>" + "0" + second01 + " : " + "0" + second02 + "</color>";
                else
                    timer.text = "0" + second01 + " : " + "0" + second02;
            }
        }
        if (gameIsStart)
        {
            if (pause)
            {
                if (pause.activeSelf == true)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                if (e.hp > 0 && Input.GetKeyDown(KeyCode.Escape))
                {
                    pause.SetActive(!pause.activeSelf);
                }
            }
        }
        if (e)
        {
            hpBar.fillAmount = (float)e.hp / e.maxHp;
        }
        if (field) transform.LookAt(Camera.main.transform);

        if (hpBar && hpBar.fillAmount == 0)
        {
            if(gameover) GameOver();
        }
    }
    public void GameOver()
    {

        pause.SetActive(false);
        gameIsStart = false; 
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;

        gameover.SetActive(true);
    }
    public IEnumerator GameClear()
    {
        Player.p.GetComponent<Player>().invincible = true;

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;

        gameclear.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        FadeOut();
        mP.SceneLoadWithFade("Game EndScene");
        yield break;
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1, 0));
    }
    public void FadeOut()
    {
        gameIsStart = false;
        StartCoroutine(Fade(0, 1));
    }

    IEnumerator Fade(int start, int end)
    {
        float aColor = start;
        while (fade.color.a != end)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            if (start > end) 
            {
                aColor -= 0.1f;
                if (fade.color.a <= end) aColor = end;
            }
            else 
            {
                aColor += 0.1f;
                if (fade.color.a >= end) aColor = end;
            }
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, aColor);
            
        }
        yield break;
    }
}
