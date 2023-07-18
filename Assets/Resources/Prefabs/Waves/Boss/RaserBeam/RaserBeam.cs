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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameScene.instance.PlayerGetDamage(10);
        }
    }

}
