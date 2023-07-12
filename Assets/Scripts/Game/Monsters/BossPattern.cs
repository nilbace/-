using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BossPattern : MonoBehaviour
{
    float PatternStartDelay = 2f;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject LongBulletPrefab;
    [SerializeField] GameObject RaserDanger;
    [SerializeField] GameObject RaserPrefab;
    [SerializeField] GameObject SplitBall;
    Transform _PlayerTrans;

    [Header("패턴 이름 / 패턴간의 간격(초)")]
    public Vector2[] BossPatternData;
    

    void Start()
    {
        StartCoroutine(StartBattle());
        _PlayerTrans = GameObject.Find("Player").transform;
    }

    #region CommonPatterns
    IEnumerator ShootHalfCircle()
    {
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
        yield return null;
    }

    IEnumerator ShootCircle()
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

    IEnumerator Shoot3Way()
    {
        for (int j = 0; j < 5; j++)
        {
            ShootBullet(_baseLeft, new Vector3(-0.5f, 0, 0));
            ShootBullet(_basePattern);
            ShootBullet(_baseRight, new Vector3(0.5f, 0f, 0));
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Shoot2_3_2(float xValue = 0)
    {
        float offset12 = 0.8f;

        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 0.5f, 0f, 0f), 0.5f);
        ShootBullet(_basePattern, new Vector3(xValue + offset12 * 0.5f, 0f, 0f), 0.5f);

        yield return new WaitForSeconds(0.3f);

        ShootBullet(_basePattern, new Vector3(xValue - offset12, 0f, 0f), 0.5f);
        ShootBullet(_basePattern, new Vector3(xValue , 0f, 0f), 0.5f);
        ShootBullet(_basePattern, new Vector3(xValue + offset12 , 0f, 0f), 0.5f);

        yield return new WaitForSeconds(0.3f);

        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 0.5f, 0f, 0f), 0.5f);
        ShootBullet(_basePattern, new Vector3(xValue + offset12 * 0.5f, 0f, 0f), 0.5f);

        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator ShootSpiral()
    {
        float angle1 = 0f; float angle2 = 180f;
            for (int i = 0; i < 10; i++)
            {
                Vector3 dir1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle1), Mathf.Sin(Mathf.Deg2Rad * angle1), 0f);
                Vector3 dir2 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle2), Mathf.Sin(Mathf.Deg2Rad * angle2), 0f);

                GameObject bullet = Instantiate(BulletPrefab);
                Vector3 temp = transform.position;
                bullet.GetComponent<MonsterBullet>().Setting(dir1, 3f, temp);
                bullet.transform.localScale = Vector3.one * 0.7f;

                GameObject bullet2 = Instantiate(BulletPrefab);
                Vector3 temp2 = transform.position;
                bullet2.GetComponent<MonsterBullet>().Setting(dir2, 3f, temp2);
                bullet2.transform.localScale = Vector3.one * 0.7f;

                angle1 += 18f; angle2 += 18f;
                yield return new WaitForSeconds(0.1f);
            }
    }

    IEnumerator FireLaser(bool toPlayer = false)
    {
        GameObject danger2 = Instantiate(RaserDanger);
        danger2.transform.position +=  transform.position + new Vector3(0,-1f,0);
        Vector3 playerDirection = Vector3.down;

        if (toPlayer)
        {
            playerDirection = gameObject.transform.position - _PlayerTrans.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.back, playerDirection);
            danger2.transform.rotation = rotation;
        }

        Destroy(danger2, 1.8f);
        yield return new WaitForSeconds(2f);

        GameObject Raser = Instantiate(RaserPrefab);
        Raser.transform.position +=  transform.position + new Vector3(0, -1f, 0);

        if (toPlayer)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, playerDirection);
            Raser.transform.rotation = rotation;
        }

        yield return new WaitForSeconds(1.6f);
    }


    #endregion


    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(PatternStartDelay);
         
        while(true)
        {
            for (int i = 0; i < BossPatternData.Length; i++)
            {
                switch (BossPatternData[i].x)
                {
                    case 0: 

                        break;
                    #region Pig
                    case 1: //큰거 두발 발사
                        ShootBullet(_basePattern, Vector3.right *2, 1.5f);
                        ShootBullet(_basePattern, Vector3.left  *2, 1.5f);

                        break;

                    case 2: // 작게 5발 연속 발사
                        for(int j = 0; j <5; j++)
                        {
                            ShootBullet(_basePattern, Vector3.right);
                            ShootBullet(_basePattern, Vector3.left );
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;

                    case 3: //왼쪽으로 5번
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseLeft, new Vector3(-0.5f, 0,0) );
                            ShootBullet(_baseLeft, new Vector3(1.1f, -0.7f, 0));
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;

                    case 4: //왼쪽으로 5번
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseRight, new Vector3(-1.1f, -0.7f, 0));
                            ShootBullet(_baseRight, new Vector3(0.5f, 0f, 0));
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;

                    case 5: //큰거 4개
                        ShootBullet(_basePattern, new Vector3(-2.2f,0,0),   1.2f);
                        ShootBullet(_basePattern, new Vector3(-1.2f, 0, 0), 1.2f);
                        ShootBullet(_basePattern, new Vector3(1.2f, 0, 0),  1.2f);
                        ShootBullet(_basePattern, new Vector3(2.2f, 0, 0),  1.2f);
                        break;

                    case 6: //원형으로 발사
                        yield return StartCoroutine(ShootHalfCircle());
                        break;

                    case 7:  //3갈래로 발사
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseLeft, new Vector3(-0.5f, 0, 0));
                            ShootBullet(_basePattern );
                            ShootBullet(_baseRight, new Vector3(0.5f, 0f, 0));
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;

                    case 8: //레이저 발사
                        yield return StartCoroutine(FireLaser());
                        break;

                    #endregion

                    #region Dog

                    

                    case 10: //양쪽으로 2_3_2 발사
                        float offset12 = 0.8f;
                        float xValue = 2.5f;
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12*0.5f, 0f, 0f) ,0.5f);
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12 * 1.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 0.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 1.5f, 0f, 0f), 0.5f);

                        yield return new WaitForSeconds(0.3f);

                        ShootBullet(_basePattern, new Vector3(-xValue, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12*2, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 2, 0f, 0f), 0.5f);

                        yield return new WaitForSeconds(0.3f);

                        ShootBullet(_basePattern, new Vector3(-xValue + offset12 * 0.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12 * 1.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 0.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 1.5f, 0f, 0f), 0.5f);

                        yield return new WaitForSeconds(1f);
                        break;

                    case 11:  //양쪽 큰거, 가운데 작은거 두개
                        for(int j =0; j<2;j++)
                        {
                            ShootBullet(_basePattern, Vector3.right * 2.3f, 1.5f);
                            ShootBullet(_basePattern, Vector3.left * 2.3f, 1.5f);
                            ShootBullet(_basePattern, Vector3.right * 0.2f, 0.5f);
                            ShootBullet(_basePattern, Vector3.left * 0.2f, 0.5f);
                            yield return new WaitForSeconds(1f);
                        }
                        break;

                    #endregion

                    #region chick
                    case 13:  //긴 총알 좌우에서 발사
                        ShootLongBullet(_basePattern, new Vector3(-2f,0f,0f));
                        ShootLongBullet(_basePattern, new Vector3(2f, 0f, 0f));
                        break;
                    

                    case 14:   //좌우 한발, 가운데 한발
                        ShootBullet(_basePattern, new Vector3(-2.2f, -0f, 0f) ,1.5f);
                        ShootBullet(_basePattern, new Vector3(2.2f, -0f, 0f), 1.5f);
                        yield return new WaitForSeconds(1f);
                        ShootBullet(_basePattern, new Vector3(0.5f, -0f, 0f), 1.5f);
                        ShootBullet(_basePattern, new Vector3(-0.5f, -0f, 0f), 1.5f);

                        break;


                    case 15:    //왼쪽에서 3갈래 쏘고 복귀
                        transform.DOMoveX(-1f, 1f);
                        yield return new WaitForSeconds(1f);
                        //왼쪽으로 이동
                        yield return StartCoroutine(Shoot3Way());
                      
                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1f);
                        //돌아오기
                        break;

                    case 16:    //오른쪽에서 3갈래 쏘고 복귀
                        transform.DOMoveX(1f, 1f);
                        yield return new WaitForSeconds(1f);
                        //왼쪽으로 이동
                        yield return StartCoroutine(Shoot3Way());

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
                            ShootBullet(_basePattern, (-2f +j)*offset ,0.5f);
                            ShootBullet(_basePattern, (2f - j) * offset, 0.5f);
                            yield return new WaitForSeconds(0.5f);
                        }
                   break;

                    case 9:  //2_3_2로 대체
                        yield return StartCoroutine(Shoot2_3_2());
                        break;

                    //다음 공격은 13번 3번 연속으로 대체

                    case 18: //왼쪽부터 이동하면서 3번 공격
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

                                ShootBullet(_basePattern, new Vector3(offset2, 0, 0), 0.5f);
                                ShootBullet(_basePattern, new Vector3(-offset2, 0, 0), 0.5f);
                                yield return new WaitForSeconds(0.5f);
                            }
                        }
                        transform.DOMoveX(0, 1f);
                        yield return new WaitForSeconds(1f);
                        break;

                    case 20:  //왼쪽에서 오른쪽으로 이동하면서 5발
                        transform.DOMoveX(-1.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        transform.DOMoveX(1.5f, 1f);
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern , null, 0.5f);
                            ShootBullet(_basePattern, new Vector3(0,-1f,0), 0.5f);
                            yield return new WaitForSeconds(0.2f);
                        }
                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 21:  //오른쪽에서 왼쪽으로 이동하면서 5발
                        transform.DOMoveX(1.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        transform.DOMoveX(-1.5f, 1f);
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern , null, 0.5f);
                            ShootBullet(_basePattern, new Vector3(0, -1f, 0), 0.5f);
                            yield return new WaitForSeconds(0.2f);
                        }
                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;
                    

                    case 22:  //내려와서 원형으로 발사
                        transform.DOMoveY(0.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(ShootCircle());

                        transform.DOMoveY(3.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 23:   //분신술

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

                    #region Sheep

                    case 24:  //긴 탄환 3번, 2번
                        ShootLongBullet(_basePattern, new Vector3(-2, -2, 0));
                        ShootLongBullet(_basePattern, new Vector3(0, -2, 0));
                        ShootLongBullet(_basePattern, new Vector3(2, -2, 0));
                        yield return new WaitForSeconds(2f);
                        ShootLongBullet(_basePattern, new Vector3(1, 0, 0));
                        ShootLongBullet(_basePattern, new Vector3(-1, 0, 0));

                        break;

                    //왼쪽에서 3갈래
                    //15번 닭과 공유

                    case 25:
                        transform.DOMoveX(-1f, 1f);
                        yield return new WaitForSeconds(1f);
                        //왼쪽으로 이동
                        yield return StartCoroutine(ShootCircle());

                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1f);
                        //돌아오기
                        break;

                    //22번 원숭이와 공유

                    case 26: 
                        //4번 웨이브 소환
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(4);
                        break;

                    case 27: // 내려와서 나선 탄막
                        transform.DOMoveY(0.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(ShootSpiral());

                        transform.DOMoveY(3.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 28: //몬스터1 + 반원 탄막
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        yield return StartCoroutine(ShootHalfCircle());
                        break;

                    case 29: // 오른쪽으로 가면서 탄환 발사
                        for(int j = 0; j<5;j++)
                        {
                            ShootBullet(_basePattern, null, 0.5f);
                            yield return new WaitForSeconds(0.2f);
                        }

                        transform.DOMoveX(1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, null, 0.5f);
                            yield return new WaitForSeconds(0.2f);
                        }

                        transform.DOMoveX(2f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, null, 0.5f);
                            yield return new WaitForSeconds(0.2f);
                        }

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 30:  //레이저 0.5초 간격으로 두번 발사
                        StartCoroutine(FireLaser(true));
                        yield return new WaitForSeconds(1f);
                        yield return StartCoroutine(FireLaser(true));
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        
                        break;

                    #endregion


                    #region horse

                    case 31: //왼쪽 > 가운데 > 오른쪽 쪼개지는총알
                        transform.DOMoveX(-1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        ShootSplitBall(_basePattern);
                        yield return new WaitForSeconds(0.5f);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        ShootSplitBall(_basePattern);
                        yield return new WaitForSeconds(0.5f);

                        transform.DOMoveX(1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        ShootSplitBall(_basePattern);
                        yield return new WaitForSeconds(0.5f);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 32: // 오른쪽에서 올라오면서 3갈래 쏘기
                        
                        transform.DOMoveX(1f, 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        transform.DOMoveY(0.5f, 0.3f);
                        yield return new WaitForSeconds(0.3f);
                        StartCoroutine(Shoot3Way());
                        transform.DOMove(new Vector3(0, 3.5f, 0), 2f);
                        yield return new WaitForSeconds(2f);
                        

                        break;

                    case 33:  //원형으로 쏘기 + 잡몹소환
                        for(int j =0; j<2; j++)
                        {
                            StartCoroutine(ShootCircle());
                            Stage1MonsterSpawner.instance.SpawnMonsterWave(4);
                            Stage1MonsterSpawner.instance.SpawnMonsterWave(12);
                        }

                        break;

                    case 34: //왼쪽가서 분해, 통나무 두개

                        transform.DOMoveX(-1.7f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        ShootSplitBall(_basePattern);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        break;


                    case 35: //왼쪽에서 올라오면서
                        
                        transform.DOMoveX(-1f, 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        transform.DOMoveY(0.5f, 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        transform.DOMove(new Vector3(0, 3.5f, 0), 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        for(int j =0; j<5; j++)
                        {
                            ShootBullet(_basePattern, new Vector3(-1, 0, 0));
                            ShootBullet(_basePattern, new Vector3(1, 0, 0));
                            yield return new WaitForSeconds(0.4f);
                        }
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);

                        break;

                    case 36:  //2_3_2 두번
                        yield return StartCoroutine(Shoot2_3_2());
                        yield return new WaitForSeconds(0.4f);
                        yield return StartCoroutine(Shoot2_3_2());
                        break;

                    //마지막은 27번

                    #endregion


                    #region snake

                    //2번으로 시작

                    case 37:  //X 자로 쏘기
                        BulletPattern Left2_37 = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(-2f, -3f, 0));
                        BulletPattern Right2_37 = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(2f, -3f, 0));

                        for(int j = 0; j<5; j++)
                        {
                            ShootBullet(Left2_37, new Vector3(2, -1, 0));
                            ShootBullet(Right2_37, new Vector3(-2, -1, 0));
                            yield return new WaitForSeconds(0.5f);
                        }

                        break;

                    case 38:  //팔자로 쏘기
                        BulletPattern Left2_38 = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(-1f, -4f, 0));
                        BulletPattern Right2_38 = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(1f, -4f, 0));

                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(Left2_38, new Vector3(-1, -1, 0));
                            ShootBullet(Right2_38, new Vector3(1, -1, 0));
                            yield return new WaitForSeconds(0.5f);
                        }

                        break;

                    case 39:  //원형쏘기 두번+1번몹
                        StartCoroutine(ShootCircle());
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        yield return new WaitForSeconds(1f);
                        StartCoroutine(ShootCircle());

                        break;

                        #endregion
                }
                yield return new WaitForSeconds(BossPatternData[i].y);
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
    BulletPattern _basePattern = new BulletPattern("BasePattern", 3);
    //속도3, 방향 아래, 조금 아래에서 시작
    BulletPattern _baseLeft = new BulletPattern("BaseLeft" , 3f, bulletDir: new Vector3(-1.2f, -3f, 0));
    //왼쪽방향으로 큰거
    BulletPattern _baseRight = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(1.2f, -3f, 0));

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    void ShootBullet(BulletPattern bulletPattern, Vector3? poz = null, float BulletSize = 1)
    {
        GameObject bullet;

        if (bulletPool.Count > 0)
        {
            bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
        }
        else
        {
            bullet = Instantiate(BulletPrefab);
        }

        Vector3 temp = transform.position + bulletPattern.Offset + (poz ?? Vector3.zero);
        bullet.GetComponent<MonsterBullet>().Setting(bulletPattern.BulletDir, bulletPattern.BulletSpeed, temp);
        bullet.transform.localScale = Vector3.one * BulletSize;

        StartCoroutine(ReturnBulletAfterDelay(bullet, 5f));
    }

    IEnumerator ReturnBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnBullet(bullet);
    }

    void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    private Queue<GameObject> longBulletPool = new Queue<GameObject>();

    void ShootLongBullet(BulletPattern bulletPattern, Vector3? poz = null)
    {
        GameObject bullet;

        if (longBulletPool.Count > 0)
        {
            bullet = longBulletPool.Dequeue();
            bullet.SetActive(true);
        }
        else
        {
            bullet = Instantiate(LongBulletPrefab);
        }

        Vector3 temp = transform.position + bulletPattern.Offset + (poz ?? Vector3.zero);
        bullet.transform.position = temp;

        Vector3 dir = _PlayerTrans.position - bullet.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.GetComponent<MonsterBullet>().Setting(dir, bulletPattern.BulletSpeed, temp);

        StartCoroutine(ReturnLongBulletAfterDelay(bullet, 5f));
    }

    IEnumerator ReturnLongBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnLongBullet(bullet);
    }

    void ReturnLongBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        longBulletPool.Enqueue(bullet);
    }

    void ShootSplitBall(BulletPattern bulletPattern, Vector3? poz = null, float BulletSize = 1)
    {
        GameObject _splitball = Instantiate(SplitBall);

        Vector3 temp = transform.position + bulletPattern.Offset + (poz ?? Vector3.zero);
        _splitball.GetComponent<MonsterBullet>().Setting(bulletPattern.BulletDir, bulletPattern.BulletSpeed, temp);
        _splitball.transform.localScale = Vector3.one * BulletSize;
    }
}

[System.Serializable]
public struct BulletPattern
{
    public string PatternName;
    public float BulletSpeed;
    public Vector3 BulletDir;
    public Vector3 Offset;

    public BulletPattern(string patternName, float bulletSpeed = 3f, 
        Vector3 bulletDir = default(Vector3), Vector3 offset = default(Vector3))
    {
        PatternName = patternName ?? throw new ArgumentNullException(nameof(patternName));
        BulletSpeed = bulletSpeed;
        BulletDir = bulletDir != default(Vector3) ? bulletDir : Vector3.down;
        Offset = offset != default(Vector3) ? offset : Vector3.down;
    }
}
