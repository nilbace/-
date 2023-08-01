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
    public int[] resultStats = new int[6];
    float _attackTerm;

    InGameData thisCatData;
    public static Player instance;

    public float invincibleTime;

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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        _statmanager = GetComponent<StatManager>();
        thisCatData = _statmanager.playerdata.Chars[Managers.Data.MyCharDatas.nowSelectCatIndex];

        GetComponent<SpriteRenderer>().sprite = thisCatData.BackImg;
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

        bombImage.sprite = CharBombs[0].BombSprite;
        StartCoroutine(timeChecker());
    }

    private void FixedUpdate()
    {
        //float x = _nowjoystick.Horizontal;
        //float y = _nowjoystick.Vertical;

        //moveVec = new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
        //rigid.MovePosition((Vector3)rigid.position + moveVec);

        //if (moveVec.sqrMagnitude == 0)
        //    return;        
    }
    private Vector2 offset;
    private bool isDragging = false;
    private float minX = -2.4f;
    private float maxX = 2.4f;
    private float minY = -4.1f;
    private float maxY = 2.6f;

    private void Update()
    {
        // �巡�� ����
        if(Time.timeScale == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                {
                    offset = transform.position - mouseWorldPos;
                    isDragging = true;
                }

            }

            // �巡�� ��
            if (isDragging && Input.GetMouseButton(0))
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float clampedX = Mathf.Clamp(mouseWorldPos.x + offset.x, minX, maxX);
                float clampedY = Mathf.Clamp(mouseWorldPos.y + offset.y, minY, maxY);
                transform.position = new Vector3(clampedX, clampedY, transform.position.z);
            }

            // �巡�� ��
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }

    void SetStat()
    {
        resultStats = _statmanager.ResultStats;
        _attackTerm = 25f / (float)resultStats[1];
        StartCoroutine(Shooting());
    }



    IEnumerator Shooting()
    {
        while(true)
        {
            TempSound.instance.SFX(TempSound.EffectSoundName.wea4);
            if (PowerLevel == 1)
            {
                yield return new WaitForSeconds(_attackTerm);
                GameObject bullet = bulletPool.GetBullet();
                if (bullet != null)
                {
                    bullet.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level1, 0f, resultStats[0], thisCatData.SkillIcon);
                }
            }

            else if (PowerLevel == 2)
            {
                yield return new WaitForSeconds(_attackTerm);
                GameObject bullet1 = bulletPool.GetBullet();
                bullet1.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level2_1, 0f, resultStats[0], thisCatData.SkillIcon);
                GameObject bullet2 = bulletPool.GetBullet();
                bullet2.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level2_2, 0f, resultStats[0], thisCatData.SkillIcon);
            }
            else
            {
                yield return new WaitForSeconds(_attackTerm);
                GameObject bullet1 = bulletPool.GetBullet();
                bullet1.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_1, 15f, resultStats[0], thisCatData.SkillIcon);

                GameObject bullet2 = bulletPool.GetBullet();
                bullet2.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_2, 0f, resultStats[0], thisCatData.SkillIcon);

                GameObject bullet3 = bulletPool.GetBullet();
                bullet3.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_3, 0f, resultStats[0], thisCatData.SkillIcon);

                GameObject bullet4 = bulletPool.GetBullet();
                bullet4.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_4, -15f, resultStats[0], thisCatData.SkillIcon);
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

    public void GoInvincibleTime()
    {
        StartCoroutine(InvincibleTimes());
    }


    IEnumerator InvincibleTimes()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color temp1 = GetComponent<SpriteRenderer>().color;
        temp1.a = 0.5f;
        sprite.color = temp1;

        yield return new WaitForSeconds(invincibleTime);

        GetComponent<BoxCollider2D>().enabled = true;
        temp1.a = 1f;
        sprite.color = temp1;
    }
}
