using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] float _myHP = -5;
    protected Vector3 _movedir;
    float _movespeed;
    public float MonsterHP { get { return _myHP; } set { _myHP = value; } }
    void Start()
    {
        Destroy(gameObject, 7f);
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
        if (MonsterHP < 0)
            gameObject.SetActive(false);
        else
            transform.position += _movedir.normalized * Time.deltaTime * _movespeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            _myHP--;
        }
    }
}

