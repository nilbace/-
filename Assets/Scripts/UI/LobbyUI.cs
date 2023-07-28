using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] GameObject[] bells;
    [SerializeField] TMPro.TMP_Text bellPlusText;
    [SerializeField] TMPro.TMP_Text moneyText;
    [SerializeField] TMPro.TMP_Text RubyText;

    private void Start()
    {
        Time.timeScale = 1f;
        TempSound.instance.TurnONBGM(TempSound.BGMName.Lobby);
    }

    private void FixedUpdate()
    {
        Managers.Data.CalculateAndAddBell();

        if(Managers.Data.MyBellData.NowBellCount == 5)
        {
            bellPlusText.gameObject.SetActive(false);
        }
        else
        {
            bellPlusText.gameObject.SetActive(true);
            bellPlusText.text = Managers.Data.LastTimeForBell();
        }
        for (int i = 0; i < 5; i++)
        {
            bells[i].gameObject.SetActive(i<Managers.Data.MyBellData.NowBellCount);
        }

        moneyText.text = Define.FormatNumber(Managers.Data.MyStoreData.MyGoldAmount);
        RubyText.text = Define.FormatNumber(Managers.Data.MyStoreData.MyRubyAmount);
    }

    [ContextMenu("데이터 임시 체크")]
    void showdata()
    {
        moneyText.text = Define.FormatNumber(Managers.Data.MyStoreData.MyGoldAmount) 
            + " / " + Define.FormatNumber(Managers.Data.MyStoreData.MyRubyAmount);
    }

    public void StartBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Scene.LoadScene(Define.Scene.StageSelect);
    }

    public void OpenSetting()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.Setting);
    }

    public void OpenCharPet()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.CharPet);
    }

    public void StageSelectBackBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
}
