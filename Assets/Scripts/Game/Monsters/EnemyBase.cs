using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] float _myHP = -5;
    protected Vector3 _movedir;
    float _movespeed;
    public float MonsterHP { get { return _myHP; } set { _myHP = value; } }
    public bool IsBoss = false;
    public bool IsNamed = false;

    string ItemPath = "Prefabs/MonItems/";

    private void Start()
    {
        if(IsBoss)
        {
            GameScene.instance.BossAppear((int)_myHP, this);
        }
    }
    public void SetMonster(Define.WaveData waveData)
    {
        MonsterHP = waveData.MonsterHP;
        _movedir = waveData.WaveDir;
        _movespeed = waveData.WaveMoveSpeed;
    }

    public void ChangeDir(Vector3 newdir, float newSpeed)
    {
        _movedir = newdir;
        _movespeed = newSpeed;
    }

    void Update()
    {
        if (MonsterHP <= 0)
            Dead();

        else
            transform.position += _movedir.normalized * Time.deltaTime * _movespeed;

        float distance = Vector3.Distance(transform.position, Vector3.zero);
        if (distance > 10f)
        {
            Destroy(gameObject);
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
        if (IsBoss)
        {
            GameScene.instance.BossDead();
        }
    }

    

    private void OnDisable()
    {
        if (Time.timeScale != 0 && _myHP <= 0)
        {
            DropGolds();
            DropScores();
            if (IsNamed)
            {
                DropRandItem();
            }
        }

    }

    void DropRandItem()
    {
        int rand = Random.Range(1, 101);
        string path = "Prefabs/MonItems/";
        if(rand <= 10)
        {
            Instantiate(Resources.Load<GameObject>(path + "Potion"), transform.position, Quaternion.identity);
        }
        else if(rand <= 50)
        {
            Instantiate(Resources.Load<GameObject>(path + "Magnet"), transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(Resources.Load<GameObject>(path + "Bomb"), transform.position, Quaternion.identity);
        }
    }

    void DropScores()
    {
        if (IsNamed)
        {
            GameScene.instance.GetScore(1000);
        }
        else if (IsBoss)
        {
            GameScene.instance.GetScore(10000);
        }
        else
        {
            GameScene.instance.GetScore(100);
        }
    }


    void DropGolds()
    {
        if(IsNamed)
        {
            for(int i = 0; i<5; i++)
            {
                DropGold();
            }
        }
        else if(IsBoss)
        {
            for (int i = 0; i < 10; i++)
            {
                DropGold();
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                DropGold();
            }
        }
    }

    void DropGold()
    {
        string temppath = ItemPath + Define.DropItems.DropGold.ToString();
        float Randx = Random.Range(-0.5f, 0.5f);
        float Randy = Random.Range(-0.5f, -1.5f);
        Vector3 randPoz = new Vector3(Randx, Randy, 0) + transform.position;
        GameObject go = Instantiate(Resources.Load<GameObject>(temppath), randPoz, Quaternion.identity);

        go.GetComponent<DropGold>().Setting(10);
    }

    public void MonGetDamage(int n)
    {
        _myHP -= n;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!IsBoss)
            {
                GameScene.instance.PlayerGetDamage();
                gameObject.SetActive(false);
            }
            else
            {
                GameScene.instance.PlayerGetDamage();
            }
        }

    }
}

