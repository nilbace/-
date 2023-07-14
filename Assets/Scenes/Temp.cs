using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] float Waittime;
    [SerializeField] float RoatateValue;
    bool _aftertime;

    void Start()
    {
        StartCoroutine(RotateAfter30());
    }

    void Update()
    {
        if (_aftertime)
            transform.Rotate(0, 0, RoatateValue);

        print(RotateAfter30());
    }

    IEnumerator RotateAfter30()
    {
        yield return new WaitForSeconds(Waittime);
        _aftertime = true;
    
    }
}
