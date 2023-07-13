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
        string path = "Prefabs/Stage/StageWave" + i.ToString(); // �������� ���
        GameObject waveStagePrefab = Resources.Load<GameObject>(path); // ��θ� ���� ������ �ε�

        if (waveStagePrefab == null)
        {
            Debug.LogError("Failed to load WaveStage prefab at path: " + path);
            return;
        }

        GameObject waveStageObj = GameObject.Instantiate(waveStagePrefab, Vector3.zero, Quaternion.identity); // ������ �ν��Ͻ�ȭ

        waveStageObj.name = "WaveStageObject";
    }
}
