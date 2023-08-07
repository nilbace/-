using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EachPet : MonoBehaviour
{
    public Image charImg;
    public TMP_Text charNameTMP;
    public Image charLock;
    public GameObject Locker;
    public TMP_Text LockerText;
    public int thispetIndex;

    public enum petKorName { 
    ����, ��, ��, ������, ��, ��, ��, ��, �䳢, ȣ����, ��, ��}

    public void Setting(Sprite Img, int index)
    {
        charImg.sprite = Img;

        charImg.gameObject.GetComponent<Button>().onClick.AddListener(() => setDataPetIndex());

        charNameTMP.text = GetNameByIndex(index);

        if(Managers.Data.MyHighScoreData.boughtpet[index] == true)
        {
            charLock.gameObject.SetActive(false);
        }

        if(Managers.Data.MyHighScoreData.clearStageIndex > index)
        {
            Locker.SetActive(false);
        }

        LockerText.text = (index + 1).ToString();
    }

    void setDataPetIndex()
    {
        Managers.Data.NowSelectedPet = thispetIndex;
        Managers.UI.ShowPopup(Define.Popup.PetD);
    }

    string GetNameByIndex(int i)
    {
        string temp;
        temp = ((petKorName)i).ToString();
        return temp;
    }

    
}
