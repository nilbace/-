using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMonster : EnemyBase
{
    private void Start()
    {
        StartCoroutine(GoDown());
    }

    IEnumerator GoDown()
    {
        yield return new WaitForSeconds(1f);
        _movedir = new Vector3(0, -1, 0);
    }


}
