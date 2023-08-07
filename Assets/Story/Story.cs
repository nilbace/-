using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshPro typingText;
    [SerializeField] string[] message;

    [SerializeField] bool nextStory;
    [SerializeField] GameObject continueBtn;
    [SerializeField] GameObject endText;

    float defaltSpeed = 0.1f;
    float speed = 0.1f;
    int textindex;

    private void OnEnable()
    {
        typingText.text = "";
        TypingStart(0);
    }

    public void TypingStart(int messageIndex)
    {
        if(messageIndex < message.Length)
        {
            textindex = messageIndex;
            StartCoroutine(Typing(typingText, message[messageIndex]));
        }
        else if(messageIndex == message.Length)
        {
            //계속버튼 또는 스토리종료표기 켜주기
            if (nextStory)
            {
                continueBtn.SetActive(true);
            }
            else
            {
                endText.SetActive(true);
            }

        }
    }

    IEnumerator Typing(TMPro.TextMeshPro typingText, string message)
    {
        string beforeText = typingText.text;
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = beforeText + message.Substring(0, i + 1);
            if (string.IsNullOrWhiteSpace(message.Substring(0, i + 1)))//) != " ")
            {
                Debug.Log("no Typing");

            }
            else
            {

                TempSound.instance.SFX(TempSound.EffectSoundName.talk);
                yield return new WaitForSeconds(speed);
            }
        }
        typingText.text +=  "\n";
        yield return new WaitForSeconds(speed);
        TypingStart(textindex+1);
    }

    public void SpeedUp()
    {
        defaltSpeed = speed;
        speed = defaltSpeed / 5;
        Debug.Log("Typing speed " + speed);

    }

    public void SpeedBack()
    {
        speed = defaltSpeed;
        Debug.Log("Typing speed " + speed);

    }
}
