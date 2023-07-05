using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField] float PatternStartDelay;
    [SerializeField] GameObject BulletPrefab;
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

                case 1:
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
