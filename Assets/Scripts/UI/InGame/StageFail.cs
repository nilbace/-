using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFail : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text Scoretext;

    private void Start()
    {
        setScore(GameScene.instance.ScoreAmount);
    }

    public void setScore(int score)
    {
        Scoretext.text = score.ToString();
    }

    public void GoLobbyBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }

    public void StageSelBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Scene.LoadScene(Define.Scene.StageSelect);
    }

    public void CharPetBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.CharPet);
    }
}
