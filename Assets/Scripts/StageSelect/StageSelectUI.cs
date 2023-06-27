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


    public void BaseBackBTN()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void SelectBoss(string BossName)
    {
        PannelBossImg.GetComponent<Image>().sprite = GameObject.Find(BossName).GetComponent<Image>().sprite;
        StageNameTMP.text = BossName + "스테이지";
        if (BossName == "Chicken")
        {
            
        }
        else if(BossName == "Tiger")
        {

        }
        else if(BossName == "Pig")
        {

        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
