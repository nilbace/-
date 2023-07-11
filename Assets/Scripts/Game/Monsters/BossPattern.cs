using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossPattern : MonoBehaviour
{
    [SerializeField] float PatternStartDelay;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject LongBulletPrefab;
    [SerializeField] GameObject RaserDanger;
    [SerializeField] GameObject RaserPrefab;
    Transform _PlayerTrans;

    [Header("패턴 이름 / 패턴간의 간격(초)")]
    public Vector2[] pigPattern;
    [Header("각 패턴별 정보")]
    [SerializeField] BulletPattern[] PatternData;
    [SerializeField] float[] PatternDelay;
    

    void Start()
    {
        StartCoroutine(StartBattle());
        _PlayerTrans = GameObject.Find("Player").transform;
    }


    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(PatternStartDelay);
         
        while(true)
        {
            for (int i = 0; i < pigPattern.Length; i++)
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

                    #region Dog

                    case 12:
                        float offset12 = 0.8f;
                        float xValue = 2.5f;
                        ShootBullet(PatternData[9], new Vector3(-xValue + offset12*0.5f, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(-xValue + offset12 * 1.5f, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(xValue - offset12 * 0.5f, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(xValue - offset12 * 1.5f, 0f, 0f));

                        yield return new WaitForSeconds(0.3f);

                        ShootBullet(PatternData[9], new Vector3(-xValue, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(-xValue + offset12, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(-xValue + offset12*2, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(xValue, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(xValue - offset12, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(xValue - offset12 * 2, 0f, 0f));

                        yield return new WaitForSeconds(0.3f);

                        ShootBullet(PatternData[9], new Vector3(-xValue + offset12 * 0.5f, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(-xValue + offset12 * 1.5f, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(xValue - offset12 * 0.5f, 0f, 0f));
                        ShootBullet(PatternData[9], new Vector3(xValue - offset12 * 1.5f, 0f, 0f));

                        break;
                    #endregion

                    #region chick
                    case 13:  //긴 총알 좌우에서 발사
                        ShootLongBullet(PatternData[9], new Vector3(-2f,0f,0f));
                        ShootLongBullet(PatternData[9], new Vector3(2f, 0f, 0f));
                        
                        break;
                    

                    case 14:   //1번 패턴
                        ShootBullet(PatternData[0], new Vector3(-1f, -0f, 0f));
                        ShootBullet(PatternData[1], new Vector3(1f, -0f, 0f));
                        yield return new WaitForSeconds(1f);
                        ShootBullet(PatternData[0], new Vector3(0.5f, -0f, 0f));
                        ShootBullet(PatternData[1], new Vector3(-0.5f, -0f, 0f));

                        break;


                    case 15:    //3번 패턴
                        transform.DOMoveX(-1f, 1f);
                        yield return new WaitForSeconds(1f);
                        //왼쪽으로 이동

                        for (int j = 0; j < 5; j++)
                        {
                            Pattern6();
                            yield return new WaitForSeconds(PatternDelay[4]);
                        }
                        yield return new WaitForSeconds(2f);
                        //발사 + 대기까지 2초

                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1f);
                        //돌아오기

                        break;

                    case 16:    //4번 패턴
                        transform.DOMoveX(1f, 1f);
                        yield return new WaitForSeconds(1f);
                        //왼쪽으로 이동

                        for (int j = 0; j < 5; j++)
                        {
                            Pattern6();
                            yield return new WaitForSeconds(PatternDelay[4]);
                        }
                        yield return new WaitForSeconds(2f);
                        //발사 + 대기까지 2초

                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1f);
                        //돌아오기

                        break;
                    #endregion

                    #region Monkey
                    case 17:  //X자 공격
                        Vector3 offset = new Vector3(1.4f, 0, 0);

                        for(int j = 0; j < 5; j++)
                        {
                            ShootBullet(PatternData[10], (-2f +j)*offset);
                            ShootBullet(PatternData[10], (2f - j) * offset);
                            yield return new WaitForSeconds(0.5f);
                        }
                   break;

                    case 18: //왼쪽부터 이동하면서 공격
                        for(int k = -1; k <=1; k++)
                        {
                            float poz = k;
                            transform.DOMoveX(poz, 1f);
                            yield return new WaitForSeconds(1f);

                            for (int j = -1; j <= 1; j++)
                            {
                                float offset2;
                                if (j == -1 || j == 1) offset2 = 0.5f;
                                else offset2 = 1f;

                                ShootBullet(PatternData[10], new Vector3(offset2, 0, 0));
                                ShootBullet(PatternData[10], new Vector3(-offset2, 0, 0));
                                yield return new WaitForSeconds(0.5f);
                            }
                        }
                        transform.DOMoveX(0, 1f);
                        yield return new WaitForSeconds(1f);
                        break;

                    case 19: //왼쪽부터 이동하면서 공격
                        for(int j =0; j<3;j++)
                        {
                            ShootLongBullet(PatternData[9], new Vector3(-2f, 0f, 0f));
                            ShootLongBullet(PatternData[9], new Vector3(2f, 0f, 0f));
                            yield return new WaitForSeconds(1f);
                        }
                        break;

                    case 20:
                        transform.DOMoveX(-1.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        transform.DOMoveX(1.5f, 1f);
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(PatternData[10]);
                            ShootBullet(PatternData[10], new Vector3(0,-1f,0));
                            yield return new WaitForSeconds(0.2f);
                        }
                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 21:
                        transform.DOMoveX(1.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        transform.DOMoveX(-1.5f, 1f);
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(PatternData[10]);
                            ShootBullet(PatternData[10], new Vector3(0, -1f, 0));
                            yield return new WaitForSeconds(0.2f);
                        }
                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;
                    

                    case 22:  //원형으로 발사
                        transform.DOMoveY(0.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        float angle2 = 18f;
                        for (int j = 0; j < 10; j++)
                        {
                            Vector3 dir1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle2), Mathf.Sin(Mathf.Deg2Rad * angle2), 0f);

                            GameObject bullet = Instantiate(BulletPrefab);
                            Vector3 temp = transform.position + new Vector3(0, -1, 0);
                            bullet.GetComponent<MonsterBullet>().Setting(dir1, 3f, temp);
                            bullet.transform.localScale = Vector3.one * 1f;



                            angle2 -= 36f;
                        }
                        yield return new WaitForSeconds(0.5f);

                        transform.DOMoveY(3.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 23:

                        Vector3[] _pozs = new Vector3[]
                        {
                            new Vector3(-1, 0, 0),
                            new Vector3(0, 0.5f, 0),
                            new Vector3(1, 0, 0)
                        };

                        for (int j =0; j <3; j++)
                        {
                            GameObject mon2 = Instantiate(gameObject, transform.position, Quaternion.identity);
                            Vector3 temppoz = _pozs[j];
                            Color tempColor = mon2.GetComponent<SpriteRenderer>().color;
                            tempColor.r = tempColor.g = tempColor.b = 0.4f;
                            mon2.GetComponent<SpriteRenderer>().color = tempColor;
                            mon2.transform.DOMove(temppoz, 1f);
                            Destroy(mon2, 2f);
                            yield return new WaitForSeconds(1f);
                            mon2.GetComponent<BossPattern>().MonkeyClone();
                        }
                        break;

                        #endregion
                }
                yield return new WaitForSeconds(pigPattern[i].y);
            }
        }
    }

    public void MonkeyClone()
    {
        StopAllCoroutines();
        StartCoroutine(MonkeyCloneSkill());
    }

    IEnumerator MonkeyCloneSkill()
    {
        float angle2 = 18f;
        for (int j = 0; j < 10; j++)
        {
            Vector3 dir1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle2), Mathf.Sin(Mathf.Deg2Rad * angle2), 0f);

            GameObject bullet = Instantiate(BulletPrefab);
            Vector3 temp = transform.position + new Vector3(0, -1, 0);
            bullet.GetComponent<MonsterBullet>().Setting(dir1, 3f, temp);
            bullet.transform.localScale = Vector3.one * 1f;

            angle2 -= 36f;
        }
        yield return new WaitForSeconds(0.5f);
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

    

    void ShootBullet(BulletPattern bulletPattern, Vector3? poz = null)
    {
        GameObject bullet = Instantiate(BulletPrefab);
        Vector3 temp = transform.position + bulletPattern.Offset + (poz ?? Vector3.zero);
        bullet.GetComponent<MonsterBullet>().Setting(bulletPattern.BulletDir, bulletPattern.BulletSpeed, temp);
        bullet.transform.localScale = Vector3.one * bulletPattern.BulletSize;
    }

    void ShootLongBullet(BulletPattern bulletPattern, Vector3? poz = null)
    {
        GameObject bullet = Instantiate(LongBulletPrefab);
        Vector3 temp = transform.position + bulletPattern.Offset + (poz ?? Vector3.zero);
        bullet.transform.position = temp;

        Vector3 dir = _PlayerTrans.position - bullet.transform.position;
        print(bullet.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //총알이 플레이어쪽으로 회전

        bullet.GetComponent<MonsterBullet>().Setting(dir, bulletPattern.BulletSpeed, temp);
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
