using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : ControllerBaseModel
{
    public VisualEnvironmentModel[] VisualEnvironments;
    [SerializeField] Material bgMaterial;


    public void LoadVisaulEnvironment()
    {
        VisualEnvironmentModel visualEnvironment = VisualEnvironments[PlayerDataModel.Data.EnvironmentId];
        bgMaterial.SetTexture("_MainTex", visualEnvironment.BG);
        RenderSettings.fogColor = visualEnvironment.FogColor;
    }

    public void IncreaseEnvironmentId()
    {
        PlayerDataModel.Data.EnvironmentId = PlayerDataModel.Data.EnvironmentId + 1 < VisualEnvironments.Length ? PlayerDataModel.Data.EnvironmentId + 1 : 0;
    }
}

[System.Serializable]
public class VisualEnvironmentModel
{
    public Texture2D BG;
    public Color FogColor;
}
