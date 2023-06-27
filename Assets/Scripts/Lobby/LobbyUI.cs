using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public void StartBTN()
    {
         SceneManager.LoadScene("StageSelect");
    }
}
