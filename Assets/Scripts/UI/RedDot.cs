using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDot : MonoBehaviour
{
    [SerializeField] GameObject Dot;

    public static RedDot instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Init();   
    }

    public void Init()
    {
        if(Managers.Data.MyMailData.MailAmount>0)
        {
            Dot.SetActive(true);
        }
        else
        {
            Dot.SetActive(false);
        }
    }
}
