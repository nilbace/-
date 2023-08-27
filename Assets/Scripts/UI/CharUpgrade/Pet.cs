using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pet : MonoBehaviour
{
    [SerializeField] TMP_Text atkTMP;
    [SerializeField] TMP_Text atkspeedTMP;
    [SerializeField] TMP_Text critperTMP;
    [SerializeField] TMP_Text GoldTMP;
    [SerializeField] TMP_Text ScoreTMP;
    [SerializeField] TMP_Text LifeTMP;
    [SerializeField] Sprite[] PetImgs;
    [SerializeField] Transform ParentTR;
    [SerializeField] EachPet[] eachpets;

    public static Pet instance;

    bool pettu = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Init();

        bool pettu = PlayerPrefs.GetInt("pettu", 0) == 1;

        if (!pettu)
        {
            Managers.Data.StartTuitorial(Define.Tutorials.Pet);
            PlayerPrefs.SetInt("pettu", 1);
        }
    }

    public void Init()
    {
        int nowclearindex = Managers.Data.MyHighScoreData.clearStageIndex;

        for(int i = 0; i<12;i++)
        {
            int temp2 = i;
            eachpets[i].GetComponent<EachPet>().Setting(PetImgs[i], temp2);
        }
        
        PetStat temp = Managers.Data.GetPetResultStat();
        atkTMP.text = temp.petatk.ToString();
        atkspeedTMP.text = temp.petAtkSpeed.ToString() + "%";
        critperTMP.text = temp.petCritper.ToString() + "%";
        GoldTMP.text = temp.petGoldBonus.ToString() + "%";
        ScoreTMP.text = temp.ScoreBonus.ToString() + "%";
        LifeTMP.text = (temp.HeartBonus / 10).ToString();
    }

    void UnLockPet(int gold, int index)
    {
        print(index);
        if(gold <= Managers.Data.MyStoreData.MyGoldAmount)
        {
            Managers.Data.MyStoreData.MyGoldAmount -= gold;
            Managers.Data.MyHighScoreData.boughtpet[index] = true;
            Managers.Data.SaveAllDatas();
            Init();
        }
    }

    public void CloseBTN()
    {
        Managers.UI.ClosePopup();
    }

    public void OpenCharPet()
    {
        Managers.UI.ClosePopup();

        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.CharPet);
    }
}
