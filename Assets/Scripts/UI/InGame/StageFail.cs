using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFail : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text Scoretext;
    public void setScore(int score)
    {
        Scoretext.text = score.ToString();
    }

    public void GoLobbyBTN()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }

    public void StageSelBTN()
    {
        Managers.Scene.LoadScene(Define.Scene.StageSelect);
    }

    public void CharPetBTN()
    {
        Managers.UI.ShowPopup(Define.Popup.CharPet);
    }
}
