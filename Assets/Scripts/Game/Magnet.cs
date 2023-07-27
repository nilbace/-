using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    public static Magnet instance;
    private void Awake()
    {
        instance = this;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    public void OnMagnet(int n)
    {
        StartCoroutine(MagnetOnOff(n));
    }

    IEnumerator MagnetOnOff(int n)
    {
        GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(n);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Gold"))
        {
            collision.gameObject.GetComponent<DropGold>().onMagnet = true;
        }
    }
}
