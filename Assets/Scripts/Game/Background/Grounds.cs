using UnityEngine;
using UnityEngine.UI;

public class Grounds: MonoBehaviour
{
    public Image image;
    public float minYPosition = -1000f;
    public float scrollAmount = 1156.8f * 3f;
    public float moveSpeed;

    private void Update()
    {
        Vector2 CurrentPosition = image.rectTransform.anchoredPosition;
        CurrentPosition.y -= moveSpeed * Time.deltaTime;
        image.rectTransform.anchoredPosition = CurrentPosition;

        if (image.rectTransform.anchoredPosition.y < minYPosition)
        {
            Vector2 currentPosition = image.rectTransform.anchoredPosition;
            currentPosition.y += scrollAmount;
            image.rectTransform.anchoredPosition = currentPosition;
        }
    }
}
