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
        explain.text = "2초 후에 설명창을 닫을 수 있습니다";
        yield return new WaitForSeconds(1f);
        explain.text = "1초 후에 설명창을 닫을 수 있습니다";
        yield return new WaitForSeconds(1f);
        explain.text = "아무 곳이나 눌러 창을 닫으세요";
        canClose = true;
    }

    public float minAlpha = 0.2f;
    public float maxAlpha = 1f;
    public float blinkSpeed = 1f;

    private bool increasingAlpha = true;

    private void Update()
    {
        // 현재 alpha 값을 가져옵니다.
        Color currentColor = explain.color;
        float currentAlpha = currentColor.a;

        // alpha 값이 최소값 또는 최대값에 도달하면 방향을 변경합니다.
        if (currentAlpha <= minAlpha)
        {
            increasingAlpha = true;
        }
        else if (currentAlpha >= maxAlpha)
        {
            increasingAlpha = false;
        }

        // alpha 값을 왕복시킵니다.
        float targetAlpha = increasingAlpha ? maxAlpha : minAlpha;
        float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime * blinkSpeed);

        // 새로운 alpha 값을 설정하여 TextMeshPro의 색상을 변경합니다.
        explain.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

        if(canClose)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                Managers.UI.ClosePopup();
            }
        }
    }
}
