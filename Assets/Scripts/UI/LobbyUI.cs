using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] GameObject[] bells;
    [SerializeField] TMPro.TMP_Text bellPlusText;
    [SerializeField] TMPro.TMP_Text moneyText;
    [SerializeField] TMPro.TMP_Text RubyText;
    int _lastbellCount;

    private void Start()
    {
        _lastbellCount = Managers.Data.MyBellData.NowBellCount;

        for (int i =0;  i< Managers.Data.MyBellData.NowBellCount; i++)
        {
            bells[i].gameObject.SetActive(true);
        }
        moneyText.text = Managers.Data.MyStoreData.MyGoldAmount.ToString();
        RubyText.text = Managers.Data.MyStoreData.MyRubyAmount.ToString();
    }

  

    private void FixedUpdate()
    {
        if (Managers.Data.MyBellData.NowBellCount < 5)
        {
            bellPlusText.text = Managers.Data.LastTimeForBell();
            bellPlusText.gameObject.SetActive(true);
        }
        else if (Managers.Data.MyBellData.NowBellCount == 5)
            bellPlusText.gameObject.SetActive(false);

        if(Managers.Data.LastTimeForBell() == "0:-1")
        {
            Managers.Data.CalculateAndAddBell();
            Start();
        }
    }

    public void StartBTN()
    {
        Managers.Scene.LoadScene(Define.Scene.StageSelect);
    }

    public void OpenSetting()
    {
        Managers.UI.ShowPopup(Define.Popup.Setting);
    }

    public void StageSelectBackBTN()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
}
