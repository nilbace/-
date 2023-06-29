using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    Vector3 bulletDir;
    float bulletSpeed;
    void Start()
    {
        Destroy(gameObject, 8f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().PlayerHP--;
            Destroy(gameObject);
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
}
