using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        LoadWave(Managers.Data.SelectedBossindex);
    }

    void Update()
    {
        
    }
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
}
