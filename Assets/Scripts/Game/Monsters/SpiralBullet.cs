using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBullet : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float bulletSpeed = 5f;      // źȯ �ӵ�
    [SerializeField] int numBullets;
    [SerializeField] float spiralRadius = 1f;     // ���� ������
    [SerializeField] float spiralSpacing = 0.2f;  // ���� ����

    private float anglePerBullet;       // �� źȯ�� ����

    private void Start()
    {
        anglePerBullet = 360f / numBullets;  // źȯ ���� ���
        StartCoroutine(ShootSpiralBullets());
    }

    private IEnumerator ShootSpiralBullets()
    {
        for (int i = 0; i < numBullets; i++)
        {
            float angle = i * anglePerBullet;                     // źȯ�� ȸ�� ����
            float spiralOffset = i * spiralSpacing;               // �������� ��ġ ������

            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * spiralRadius * spiralOffset;  // x ��ǥ ���
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * spiralRadius * spiralOffset;  // y ��ǥ ���

            Vector2 direction = new Vector2(x, y).normalized;      // �߻� ���� ���
            GameObject bullet = InstantiateBullet();               // �Ѿ� ����
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;  // �߻�

            yield return new WaitForSeconds(0.1f);  // źȯ ������ ������
        }
    }

    private GameObject InstantiateBullet()
    {
        // �Ѿ��� �����ϰ� �ʱ� ������ ������ �� ��ȯ
        GameObject bullet = new GameObject("Bullet");
        bullet.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BulletSprite");
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0f;
        // �Ѿ��� ��Ÿ ���� (ũ��, �浹 ó�� ��) �߰� ����
        return bullet;
    }

    
}
