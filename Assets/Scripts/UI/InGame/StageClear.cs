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
            for(int i =0;i<3;i++)
            {
                Stars[i].SetActive(true);
            }
        }
        else if(score >= TwoStarScore)
        {
            for (int i = 0; i < 2; i++)
            {
                Stars[i].SetActive(true);
            }
        }
        else if (score >= OneStarscore)
        {
            for (int i = 0; i < 1; i++)
            {
                Stars[i].SetActive(true);
            }
        }
    }
}
