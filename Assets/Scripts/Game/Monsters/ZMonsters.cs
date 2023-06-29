using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMonsters : EnemyBase
{
    [SerializeField] float startDelay;
    [SerializeField] float movedelay;
    private void Start()
    {
        StartCoroutine(GoZ());
    }

    IEnumerator GoZ()
    {
        yield return new WaitForSeconds(startDelay);
        _movedir = new Vector3(1, -1, 0);

        yield return new WaitForSeconds(movedelay);
        _movedir = new Vector3(-1, -1, 0);

        yield return new WaitForSeconds(movedelay);
        _movedir = new Vector3(1, -1, 0);
    }
}
