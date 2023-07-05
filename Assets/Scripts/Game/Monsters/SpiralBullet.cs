using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBullet : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float bulletSpeed = 5f;      // 탄환 속도
    [SerializeField] int numBullets;
    [SerializeField] float spiralRadius = 1f;     // 나선 반지름
    [SerializeField] float spiralSpacing = 0.2f;  // 나선 간격

    private float anglePerBullet;       // 각 탄환의 각도

    private void Start()
    {
        anglePerBullet = 360f / numBullets;  // 탄환 각도 계산
        StartCoroutine(ShootSpiralBullets());
    }

    private IEnumerator ShootSpiralBullets()
    {
        for (int i = 0; i < numBullets; i++)
        {
            float angle = i * anglePerBullet;                     // 탄환의 회전 각도
            float spiralOffset = i * spiralSpacing;               // 나선상의 위치 오프셋

            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * spiralRadius * spiralOffset;  // x 좌표 계산
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * spiralRadius * spiralOffset;  // y 좌표 계산

            Vector2 direction = new Vector2(x, y).normalized;      // 발사 방향 계산
            GameObject bullet = InstantiateBullet();               // 총알 생성
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;  // 발사

            yield return new WaitForSeconds(0.1f);  // 탄환 사이의 딜레이
        }
    }

    private GameObject InstantiateBullet()
    {
        // 총알을 생성하고 초기 설정을 진행한 후 반환
        GameObject bullet = new GameObject("Bullet");
        bullet.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BulletSprite");
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0f;
        // 총알의 기타 설정 (크기, 충돌 처리 등) 추가 가능
        return bullet;
    }

    
}
