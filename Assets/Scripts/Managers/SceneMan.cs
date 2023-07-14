using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan 
{
    public void LoadScene(Define.Scene sceneName)
    {
        string temp = sceneName.ToString();
        SceneManager.LoadScene(temp);
    }
}
