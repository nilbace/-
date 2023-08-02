using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void QuitBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Scene.LoadScene(Define.Scene.StageSelect);
    }

    public void ContinueBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Time.timeScale = 1;
        Managers.UI.ClosePopup();
    }
}
