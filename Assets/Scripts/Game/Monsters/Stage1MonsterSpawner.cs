using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MonsterSpawner : MonoBehaviour
{
    [SerializeField] Define.MonsterWave _wavesData;
    [Header("X�� ��, Y�� ����̺�����")]
    [SerializeField] Vector2[] _waveControlData;
    public static Stage1MonsterSpawner instance;
    [SerializeField] GameObject Log;
    [SerializeField] GameObject LogWarning;
    
    void Start()
    {
        instance = this;
        StartCoroutine(MonsterSpawner());
    }

    IEnumerator MonsterSpawner()
    {
        for (int i = 0; i < _waveControlData.Length; i++)
        {
            yield return new WaitForSeconds(_waveControlData[i].x);
            SpawnMonsterWave((int)_waveControlData[i].y);
        }
    }

    public void SpawnMonsterWave(Define.WaveData waveData)
    {
         GameObject waveMonsters = Instantiate(waveData.WaveMonster);
         waveMonsters.transform.position = waveData.SpawnPoint;
         EnemyBase[] enemyBases = waveMonsters.GetComponentsInChildren<EnemyBase>();
         foreach (EnemyBase enemyBase in enemyBases)
         {
             enemyBase.SetMonster(waveData);
         }
    }

    public void SpawnMonsterWave(int n)
    {
        if (n == 0) return;

        //��Ī�� ���
        if(n ==1 || n==4 || n == 31)
        {
            SpawnMonsterWave(FindDataWithWaveIndex(n, "Left"));
            SpawnMonsterWave(FindDataWithWaveIndex(n, "Right"));
        }
        else if(n==35 || n==36 || n==37) // �볪��
        {
            StartCoroutine(LogComming(n));
        }
        else
            SpawnMonsterWave(FindDataWithWaveIndex(n));
    }

    public Define.WaveData FindDataWithWaveIndex(int n, string str = null)
        //n�� �̿��� �������� ã�Ƽ� SpawnMonsterWave�� �Ѱ���
    {
        foreach (Define.WaveData waveData in _wavesData.waveDatas)
        {
            if (waveData.WaveName == "Wave"+n + str)
            {
                return waveData;
            }
        }

        return new Define.WaveData();
    }

    IEnumerator LogComming(int n)
    {
        Vector3 tempPoz = new Vector3((n - 36) * 2.1f, 0f, 0);

        GameObject LogWarn = Instantiate(LogWarning);
        LogWarn.transform.position += tempPoz;
        Destroy(LogWarn,1.8f);
        yield return new WaitForSeconds(1.8f);

        tempPoz += new Vector3(0, 8, 0);
        
        GameObject logGo = Instantiate(Log);
        logGo.transform.position += tempPoz;
        Destroy(logGo, 6f);
    }
}
