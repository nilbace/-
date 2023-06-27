using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScreen : MonoBehaviour
{
    public int screenWidth = 180;
    public int screenHeight = 320;

    private void Start()
    {
        SetWindowResolution(screenWidth, screenHeight);
    }

    private void SetWindowResolution(int width, int height)
    {
        Screen.SetResolution(width, height, FullScreenMode.Windowed);
    }
}
