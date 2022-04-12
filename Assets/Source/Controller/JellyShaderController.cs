using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyShaderController : ControllerBaseModel
{
    [SerializeField] Material material;
    [SerializeField] float speed;
    [SerializeField] float value;


    private void Update()
    {
        value += speed * Time.deltaTime;
        if (value >= 1000)
            value = 0;

        material.SetFloat("_Frequency", value);
    }
}
