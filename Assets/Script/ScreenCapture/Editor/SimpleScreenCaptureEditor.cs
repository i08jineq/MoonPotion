using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace DarkLordGame
{
    public class SimpleScreenCaptureEditor
    {
        [MenuItem("DarkLordGame/Screen Capture/Simple CaptureScreen")]
        public static void CaptureScreen()
        {
            int rand = Random.Range(0, 1000000);
            ScreenCapture.CaptureScreenshot("Design/CapturedScreen" + rand + ".png");
        }
    }
}