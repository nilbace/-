using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Shooter : MonoBehaviour
{
    [SerializeField] float toShootDelay;    //첫 발사 까지
    [SerializeField] GameObject BulletPrefab;
    public float BulletSize = 1;
    [SerializeField] Vector3 bulletDir;
    [SerializeField] float bulletSpeed;
    [SerializeField] int[] bulletNumbers;
    [SerializeField] float twoBulletOffset;    //2개일때 중앙에서 얼마나 벗어날지
    [SerializeField] float threeBulletOffset;  //3개일때 중앙에서 얼마나 벗어날지
    [SerializeField] float attackTerm;     //발사 중간 중간 딜레이
    void Start()
    {
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(toShootDelay);

        for(int i = 0; i < bulletNumbers.Length; i++)
        {
            if(bulletNumbers[i] == 1)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, transform.position);
                bullet.transform.localScale = Vector3.one * BulletSize;
            }

            else if(bulletNumbers[i] == 2)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                Vector3 temp = transform.position + new Vector3(twoBulletOffset, 0f, 0f);
                bullet.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, temp);
                bullet.transform.localScale = Vector3.one * BulletSize;

                GameObject bullet2 = Instantiate(BulletPrefab);
                Vector3 temp2 = transform.position - new Vector3(twoBulletOffset, 0f, 0f);
                bullet2.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, temp2);
                bullet2.transform.localScale = Vector3.one * BulletSize;
            }

            else if (bulletNumbers[i] == 3)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                Vector3 temp = transform.position + new Vector3(threeBulletOffset, 0f, 0f);
                bullet.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, temp);
                bullet.transform.localScale = Vector3.one * BulletSize;

                GameObject bullet2 = Instantiate(BulletPrefab);
                Vector3 temp2 = transform.position;
                bullet2.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, temp2);
                bullet2.transform.localScale = Vector3.one * BulletSize;

                GameObject bullet3 = Instantiate(BulletPrefab);
                Vector3 temp3 = transform.position - new Vector3(threeBulletOffset, 0f, 0f);
                bullet3.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, temp3);
                bullet3.transform.localScale = Vector3.one * BulletSize;
            }

            yield return new WaitForSeconds(attackTerm);
        }
    }
}
