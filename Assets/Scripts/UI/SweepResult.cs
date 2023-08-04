using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepResult : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text firstTMp;
    [SerializeField] TMPro.TMP_Text secTMp;

    public void Setting(int times)
    {
        int nowstage = Managers.Data.SelectedBossindex;

        firstTMp.text = Managers.Data.MyHighScoreData.HighGoldScores[nowstage].ToString()
            + " X " +times.ToString() + "ȸ Ŭ����";

        secTMp.text = (Managers.Data.MyHighScoreData.HighGoldScores[nowstage] * times).ToString()
            + "���";
    }
    public void CloseBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }
}
