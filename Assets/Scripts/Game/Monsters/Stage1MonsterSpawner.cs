using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MonsterSpawner : MonoBehaviour
{
    [SerializeField] Define.MonsterWave _wavesData;
    [Header("X´Â ÅÒ, Y´Â ¸î¿þÀÌºêÀÎÁö")]
    [SerializeField] Vector2[] _waveControlData;
    
    void Start()
    {
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

    void SpawnMonsterWave(int n)
    {
        if (n == 0) return;

        //´ëÄªÀÎ ³ðµé
        if(n ==1 || n==4 || n == 31)
        {
            SpawnMonsterWave(FindDataWithWaveIndex(n, "Left"));
            SpawnMonsterWave(FindDataWithWaveIndex(n, "Right"));
        }
        else
            SpawnMonsterWave(FindDataWithWaveIndex(n));
    }

    Define.WaveData FindDataWithWaveIndex(int n, string str = null)
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
}
