using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MonsterSpawner : MonoBehaviour
{
    [SerializeField] Define.MonsterWave _wavesData;
    
    void Start()
    {
        StartCoroutine(MonsterSpawner());
    }

    IEnumerator MonsterSpawner()
    {
        SpawnMonsterWave(_wavesData.waveDatas[4]);
        yield return new WaitForSeconds(3f);
        SpawnMonsterWave(_wavesData.waveDatas[0]);
        SpawnMonsterWave(_wavesData.waveDatas[1]);

        yield return new WaitForSeconds(5f);
        SpawnMonsterWave(_wavesData.waveDatas[2]);

        yield return new WaitForSeconds(4f);
        SpawnMonsterWave(_wavesData.waveDatas[3]);

        yield return new WaitForSeconds(4f);
        SpawnMonsterWave(_wavesData.waveDatas[4]);
        SpawnMonsterWave(_wavesData.waveDatas[5]);
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
}
