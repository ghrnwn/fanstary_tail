using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//대사 저장
public class TalkManager : MonoBehaviour
{

    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:5", "하세요 :6" }); //:숫자는 표정
        talkData.Add(2000,new string[] { "하이?:1" });


        portraitData.Add(1000 + 5, portraitArr[5]);
        portraitData.Add(1000 + 6, portraitArr[6]);
        portraitData.Add(2000 + 1, portraitArr[1]);


    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
        return talkData[id][talkIndex];

    }
    public Sprite GetPortrait(int id,int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
