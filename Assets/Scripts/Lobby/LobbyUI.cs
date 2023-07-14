using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] GameObject[] bells;

    public void StartBTN()
    {
        Managers.Scene.LoadScene(Define.Scene.StageSelect);
    }
}
