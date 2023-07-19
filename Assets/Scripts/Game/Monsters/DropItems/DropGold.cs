using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    int _goldAmount = 20;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        Destroy(gameObject, 7f);
    }
    public void Setting(int n)
    {
        _goldAmount = n;
    }

    private void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameScene.instance.PlayerGetGold(_goldAmount);
            Destroy(gameObject);
        }
    }

}
