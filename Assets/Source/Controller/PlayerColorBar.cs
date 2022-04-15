using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorBar : MonoBehaviour
{
    public Color CurrentColor;
    private float sliderValue;
    private Image imgColorBar;
    [SerializeField] PlayerController player;
    private List<Color> roadColors = new List<Color>();
    [SerializeField] Slider colorSlider;
    int colorIndex = 0;

    public void Initialize(Color color)
    {
        roadColors.Add(color);
    }

    public void OnValueChange()
    {
        sliderValue = 1f / roadColors.Count;
        foreach (var item in roadColors)
        {
            colorIndex = roadColors.IndexOf(item);
            if (sliderValue * colorIndex < colorSlider.value && colorSlider.value < sliderValue * colorIndex + 1)
            {
                CurrentColor = roadColors[colorIndex];
            }
        }

        player.OnColorChange(CurrentColor);
    }
}
