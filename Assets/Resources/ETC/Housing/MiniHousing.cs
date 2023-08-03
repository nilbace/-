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
                    //���������� �ر��� ó��
                    GameObject copynewEffect = Instantiate(newEffect, BossHouse[i].transform.position, newEffect.transform.rotation);
                    copynewEffect.name = i.ToString();
                    copynewEffect.SetActive(true);
                }
                else if (Managers.Data.MyHousingData.houses[i] == true)
                {
                    //�� �ر��� ó��
                    BossHouse[i].SetActive(true);
                }
                else
                {
                    //�������� ��Ŭ����� ó��
                    BossHouse[i].SetActive(false);
                }
            }
            else
            {
                if (Managers.Data.MyHousingData.houses[i] == false && i == 12)
                {
                    //������ �ر��� ó��
                    GameObject copynewEffect = Instantiate(newEffect, BossHouse[i].transform.position, newEffect.transform.rotation);
                    copynewEffect.name = i.ToString();
                    copynewEffect.SetActive(true);
                }
                else if (Managers.Data.MyHousingData.houses[i] == true)
                {
                    //�� �ر��� ó��
                    BossHouse[i].SetActive(true);
                }
                else
                {
                    //�������� ��Ŭ����� ó��
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
        //���� �Բ� ��������Ʈ ���
        copyopenEffect.SetActive(true);
        BossHouse[openNumber].SetActive(true);
        //�رݵ����� ����
        Managers.Data.MyHousingData.houses[openNumber] = true;
        Managers.Data.SaveHousingData();
        //���½� �ٷ� ��ǳ�� ���
        BossHouse[openNumber].gameObject.GetComponentInChildren<MovingPet>().TouchPet();
    }
}
