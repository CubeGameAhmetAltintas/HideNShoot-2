using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogo : MonoBehaviour
{
    public float Value;
    [SerializeField] Material Material;

    void Update()
    {
        Material.SetFloat("_ShineLocation", Value);
    }
}
