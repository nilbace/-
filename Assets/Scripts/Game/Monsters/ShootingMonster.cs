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

    [Header("³×ÀÓµå")]
    [SerializeField] bool is_N_1_Left;
    [SerializeField] bool is_N_1_Right;
    float plusangle = 0.2f;
    [SerializeField] bool is_N_2;
    [SerializeField] bool is_N_3;
    [SerializeField] float N_3_xValue;
    void Start()
    {
        base.Start();
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(toShootDelay);


        float angle1 = 0f; float angle2 = 180f;
        if (is_N_2)
        {
            for(int i = 0; i< 10; i++)
            {
                Vector3 dir1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle1), Mathf.Sin(Mathf.Deg2Rad * angle1), 0f);
                Vector3 dir2 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle2), Mathf.Sin(Mathf.Deg2Rad * angle2), 0f);

                GameObject bullet = Instantiate(BulletPrefab);
                Vector3 temp = transform.position + twoOffset;
                bullet.GetComponent<MonsterBullet>().Setting(dir1, bulletSpeed, temp);
                bullet.transform.localScale = Vector3.one * BulletSize;

                GameObject bullet2 = Instantiate(BulletPrefab);
                Vector3 temp2 = transform.position + new Vector3(twoOffset.x * (-1f), twoOffset.y, 0f);
                bullet2.GetComponent<MonsterBullet>().Setting(dir2, bulletSpeed, temp2);
                bullet2.transform.localScale = Vector3.one * BulletSize;

                angle1 += 18f; angle2 += 18f;
                yield return new WaitForSeconds(attackTerm);
            }
        }

        else if(is_N_3)
        {
            for(int i = 0; i <8; i++)
            {
                for(int j = -1 ; j<2; j++)
                {
                    GameObject bullet = Instantiate(BulletPrefab);
                    Vector3 temp = transform.position + twoOffset;
                    Vector3 tempvalue = new Vector3(j * N_3_xValue, -1, 0).normalized;
                    bullet.GetComponent<MonsterBullet>().Setting(tempvalue, bulletSpeed, temp);
                    bullet.transform.localScale = Vector3.one * BulletSize;
                }
                yield return new WaitForSeconds(attackTerm);
                if (i == 3) yield return new WaitForSeconds(attackTerm * 2);
            }
        }

        else
        {
            if (!isTwoWay)
            {
                for (int i = 0; i < attackNumber; i++)
                {
                    GameObject bullet = Instantiate(BulletPrefab);
                    bullet.GetComponent<MonsterBullet>().Setting(bulletDir, bulletSpeed, transform.position);
                    bullet.transform.localScale = Vector3.one * BulletSize;
                    yield return new WaitForSeconds(attackTerm);
                    if (is_N_1_Left) bulletDir.x += plusangle;
                    else if (is_N_1_Right) bulletDir.x -= plusangle;
                    plusangle *= 0.6f;
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

    public void Changedir(Vector3 newdir, float _newSpeed)
    {
        
    }
}
