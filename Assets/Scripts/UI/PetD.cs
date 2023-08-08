using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetD : MonoBehaviour
{
    string path = "ETC/PetD/8_2_pet_";
    [SerializeField] Image BackImage;
    [SerializeField] TMPro.TMP_Text reqGold;
    [SerializeField] Button CloseBTN;
    [SerializeField] Button unLockBTN;

    public enum petname
    {
        pig,
        dog,
        chicken,
        monkey,
        sheep,
        horse,
        snake,
        dragon,
        rabbit,
        tiger,
        cow,
        mouse
    }


    void Start()
    {
        CloseBTN.onClick.AddListener(() => Managers.UI.ClosePopup());
        Init();

    }


    void Init()
    {
        

        int index = Managers.Data.NowSelectedPet;
        if (Managers.Data.MyHighScoreData.boughtpet[index] == true)
        {
            BackImage.sprite = Resources.Load<Sprite>(path + GetNameByIndex(index));
            reqGold.text = "";
        }

        else
        {
            BackImage.sprite = Resources.Load<Sprite>(path + GetNameByIndex(index) + "_lock");
            amount = 1000 + 200 * index;
            reqGold.text = amount.ToString();
            unLockBTN.onClick.AddListener(() => UnlockByGold());
        }
        

    }

    string GetNameByIndex(int i)
    {
        string temp;
        temp = ((petname)i).ToString();
        return temp;
    }

    int amount;

    void UnlockByGold( )
    {
        if(Managers.Data.MyStoreData.MyGoldAmount >= amount)
        {
            Managers.Data.MyStoreData.MyGoldAmount -= amount;
            Managers.Data.MyHighScoreData.boughtpet[Managers.Data.NowSelectedPet] = true;

            unLockBTN.onClick.RemoveAllListeners();
            Managers.Data.SaveAllDatas();

            Pet.instance.Init();
            Init();
        }
    }

}
