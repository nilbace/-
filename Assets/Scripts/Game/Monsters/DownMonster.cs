using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMonster : EnemyBase
{
    [SerializeField] Vector3 newPoz;
    private void Start()
    {
        StartCoroutine(GoDown());
    }

    IEnumerator GoDown()
    {
        yield return new WaitForSeconds(1f);
        _movedir = newPoz;
    }


}
