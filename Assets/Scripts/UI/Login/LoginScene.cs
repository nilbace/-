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
        // ���� alpha ���� �����ɴϴ�.
        Color currentColor = text.color;
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
        text.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
    }
}
