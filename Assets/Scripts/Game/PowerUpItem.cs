using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    [SerializeField] GameObject Effect;
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
            TempSound.instance.SFX(TempSound.EffectSoundName.powerUp);
            collision.gameObject.GetComponent<Player>().PowerUp();



            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GameObject go = Instantiate(Effect);
            go.transform.position = Player.instance.gameObject.transform.position;
            go.transform.SetParent(Player.instance.gameObject.transform);

            Destroy(go, 2f);

            Destroy(gameObject, 4f);
        }
    }
}
