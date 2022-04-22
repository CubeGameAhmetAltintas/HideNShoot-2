using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentModel : ObjectModel
{
    public int Id;

    public void Initialize(WorldItemDataModel data)
    {
        transform.position = data.Position;
        transform.rotation = data.Rotation;

        SetActive();
    }
}
