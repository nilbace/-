using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public Joystick fixedJoystick;
    public float moveSpeed;
    Vector3 moveVec;

    public GameObject Bullet;
    public float AttackTerm;

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
            yield return new WaitForSeconds(AttackTerm);
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }
    }
}
