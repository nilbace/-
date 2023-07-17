using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeList : MonoBehaviour
{
    [SerializeField] TMP_Text NowLevelTMP;
    [SerializeField] TMP_Text MaxLevelTMP;
    [SerializeField] TMP_Text UpLevelValueTMP;
    [SerializeField] Slider NowValueSlider;
    [SerializeField] Button UpgradeButton;
    int _n;
    InGameData temp;

    public void Setting(InGameData charData, int n)
    {
        _n = n; temp = charData;
        NowLevelTMP.text = "Lv " + Managers.Data.GetThisCatStat((Define.StatName)n).ToString();

        MaxLevelTMP.text = "Max" + charData.ThreeValues[n].MaxValue.ToString();

        UpLevelValueTMP.text = charData.ThreeValues[n].StatName + " + " +
                                charData.ThreeValues[n].UpValue.ToString();

        NowValueSlider.value = (float)Managers.Data.GetThisCatStat((Define.StatName)n) /
            (float)charData.ThreeValues[n].MaxValue;

        if(n==5)
        {
            UpLevelValueTMP.text = charData.ThreeValues[n].StatName + " + 1";
        }
        

        if(Managers.Data.MyCharDatas.charSaveDatas
            [Managers.Data.SelectedCatIndex].StatLevels[6] > 0)
        {
            UpgradeButton.interactable = true;
        }

        if(n==5 && Managers.Data.MyCharDatas.charSaveDatas
            [Managers.Data.SelectedCatIndex].StatLevels[6] < 5)
        {
            UpgradeButton.interactable = false;
        }
    }

    public void Upgrade()
    {
        Managers.Data.CalThisCatStat((Define.StatName)_n, 1);
        Managers.Data.CalThisCatStat(Define.StatName.extra, -1);
        if (_n ==5)
        {
            Managers.Data.CalThisCatStat((Define.StatName)_n, 4);
            Managers.Data.CalThisCatStat(Define.StatName.extra, -4);
        }

        Managers.Data.SaveDatas();
        CharUpgrade.instance.Init();
    }

}
