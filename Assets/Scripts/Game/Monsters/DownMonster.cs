using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMonster : EnemyBase
{
    [SerializeField] Vector3 newPoz;
    [SerializeField] float ChangeDirTime;
    private void Start()
    {
        base.Start();
        StartCoroutine(GoDown());
    }

    IEnumerator GoDown()
    {
        yield return new WaitForSeconds(ChangeDirTime);
        _movedir = newPoz;
    }
}
