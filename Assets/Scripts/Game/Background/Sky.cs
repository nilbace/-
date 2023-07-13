using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sky : MonoBehaviour
{
    public Image image;
    public float minYPosition = -1000f;
    public float scrollAmount = 2000f * 2f;
    public float moveSpeed;

    private void Update()
    {
        Vector2 CurrentPosition = image.rectTransform.anchoredPosition;
        CurrentPosition.x -= moveSpeed * Time.deltaTime;
        image.rectTransform.anchoredPosition = CurrentPosition;

        if (image.rectTransform.anchoredPosition.x < minYPosition)
        {
            Vector2 currentPosition = image.rectTransform.anchoredPosition;
            currentPosition.x += scrollAmount;
            image.rectTransform.anchoredPosition = currentPosition;
        }
    }
}
