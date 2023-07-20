using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void Update()
    {
        transform.Translate(0, -2 * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().PowerUp();
            Destroy(gameObject);
        }
    }
}
