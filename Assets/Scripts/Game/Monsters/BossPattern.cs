using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField] float PatternStartDelay;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject RaserPrefab;

    [Header("���� �̸� / ���ϰ��� ����(��)")]
    public Vector2[] pigPattern;
    [Header("�� ���Ϻ� ����")]
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

                case 3: //�Ʒ��� 5��
                    for (int j = 0; j < 5; j++)
                    {
                        Pattern3();
                        yield return new WaitForSeconds(PatternDelay[3]);
                    }
                    break;

                case 4: //�������� 5��
                    for (int j = 0; j < 5; j++)
                    {
                        Pattern4();
                        yield return new WaitForSeconds(PatternDelay[4]);
                    }
                    break;

                case 5: //���������� 5��
                    for (int j = 0; j < 5; j++)
                    {
                        Pattern5();
                        yield return new WaitForSeconds(PatternDelay[4]);
                    }
                    break;

                case 6: //3������ 5��
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
    

    #region Patterns //���ϵ� ���


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
