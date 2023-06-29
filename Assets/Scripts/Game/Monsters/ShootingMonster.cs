using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMonster : EnemyBase
{
    [SerializeField] float toShootDelay;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Vector3 bulletDir;
    [SerializeField] float bulletSpeed;
    void Start()
    {
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(toShootDelay);
        GameObject bullet = Instantiate(BulletPrefab);
        bullet.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, transform.position);
    }
}
