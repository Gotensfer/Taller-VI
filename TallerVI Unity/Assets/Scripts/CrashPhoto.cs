using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CrashPhoto : MonoBehaviour
{
    private string directoryName = "Assets/Resources";
    private string fileName = "TestImage.png", fullPath = "";
    public Image display;
    private bool canDisplay = false;

    private void Update()
    {
        //Display(fullPath);
    }

    public void TakeScreenshot()
    {
        DirectoryInfo screenshotDirectory = Directory.CreateDirectory(directoryName);
        fullPath = Path.Combine(screenshotDirectory.FullName, fileName);

        ScreenCapture.CaptureScreenshot(fullPath);
        
        canDisplay = true;
    }

    void Display(string path)
    {
        if (canDisplay)
        {
            display.sprite = Resources.Load(fullPath, typeof(Sprite)) as Sprite;
        }
    }
}
