using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageClear : MonoBehaviour
{
    int _nowstageNumber;
    [SerializeField] GameObject[] Stars;
    [SerializeField] GameObject[] Rewards;

    [SerializeField] TMP_Text ScoreText;

    GameScene _gamescene;

    private void Start()
    {
        _gamescene = GameScene.instance;
        int _tempOnestar = _gamescene.stageInfoDatas.datas[Managers.Data.SelectedBossindex].OneStarScore;
        int _tempTwostar = _gamescene.stageInfoDatas.datas[Managers.Data.SelectedBossindex].TwoStarScore;
        int _tempThreestar = _gamescene.stageInfoDatas.datas[Managers.Data.SelectedBossindex].ThreeStarScore;
        Setting(_gamescene.ScoreAmount, _tempOnestar, _tempTwostar, _tempThreestar);
    }

    public void Setting(int score, int OneStarscore, int TwoStarScore, int ThreeStarScore)
    {
        ScoreText.text = score.ToString();

        if(score >= ThreeStarScore)
        {
            CheckReward(3);
            for (int i =0;i<3;i++)
            {
                Stars[i].SetActive(true);
            }
        }
        else if(score >= TwoStarScore)
        {
            CheckReward(2);
            for (int i = 0; i < 2; i++)
            {
                Stars[i].SetActive(true);
            }
        }
        else if (score >= OneStarscore)
        {
            CheckReward(1);
            for (int i = 0; i < 1; i++)
            {
                Stars[i].SetActive(true);
            }
        }

        
    }

    void CheckReward(int j)
    {
        for (int i = 0; i < j; i++)
        {
            int temp = Managers.Data.SelectedBossindex;
            if (Managers.Data.MyHighScoreData.GetReward[(temp * 3) + i] == false)
            {
                Rewards[i].SetActive(true);
                switch(i)
                {
                    case 0:
                        Givestar1Reward();
                        break;
                    case 1:
                        Givestar2Reward();
                        break;
                    case 2:
                        Give3StarReward();
                        break; 
                }
            }

        }
    }

    void Givestar1Reward()
    {
        int basereward = 1000;
        basereward += Managers.Data.SelectedBossindex * 100;
        int temp = Managers.Data.SelectedBossindex;

        Managers.Data.MyHighScoreData.GetReward[temp * 3] = true;
        Managers.Data.MakeAndAddMail(basereward, 0, 0, 0, 0, 0, $"스테이지{temp + 1} 1별 보상");
    }

    void Givestar2Reward()
    {
        int basereward = 2000;
        basereward += Managers.Data.SelectedBossindex * 200;
        int temp = Managers.Data.SelectedBossindex;

        Managers.Data.MyHighScoreData.GetReward[(temp * 3) + 1] = true;
        Managers.Data.MakeAndAddMail(basereward, 0, 0, 0, 0, 0, $"스테이지{temp + 1} 2별 보상");
    }

    void Give3StarReward()
    {
        int temp = 0;
        int stage = Managers.Data.SelectedBossindex;
        switch (stage)
        {
            case 0:
                temp = 5;
                break;
            case 1:
                temp = 7;
                break;
            case 2:
                temp = 10;
                break;
            case 3:
                temp = 14;
                break;
            case 4:
                temp = 17;
                break;
            case 5:
                temp = 21;
                break;
            case 6:
                temp = 26;
                break;
            case 7:
                temp = 30;
                break;
            case 8:
                temp = 34;
                break;
            case 9:
                temp = 41;
                break;
            case 10:
                temp = 45;
                break;
            case 11:
                temp = 50;
                break;
        }

        Managers.Data.MyHighScoreData.GetReward[(stage * 3) + 2] = true;
        Managers.Data.MakeAndAddMail(0, temp, 0, 0, 0, 0, $"스테이지{stage + 1} 3별 보상");
    }
}
