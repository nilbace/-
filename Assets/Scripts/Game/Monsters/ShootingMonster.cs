using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMonster : EnemyBase
{
    [SerializeField] float toShootDelay;
    [SerializeField] GameObject BulletPrefab;
    public float BulletSize = 1;
    [SerializeField] Vector3 bulletDir;
    [SerializeField] float bulletSpeed;
    [SerializeField] bool isTwoWay;
    [SerializeField] Vector3 twoOffset;
    [SerializeField] int attackNumber;
    [SerializeField] float attackTerm;
    void Start()
    {
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(toShootDelay);

        if (!isTwoWay)
        {
            for(int i =0; i < attackNumber; i++)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, transform.position);
                bullet.transform.localScale = Vector3.one * BulletSize;
                yield return new WaitForSeconds(attackTerm);
            }
        }
        else
        {
            for (int i = 0; i < attackNumber; i++)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                Vector3 temp = transform.position + twoOffset;
                bullet.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, temp);
                bullet.transform.localScale = Vector3.one * BulletSize;

                GameObject bullet2 = Instantiate(BulletPrefab);
                Vector3 temp2 = transform.position + new Vector3(twoOffset.x * (-1f), twoOffset.y, 0f);
                bullet2.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, temp2);
                bullet2.transform.localScale = Vector3.one * BulletSize;
                yield return new WaitForSeconds(attackTerm);
            }
            
        }
    }
}
