using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class SelectedUI : MonoBehaviour
{
    [SerializeField] GameObject[]    _123stars;
    [SerializeField] Image _bossImg;
    [SerializeField] TMP_Text[] _321ReqScoreTmps;
    [SerializeField] TMP_Text _stageName;
    [SerializeField] TMP_Text _myHighScore;
    [SerializeField] StageInfoDatas stageInfos;
    [SerializeField] Button[] _stageBTNs;
    [SerializeField] Button   _startBTN;

    private void Start()
    {
        for(int i = 0; i <= Managers.Data.MyHighScoreData.clearStageIndex; i++)
        {
            if (i == 12) continue;
            _stageBTNs[i].interactable = true;
        }
        
        gameObject.SetActive(false);
        if(Managers.Data.MyBellData.NowBellCount>0)
        {
            _startBTN.interactable = true;
        }
        else
        {
            _startBTN.interactable = false;
        }
    }

    public void GoLobby()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }

    public void SelectStage(int k)
        //k�� 0���� ���� n�� 1���� ����
    {
        int n = k;
        Managers.Data.SelectedBossindex = n;
        StageInfoData thisStageData = stageInfos.datas[k];

        _stageName.text = "STAGE" + (n+1).ToString();
        _myHighScore.text = "BEST SCORE : " + Managers.Data.MyHighScoreData.HighScores[k].ToString();
        _321ReqScoreTmps[0].text = "���� " +thisStageData.ThreeStarScore.ToString() + "�� �̻�";
        _321ReqScoreTmps[1].text = "���� " + thisStageData.TwoStarScore.ToString() + "�� �̻�";
        _321ReqScoreTmps[2].text = "���� " + thisStageData.OneStarScore.ToString() + "�� �̻�";

        //�� ����� �����ֱ�
        int myscore = Managers.Data.MyHighScoreData.HighScores[k];


        _bossImg.sprite = thisStageData.BossImg;


        if(myscore >= thisStageData.ThreeStarScore)
        {
            _123stars[0].SetActive(true);
            _123stars[1].SetActive(true);
            _123stars[2].SetActive(true);
        }
        else if(myscore >= thisStageData.TwoStarScore)
        {
            _123stars[0].SetActive(true);
            _123stars[1].SetActive(true);
            _123stars[2].SetActive(false);
        }
        else if(myscore >= thisStageData.OneStarScore)
        {
            _123stars[0].SetActive(true);
            _123stars[1].SetActive(false);
            _123stars[2].SetActive(false);
        }
        else
        {
            _123stars[0].SetActive(false);
            _123stars[1].SetActive(false);
            _123stars[2].SetActive(false);
        }
    }

    public void StartGame()
    {
        if(Managers.Data.CanUseBell())
        {
            //Managers.Data.UseBell();
            Managers.Data.SaveAllDatas();
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

}
