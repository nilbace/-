using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class CharPet : MonoBehaviour
{

    public void CloseBTN()
    {
        Managers.UI.ClosePopup();
    }

    public void UpgradeBTN()
    {
        Managers.UI.ShowPopup(Define.Popup.CharUpgrade);
    }

    public void ChangeBTN()
    {
        
    }
}
