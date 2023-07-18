using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void QuitBTN()
    {
        Managers.Scene.LoadScene(Define.Scene.StageSelect);
    }

    public void ContinueBTN()
    {
        GameScene.instance.Quit_Continue();
    }
}
