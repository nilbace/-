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
        if (MonsterHP < 0)
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
            SceneManager.LoadScene("StageSelect");
        }
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

