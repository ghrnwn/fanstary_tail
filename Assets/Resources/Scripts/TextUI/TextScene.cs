using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
    public Sprite cg; 
}
public class TextScene : MonoBehaviour
{
    [SerializeField] private Image sprite_StandingCG;
    [SerializeField] private Image sprite_DialougueBox;
    [SerializeField] private Text txt_Dialogue;
    

    private bool isDialogue = false;
    private bool isCoroutine = false;

    private int count = 0;

    [SerializeField] private Dialogue[] dialogue;
    private void DialogueAni(int _count)
    {

    }
    public void ShowDialogue()
    {
        OnOff(true);
        count = 0;
        NextDialogue();
    }

    private void OnOff(bool _flag)
    {
        sprite_DialougueBox.gameObject.SetActive(_flag);
        sprite_StandingCG.gameObject.SetActive(_flag);
        txt_Dialogue.gameObject.SetActive(_flag);
        isDialogue = _flag;
    }

    private void NextDialogue()
    {
        DialogueAni(count);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue[count].dialogue));

        sprite_StandingCG.sprite = dialogue[count].cg;
       
            count++;
    }
    IEnumerator TypeSentence(string sentence)
    {
        isCoroutine = true;
            txt_Dialogue.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                txt_Dialogue.text += letter;
                yield return null;
            }
    }
    //텍스트 애니메이션

    void FinTexting()
    {
        txt_Dialogue.text = "";
        txt_Dialogue.text = dialogue[count-1].dialogue;

    }
   
    void Update()
    {
        if (count == 0)
            ShowDialogue();

        if (isDialogue)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(count ==dialogue.Length)
                {
                    OnOff(false);
                }
                else if (count < dialogue.Length)
                 {
                    if (isCoroutine == true)
                        {
                            StopAllCoroutines();
                            isCoroutine = false;
                            FinTexting();
                        if (count == dialogue.Length - 1)
                            count++;
                        }
                    else if (isCoroutine == false)
                            NextDialogue();
                }
            }
        }
    }
}
