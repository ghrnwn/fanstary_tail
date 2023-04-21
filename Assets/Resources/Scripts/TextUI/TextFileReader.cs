using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class TextFileReader : MonoBehaviour
{
    [SerializeField] private TextAsset file;
    [SerializeField] private Text uiText;
    [SerializeField] private int simOption = 50;

    StringBuilder sB;
    public Sprite[] cg;
    public Image cgImage;
    public GameObject ActiveManger;
    TypingAnimation typingAnimation;

    private Dictionary<string, List<KeyValuePair<string, int>>> textDictionary;

    private int indexValue = 0;
    private bool isTyped = false;
    public Image endCursor;
    public bool isntLocked = true;

    private void Awake()
    {
        typingAnimation = GetComponent<TypingAnimation>();
        textDictionary = new Dictionary<string, List<KeyValuePair<string, int>>>();
        sB = new StringBuilder(simOption);
    }
    void Start()
    {
        ReadTextFile(file);
        sB.Append("A");
        PrintTextFile(sB.ToString());
    }

    void PrintTextFile(string returnValue)
    {
        endCursor.enabled = false;
        if (isTyped == true)
        {
            indexValue++;
            isTyped = false;
        }
        if (textDictionary.ContainsKey(returnValue))
        {
            if (indexValue <= textDictionary[returnValue].Count - 1)
            {
                KeyValuePair<string, int> textEntry = textDictionary[returnValue][indexValue];
                //uiText.text = textEntry.Key; 
                typingAnimation.AnimateText(textEntry.Key);
                cgImage.sprite = cg[textEntry.Value];
            }
            else
            {
                ActiveManger.SetActive(isntLocked);
                if(isntLocked==false) SceneManager.LoadScene("GameStage2");
            }
        }
    }
    public void AciveLock()
     {
        isntLocked = false;
     }
    public void isTypedAll()
    {
        endCursor.enabled = true;
        isTyped = true;
    }
    public void SetStringValue(string value)
    {
        sB.Append(value);
        indexValue = 0;
        PrintTextFile(sB.ToString());
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintTextFile(sB.ToString());
            Debug.Log(sB.ToString());
        }
   
    }

    void ReadTextFile(TextAsset file)
    {
        if (file == null)
        {
            Debug.LogError("File not found");
            return;
        }

        string[] fileLines = file.text.Split('\n');
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] fields = fileLines[i].Split(',');
            if (fields.Length > 1)
            {
                string key = fields[0];
                List<KeyValuePair<string, int>> values = new List<KeyValuePair<string, int>>();
                for (int j = 1; j < fields.Length; j += 2)
                {
                    string text = fields[j];
                    int portrait = int.Parse(fields[j + 1]);
                    values.Add(new KeyValuePair<string, int>(text, portrait));
                }
                textDictionary.Add(key, values);
            }
        }
    }
    
}
