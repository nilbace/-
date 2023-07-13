using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static Define;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] GameObject SelectedStagePannel;
    [SerializeField] GameObject PannelBossImg;
    [SerializeField] TMP_Text StageNameTMP;
    Sprite _tempBossImage;
    static int _nowSelectedBoss;

   

    public void BaseBackBTN()
    {
        SceneManager.LoadScene("Lobby");
    }

    string indexToName(int i)
    {
        Define.BossName temp = (Define.BossName)(i);
        return temp.ToString();
    }

    public void SelectBoss(int BossIndex)
    {
        PannelBossImg.GetComponent<Image>().sprite = GameObject.Find(indexToName(BossIndex)).GetComponent<Image>().sprite;
        StageNameTMP.text = indexToName(BossIndex) + "스테이지";
        _nowSelectedBoss = (int)BossIndex;
        Managers.Data.SelectedBossindex = BossIndex;
    }

    

        public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
