using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick fixedJoystick;

    Rigidbody2D rigid;
    public float moveSpeed;
    Vector3 moveVec;

    public float AttackTerm;

    [SerializeField] BulletPool bulletPool;

    [Header("HP")]
    [SerializeField] int _hp = 5;
    public int PlayerHP { get { return _hp; } set { _hp = value; } }

    [Header("Power")]
    public int PowerLevel = 1;
    [SerializeField] Define.BulletPosition _bulletpositionGroup;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(Shooting());
    }

    private void FixedUpdate()
    {
        float x = fixedJoystick.Horizontal;
        float y = fixedJoystick.Vertical;

        moveVec = new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
        rigid.MovePosition((Vector3)rigid.position + moveVec);

        if (moveVec.sqrMagnitude == 0)
            return;
    }

    IEnumerator Shooting()
    {
        while(true)
        {
            if(PowerLevel == 1)
            {
                yield return new WaitForSeconds(AttackTerm);
                GameObject bullet = bulletPool.GetBullet();
                if (bullet != null)
                {
                    bullet.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level1, 0f);
                }
            }

            else if(PowerLevel == 2)
            {
                yield return new WaitForSeconds(AttackTerm);
                GameObject bullet1 = bulletPool.GetBullet();
                bullet1.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level2_1, 0f);
                GameObject bullet2 = bulletPool.GetBullet();
                bullet2.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level2_2, 0f);
            }

            else
            {
                yield return new WaitForSeconds(AttackTerm);
                GameObject bullet1 = bulletPool.GetBullet();
                bullet1.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_1, 30f);
                GameObject bullet2 = bulletPool.GetBullet();
                bullet2.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_2, 0f);
                GameObject bullet3 = bulletPool.GetBullet();
                bullet3.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_3, 0f);
                GameObject bullet4 = bulletPool.GetBullet();
                bullet4.GetComponent<Bullet>().SetBullet(transform.position + _bulletpositionGroup.Level3_4, -30f);
            }
        }
    }
}
