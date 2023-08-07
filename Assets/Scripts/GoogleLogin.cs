using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class GoogleLogin : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text GoogleTMP;

    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Login();
    }


    void Login()
    {
        if(PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success) GoogleTMP.text = $"{Social.localUser.id} |n {Social.localUser.userName}";
                else GoogleTMP.text = "Failed";
            });
        }
    }

   
}
