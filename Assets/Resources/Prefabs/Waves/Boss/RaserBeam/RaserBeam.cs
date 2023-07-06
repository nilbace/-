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
            // 이미 겹치는 Collider를 검사하여 처리
            Collider2D[] overlappedColliders = Physics2D.OverlapAreaAll(boxCollider2D.bounds.min, boxCollider2D.bounds.max);
            foreach (Collider2D collider in overlappedColliders)
            {
                // 특정 함수를 실행하는 로직을 작성하세요.
                // 겹치는 Collider에 대한 추가적인 동작을 수행할 수 있습니다.
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

        int previousNumber = -1; // 이전에 생성된 숫자를 저장할 변수

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
