using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorBar : MonoBehaviour
{
    public Color CurrentColor;
    [SerializeField] Image imgColorBar;
    [SerializeField] PlayerController player;
    [SerializeField] Slider colorSlider;
    [SerializeField] List<Color> targetColors;
    Texture2D currentTexture;

    public void Initialize(List<Color> colors)
    {
        targetColors = colors;
        int diff = (914 / colors.Count);
        List<Color> rangeColor = new List<Color>();
        for (int i = 0; i < colors.Count; i++)
        {
            for (int j = 0; j < diff; j++)
            {
                if (i == 0)
                {
                    float value = (float)j / (float)diff;
                    if (value < 0.25)
                    {
                        rangeColor.Add(colors[i]);
                    }
                    else
                    {
                        float percent = (value - 0.25f) / 0.75f;
                        rangeColor.Add(Helpers.Colors.GetColorWithPercent(colors[i], colors[i + 1], percent * 0.5f));
                    }
                }
                else if (i == colors.Count - 1)
                {
                    float value = (float)j / (float)diff;
                    if (value > 0.75f)
                    {
                        rangeColor.Add(colors[i]);
                    }
                    else
                    {
                        float percent = ((value / 0.75f) * 0.5f) + 0.5f;
                        rangeColor.Add(Helpers.Colors.GetColorWithPercent(colors[i - 1], colors[i], percent));
                    }
                }
                else
                {
                    float value = (float)j / (float)diff;
                    if (value < 0.25f)
                    {
                        float percent = ((value / 0.25f) * 0.5f) + 0.5f;
                        rangeColor.Add(Helpers.Colors.GetColorWithPercent(colors[i - 1], colors[i], percent));
                    }
                    else if (value > 0.75f)
                    {
                        float percent = ((value - 0.75f) / 0.25f) * 0.5f;
                        rangeColor.Add(Helpers.Colors.GetColorWithPercent(colors[i], colors[i + 1], percent));
                    }
                    else
                    {
                        rangeColor.Add(colors[i]);
                    }
                }
            }
        }

        currentTexture = new Texture2D(rangeColor.Count, 1);
        currentTexture.SetPixels(rangeColor.ToArray());
        currentTexture.wrapMode = TextureWrapMode.Clamp;
        currentTexture.Apply();

        Sprite sprColorView = Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), Vector2.zero);
        imgColorBar.sprite = sprColorView;
        OnValueChange();
    }


    public void OnValueChange()
    {

        if (GameplayTypeController.CurrentType == GameplayTypes.Running)
        {
            player.OnColorChange(currentTexture.GetPixel((int)(currentTexture.width * colorSlider.value), 1));
        }
    }
}
