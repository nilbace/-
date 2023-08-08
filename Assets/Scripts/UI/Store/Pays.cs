using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pays : MonoBehaviour
{
    public enum Result { Success, Fail, Ready}
    public TMPro.TMP_Text Texts;
    public static Pays instance;

    private void Awake()
    {
        instance = this;
    }

    public void Setting( Result result)
    {
        if(result == Result.Success)
        {
            Texts.text = "결제 완료, 우편함에서 수령할 수 있습니다.";
        }
        else if(result == Result.Fail)
        {
            Texts.text = "결제가 실패하였거나, 취소되었습니다.";
        }
        else
        {
            Texts.text = "준비중입니다";
        }
    }
    public void Close()
    {
        Managers.UI.ClosePopup();
    }
}
