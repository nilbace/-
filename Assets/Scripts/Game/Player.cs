using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Player : MonoBehaviour
{
    public Joystick[] Joystick;
    Joystick _nowjoystick;

    Rigidbody2D rigid;
    public float moveSpeed;
    Vector3 moveVec;

    [SerializeField] BulletPool bulletPool;

    StatManager _statmanager;
    int[] resultStats = new int[6];
    float _attackTerm;

    [Header("Power")]
    public int PowerLevel = 1;
    [SerializeField] Define.BulletPosition _bulletpositionGroup;

    [Header("Bomb")]
    public UnityEngine.UI.Image bombImage;
    public CharBomb[] CharBombs;



    public void PowerUp()
    {
        if(PowerLevel < 3)
        {
            PowerLevel++;
        }
        else
        {
            return;
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        _statmanager = GetComponent<StatManager>();

        SetStat();

        if (Managers.Data.MySettingData.isFixedJoystick)
        {
            _nowjoystick = Joystick[0];
            Joystick[1].gameObject.SetActive(false);
        }
        else
        {
            _nowjoystick = Joystick[1];
            Joystick[0].gameObject.SetActive(false);
        }

        bombImage.sprite = CharBombs[Managers.Data.SelectedCatIndex].BombSprite;
        StartCoroutine(timeChecker());
    }

    private void FixedUpdate()
    {
        float x = _nowjoystick.Horizontal;
        float y = _nowjoystick.Vertical;



        moveVec = new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
        rigid.MovePosition((Vector3)rigid.position + moveVec);

        if (moveVec.sqrMagnitude == 0)
            return;

        
        
    }

    void SetStat()
    {
        resultStats = _statmanager.ResultStats;
        _attackTerm = 25f / (float)resultStats[1];
        StartCoroutine(Shooting());
    }


    public void ShootBomb()
    {
        Instantiate(CharBombs[Managers.Data.SelectedCatIndex].BombGO, transform.position, Quaternion.identity);
    }

    IEnumerator Shooting()
    {
        while(true)
        {
            if(Managers.Data.SelectedCatIndex == 0)
            {
                if (PowerLevel == 1)
                {
                    yield return new WaitForSeconds(_attackTerm);
                    GameObject bullet = bulletPool.GetBullet();
                    if (bullet != null)
                    {
                        bullet.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level1, 0f, resultStats[0]);
                    }
                }

                else if (PowerLevel == 2)
                {
                    yield return new WaitForSeconds(_attackTerm);
                    GameObject bullet1 = bulletPool.GetBullet();
                    bullet1.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level2_1, 0f, resultStats[0]);
                    GameObject bullet2 = bulletPool.GetBullet();
                    bullet2.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level2_2, 0f, resultStats[0]);
                }

                else
                {
                    yield return new WaitForSeconds(_attackTerm);
                    GameObject bullet1 = bulletPool.GetBullet();
                    bullet1.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_1, 15f, resultStats[0]);

                    GameObject bullet2 = bulletPool.GetBullet();
                    bullet2.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_2, 0f, resultStats[0]);

                    GameObject bullet3 = bulletPool.GetBullet();
                    bullet3.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_3, 0f, resultStats[0]);

                    GameObject bullet4 = bulletPool.GetBullet();
                    bullet4.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_4, -15f, resultStats[0]);
                }
            }

            else if(Managers.Data.SelectedCatIndex == 1)
            {

            }


        }
    }

    IEnumerator timeChecker()
    {
        while(true)
        {
            if (Time.timeScale == 0)
            {
                if (Managers.Data.MySettingData.isFixedJoystick)
                {
                    Joystick[0].gameObject.SetActive(false);
                }
                else
                {
                    Joystick[1].gameObject.SetActive(false);
                }
            }
            else
            {
                if (Managers.Data.MySettingData.isFixedJoystick)
                {
                    Joystick[0].gameObject.SetActive(true);
                }
                else
                {
                    Joystick[1].gameObject.SetActive(true);
                }
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
