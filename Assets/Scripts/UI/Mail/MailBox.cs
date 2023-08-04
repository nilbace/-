using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour
{
    [SerializeField] Transform content;
    public static MailBox instance;
    string path = "Prefabs/UI/Mail";

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
        DestroyChildrenObjects(content);
        for (int i = 0; i < Managers.Data.MyMailData.MailAmount; i++)
        {
            GameObject mail = Instantiate(Resources.Load<GameObject>(path));
            mail.transform.SetParent(content, false);

            OneMail temp = Managers.Data.MyMailData.MailBox[i];
            mail.GetComponent<Mail>().Setting(temp, i);

        }
    }

    private void DestroyChildrenObjects(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            // 자식 게임오브젝트의 모든 하위 자식도 삭제하려면 아래 주석 해제
            //DestroyChildrenObjects(child);
            Destroy(child.gameObject);
        }
    }
    [ContextMenu("치트")]

    void tempAdd()
    {
        Managers.Data.MakeAndAddMail(500000, 500, 50, 5, 5,5, "개발자 치트");
    }

    public void CLoseBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }
}
