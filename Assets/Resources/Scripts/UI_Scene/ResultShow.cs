using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultShow : MonoBehaviour
{
    public int savedRecord;
    public Text playerScore;
    public Text BestRecord;
    public Text newRecord;
    // Start is called before the first frame update
    void Start()
    {
        savedRecord = PlayerPrefs.GetInt("Record",0);
        if (Player.score > savedRecord)
        {
            PlayerPrefs.SetInt("Record", Player.score);
            newRecord.enabled = true;
        }
        playerScore.text = Player.score.ToString();
        BestRecord.text = PlayerPrefs.GetInt("Record", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
