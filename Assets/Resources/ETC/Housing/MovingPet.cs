using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPet : MonoBehaviour
{
    [SerializeField] GameObject talkBubble;
    [SerializeField] string[] talk;
    [SerializeField] GameObject rangeObject;
    BoxCollider rangeCollider;
    Vector3 newTarget;

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
        //�ڱ� ��ǳ�� �ѱ�
        talkBubble.GetComponentInChildren<TextMesh>().text = talk[Random.Range(0, talk.Length)];
        talkBubble.SetActive(true);
        StartCoroutine(CloseBubble());
        //��ǳ�� �Ȼ����� ��帻ǳ�� �߰� ������ ����â
    }

    private IEnumerator CloseBubble()
    {
        yield return new WaitForSeconds(2f);
        talkBubble.SetActive(false);

    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 targetPosition = originPosition + RandomPostion;
        return targetPosition;
    }
}
