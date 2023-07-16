using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedUI : MonoBehaviour
{
    [SerializeField] Image[]    _123stars;
    [SerializeField] TMP_Text[] _321ReqScoreTmps;
   

    public void GoLobby()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }

}
