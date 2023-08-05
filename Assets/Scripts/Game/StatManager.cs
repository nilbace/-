using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public PlayerData playerdata;
    public int[] ResultStats = new int[6];
    public static StatManager instance;
    public int _nowcatindex;

    void Awake()
    {
        instance = this;
        _nowcatindex = Managers.Data.MyCharDatas.nowSelectCatIndex;
        PetStat temp = Managers.Data.GetPetResultStat();
        for(int i = 0; i<6; i++)
        {
            ResultStats[i] = playerdata.Chars[_nowcatindex].ThreeValues[i].baseStat +
                playerdata.Chars[_nowcatindex].ThreeValues[i].UpValue * Managers.Data.MyCharDatas.charSaveDatas[_nowcatindex].StatLevels[i];

            switch (i)
            {
                case 0:
                    ResultStats[i] += temp.petatk;
                    break;
                case 1:
                    ResultStats[i] += temp.petAtkSpeed;
                    break;
                case 4:
                    ResultStats[i] += temp.petGoldBonus;
                    break;
                case 5:
                    ResultStats[i] += temp.HeartBonus;
                    break;

            }
            
        }
    }

    public int GetReusltStat(Define.StatName statname)
    {
        if (statname == Define.StatName.HpBonus)
            return ResultStats[(int)Define.StatName.HpBonus] / 10;

        return ResultStats[(int)statname];
    }
}
