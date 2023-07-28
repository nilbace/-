using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public enum ItemName {Magnet, Potion, Bomb }
    public ItemName thisItem;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject Effect;

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
                    TempSound.instance.SFX(TempSound.EffectSoundName.item);
                    break;

                case ItemName.Potion:
                    GameScene.instance.AddHeart();
                    TempSound.instance.SFX(TempSound.EffectSoundName.item);
                    break;
                    

                case ItemName.Bomb:
                    GameScene.instance.AddBomb();
                    TempSound.instance.SFX(TempSound.EffectSoundName.item);
                    break;
            }
            StartCoroutine(itemPickUP());
        }
    }

    IEnumerator itemPickUP()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<CircleCollider2D>().enabled = false;
        GameObject go =  Instantiate(Effect, transform.position, Quaternion.identity);
        if(thisItem == ItemName.Magnet)
        {
            go.transform.SetParent(Player.instance.transform);
            go.transform.position = Player.instance.gameObject.transform.position;
        }
        yield return new WaitForSeconds(5f);
        Destroy(go); Destroy(gameObject);
    }
}
