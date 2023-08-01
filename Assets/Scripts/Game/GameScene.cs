using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class GameScene : MonoBehaviour
{
    public static GameScene instance;

    public StageInfoDatas stageInfoDatas;

    [SerializeField] GameObject[] Hearts;
    int _heartCount = 0;
    bool isDead = false;

    bool _bossAppear;
    int _bossMaxHP;
    EnemyBase _bossBase;
    [SerializeField] Image BossHP;
    [SerializeField] GameObject BossGO;

    [SerializeField] TMP_Text ScroeTMP;
    public int ScoreAmount {get; set;}
    [SerializeField] TMP_Text GoldTMP;
    public int GoldAmount { get; set; }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        Time.timeScale = 1f;
        TempSound.instance.TurnONBGM(TempSound.BGMName.Stage);
        LoadWave(Managers.Data.SelectedBossindex);
        SetStartHeart();
        BossGO.SetActive(false);
    }

    void Update()
    {
        ScroeTMP.text = ScoreAmount.ToString();
        GoldTMP.text = Define.FormatNumber(GoldAmount);
        if(_bossAppear)
        {
            BossHP.rectTransform.sizeDelta = new Vector2(GetBossHPRatio(), 100);
        }

        if(_heartCount < 0 && !isDead)
        {
            StageFailed();
        }
    }

    void StageFailed()
    {
        isDead = true;
        Time.timeScale = 0;
        Managers.UI.ShowPopup(Popup.StageFail);
        TempSound.instance.SFX(TempSound.EffectSoundName.result);
    }

    #region Heart
    void SetStartHeart()
    {
        _heartCount = StatManager.instance.GetReusltStat(Define.StatName.HpBonus) - 1;
        SetHeartIMG();
    }

    void SetHeartIMG()
    {
        for(int i = 0; i <5;i++)
        {
            Hearts[i].SetActive(false);
        }
        for (int i = 0; i <= _heartCount; i++)
        {
            Hearts[i].SetActive(true);
        }
    }

    public void PlayerGetDamage( int n = 1)
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.charDie2);
        if (n != 1)
        {
            for(int i = _heartCount; i > _heartCount-n; i--)
            {
                if (i == -1)
                    break;
                Hearts[i].SetActive(false);
            }
            _heartCount -= n;
            return;
        }
        
        Hearts[_heartCount].SetActive(false);
        _heartCount--;
        //Player.instance.PowerLevel--;

        Player.instance.GoInvincibleTime();
    }

    public void AddHeart()
    {
        _heartCount++;
        SetHeartIMG();
    }



    #endregion

    #region Score
    public void GetScore(int n)
    {
        ScoreAmount += n;
    }
    #endregion

    #region Gold

    public void PlayerGetGold(int n)
    {
        GoldAmount += n;
    }

    #endregion

    #region Boss

    public void BossDead()
    {
        StartCoroutine(BossDie());
    }

    bool bossAppeared = false;
    public void BossAppear(int MaxHP, EnemyBase _bossScript)
    {
        if (bossAppeared) return;
        bossAppeared = true;

        BossGO.SetActive(true);
        _bossMaxHP = MaxHP;
        _bossBase = _bossScript;
        _bossAppear = true;
    }

    public float GetBossHPRatio()
    {
        return ((float)_bossBase.MonsterHP / _bossMaxHP * 100);
    }

    IEnumerator BossDie()
    {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0;
        Managers.UI.ShowPopup(Define.Popup.StageClear);
        Managers.Data.MyStoreData.MyGoldAmount += GoldAmount;

        if(Managers.Data.MyHighScoreData.HighScores[Managers.Data.SelectedBossindex] < ScoreAmount)
        Managers.Data.MyHighScoreData.HighScores[Managers.Data.SelectedBossindex] = ScoreAmount;

        Managers.Data.MyHighScoreData.clearStageIndex = Managers.Data.SelectedBossindex + 1;
        Managers.Data.SaveAllDatas();
    }

    #endregion

    #region Bomb

    int bombCount = 1;
    [SerializeField] Button BombBTN;
    [SerializeField] GameObject BombPrefab;
    [SerializeField] TMP_Text BomBTMP;
    public void ShootBomb()
    {
        Instantiate(BombPrefab, transform.position, Quaternion.identity);
        bombCount--;
        if(bombCount <= 0 )
        {
            BombBTN.interactable = false;
        }
        RefreshBomBount();
    }
    
    public void AddBomb()
    {
        bombCount++;
        BombBTN.interactable = true;
        RefreshBomBount();
    }

    void RefreshBomBount()
    {
        BomBTMP.text = bombCount.ToString();
    }


    #endregion


    #region Clear

    IEnumerator showClearUI()
    {
        
        yield return new WaitForSeconds(2f);
        TempSound.instance.SFX(TempSound.EffectSoundName.result);
        Managers.UI.ShowPopup(Define.Popup.StageClear);

    }

    #endregion

    #region LoadWave+UI
    public static void LoadWave(int i)
    {
        string path = "Prefabs/Stage/StageWave" + (i+1).ToString(); // �������� ���
        GameObject waveStagePrefab = Resources.Load<GameObject>(path); // ��θ� ���� ������ �ε�

        if (waveStagePrefab == null)
        {
            Debug.LogError("Failed to load WaveStage prefab at path: " + path);
            return;
        }

        GameObject waveStageObj = GameObject.Instantiate(waveStagePrefab, Vector3.zero, Quaternion.identity); // ������ �ν��Ͻ�ȭ

        waveStageObj.name = "WaveStageObject";
    }


    public void PauseBTN()
    {
        Time.timeScale = 0;
        Managers.UI.ShowPopup(Define.Popup.Pause);
    }

    public void Quit_Continue()
    {
        Time.timeScale = 1;
        Managers.UI.ClosePopup(); Managers.UI.ClosePopup();
    }

    #endregion
}
