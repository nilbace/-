using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class GameScene : MonoBehaviour
{
    public static GameScene instance;

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
        LoadWave(Managers.Data.SelectedBossindex);
        SetHeart();
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
            isDead = true;
            Time.timeScale = 0;
            Managers.UI.ShowPopup(Popup.StageFail);
        }
    }

    #region Heart
    void SetHeart()
    {
        _heartCount = StatManager.instance.GetReusltStat(Define.StatName.HpBonus) - 1;
        for(int i = 0; i<= _heartCount; i++)
        {
            Hearts[i].SetActive(true);
        }
    }

    public void PlayerGetDamage( int n = 1)
    {
        if(n != 1)
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


    public void BossAppear(int MaxHP, EnemyBase _bossScript)
    {
        BossGO.SetActive(true);
        _bossMaxHP = MaxHP;
        _bossBase = _bossScript;
        _bossAppear = true;
    }

    public float GetBossHPRatio()
    {
        return ((float)_bossBase.MonsterHP / _bossMaxHP * 100);
    }

    #endregion

    #region Clear

    public void ClearStage(int n)
    {
        StartCoroutine(showClearUI());
    }

    IEnumerator showClearUI()
    {
        yield return new WaitForSeconds(2f);
        Managers.UI.ShowPopup(Define.Popup.StageClear);
    }

    #endregion

    #region LoadWave+UI
    public static void LoadWave(int i)
    {
        string path = "Prefabs/Stage/StageWave" + i.ToString(); // 프리팹의 경로
        GameObject waveStagePrefab = Resources.Load<GameObject>(path); // 경로를 통해 프리팹 로드

        if (waveStagePrefab == null)
        {
            Debug.LogError("Failed to load WaveStage prefab at path: " + path);
            return;
        }

        GameObject waveStageObj = GameObject.Instantiate(waveStagePrefab, Vector3.zero, Quaternion.identity); // 프리팹 인스턴스화

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
