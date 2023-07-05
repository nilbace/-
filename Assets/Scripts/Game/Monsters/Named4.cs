using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Named4 : MonoBehaviour
{
    [SerializeField] GameObject Raser;
    [SerializeField] GameObject Bullet;

    private void Start()
    {
        StartCoroutine(shoottwice());
    }

    IEnumerator shoottwice()
    {
        yield return new WaitForSeconds(1f);
    }
}
