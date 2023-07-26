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
        for(int i = 0; i<6; i++)
        {
            ResultStats[i] = playerdata.Chars[_nowcatindex].ThreeValues[i].baseStat +
                playerdata.Chars[_nowcatindex].ThreeValues[i].UpValue * Managers.Data.MyCharDatas.charSaveDatas[_nowcatindex].StatLevels[i];
        }
    }

    public int GetReusltStat(Define.StatName statname)
    {
        if (statname == Define.StatName.HpBonus)
            return ResultStats[(int)Define.StatName.HpBonus] / 10;

        return ResultStats[(int)statname];
    }
}
