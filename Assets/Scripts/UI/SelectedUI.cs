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
        FirstInit();
        Init();
    }

    void FirstInit()
    {
        for (int i = 0; i < 12; i++)
        {
            _stageBTNs[i].interactable = false;
        }
        for (int i = 0; i <= Managers.Data.MyHighScoreData.clearStageIndex; i++)
        {
            if (i == 12) continue;
            _stageBTNs[i].interactable = true;
        }
        gameObject.SetActive(false);
    }

    void Init()
    {
        
        if (Managers.Data.MyBellData.NowBellCount > 0)
        {
            _startBTN.interactable = true;
        }
        else
        {
            _startBTN.interactable = false;
        }
        sweepInit();
        CatInit();

    }

    public void GoLobby()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
    int _nowSelectedStage;
    public void SelectStage(int k)
        //k은 0부터 시작 n은 1부터 시작
    {
        gameObject.SetActive(true);
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        int n = k;  _nowSelectedStage = k;
        Managers.Data.SelectedBossindex = n;
        StageInfoData thisStageData = stageInfos.datas[k];

        _stageName.text = (n+1).ToString();
        _myHighScore.text = Managers.Data.MyHighScoreData.HighScores[k].ToString();
        _321ReqScoreTmps[0].text = "점수 " +thisStageData.ThreeStarScore.ToString() + "점 이상";
        _321ReqScoreTmps[1].text = "점수 " + thisStageData.TwoStarScore.ToString() + "점 이상";
        _321ReqScoreTmps[2].text = "점수 " + thisStageData.OneStarScore.ToString() + "점 이상";

        //별 몇개인지 보여주기
        int myscore = Managers.Data.MyHighScoreData.HighScores[k];


        _bossImg.sprite = thisStageData.BossImg;

        SweepBTN.interactable = false;
        if(myscore >= thisStageData.ThreeStarScore)
        {
            _123stars[0].SetActive(true);
            _123stars[1].SetActive(true);
            _123stars[2].SetActive(true);
            SweepBTN.interactable = true;
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
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if (Managers.Data.CanUseBell())
        {
            Managers.Data.UseBell();
            Managers.Data.SaveAllDatas();
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    #region Sweep

    int _nowSelSweepCount = 0;
    int _nowSweepTicketCount;

    [Header("Sweep")]
    [SerializeField] TMP_Text nowSweepCount;
    [SerializeField] TMP_Text selSweepCount;
    [SerializeField] Button PlusBTN;
    [SerializeField] Button MinBTN;
    [SerializeField] Button SweepBTN;

    void sweepInit()
    {
        _nowSweepTicketCount = Managers.Data.MyStoreData.MySweepTicketAmount;
        selSweepCount.text = _nowSelSweepCount.ToString();
        nowSweepCount.text = _nowSweepTicketCount.ToString();
        if (_nowSelSweepCount >= _nowSweepTicketCount)
        {
            PlusBTN.interactable = false;
        }
        else
        {
            PlusBTN.interactable = true;
        }

        if(_nowSelSweepCount <=0)
        {
            MinBTN.interactable = false;
        }
        else
        {
            PlusBTN.interactable = true;
        }

        
    }
    public void PlusMinSweepBTN(int n)
    {
        _nowSelSweepCount += n;
        Init();
    }

    public void UseSweepTicket()
    {
        int temp = Managers.Data.MyHighScoreData.HighGoldScores[_nowSelectedStage] * _nowSelSweepCount;

        Managers.Data.MyStoreData.MySweepTicketAmount -= _nowSelSweepCount;
        Managers.Data.MyStoreData.MyGoldAmount += temp;
        
        Managers.UI.ShowPopup(Define.Popup.SweepResult);
        GameObject.Find("SweepResult(Clone)").GetComponent<SweepResult>().Setting(_nowSelSweepCount);
        _nowSelSweepCount = 0;
        Init();
    }

    #endregion

    #region Cat
    [Header("Cat")]
    [SerializeField] TMP_Text catName;
    [SerializeField] Image CatImg;

    int _nowcatIndex;
    string path = "Prefabs/PlayerData";


    void CatInit()
    {
        _nowcatIndex = Managers.Data.MyCharDatas.nowSelectCatIndex;
        PlayerData temp1 = Resources.Load<PlayerData>(path);
        InGameData temp2 = temp1.Chars[_nowcatIndex];

        catName.text = "Lv." + Managers.Data.GetThisCatStat(StatName.Total) + temp2.CatName;
        CatImg.sprite = temp2.FrontImg;
    }

    private void FixedUpdate()
    {
        if(_nowcatIndex != Managers.Data.MyCharDatas.nowSelectCatIndex)
        {
            Init();
        }
    }

    public void CatChangeBTN()
    {
        Managers.UI.ShowPopup(Popup.CharPet);
    }


    #endregion

}
