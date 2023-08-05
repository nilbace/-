using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //적과 닿으면 반납
    //화면 밖으로 벗어나면 반납
    //직진하는 코드(바라보는 방향에 따라 달라짐)

    [SerializeField] float _moveSpeed;
    private Camera mainCamera;
    int _damage;
    int critper;
    int critDMG;

    private void Start()
    {
        mainCamera = Camera.main;
        critDMG = Player.instance.resultStats[(int)Define.StatName.CritDmg];
        critper = Player.instance.gameObject.GetComponent<StatManager>().playerdata.Chars[Managers.Data.MyCharDatas.nowSelectCatIndex].baseCritPer
            +Managers.Data.GetPetResultStat().petCritper;
    }
    public void SetBullet(Vector3 playerposition, float rotation, int damage, Sprite spriteee)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        transform.position = playerposition;

        _damage = CalculateCritDamage(damage, critper, critDMG);

        GetComponent<SpriteRenderer>().sprite = spriteee;
    }

    public int CalculateCritDamage(int damage, int critPer, int critDMG)
    {
        int roll = Random.Range(1, 101); // Generate a random number between 1 and 100

        // Check if the roll is within the critPer range
        if (roll <= critPer)
        {
            // Calculate the crit damage and add it to the original damage
            int critDamage = damage * critDMG / 100;
            damage = critDamage;
        }

        return damage;
    }


    private void FixedUpdate()
    {
        if(gameObject.activeInHierarchy)
        {
            transform.Translate(0f, _moveSpeed * Time.deltaTime, 0f);
        }

        if (!IsVisibleOnCamera())
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsVisibleOnCamera()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0f || viewportPosition.x > 1f || viewportPosition.y < 0f || viewportPosition.y > 1f)
        {
            return false;
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().MonGetDamage(_damage);
            gameObject.SetActive(false);
        }    
    }
}
