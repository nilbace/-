using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UIManager 
{
    Stack<GameObject> _popupStack = new Stack<GameObject>();
    string path = "Prefabs/UI/";

    public void ShowPopup(Popup popupName)
    {
        string temppath = path + popupName.ToString();
        GameObject go = Resources.Load<GameObject>(temppath);
        
        GameObject popupinstance = MonoBehaviour.Instantiate(go);

        _popupStack.Push(popupinstance);

        popupinstance.GetComponent<Canvas>().sortingOrder = _popupStack.Count + 20;
    }

    public void ClosePopup()
    {
        if(RedDot.instance != null)
        {
            RedDot.instance.Init();
        }
        MonoBehaviour.Destroy(_popupStack.Pop());

    }
    
}
