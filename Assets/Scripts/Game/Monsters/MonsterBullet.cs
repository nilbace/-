using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    Vector3 bulletDir;
    float bulletSpeed;
    [SerializeField] bool isSplit;
    bool hassplited = false;
    [SerializeField] float splittime;
    [SerializeField] GameObject baseBall;

    private void Start()
    {
        if (isSplit )
        {
            StartCoroutine(Split());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().PlayerHP--;
            gameObject.SetActive(false);
        }
    }

    public void Setting(Vector3 dir, float _speed, Vector3 poz)
    {
        bulletDir = dir;
        bulletSpeed = _speed;
        transform.position = poz;
    }

    private void FixedUpdate()
    {
        transform.position += bulletDir.normalized * bulletSpeed * Time.deltaTime;
    }


    IEnumerator Split()
    {
        yield return new WaitForSeconds(splittime);

        hassplited = true;

        GameObject ball1 = Instantiate(baseBall);
        Vector3 leftRotate = Quaternion.Euler(0f, 0f, 90f) * bulletDir;
        ball1.GetComponent<MonsterBullet>().Setting(leftRotate, bulletSpeed, transform.position);
        ball1.GetComponent<MonsterBullet>().Splited();

        GameObject ball2 = Instantiate(baseBall);
        Vector3 rightRotate = Quaternion.Euler(0f, 0f, -90f) * bulletDir;
        ball2.GetComponent<MonsterBullet>().Setting(rightRotate, bulletSpeed, transform.position);
        ball2.GetComponent<MonsterBullet>().Splited();

        Destroy(gameObject);
    }

    public void Splited()
    {
        Destroy(gameObject, 5f);
    }
}
