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
            // �ڽ� ���ӿ�����Ʈ�� ��� ���� �ڽĵ� �����Ϸ��� �Ʒ� �ּ� ����
            //DestroyChildrenObjects(child);
            Destroy(child.gameObject);
        }
    }

    void tempAdd()
    {
        Managers.Data.MakeAndAddMail(500000, 500, 50, 5, 5, "������ ġƮ");
    }

    public void CLoseBTN()
    {
        Managers.UI.ClosePopup();
    }
}
