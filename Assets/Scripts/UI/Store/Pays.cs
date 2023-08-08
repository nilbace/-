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
            Texts.text = "���� �Ϸ�, �����Կ��� ������ �� �ֽ��ϴ�.";
        }
        else if(result == Result.Fail)
        {
            Texts.text = "������ �����Ͽ��ų�, ��ҵǾ����ϴ�.";
        }
        else
        {
            Texts.text = "�غ����Դϴ�";
        }
    }
    public void Close()
    {
        Managers.UI.ClosePopup();
    }
}
