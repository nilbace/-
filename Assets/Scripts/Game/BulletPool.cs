using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int initialPoolSize = 10;
    public int additionalPoolSize = 5;

    private List<GameObject> bulletPool;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        bulletPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateBullet();
        }
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform);
        bullet.SetActive(false);
        bulletPool.Add(bullet);
        return bullet;
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        // 추가로 요청한 총알 생성
        for (int i = 0; i < additionalPoolSize; i++)
        {
            GameObject bullet = CreateBullet();
            bulletPool.Add(bullet);
            bullet.SetActive(false);
        }

        // 마지막에 생성된 총알 반환
        GameObject lastBullet = bulletPool[bulletPool.Count - 1];
        return lastBullet;
    }

   

    public void ReturnBullet(GameObject bullet)
    {
        if (bullet.activeInHierarchy)
            bullet.SetActive(false);
    }
}
