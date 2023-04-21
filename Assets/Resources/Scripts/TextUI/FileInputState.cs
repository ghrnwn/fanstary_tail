using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Text;

public class FileInputState : MonoBehaviour
{
   

    [SerializeField] TextAsset file;
    StringBuilder sb;
    [SerializeField] int simOption = 50;// 선택지 최대 계수(더만들고싶으면 늘리세요).
    [SerializeField] Text buttonTextA;
    [SerializeField] Text buttonTextB;
    [SerializeField] Text buttonTextC;

    Dictionary<string, string> buttonTextInputA;
    Dictionary<string, string> buttonTextInputB;
    Dictionary<string, string> buttonTextInputC;

    TextFileReader textFileReader;
    public GameObject ActiveManager;


    void Start()
    {
       
        buttonTextInputA = new Dictionary<string, string>();
        buttonTextInputB = new Dictionary<string, string>();
        buttonTextInputC = new Dictionary<string, string>();
        textFileReader = FindObjectOfType<TextFileReader>();

        ReadFile();
        sb = new StringBuilder(simOption);
        sb.Append("A");
        buttonTextA.text = buttonTextInputA[sb.ToString()];
        buttonTextB.text = buttonTextInputB[sb.ToString()];
        buttonTextC.text = buttonTextInputC[sb.ToString()];
        // print(file.text); 내용물확인
    }
    
    public void ButtonClick(string buttonLetter)
    {
        sb.Append(buttonLetter);
        textFileReader.SetStringValue(buttonLetter);
       // Debug.Log(sb.ToString()); 디버그

        if (buttonTextInputA.ContainsKey(sb.ToString()))
        {
            buttonTextA.text = buttonTextInputA[sb.ToString()];
            buttonTextB.text = buttonTextInputB[sb.ToString()];
            buttonTextC.text = buttonTextInputC[sb.ToString()];
            ActiveManager.SetActive(false);
        }
        else
        {
            ActiveManager.SetActive(false);
            textFileReader.AciveLock();
        }
    }
    public void ButtonA()
    {
        ButtonClick("A");
    }
    public void ButtonB()
    {
        ButtonClick("B");
    }
    public void ButtonC()
    {
        ButtonClick("C");
    }

    void ReadFile()
    {
        var splitFile = new string[] { "\r\n", "\r", "\n" };
        var spileLine = new char[] { ',' };

        var Lines = file.text.Split(splitFile, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < Lines.Length; i++)
        {
            var line = Lines[i].Split(spileLine, System.StringSplitOptions.None);
            string abbr = line[0];
            string buttonA = line[1];
            string buttonB = line[2];
            string buttonC = line[3];


            buttonTextInputA.Add(abbr, buttonA);
            buttonTextInputB.Add(abbr, buttonB);
            buttonTextInputC.Add(abbr, buttonC);
            
            //선택지 추가시 여기 더추가가능.

        }
    }

}
