using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHousing : MonoBehaviour
{
    [SerializeField] GameObject[] BossHouse;
    [SerializeField] GameObject[] talkBox;
    [SerializeField] GameObject newEffect;
    [SerializeField] GameObject openEffect;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= Managers.Data.MyHousingData.houses.Length; i++)
        {
            if (i == 12) continue;

            if(Managers.Data.MyHousingData.houses[i]==false && Managers.Data.MyHighScoreData.HighScores[i] > 0)
            {
                GameObject copynewEffect = Instantiate(newEffect, BossHouse[i].transform.position, newEffect.transform.rotation);
                //copynewEffect.transform.localScale = new Vector3(0.5f, 0.5f);
                copynewEffect.name = i.ToString();
                copynewEffect.SetActive(true);
            }
            else if (Managers.Data.MyHousingData.houses[i] == true)
            {
                BossHouse[i].SetActive(true);
            }
            else
            {
                BossHouse[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewHouseOpen(Transform transform)
    {
        int openNumber = int.Parse(transform.gameObject.name);
        GameObject copyopenEffect = Instantiate(openEffect, transform.position, transform.rotation);
        transform.gameObject.SetActive(false);
        copyopenEffect.SetActive(true);
        BossHouse[openNumber].SetActive(true);
        Managers.Data.MyHousingData.houses[openNumber] = true;
        Managers.Data.SaveHousingData();
    }
}
