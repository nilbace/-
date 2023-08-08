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
        CloseBTN.onClick.AddListener(() => Managers.UI.ClosePopup());
    }

    public Button SilverGunBTN;
    private DateTime lastButtonPressTime;

    private void UpdateButtonState()
    {
        // ��ư�� ���� �ð��� ���� �ð��� ���� ���
        TimeSpan timeSinceLastPress = DateTime.Now - lastButtonPressTime;

        // �Ϸ簡 �������� Ȯ��
        if (timeSinceLastPress.TotalDays >= 1)
        {
            SilverGunBTN.interactable = true; // ��ư Ȱ��ȭ
        }
        else
        {
            SilverGunBTN.interactable = false; // ��ư ��Ȱ��ȭ
        }
    }

    private void OnButtonClick()
    {
        // ���� �ð� ����
        lastButtonPressTime = DateTime.Now;

        // ��ư ���� ����
        ExecuteButtonAction();

        // ��ư Ȱ��ȭ ���� ����
        UpdateButtonState();

        // ������ ��ư�� ���� �ð� ���� (���� ���� ���� �� ����Ͻʽÿ�)
        SaveLastButtonPressTime(lastButtonPressTime);
    }


    private void ExecuteButtonAction()
    {
        Debug.Log("Button Clicked!");
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
