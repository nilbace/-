using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    public TMP_Text text;
    public float minAlpha = 0.2f;
    public float maxAlpha = 1f;
    public float blinkSpeed = 1f;

    private bool increasingAlpha = true;

    private void Update()
    {
        // 현재 alpha 값을 가져옵니다.
        Color currentColor = text.color;
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
        text.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
    }
}
