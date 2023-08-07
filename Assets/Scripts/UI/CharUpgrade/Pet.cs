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
    [SerializeField] Button[] Locks;

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

        for (int i = 0; i < 12; i++)
        {
            Locks[i].transform.GetComponentInChildren<TMPro.TMP_Text>().text = $"스테이지{i + 1}을 클리어하세요";
        }

        for (int i = 0; i <= nowclearindex - 1; i++)
        {
            if (i == 12) continue;
            int tempnum = 1000 + i * 200;
            print($"전달 전 i값{i}");
            int index = i;
            Locks[i].onClick.AddListener(() => UnLockPet(tempnum, index));
            Locks[i].transform.GetComponentInChildren<TMPro.TMP_Text>().text = tempnum.ToString() + "G";
            if (Managers.Data.MyHighScoreData.boughtpet[i] == true)
            {
                Locks[i].gameObject.SetActive(false);
            }
            else
            {
                Locks[i].gameObject.SetActive(true);
            }
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
}
