using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pet : MonoBehaviour
{
    [SerializeField] Transform twelve;
    [SerializeField] TMP_Text atkTMP;
    [SerializeField] TMP_Text atkspeedTMP;
    [SerializeField] TMP_Text critperTMP;
    [SerializeField] TMP_Text GoldTMP;
    [SerializeField] TMP_Text ScoreTMP;
    [SerializeField] TMP_Text LifeTMP;

    void Start()
    {
        Init();
    }

    void Init()
    {
        int nowclearindex = Managers.Data.MyHighScoreData.clearStageIndex;
        Transform[] trans = new Transform[12]; 
        for(int i = 0;i<12;i++)
        {
            trans[i] = twelve.GetChild(i).GetChild(1);
        }

        for (int i = 0; i <= nowclearindex - 1; i++)
        {
            if (i == 12) continue;
            trans[i].gameObject.SetActive(false);

        }

        PetStat temp = Managers.Data.GetPetResultStat();
        atkTMP.text = temp.petatk.ToString();
        atkspeedTMP.text = temp.petAtkSpeed.ToString() + "%";
        critperTMP.text = temp.petCritper.ToString() + "%";
        GoldTMP.text = temp.petGoldBonus.ToString() + "%";
        ScoreTMP.text = temp.ScoreBonus.ToString() + "%";
        LifeTMP.text = (temp.HeartBonus / 10).ToString();

    }

    public void CloseBTN()
    {
        Managers.UI.ClosePopup();
    }
}
