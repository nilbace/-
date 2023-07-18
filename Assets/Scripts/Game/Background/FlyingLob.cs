using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingLob : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameScene.instance.PlayerGetDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameScene.instance.PlayerGetDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -4 * Time.deltaTime, 0);
    }
}
