using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField] float PatternStartDelay;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject RaserDanger;
    [SerializeField] GameObject RaserPrefab;

    [Header("패턴 이름 / 패턴간의 간격(초)")]
    public Vector2[] pigPattern;
    [Header("각 패턴별 정보")]
    [SerializeField] BulletPattern[] PatternData;
    [SerializeField] float[] PatternDelay;
    

    void Start()
    {
        StartCoroutine(StartBattle());
    }


    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(PatternStartDelay);
         
        for(int i = 0; i<pigPattern.Length; i++)
        {
            switch (pigPattern[i].x)
            {
                case 0:
                    break;

                case 1:   //큰거 두개 발사
                    Pattern1();
                    break;

                case 2:
                    break;

                case 3: //아래로 5번
                    for (int j = 0; j < 5; j++)
                    {
                        Pattern3();
                        yield return new WaitForSeconds(PatternDelay[3]);
                    }
                    break;

                case 4: //왼쪽으로 5번
                    for (int j = 0; j < 5; j++)
                    {
                        Pattern4();
                        yield return new WaitForSeconds(PatternDelay[4]);
                    }
                    break;

                case 5: //오른쪽으로 5번
                    for (int j = 0; j < 5; j++)
                    {
                        Pattern5();
                        yield return new WaitForSeconds(PatternDelay[4]);
                    }
                    break;

                case 6: //3갈래로 5번
                    for (int j = 0; j < 5; j++)
                    {
                        Pattern6();
                        yield return new WaitForSeconds(PatternDelay[4]);
                    }
                    break;

                case 7:  //원형으로 발사
                    float angle1 = 0f;
                    for (int j = 0; j < 10; j++)
                    {
                        Vector3 dir1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle1), Mathf.Sin(Mathf.Deg2Rad * angle1), 0f);

                        GameObject bullet = Instantiate(BulletPrefab);
                        Vector3 temp = transform.position + new Vector3(0, -1, 0);
                        bullet.GetComponent<MonsterBullet>().Setting(dir1, 3f, temp);
                        bullet.transform.localScale = Vector3.one * 1f;



                        angle1 -= 18f;
                    }
                    break;

                case 8: //레이저 발사
                    GameObject danger = Instantiate(RaserDanger);
                    Destroy(danger, 2f);
                    yield return new WaitForSeconds(2f);
                    Instantiate(RaserPrefab);
                    break;

            }
            yield return new WaitForSeconds(pigPattern[i].y);
        }
    }
    

    #region Patterns //패턴들 모두


    void Pattern1()
    {
        ShootBullet(PatternData[0]);
        ShootBullet(PatternData[1]);
    }


    void Pattern3()
    {
        ShootBullet(PatternData[2]);
        ShootBullet(PatternData[3]);
    }

    void Pattern4()
    {
        ShootBullet(PatternData[4]);
        ShootBullet(PatternData[5]);
    }

    void Pattern5()
    {
        ShootBullet(PatternData[6]);
        ShootBullet(PatternData[7]);
    }

    void Pattern6()
    {
        ShootBullet(PatternData[4]);
        ShootBullet(PatternData[8]);
        ShootBullet(PatternData[7]);
    }

    void Pattern8()
    {
        
    }

    void ShootBullet(BulletPattern bulletPattern)
    {
        GameObject bullet = Instantiate(BulletPrefab);
        Vector3 temp = transform.position + bulletPattern.Offset;
        bullet.GetComponent<MonsterBullet>().Setting(bulletPattern.BulletDir, bulletPattern.BulletSpeed, temp);
        bullet.transform.localScale = Vector3.one * bulletPattern.BulletSize;
    }

    #endregion
}

[System.Serializable]
public struct BulletPattern
{
    public string PatternName;
    public float BulletSize;
    public float BulletSpeed;
    public Vector3 BulletDir;
    public Vector3 Offset;
}
