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

    public void Initialize(List<Color> colors)
    {
        roadColors = colors;
    }

    public void OnValueChange()
    {
        sliderValue = 1f / roadColors.Count;
        for (int i = 0; i < roadColors.Count; i++)
        {
            if (sliderValue * i < colorSlider.value && colorSlider.value < sliderValue * i + 1)
            {
                CurrentColor = roadColors[i];
            }
        }

        player.OnColorChange(CurrentColor);
    }
}
