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

    [Header("���� �̸� / ���ϰ��� ����(��)")]
    public Vector2[] pigPattern;
    [Header("�� ���Ϻ� ����")]
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

                    case 1:   //ū�� �ΰ� �߻�
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

                    case 7:  //�������� �߻�
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

                    case 8: //������ �߻�
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
                    case 13:  //�� �Ѿ� �¿쿡�� �߻�
                        ShootLongBullet(PatternData[9], new Vector3(-2f,0f,0f));
                        ShootLongBullet(PatternData[9], new Vector3(2f, 0f, 0f));
                        
                        break;
                    

                    case 14:   //1�� ����
                        ShootBullet(PatternData[0], new Vector3(-1f, -0f, 0f));
                        ShootBullet(PatternData[1], new Vector3(1f, -0f, 0f));
                        yield return new WaitForSeconds(1f);
                        ShootBullet(PatternData[0], new Vector3(0.5f, -0f, 0f));
                        ShootBullet(PatternData[1], new Vector3(-0.5f, -0f, 0f));

                        break;


                    case 15:    //3�� ����
                        transform.DOMoveX(-1f, 1f);
                        yield return new WaitForSeconds(1f);
                        //�������� �̵�

                        for (int j = 0; j < 5; j++)
                        {
                            Pattern6();
                            yield return new WaitForSeconds(PatternDelay[4]);
                        }
                        yield return new WaitForSeconds(2f);
                        //�߻� + ������ 2��

                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1f);
                        //���ƿ���

                        break;

                    case 16:    //4�� ����
                        transform.DOMoveX(1f, 1f);
                        yield return new WaitForSeconds(1f);
                        //�������� �̵�

                        for (int j = 0; j < 5; j++)
                        {
                            Pattern6();
                            yield return new WaitForSeconds(PatternDelay[4]);
                        }
                        yield return new WaitForSeconds(2f);
                        //�߻� + ������ 2��

                        transform.DOMoveX(0f, 1f);
                        yield return new WaitForSeconds(1f);
                        //���ƿ���

                        break;
                    #endregion

                    #region Monkey
                    case 17:  //X�� ����
                        Vector3 offset = new Vector3(1.4f, 0, 0);

                        for(int j = 0; j < 5; j++)
                        {
                            ShootBullet(PatternData[10], (-2f +j)*offset);
                            ShootBullet(PatternData[10], (2f - j) * offset);
                            yield return new WaitForSeconds(0.5f);
                        }
                   break;

                    case 18: //���ʺ��� �̵��ϸ鼭 ����
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

                    case 19: //���ʺ��� �̵��ϸ鼭 ����
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
                    

                    case 22:  //�������� �߻�
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
        //�Ѿ��� �÷��̾������� ȸ��

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
