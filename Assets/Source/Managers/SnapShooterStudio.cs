using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SnapShooterStudio : ObjectModel
{
    [SerializeField] RenderTexture renderTexture;
    public int Index;
    public int SnappedShood;
    public bool UseObjectNames;
    string fileName;
    string screenShootPath;

    public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes = _texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(_fullPath, _bytes);
        Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + _fullPath);
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBAFloat, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public void GetFileName(string file)
    {
        if (System.IO.File.Exists(file))
        {
            SnappedShood++;
            GetFileName(screenShootPath + "/ScreenShoot_" + Screen.width.ToString() + "x" + Screen.height.ToString() + "_" + SnappedShood + ".png");
        }
        else
        {
            fileName = screenShootPath + "/ScreenShoot_" + Screen.width.ToString() + "x" + Screen.height.ToString() + "_" + SnappedShood + ".png";
        }
    }

    [EditorButton]
    public void TakeSnapShoot()
    {
        Texture2D tex = toTexture2D(renderTexture);
        screenShootPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\" + "ScreenShoots_" + Application.productName + "\\ScreenShoot_" + Screen.width.ToString() + "x" + Screen.height.ToString();

        if (!System.IO.Directory.Exists(screenShootPath))
        {
            System.IO.Directory.CreateDirectory(screenShootPath);
        }

        if (UseObjectNames)
        {
            if (Index - 1 > 0)
            {
                fileName = screenShootPath + "/" + transform.GetChild(Index - 1).gameObject.name + ".png";
            }
            else
            {
                fileName = screenShootPath + "/" + "empty" + ".png";
            }
        }
        else
        {
            GetFileName(screenShootPath + "/ScreenShoot_" + Screen.width.ToString() + "x" + Screen.height.ToString() + "_" + SnappedShood + ".png");
        }

        SaveTextureAsPNG(tex, fileName);
    }


    IEnumerator StartShooting()
    {
        transform.GetChild(Index).gameObject.SetActive(true);
        TakeSnapShoot();

        if (Index + 1 < transform.childCount)
        {
            yield return new WaitForSeconds(1);
            transform.GetChild(Index).gameObject.SetActive(false);
            Index++;

            yield return StartCoroutine(StartShooting());
        }

        yield return null;
    }
}