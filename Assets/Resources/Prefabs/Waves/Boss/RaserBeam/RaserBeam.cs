using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaserBeam : MonoBehaviour
{
    [SerializeField] Sprite[] beamImgs;
    [SerializeField] float AniSpeed;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(FireRaser());
    }

    void Update()
    {
        if (boxCollider2D.enabled == true)
        {
            // �̹� ��ġ�� Collider�� �˻��Ͽ� ó��
            Collider2D[] overlappedColliders = Physics2D.OverlapAreaAll(boxCollider2D.bounds.min, boxCollider2D.bounds.max);
            foreach (Collider2D collider in overlappedColliders)
            {
                // Ư�� �Լ��� �����ϴ� ������ �ۼ��ϼ���.
                // ��ġ�� Collider�� ���� �߰����� ������ ������ �� �ֽ��ϴ�.
                SpecialFunction(collider.gameObject);
            }
        }
    }

    IEnumerator FireRaser()
    {
        boxCollider2D.enabled = false;

        spriteRenderer.sprite = null;
        yield return new WaitForSeconds(1f);
        for(int i = 0; i<4; i++)
        {
            spriteRenderer.sprite = beamImgs[i];
            yield return new WaitForSeconds(AniSpeed);
        }

        int previousNumber = -1; // ������ ������ ���ڸ� ������ ����

        boxCollider2D.enabled = true;
        for (int i = 0; i < 20; i++)
        {
            int randomNumber;

            do
            {
                randomNumber = new System.Random().Next(4, 8);
            } while (randomNumber == previousNumber);

            previousNumber = randomNumber;
            spriteRenderer.sprite = beamImgs[randomNumber];
            yield return new WaitForSeconds(AniSpeed);
        }

        Destroy(gameObject);
    }

    

    private void SpecialFunction(GameObject otherObject)
    {
        if (otherObject.tag == "Player")
        {
            otherObject.GetComponent<Player>().PlayerHP -= 100;
        }
    }
}
