using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using TMPro;
using UnityEngine.UI;

public class CP_EachChar : MonoBehaviour
{
    [SerializeField] CatName catname;
    [SerializeField] TMP_Text Name;
    [SerializeField] Image CheckBox;
    [SerializeField] Button CharBTN;
    [SerializeField] Button Locker;

    private void Start()
    {
        Init();   
    }

    public void Init()
    {
        CharSaveData thiscat = Managers.Data.MyCharDatas.charSaveDatas[(int)catname];

        Name.text = "Lv." + Managers.Data.GetCatStat(catname, StatName.Total).ToString()+ " " +thiscat.Name;

        if (thiscat.bought)
        {
            Locker.gameObject.SetActive(false);
        }

        if (Managers.Data.MyCharDatas.nowSelectCatIndex == (int)catname)
        {
            CheckBox.gameObject.SetActive(true);
            CharBTN.interactable = false;
        }
        else
        {
            CheckBox.gameObject.SetActive(false);
            CharBTN.interactable = true;
        }
    }

    public void UnLock_Gold(int n)
    {
        int mygold = Managers.Data.MyStoreData.MyGoldAmount;

        if(mygold > n)
        {
            Managers.Data.MyStoreData.MyGoldAmount -= n;
            Managers.Data.MyCharDatas.charSaveDatas[(int)catname].bought = true;
            Managers.Data.SaveAllDatas();
        }
        CharPet.instance.Init();
    }

    public void UnLock_Ruby(int n)
    {
        int myruby = Managers.Data.MyStoreData.MyRubyAmount;

        if (myruby > n)
        {
            Managers.Data.MyStoreData.MyRubyAmount -= n;
            Managers.Data.MyCharDatas.charSaveDatas[(int)catname].bought = true;
            Managers.Data.SaveAllDatas();
        }
        CharPet.instance.Init();
    }

    public void CharChange()
    {
        Managers.Data.MyCharDatas.nowSelectCatIndex = (int)catname;
        Managers.Data.SaveAllDatas();

        CharPet.instance.Init();
    }

}
