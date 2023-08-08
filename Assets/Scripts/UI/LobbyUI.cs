using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text bellPlusText;
    [SerializeField] TMPro.TMP_Text NowBellText;
    [SerializeField] TMPro.TMP_Text moneyText;
    [SerializeField] TMPro.TMP_Text RubyText;

    bool lobbytu = false;

    private void Start()
    {
        Time.timeScale = 1f;
        TempSound.instance.TurnONBGM(TempSound.BGMName.Lobby);

        bool lobbytu = PlayerPrefs.GetInt("lobbytu", 0) == 1;

        if (!lobbytu)
        {
            Managers.Data.StartTuitorial(Define.Tutorials.Lobby);
            PlayerPrefs.SetInt("lobbytu", 1);
        }
    }


    private void FixedUpdate()
    {
        Managers.Data.CalculateAndAddBell();

        NowBellText.text = Managers.Data.MyBellData.NowBellCount.ToString();
        if (Managers.Data.MyBellData.NowBellCount >= 5)
            bellPlusText.text = "";
        else
            bellPlusText.text = Managers.Data.LastTimeForBell();

        moneyText.text = Define.FormatNumber(Managers.Data.MyStoreData.MyGoldAmount);
        RubyText.text = Define.FormatNumber(Managers.Data.MyStoreData.MyRubyAmount);
    }

    public void StartBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Scene.LoadScene(Define.Scene.StageSelect);
    }

    public void ShopBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.Store);
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

    public void OpenMailBox()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.MailBox);
    }

    public void PetBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.Pet);
    }
}
