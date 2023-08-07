using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class BossPattern : MonoBehaviour
{
    float PatternStartDelay = 2f;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject LongBulletPrefab;
    [SerializeField] GameObject RaserDanger;
    [SerializeField] GameObject RaserPrefab;
    [SerializeField] GameObject SplitBall;
    Transform _PlayerTrans;

    [Header("���� �̸� / ���ϰ��� ����(��)")]
    public Vector2[] BossPatternData;
    

    void Start()
    {
        StartCoroutine(StartBattle());
        _PlayerTrans = GameObject.Find("Player").transform;
        gameObject.GetComponent<EnemyBase>().IsBoss = true;
    }

 



    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }


    #region CommonPatterns
    IEnumerator ShootHalfCircle(float size = 1f, bool second = false)
    {
        float angle1 = 0f;  float delta = 14;
        if (second) angle1 -= delta * 0.5f;
        for (int j = 0; j < 15; j++)
        {
            Vector3 dir1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle1), Mathf.Sin(Mathf.Deg2Rad * angle1), 0f);

            GameObject bullet = Instantiate(BulletPrefab);
            Vector3 temp = transform.position + new Vector3(0, -1, 0);
            bullet.GetComponent<MonsterBullet>().Setting(dir1, 3f, temp);
            bullet.transform.localScale = Vector3.one * size;

            angle1 -= delta;
        }
        yield return null;
    }

    IEnumerator ShootCircle(float size = 1f)
    {
        float angle2 = 18f;
        for (int j = 0; j < 10; j++)
        {
            Vector3 dir1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle2), Mathf.Sin(Mathf.Deg2Rad * angle2), 0f);

            GameObject bullet = Instantiate(BulletPrefab);
            Vector3 temp = transform.position + new Vector3(0, -1, 0);
            bullet.GetComponent<MonsterBullet>().Setting(dir1, 3f, temp);
            bullet.transform.localScale = Vector3.one * size;

            angle2 -= 36f;
        }
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Shoot3Way(float size = 1f)
    {
        for (int j = 0; j < 5; j++)
        {
            ShootBullet(_baseLeft, new Vector3(-0.5f, 0, 0), size);
            ShootBullet(_basePattern, null, size);
            ShootBullet(_baseRight, new Vector3(0.5f, 0f, 0), size);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ShootDown5Time(float size = 1f)
    {
        for (int j = 0; j < 5; j++)
        {
            ShootBullet(_basePattern, Vector3.right, size);
            ShootBullet(_basePattern, Vector3.left, size);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Shoot2_3_2(float xValue = 0, float size = 1f)
    {
        float offset12 = 0.8f;

        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 0.5f, 0f, 0f), 0.5f * size);
        ShootBullet(_basePattern, new Vector3(xValue + offset12 * 0.5f, 0f, 0f), 0.5f * size);

        yield return new WaitForSeconds(0.3f);

        ShootBullet(_basePattern, new Vector3(xValue - offset12, 0f, 0f), 0.5f * size);
        ShootBullet(_basePattern, new Vector3(xValue , 0f, 0f), 0.5f * size);
        ShootBullet(_basePattern, new Vector3(xValue + offset12 , 0f, 0f), 0.5f * size);

        yield return new WaitForSeconds(0.3f);

        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 0.5f, 0f, 0f), 0.5f * size);
        ShootBullet(_basePattern, new Vector3(xValue + offset12 * 0.5f, 0f, 0f), 0.5f * size);

        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator ShootSpiral(float size = 0.7f)
    {
        float angle1 = 0f; float angle2 = 180f;
            for (int i = 0; i < 10; i++)
            {
                Vector3 dir1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle1), Mathf.Sin(Mathf.Deg2Rad * angle1), 0f);
                Vector3 dir2 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle2), Mathf.Sin(Mathf.Deg2Rad * angle2), 0f);

                GameObject bullet = Instantiate(BulletPrefab);
                Vector3 temp = transform.position;
                bullet.GetComponent<MonsterBullet>().Setting(dir1, 3f, temp);
                bullet.transform.localScale = Vector3.one * size;

                GameObject bullet2 = Instantiate(BulletPrefab);
                Vector3 temp2 = transform.position;
                bullet2.GetComponent<MonsterBullet>().Setting(dir2, 3f, temp2);
                bullet2.transform.localScale = Vector3.one * size;

                angle1 += 18f; angle2 += 18f;
                yield return new WaitForSeconds(0.1f);
            }
    }

    IEnumerator FireLaser(bool toPlayer = false, Vector3? dir = null)
    {
        GameObject danger2 = Instantiate(RaserDanger);
        danger2.transform.position +=  transform.position + new Vector3(0,-1f,0);
        Vector3 playerDirection = Vector3.down;

        if (dir != null)
        {
            playerDirection = dir.Value;
            Quaternion rotation = Quaternion.LookRotation(Vector3.back, playerDirection);
            danger2.transform.rotation = rotation;
        }

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

        if (toPlayer || dir!= null)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, playerDirection);
            Raser.transform.rotation = rotation;
        }

        yield return new WaitForSeconds(1.6f);
    }

    IEnumerator ShootLong2Time()
    {
        for(int j =0; j<2;j++)
        {
            ShootLongBullet(_basePattern, new Vector3(-2f, 0f, 0f));
            ShootLongBullet(_basePattern, new Vector3(2f, 0f, 0f));
            yield return new WaitForSeconds(1f);
        }
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
                    case 1: //ū�� �ι� �߻�
                        ShootBullet(_basePattern, Vector3.right *0.5f, 0.7f);
                        ShootBullet(_basePattern, Vector3.left  *0.5f, 0.7f);

                        break;

                    case 301: //ū�� �ι� �߻�
                        ShootBullet(_basePattern, Vector3.right * 1f, 0.7f);
                        ShootBullet(_basePattern, Vector3.left * 1f, 0.7f);

                        break;

                    case 2: // �۰� 5�� ���� �߻�
                        for(int j = 0; j <5; j++)
                        {
                            ShootBullet(_basePattern, Vector3.right*0.8f , 0.5f);
                            ShootBullet(_basePattern, Vector3.left*0.8f ,  0.5f);
                            yield return new WaitForSeconds(0.7f);
                        }
                        break;

                    case 302: // �۰� 5�� ���� �߻�
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, Vector3.right * 1.6f, 0.5f);
                            ShootBullet(_basePattern, Vector3.left * 1.6f, 0.5f);
                            yield return new WaitForSeconds(0.7f);
                        }
                        break;

                    case 3: //�������� 5��
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseLeft, new Vector3(-0.5f, 0,0), 0.5f );
                            ShootBullet(_baseLeft, new Vector3(1.1f, -0.7f, 0), 0.5f);
                            yield return new WaitForSeconds(0.7f);
                        }
                        break;

                    case 4: //���������� 5��
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseRight, new Vector3(-1.1f, -0.7f, 0), 0.5f);
                            ShootBullet(_baseRight, new Vector3(0.5f, 0f, 0), 0.5f);
                            yield return new WaitForSeconds(0.7f);
                        }
                        break;

                    case 5: //ū�� 4��
                        ShootBullet(_basePattern, new Vector3(-2.2f, 0, 0), 0.7f);
                        ShootBullet(_basePattern, new Vector3(-1.1f, 0, 0), 0.7f);
                        ShootBullet(_basePattern, new Vector3(1.1f, 0, 0), 0.7f);
                        ShootBullet(_basePattern, new Vector3(2.2f, 0, 0), 0.7f);
                        break;

                    case 6: //�ݿ������� �߻�
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.7f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        break;

                    case 7:  //3������ �߻�
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseLeft, new Vector3(-0.5f, 0, 0), 0.6f);
                            ShootBullet(_basePattern, new Vector3(0f, 0, 0), 0.6f);
                            ShootBullet(_baseRight, new Vector3(0.5f, 0f, 0), 0.6f);
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;

                    case 8: //������ �߻�
                        yield return StartCoroutine(FireLaser(true));
               
                        break;

                    case 500 ://case3,4 ���ÿ� �߻�
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseLeft, new Vector3(-0.5f, 0, 0), 0.5f);
                            ShootBullet(_baseLeft, new Vector3(1.1f, -0.7f, 0), 0.5f);
                            ShootBullet(_baseRight, new Vector3(-1.1f, -0.7f, 0), 0.5f);
                            ShootBullet(_baseRight, new Vector3(0.5f, 0f, 0), 0.5f);
                            yield return new WaitForSeconds(0.7f);
                        }

                        break;

                    #endregion

                    #region Dog

                    

                    case 10: //�������� 2_3_2 �߻�
                        float offset12 = 0.8f;
                        float xValue = 2.5f;
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12*0.5f, 0f, 0f) ,0.5f);
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12 * 1.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 0.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 1.5f, 0f, 0f), 0.5f);

                        yield return new WaitForSeconds(0.4f);

                        ShootBullet(_basePattern, new Vector3(-xValue, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12*2, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 2, 0f, 0f), 0.5f);

                        yield return new WaitForSeconds(0.4f);

                        ShootBullet(_basePattern, new Vector3(-xValue + offset12 * 0.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(-xValue + offset12 * 1.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 0.5f, 0f, 0f), 0.5f);
                        ShootBullet(_basePattern, new Vector3(xValue - offset12 * 1.5f, 0f, 0f), 0.5f);

                        yield return new WaitForSeconds(0.4f);
                        break;

                    case 11:  //���� ū��, ��� ������ �ΰ�
                        for(int j =0; j<2;j++)
                        {
                            ShootBullet(_basePattern, Vector3.right * 2.3f, 0.8f);
                            ShootBullet(_basePattern, Vector3.left * 2.3f, 0.8f);
                            ShootBullet(_basePattern, Vector3.right * 0.3f, 0.5f);
                            ShootBullet(_basePattern, Vector3.left * 0.3f, 0.5f);
                            yield return new WaitForSeconds(0.6f);
                        }
                        break;

                    case 501: //case3,4 ���ÿ� �߻�
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseLeft, new Vector3(0.1f, 0, 0), 0.5f);
                            ShootBullet(_baseLeft, new Vector3(-1.1f, -0.7f, 0), 0.5f);
                            ShootBullet(_baseRight, new Vector3(1.1f, -0.7f, 0), 0.5f);
                            ShootBullet(_baseRight, new Vector3(-0.1f, 0f, 0), 0.5f);
                            yield return new WaitForSeconds(0.7f);
                        }

                        break;

                    #endregion

                    #region chick

                    case 303: //ū�� �ι� �߻�
                        ShootBullet(_basePattern, Vector3.right * 0.5f, 0.7f, true) ;
                        ShootBullet(_basePattern, Vector3.left * 0.5f, 0.7f, true);

                        break;

                    case 304: // �۰� 5�� ���� �߻�
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, Vector3.right * 0.8f, 0.5f, true);
                            ShootBullet(_basePattern, Vector3.left * 0.8f, 0.5f, true);
                            yield return new WaitForSeconds(0.7f);
                        }
                        break;

                    case 13:  //�� �Ѿ� �¿쿡�� �߻�
                        ShootLongBullet(_basePattern, new Vector3(-2f,0f,0.7f));
                        ShootLongBullet(_basePattern, new Vector3(2f, 0f, 0.7f));
                        
                        break;
                    

                    case 14:   //�¿� �ѹ�, ��� �ѹ�
                        ShootBullet(_basePattern, new Vector3(-2.2f, -0f, 0f) , 1.0f);
                        ShootBullet(_basePattern, new Vector3(2.2f, -0f, 0f), 1.0f);
                        yield return new WaitForSeconds(0.5f);
                        ShootBullet(_basePattern, new Vector3(0.5f, -0f, 0f), 0.7f);
                        ShootBullet(_basePattern, new Vector3(-0.5f, -0f, 0f), 0.7f);

                        break;


                    case 15:    //���ʿ��� 3���� ��� ����
                        transform.DOMoveX(-1f, 1f);
                        yield return new WaitForSeconds(1.5f);
                        //�������� �̵�
                        yield return StartCoroutine(Shoot3Way(0.7f));
                      
                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1.5f);
                        //���ƿ���
                        break;

                    case 16:    //�����ʿ��� 3���� ��� ����
                        transform.DOMoveX(1f, 1f);
                        yield return new WaitForSeconds(1.5f);
                        //�������� �̵�
                        yield return StartCoroutine(Shoot3Way(0.7f));

                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1.5f);
                        //���ƿ���
                        break;
                    #endregion

                    #region Monkey
                    case 17:  //X�� ����
                        Vector3 offset = new Vector3(1.4f, 0, 0);

                        for(int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, (-2f +j)*offset ,0.5f);
                            ShootBullet(_basePattern, (2f - j) * offset, 0.5f);
                            yield return new WaitForSeconds(0.5f);
                        }
                   break;

                    case 9:  //2_3_2�� ��ü
                        yield return StartCoroutine(Shoot2_3_2());
                        break;

                    //���� ������ 13�� 3�� �������� ��ü

                    case 18: //���ʺ��� �̵��ϸ鼭 3�� ����
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

                                ShootBullet(_basePattern, new Vector3( offset2, 0, 0), 0.5f);
                                ShootBullet(_basePattern, new Vector3(-offset2, 0, 0), 0.5f);
                                yield return new WaitForSeconds(0.5f);
                            }
                        }
                        transform.DOMoveX(0, 1f);
                        yield return new WaitForSeconds(1f);
                        break;

                    case 20:  //���ʿ��� ���������� �̵��ϸ鼭 5��
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

                    case 21:  //�����ʿ��� �������� �̵��ϸ鼭 5��
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
                    

                    case 22:  //�����ͼ� �������� �߻�
                        transform.DOMoveY(0.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(ShootCircle(0.7f));

                        transform.DOMoveY(3.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 23:   //�нż�

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

                    case 24:  //�� źȯ 3��, 2��
                        ShootLongBullet(_basePattern, new Vector3(-2, -2, 0));
                        ShootLongBullet(_basePattern, new Vector3(0, -2, 0));
                        ShootLongBullet(_basePattern, new Vector3(2, -2, 0));
                        yield return new WaitForSeconds(0.5f);
                        ShootLongBullet(_basePattern, new Vector3(1, 0, 0));
                        ShootLongBullet(_basePattern, new Vector3(-1, 0, 0));

                        break;

                 

                    case 400:    //���ʿ��� 3���� �߻�
                        transform.DOMoveX(-1f, 1f);
                        yield return new WaitForSeconds(1.5f);
                        //�������� �̵�
                        yield return StartCoroutine(Shoot3Way(0.7f));

                     
                        break;


                    //���ʿ��� 3����
                    //15�� �߰� ����

                    case 25:
                        transform.DOMoveX(-1f, 1f);
                        yield return new WaitForSeconds(1f);
                        //�������� �̵�
                        yield return StartCoroutine(ShootCircle(0.8f));

                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1f);
                        //���ƿ���
                        break;

                    //22�� �����̿� ����

               

                    case 26: 
                        //4�� ���̺� ��ȯ
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(4);
                        break;

                    case 27: // �����ͼ� ���� ź��
                        transform.DOMoveY(0.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(ShootSpiral(0.5f));

                        transform.DOMoveY(2.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 28: //����1 + �ݿ� ź��
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        break;

                    case 29: // ���������� ���鼭 źȯ �߻�
                        for(int j = 0; j<5;j++)
                        {
                            ShootBullet(_basePattern, null, 0.5f, true);
                            yield return new WaitForSeconds(0.5f);
                        }

                        transform.DOMoveX(1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, null, 0.5f, true);
                            yield return new WaitForSeconds(0.5f);
                        }

                        transform.DOMoveX(2f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, null, 0.5f, true);
                            yield return new WaitForSeconds(0.5f);
                        }

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 30:  //������ 0.5�� �������� �ι� �߻�
                        StartCoroutine(FireLaser(true));
                        yield return new WaitForSeconds(1f);
                        yield return StartCoroutine(FireLaser(true));
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        
                        break;

                    #endregion


                    #region horse

                    case 31: //���� > ��� > ������ �ɰ������Ѿ�
                        transform.DOMoveX(-1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        ShootSplitBall(_basePattern, null, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        ShootSplitBall(_basePattern, null, 0.5f ,1.4f);
                        yield return new WaitForSeconds(0.5f);

                        transform.DOMoveX(1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        ShootSplitBall(_basePattern, null, 0.5f, 0.9f);
                        yield return new WaitForSeconds(0.5f);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 32: // �����ʿ��� �ö���鼭 3���� ���
                        
                        transform.DOMoveX(1f, 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        transform.DOMoveY(0.5f, 0.3f);
                        yield return new WaitForSeconds(0.3f);
                        StartCoroutine(Shoot3Way(0.7f));
                        transform.DOMove(new Vector3(0, 3.5f, 0), 2f);
                        yield return new WaitForSeconds(2f);
                        

                        break;

                    case 33:  //�������� ��� + �����ȯ
                        for(int j =0; j<2; j++)
                        {
                            StartCoroutine(ShootCircle(0.7f));
                            Stage1MonsterSpawner.instance.SpawnMonsterWave(4);
                            Stage1MonsterSpawner.instance.SpawnMonsterWave(12);
                        }

                        break;

                    case 34: //���ʰ��� ����, �볪�� �ΰ�

                        transform.DOMoveX(-1.7f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        ShootSplitBall(_basePattern);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        break;


                    case 35: //���ʿ��� �ö���鼭
                        
                        transform.DOMoveX(-1f, 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        transform.DOMoveY(0.5f, 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        transform.DOMove(new Vector3(0, 3.5f, 0), 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        for(int j =0; j<5; j++)
                        {
                            ShootBullet(_basePattern, new Vector3(-1, 0, 0), 0.7f);
                            ShootBullet(_basePattern, new Vector3(1, 0, 0), 0.7f);
                            yield return new WaitForSeconds(0.4f);
                        }
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);

                        break;

                    case 36:  //2_3_2 �ι�
                        yield return StartCoroutine(Shoot2_3_2(-0.7f));
                        yield return new WaitForSeconds(0.4f);
                        yield return StartCoroutine(Shoot2_3_2(0.7f));
                        break;

                    //�������� 27��

                    #endregion


                    #region snake

                    //2������ ����

                    case 37:  //X �ڷ� ���
                        BulletPattern Left2_37 = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(-2f, -3f, 0));
                        BulletPattern Right2_37 = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(2f, -3f, 0));

                        for(int j = 0; j<5; j++)
                        {
                            ShootBullet(Left2_37, new Vector3(2, -1, 0), 0.5f);
                            ShootBullet(Right2_37, new Vector3(-2, -1, 0), 0.5f);
                            yield return new WaitForSeconds(0.5f);
                        }

                        break;

                    case 38:  //���ڷ� ���
                        BulletPattern Left2_38 = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(-1f, -4f, 0));
                        BulletPattern Right2_38 = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(1f, -4f, 0));

                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(Left2_38, new Vector3(-1, -1, 0), 0.5f);
                            ShootBullet(Right2_38, new Vector3(1, -1, 0), 0.5f);
                            yield return new WaitForSeconds(0.5f);
                        }

                        break;

                    case 39:  //������� �ι�+1����
                        StartCoroutine(ShootCircle(0.5f));
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        yield return new WaitForSeconds(1f);
                        StartCoroutine(ShootCircle(0.5f));

                        break;

                    case 40: //���ʾƷ� ���� > �����ʾƷ� ���� > ����
                        transform.DOMove(new Vector3(-1.5f, 0.5f, 0), 1);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(ShootCircle(0.5f));

                        transform.DOMove(new Vector3(1.5f, 0.5f, 0), 1);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(ShootCircle(0.5f));


                        transform.DOMove(new Vector3(0, 3.5f, 0), 1);
                        yield return new WaitForSeconds(1);

                        break;

                    case 41:  //�������°� 4��

                        for(float j = -1.5f; j<= 1.5f; j++)
                        {
                            float k = j * 1.5f;
                            ShootSplitBall(_basePattern, new Vector3(k, 0,0), 0.4f);
                        }

                        break;

                    case 42:  //��� + �ݿ� + �볪��
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(3);
                        yield return new WaitForSeconds(1);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        StartCoroutine(ShootHalfCircle(0.5f));

                        break;

                    case 43:  //��� + �ݿ�
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(3);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(ShootHalfCircle(0.5f));
                        break;


                    case 44:  // ��� + ���� �̵� �� ������
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(12);
                        yield return new WaitForSeconds(2);
                        transform.DOMoveX(-1f, 1f);
                        yield return new WaitForSeconds(1.5f);
                        //�������� �̵�
                        yield return StartCoroutine(Shoot3Way(0.5f));

                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1.5f);
                        //���ƿ���
                        break;

                    #endregion

                    #region Dragon

                    case 45:  //��� 3�� �� 5��
                        ShootLongBullet(_basePattern, new Vector3(-2, -2, 0));
                        ShootLongBullet(_basePattern, new Vector3(0, -2, 0));
                        ShootLongBullet(_basePattern, new Vector3(2, -2, 0));

                        yield return new WaitForSeconds(0.5f);

                        for (int j = -2; j < 3; j++)
                        {
                            float temp = j * 1.5f;
                            ShootBullet(_basePattern, new Vector3(temp,0,0), 0.5f);
                        }

                        break;

                    case 46:  //�볪�� + �ݿ� �ι�
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        StartCoroutine(ShootHalfCircle(0.4f));

                        yield return new WaitForSeconds(3f);

                        StartCoroutine(ShootHalfCircle(0.4f));
                        break;

                        //�����ͼ� ���� ź�� 27��

                    case 47://�������� ���� ź����� �볪��

                        transform.DOMoveX(-1f, 0.3f);
                        yield return new WaitForSeconds(0.3f);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        yield return StartCoroutine(Shoot3Way(0.5f));

                        transform.DOMoveX(0f, 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        break;

                    case 48:  //27���ϰ� ���� ���
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(8);
                        break;

                    case 49://���������� ���� ź����� �볪��

                        transform.DOMoveX(1f, 0.3f);
                        yield return new WaitForSeconds(0.3f);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        yield return StartCoroutine(Shoot3Way(0.6f));

                        transform.DOMoveX(0f, 0.3f);
                        yield return new WaitForSeconds(0.3f);

                        break;

                    //�ٽ� 27+48 ����

                    case 50: //���� + �밢�� ���ø�
                        StartCoroutine(ShootCircle(0.5f));
                        yield return new WaitForSeconds(1f);
                        ShootSplitBall(_baseLeft);
                        ShootSplitBall(_baseRight);

                        break;

                    case 51: //���� + �׳� ���ø�
                        StartCoroutine(ShootCircle(0.5f));
                        yield return new WaitForSeconds(1f);
                        ShootSplitBall(_basePattern , new Vector3(-1,0,0));
                        ShootSplitBall(_basePattern, new Vector3(1, 0, 0));
                        break;

                    case 52:  //������3���� ���
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        StartCoroutine(ShootHalfCircle(0.7f));
                        StartCoroutine(FireLaser(false, new Vector3(1, 3, 0)));
                        StartCoroutine(FireLaser(false, new Vector3(-1, 3, 0)));
                        StartCoroutine(FireLaser(true));

                        break;
                    #endregion

                    #region Rabbit

                    case 53: //���ڸ� 3����
                        ShootBullet(_basePattern, Vector3.right * 1f, 0.5f);
                        ShootBullet(_basePattern, Vector3.left * 1f, 0.5f);
                        yield return StartCoroutine(Shoot3Way(0.7f));

                        break;

                    case 54: // ���� + 3����
                        StartCoroutine(ShootCircle(0.5f));
                        yield return StartCoroutine(Shoot3Way(0.5f));
                        break;

                    case 55: //���ʰ��� ��鼭 �볪��2��
                        transform.DOMoveX(-1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, Vector3.right, 0.8f);
                            ShootBullet(_basePattern, Vector3.left, 0.8f);
                            yield return new WaitForSeconds(0.5f);
                        }

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        break;

                    case 56: //�����ʰ��� ��鼭 �볪��2��
                        transform.DOMoveX(1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_basePattern, Vector3.right, 0.8f);
                            ShootBullet(_basePattern, Vector3.left, 0.8f);
                            yield return new WaitForSeconds(0.5f);
                        }

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        break;

                    case 306: //�ݿ������� �߻�
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        break;

                    case 307: // �����ͼ� ���� ź��
                        transform.DOMoveY(0.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(ShootSpiral(0.5f));
                        yield return StartCoroutine(ShootSpiral(0.5f));
                        transform.DOMoveY(2.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    //27�� ���

                    case 57:  //���ʿ��� 3����� ����
                        transform.DOMoveX(-1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        for(int j =0;j<3;j++)
                        {
                            StartCoroutine(ShootCircle(0.7f));
                            yield return new WaitForSeconds(0.5f);
                        }
                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        ShootLongBullet(_basePattern, new Vector3(-2f, 0f, 0.7f));
                        ShootLongBullet(_basePattern, new Vector3(2f, 0f, 0.7f));
                        break;

                    case 58:  //�����ʿ��� 2����� ����
                        transform.DOMoveX(1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        for (int j = 0; j < 2; j++)
                        {
                            StartCoroutine(ShootCircle(0.7f));
                            yield return new WaitForSeconds(0.5f);
                        }

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        ShootLongBullet(_basePattern, new Vector3(-2f, 0f, 0.7f));
                        ShootLongBullet(_basePattern, new Vector3(2f, 0f, 0.7f));
                        break;

                    case 59:  //õõ�� ����
                        transform.DOMove(new Vector3(2.5f, 0.5f, 0), 2f);
                        yield return new WaitForSeconds(2f);

                        transform.DOMove(new Vector3(-2.5f, 0.5f, 0), 2f);
                        yield return new WaitForSeconds(2f);

                        Stage1MonsterSpawner.instance.SpawnMonsterWave(31);


                        transform.DOMove(new Vector3(-1.5f, -2.5f, 0), 2f);
                        yield return new WaitForSeconds(2f);

                        transform.DOMove(new Vector3(1.5f, -2.5f, 0), 2f);
                        yield return new WaitForSeconds(2f);

                        transform.DOMove(new Vector3(0, 3.5f, 0), 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        break;

                    case 60:  //���� �ι�
                        for (int j = 0; j < 2; j++)
                        {
                            StartCoroutine(ShootCircle(0.7f));
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;

                    case 61:  //���� + �볪�� 2��
                        StartCoroutine(ShootCircle(0.7f));
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        break;

                    #endregion

                    #region Tiger

                    case 62:  //3���� 2��
                        yield return StartCoroutine(Shoot3Way(0.6f));
                        yield return new WaitForSeconds(1f);
                        yield return StartCoroutine(Shoot3Way(0.6f));

                        break;

                    case 63:  //�볪�� + �������4��
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        StartCoroutine(ShootCircle(0.6f));
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(ShootHalfCircle(0.6f));
                        yield return new WaitForSeconds(1.5f);
                        StartCoroutine(ShootCircle(0.6f));
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(ShootHalfCircle(0.5f));
                        break;

                    case 64: //���ø� + �����
                        ShootSplitBall(_basePattern, Vector3.left);
                        ShootSplitBall(_basePattern, Vector3.right);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(15);
                        break;

                    case 65: //�볪�� + 4����
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        yield return new WaitForSeconds(1.5f);
                        for (int j = 0; j < 5; j++)
                        {
                            ShootBullet(_baseLeft, new Vector3(-1.5f, 0, 0));
                            ShootBullet(_baseLeft, new Vector3(0.2f, -0.7f, 0));
                            ShootBullet(_baseRight, new Vector3(-0.2f, -0.7f, 0));
                            ShootBullet(_baseRight, new Vector3(1.5f, 0f, 0));
                            yield return new WaitForSeconds(0.5f);
                        }
                        break;

                    case 66: //��� + ���ø� + ���
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(31);
                        yield return new WaitForSeconds(3f);
                        ShootSplitBall(_basePattern, Vector3.left);
                        ShootSplitBall(_basePattern, Vector3.right);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        break;

                    case 67: 
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(3);

                        break;

                    //37�� ���� 68�� �̾ ���

                    case 68:
                        yield return new WaitForSeconds(1f);
                        StartCoroutine(ShootCircle(0.7f));
                        break;



                    case 69: //3����+ ���3��
                        StartCoroutine(ShootLong2Time());
                        for(int j =0;j<5;j++)
                        {
                            ShootBullet(_basePattern, Vector3.right * 2.5f, 0.5f);
                            ShootBullet(_basePattern, null, 0.5f);
                            ShootBullet(_basePattern, Vector3.left * 2.5f, 0.5f);

                            yield return new WaitForSeconds(0.5f);
                        }
                        break;

                    case 70: //������ 5��
                        StartCoroutine(Shoot2_3_2(0f));
                        StartCoroutine(FireLaser(false, new Vector3(3, 3, 0)));
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(FireLaser(false, new Vector3(1, 3, 0)));
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(FireLaser(true));
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(FireLaser(false, new Vector3(-1, 3, 0)));
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(FireLaser(false, new Vector3(-3, 3, 0)));
                        yield return new WaitForSeconds(0.5f);


                        break;
                    #endregion


                    #region cow

                    case 71:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(4);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        yield return StartCoroutine(Shoot2_3_2(0.7f));


                        break;

                    case 72:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        transform.DOMoveX(-1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(Shoot3Way(0.7f));
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(7);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;


                    case 73:
                        transform.DOMoveX(-1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        Stage1MonsterSpawner.instance.SpawnMonsterWave(6);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(14);

                        StartCoroutine(ShootCircle(0.7f));
                        StartCoroutine(ShootDown5Time(0.5f));

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 74:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);

                        yield return StartCoroutine(ShootCircle(0.5f));
                        yield return new WaitForSeconds(1.5f);
                        yield return StartCoroutine(ShootCircle(0.5f));
                        yield return new WaitForSeconds(1.5f);
                        yield return StartCoroutine(ShootCircle(0.5f));
                        yield return new WaitForSeconds(1.5f);
                        break;

                    case 75:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(6);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(7);
                        yield return StartCoroutine(Shoot3Way(0.7f));

                        
                        break;

                    case 76:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(13);
                        transform.DOMoveX(1f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(Shoot3Way(0.5f));
                       
                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;


                    case 77:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(8);
                        yield return StartCoroutine(Shoot3Way(0.7f));

                        break;

                    case 78:
                        transform.DOMoveX(-1.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        StartCoroutine(FireLaser());
                        StartCoroutine(FireLaser(false, new Vector3(-0.7f,1,0) ));
                        yield return StartCoroutine(FireLaser(false, new Vector3(-1.4f, 1, 0)));
                        yield return new WaitForSeconds(1.5f);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);


                        break;

                    case 79:
                        transform.DOMoveX(1.5f, 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        StartCoroutine(FireLaser());
                        StartCoroutine(FireLaser(false, new Vector3(0.7f, 1, 0)));
                        yield return StartCoroutine(FireLaser(false, new Vector3(1.4f, 1, 0)));
                        yield return new WaitForSeconds(1.5f);

                        transform.DOMoveX(0f, 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;

                    #endregion


                    #region Rat

                    case 80:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(4);
                        yield return StartCoroutine(ShootCircle(0.6f));
                        yield return new WaitForSeconds(1.5f);
                        yield return StartCoroutine(ShootCircle(0.6f));

                        break;

                    case 81:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(31);
                        yield return StartCoroutine(Shoot3Way(0.7f));
                        yield return new WaitForSeconds(1.0f);
                        yield return StartCoroutine(Shoot3Way(0.7f));
                        break;

                    case 82:
                        
                        transform.DOMove(new Vector3(-1.5f, 0.5f, 0), 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(ShootCircle(0.5f));
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(12);

                        transform.DOMove(new Vector3(1.5f, 0.5f, 0), 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(ShootCircle(0.5f));

                        transform.DOMove(new Vector3(0f, 3.5f, 0), 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(ShootCircle(0.5f));


                        break;

                    case 83:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(5);

                        yield return StartCoroutine(Shoot2_3_2(0.6f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(Shoot2_3_2(0.6f));

                        break;

                    case 84:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(4);
                        transform.DOMove(new Vector3(0f, 0.5f, 0), 0.5f);
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(ShootSpiral(0.5f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootSpiral(0.5f));

                        transform.DOMove(new Vector3(0f, 3.5f, 0), 0.5f);
                        yield return new WaitForSeconds(0.5f);
                        break;


                         

                    case 85:  //�׸��ںн��̶� ���� �� �� 23��
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        break;

                    case 86:
                        ShootSplitBall(_baseLeft);
                        ShootSplitBall(_basePattern);
                        ShootSplitBall(_baseRight);
                        break;

                    case 87:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(6);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(7);
                        yield return StartCoroutine(Shoot3Way(0.6f));
                        break;


                    case 88:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        yield return new WaitForSeconds(0.5f);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(36);
                        yield return new WaitForSeconds(0.5f);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(1);
                        yield return StartCoroutine(ShootCircle(0.5f));
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(ShootCircle(0.5f));
                        yield return new WaitForSeconds(0.5f);

                        yield return StartCoroutine(ShootCircle(0.5f));
                        yield return new WaitForSeconds(0.5f);
                        break;

                    case 89:
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(35);
                        Stage1MonsterSpawner.instance.SpawnMonsterWave(37);
                        StartCoroutine(Shoot2_3_2());

                        StartCoroutine(FireLaser(false, new Vector3(1, 1, 0)));
                        yield return new WaitForSeconds(0.5f);

                        StartCoroutine(FireLaser(false, new Vector3(1, 3, 0)));
                        yield return new WaitForSeconds(0.5f);

                        StartCoroutine(FireLaser(true));
                        yield return new WaitForSeconds(0.5f);

                        StartCoroutine(FireLaser(false, new Vector3(-1, 3, 0)));
                        yield return new WaitForSeconds(0.5f);

                        StartCoroutine(FireLaser(false, new Vector3(-1, 1, 0)));
                        yield return new WaitForSeconds(0.5f);

                        break;

                    case 308: //�ݿ������� �߻�
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f));
                        yield return new WaitForSeconds(0.5f);
                        yield return StartCoroutine(ShootHalfCircle(0.4f, true));
                        yield return new WaitForSeconds(0.5f);
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
    //�ӵ�3, ���� �Ʒ�, ���� �Ʒ����� ����
    BulletPattern _baseLeft = new BulletPattern("BaseLeft" , 3f, bulletDir: new Vector3(-1.2f, -3f, 0));
    //���ʹ������� ū��
    BulletPattern _baseRight = new BulletPattern("BaseLeft", 3f, bulletDir: new Vector3(1.2f, -3f, 0));

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    void ShootBullet(BulletPattern bulletPattern, Vector3? poz = null, float BulletSize = 1 , bool toPlayer = false)
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

        if (toPlayer) bulletPattern.BulletDir = Player.instance.transform.position -temp;
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

    void ShootSplitBall(BulletPattern bulletPattern, Vector3? poz = null, float BulletSize = 1, float splittime = 1.9f)
    {
        GameObject _splitball = Instantiate(SplitBall);
        Vector3 temp = transform.position + bulletPattern.Offset + (poz ?? Vector3.zero);
        _splitball.GetComponent<MonsterBullet>().Setting(bulletPattern.BulletDir, bulletPattern.BulletSpeed, temp);
        _splitball.GetComponent<MonsterBullet>().splittime = splittime;
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
