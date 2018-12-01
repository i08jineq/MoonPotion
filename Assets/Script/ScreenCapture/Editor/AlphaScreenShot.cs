using System.Collections;

using UnityEngine;
using UnityEditor;
namespace DarkLordGame
{
    public class AlphaScreenShot
    {

        [MenuItem("DarkLordGame/Screen Capture/CaptureScreen With Alpha")]
        public static void CaptureScreen()
        {
            int rand = Random.Range(0, 1000000);
            SaveScreenshotToFile("Design/ScreenShot With Alpha" + rand + ".png");
        }

        private static Texture2D Screenshot()
        {
            Camera camera = GameObject.FindObjectOfType<Camera>();
            int resWidth = camera.pixelWidth;
            int resHeight = camera.pixelHeight;

            RenderTexture rt = new RenderTexture(resWidth, resHeight, 32);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
            screenShot.alphaIsTransparency = true;

            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            screenShot.Apply();

            var colors = screenShot.GetPixels(0, 0, screenShot.width, screenShot.height);
            for (int y = 0; y < screenShot.height; y++)
            {
                for (int x = 0; x < screenShot.width; x++)
                {
                    Color c = colors[screenShot.width * y + x];
                    if (c.a > 0.0f)
                    {
                        c.r /= c.a;
                        c.g /= c.a;
                        c.b /= c.a;
                        colors[screenShot.width * y + x] = c;
                    }
                }
            }

            screenShot.SetPixels(colors);
            screenShot.Apply();

            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            GameObject.DestroyImmediate(rt);
            return screenShot;
        }

        private static Texture2D SaveScreenshotToFile(string fileName)
        {
            Texture2D screenShot = Screenshot();
            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(fileName, bytes);
            return screenShot;
        }
    }
}