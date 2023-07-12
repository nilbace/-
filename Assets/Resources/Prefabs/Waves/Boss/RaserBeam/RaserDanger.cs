using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaserDanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float duration = 1.0f;
    private bool isAnimating = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartAnimation();
    }

    private void Update()
    {
        if (isAnimating)
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            float alpha = Mathf.Lerp(0f, 0.5f, t);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        }
    }

    private void StartAnimation()
    {
        isAnimating = true;
    }
}
