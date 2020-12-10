using UnityEngine;
using System.IO;
using Dominion.Enums;
using Dominion.Enums.Design;

namespace Dominion.FunctionLibrary
{

#region UI & DTouchSystem
    public static class UIFunctionLibrary
    {
        public static Vector2 ImageSeizeSetExpand(Vector2 panelSize, Vector2 imgSize)
        {
            Vector2 result;
            
            float insPanelRatio = panelSize.y / panelSize.x;
            float insImgRatio = (float)imgSize.y / (float)imgSize.x;

            if (insPanelRatio >= insImgRatio)
            {
                // Image long on Horizontal
                result = new Vector2((panelSize.y * imgSize.x) / imgSize.y, panelSize.y);
            }
            else
            {
                // Image long on Vertical 
                result = new Vector2(panelSize.x, (panelSize.x * imgSize.y) / imgSize.x);
            }
            return result;
        }
        public static Vector2 ImageSeizeSetShrink(Vector2 panelSize, Vector2 imgSize)
        {
            Vector2 result;

            float insPanelRatio = panelSize.y / panelSize.x;
            float insImgRatio = (float)imgSize.y / (float)imgSize.x;

            if (insPanelRatio >= insImgRatio)
            {
                // Image long on Horizontal
                result = new Vector2( panelSize.x, (panelSize.x * imgSize.y) / imgSize.x);
            }
            else
            {
                // Image long on Vertical 
                result = new Vector2((panelSize.y * imgSize.x) / imgSize.y, panelSize.y);
            }
            return result;
        }
        // Need to V/H Expend/Shrink



    }
#endregion


#region D-GCVision

    public static class ImageConvert
    {
        public static string ConvertTexture2dToByte(Texture2D texture)
        {
            return System.Convert.ToBase64String(texture.EncodeToJPG()); // .EncodeToJPG .EncodeToPNG
        }
        public static Texture2D GetTextureFromPath(string path)
        {
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            
            texture.LoadImage(File.ReadAllBytes(path));
            texture.Apply();

            return texture;
        }
        public static Texture2D GetTextureFromARCamera(RenderTexture renderTexture)
        {
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

            return texture;
        }
        public static Texture2D GetTextureFromScreenShot()
        {
            Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
            return texture;
        }
        public static Texture2D ResizeTexture2D(Texture2D insTexture, Vector2 insSize)
        {
            // Check  insTexture.isReadable = true;
            Texture2D texture = insTexture;
            texture.Resize((int)insSize.x, (int)insSize.y);
            return texture;
        }
        public static bool isVerticalImage(Texture2D insTexture)
        {
            bool ins;
            if (insTexture.width > insTexture.height) ins = false;
            else ins = true;
            return ins;
        }
    }



    public static class ResponseConverter
    {
        public static Color32 ConvertToUnityColor(Dominion.GCVision.Color color)
        {
            Color32 unityColor = new Color32((byte)color.red, (byte)color.green, (byte)color.blue, (byte)(255 - color.alpha));

            return unityColor;
        }
        public static Vector3 ConvertToUnityVector3(Dominion.GCVision.Position position)
        {
            Vector3 vector3 = new Vector3((float)position.x, (float)position.y, (float)position.z);

            return vector3;
        }

        public static Vector2 ConvertToUnityVector2(Dominion.GCVision.Vertex vertex)
        {
            Vector2 vector2 = new Vector2((float)vertex.x, (float)vertex.y);

            return vector2;
        }

    }
    #endregion


#region UnitConvert
    public static class UnitConvert
    {
        public static int quater = 25;
        public static float InchiToMilli (this float inchi)
        {
            return inchi * 0.0254f;
        }

        public static Vector3 InchiToMilli (this Vector3 inchi)
        {
            return inchi * 0.0254f;
        }

        public static float MeterToInchi (this float meter)
        {
            float inch = meter * 39.3701f;
            int wholes = (int)inch;
            float decialpoints = inch - wholes;

            int value = (int)(100f * decialpoints) / 25;
            inch = wholes + value * 0.25f;
            return inch;
        }

        public static Vector3 MeterToInchi (this Vector3 meter)
        {
            return meter * 39.3701f;
        }

        public static float MeterToMilli (this float meter)
        {
            return meter * 0.001f;
        }

        public static Vector3 MeterToMilli (this Vector3 meter)
        {
            return meter * 0.001f;
        }


        public static Vector3 MilliToInchi (this Vector3 milli)
        {
            return milli * 25.4f;
        }

        public static float MilliToInchi (this float milli)
        {
            return milli * 25.4f;
        }

        public static Vector2 InchiToMilli (this Vector2 inchi)
        {
            return inchi * 0.0254f;
        }

    }
    #endregion

}
