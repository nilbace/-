using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SilverGun : MonoBehaviour
{
    [SerializeField] Button CloseBTN;
    

    // Start is called before the first frame update
    void Start()
    {
        //���� ��ư
        CloseBTN.onClick.AddListener(() => Managers.UI.ClosePopup());



        SilverGunBTN.onClick.AddListener(OnButtonClick);

        // ������ ��ư�� ���� �ð� �ҷ����� (���� ���� �ε��� �� ����Ͻʽÿ�)
        lastButtonPressTime = LoadLastButtonPressTime();

        
    }

    public Button SilverGunBTN;
    private DateTime lastButtonPressTime;



    private void OnButtonClick()
    {
        // ���� �ð� ����
        lastButtonPressTime = DateTime.Now;

        // ��ư ���� ����
        ExecuteButtonAction();


        // ������ ��ư�� ���� �ð� ���� (���� ���� ���� �� ����Ͻʽÿ�)
        SaveLastButtonPressTime(lastButtonPressTime);
    }


    private void ExecuteButtonAction()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);

        TimeSpan timeSinceLastPress = DateTime.Now - lastButtonPressTime;

        if (timeSinceLastPress.TotalDays >= 1)
        {
            buySilverGun();
            Managers.UI.ShowPopup(Define.Popup.PaySuccess);
            Pays.instance.Setting(Pays.Result.Success);
        }
        else
        {
            Managers.UI.ShowPopup(Define.Popup.PaySuccess);
            Pays.instance.Setting(Pays.Result.Fail);
        }
        
    }

    void buySilverGun()
    {
        int ruby = UnityEngine.Random.Range(1, 11);
        int gold = GenerateRandomValue();

        if (ruby == 1)
            Managers.Data.MakeAndAddMail(gold, 30, 0, 0, 0, 0, "���� ����");
        else
        {
            Managers.Data.MakeAndAddMail(gold, 0, 0, 0, 0, 0, "���� ����");
        }

        Managers.Data.SaveAllDatas();
    }

    public int GenerateRandomValue()
    {
        int rand = UnityEngine.Random.Range(1, 101); // 1���� 100������ ���� ���� ����

        if (rand <= 10)
        {
            return 3000;
        }
        else if (rand <= 40)
        {
            return 1000;
        }
        else if (rand <= 100)
        {
            return 500;
        }
        else
        {
            // ���⿡ ó���� ����� �� �߰�
            return 0;
        }

    }

        private void SaveLastButtonPressTime(DateTime time)
    {
        // ������ ��ư�� ���� �ð��� �����ϴ� �ڵ� (��: PlayerPrefs)
        PlayerPrefs.SetString("LastButtonPressTime", time.ToString());
        PlayerPrefs.Save();
    }

    private DateTime LoadLastButtonPressTime()
    {
        // ������ ��ư�� ���� �ð��� �ҷ����� �ڵ� (��: PlayerPrefs)
        string savedTime = PlayerPrefs.GetString("LastButtonPressTime", DateTime.MinValue.ToString());
        return DateTime.Parse(savedTime);
    }

}
