using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public enum ItemName {Magnet, Potion, Bomb }
    public ItemName thisItem;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        Destroy(gameObject, 8f);
    }

    private void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            switch (thisItem)
            {
                
                case ItemName.Magnet:
                    Magnet.instance.OnMagnet(5);
                        
                    break;

                case ItemName.Potion:
                    GameScene.instance.AddHeart();
                    break;
                    

                case ItemName.Bomb:
                    GameScene.instance.AddBomb();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
