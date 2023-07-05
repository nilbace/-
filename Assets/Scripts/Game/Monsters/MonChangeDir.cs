using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonChangeDir : MonoBehaviour
{
    [SerializeField] Vector3 newDir;
    [SerializeField] float ChangeDirTime;
    [SerializeField] float newSpeed;
    [SerializeField] float changeDirDelay;

    EnemyBase _EnemyBase;
    private void Start()
    {
        _EnemyBase = GetComponent<EnemyBase>();
        StartCoroutine(changerdir());
    }

    IEnumerator changerdir()
    {
        yield return new WaitForSeconds(changeDirDelay);
        _EnemyBase.ChangeDir(newDir, newSpeed);
    }
}
