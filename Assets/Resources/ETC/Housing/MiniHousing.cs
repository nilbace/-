using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHousing : MonoBehaviour
{
    [SerializeField] GameObject[] BossHouse;
    [SerializeField] GameObject newEffect;
    [SerializeField] GameObject openEffect;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= Managers.Data.MyHousingData.houses.Length-1; i++)
        {
            if(i < Managers.Data.MyHighScoreData.HighScores.Length)
            {
                if (Managers.Data.MyHousingData.houses[i] == false && Managers.Data.MyHighScoreData.HighScores[i] > 0)
                {
                    //십이지신집 해금전 처리
                    GameObject copynewEffect = Instantiate(newEffect, BossHouse[i].transform.position, newEffect.transform.rotation);
                    copynewEffect.name = i.ToString();
                    copynewEffect.SetActive(true);
                }
                else if (Managers.Data.MyHousingData.houses[i] == true)
                {
                    //집 해금후 처리
                    BossHouse[i].SetActive(true);
                }
                else
                {
                    //스테이지 미클리어시 처리
                    BossHouse[i].SetActive(false);
                }
            }
            else
            {
                if (Managers.Data.MyHousingData.houses[i] == false && i == 12)
                {
                    //냥이집 해금전 처리
                    GameObject copynewEffect = Instantiate(newEffect, BossHouse[i].transform.position, newEffect.transform.rotation);
                    copynewEffect.name = i.ToString();
                    copynewEffect.SetActive(true);
                }
                else if (Managers.Data.MyHousingData.houses[i] == true)
                {
                    //집 해금후 처리
                    BossHouse[i].SetActive(true);
                }
                else
                {
                    //스테이지 미클리어시 처리
                    BossHouse[i].SetActive(false);
                }
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
        //집과 함께 오픈이펙트 출력
        copyopenEffect.SetActive(true);
        BossHouse[openNumber].SetActive(true);
        //해금데이터 저장
        Managers.Data.MyHousingData.houses[openNumber] = true;
        Managers.Data.SaveHousingData();
        //오픈시 바로 말풍선 띄움
        BossHouse[openNumber].gameObject.GetComponentInChildren<MovingPet>().TouchPet();
    }
}
