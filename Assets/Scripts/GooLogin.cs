using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooLogin : MonoBehaviour
{
    public TMPro.TMP_Text txtLoginResult;

    // Start is called before the first frame update
    void Start()
    {
        txtLoginResult = GetComponent<TMPro.TMP_Text>();
        var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Login();
    }

    public void Login()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (success) =>
        {
            if (success == SignInStatus.Success)
                txtLoginResult.text = "Success";
            else
                txtLoginResult.text = "Failed";
        });
    }
}
