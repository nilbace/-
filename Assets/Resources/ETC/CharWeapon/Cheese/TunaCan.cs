using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunaCan : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] GameObject BoomEffect;
    bool boomed = false;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 0 && !boomed)
        {
            boomed = true;
            Boom();
        }

        transform.Translate(0, MoveSpeed * Time.deltaTime, 0);
    }

    void Boom()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        GameObject go = Instantiate(BoomEffect, Vector3.zero, Quaternion.identity);

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach(GameObject bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }

        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject mon in Monsters)
        {
            mon.gameObject.GetComponent<EnemyBase>().MonsterHP -= 800;
        }

        Destroy(go, 2f);
        Destroy(gameObject, 3f);
    }
}
