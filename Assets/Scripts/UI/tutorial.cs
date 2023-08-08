using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tutorial : MonoBehaviour
{
    public Image BackImg;
    public TMP_Text explain;
    bool canClose = false;

    string path = "ETC/Tutorial/";
    
    public static tutorial instance;

    private void Awake()
    {
        instance = this;
    }

    public void Setting(Define.Tutorials tutorials)
    {
        BackImg.sprite = Resources.Load<Sprite>(path + tutorials.ToString());
    }

    
    void Start()
    {
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(1f);
        explain.text = "2�� �Ŀ� ����â�� ���� �� �ֽ��ϴ�";
        yield return new WaitForSeconds(1f);
        explain.text = "1�� �Ŀ� ����â�� ���� �� �ֽ��ϴ�";
        yield return new WaitForSeconds(1f);
        explain.text = "�ƹ� ���̳� ���� â�� ��������";
        canClose = true;
    }

    public float minAlpha = 0.2f;
    public float maxAlpha = 1f;
    public float blinkSpeed = 1f;

    private bool increasingAlpha = true;

    private void Update()
    {
        // ���� alpha ���� �����ɴϴ�.
        Color currentColor = explain.color;
        float currentAlpha = currentColor.a;

        // alpha ���� �ּҰ� �Ǵ� �ִ밪�� �����ϸ� ������ �����մϴ�.
        if (currentAlpha <= minAlpha)
        {
            increasingAlpha = true;
        }
        else if (currentAlpha >= maxAlpha)
        {
            increasingAlpha = false;
        }

        // alpha ���� �պ���ŵ�ϴ�.
        float targetAlpha = increasingAlpha ? maxAlpha : minAlpha;
        float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime * blinkSpeed);

        // ���ο� alpha ���� �����Ͽ� TextMeshPro�� ������ �����մϴ�.
        explain.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

        if(canClose)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                Time.timeScale = 1f;
                Managers.UI.ClosePopup();
            }
        }
    }
}
