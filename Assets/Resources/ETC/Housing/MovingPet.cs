using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPet : MonoBehaviour
{
    //대사 관련 변수
    [SerializeField] int petIndex;
    [SerializeField] GameObject talkBubble;
    [SerializeField] TMPro.TextMeshPro talkText;
    [SerializeField] GameObject unlock;
    [SerializeField] GameObject unlockEffect;
    [SerializeField] string[] talk;

    //이동 관련 변수
    [SerializeField] GameObject rangeObject;
    BoxCollider rangeCollider;
    Vector3 newTarget;
    Coroutine runningCoroutine = null;

    // Start is called before the first frame update
    void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        newTarget = Return_RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, newTarget, 0.001f);
        if(transform.position == newTarget)
        {
            newTarget = Return_RandomPosition();
        }
    }

    public void TouchPet()
    {
        if (Managers.Data.MyHousingData.talkBox[petIndex])
        {
            //자기 대사 켜기
            talkText.text = talk[Random.Range(0, talk.Length)];
            talkText.gameObject.SetActive(true);
            unlock.SetActive(false);
        }
        else
        {
            //해금표시 켜기
            talkText.gameObject.SetActive(false);
            unlock.SetActive(true);
        }
        talkBubble.SetActive(true);

        //코루틴 중복방지
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(CloseBubble());
    }

    private IEnumerator CloseBubble()
    {
        yield return new WaitForSeconds(2f);
        talkBubble.SetActive(false);

    }

    public void UnlockTalk()
    {
        if (Managers.Data.MyHousingData.talkBox[petIndex] == false)//중복해금방지
        {
            if (Managers.Data.MyStoreData.MyGoldAmount >= 500)
            {
                Managers.Data.MyHousingData.talkBox[petIndex] = true;
                Managers.Data.MyStoreData.MyGoldAmount -= 500;
                Managers.Data.SaveAllDatas();
                GameObject copyunlockEffect = Instantiate(unlockEffect, talkText.gameObject.transform.position, talkText.gameObject.transform.rotation);
                copyunlockEffect.SetActive(true);
                TouchPet();
            }
            else
            {
                talkText.text = "골드가 부족해 ㅠㅅㅠ";
                talkText.gameObject.SetActive(true);
                unlock.SetActive(false);
                //코루틴 중복방지
                if (runningCoroutine != null)
                {
                    StopCoroutine(runningCoroutine);
                }
                runningCoroutine = StartCoroutine(CloseBubble());
            }
        }
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);

        Vector3 targetPosition = originPosition + RandomPostion;
        //Debug.Log(gameObject.name + " targetPosition : " + targetPosition);
        return targetPosition;
    }
}
