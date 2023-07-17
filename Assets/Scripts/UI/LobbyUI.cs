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
    int _lastGold;
    int _lastRuby;

    private void Start()
    {
        _lastbellCount = Managers.Data.MyBellData.NowBellCount;
        _lastGold = Managers.Data.MyStoreData.MyGoldAmount;
        _lastRuby = Managers.Data.MyStoreData.MyRubyAmount;

        for (int i =0;  i< Managers.Data.MyBellData.NowBellCount; i++)
        {
            bells[i].gameObject.SetActive(true);
        }
        moneyText.text = Define.FormatNumber(Managers.Data.MyStoreData.MyGoldAmount);
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

        if(_lastbellCount != Managers.Data.MyBellData.NowBellCount ||
            _lastGold     != Managers.Data.MyStoreData.MyGoldAmount ||
            _lastRuby     != Managers.Data.MyStoreData.MyRubyAmount
            )
        {
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

    public void OpenCharPet()
    {
        Managers.UI.ShowPopup(Define.Popup.CharPet);
    }

    public void StageSelectBackBTN()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
}
