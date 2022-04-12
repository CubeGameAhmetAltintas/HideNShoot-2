using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetter : MonoBehaviour
{
    public Material Material;
    public float Speed;
    public bool IsActive;
    float value = 1;

    private void Update()
    {
        if (IsActive)
        {
            value += Speed * Time.deltaTime;
            Material.SetTextureOffset("_BaseMap", new Vector2(1, value));
        }
    }

}
