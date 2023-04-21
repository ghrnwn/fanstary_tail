using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingAnimation : MonoBehaviour
{
   [SerializeField] private Text textComponent;
    private bool isAnimating = false; // 코루틴 실행 여부를 판단하는 변수
    private string currentString = string.Empty;
    private Coroutine animateCoroutine;
    TextFileReader textFile;

  
    public float charPerSec = 0.1f;
    private void Start()
    {
        textFile = GetComponent<TextFileReader>();  
    }
    public void AnimateText(string text)
    {
        currentString = text;
        if (isAnimating)
        {
            textComponent.text = "";
            StopCoroutine(animateCoroutine);
            isAnimating = false;
            textComponent.text = currentString;
            textFile.isTypedAll();
            return;
            //1. 스탑코루틴 2.스트링지우기 3.값을 그냥넣기 함수호출하기.
        }
       

        animateCoroutine= StartCoroutine(AnimateTextCoroutine(text));
    }

    private IEnumerator AnimateTextCoroutine(string text)
    {
        isAnimating = true; // 코루틴 실행
        textComponent.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text += text[i];
            yield return new WaitForSeconds(charPerSec);
        }
       textFile.isTypedAll();
        isAnimating = false; // 코루틴 종료
    }
}
